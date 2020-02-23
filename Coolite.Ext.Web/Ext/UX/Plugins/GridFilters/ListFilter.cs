/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    public class ListFilter : GridFilter
    {
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override FilterType Type
        {
            get 
            { 
                return FilterType.List;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Loading...")]
        [NotifyParentProperty(true)]
        [Description("The loading text displayed when data loading")]
        public string LoadingText
        {
            get
            {
                object obj = this.ViewState["LoadingText"];
                return obj == null ? "Loading..." : (string)obj;
            }
            set
            {
                this.ViewState["LoadingText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("If true then the data loading on show")]
        public bool LoadOnShow
        {
            get
            {
                object obj = this.ViewState["LoadOnShow"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["LoadOnShow"] = value;
            }
        }

        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [DefaultValue(null)]
        [Description("The list of options")]
        public virtual string[] Options
        {
            get
            {
                object obj = this.ViewState["Options"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["Options"] = value;
            }
        }

        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [DefaultValue(null)]
        [Description("The list of options")]
        public virtual string[] Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        public void SetValue(string[] value)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), JSON.Serialize(value));
            }
        }
    }
}
