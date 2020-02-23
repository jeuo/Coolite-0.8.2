/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Xml;
using Coolite.Utilities;
using Newtonsoft.Json.Linq;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A specialized grid implementation intended to mimic the traditional property grid as typically seen in development IDEs. Each row in the grid represents a property of some object, and the data is stored as a set of name/value pairs
    /// </summary>
    [Xtype("coolitepropertygrid")]
    [InstanceOf(ClassName = "Coolite.Ext.PropertyGrid")]
    [ToolboxData("<{0}:PropertyGrid runat=\"server\"></{0}:PropertyGrid>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.GridPanel), "Build.Resources.ToolboxIcons.GridPanel.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [ClientScript(FilePath = "/coolite/coolite-data.js", PathDebug = "/coolite/coolite-data-debug.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data.js", WebResourceDebug = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data-debug.js")]
    [Description("A specialized grid implementation intended to mimic the traditional property grid as typically seen in development IDEs. Each row in the grid represents a property of some object, and the data is stored as a set of name/value pairs")]
    public class PropertyGrid : GridPanelBase, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);
            if (this.Source.Count > 0)
            {
                string fn = "this.getDataField().setValue(Ext.encode(source));";
                this.On("propertychange", new JFunction(fn, "source"));
            }
        }
        
        private PropertyGridParameterCollection source;

        /// <summary>
        /// A data object to use as the data source of the grid.
        /// </summary>
        [ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("A data object to use as the data source of the grid.")]
        [AjaxEventUpdate(MethodName = "SetSource")] // <--- this is not working because setter is absent, we can't detect changes.
        public virtual PropertyGridParameterCollection Source
        {
            get
            {
                if (this.source == null)
                {
                    this.source = new PropertyGridParameterCollection();
                }

                return this.source;
            }
        }

        public void SetSource(PropertyGridParameterCollection source)
        {
            this.AddScript(string.Concat(this.ClientID, ".setSource(", source.ToJsonObject(), ");"));
        }

        [ClientConfig(JsonMode.Raw)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(null)]
        public string CustomEditors
        {
            get
            {
                int count = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                foreach (PropertyGridParameter parameter in this.Source)
                {
                    if(parameter.Editor.Count > 0)
                    {
                        if (parameter.Editor.Editor is ComboBox)
                        {
                            ComboBox cb = (ComboBox)parameter.Editor.Editor;
                            if(string.IsNullOrEmpty(cb.StoreID))
                            {
                                cb.TriggerAction = TriggerAction.All;
                                cb.Mode = DataLoadMode.Local;
                            }
                        }
                        sb.Append(string.Concat(JSON.Serialize(parameter.Name), ":new Ext.grid.GridEditor(", parameter.Editor.Editor.GetClientConstructor(true), "),"));
                        count++;
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("}");

                return count > 0 ? sb.ToString() : null;
            }
        }

        /// <summary>
        /// If false then all cells will be read only
        /// </summary>
        /// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
        [ClientConfig]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [Description("If false then all cells will be read only")]
        [DefaultValue(true)]
        public virtual bool Editable
        {
            get
            {
                object o = this.ViewState["Editable"];
                return o != null ? (bool) o : true;
            }
            set
            {
                this.ViewState["Editable"] = value;
            }
        }

        private BaseAjaxEvent ajaxEventConfig;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [ClientConfig(JsonMode.Object)]
        public BaseAjaxEvent AjaxEventConfig
        {
            get
            {
                if (this.ajaxEventConfig == null)
                {
                    this.ajaxEventConfig = new BaseAjaxEvent();
                }

                return this.ajaxEventConfig;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            CreateEditors();
            base.OnPreRender(e);
        }

        //private System.Web.UI.HtmlControls.HtmlInputHidden input;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.CreateEditors();

            //this.input = new System.Web.UI.HtmlControls.HtmlInputHidden();
            //this.Controls.Add(this.input);
            //this.input.EnableViewState = false;
            //this.input.ID = string.Concat(this.ID, "_Data");
        }

        private void CreateEditors()
        {
            foreach (PropertyGridParameter parameter in this.Source)
            {
                if (parameter.Editor.Count == 0)
                {
                    continue;
                }

                Field editor = parameter.Editor.Editor;
                editor.Visible = false;
                if (!this.Controls.Contains(editor))
                {
                    this.Controls.Add(editor);
                }

                if (!this.LazyItems.Contains(editor))
                {
                    this.LazyItems.Add(editor);
                }
            }
        }

        private static readonly object EventDataChanged = new object();

        /// <summary>
        /// Fires when the the PropertyGrid has changed records
        /// </summary>
        [Category("Action")]
        [Description("Fires when the the PropertyGrid has changed records")]
        public event EventHandler DataChanged
        {
            add
            {
                this.Events.AddHandler(EventDataChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventDataChanged, value);
            }
        }

        protected virtual void OnDataChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventDataChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private bool baseLoadPostData;
        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            baseLoadPostData = base.LoadPostData(postDataKey, postCollection);
            string val = postCollection[string.Concat(this.ClientID, "_Data")];
            if (!string.IsNullOrEmpty(val))
            {
                this.BuildSource(val);
                return true;
            }

            return false || baseLoadPostData;
        }

        protected override void RaisePostDataChangedEvent()
        {
            if (this.baseLoadPostData)
            {
                base.RaisePostDataChangedEvent();
                this.baseLoadPostData = false;
            }
            
            if (raiseChanged)
            {
                this.OnDataChanged(EventArgs.Empty);
                raiseChanged = false;
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if(Ext.IsAjaxRequest)
            {
                this.RaiseCallBackEvent(eventArgument);
            }
        }

        private string ChopQuotes(string value)
        {
            if(!string.IsNullOrEmpty(value) && value.StartsWith("\"") && value.EndsWith("\""))
            {
                return StringUtils.Chop(value);
            }

            return value;
        }

        private PropertyGridParameter FindParam(string name)
        {
            foreach (PropertyGridParameter parameter in this.Source)
            {
                if(parameter.Name == name)
                {
                    return parameter;
                }
            }

            return new PropertyGridParameter();
        }

        private bool dataChangedEventHandled = false;
        bool raiseChanged = false;
        
        void BuildSource(string strSource)
        {
            if(this.dataChangedEventHandled)
            {
                return;
            }
            
            PropertyGridParameterCollection result = new PropertyGridParameterCollection();
            JObject jo = JObject.Parse(strSource);
            foreach (JProperty property in jo.Properties())
            {
                PropertyGridParameter newP = new PropertyGridParameter(this.ChopQuotes(property.Name), this.ChopQuotes(property.Value.ToString()));
                PropertyGridParameter oldP = this.FindParam(property.Name);
                newP.Mode = oldP.Mode;
                
                if(oldP.Editor.Count > 0)
                {
                    newP.Editor.Add(oldP.Editor.Editor);    
                }

                newP.IsChanged = string.IsNullOrEmpty(newP.Name) || oldP.Value != newP.Value;
                if(newP.IsChanged)
                {
                    raiseChanged = true;
                }
                result.Add(newP);
            }

            this.Source.Clear();
            foreach (PropertyGridParameter parameter in result)
            {
                this.Source.Add(parameter);
            }

            this.dataChangedEventHandled = true;
        }

        private void RaiseCallBackEvent(string eventArgument)
        {
            Response response = new Response(true, null);
            try
            {
                if (string.IsNullOrEmpty(eventArgument))
                {
                    throw new ArgumentNullException("eventArgument");
                }

                XmlNode xmlData = this.SubmitConfig;
                XmlNode parametersNode = xmlData.SelectSingleNode("config/extraParams");

                string data = null;
                XmlNode serviceNode = xmlData.SelectSingleNode("config/serviceParams");
                if (serviceNode != null)
                {
                    data = serviceNode.InnerText;
                }

                string action = eventArgument;

                switch (action)
                {
                    case "update":
                        if (data == null)
                        {
                            throw new InvalidOperationException("No data in request");
                        }

                        this.BuildSource(data);
                        if (raiseChanged)
                        {
                            this.OnDataChanged(EventArgs.Empty);
                            raiseChanged = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Msg = this.IsDebugging ? ex.ToString() : ex.Message;
            }

            ScriptManager.ServiceResponse = response;
        }

        private PropertyGridListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        public PropertyGridListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PropertyGridListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private PropertyGridAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public PropertyGridAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PropertyGridAjaxEvents();
                    this.ajaxEvents.InitOwners(this);
                }
                return this.ajaxEvents;
            }
        }
    }
}
