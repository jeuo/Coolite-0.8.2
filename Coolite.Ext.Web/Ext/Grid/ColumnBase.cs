/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class ColumnBase : StateManagedItem
    {
        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool Wrap
        {
            get
            {
                object obj = this.ViewState["Wrap"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Wrap"] = value;
            }
        }

        /// <summary>
        /// optional) Set the CSS text-align property of the column. Defaults to undefined.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(Alignment.Left)]
        [Description("(optional) Set the CSS text-align property of the column. Defaults to undefined.")]
        public virtual Alignment Align
        {
            get
            {
                object obj = this.ViewState["Align"];
                return (obj == null) ? Alignment.Left : (Alignment)obj;
            }
            set
            {
                this.ViewState["Align"] = value;
            }
        }

        /// <summary>
        /// (optional) Set custom CSS for all table cells in the column (excluding headers). Defaults to undefined.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) Set custom CSS for all table cells in the column (excluding headers). Defaults to undefined.")]
        public virtual string Css
        {
            get
            {
                object obj = this.ViewState["Css"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Css"] = value;
            }
        }

        /// <summary>
        /// (optional) The name of the field in the grid's Store's Record definition from which
        /// to draw the column's value. If not specified, the column's index is used as an index
        /// into the Record's data Array.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) The name of the field in the grid's Ext.data.Store's Ext.data.Record definition from which to draw the column's value. If not specified, the column's index is used as an index into the Record's data Array.")]
        public virtual string DataIndex
        {
            get
            {
                object obj = this.ViewState["DataIndex"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["DataIndex"] = value;
            }
        }

        private EditorCollection editor;

        /// <summary>
        /// (optional) The Field to use when editing values in this column if editing is supported by the grid.
        /// </summary>
        [ClientConfig("editor>Editor")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("(optional) The Ext.form.Field to use when editing values in this column if editing is supported by the grid.")]
        public virtual EditorCollection Editor
        {
            get
            {
                if (this.editor == null)
                {
                    editor = new EditorCollection();
                }

                return editor;
            }
        }

        /// <summary>
        /// (optional) True if the column width cannot be changed. Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("(optional) True if the column width cannot be changed. Defaults to false.")]
        public virtual bool Fixed
        {
            get
            {
                object obj = this.ViewState["Fixed"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Fixed"] = value;
            }
        }

        /// <summary>
        /// The header text to display in the Grid view.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The header text to display in the Grid view.")]
        public virtual string Header
        {
            get
            {
                object obj = this.ViewState["Header"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Header"] = value;
            }
        }

        /// <summary>
        /// (optional) True to hide the column. Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("(optional) True to hide the column. Defaults to false.")]
        public virtual bool Hidden
        {
            get
            {
                object obj = this.ViewState["Hidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hidden"] = value;
            }
        }

        /// <summary>
        /// (optional) Specify as false to prevent the user from hiding this column. Defaults to true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("(optional) Specify as false to prevent the user from hiding this column. Defaults to true.")]
        public virtual bool Hideable
        {
            get
            {
                object obj = this.ViewState["Hideable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Hideable"] = value;
            }
        }

        /// <summary>
        /// (optional) Defaults to the column's initial ordinal position. A name which identifies
        /// this column. The id is used to create a CSS class name which is applied to all table
        /// cells (including headers) in that column. The class name takes the form of
        /// 
        /// x-grid3-td-id
        ///
        ///
        /// Header cells will also recieve this class name, but will also have the class x-grid3-hd,
        /// so to target header cells, use CSS selectors such as:
        /// 
        /// .x-grid3-hd.x-grid3-td-id
        /// 
        /// The AutoExpandColumn grid config option references the column via this identifier.
        /// </summary>
        [ClientConfig("id")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) Defaults to the column's initial ordinal position. A name which identifies this column. The id is used to create a CSS class name which is applied to all table cells (including headers) in that column.")]
        //If the name of this property as ID then VS throws compiler error if same Column ID exists in another Component on the Page.
        public virtual string ColumnID
        {
            get
            {
                object obj = this.ViewState["ColumnID"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ColumnID"] = value;
            }
        }

        /// <summary>
        /// (optional) True to disable the column menu. Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("(optional) True to disable the column menu. Defaults to false.")]
        public virtual bool MenuDisabled
        {
            get
            {
                object obj = this.ViewState["MenuDisabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["MenuDisabled"] = value;
            }
        }

        /// <summary>
        /// (optional) A function used to generate HTML markup for a cell given the cell's data value.
        /// If not specified, the default renderer uses the raw data value.
        /// 
        /// Sets the rendering (formatting) function for a column. 
        /// See Ext.util.Format for some default formatting functions.
        ///
        /// The render function is called with the following parameters:
        ///     value : Object
        ///         The data value for the cell.
        ///     metadata : Object
        ///         An object in which you may set the following attributes:
        ///         
        ///         css : String
        ///             A CSS class name to add to the cell's TD element.
        ///         attr : String
        ///             An HTML attribute definition string to apply to the data container element
        ///              within the table cell (e.g. 'style="color:red;"').
        ///     
        ///     record : Ext.data.record
        ///         The Ext.data.Record from which the data was extracted.
        ///     rowIndex : Number
        ///         Row index
        ///     colIndex : Number
        ///         Column index
        ///     store : Ext.data.Store
        ///         The Ext.data.Store object from which the Record was extracted.
        /// Returns:
        ///     void
        /// </summary>

        private Renderer renderer;

        [ClientConfig(typeof(RendererJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("(optional) A function used to generate HTML markup for a cell given the cell's data value.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ViewStateMember]
        public virtual Renderer Renderer
        {
            get
            {
                if (this.renderer == null)
                {
                    this.renderer = new Renderer();
                }

                return this.renderer;
            }
            set
            {
                this.renderer = value;
            }
        }

        private Renderer groupRenderer;

        [ClientConfig(typeof(RendererJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("(optional) A function used to generate HTML markup for a cell given the cell's data value.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ViewStateMember]
        public virtual Renderer GroupRenderer
        {
            get
            {
                if (this.groupRenderer == null)
                {
                    this.groupRenderer = new Renderer();
                }

                return this.groupRenderer;
            }
            set
            {
                this.groupRenderer = value;
            }
        }

        /// <summary>
        /// (optional) False to disable grouping by this column. Defaults to true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("(optional) False to disable grouping by this column. Defaults to true.")]
        public virtual bool Groupable
        {
            get
            {
                object obj = this.ViewState["Groupable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Groupable"] = value;
            }
        }

        /// <summary>
        /// (optional) False to disable column resizing. Defaults to true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("(optional) False to disable column resizing. Defaults to true.")]
        public virtual bool Resizable
        {
            get
            {
                object obj = this.ViewState["Resizable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Resizable"] = value;
            }
        }

        /// <summary>
        /// (optional) True if sorting is to be allowed on this column. Defaults to the value
        /// of the defaultSortable property. Whether local/remote sorting is used is 
        /// specified in Ext.data.Store.remoteSort.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("(optional) True if sorting is to be allowed on this column. Defaults to the value of the defaultSortable property. Whether local/remote sorting is used is specified in Ext.data.Store.remoteSort.")]
        public virtual bool Sortable
        {
            get
            {
                object obj = this.ViewState["Sortable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Sortable"] = value;
            }
        }

        /// <summary>
        /// (optional) A text string to use as the column header's tooltip. If Quicktips are enabled,
        /// this value will be used as the text of the quick tip, otherwise it will be set as the
        /// header's HTML title attribute. Defaults to ''.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) A text string to use as the column header's tooltip. If Quicktips are enabled, this value will be used as the text of the quick tip, otherwise it will be set as the header's HTML title attribute. Defaults to ''.")]
        public virtual string Tooltip
        {
            get
            {
                object obj = this.ViewState["Tooltip"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Tooltip"] = value;
            }
        }

        /// <summary>
        /// (optional) The initial width in pixels of the column. Using this instead of Ext.grid.GridPanel.autoSizeColumns is more efficient.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "100")]
        [Description("(optional) The initial width in pixels of the column. Using this instead of Ext.grid.Grid.autoSizeColumns is more efficient.")]
        public virtual Unit Width
        {
            get
            {
                object obj = this.ViewState["Width"];
                return (obj == null) ? Unit.Pixel(100) : (Unit)obj;
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }
    }
}