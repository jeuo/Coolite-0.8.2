/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.UI;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class DateFilter : GridFilter
    {
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override FilterType Type
        {
            get 
            { 
                return FilterType.Date;
            }
        }

        [Category("Config Options")]
        [DefaultValue("d")]
        [Description("")]
        public virtual string Format
        {
            get
            {
                return (string)this.ViewState["Format"] ?? "d";
            }
            set
            {
                this.ViewState["Format"] = value;
            }
        }

        [ClientConfig("dateFormat")]
        [DefaultValue("")]
        protected virtual string FormatProxy
        {
            get
            {
                if(this.DatePickerOptions.Format != "d")
                {
                    return DateTimeUtils.ConvertNetToPHP(this.DatePickerOptions.Format);
                }
                return DateTimeUtils.ConvertNetToPHP(this.Format);
            }
        }
        /// <summary>
        /// The minimum allowed date.
        /// </summary>
        [ClientConfig(typeof(CtorDateTimeJsonConverter))]
        [Category("Config Options")]
        [Description("The minimum allowed date.")]
        public virtual DateTime MinDate
        {
            get
            {
                object obj = this.ViewState["MinDate"];
                return (obj == null) ? DateTime.MinValue : (DateTime)obj;

            }
            set
            {
                this.ViewState["MinDate"] = value.Date;
            }
        }

        /// <summary>
        /// The maximum allowed date.
        /// </summary>
        [ClientConfig(typeof(CtorDateTimeJsonConverter))]
        [Category("Config Options")]
        [Description("The maximum allowed date.")]
        public virtual DateTime MaxDate
        {
            get
            {
                object obj = this.ViewState["MaxDate"];
                return (obj == null) ? DateTime.MinValue : (DateTime)obj;

            }
            set
            {
                this.ViewState["MaxDate"] = value.Date;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Before")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'Before' menu item")]
        public string BeforeText
        {
            get
            {
                object obj = this.ViewState["BeforeText"];
                return obj == null ? "Before" : (string)obj;
            }
            set
            {
                this.ViewState["BeforeText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("After")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'After' menu item")]
        public string AfterText
        {
            get
            {
                object obj = this.ViewState["AfterText"];
                return obj == null ? "After" : (string)obj;
            }
            set
            {
                this.ViewState["AfterText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("On")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'On' menu item")]
        public string OnText
        {
            get
            {
                object obj = this.ViewState["OnText"];
                return obj == null ? "On" : (string)obj;
            }
            set
            {
                this.ViewState["OnText"] = value;
            }
        }

        private DatePickerOptions pickerOptions;

        [ClientConfig("pickerOpts", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public DatePickerOptions DatePickerOptions
        {
            get
            {
                if(pickerOptions == null)
                {
                    pickerOptions = new DatePickerOptions();
                }

                return pickerOptions;
            }
        }

        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Predefined filter value")]
        public virtual DateTime? BeforeValue
        {
            get
            {
                object obj = this.ViewState["BeforeValue"];
                return (obj == null) ? null : (DateTime?)obj;
            }
            set
            {
                this.ViewState["BeforeValue"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Predefined filter value")]
        public virtual DateTime? AfterValue
        {
            get
            {
                object obj = this.ViewState["AfterValue"];
                return (obj == null) ? null : (DateTime?)obj;
            }
            set
            {
                this.ViewState["AfterValue"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Predefined filter value")]
        public virtual DateTime? OnValue
        {
            get
            {
                object obj = this.ViewState["OnValue"];
                return (obj == null) ? null : (DateTime?)obj;
            }
            set
            {
                this.ViewState["OnValue"] = value;
            }
        }

        public void SetValue(DateTime? afterValue, DateTime? beforeValue)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                string value = string.Concat("{before:", beforeValue.HasValue ? DateTimeUtils.DateNetToJs(beforeValue.Value) : "undefined",
                    ",after:", afterValue.HasValue ? DateTimeUtils.DateNetToJs(afterValue.Value) : "undefined", "}");
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), value);
            }
        }

        public void SetValue(DateTime? onValue)
        {
            Ext.EnsureAjaxEvent();
            if (this.ParentGrid != null)
            {
                string value = string.Concat("{on:", onValue.HasValue ? DateTimeUtils.DateNetToJs(onValue.Value) : "undefined", "}");
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setValue({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), value);
            }
        }
       
        [ClientConfig("value", JsonMode.Raw)]
        [DefaultValue("")]
        internal string ValueProxy
        {
            get
            {
                if(this.BeforeValue.HasValue || this.AfterValue.HasValue || this.OnValue.HasValue)
                {
                    StringWriter sw = new StringWriter(new StringBuilder());
                    JsonTextWriter jw = new JsonTextWriter(sw);
                    jw.WriteStartObject();
                    if(this.BeforeValue.HasValue)
                    {
                        jw.WritePropertyName("before");
                        jw.WriteRaw(DateTimeUtils.DateNetToJs(this.BeforeValue.Value));
                    }

                    if (this.AfterValue.HasValue)
                    {
                        jw.WritePropertyName("after");
                        jw.WriteRaw(DateTimeUtils.DateNetToJs(this.AfterValue.Value));
                    }

                    if (this.OnValue.HasValue)
                    {
                        jw.WritePropertyName("on");
                        jw.WriteRaw(DateTimeUtils.DateNetToJs(this.OnValue.Value));
                    }
                    jw.WriteEndObject();

                    return sw.GetStringBuilder().ToString();
                }

                return "";
            }
        }
    }
}
