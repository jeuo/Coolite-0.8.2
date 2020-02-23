/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    public class JFunction : StateManagedItem
    {
        public JFunction() { }

        public JFunction(string handler)
        {
            this.Handler = handler;
        }

        public JFunction(string handler, params string[] args) 
        {
            this.Handler = handler;
            this.Args = args;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.Fn))
            {
                return this.Fn;
            }

            if (this.Args != null && this.Args.Length > 0)
            {
                if (this.FormatHandler)
                {
                    return string.Concat(
                            "function(",
                            String.Join(",", this.Args),
                            "){",
                            string.Format(this.Handler, this.Args),
                            "}"
                        );
                }
                else
                {
                    return string.Concat("function(", String.Join(",", this.Args), "){", this.Handler, "}");
                }
            }
            else
            {
                return string.Concat("function(){", this.Handler, "}");
            }
        }

        private string fn;

        [NotifyParentProperty(true)]
        [Description("The raw JavaScript code to call.")]
        public virtual string Fn
        {
            get 
            { 
                return this.fn; 
            }
            set 
            {
                this.fn = value; 
            }
        }

        string handler = "";

        [NotifyParentProperty(true)]
        [Description("The JavaScript logic within the function(){} signature.")]
        public virtual string Handler
        {
            get
            {
                return this.handler;
            }
            set
            {
                this.handler = value;
            }
        }

        string[] args;

        [TypeConverter(typeof(StringArrayConverter))]
        [NotifyParentProperty(true)]
        [Description("Custom arguments passed to event handler if Handler property is set.")]
        public virtual string[] Args
        {
            get
            {
                return this.args;
            }
            set
            {
                this.args = value;
            }
        }

        private bool formatHandler = false;

        [NotifyParentProperty(true)]
        [Description("If FormatHander=true, then the Handler property will be passed through the string.Format() Method and argument placeholders/tokens with be replaced with the argument index value.")]
        public virtual bool FormatHandler
        {
            get
            {
                return this.formatHandler;
            }
            set
            {
                this.formatHandler = value;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.Handler) && string.IsNullOrEmpty(this.Fn);
            }
        }
    }
}