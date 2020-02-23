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
using System.Text;
using System.Web;
using Coolite.Utilities;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [DefaultProperty("Handler")]
    [TypeConverter(typeof(ListenerConverter))]
    [ToolboxItem(false)]
    public class ComponentListener : BaseListener, IAutoPostBack
    {
        //private ListenerActionCollection actions;

        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[NotifyParentProperty(true)]
        //private ListenerActionCollection Actions
        //{
        //    get
        //    {
        //        if (this.actions == null)
        //        {
        //            this.actions = new ListenerActionCollection();
        //        }
        //        return this.actions;
        //    }
        //    set
        //    {
        //        this.actions = value;
        //    }
        //}

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True for the postback initiate.")]
        public virtual bool AutoPostBack
        {
            get
            {
                object obj = this.ViewState["AutoPostBack"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Themeable(false)]
        [Description("Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.")]
        public virtual bool CausesValidation
        {
            get
            {
                object obj = this.ViewState["CausesValidation"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Controls ValidationGroup
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [Description("Gets or Sets the Controls ValidationGroup")]
        public virtual string ValidationGroup
        {
            get
            {
                return (string)this.ViewState["ValidationGroup"] ?? "";
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }

        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [Description("PostBackEvent Argument")]
        public virtual string EventArgument
        {
            get
            {
                return (string)this.ViewState["EventArgument"] ?? "";
            }
            set
            {
                ViewState["EventArgument"] = value;
            }
        }

        protected virtual string PostBackFunction
        {
            get
            {
                string ea = this.EventArgument;

                if (HttpContext.Current == null || !(HttpContext.Current.CurrentHandler is Page))
                {
                    return "";
                }

                Page page = (Page)HttpContext.Current.CurrentHandler;

                if (this.CausesValidation)
                {
                    PostBackOptions options = new PostBackOptions(this.Owner, ea);
                    options.ValidationGroup = this.ValidationGroup;

                    options.AutoPostBack = this.AutoPostBack;
                    options.PerformValidation = this.CausesValidation;

                    return string.Concat(page.ClientScript.GetPostBackEventReference(options, false), ";");
                }
                
                return string.Concat(page.ClientScript.GetPostBackEventReference(this.Owner, ea), ";");
            }
        }

        [ClientConfig("fn", JsonMode.Raw)]
        [DefaultValue("")]
        internal virtual string FnInternal
        {
            get
            {
                string handler = this.Handler;

                if (string.IsNullOrEmpty(this.Fn) && (!string.IsNullOrEmpty(handler) || this.AutoPostBack))
                {
                    if (string.IsNullOrEmpty(handler) && this.AutoPostBack)
                    {
                        return string.Format(ScriptManager.FunctionTemplateWithParams, "", this.PostBackFunction);
                    }
                    
                    string parsedHandler = TokenUtils.ParseTokens(handler, this.Owner);

                    if(this.AutoPostBack)
                    {
                        string semi = parsedHandler.Trim().EndsWith(";") ? "" : ";";

                        parsedHandler = parsedHandler + semi + this.PostBackFunction;
                    }

                    if (TokenUtils.IsRawToken(parsedHandler))
                    {
                        string temp = TokenUtils.ReplaceRawToken(parsedHandler);
                        if (!temp.StartsWith("Ext."))
                        {
                            return temp;
                        }
                    }

                    return string.Format(ScriptManager.FunctionTemplateWithParams, StringUtils.Concat(this.ArgumentList), TokenUtils.ReplaceRawToken(parsedHandler));
                }

                return (string.IsNullOrEmpty(this.Fn)) ? "" : TokenUtils.ReplaceRawToken(TokenUtils.ParseTokens(this.Fn, this.Owner));
            }
        }

        /// <summary>
        /// The handler function the event invokes. This function is passed the following parameters:
        ///     evt : EventObject
        ///         The EventObject describing the event.
        ///     t : Element
        ///         The Element which was the target of the event. Note that this may be filtered by using the delegate option.
        ///     o : Object
        ///         The options object from the addListener call.
        /// </summary>
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The raw JavaScript function to be called when this Listener fires.")]
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
        [Description("The JavaScript logic to be called when this Listener fires. The Handler will be automatically wrapped in a proper function(){} template and passed the correct arguments for this event.")]
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

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.FnInternal);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Are all the properties still set with their default values, except .Fn or .Handler.")]
        public virtual bool IsAlmostDefault
        {
            get
            {
                return (
                    (!string.IsNullOrEmpty(this.Fn) || !string.IsNullOrEmpty(this.Handler) || this.AutoPostBack)
                    && string.IsNullOrEmpty(this.Scope)
                    //&& string.IsNullOrEmpty(this.Delegate)
                    //&& !this.StopEvent
                    //&& !this.PreventDefault
                    //&& !this.StopPropagation
                    //&& !this.Normalized
                    && this.Delay == 0
                    && !this.Single
                    //&& this.Actions.Count == 0
                    && this.Buffer == 0);
            }
        }

        public virtual void Clear()
        {
            this.Fn = this.Handler = this.Scope = "";
            this.Single = false;
            this.Delay = this.Buffer = 0;
            //this.Actions.Clear();
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