/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using Coolite.Utilities;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [DefaultProperty("Handler")]
    [TypeConverter(typeof(RendererConverter))]
    [ToolboxItem(false)]
    public class Renderer : StateManagedItem
    {
        public Renderer() { }

        public Renderer(string handler) 
        {
            this.Handler = handler;
        }

        public virtual string ToConfigString()
        {
            if (this.Format != RendererFormat.None)
            {
                if (this.FormatArgs != null && this.FormatArgs.Length > 0)
                {
                    return new JFunction(
                        string.Concat(
                            "return Ext.util.Format.",
                            StringUtils.ToLowerCamelCase(this.Format.ToString()),
                            "(value,",
                            StringUtils.Concat(this.FormatArgs),
                            ");"), "value").ToString();
                }

                if (string.IsNullOrEmpty(this.Handler))
                {
                    return string.Concat("Ext.util.Format.", StringUtils.ToLowerCamelCase(this.Format.ToString()));
                }
            }

            if (!string.IsNullOrEmpty(this.Handler))
            {
                string temp = TokenUtils.ParseTokens(this.Handler, this.Owner);

                if (TokenUtils.IsRawToken(temp))
                {
                    return TokenUtils.ReplaceRawToken(temp);
                }

                return new JFunction(
                        temp, 
                        new string[] { "value", "metadata", "record", "rowIndex", "colIndex", "store" })
                        .ToString();
            }

            return TokenUtils.ReplaceRawToken(TokenUtils.ParseTokens(this.Fn));
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The raw JavaScript function to be called when this Renderer fires.")]
        public virtual string Fn
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

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The JavaScript logic to be called when this Renderer fires. The Handler will be automatically wrapped in a proper function{} template and passed the correct arguments for this event.")]
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

        [DefaultValue(RendererFormat.None)]
        [NotifyParentProperty(true)]
        [Description("A simple helper type to format the value. For custom renderer formating please use .Fn or .Handler.")]
        public virtual RendererFormat Format
        {
            get
            {
                object obj = this.ViewState["Format"];
                return (obj == null) ? RendererFormat.None : (RendererFormat)obj;
            }
            set
            {
                this.ViewState["Format"] = value;
            }
        }

        [TypeConverter(typeof(StringArrayConverter))]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        [Description("Custom arguments passed to Format. Required if Format is Date, DateRenderer, DefaultValue, Ellipsis or Substr.")]
        public virtual string[] FormatArgs
        {
            get
            {
                object obj = this.ViewState["FormatArgs"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["FormatArgs"] = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Are all the properties still set with thier default values.")]
        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.Fn) && string.IsNullOrEmpty(this.Handler) && this.Format == RendererFormat.None;
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

        //public static string ReplaceIDTokens(string script, ControlCollection controls)
        //{
        //    if (controls != null)
        //    {
        //        Regex regex = new Regex(@"{[\w]+}");
        //        MatchCollection matches = regex.Matches(script);
        //        foreach (Match match in matches)
        //        {
        //            Control control = ControlUtils.FindControl(controls, StringUtils.Chop(match.Value));
        //            if (control != null)
        //            {
        //                script = script.Replace(match.Value, control.ClientID);
        //            }
        //        }
        //    }
        //    return script;
        //}

        //public static bool IsIDToken(string value)
        //{
        //    return (new Regex(@"^{+[\w]+}$").Matches(value).Count == 1);
        //    //return (new Regex(@"^({)([\w}]+)(})").Matches(value).Count == 1);
        //}
    }
}