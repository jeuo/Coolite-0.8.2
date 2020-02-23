/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.IO;
using System.Text;
using Coolite.Ext.Web;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class NumericFilter : GridFilter
    {
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override FilterType Type
        {
            get 
            { 
                return FilterType.Numeric;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Predefined filter value")]
        public virtual float? GreaterThanValue
        {
            get
            {
                object obj = this.ViewState["GreaterThanValue"];
                return (obj == null) ? null : (float?)obj;
            }
            set
            {
                this.ViewState["GreaterThanValue"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Predefined filter value")]
        public virtual float? LessThanValue
        {
            get
            {
                object obj = this.ViewState["LessThanValue"];
                return (obj == null) ? null : (float?)obj;
            }
            set
            {
                this.ViewState["LessThanValue"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Predefined filter value")]
        public virtual float? EqualValue
        {
            get
            {
                object obj = this.ViewState["EqualValue"];
                return (obj == null) ? null : (float?)obj;
            }
            set
            {
                this.ViewState["EqualValue"] = value;
            }
        }

        [ClientConfig("value", JsonMode.Raw)]
        [DefaultValue("")]
        internal string ValueProxy
        {
            get
            {
                if (this.GreaterThanValue.HasValue || this.LessThanValue.HasValue || this.EqualValue.HasValue)
                {
                    StringWriter sw = new StringWriter(new StringBuilder());
                    JsonTextWriter jw = new JsonTextWriter(sw);
                    jw.WriteStartObject();
                    if (this.GreaterThanValue.HasValue)
                    {
                        jw.WritePropertyName("gt");
                        jw.WriteValue(this.GreaterThanValue);
                    }

                    if (this.LessThanValue.HasValue)
                    {
                        jw.WritePropertyName("lt");
                        jw.WriteValue(this.LessThanValue);
                    }

                    if (this.EqualValue.HasValue)
                    {
                        jw.WritePropertyName("eq");
                        jw.WriteValue(this.EqualValue);
                    }
                    jw.WriteEndObject();

                    return sw.GetStringBuilder().ToString();
                }

                return "";
            }
        }

        public void SetValue(float? greaterThanValue, float? lessThanValue)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                string value = string.Concat("{gt:", greaterThanValue.HasValue ? JSON.Serialize(greaterThanValue.Value) : "undefined",
                    ",lt:", lessThanValue.HasValue ? JSON.Serialize(lessThanValue.Value) : "undefined", "}");
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), value);
            }
        }

        public void SetValue(float? eqValue)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                string value = string.Concat("{eq:", eqValue.HasValue ? JSON.Serialize(eqValue.Value) : "undefined", "}");
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), value);
            }
        }
    }
}
