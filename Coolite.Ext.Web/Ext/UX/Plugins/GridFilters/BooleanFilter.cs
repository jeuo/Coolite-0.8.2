/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    public class BooleanFilter : GridFilter
    {
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override FilterType Type
        {
            get 
            {
                return FilterType.Boolean;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Yes")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'Yes' checkbox")]
        public string YesText
        {
            get
            {
                object obj = this.ViewState["YesText"];
                return obj == null ? "Yes" : (string)obj;
            }
            set
            {
                this.ViewState["YesText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("No")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'No' checkbox")]
        public string NoText
        {
            get
            {
                object obj = this.ViewState["NoText"];
                return obj == null ? "No" : (string)obj;
            }
            set
            {
                this.ViewState["NoText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("The default value of this filter (defaults to false)")]
        public bool DefaultValue
        {
            get
            {
                object obj = this.ViewState["DefaultValue"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DefaultValue"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Predefined filter value")]
        public virtual bool? Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? null : (bool?)obj;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        public void SetValue(bool? value)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), JSON.Serialize(value));
            }
        }
    }
}
