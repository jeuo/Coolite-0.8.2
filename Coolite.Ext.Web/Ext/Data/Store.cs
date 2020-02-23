/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// The Store class encapsulates a client side cache of Record objects which provide
    /// input data for Components such as the GridPanel, the ComboBox, or the DataView
    /// 
    /// A Store object uses its configured implementation of DataProxy to access a data
    /// object unless you call loadData directly and pass in your data.
    ///
    /// A Store object has no knowledge of the format of the data returned by the Proxy.
    ///
    /// A Store object uses its configured implementation of DataReader to create Record
    /// instances from the data object. These Records are cached and made available through
    /// accessor functions.
    /// </summary>
    [InstanceOf(ClassName = "Coolite.Ext.Store")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Store), "Build.Resources.ToolboxIcons.Store.bmp")]
    [ClientScript(FilePath = "/coolite/coolite-data.js", PathDebug = "/coolite/coolite-data-debug.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data.js", WebResourceDebug = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data-debug.js")]
    [Description("The Store class encapsulates a client side cache of Record objects which provide input data for Components such as the GridPanel, the ComboBox, or the DataView.")]
    public class Store : StoreDataBound, IPostBackEventHandler, ICustomConfigSerialization
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(!this.DesignMode)
            {
                this.Page.LoadComplete += Page_LoadComplete;
            }
        }

        protected internal override bool IsIDRequired
        {
            get
            {
                return true;
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (this.ParentComponent == null || (Ext.IsMicrosoftAjaxRequest && !this.IsInUpdatePanelRefresh))
            {
                return;
            }

            Component parent = this.ParentComponent;
            if (parent != null)
            {
                parent = this.ParentComponent;
                while (parent != null && (parent.IsLazy || parent.IsLayout))
                {
                    parent = parent.ParentComponent;
                }
            }


            if (parent != null)
            {
                parent.BeforeClientInit += Parent_BeforeClientInit;
            }
        }

        void Parent_BeforeClientInit(Observable sender)
        {
            this.ForcePreRender();
        }

        internal override void ForcePreRender()
        {
            if(!Ext.IsAjaxRequest)
            {
                this.EnsureDataBound();
                base.ForcePreRender();    
            }
        }

        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.MemoryDataPresent && this.Proxy.Count == 0)
            {
                this.AddBeforeClientInitScript(string.Format("this.{0}={1};", DataID, this.Data != null ? JSON.Serialize(this.Data) : JsonData));
            }
        }

        protected override void OnAfterClientInit(Observable sender)
        {
            if (this.MemoryDataPresent && this.Proxy.Count == 0)
            {
                string template = "{0}.proxy=new Ext.data.PagingMemoryProxy({1}, {2});";
                this.AddAfterClientInitScript(string.Format(template, this.ClientID, DataID, JSON.Serialize(this.IsUrl)));
            }

            if(this.Proxy.Count == 0 && !MemoryDataPresent)
            {
                string template = "{0}.proxy=new Ext.data.PagingMemoryProxy({{}});";
                this.AddAfterClientInitScript(string.Format(template, this.ClientID));
            }

            if (this.BaseParams.Count > 0)
            {
                string template = "{0}.on(\"{1}\",{2});";
                this.AddAfterClientInitScript(string.Format(template, this.ClientID, "beforeload", BuildParams(this.BaseParams)));
            }

            if (this.WriteBaseParams.Count > 0)
            {
                string template = "{0}.on(\"{1}\",{2});";
                this.AddAfterClientInitScript(string.Format(template, this.ClientID, "beforesave", BuildParams(this.WriteBaseParams)));
            }
        }

        private bool MemoryDataPresent
        {
            get { return this.Reader != null && this.Reader.Reader != null && (this.Data != null || !string.IsNullOrEmpty(this.JsonData)); }
        }

        private string DataID
        {
            get { return string.Concat(this.ClientID, "_Data"); }
        }

        private string BuildParams(ParameterCollection parameters)
        {
            StringBuilder sb = new StringBuilder("function(store,options){if(!options.params){options.params = {};};");

            sb.AppendFormat("Ext.apply(options.params,{0});", parameters.ToJson(2));
            sb.AppendFormat("Ext.applyIf(options.params,{0});", parameters.ToJson(1));
            sb.Append("}");
            return sb.ToString();
        }

        private StoreListeners listeners;

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
        [ViewStateMember]
        public StoreListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new StoreListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private StoreAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public StoreAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new StoreAjaxEvents();
                    this.ajaxEvents.InitOwners(this);
                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                
                return this.ajaxEvents;
            }
        }

        /*  IPostBackEventHandler
        -----------------------------------------------------------------------------------------------*/

        private static readonly object EventBeforeStoreChanged = new object();
        private static readonly object EventAfterStoreChanged = new object();
        private static readonly object EventBeforeRecordUpdated = new object();
        private static readonly object EventAfterRecordUpdated = new object();
        private static readonly object EventBeforeRecordDeleted = new object();
        private static readonly object EventAfterRecordDeleted = new object();
        private static readonly object EventBeforeRecordPostBackInserted = new object();
        private static readonly object EventBeforeRecordInserted = new object();
        private static readonly object EventAfterRecordInserted = new object();
        private static readonly object EventBeforeAjaxEvent = new object();
        private static readonly object EventAfterAjaxEvent = new object();
        private static readonly object EventRefreshData = new object();
        private static readonly object EventSubmitData = new object();

        public delegate void BeforeStoreChangedEventHandler(object sender, BeforeStoreChangedEventArgs e);
        public delegate void AfterStoreChangedEventHandler(object sender, AfterStoreChangedEventArgs e);
        public delegate void BeforeRecordUpdatedEventHandler(object sender, BeforeRecordUpdatedEventArgs e);
        public delegate void AfterRecordUpdatedEventHandler(object sender, AfterRecordUpdatedEventArgs e);
        public delegate void BeforeRecordDeletedEventHandler(object sender, BeforeRecordDeletedEventArgs e);
        public delegate void AfterRecordDeletedEventHandler(object sender, AfterRecordDeletedEventArgs e);
        public delegate void BeforeRecordInsertedEventHandler(object sender, BeforeRecordInsertedEventArgs e);
        public delegate void AfterRecordInsertedEventHandler(object sender, AfterRecordInsertedEventArgs e);
        public delegate void BeforeAjaxEventHandler(object sender, BeforeAjaxEventArgs e);
        public delegate void AfterAjaxEventHandler(object sender, AfterAjaxEventArgs e);
        public delegate void AjaxRefreshDataEventHandler(object sender, StoreRefreshDataEventArgs e);
        public delegate void AjaxSubmitDataEventHandler(object sender, StoreSubmitDataEventArgs e);

        [Category("Action")]
        public event BeforeStoreChangedEventHandler BeforeStoreChanged
        {
            add
            {
                this.Events.AddHandler(EventBeforeStoreChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventBeforeStoreChanged, value);
            }
        }

        [Category("Action")]
        public event AfterStoreChangedEventHandler AfterStoreChanged
        {
            add
            {
                this.Events.AddHandler(EventAfterStoreChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventAfterStoreChanged, value);
            }
        }

        [Category("Action")]
        public event BeforeRecordUpdatedEventHandler BeforeRecordUpdated
        {
            add
            {
                this.Events.AddHandler(EventBeforeRecordUpdated, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventBeforeRecordUpdated, value);
            }
        }

        [Category("Action")]
        public event AfterRecordUpdatedEventHandler AfterRecordUpdated
        {
            add
            {
                this.Events.AddHandler(EventAfterRecordUpdated, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventAfterRecordUpdated, value);
            }
        }

        [Category("Action")]
        public event BeforeRecordDeletedEventHandler BeforeRecordDeleted
        {
            add
            {
                this.Events.AddHandler(EventBeforeRecordDeleted, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventBeforeRecordDeleted, value);
            }
        }

        [Category("Action")]
        public event AfterRecordDeletedEventHandler AfterRecordDeleted
        {
            add
            {
                this.Events.AddHandler(EventAfterRecordDeleted, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventAfterRecordDeleted, value);
            }
        }

        [Category("Action")]
        public event BeforeRecordInsertedEventHandler BeforeRecordInserted
        {
            add
            {
                this.Events.AddHandler(EventBeforeRecordInserted, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventBeforeRecordInserted, value);
            }
        }

        [Category("Action")]
        public event AfterRecordInsertedEventHandler AfterRecordInserted
        {
            add
            {
                this.Events.AddHandler(EventAfterRecordInserted, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventAfterRecordInserted, value);
            }
        }

        [Category("Action")]
        public event AfterAjaxEventHandler AfterAjaxEvent
        {
            add
            {
                this.Events.AddHandler(EventAfterAjaxEvent, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventAfterAjaxEvent, value);
            }
        }

        [Category("Action")]
        public event BeforeAjaxEventHandler BeforeAjaxEvent
        {
            add
            {
                this.Events.AddHandler(EventBeforeAjaxEvent, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventBeforeAjaxEvent, value);
            }
        }

        [Category("Action")]
        public event AjaxRefreshDataEventHandler RefreshData
        {
            add
            {
                this.Events.AddHandler(EventRefreshData, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventRefreshData, value);
            }
        }

        [Category("Action")]
        public event AjaxSubmitDataEventHandler SubmitData
        {
            add
            {
                this.Events.AddHandler(EventSubmitData, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventSubmitData, value);
            }
        }


        protected virtual void OnAfterStoreChanged(AfterStoreChangedEventArgs e)
        {
            AfterStoreChangedEventHandler handler = (AfterStoreChangedEventHandler)Events[EventAfterStoreChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeStoreChanged(BeforeStoreChangedEventArgs e)
        {
            BeforeStoreChangedEventHandler handler = (BeforeStoreChangedEventHandler)Events[EventBeforeStoreChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeRecordUpdated(BeforeRecordUpdatedEventArgs e)
        {
            BeforeRecordUpdatedEventHandler handler = (BeforeRecordUpdatedEventHandler)Events[EventBeforeRecordUpdated];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterRecordUpdated(AfterRecordUpdatedEventArgs e)
        {
            AfterRecordUpdatedEventHandler handler = (AfterRecordUpdatedEventHandler)Events[EventAfterRecordUpdated];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeRecordDeleted(BeforeRecordDeletedEventArgs e)
        {
            BeforeRecordDeletedEventHandler handler = (BeforeRecordDeletedEventHandler)Events[EventBeforeRecordDeleted];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterRecordDeleted(AfterRecordDeletedEventArgs e)
        {
            AfterRecordDeletedEventHandler handler = (AfterRecordDeletedEventHandler)Events[EventAfterRecordDeleted];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnBeforeRecordInserted(BeforeRecordInsertedEventArgs e)
        {
            BeforeRecordInsertedEventHandler handler = (BeforeRecordInsertedEventHandler)Events[EventBeforeRecordInserted];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterRecordInserted(AfterRecordInsertedEventArgs e)
        {
            AfterRecordInsertedEventHandler handler = (AfterRecordInsertedEventHandler)Events[EventAfterRecordInserted];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAjaxPostBack(BeforeAjaxEventArgs e)
        {
            BeforeAjaxEventHandler handler = (BeforeAjaxEventHandler)Events[EventBeforeAjaxEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRefreshData(StoreRefreshDataEventArgs e)
        {
            AjaxRefreshDataEventHandler handler = (AjaxRefreshDataEventHandler)Events[EventRefreshData];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSubmitData(StoreSubmitDataEventArgs e)
        {
            AjaxSubmitDataEventHandler handler = (AjaxSubmitDataEventHandler)Events[EventSubmitData];
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void OnAjaxPostBackResult(AfterAjaxEventArgs e)
        {
            AfterAjaxEventHandler handler = (AfterAjaxEventHandler)Events[EventAfterAjaxEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private IDictionary keys;
        private IDictionary values;
        private IDictionary oldValues;
        private bool needRetrieve;
        private ConfirmationRecord confirmation;
        private XmlNode record;
        
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            if(Ext.IsAjaxRequest)
            {
                if (string.IsNullOrEmpty(eventArgument))
                {
                    return;
                }
                this.RaiseAjaxPostBackEvent(eventArgument);
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Ext.IsAjaxRequest && this.ParentForm == null)
            {
                this.Page.LoadComplete += new EventHandler(Store_LoadComplete);
            }
        }

        private void Store_LoadComplete(object sender, EventArgs e)
        {
            string _ea = this.Page.Request["__EVENTARGUMENT"];
            if (!string.IsNullOrEmpty(_ea))
            {
                string _et = this.Page.Request["__EVENTTARGET"];

                if (_et == this.UniqueID)
                {
                    RaiseAjaxPostBackEvent(_ea);
                }

                return;
            }

            XmlNode eventArgumentNode = this.SubmitConfig.SelectSingleNode("config/__EVENTARGUMENT");
            if (eventArgumentNode == null)
            {
                throw new InvalidOperationException(
                    "Incorrect submit config - the '__EVENTARGUMENT' parameter is absent");
            }

            XmlNode eventTargetNode = this.SubmitConfig.SelectSingleNode("config/__EVENTTARGET");
            if (eventTargetNode == null)
            {
                throw new InvalidOperationException(
                    "Incorrect submit config - the '__EVENTTARGET' parameter is absent");
            }

            if (eventTargetNode.InnerText == this.UniqueID)
            {
                RaiseAjaxPostBackEvent(eventArgumentNode.InnerText);
            }
        }

        private BeforeStoreChangedEventArgs changingEventArgs;
        private void DoSaving(string jsonData, XmlNode callbackParameters)
        {
            changingEventArgs = new BeforeStoreChangedEventArgs(jsonData, null, callbackParameters);

            ConfirmationList confirmationList = null;
            if (this.UseIdConfirmation && this.Reader.Reader != null)
            {
                confirmationList = changingEventArgs.DataHandler.BuildConfirmationList(GetIdColumnName());
            }
            
            changingEventArgs.ConfirmationList = confirmationList;

            this.OnBeforeStoreChanged(changingEventArgs);

            Exception ex = null;
            try
            {
                //if (!changingEventArgs.Cancel && !string.IsNullOrEmpty(this.DataSourceID))
                if (!changingEventArgs.Cancel)
                {
                    this.MakeChanges();
                }
            }
            catch (Exception e)
            {
                ex = e;
            }

            AfterStoreChangedEventArgs eStoreChanged = new AfterStoreChangedEventArgs(true, ex, confirmationList);
            this.OnAfterStoreChanged(eStoreChanged);
            if(eStoreChanged.Exception != null && !eStoreChanged.ExceptionHandled)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void MakeChanges()
        {
            bool noDs = string.IsNullOrEmpty(this.DataSourceID);
            IDataSource ds = null;
            if(!noDs)
            {
                ds = this.GetDataSource();
            }

            if (ds == null && !noDs)
            {
                throw new HttpException("Can't find DataSource");
            }

            if(this.Reader.Reader == null)
            {
                throw new InvalidOperationException("The Store does not contain a Reader.");
            }

            XmlDocument xml = changingEventArgs.DataHandler.XmlData;
            
            if(noDs || ds.GetView("").CanUpdate)
            {
                this.MakeUpdates(ds, xml); 
            }
            if (noDs || ds.GetView("").CanDelete)
            {
                this.MakeDeletes(ds, xml);
            }
            if (noDs || ds.GetView("").CanInsert)
            {
                this.MakeInsertes(ds, xml);
            }
        }

        private string GetIdColumnName()
        {
            string id = "";
            JsonReader jsonReader = this.Reader.Reader as JsonReader;
            if (jsonReader != null)
            {
                id = jsonReader.ReaderID;
            }

            XmlReader xmlReader = this.Reader.Reader as XmlReader;
            if (xmlReader != null)
            {
                id = xmlReader.ReaderID;
            }
            return id;
        }

        private void MakeUpdates(IDataSource ds, XmlDocument xml)
        {
            XmlNodeList updatingRecords = xml.SelectNodes("records/Updated/record");
            
            string id = GetIdColumnName();

            foreach (XmlNode node in updatingRecords)
            {
                record = node;
                values = new SortedList(this.Reader.Reader.Fields.Count);
                keys = new SortedList();
                oldValues = new SortedList();
                foreach (RecordField field in this.Reader.Reader.Fields)
                {
                    XmlNode keyNode = node.SelectSingleNode(field.Name);
                    values[field.Name] = keyNode != null ? keyNode.InnerText : null;
                }

                confirmation = null;
                if (!string.IsNullOrEmpty(id))
                {
                    XmlNode keyNode = node.SelectSingleNode(id);
                    string idStr = keyNode != null ? keyNode.InnerText : null;
                    
                    int idInt;
                    if (int.TryParse(idStr, out idInt))
                    {
                        keys[id] = idInt;
                    }
                    else
                    {
                        keys[id] = idStr;
                    }
                    
                    if(this.UseIdConfirmation && keys[id] != null)
                    {
                        confirmation = changingEventArgs.ConfirmationList[keys[id].ToString()];
                    }
                }

                BeforeRecordUpdatedEventArgs eBeforeRecordUpdated = new BeforeRecordUpdatedEventArgs(record, keys, values, oldValues, confirmation);
                this.OnBeforeRecordUpdated(eBeforeRecordUpdated);
                if (eBeforeRecordUpdated.CancelAll)
                {
                    break;
                }
                if (eBeforeRecordUpdated.Cancel)
                {
                    continue;
                }
                if(ds !=null)
                {
                    ds.GetView("").Update(keys, values, oldValues, this.UpdateCallback);
                }
                else
                {
                    this.UpdateCallback(0, null);
                }
                
            }
        }

        private void MakeDeletes(IDataSource ds, XmlDocument xml)
        {
            XmlNodeList deletingRecords = xml.SelectNodes("records/Deleted/record");
            string id = GetIdColumnName();
            foreach (XmlNode node in deletingRecords)
            {
                record = node;
                values = new SortedList(0);
                keys = new SortedList();
                oldValues = new SortedList(0);

                confirmation = null;
                if(!string.IsNullOrEmpty(id))
                {
                    XmlNode keyNode = node.SelectSingleNode(id);
                    string idStr = keyNode != null ? keyNode.InnerText : null;

                    int idInt;
                    if (int.TryParse(idStr, out idInt))
                    {
                        keys[id] = idInt;
                    }
                    else
                    {
                        keys[id] = idStr;
                    }

                    if (this.UseIdConfirmation && keys[id] != null)
                    {
                        confirmation = changingEventArgs.ConfirmationList[keys[id].ToString()];
                    }
                }

                BeforeRecordDeletedEventArgs eBeforeRecordDeleted = new BeforeRecordDeletedEventArgs(record, keys, confirmation);
                this.OnBeforeRecordDeleted(eBeforeRecordDeleted);
                if (eBeforeRecordDeleted.CancelAll)
                {
                    break;
                }
                if (eBeforeRecordDeleted.Cancel)
                {
                    continue;
                }
                
                if (ds != null)
                {
                    ds.GetView("").Delete(keys, oldValues, DeleteCallback);
                }
                else
                {
                    this.DeleteCallback(0, null);
                }
            }

            if (deletingRecords.Count > 0)
            {
                needRetrieve = true;
            }
        }

        private void MakeInsertes(IDataSource ds, XmlDocument xml)
        {
            XmlNodeList insertingRecords = xml.SelectNodes("records/Created/record");
            string id = GetIdColumnName();
            foreach (XmlNode node in insertingRecords)
            {
                record = node;
                values = new SortedList(this.Reader.Reader.Fields.Count);
                keys = new SortedList();
                oldValues = new SortedList();
                foreach (RecordField field in this.Reader.Reader.Fields)
                {
                    XmlNode keyNode = node.SelectSingleNode(field.Name);
                    values[field.Name] = keyNode != null ? keyNode.InnerText : null;
                }

                confirmation = null;
                if (!string.IsNullOrEmpty(id))
                {
                    XmlNode keyNode = node.SelectSingleNode(id);
                    if (this.UseIdConfirmation && keyNode != null && !string.IsNullOrEmpty(keyNode.InnerText))
                    {
                        confirmation = changingEventArgs.ConfirmationList[keyNode.InnerText];
                    }
                }

                BeforeRecordInsertedEventArgs eBeforeRecordInserted = new BeforeRecordInsertedEventArgs(record, keys, values, confirmation);
                this.OnBeforeRecordInserted(eBeforeRecordInserted);
                if (eBeforeRecordInserted.CancelAll)
                {
                    break;
                }
                if (eBeforeRecordInserted.Cancel)
                {
                    continue;
                }
                if (ds != null)
                {
                    ds.GetView("").Insert(values, InsertCallback);
                }
                else
                {
                    this.InsertCallback(0, null);
                }
            }

            if (insertingRecords.Count > 0)
            {
                needRetrieve = true;
            }
        }

        bool UpdateCallback(int recordsAffected, Exception exception)
        {
            if(confirmation != null && recordsAffected > 0)
            {
                confirmation.ConfirmRecord();
            }
            AfterRecordUpdatedEventArgs eAfterRecordUpdated = new AfterRecordUpdatedEventArgs(record, recordsAffected, exception, keys, values, oldValues, confirmation);
            this.OnAfterRecordUpdated(eAfterRecordUpdated);
            return eAfterRecordUpdated.ExceptionHandled;
        }

        bool DeleteCallback(int recordsAffected, Exception exception)
        {
            if (confirmation != null && recordsAffected > 0)
            {
                confirmation.ConfirmRecord();
            }
            AfterRecordDeletedEventArgs eAfterRecordDeleted = new AfterRecordDeletedEventArgs(record, recordsAffected, exception, keys, confirmation);
            this.OnAfterRecordDeleted(eAfterRecordDeleted);
            return eAfterRecordDeleted.ExceptionHandled;
        }

        bool InsertCallback(int recordsAffected, Exception exception)
        {
            if (confirmation != null && recordsAffected > 0)
            {
                confirmation.ConfirmRecord();
            }

            AfterRecordInsertedEventArgs eAfterRecordInserted = new AfterRecordInsertedEventArgs(record, recordsAffected, exception, keys, values, confirmation);
            this.OnAfterRecordInserted(eAfterRecordInserted);
            return eAfterRecordInserted.ExceptionHandled;
        }


        /*  ------------------------------------------------------------------------------------------*/

        private bool success = true;
        private string msg;

        private void RaiseAjaxPostBackEvent(string eventArgument)
        {
            bool needConfirmation = false;
            try
            {
                if(string.IsNullOrEmpty(eventArgument))
                {
                    throw new ArgumentNullException("eventArgument");
                }

                XmlNode xmlData = this.SubmitConfig;
                string data = null;
                XmlNode parametersNode = null;

                if (xmlData != null)
                {
                    parametersNode = xmlData.SelectSingleNode("config/extraParams");
                
                    XmlNode serviceNode = xmlData.SelectSingleNode("config/serviceParams");
                    if (serviceNode != null)
                    {
                        data = serviceNode.InnerText;
                    }
                }

                string action = eventArgument;

                BeforeAjaxEventArgs e = new BeforeAjaxEventArgs(action, data, parametersNode);
                this.OnAjaxPostBack(e);
                DataSourceProxy dsp = this.Proxy.Proxy as DataSourceProxy;

                if(this.AutoDecode && !string.IsNullOrEmpty(data))
                {
                    data = this.Page.Server.HtmlDecode(data);
                }

                switch(action)
                {
                    case "update":
                        if (data == null)
                        {
                            throw new InvalidOperationException("No data in request");
                        }
                        needConfirmation = this.UseIdConfirmation;
                        this.DoSaving(data, parametersNode);
                        
                        if (this.RefreshAfterSaving == RefreshAfterSavingMode.None || dsp != null)
                        {
                            needRetrieve = false;
                        }
                        break;
                    case "refresh":
                        needRetrieve = true;
                        StoreRefreshDataEventArgs refreshArgs = new StoreRefreshDataEventArgs(parametersNode);
                        this.OnRefreshData(refreshArgs);
                        if(dsp != null)
                        {
                            if (refreshArgs.TotalCount > -1)
                            {
                                dsp.TotalCount = refreshArgs.TotalCount; 
                            }
                        }
                        break;
                    case "submit":
                        needRetrieve = false;
                        if (data == null)
                        {
                            throw new InvalidOperationException("No data in request");
                        }

                        StoreSubmitDataEventArgs args =new StoreSubmitDataEventArgs(data, parametersNode);
                        this.OnSubmitData(args);

                        break;
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = this.IsDebugging ? ex.ToString() : ex.Message;
                if (this.ScriptManager.RethrowAjaxExceptions)
                {
                    throw;
                }
            }

            AfterAjaxEventArgs eAjaxPostBackResult = new AfterAjaxEventArgs(new Response(success, msg));
            this.OnAjaxPostBackResult(eAjaxPostBackResult);
            
            StoreResponseData response = new StoreResponseData();
            
            if (needRetrieve && eAjaxPostBackResult.Response.Success)
            {
                if(this.RequiresDataBinding)
                {
                    this.DataBind(); 
                }

                response.Data = this.Data != null ? JSON.Serialize(this.Data) : this.JsonData;
                DataSourceProxy dsp = this.Proxy.Proxy as DataSourceProxy;
                response.TotalCount = dsp != null ? dsp.TotalCount : 0;
            }

            if(needConfirmation)
            {
                response.Confirmation = changingEventArgs.ConfirmationList;
            }

            eAjaxPostBackResult.Response.Data = response.ToString();

            ScriptManager.ServiceResponse = eAjaxPostBackResult.Response;
        }

        string ICustomConfigSerialization.Serialize(Control owner)
        {
            string className = "Coolite.Ext.Store";

            if (this.Proxy.Count == 0 || !this.RemotePaging)
            {
                className = "Ext.ux.data.PagingStore";
            }

            return string.Concat("this.", this.ClientID, " = new ", className, "(", new ClientConfig().Serialize(this, true), ");");
        }
    }
}
