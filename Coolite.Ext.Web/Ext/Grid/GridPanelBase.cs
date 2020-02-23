/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public abstract class GridPanelBase : PanelBase
    {
        protected override void OnPreRender(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.AutoExpandColumn))
            {
                bool found = false;
                ColumnBase foundColumn = null;
                foreach (ColumnBase column in this.ColumnModel.Columns)
                {
                    if (column.ColumnID == this.AutoExpandColumn)
                    {
                        found = true;
                        break;
                    }

                    if(column.DataIndex == this.AutoExpandColumn)
                    {
                        foundColumn = column;
                    }
                }

                if (!found && foundColumn != null)
                {
                    foundColumn.ColumnID = this.AutoExpandColumn;
                }
            }

            foreach (ColumnBase column in this.ColumnModel.Columns)
            {
                if (column.Editor.Count == 0)
                {
                    continue;
                }

                Field editor = column.Editor[0];
                editor.RegisterStyles();
                editor.RegisterScripts();

                if(column.Editor[0] is ComboBox)
                {
                    ComboBox cbx = (ComboBox) column.Editor[0];
                    XTemplate tpl = cbx.Template;
                    if(!string.IsNullOrEmpty(tpl.Text))
                    {
                        cbx.Controls.Remove(tpl);
                        this.Controls.Add(tpl);
                        tpl.RegisterScript(true);
                    }
                }
            }
            
            if (!this.DesignMode && Ext.IsIE6)
            {
                this.ScriptManager.RegisterClientStyleBlock("grid-header-offset-fix", ".x-grid3-header-offset {width: auto;}");
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// The id of a column in this grid that should expand to fill unused space. This id can not be 0.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id of a column in this grid that should expand to fill unused space. This id can not be 0.")]
        public virtual string AutoExpandColumn
        {
            get
            {
                return (string)this.ViewState["AutoExpandColumn"] ?? "";
            }
            set
            {
                this.ViewState["AutoExpandColumn"] = value;
            }
        }

        /// <summary>
        /// The maximum width the autoExpandColumn can have (if enabled). Defaults to 1000.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(1000)]
        [Description("The maximum width the autoExpandColumn can have (if enabled). Defaults to 1000.")]
        public virtual int AutoExpandMax
        {
            get
            {
                object obj = this.ViewState["AutoExpandMax"];
                return (obj == null) ? 1000 : (int)obj;
            }
            set
            {
                this.ViewState["AutoExpandMax"] = value;
            }
        }

        /// <summary>
        /// The minimum width the autoExpandColumn can have (if enabled). defaults to 50.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(50)]
        [Description("The minimum width the autoExpandColumn can have (if enabled). defaults to 50.")]
        public virtual int AutoExpandMin
        {
            get
            {
                object obj = this.ViewState["AutoExpandMin"];
                return (obj == null) ? 50 : (int)obj;
            }
            set
            {
                this.ViewState["AutoExpandMin"] = value;
            }
        }

        private ColumnModel columnModel;

        /// <summary>
        /// The Ext.grid.ColumnModel to use when rendering the grid (required).
        /// </summary>
        [ClientConfig("cm", JsonMode.Object)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("The Ext.grid.ColumnModel to use when rendering the grid (required).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ColumnModel ColumnModel
        {
            get
            {
                if (this.columnModel == null)
                {
                    this.columnModel = new ColumnModel();
                    this.columnModel.Columns.AfterItemAdd += Columns_AfterItemAdd;
                }
                return this.columnModel;
            }
        }

        void Columns_AfterItemAdd(ColumnBase item)
        {
            this.CreateColumn(item);
        }

        /// <summary>
        /// True to clear editor filter before start editing. Default is true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to clear editor filter before start editing. Default is true.")]
        public virtual bool ClearEditorFilter
        {
            get
            {
                object obj = this.ViewState["ClearEditorFilter"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ClearEditorFilter"] = value;
            }
        }

        /// <summary>
        /// True to enable deferred row rendering. Default is true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable deferred row rendering. Default is true.")]
        public virtual bool DeferRowRender
        {
            get
            {
                object obj = this.ViewState["DeferRowRender"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["DeferRowRender"] = value;
            }
        }

        /// <summary>
        /// True to disable selections in the grid (defaults to false). - ignored a SelectionModel is specified.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to disable selections in the grid (defaults to false). - ignored a SelectionModel is specified.")]
        public virtual bool DisableSelection
        {
            get
            {
                object obj = this.ViewState["DisableSelection"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DisableSelection"] = value;
            }
        }

        /// <summary>
        /// True to enable hiding of columns with the header context menu.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable hiding of columns with the header context menu.")]
        public virtual bool EnableColumnHide
        {
            get
            {
                object obj = this.ViewState["EnableColumnHide"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableColumnHide"] = value;
            }
        }

        /// <summary>
        /// True to enable drag and drop reorder of columns.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable drag and drop reorder of columns.")]
        public virtual bool EnableColumnMove
        {
            get
            {
                object obj = this.ViewState["EnableColumnMove"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableColumnMove"] = value;
            }
        }

        /// <summary>
        /// False to turn off column resizing for the whole grid (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to turn off column resizing for the whole grid (defaults to true).")]
        public virtual bool EnableColumnResize
        {
            get
            {
                object obj = this.ViewState["EnableColumnResize"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableColumnResize"] = value;
            }
        }

        /// <summary>
        /// True to enable drag and drop of rows.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable drag and drop of rows.")]
        public virtual bool EnableDragDrop
        {
            get
            {
                object obj = this.ViewState["EnableDragDrop"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableDragDrop"] = value;
            }
        }

        /// <summary>
        /// True to enable the drop down button for menu in the headers.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable the drop down button for menu in the headers.")]
        public virtual bool EnableHdMenu
        {
            get
            {
                object obj = this.ViewState["EnableHdMenu"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableHdMenu"] = value;
            }
        }

        /// <summary>
        /// True to hide the grid's header (defaults to false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetHideHeaders")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the grid's header (defaults to false).")]
        public virtual bool HideHeaders
        {
            get
            {
                object obj = this.ViewState["HideHeaders"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideHeaders"] = value;
            }
        }

        private LoadMask loadMask;

        /// <summary>
        /// An Ext.LoadMask to mask the grid while loading (defaults to false).
        /// </summary>
        [ClientConfig("loadMask", typeof(LoadMaskJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An Ext.LoadMask to mask the grid while loading (defaults to false).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public override LoadMask LoadMask
        {
            get
            {
                if (this.loadMask == null)
                { 
                    this.loadMask = new LoadMask();
                    this.loadMask.TrackViewState();
                }

                return this.loadMask;
            }
        }

        private SaveMask saveMask;

        /// <summary>
        /// An Ext.SaveMask to mask the grid while saving (defaults to false).
        /// </summary>
        [ClientConfig("saveMask", typeof(LoadMaskJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An Ext.SaveMask to mask the grid while saving (defaults to false).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SaveMask SaveMask
        {
            get
            {
                if (this.saveMask == null)
                {
                    this.saveMask = new SaveMask();
                    this.saveMask.TrackViewState();
                }

                return this.saveMask;
            }
        }

        /// <summary>
        /// Sets the maximum height of the grid - ignored if autoHeight is not on.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(400)]
        [Description("Sets the maximum height of the grid - ignored if autoHeight is not on.")]
        public virtual int MaxHeight
        {
            get
            {
                object obj = this.ViewState["MaxHeight"];
                return (obj == null) ? 400 : (int)obj;
            }
            set
            {
                this.ViewState["MaxHeight"] = value;
            }
        }

        /// <summary>
        /// The minimum width a column can be resized to. Defaults to 25.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(25)]
        [Description("The minimum width a column can be resized to. Defaults to 25.")]
        public virtual int MinColumnWidth
        {
            get
            {
                object obj = this.ViewState["MinColumnWidth"];
                return (obj == null) ? 25 : (int)obj;
            }
            set
            {
                this.ViewState["MinColumnWidth"] = value;
            }
        }

        /// <summary>
        /// True to autoSize the grid when the window resizes. Defaults to true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to autoSize the grid when the window resizes. Defaults to true.")]
        public virtual bool MonitorWindowResize
        {
            get
            {
                object obj = this.ViewState["MonitorWindowResize"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MonitorWindowResize"] = value;
            }
        }

        private SelectionModelCollection selectionModel;

        /// <summary>
        /// Any subclass of AbstractSelectionModel that will provide the selection model for the grid (defaults to Ext.grid.RowSelectionModel if not specified).
        /// </summary>
        [ClientConfig("sm>Primary")]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Any subclass of AbstractSelectionModel that will provide the selection model for the grid (defaults to Ext.grid.RowSelectionModel if not specified).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual SelectionModelCollection SelectionModel
        {
            get
            {
                if(this.selectionModel == null)
                {
                    this.selectionModel = new SelectionModelCollection();
                    this.selectionModel.AfterItemAdd += this.AfterItemAdd;
                }

                return this.selectionModel;
            }
        }

        protected virtual void AfterItemAdd(Observable item)
        {
            this.Controls.AddAt(0, item);
        }

        /// <summary>
        /// The data store to use.
        /// </summary>
        [ClientConfig("store", JsonMode.ToClientID)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The data store to use.")]
        [IDReferenceProperty(typeof(Store))]
        public virtual string StoreID
        {
            get
            {
                return (string)this.ViewState["StoreID"] ?? "";
            }
            set
            {
                this.ViewState["StoreID"] = value;
                this.ListenStore();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ListenStore();
        }

        private void ListenStore()
        {
            if (!this.DesignMode && this.Page != null && !string.IsNullOrEmpty(this.StoreID) && !Ext.IsAjaxRequest)
            {
                Store store = ControlUtils.FindControl(this, this.StoreID) as Store;

                if (store != null)
                {
                    store.BeforeClientInit -= Store_BeforeClientInit;
                    store.BeforeClientInit += Store_BeforeClientInit;
                }
            }
        }

        void Store_BeforeClientInit(Observable sender)
        {
            Store store = ControlUtils.FindControl(this, this.StoreID) as Store;

            if (store != null)
            {
                //It doesn't need show mask for store ajax postback
                //store.AjaxEventConfig.EventMask.ShowMask = false;

                if (store.Proxy.Count == 0)
                {
                    if(store.IsAutoLoadUndefined)
                    {
                        store.AutoLoad = true;    
                    }
                    
                    PagingToolbar pBar = null;
                    if (this.BottomBar.Count > 0 && this.BottomBar[0] is PagingToolbar)
                    {
                        pBar = this.BottomBar[0] as PagingToolbar;
                    }
                    else if (this.TopBar.Count > 0 && this.TopBar[0] is PagingToolbar)
                    {
                        pBar = this.TopBar[0] as PagingToolbar;
                    }
                    
                    if (pBar != null)
                    {
                        if(store.Proxy.Count == 0 || !store.RemotePaging)
                        {
                            store.RemoteSort = true;
                        }

                        if (store.BaseParams["start"] == null)
                        {
                            store.BaseParams.Add(new Parameter("start", "0", ParameterMode.Raw));
                        }
                        if (store.BaseParams["limit"] == null)
                        {
                            store.BaseParams.Add(new Parameter("limit", pBar.PageSize.ToString(), ParameterMode.Raw));
                        }

                        string script = string.Format("Coolite.Ext.initRefreshPagingToolbar({0});", this.ClientID);
                        if (!this.ScriptManager.ScriptOnReadyBag.Values.Contains(script))
                        {
                            this.AddScript(script);
                        }
                    }
                }
            }
        }

        [ClientConfig("pbarID")]
        [Category("Config Options")]
        [DefaultValue("")]
        internal virtual string PbarID
        {
            get
            {
                PagingToolbar pBar = null;
                if (this.BottomBar.Count > 0 && this.BottomBar[0] is PagingToolbar)
                {
                    pBar = this.BottomBar[0] as PagingToolbar;
                }
                else if (this.TopBar.Count > 0 && this.TopBar[0] is PagingToolbar)
                {
                    pBar = this.TopBar[0] as PagingToolbar;
                }

                return (pBar != null && pBar.Visible) ? pBar.ClientID : "";
            }
        }

        /// <summary>
        /// True to stripe the rows. Default is false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to stripe the rows. Default is false.")]
        public virtual bool StripeRows
        {
            get
            {
                object obj = this.ViewState["StripeRows"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StripeRows"] = value;
            }
        }

        /// <summary>
        /// True to highlight rows when the mouse is over. Default is true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to highlight rows when the mouse is over. Default is true.")]
        public virtual bool TrackMouseOver
        {
            get
            {
                object obj = this.ViewState["TrackMouseOver"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TrackMouseOver"] = value;
            }
        }

        private GridViewCollection view;

        /// <summary>
        /// The Ext.grid.GridView used by the grid. This can be set before a call to render().
        /// </summary>
        [ClientConfig("view>View")]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("The Ext.grid.GridView used by the grid. This can be set before a call to render().")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual GridViewCollection View
        {
            get
            {
                if(this.view == null)
                {
                    this.view = new GridViewCollection();
                    this.view.AfterItemAdd += this.AfterViewAdd;
                }

                return this.view;
            }
        }

        private void AfterViewAdd(GridView item)
        {
            item.ParentGrid = this;
            this.Controls.AddAt(0, item);

            foreach (HeaderRow row in item.HeaderRows)
            {
                foreach (HeaderColumn column in row.Columns)
                {
                    this.EnsureHeaderColumn(item, column);
                }
            }
        }

        internal void EnsureHeaderColumn(GridView item, HeaderColumn column)
        {
            if (column.Component.Count > 0)
            {
                this.EnsureHeaderControl(item, column.Component[0]);
            }
        }

        internal void EnsureHeaderControl(GridView item, Component c)
        {
            if (!this.Controls.Contains(c))
            {
                this.Controls.AddAt(0, c);
            }

            if (!this.LazyItems.Contains(c))
            {
                this.LazyItems.Insert(0, c);
            }

            if (c is ButtonBase)
            {
                ButtonBase button = (ButtonBase)c;
                button.InitLazyMenu(item);
            }
        }

        /// <summary>
        /// True to automatically HTML encode and decode values pre and post edit (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to automatically HTML encode and decode values pre and post edit (defaults to false).")]
        public virtual bool AutoEncode
        {
            get
            {
                object obj = this.ViewState["AutoEncode"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoEncode"] = value;
            }
        }

        /// <summary>
        /// The number of clicks on a cell required to display the cell's editor (defaults to 2).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(2)]
        [Description("The number of clicks on a cell required to display the cell's editor (defaults to 2).")]
        public virtual int ClicksToEdit
        {
            get
            {
                object obj = this.ViewState["ClicksToEdit"];
                return (obj == null) ? 2 : (int)obj;
            }
            set
            {
                this.ViewState["ClicksToEdit"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Set init selecetion without event fires")]
        public virtual bool FireSelectOnLoad
        {
            get
            {
                object obj = this.ViewState["FireSelectOnLoad"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["FireSelectOnLoad"] = value;
            }
        }

        private int DefaultSelectionSavingBuffer()
        {
            if (this.SelectionModel.Primary != null && this.SelectionModel.Primary is RowSelectionModel)
            {
                return ((RowSelectionModel) this.SelectionModel.Primary).SingleSelect ? 0 : 10;
            }

            return 0;
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        public virtual int SelectionSavingBuffer
        {
            get
            {
                object obj = this.ViewState["SelectionSavingBuffer"];
                return (obj == null) ? this.DefaultSelectionSavingBuffer() : (int)obj;
            }
            set
            {
                this.ViewState["SelectionSavingBuffer"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(SelectionMemoryMode.Auto)]
        [Description("True to enable saving selection during paging. Default is true.")]
        public virtual SelectionMemoryMode SelectionMemory
        {
            get
            {
                object obj = this.ViewState["SelectionMemory"];
                return (obj == null) ? SelectionMemoryMode.Auto : (SelectionMemoryMode)obj;
            }
            set
            {
                this.ViewState["SelectionMemory"] = value;
            }
        }

        [ClientConfig("selectionMemory")]
        [DefaultValue(true)]
        internal bool SelectionMemoryProxy
        {
            get
            {
                switch(this.SelectionMemory)
                {
                    case SelectionMemoryMode.Auto:
                        RowSelectionModel sm = this.SelectionModel.Primary as RowSelectionModel;
                        if(sm != null && sm.SingleSelect)
                        {
                            return false;
                        }
                        
                        return !string.IsNullOrEmpty(this.PbarID);
                    case SelectionMemoryMode.Disabled:
                        return false;
                    case SelectionMemoryMode.Enabled:
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        public virtual string MemoryIDField
        {
            get
            {
                return (string)this.ViewState["MemoryIDField"] ?? "";
            }
            set
            {
                this.ViewState["MemoryIDField"] = value;
            }
        }

        [Description("Add new column to the end.")]
        public virtual void AddColumn(ColumnBase column)
        {
            if (column.Editor.Editor != null)
            {
                Field editor = column.Editor.Editor;
                editor.ApplyTo = null;
                editor.RenderTo = "";
                editor.CancelRenderToParameter = true;
                ComboBox cbx = editor as ComboBox;
                if (cbx != null && string.IsNullOrEmpty(cbx.StoreID))
                {
                    cbx.TriggerAction = TriggerAction.All;
                    cbx.Mode = DataLoadMode.Local;
                }

                editor.Visible = false;
            }
            
            const string template = "{0}.addColumn({1});";
            this.AddScript(template, this.ClientID, new ClientConfig(true).SerializeInternal(column, this));
        }

        [Description("Insert new column.")]
        public virtual void InsertColumn(int index, ColumnBase column)
        {
            if (column.Editor.Editor != null)
            {
                Field editor = column.Editor.Editor;
                editor.ApplyTo = null;
                editor.RenderTo = "";
                editor.CancelRenderToParameter = true;
                ComboBox cbx = editor as ComboBox;
                if (cbx != null && string.IsNullOrEmpty(cbx.StoreID))
                {
                    cbx.TriggerAction = TriggerAction.All;
                    cbx.Mode = DataLoadMode.Local;
                }
                editor.Visible = false;
            }

            const string template = "{0}.insertColumn({1}, {2});";
            this.AddScript(template, this.ClientID, index, new ClientConfig(true).SerializeInternal(column, this));
        }

        [Description("Remove column.")]
        public virtual void RemoveColumn(int index)
        {
            const string template = "{0}.removeColumn({1});";
            this.AddScript(template, this.ClientID, index);
        }

        [Description("Reconfigure columns.")]
        public virtual void Reconfigure()
        {
            foreach (ColumnBase column in this.ColumnModel.Columns)
            {
                if (column.Editor.Editor != null)
                {
                    Field editor = column.Editor.Editor;
                    editor.ApplyTo = null;
                    editor.RenderTo = "";
                    editor.CancelRenderToParameter = true;
                    ComboBox cbx = editor as ComboBox;
                    if (cbx != null && string.IsNullOrEmpty(cbx.StoreID))
                    {
                        cbx.TriggerAction = TriggerAction.All;
                        cbx.Mode = DataLoadMode.Local;
                    }

                    editor.Visible = false;
                }
            }
            
            const string template = "{0}.reconfigureColumns({1});";
            this.AddScript(template, this.ClientID, this.ColumnModel.ColumnsToJson());
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!this.Controls.Contains(this.ColumnModel))
            {
                this.Controls.Add(this.ColumnModel);
            }
            foreach (ColumnBase column in this.ColumnModel.Columns)
            {
                CreateColumn(column);
            }
        }

        private void CreateColumn(ColumnBase column)
        {
            if (column.Editor.Count == 0)
            {
                return;
            }

            Field editor = column.Editor[0];
            editor.Visible = false;
            editor.RenderTo = "";
            editor.ApplyTo = "";
            editor.CancelRenderToParameter = true;
            ComboBox cbx = editor as ComboBox;
            if (cbx != null && string.IsNullOrEmpty(cbx.StoreID))
            {
                cbx.TriggerAction = TriggerAction.All;
                cbx.Mode = DataLoadMode.Local;
            }

            if(cbx != null)
            {
                cbx.AllowCustomValue = cbx.Editable;
            }

            if (!this.Controls.Contains(editor))
            {
                this.Controls.Add(editor);
            }

            if (!this.LazyItems.Contains(editor))
            {
                this.LazyItems.Add(editor);
            }
        }

        [Description("Insert record")]
        public virtual void InsertRecord(int index, IDictionary<string, object> values)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.insertRecord({1}, {2});";
            this.AddScript(template, this.ClientID, index, JSON.Serialize(values, null, true));
        }

        [Description("Add record")]
        public virtual void AddRecord(IDictionary<string, object> values)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.addRecord({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(values, null, true));
        }

        [Description("Delete selected records")]
        public virtual void DeleteSelected()
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.deleteSelected();";
            this.AddScript(template, this.ClientID);
        }

        [Description("Refresh view")]
        public virtual void RefreshView()
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.getView().refresh(true);";
            this.AddScript(template, this.ClientID);
        }

        [Description("Show/Hide the grid's header.")]
        internal virtual void SetHideHeaders(bool hide)
        {
            Ext.EnsureAjaxEvent();
            this.AddScript("{0}.getView().mainHd.setDisplayed({1});", this.ClientID, JSON.Serialize(!hide));
        }
    }
}