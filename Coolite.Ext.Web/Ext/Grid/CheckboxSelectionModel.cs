/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A custom selection model that renders a column of checkboxes that can be toggled to select or deselect rows.
    /// </summary>
    [Description("A custom selection model that renders a column of checkboxes that can be toggled to select or deselect rows.")]
    [InstanceOf(ClassName = "Ext.grid.CheckboxSelectionModel")]
    [ToolboxItem(false)]
    public class CheckboxSelectionModel : RowSelectionModel
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.BeforeClientInit += CheckboxSelModel_OnBeforeClientInit;
            if (!this.DesignMode)
            {
                this.Page.LoadComplete += Page_LoadComplete;
            }
        }
        
        void CheckboxSelModel_OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (this.SingleSelect)
            {
                this.HideCheckAll = true;
            }

            if(this.HideCheckAll && !this.DesignMode && !Ext.IsAjaxRequest)
            {
                this.Header = "&nbsp;";
                this.ScriptManager.RegisterClientStyleBlock("hide-checkall-fix" + this.ParentComponent.ClientID.GetHashCode(), string.Concat("#", this.ParentComponent.ClientID, " .x-grid3-hd-checker{background: transparent !important;}"));
            }
        }

        /// <summary>
        /// Any valid text or HTML fragment to display in the header cell for the checkbox column
        /// (defaults to '<div class="x-grid3-hd-checker"> </div>'). The default CSS class 
        /// of 'x-grid3-hd-checker' displays a checkbox in the header and provides support for 
        /// automatic check all/none behavior on header click. This string can be replaced by any 
        /// valid HTML fragment, including a simple text string (e.g., 'Select Rows'), but the 
        /// automatic check all/none behavior will only work if the 'x-grid3-hd-checker' class 
        /// is supplied.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("<div class=\"x-grid3-hd-checker\"> </div>")]
        [Description("Any valid text or HTML fragment to display in the header cell for the checkbox column (defaults to '<div class='x-grid3-hd-checker'> </div>'). The default CSS class of 'x-grid3-hd-checker' displays a checkbox in the header and provides support for automatic check all/none behavior on header click. This string can be replaced by any valid HTML fragment, including a simple text string (e.g., 'Select Rows'), but the automatic check all/none behavior will only work if the 'x-grid3-hd-checker' class is supplied.")]
        public virtual string Header
        {
            get
            {
                return (string)this.ViewState["Header"] ?? "<div class=\"x-grid3-hd-checker\"> </div>";
            }
            set
            {
                this.ViewState["Header"] = value;
            }
        }

        /// <summary>
        /// True if the checkbox column is sortable (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if the checkbox column is sortable (defaults to false).")]
        public virtual bool Sortable
        {
            get
            {
                object obj = this.ViewState["Sortable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Sortable"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if need hide the checkbox in the header (defaults to false).")]
        public virtual bool HideCheckAll
        {
            get
            {
                object obj = this.ViewState["HideCheckAll"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideCheckAll"] = value;
            }
        }

        /// <summary>
        /// False if need disable deselection
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False if need disable deselection")]
        public virtual bool AllowDeselect
        {
            get
            {
                object obj = this.ViewState["AllowDeselect"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDeselect"] = value;
            }
        }

        /// <summary>
        /// The default width in pixels of the checkbox column (defaults to 20).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(20)]
        [Description("The default width in pixels of the checkbox column (defaults to 20).")]
        new public virtual int Width
        {
            get
            {
                object obj = this.ViewState["Width"];
                return (obj == null) ? 20 : (int)obj;
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        [DefaultValue(0)]
        public int ColumnPosition
        {
            get
            {
                object obj = this.ViewState["ColumnPosition"];
                return obj != null ? (int)obj : 0;
            }
            set
            {
                this.ViewState["ColumnPosition"] = value;
            }
        }

        /// <summary>
        /// true if rows can only be selected by clicking on the checkbox column (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("true if rows can only be selected by clicking on the checkbox column (defaults to false).")]
        public virtual bool CheckOnly
        {
            get
            {
                object obj = this.ViewState["CheckOnly"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CheckOnly"] = value;
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            GridPanel parent = this.ParentComponent as GridPanel;
            parent.ColumnModel.BeforeClientInit += new OnBeforeClientInitializedHandler(GridPanel_BeforeClientInit);
        }

        void GridPanel_BeforeClientInit(Observable sender)
        {
            if(!this.Visible)
            {
                return;
            }
            GridPanel parent = this.ParentComponent as GridPanel;
            parent.ColumnModel.Columns.Insert(this.ColumnPosition, new ReferenceColumn(this.ClientID));
        }
    }
}
