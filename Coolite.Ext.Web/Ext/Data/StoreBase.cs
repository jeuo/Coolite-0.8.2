/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class StoreBase : Observable
    {
        protected override bool RemoveContainer
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// If passed, this store's load method is automatically called after creation with the autoLoad object.
        /// </summary>
        /// <value><c>true</c> if [auto load]; otherwise, <c>false</c>.</value>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If passed, this store's load method is automatically called after creation with the autoLoad object.")]
        public virtual bool AutoLoad
        {
            get
            {
                object obj = this.ViewState["AutoLoad"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoLoad"] = value;
            }
        }

        internal bool IsAutoLoadUndefined
        {
            get
            {
                return this.ViewState["AutoLoad"] == null;
            }
        }

        [ClientConfig("autoLoad")]
        [DefaultValue(false)]
        internal bool AutoLoadProxy
        {
            get
            {
                if(this.AutoLoadParams.Count == 0)
                {
                    return this.AutoLoad;
                }

                return false;
            }
        }

        /// <summary>
        /// If true then submitted data will be decoded
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If true then submitted data will be decoded")]
        public virtual bool AutoDecode
        {
            get
            {
                object obj = this.ViewState["AutoDecode"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoDecode"] = value;
            }
        }

        private ParameterCollection baseParams;

        /// <summary>
        /// An object containing properties which are to be sent as parameters on any HTTP request.
        /// </summary>
        //[ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object containing properties which are to be sent as parameters on any HTTP request.")]
        [ViewStateMember]
        public virtual ParameterCollection BaseParams
        {
            get
            {
                if (this.baseParams == null)
                {
                    this.baseParams = new ParameterCollection();
                    this.baseParams.Owner = this;
                }

                return this.baseParams;
            }
        }

        private ParameterCollection autoParams;

        /// <summary>
        /// An object containing properties which are to be sent as parameters on auto load HTTP request.")]
        /// </summary>
        //[ClientConfig("autoLoad", typeof(AutoLoadParamsJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object containing properties which are to be sent as parameters on auto load HTTP request.")]
        [ViewStateMember]
        public virtual ParameterCollection AutoLoadParams
        {
            get
            {
                if (this.autoParams == null)
                {
                    this.autoParams = new ParameterCollection();
                    this.autoParams.Owner = this;
                }

                return this.autoParams;
            }
        }

        [ClientConfig("autoLoad", typeof(AutoLoadParamsJsonConverter))]
        internal ParameterCollection AutoLoadParamsProxy
        {
            get
            {
                if(this.AutoLoad == false)
                {
                    return new ParameterCollection();
                }
                return this.AutoLoadParams;
            }
        }


        private ParameterCollection writeBaseParams;

        /// <summary>
        /// An object containing properties which are to be sent as parameters on any HTTP request.
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object containing properties which are to be sent as parameters on any HTTP request.")]
        [ViewStateMember]
        public virtual ParameterCollection WriteBaseParams
        {
            get
            {
                if (this.writeBaseParams == null)
                {
                    this.writeBaseParams = new ParameterCollection();
                    this.writeBaseParams.Owner = this;
                }

                return this.writeBaseParams;
            }
        }

        // add:data

        private ProxyCollection proxy;

        /// <summary>
        /// The Proxy object which provides access to a data object.
        /// </summary>
        //[Editor(typeof(ProxyCollectionEditor), typeof(UITypeEditor))]
        [ClientConfig("proxy>Proxy")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The Proxy object which provides access to a data object.")]
        [ViewStateMember]
        public virtual ProxyCollection Proxy
        {
            get
            {
                if (this.proxy == null)
                {
                    this.proxy = new ProxyCollection();
                    this.proxy.Store = this as Store;
                }
                return this.proxy;
            }
        }

        private UpdateProxyCollection updateProxy;

        [ClientConfig("updateProxy>Proxy")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public virtual UpdateProxyCollection UpdateProxy
        {
            get
            {
                if (this.updateProxy == null)
                {
                    this.updateProxy = new UpdateProxyCollection();
                    this.updateProxy.Store = this;
                }
                return this.updateProxy;
            }
        }

        private ReaderCollection reader;

        /// <summary>
        /// The DataReader object which processes the data object and returns an Array of Ext.data.Record objects which are cached keyed by their id property.
        /// </summary>
        [ClientConfig("reader>Reader")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The DataReader object which processes the data object and returns an Array of Ext.data.Record objects which are cached keyed by their id property.")]
        [ViewStateMember]
        public virtual ReaderCollection Reader
        {
            get
            {
                if (this.reader == null)
                {
                    this.reader = new ReaderCollection();
                }
                return this.reader;
            }
        }

        /// <summary>
        /// True to clear all modified record information each time the store is loaded or when a record is removed. (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to clear all modified record information each time the store is loaded or when a record is removed. (defaults to false).")]
        public virtual bool PruneModifiedRecords
        {
            get
            {
                object obj = this.ViewState["PruneModifiedRecords"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["PruneModifiedRecords"] = value;
            }
        }

        /// <summary>
        /// True if sorting is to be handled by requesting the Proxy to provide a refreshed version of the data object in sorted order, as opposed to sorting the Record cache in place (defaults to false). If remote sorting is specified, then clicking on a column header causes the current page to be requested from the server with the addition of the following two parameters: sort: String - The name (as specified in the Record's Field definition) of the field to sort on. dir : String - The direction of the sort, 'ASC' or 'DESC' (case-sensitive).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if sorting is to be handled by requesting the Proxy to provide a refreshed version of the data object in sorted order, as opposed to sorting the Record cache in place (defaults to false). If remote sorting is specified, then clicking on a column header causes the current page to be requested from the server with the addition of the following two parameters: sort: String - The name (as specified in the Record's Field definition) of the field to sort on. dir : String - The direction of the sort, 'ASC' or 'DESC' (case-sensitive).")]
        public virtual bool RemoteSort
        {
            get
            {
                object obj = this.ViewState["RemoteSort"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["RemoteSort"] = value;
            }
        }

        /// <summary>
        /// True to perform remote paging
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to perform remote paging.")]
        public virtual bool RemotePaging
        {
            get
            {
                object obj = this.ViewState["RemotePaging"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["RemotePaging"] = value;
            }
        }


        private SortInfo sortInfo;
        /// <summary>
        /// An object which determines the Store sorting information.
        /// </summary>
        [ClientConfig(JsonMode.Object)]
        [Category("Data")]
        [DefaultValue(null)]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("An object which determines the Store sorting information.")]
        [ViewStateMember]
        public virtual SortInfo SortInfo
        {
            get
            {
                if(this.sortInfo == null)
                {
                    this.sortInfo = new SortInfo();
                }
                return this.sortInfo;
            }
        }

        /// <summary>
        /// If true show a warning before load/reload if store has dirty data
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("If true show a warning before load/reload if store has dirty data")]
        public virtual bool WarningOnDirty
        {
            get
            {
                object obj = this.ViewState["WarningOnDirty"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["WarningOnDirty"] = value;
            }
        }

        /// <summary>
        /// The title of window showing before load if the dirty data exists
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("Uncommitted Changes")]
        [Description("The title of window showing before load if the dirty data exists")]
        public virtual string DirtyWarningTitle
        {
            get
            {
                return (string)this.ViewState["DirtyWarningTitle"] ?? "Uncommitted Changes";
            }
            set
            {
                this.ViewState["DirtyWarningTitle"] = value;
            }
        }

        /// <summary>
        /// The text of window showing before load if the dirty data exists
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("You have uncommitted changes.  Are you sure you want to load/reload data?")]
        [Description("The text of window showing before load if the dirty data exists")]
        public virtual string DirtyWarningText
        {
            get
            {
                object obj = this.ViewState["DirtyWarningText"];
                return (obj == null) ? "You have uncommitted changes.  Are you sure you want to load/reload data?" : (string)obj;
            }
            set
            {
                this.ViewState["DirtyWarningText"] = value;
            }
        }

        /// <summary>
        /// The refresh mode
        /// </summary>
        [ClientConfig("refreshAfterSave", JsonMode.Value)]
        [Category("Config Options")]
        [DefaultValue(RefreshAfterSavingMode.Auto)]
        [Description("The refresh mode")]
        public virtual RefreshAfterSavingMode RefreshAfterSaving
        {
            get
            {
                object obj = this.ViewState["RefreshAfterSaving"];
                return (obj == null) ? RefreshAfterSavingMode.Auto : (RefreshAfterSavingMode)obj;
            }
            set
            {
                this.ViewState["RefreshAfterSaving"] = value;
            }
        }

        [ClientConfig("useIdConfirmation", JsonMode.Value)]
        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool UseIdConfirmation
        {
            get
            {
                object obj = this.ViewState["UseIdConfirmation"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["UseIdConfirmation"] = value;
            }
        }

        private BaseAjaxEvent ajaxEventConfig;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [ClientConfig(JsonMode.ObjectAllowEmpty)]
        public BaseAjaxEvent AjaxEventConfig
        {
            get
            {
                if(this.ajaxEventConfig == null)
                {
                    this.ajaxEventConfig = new BaseAjaxEvent();
                }

                return this.ajaxEventConfig;
            }
        }

        /// <summary>
        /// The field name by which to sort the store's data (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The field name by which to sort the store's data (defaults to '').")]
        public virtual string GroupField
        {
            get
            {
                return (string)this.ViewState["GroupField"] ?? "";
            }
            set
            {
                this.ViewState["GroupField"] = value;
            }
        }

        /// <summary>
        /// True to sort the data on the grouping field when a grouping operation occurs, false to sort based on the existing sort info (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to sort the data on the grouping field when a grouping operation occurs, false to sort based on the existing sort info (defaults to false).")]
        public virtual bool GroupOnSort
        {
            get
            {
                object obj = this.ViewState["GroupOnSort"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["GroupOnSort"] = value;
            }
        }

        /// <summary>
        /// True if the grouping should apply on the server side, false if it is local only (defaults to false). If the grouping is local, it can be applied immediately to the data. If it is remote, then it will simply act as a helper, automatically sending the grouping field name as the 'groupBy' param with each XHR call.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if the grouping should apply on the server side, false if it is local only (defaults to false). If the grouping is local, it can be applied immediately to the data. If it is remote, then it will simply act as a helper, automatically sending the grouping field name as the 'groupBy' param with each XHR call.")]
        public virtual bool RemoteGroup
        {
            get
            {
                object obj = this.ViewState["RemoteGroup"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["RemoteGroup"] = value;
            }
        }

        /// <summary>
        /// If true then only properties included to reader will be converted to json
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("If true then only properties included to reader will be converted to json")]
        public virtual bool IgnoreExtraFields
        {
            get
            {
                object obj = this.ViewState["IgnoreExtraFields"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["IgnoreExtraFields"] = value;
            }
        }

        /// <summary>
        /// Show warning if request fail.
        /// </summary>
        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Show a Window with error message is AjaxEvent request fails.")]
        public bool ShowWarningOnFailure
        {
            get
            {
                object obj = this.ViewState["ShowWarningOnFailure"];
                return obj != null ? (bool)obj : true;
            }
            set
            {
                this.ViewState["ShowWarningOnFailure"] = value;
            }
        }

        /// <summary>
        /// Skip Id field from save data for new records.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Skip Id field from save data for new records.")]
        public virtual bool SkipIdForNewRecords
        {
            get
            {
                object obj = this.ViewState["SkipIdForNewRecords"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["SkipIdForNewRecords"] = value;
            }
        }


        private object data;
        protected object Data
        {
            get { return data; }
            set { data = value; }
        }

        private string jsonData;
        protected string JsonData
        {
            get { return jsonData; }
            set { jsonData = value; }
        }

        private bool isUrl;
        protected bool IsUrl
        {
            get { return isUrl; }
            set { isUrl = value; }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Loads data from a passed data block. A Reader which understands the format of the data must have been configured in the constructor.
        /// </summary>
        [Description("Loads data from a passed data block. A Reader which understands the format of the data must have been configured in the constructor.")]
        public virtual void LoadData(string data)
        {
            this.AddScript("{0}.loadData({1});", this.ClientID, data);
        }

        /// <summary>
        /// Loads data from a passed data block. A Reader which understands the format of the data must have been configured in the constructor.
        /// </summary>
        [Description("Loads data from a passed data block. A Reader which understands the format of the data must have been configured in the constructor.")]
        public virtual void LoadData(string data, bool append)
        {
            this.AddScript("{0}.loadData({1},{2});", this.ClientID, data, append.ToString().ToLower());
        }

        /// <summary>
        /// Add Record to the Store and fires the add event.
        /// </summary>
        [Description("Add Record to the Store and fires the add event.")]
        public virtual void AddRecord(IDictionary<string, object> values)
        {
            this.AddScript("{0}.addRecord({1});", this.ClientID, JSON.Serialize(values,null,true));
        }

        /// <summary>
        /// Inserts Record into the Store at the given index and fires the add event.
        /// </summary>
        [Description("Inserts Record into the Store at the given index and fires the add event.")]
        public virtual void InsertRecord(int index, IDictionary<string, object> values)
        {
            this.AddScript("{0}.insertRecord({1}, {2});", this.ClientID, index, JSON.Serialize(values, null, true));
        }
        
        /// <summary>
        /// (Local sort only) Inserts the passed Record into the Store at the index where it should go based on the current sort information.
        /// </summary>
        [Description("(Local sort only) Inserts the passed Record into the Store at the index where it should go based on the current sort information.")]
        public virtual void AddSortedRecord(IDictionary<string, object> values)
        {
            this.AddScript("{0}.addSortedRecord({1});", this.ClientID, JSON.Serialize(values, null, true));
        }

        /// <summary>
        /// Revert to a view of the Record cache with no filtering applied.
        /// </summary>
        [Description("Revert to a view of the Record cache with no filtering applied.")]
        public virtual void ClearFilter()
        {
            this.ClearFilter(false);
        }

        /// <summary>
        /// Revert to a view of the Record cache with no filtering applied.
        /// </summary>
        /// <param name="suppressEvent">If true the filter is cleared silently without notifying listeners</param>
        [Description("Revert to a view of the Record cache with no filtering applied.")]
        public virtual void ClearFilter(bool suppressEvent)
        {
            this.AddScript("{0}.clearFilter({1});", this.ClientID, JSON.Serialize(suppressEvent));
        }

        /// <summary>
        /// Commit all Records with outstanding changes. To handle updates for changes, subscribe to the Store's "update" event, and perform updating when the third parameter is Ext.data.Record.COMMIT.
        /// </summary>
        [Description("Commit all Records with outstanding changes. To handle updates for changes, subscribe to the Store's \"update\" event, and perform updating when the third parameter is Ext.data.Record.COMMIT.")]
        public virtual void CommitChanges()
        {
            this.AddScript("{0}.commitChanges();", this.ClientID);
        }

        /// <summary>
        /// Filter the records by a specified property.
        /// </summary>
        /// <param name="field">A field on your records</param>
        /// <param name="value">Either a string that the field should begin with, or a RegExp (should be raw token) to test against the field.</param>
        /// <param name="anyMatch">True to match any part not just the beginning</param>
        /// <param name="caseSensitive">True for case sensitive comparison</param>
        [Description("Filter the records by a specified property.")]
        public virtual void Filter(string field, string value, bool anyMatch, bool caseSensitive)
        {
            if(TokenUtils.IsRawToken(value))
            {
                value = TokenUtils.ReplaceRawToken(value);
            }
            else
            {
                value = JSON.Serialize(value);
            }
            this.AddScript("{0}.filter({1},{2},{3},{4});", this.ClientID, JSON.Serialize(field), value, JSON.Serialize(anyMatch), JSON.Serialize(caseSensitive));
        }

        /// <summary>
        /// Filter by a function. The specified function will be called for each Record in this Store. If the function returns true the Record is included, otherwise it is filtered out.
        /// </summary>
        /// <param name="fn">The function to be called. It will be passed the following parameters: record - The record to test for filtering. Access field values using Ext.data.Record.get. id - The ID of the Record passed.</param>
        [Description("Filter by a function. The specified function will be called for each Record in this Store. If the function returns true the Record is included, otherwise it is filtered out.")]
        public virtual void FilterBy(JFunction fn)
        {
            this.AddScript("{0}.filterBy({1});", this.ClientID, fn.ToString());
        }

        /// <summary>
        /// Filter by a function. The specified function will be called for each Record in this Store. If the function returns true the Record is included, otherwise it is filtered out.
        /// </summary>
        /// <param name="fn">The function to be called. It will be passed the following parameters: record - The record to test for filtering. Access field values using Ext.data.Record.get. id - The ID of the Record passed.</param>
        /// <param name="scope">The scope of the function (defaults to this)</param>
        [Description("Filter by a function. The specified function will be called for each Record in this Store. If the function returns true the Record is included, otherwise it is filtered out.")]
        public virtual void FilterBy(JFunction fn, string scope)
        {
            this.AddScript("{0}.filterBy({1},{2});", this.ClientID, fn.ToString(), scope);
        }

        /// <summary>
        /// Cancel outstanding changes on all changed records.
        /// </summary>
        [Description("Cancel outstanding changes on all changed records.")]
        public virtual void RejectChanges()
        {
            this.AddScript("{0}.rejectChanges();", this.ClientID);
        }

        /// <summary>
        /// Remove all Records from the Store and fires the clear event.
        /// </summary>
        [Description("Remove all Records from the Store and fires the clear event.")]
        public virtual void RemoveAll()
        {
            this.AddScript("{0}.removeAll();", this.ClientID);
        }

        /// <summary>
        /// Sets the default sort column and order to be used by the next load operation.
        /// </summary>
        /// <param name="field">The name of the field to sort by.</param>
        /// <param name="dir">The sort order, "ASC" or "DESC"</param>
        [Description("Sets the default sort column and order to be used by the next load operation.")]
        public virtual void SetDefaultSort(string field, SortDirection dir)
        {
            this.AddScript("{0}.setDefaultSort({1}, {2});", this.ClientID, JSON.Serialize(field), JSON.Serialize(dir.ToString()));
        }

        /// <summary>
        /// Sort the Records. If remote sorting is used, the sort is performed on the server, and the cache is reloaded. If local sorting is used, the cache is sorted internally.
        /// </summary>
        /// <param name="field">The name of the field to sort by.</param>
        /// <param name="dir">The sort order, "ASC" or "DESC"</param>
        [Description("Sort the Records. If remote sorting is used, the sort is performed on the server, and the cache is reloaded. If local sorting is used, the cache is sorted internally.")]
        public virtual void Sort(string field, SortDirection dir)
        {
            this.AddScript("{0}.sort({1}, {2});", this.ClientID, JSON.Serialize(field), JSON.Serialize(dir.ToString()));
        }

        public virtual void AddField(RecordField field)
        {
            this.AddField(field, -1);
        }

        public virtual void AddField(RecordField field, int index)
        {
            if (this.Reader.Reader != null)
            {
                if (index >= 0 && index < this.Reader.Reader.Fields.Count)
                {
                    this.Reader.Reader.Fields.Insert(index, field);
                }
                else
                {
                    this.Reader.Reader.Fields.Add(field);
                }
            }
            this.AddScript("{0}.addField({1}{2});", this.ClientID, new ClientConfig(true).Serialize(field), index >=0 ? ", "+index : "");
            
            if (this.Reader.Reader != null && this.Reader.Reader is JsonReader)
            {
                this.ClearMeta();
            }
        }

        public virtual void RemoveField(RecordField field)
        {
            this.AddScript("{0}.removeField({1});", this.ClientID, new ClientConfig(true).Serialize(field));
        }

        public virtual void RemoveFields()
        {
            this.AddScript("{0}.removeFields();", this.ClientID);
        }

        public virtual void ClearMeta()
        {
            this.AddScript("if({0}.reader.ef){{delete {0}.reader.ef;}}", this.ClientID);
        }
    }
}