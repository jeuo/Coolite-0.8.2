/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.grid.ColumnModel")]
    [ToolboxItem(false)]
    public class ColumnModel : InnerObservable
    {
        private ColumnCollection columns;

        /// <summary>
        /// The columns to use when rendering the grid (required).
        /// </summary>
        [ClientConfig("columns", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("The columns to use when rendering the grid (required).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new ColumnCollection();
                }
                return this.columns;
            }
        }

        private ColumnListeners listeners;


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
        public ColumnListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ColumnListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private ColumnAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public ColumnAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ColumnAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        public string ColumnsToJson()
        {
            StringBuilder sb = new StringBuilder();
            bool comma = false;
            sb.Append("[");
            foreach (ColumnBase column in this.Columns)
            {
                if (comma)
                {
                    sb.Append(",");
                }

                sb.Append(new ClientConfig(true).SerializeInternal(column, this));

                comma = true;
            }
            sb.Append("]");

            return sb.ToString();
        }

        public GridPanel ParentGrid
        {
            get
            {
                return this.ParentComponent as GridPanel;
            }
        }

        /// <summary>
        /// Sets the header for a column.
        /// </summary>
        [Description("Sets the header for a column.")]
        public virtual void SetColumnHeader(int columnIndex, string header)
        {
            const string template = "{0}.getColumnModel().setColumnHeader({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, JSON.Serialize(header));
        }

        /// <summary>
        /// Sets the tooltip for a column.
        /// </summary>
        [Description("Sets the tooltip for a column.")]
        public virtual void SetColumnTooltip(int columnIndex, string tooltip)
        {
            const string template = "{0}.getColumnModel().setColumnTooltip({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, JSON.Serialize(tooltip));
        }

        /// <summary>
        /// Sets the width for a column.
        /// </summary>
        [Description("Sets the width for a column.")]
        public virtual void SetColumnWidth(int columnIndex, int width)
        {
            const string template = "{0}.getColumnModel().setColumnWidth({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, width);
        }

        /// <summary>
        /// Sets the dataIndex for a column.
        /// </summary>
        [Description("Sets the dataIndex for a column.")]
        public virtual void SetDataIndex(int columnIndex, string dataIndex)
        {
            const string template = "{0}.getColumnModel().setDataIndex({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, JSON.Serialize(dataIndex));
            if(Ext.IsAjaxRequest)
            {
                this.ParentGrid.RefreshView();
            }
        }

        /// <summary>
        /// Sets if a column is editable.
        /// </summary>
        [Description("Sets if a column is editable.")]
        public virtual void SetEditable(int columnIndex, bool editable)
        {
            const string template = "{0}.getColumnModel().setEditable({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, JSON.Serialize(editable));
        }

        /// <summary>
        /// Sets the editor for a column.
        /// </summary>
        [Description("Sets the editor for a column.")]
        public virtual void SetEditor(int columnIndex, Field editor)
        {
            editor.ApplyTo = "";
            editor.RenderTo = "";
            editor.CancelRenderToParameter = true;
            ComboBox cbx = editor as ComboBox;
            if (cbx != null && string.IsNullOrEmpty(cbx.StoreID))
            {
                cbx.TriggerAction = TriggerAction.All;
                cbx.Mode = DataLoadMode.Local;
            }
            editor.Visible = false;

            const string template = "{0}.getColumnModel().setEditor({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, editor.GetClientConstructor(true));
        }

        /// <summary>
        /// Sets if a column is hidden.
        /// </summary>
        [Description("Sets if a column is hidden.")]
        public virtual void SetHidden(int columnIndex, bool hidden)
        {
            const string template = "{0}.getColumnModel().setHidden({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, JSON.Serialize(hidden));
        }

        /// <summary>
        /// Sets the rendering (formatting) function for a column. See Ext.util.Format for some default formatting functions.
        /// Parameters:
        ///     col : Number
        ///         The column index
        ///     fn : Function
        ///         The function to use to process the cell's raw data to return HTML markup for the grid view. 
        ///         The render function is called with the following parameters:
        ///             value : Object
        ///                 The data value for the cell.
        ///             metadata : Object
        ///                 An object in which you may set the following attributes:
        ///                     css : String
        ///                         A CSS class name to add to the cell's TD element.
        ///                     attr : String
        ///                         An HTML attribute definition string to apply to the data container element within the table cell (e.g. 'style="color:red;"').
        ///             record : Ext.data.record
        ///                 The Ext.data.Record from which the data was extracted.
        ///             rowIndex : Number
        ///                 Row index
        ///             colIndex : Number
        ///                 Column index
        ///             store : Ext.data.Store
        ///                 The Ext.data.Store object from which the Record was extracted.
        ///     Returns:
        ///         void
        /// </summary>
        [Description("Sets the rendering (formatting) function for a column.")]
        public virtual void SetRenderer(int columnIndex, Renderer renderer)
        {
            const string template = "{0}.getColumnModel().setRenderer({1}, {2});";
            this.AddScript(template, this.ParentGrid.ClientID, columnIndex, renderer.ToConfigString());
        }

        public virtual void RegisterCommandStyleRules()
        {
            this.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.commandcolumn.commandcolumn.css");
        }
    }
}