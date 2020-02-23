/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Adds the ability for single level grouping to the grid.
    /// </summary>
    [InstanceOf(ClassName = "Ext.grid.GroupingView")]
    [Description("Adds the ability for single level grouping to the grid.")]
    public class GroupingView : GridView
    {

        /// <summary>
        /// The text to display when there is an empty group value.
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text to display when there is an empty group value.")]
        public virtual string EmptyGroupText
        {
            get
            {
                return (string)this.ViewState["EmptyGroupText"] ?? "";
            }
            set
            {
                this.ViewState["EmptyGroupText"] = value;
            }
        }

        /// <summary>
        /// False to disable grouping functionality (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable grouping functionality (defaults to true).")]
        public virtual bool EnableGrouping
        {
            get
            {
                object obj = this.ViewState["EnableGrouping"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableGrouping"] = value;
            }
        }

        /// <summary>
        /// True to enable the grouping control in the column menu.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable the grouping control in the column menu.")]
        public virtual bool EnableGroupingMenu
        {
            get
            {
                object obj = this.ViewState["EnableGroupingMenu"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableGroupingMenu"] = value;
            }
        }

        /// <summary>
        /// True to allow the user to turn off grouping.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to allow the user to turn off grouping.")]
        public virtual bool EnableNoGroups
        {
            get
            {
                object obj = this.ViewState["EnableNoGroups"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableNoGroups"] = value;
            }
        }

        /// <summary>
        /// Text displayed in the grid header menu for grouping by a column (defaults to 'Group By This Field').
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("Group By This Field")]
        [Description("Text displayed in the grid header menu for grouping by a column (defaults to 'Group By This Field').")]
        public virtual string GroupByText
        {
            get
            {
                return (string)this.ViewState["GroupByText"] ?? "Group By This Field";
            }
            set
            {
                this.ViewState["GroupByText"] = value;
            }
        }

        //private Renderer renderer;

        ///// <summary>
        ///// The function used to format the grouping field value for display in the group header. 
        ///// Should return a string value. This takes the following parameters:
        ///// 
        ///// v : Object
        /////     The new value of the group field.
        ///// unused : undefined
        /////     Unused parameter.
        ///// r : Ext.data.Record
        /////     The Record providing the data for the row which caused group change.
        ///// rowIndex : Number
        /////     The row index of the Record which caused group change.
        ///// colIndex : Number
        /////     The column index of the group field.
        ///// ds : Ext.data.Store
        /////     The Store which is providing the data Model.
        ///// Returns:
        /////     void
        ///// </summary>
        //[ClientConfig(typeof(RendererJsonConverter))]
        //[Category("Config Options")]
        //[DefaultValue(null)]
        //[Description("The function used to format the grouping field value for display in the group header. Should return a string value.")]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //[ViewStateMember]
        //public virtual Renderer GroupRenderer
        //{
        //    get
        //    {
        //        if (this.renderer == null)
        //        {
        //            this.renderer = new Renderer();
        //        }

        //        return this.renderer;
        //    }
        //    set
        //    {
        //        this.renderer = value;
        //    }
        //}

        ///// <summary>
        ///// The function used to format the grouping field value for display in the group header. Should return a string value.
        ///// </summary>
        //[ClientConfig]
        //[Category("Config Options")]
        //[DefaultValue("")]
        //[Description("The function used to format the grouping field value for display in the group header. Should return a string value.")]
        //public virtual string GroupRenderer
        //{
        //    get
        //    {
        //        return (string)this.ViewState["GroupRenderer"] ?? "";
        //    }
        //    set
        //    {
        //        this.ViewState["GroupRenderer"] = value;
        //    }
        //}

        /// <summary>
        /// The template used to render the group header. See Ext.XTemplate for information on how to format data using a template.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The template used to render the group header. See Ext.XTemplate for information on how to format data using a template.")]
        public virtual string GroupTextTpl
        {
            get
            {
                return (string)this.ViewState["GroupTextTpl"] ?? "";
            }
            set
            {
                this.ViewState["GroupTextTpl"] = value;
            }
        }

        /// <summary>
        /// The text with which to prefix the group field value in the group header line.
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text with which to prefix the group field value in the group header line.")]
        public virtual string Header
        {
            get
            {
                return (string)this.ViewState["Header"] ?? "";
            }
            set
            {
                this.ViewState["Header"] = value;
            }
        }

        /// <summary>
        /// True to hide the column that is currently grouped.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the column that is currently grouped.")]
        public virtual bool HideGroupedColumn
        {
            get
            {
                object obj = this.ViewState["HideGroupedColumn"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideGroupedColumn"] = value;
            }
        }

        /// <summary>
        /// True to skip refreshing the view when new rows are added (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to skip refreshing the view when new rows are added (defaults to false).")]
        public virtual bool IgnoreAdd
        {
            get
            {
                object obj = this.ViewState["IgnoreAdd"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["IgnoreAdd"] = value;
            }
        }

        /// <summary>
        /// True to display the name for each set of grouped rows (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the name for each set of grouped rows (defaults to true).")]
        public virtual bool ShowGroupName
        {
            get
            {
                object obj = this.ViewState["ShowGroupName"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ShowGroupName"] = value;
            }
        }

        /// <summary>
        /// Text displayed in the grid header for enabling/disabling grouping (defaults to 'Show in Groups').
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("Show in Groups")]
        [Description("Text displayed in the grid header for enabling/disabling grouping (defaults to 'Show in Groups').")]
        public virtual string ShowGroupsText
        {
            get
            {
                return (string)this.ViewState["ShowGroupsText"] ?? "Show in Groups";
            }
            set
            {
                this.ViewState["ShowGroupsText"] = value;
            }
        }

        /// <summary>
        /// True to start all groups collapsed.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to start all groups collapsed.")]
        public virtual bool StartCollapsed
        {
            get
            {
                object obj = this.ViewState["StartCollapsed"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StartCollapsed"] = value;
            }
        }
    }
}