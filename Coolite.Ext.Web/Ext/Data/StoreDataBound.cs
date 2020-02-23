/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class StoreDataBound : StoreBase
    {
        public event EventHandler DataBound;
		
		object dataSource;
		bool initialized;
		bool preRendered;
		bool requiresDataBinding;
        DataSourceSelectArguments selectArguments;
        DataSourceView currentView;
        
        [Bindable (true)]
		[Themeable (false)]
		[DefaultValueAttribute (null)]
		[DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility.Hidden)]
		public virtual object DataSource 
        {
			get 
            {
				return this.dataSource;
			}
			set 
            {
				ValidateDataSource (value);
				this.dataSource = value;
                this.IsDataBound = false;
                if (this.initialized)
                {
                    this.OnDataPropertyChanged();
                }
			}
		}
		
		[DefaultValueAttribute ("")]
		[ThemeableAttribute (false)]
        [IDReferencePropertyAttribute(typeof(DataSourceControl))]
		public virtual string DataSourceID {
			get 
            {
                return (string)this.ViewState["DataSourceID"] ?? "";
			}
			set 
            {
				this.ViewState["DataSourceID"] = value;
                this.IsDataBound = false;
                if (initialized)
                {
                    this.OnDataPropertyChanged();
                }
			}
		}

        [ThemeableAttribute(false)]
        [DefaultValueAttribute("")]
        public virtual string DataMember
        {
            get
            {
                return (string)this.ViewState["DataMember"] ?? "";
            }
            set 
            { 
                ViewState["DataMember"] = value; 
            }
        }
        
        protected bool Initialized {
			get { return initialized; }
		}
		
		protected bool IsBoundUsingDataSourceID {
			get { return DataSourceID.Length > 0; }
		}

        protected bool RequiresDataBinding {
			get 
            { 
                return this.requiresDataBinding; 
            }
			set 
            { 
				this.requiresDataBinding = value;
                if (value && this.preRendered && this.IsBoundUsingDataSourceID && this.Page != null && !this.Page.IsCallback && !Ext.IsAjaxRequest)
                {
                    this.EnsureDataBound();
                }
			}
		}
		
		protected void ConfirmInitState()
		{
			this.initialized = true;
		}

        private bool ajaxDataBindingRequired = true;

        private void AjaxDataBind()
        {
            if (Ext.IsAjaxRequest && this.IsAjaxRequestInitiator)
            {
                return;
            }

            if (!this.ajaxDataBindingRequired || this.IsParentDeferredRender)
            {
                return;
            }

            this.RequiresDataBinding = false;
            this.PerformSelect();

            this.GenerateAjaxResponseScript();
        }

        private void GenerateAjaxResponseScript()
        {
            StoreResponseData dataResponse = new StoreResponseData();
            dataResponse.Data = this.Data != null ? JSON.Serialize(this.Data) : this.JsonData;
            DataSourceProxy dsp = this.Proxy.Proxy as DataSourceProxy;

            if (dsp == null && this.Proxy.Proxy != null)
            {
                return;
            }

            dataResponse.TotalCount = dsp != null ? dsp.TotalCount : 0;

            Response response = new Response(true);
            response.Data = dataResponse.ToString();

            this.AddScript(string.Concat(this.ClientID, ".callbackRefreshHandler(response, {serviceResponse: ", new ClientConfig().Serialize(response), "}, ", this.ClientID, ", o.eventType, o.action, o.extraParams);"));
            this.ajaxDataBindingRequired = false;
        }

        public override void DataBind ()
		{
			base.DataBind();

            if(this.IsDataBound)
            {
                return;
            }

            if(Ext.IsAjaxRequest && !this.IsAjaxRequestInitiator)
			{
			    this.AjaxDataBind();
			}

            if ((!Ext.IsAjaxRequest && this.Proxy.Count == 0) || (Ext.IsAjaxRequest && this.IsAjaxRequestInitiator))
            {
                this.RequiresDataBinding = false;
                this.PerformSelect();
            }
		}

		protected virtual void EnsureDataBound()
		{
            if(this.RequiresDataBinding && this.IsBoundUsingDataSourceID && this.Proxy.Count == 0)
            {
                this.DataBind();
            }
		}
		
		protected virtual void OnDataBound (EventArgs e)
		{
            if (this.DataBound != null)
            {
                this.DataBound(this, e);
            }
		}

		protected virtual void OnDataPropertyChanged()
		{
			this.RequiresDataBinding = true;
            this.UpdateViewData();
		}
		
		protected override void OnInit (EventArgs e)
		{
			base.OnInit (e);
			this.Page.PreLoad += OnPagePreLoad;

            if (!IsViewStateEnabled && Page != null && Page.IsPostBack)
            {
                this.RequiresDataBinding = true;
            }
		}
		
		protected virtual void OnPagePreLoad(object sender, EventArgs e)
		{
			this.ConfirmInitState ();
            this.UpdateViewData();
		}
		
		protected override void OnPreRender (EventArgs e)
		{
			this.preRendered = true;
			if(!Ext.IsAjaxRequest)
			{
                this.EnsureDataBound(); 
			}
			base.OnPreRender (e);
		}

        public static IEnumerable ResolveDataSource(object o, string data_member)
        {
            IEnumerable ds;

            ds = o as IEnumerable;
            if (ds != null)
                return ds;

            IListSource ls = o as IListSource;

            if (ls == null)
            {
                return null;
            }

            IList member_list = ls.GetList();
            if (!ls.ContainsListCollection)
            {
                return member_list;
            }

            ITypedList tl = member_list as ITypedList;
            if (tl == null)
            {
                return null;
            }

            PropertyDescriptorCollection pd = tl.GetItemProperties(new PropertyDescriptor[0]);

            if (pd == null || pd.Count == 0)
                throw new HttpException("The selected data source did not contain any data members to bind to");

            PropertyDescriptor member_desc = data_member == "" ?
                pd[0] :
                pd.Find(data_member, true);

            if (member_desc != null)
            {
                ds = member_desc.GetValue(member_list[0]) as IEnumerable;
            }

            if (ds == null)
            {
                throw new HttpException("A list corresponding to the selected DataMember was not found");
            }

            return ds;
        }

		
        protected virtual IDataSource GetDataSource()
        {
            if (this.IsBoundUsingDataSourceID)
            {
                Control ctrl = Coolite.Utilities.ControlUtils.FindControl(this, this.DataSourceID);

                if (ctrl == null)
                {
                    throw new HttpException(string.Format("A IDatasource Control with the ID '{0}' could not be found.", this.DataSourceID));
                }

                if (!(ctrl is IDataSource))
                {
                    throw new HttpException(string.Format("The control with ID '{0}' is not a control of type IDataSource.", this.DataSourceID));
                }

                return (IDataSource)ctrl;
            }

            if (this.DataSource == null)
            {
                return null;
            }

            IDataSource ds = this.DataSource as IDataSource;
            
            if (ds != null)
            {
                return ds;
            }

            System.Collections.IEnumerable ie = ResolveDataSource(DataSource, DataMember);

            if (ie != null)
            {
                return new CollectionDataSource(ie);
            }

            throw new HttpException(string.Format("Unexpected data source type: {0}", DataSource.GetType()));
        }

        protected virtual DataSourceView GetData()
        {
            if (this.currentView == null)
            {
                this.UpdateViewData();
            }
            return currentView;
        }

        DataSourceView InternalGetData()
        {
            if (this.DataSource != null && this.IsBoundUsingDataSourceID)
            {
                throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
            }

            IDataSource ds = this.GetDataSource();
            if (ds != null)
            {
                return ds.GetView(DataMember);
            }
            
            return null;
        }

        protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
        {
            this.RequiresDataBinding = true;
        }

        void UpdateViewData()
        {
            DataSourceView view = this.InternalGetData();
            if (view == currentView)
            {
                return;
            }

            if (currentView != null)
            {
                currentView.DataSourceViewChanged -= OnDataSourceViewChanged;
            }

            currentView = view;

            if (view != null)
            {
                view.DataSourceViewChanged += OnDataSourceViewChanged;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.Page.IsPostBack || (this.IsViewStateEnabled && !this.IsDataBound))
            {
                this.RequiresDataBinding = true;
            }

            base.OnLoad(e);
        }

        protected internal virtual void PerformDataBinding(IEnumerable data)
        {
            if(data == null)
            {
                this.JsonData = "[]";
                return;
            }
            this.firstRecord = null;
            IEnumerator en = data.GetEnumerator();
            AutoGeneratedFieldProperties[] autoFieldProperties = this.CreateAutoFieldProperties(data, en);
            if (autoFieldProperties != null)
            {
                StringBuilder sb = new StringBuilder(256);
                sb.Append("[");

                if(this.firstRecord != null)
                {
                    this.BindRecord(autoFieldProperties, sb, this.firstRecord);
                }

                while(en.MoveNext())
                {
                    object obj = en.Current;
                    this.BindRecord(autoFieldProperties, sb, obj);
                }

                RemoveLastComma(sb);
                sb.Append("]");

                this.JsonData = sb.ToString();
            }
        }

        private void BindRecord(AutoGeneratedFieldProperties[] autoFieldProperties, StringBuilder sb, object obj)
        {
            sb.Append("{");
            foreach (AutoGeneratedFieldProperties property in autoFieldProperties)
            {
                FieldInReader field = this.IsInReader(property.DataField);
                if (this.IgnoreExtraFields && !field.InReader)
                {
                    continue;
                }

                if(field.Fields != null && field.Fields.Count>0)
                {
                    foreach (RecordField recordField in field.Fields)
                    {
                        object value = this.GetFieldValue(property, obj, recordField);
                        sb.AppendFormat("{0}:{1},", JSON.Serialize(string.IsNullOrEmpty(recordField.Mapping) ? recordField.Name : recordField.Mapping), JSON.Serialize(value));
                    }
                }
                else
                {
                    sb.AppendFormat("{0}:{1},", JSON.Serialize(property.DataField), JSON.Serialize(DataBinder.GetPropertyValue(obj, property.DataField)));
                }
            }
            RemoveLastComma(sb);
            sb.Append("},");
        }

        private object GetFieldValue(AutoGeneratedFieldProperties property, object obj, RecordField field)
        {
            if (field!= null && !string.IsNullOrEmpty(field.ServerMapping))
            {
                string[] mapping = field.ServerMapping.Split('.');
                if (mapping.Length > 1)
                {
                    for (int i = 0; i < mapping.Length; i++)
                    {
                        PropertyInfo p = obj.GetType().GetProperty(mapping[i]);
                        obj = p.GetValue(obj, null);
                        if (obj == null)
                        {
                            return null;
                        }
                    }

                    return obj;
                }
            }

            return DataBinder.GetPropertyValue(obj, property.DataField);
        }

        private FieldInReader IsInReader(string name)
        {
            if (this.Reader.Reader == null)
            {
                return new FieldInReader(false, null);
            }

            bool found = false;

            JsonReader jr = this.Reader.Reader as JsonReader;
            if (jr != null && jr.ReaderID == name)
            {
                found = true;
            }

            XmlReader xr = this.Reader.Reader as XmlReader;
            if (xr != null && xr.ReaderID == name)
            {
                found = true;
            }
            List< RecordField> fields = new List<RecordField>();
            foreach (RecordField field in this.Reader.Reader.Fields)
            {
                if ((!string.IsNullOrEmpty(field.ServerMapping) && field.ServerMapping.Split('.')[0] == name) ||
                    ((string.IsNullOrEmpty(field.Mapping) ? field.Name : field.Mapping) == name))
                {
                   fields.Add(field);
                }
            }

            if(fields.Count >0)
            {
                return new FieldInReader(true, fields);
            }

            if(found)
            {
                return new FieldInReader(true, null);
            }

            return new FieldInReader(false, null);
        }

        private bool IsComplexField(string name)
        {
            if (this.Reader.Reader == null)
            {
                return false;
            }

            foreach (RecordField field in this.Reader.Reader.Fields)
            {
                if ((!string.IsNullOrEmpty(field.ServerMapping) && field.ServerMapping.Split('.')[0] == name))
                {
                    return true;
                }

                if (name == (string.IsNullOrEmpty(field.Mapping) ? field.Name : field.Mapping))
                {
                    return field.IsComplex;
                }
            }

            return false;
        }

        private static void RemoveLastComma(StringBuilder sb)
        {
            if(sb[sb.Length-1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }
        }

        protected static void ValidateDataSource(object dataSource)
        {
            if (dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
                return;
            throw new ArgumentException("Invalid data source source type. The data source must be of type IListSource, IEnumerable or IDataSource.");
        }

        protected void PerformSelect()
        {
            if (!this.IsBoundUsingDataSourceID)
            {
                this.OnDataBinding(EventArgs.Empty);
            }

            DataSourceView view = GetData();
            if (view != null)
            {
                view.Select(this.SelectArguments, this.OnSelect);
                this.MarkAsDataBound();
            }
        }

        void OnSelect(IEnumerable data)
        {
            this.PerformDataBinding(data);
            this.OnDataBound(EventArgs.Empty);
        }

        protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
        {
            return DataSourceSelectArguments.Empty;
        }

        protected DataSourceSelectArguments SelectArguments
        {
            get
            {
                if (this.selectArguments == null)
                {
                    this.selectArguments = this.CreateDataSourceSelectArguments();
                }
                return this.selectArguments;
            }
        }

        private bool isDataBound;
        private object firstRecord;

        private bool IsDataBound
        {
            get
            {
                return this.isDataBound;
            }
            set
            {
                this.isDataBound = value;
            }
        }

        protected void MarkAsDataBound()
        {
            this.IsDataBound = true; 
        }

        public void SetDataFromJson(string json)
        {
            this.RequiresDataBinding = false;
            this.JsonData = json;
            this.Data = null;
            if(Ext.IsAjaxRequest)
            {
                this.GenerateAjaxResponseScript();
            }
        }

        public virtual bool IsBindableType(Type type)
        {
            if (type.IsGenericType
            && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(DateTime)
                || type == typeof(Guid)
                || type == typeof(TimeSpan)
                || type == typeof(Decimal);
        }
        
        AutoGeneratedFieldProperties[] CreateAutoFieldProperties(IEnumerable source, IEnumerator en)
        {
            Data = null;
            JsonData = string.Empty;

            if(this.SerializationMode == SerializationMode.Complex)
            {
                Data = source;
                return null;
            }
            
            if (source == null) return null;

            if(source is string && source.ToString().StartsWith("http"))
            {
                this.JsonData = string.Format("'{0}'", source);
                this.IsUrl = true;
                return null;
            }

            ITypedList typed = source as ITypedList;
            PropertyDescriptorCollection props = typed == null ? null : typed.GetItemProperties(new PropertyDescriptor[0]);

            Type prop_type;

            ArrayList retVal = new ArrayList();


            if (props == null)
            {
                object fitem = null;
                prop_type = null;
                PropertyInfo prop_item = source.GetType().GetProperty("Item",
                                                  BindingFlags.Instance | BindingFlags.Static |
                                                  BindingFlags.Public, null, null,
                                                  new Type[] { typeof(int) }, null);

                if (prop_item != null)
                {
                    prop_type = prop_item.PropertyType;
                    if(prop_type.IsInterface)
                    {
                        prop_type = null;
                    }
                }

                if (prop_type == null || prop_type == typeof(object))
                {
                    if (en.MoveNext())
                    {
                        fitem = en.Current;
                        this.firstRecord = fitem;
                    }
                    if (fitem != null)
                    {
                        prop_type = fitem.GetType();
                    }
                }

                if (fitem != null && fitem is ICustomTypeDescriptor)
                {
                    props = TypeDescriptor.GetProperties(fitem);
                }
                else if (prop_type != null)
                {
                    if (IsBindableType(prop_type))
                    {
                        AutoGeneratedFieldProperties field = new AutoGeneratedFieldProperties();
                        ((IStateManager)field).TrackViewState();
                        field.Name = "Item";
                        field.DataField = BoundField.ThisExpression;
                        field.Type = prop_type;
                        retVal.Add(field);
                    }
                    else
                    {
                        if (prop_type.IsArray)
                        {
                            Data = source;
                            return null;
                        }
                        else
                        {
                            props = TypeDescriptor.GetProperties(prop_type); 
                        }
                    }
                }
            }

            if (props != null && props.Count > 0)
            {
                foreach (PropertyDescriptor current in props)
                {
                    if (this.IsBindableType(current.PropertyType) || this.IsComplexField(current.Name))
                    {
                        AutoGeneratedFieldProperties field = new AutoGeneratedFieldProperties();
                        field.Name = current.Name;
                        field.DataField = current.Name;
                        retVal.Add(field);
                    }
                }
            }

            if (retVal.Count > 0)
            {
                return (AutoGeneratedFieldProperties[])retVal.ToArray(typeof(AutoGeneratedFieldProperties));
            }
            
            return null;
        }

        public SerializationMode SerializationMode
        {
            get
            {
                object obj = this.ViewState["SerializationMode"];
                return (obj == null) ? SerializationMode.Simple : (SerializationMode)obj;
            }
            set
            {
                this.ViewState["SerializationMode"] = value;
            }
        }
    }

    public enum SerializationMode
    {
        Simple,
        Complex
    }

    internal class CollectionDataSource : IDataSource
    {
        static readonly string[] names = new string[0];
        readonly IEnumerable collection;

        public CollectionDataSource(IEnumerable collection)
        {
            this.collection = collection;
        }

        public event EventHandler DataSourceChanged
        {
            add { }
            remove { }
        }

        public DataSourceView GetView(string viewName)
        {
            return new CollectionDataSourceView(this, viewName, collection);
        }

        public ICollection GetViewNames()
        {
            return names;
        }
    }

    internal class CollectionDataSourceView : DataSourceView
    {
        readonly IEnumerable collection;

        public CollectionDataSourceView(IDataSource owner, string viewName, IEnumerable collection)
            : base(owner, viewName)
        {
            this.collection = collection;
        }

        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            return collection;
        }
    }

    internal class FieldInReader
    {
        private bool inReader;
        private List<RecordField> fields;

        public FieldInReader(bool inReader, List<RecordField> fields)
        {
            this.inReader = inReader;
            this.fields = fields;
        }

        public bool InReader
        {
            get { return inReader; }
            set { inReader = value; }
        }

        public List<RecordField> Fields
        {
            get { return fields; }
            set { fields = value; }
        }
    }
}