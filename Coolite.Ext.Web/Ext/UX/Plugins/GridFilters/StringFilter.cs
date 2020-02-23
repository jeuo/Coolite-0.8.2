/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    public class StringFilter : GridFilter
    {
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override FilterType Type
        {
            get 
            { 
                return FilterType.String;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Predefined filter value")]
        public virtual string Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        public void SetValue(string value)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), JSON.Serialize(value));
            }
        }
    }
}
