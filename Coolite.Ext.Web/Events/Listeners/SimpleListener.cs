/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class SimpleListener : StateManagedItem
    {
        public SimpleListener() { }

        public SimpleListener(string handler)
        {
            this.Handler = handler;
        }

        private string FnInternal
        {
            get
            {
                string handler = this.Handler;

                if (string.IsNullOrEmpty(this.Fn) && !string.IsNullOrEmpty(handler))
                {
                    string parsedHandler = TokenUtils.ParseTokens(handler, this.Owner);

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

        public override string ToString()
        {
            return this.FnInternal;
        }

        List<string> argumentList;

        [Description("List of Arguments passed to event handler.")]
        internal List<string> ArgumentList
        {
            get
            {
                if (this.argumentList == null)
                {
                    this.argumentList = new List<string>();
                }
                return this.argumentList;
            }
        }
    }
}