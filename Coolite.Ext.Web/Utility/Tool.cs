/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [DefaultProperty("Handler")]
    [TypeConverter(typeof(ToolConverter))]
    [ToolboxItem(false)]
    public class Tool : StateManagedItem
    {
        public Tool() { }

        public Tool(ToolType type)
        {
            this.Type = type;
        }

        public Tool(ToolType type, string handler, string qtip) 
        {
            this.Type = type;
            this.Handler = handler;
            this.Qtip = qtip;
        }

        [ClientConfig("id", JsonMode.ToLower)]
        [DefaultValue(ToolType.None)]
        [NotifyParentProperty(true)]
        [Description("The type of tool to create.")]
        public virtual ToolType Type
        {
            get
            {
                object obj = this.ViewState["Type"];
                return (obj == null) ? ToolType.None : (ToolType)obj;
            }
            set
            {
                this.ViewState["Type"] = value;
            }
        }

        [ClientConfig("id")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The custom type of tool to create.")]
        public string CustomType
        {
            get
            {
                return (string)this.ViewState["CustomType"] ?? "";
            }
            set
            {
                this.ViewState["CustomType"] = value;
            }
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The raw JavaScript function to be called when this Listener fires.")]
        public string Fn
        {
            get
            {
                return (string)this.ViewState["Fn"] ?? "";
            }
            set
            {
                this.ViewState["Fn"] = value;
            }
        }

        [ClientConfig("handler", JsonMode.Raw)]
        protected string FnProxy
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Handler))
                {
                    return new JFunction(TokenUtils.ReplaceRawToken(TokenUtils.ParseTokens(this.Handler)), "event", "toolEl", "panel").ToString();
                }
                return this.Fn;
            }
        }


        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The function to call when clicked. Arguments passed are 'event', 'toolEl' and 'panel'.")]
        public virtual string Handler
        {
            get
            {
                return (string)this.ViewState["Handler"] ?? "";
            }
            set
            {
                this.ViewState["Handler"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("this")]
        [NotifyParentProperty(true)]
        [Description("The scope in which to call the handler.")]
        public virtual string Scope
        {
            get
            {
                return (string)this.ViewState["Scope"] ?? "this";
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A tip string.")]
        public virtual string Qtip 
        {
            get
            {
                return (string)this.ViewState["Qtip"] ?? "";
            }
            set
            {
                this.ViewState["Qtip"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to initially render hidden.")]
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

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.Handler);
            }
        }

        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(CultureInfo culture)
        {
            return TypeDescriptor.GetConverter(this.GetType()).ConvertToString(null, culture, this);
        }
    }
}