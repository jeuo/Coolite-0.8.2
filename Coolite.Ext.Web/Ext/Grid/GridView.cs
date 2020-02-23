/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This class encapsulates the user interface of an Ext.grid.GridPanel. Methods of this 
    /// class may be used to access user interface elements to enable special display effects. 
    /// Do not change the DOM structure of the user interface.
    ///
    /// This class does not provide ways to manipulate the underlying data. The data model of a 
    /// Grid is held in an Ext.data.Store.
    /// </summary>
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.grid.GridView")]
    [Description("This class encapsulates the user interface of an Ext.grid.GridPanel. Methods of this class may be used to access user interface elements to enable special display effects. Do not change the DOM structure of the user interface.This class does not provide ways to manipulate the underlying data. The data model of a Grid is held in an Ext.data.Store.")]
    public class GridView : InnerObservable
    {
        private GridPanelBase parentGrid;

        public GridPanelBase ParentGrid
        {
            get
            {
                return this.parentGrid;
            }
            internal set
            {
                this.parentGrid = value;
            }
        }

        protected override void OnBeforeClientInitHandler()
        {
            base.OnBeforeClientInitHandler();
            if (this.Templates.Header != null)
            {
                this.Templates.Header.RegisterScript(false);
            }
            if (this.GetRowClass != null && !string.IsNullOrEmpty(this.GetRowClass.Handler))
            {
                this.GetRowClass.FormatHandler = false;
                this.GetRowClass.Args = new string[] { "record", "index", "rowParams", "store" };
            }
        }

        /// <summary>
        /// True to auto expand the columns to fit the grid when the grid is created.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to auto expand the columns to fit the grid when the grid is created.")]
        public virtual bool AutoFill
        {
            get
            {
                object obj = this.ViewState["AutoFill"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoFill"] = value;
            }
        }

        /// <summary>
        /// The text displayed in the \"Columns\" menu item
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text displayed in the \"Columns\" menu item")]
        public virtual string ColumnsText
        {
            get
            {
                return (string)this.ViewState["ColumnsText"] ?? "";
            }
            set
            {
                this.ViewState["ColumnsText"] = value;
            }
        }

        /// <summary>
        /// The selector used to find cells internally
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The selector used to find cells internally")]
        public virtual string CellSelector
        {
            get
            {
                return (string)this.ViewState["CellSelector"] ?? "";
            }
            set
            {
                this.ViewState["CellSelector"] = value;
            }
        }

        /// <summary>
        /// The number of levels to search for cells in event delegation (defaults to 4)
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue(4)]
        [Description("The number of levels to search for cells in event delegation (defaults to 4)")]
        public virtual int CellSelectorDepth
        {
            get
            {
                object obj = this.ViewState["CellSelectorDepth"];
                return (obj == null) ? 4 : (int)obj;
            }
            set
            {
                this.ViewState["CellSelectorDepth"] = value;
            }
        }

        /// <summary>
        /// True to defer emptyText being applied until the store's first load
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to defer emptyText being applied until the store's first load")]
        public virtual bool DeferEmptyText
        {
            get
            {
                object obj = this.ViewState["DeferEmptyText"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["DeferEmptyText"] = value;
            }
        }


        /// <summary>
        /// Default text to display in the grid body when no rows are available (defaults to '').
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Default text to display in the grid body when no rows are available (defaults to '').")]
        public virtual string EmptyText
        {
            get
            {
                return (string)this.ViewState["EmptyText"] ?? "";
            }
            set
            {
                this.ViewState["EmptyText"] = value;
            }
        }

        /// <summary>
        /// True to add a second TR element per row that can be used to provide a row body that spans 
        /// beneath the data row. Use the getRowClass method's rowParams config to customize the row body.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to add a second TR element per row that can be used to provide a row body that spans beneath the data row. Use the getRowClass method's rowParams config to customize the row body.")]
        public virtual bool EnableRowBody
        {
            get
            {
                object obj = this.ViewState["EnableRowBody"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableRowBody"] = value;
            }
        }

        /// <summary>
        /// True to auto expand/contract the size of the columns to fit the grid width and prevent 
        /// horizontal scrolling.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to auto expand/contract the size of the columns to fit the grid width and prevent horizontal scrolling.")]
        public virtual bool ForceFit
        {
            get
            {
                object obj = this.ViewState["ForceFit"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ForceFit"] = value;
            }
        }

        /// <summary>
        /// The selector used to find rows internally
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The selector used to find rows internally")]
        public virtual string RowSelector
        {
            get
            {
                return (string)this.ViewState["RowSelector"] ?? "";
            }
            set
            {
                this.ViewState["RowSelector"] = value;
            }
        }

        /// <summary>
        /// The number of levels to search for rows in event delegation (defaults to 10)
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue(10)]
        [Description("The number of levels to search for rows in event delegation (defaults to 10)")]
        public virtual int RowSelectorDepth
        {
            get
            {
                object obj = this.ViewState["RowSelectorDepth"];
                return (obj == null) ? 10 : (int)obj;
            }
            set
            {
                this.ViewState["RowSelectorDepth"] = value;
            }
        }

        /// <summary>
        /// The amount of space to reserve for the scrollbar (defaults to 19 pixels)
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue(19)]
        [Description("The amount of space to reserve for the scrollbar (defaults to 19 pixels)")]
        public virtual int ScrollOffset
        {
            get
            {
                object obj = this.ViewState["ScrollOffset"];
                return (obj == null) ? 19 : (int)obj;
            }
            set
            {
                this.ViewState["ScrollOffset"] = value;
            }
        }

        /// <summary>
        /// The text displayed in the \"Sort Ascending\" menu item
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text displayed in the \"Sort Ascending\" menu item")]
        public virtual string SortAscText
        {
            get
            {
                return (string)this.ViewState["SortAscText"] ?? "";
            }
            set
            {
                this.ViewState["SortAscText"] = value;
            }
        }

        /// <summary>
        /// The text displayed in the \"Sort Descending\" menu item
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text displayed in the \"Sort Descending\" menu item")]
        public virtual string SortDescText
        {
            get
            {
                return (string)this.ViewState["SortDescText"] ?? "";
            }
            set
            {
                this.ViewState["SortDescText"] = value;
            }
        }

        /// <summary>
        /// The width of the column header splitter target area.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(5)]
        [Description("The width of the column header splitter target area.")]
        public virtual int SplitHandleWidth
        {
            get
            {
                object obj = this.ViewState["SplitHandleWidth"];
                return (obj == null) ? 5 : (int)obj;
            }
            set
            {
                this.ViewState["SplitHandleWidth"] = value;
            }
        }

        private JFunction getRowClass;

        /// <summary>
        /// Override this function to apply custom CSS classes to rows during rendering.
        /// You can also supply custom parameters to the row template for the current row 
        /// to customize how it is rendered using the rowParams parameter. This function 
        /// should return the CSS class name (or empty string '' for none) that will be 
        /// added to the row's wrapping div. To apply multiple class names, simply return 
        /// them space-delimited within the string (e.g., 'my-class another-class').
        /// 
        /// Parameters:
        ///     record : Record
        ///         The Ext.data.Record corresponding to the current row
        ///     index : Number
        ///         The row index
        ///     rowParams : Object
        ///         A config object that is passed to the row template during rendering 
        ///         that allows customization of various aspects of a body row, if applicable. 
        ///         Note that this object will only be applied if enableRowBody = true, 
        ///         otherwise it will be ignored. The object may contain any of these properties:
        ///     store : Store
        ///         The Ext.data.Store this grid is bound to
        /// Returns:
        ///     String
        ///     a CSS class name to add to the row.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Override this function to apply custom CSS classes to rows during rendering.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction GetRowClass
        {
            get
            {
                if (this.getRowClass == null)
                {
                    this.getRowClass = new JFunction();
                }
                return this.getRowClass;
            }
        }

        private GridViewListeners listeners;

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
        public GridViewListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new GridViewListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private GridViewAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public GridViewAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new GridViewAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        public virtual bool StandardHeaderRow
        {
            get
            {
                object obj = this.ViewState["StandardHeaderRow"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["StandardHeaderRow"] = value;
            }
        }

        private HeaderRowCollection headerRows;

        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual HeaderRowCollection HeaderRows
        {
            get
            {
                if (this.headerRows == null)
                {
                    this.headerRows = new HeaderRowCollection();
                    this.headerRows.Owner = this;
                    this.headerRows.AfterItemAdd += HeaderRows_AfterItemAdd;
                }
                return this.headerRows;
            }
        }

        private void HeaderRows_AfterItemAdd(HeaderRow item)
        {
            if(this.ParentGrid != null)
            {
                foreach (HeaderColumn column in item.Columns)
                {
                    this.ParentGrid.EnsureHeaderColumn(this,column);
                }
                item.Columns.AfterItemAdd += Columns_AfterItemAdd;
            }
        }

        void Columns_AfterItemAdd(HeaderColumn item)
        {
            if (item.Component.Count > 0)
            {
                this.ParentGrid.EnsureHeaderColumn(this, item);
            }

            item.Component.AfterItemAdd += Component_AfterItemAdd;
        }

        void Component_AfterItemAdd(Component item)
        {
            this.ParentGrid.EnsureHeaderControl(this, item);
        }

        [DefaultValue("")]
        [ClientConfig("headerRows", JsonMode.Raw)]
        internal string HeaderRowsProxy
        {
            get
            {
                if(this.HeaderRows.Count == 0)
                {
                    return "";
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                foreach (HeaderRow row in this.HeaderRows)
                {
                    sb.Append("{columns:[");

                    foreach (HeaderColumn column in row.Columns)
                    {
                        sb.Append(new ClientConfig().SerializeInternal(column, this));
                        sb.Append(",");
                    }

                    if(row.Columns.Count > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }

                    sb.Append("]},");
                }

                sb.Remove(sb.Length - 1, 1);

                sb.Append("]");

                return sb.ToString();
            }
        }

        private GridViewTemplates templates;

        [ClientConfig("templates", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridViewTemplates Templates
        {
            get
            {
                if(this.templates == null)
                {
                    this.templates = new GridViewTemplates(this);
                }

                return this.templates;
            }
        }
    }

    public class GridViewTemplates : StateManagedItem
    {
        public GridViewTemplates(Control owner) : base(owner)
        {
        }

        public GridView View
        {
            get
            {
                return this.Owner as GridView;
            }
        }

        private XTemplate header;

        [ClientConfig("header", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        public XTemplate Header
        {
            get
            {
                if (this.View.ParentGrid is PropertyGrid)
                {
                    return null;
                }
                
                if (this.header == null)
                {
                    this.header = new XTemplate();
                }

                if (this.View.HeaderRows.Count > 0)
                {
                    if (this.View.ScriptManager.RenderStyles == ResourceLocationType.Embedded)
                    {
                        this.View.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.multiheader.css.multiheader-embedded.css");
                    }
                    else if (this.View.ScriptManager.RenderStyles == ResourceLocationType.File || this.View.ScriptManager.RenderStyles == ResourceLocationType.CacheFly || this.View.ScriptManager.RenderStyles == ResourceLocationType.CacheFlyAndFile)
                    {
                        this.View.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.multiheader.css.multiheader.css");
                    }
                    
                    StringBuilder sb = new StringBuilder(128);
                    sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"{tstyle}\">");

                    if (this.View.StandardHeaderRow)
                    {
                        sb.Append("<thead><tr class=\"x-grid3-hd-row x-grid3-sadd-row\">{cells}</tr></thead>");
                    }

                    sb.Append("<tbody>");
                    int rowIndex = 0;

                    foreach (HeaderRow headerRow in this.View.HeaderRows)
                    {
                        sb.AppendFormat("<tr class=\"x-grid3-hd-row x-grid3-add-row x-grid3-hd-row-r{1} {0}\">", headerRow.Cls, rowIndex++);

                        int colIndex = 0;

                        foreach (HeaderColumn headerColumn in headerRow.Columns)
                        {
                            sb.AppendFormat("<td class=\"x-grid3-hd x-grid3-cell x-grid3-td-c{0}\"><div class=\"x-grid3-hd-inner x-grid3-add {1}\"></div></td>", colIndex++, headerColumn.Cls);
                        }

                        sb.Append("</tr>");
                    }

                    sb.Append("</tbody>");
                    sb.Append("</table>");

                    this.header.Text = sb.ToString();
                }

                return this.header;
            }
        }
    }

    public class HeaderColumn : StateManagedItem
    {
        [ClientConfig("target", JsonMode.Raw)]
        [DefaultValue("")]
        internal string TargetProxy
        {
            get
            {
                if (string.IsNullOrEmpty(this.Target))
                {
                    return "undefined";
                }
                return string.Concat("Coolite.Ext.getEl(", TokenUtils.ParseAndNormalize(this.Target, this.Owner), ")");
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The target element which will be placed to the header.")]
        public virtual string Target
        {
            get
            {
                return (string)this.ViewState["Target"] ?? "";
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        [ClientConfig("autoWidth")]
        [Category("Config Options")]
        [DefaultValue(true)]
        public virtual bool AutoWidthElement
        {
            get
            {
                object obj = this.ViewState["AutoWidthElement"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoWidthElement"] = value;
            }
        }

        [ClientConfig("correction")]
        [Category("Config Options")]
        [DefaultValue(3)]
        public virtual int AutoWidthCorrection
        {
            get
            {
                object obj = this.ViewState["AutoWidthCorrection"];
                return (obj == null) ? 3 : (int)obj;
            }
            set
            {
                this.ViewState["AutoWidthCorrection"] = value;
            }
        }

        [DefaultValue("")]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        ItemsCollection<Component> component;

        /// <summary>
        /// Items collection
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ClientConfig("component", typeof(ItemCollectionJsonConverter))]
        public virtual ItemsCollection<Component> Component
        {
            get
            {
                if (this.component == null)
                {
                    this.component = new ItemsCollection<Component>();
                    this.component.SingleItemMode = true;
                }
                return this.component;
            }
        }
    }

    public class HeaderColumnCollection : StateManagedCollection<HeaderColumn>
    {

    }

    public class HeaderRow : StateManagedItem
    {
        private HeaderColumnCollection columns;

        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual HeaderColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new HeaderColumnCollection();
                }
                return this.columns;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }
    }

    public class HeaderRowCollection : StateManagedCollection<HeaderRow>
    {
    }
}
