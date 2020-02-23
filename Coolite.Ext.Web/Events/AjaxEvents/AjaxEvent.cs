/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;

namespace Coolite.Ext.Web
{
    [TypeConverter(typeof(AjaxEventConverter))]
    [ToolboxItem(false)]
    public class AjaxEvent : ComponentAjaxEvent
    {
        public AjaxEvent() { }

        //target and handler are required properties, so must be initialized always
        private AjaxEvent(string target, AjaxEventHandler handler)
        {
            this.Target = target;
            this.Event += handler;
        }

        public AjaxEvent(string target, HtmlEvent htmlEvent, AjaxEventHandler handler) : this(target, handler)
        {
            this.HtmlEvent = htmlEvent;
        }

        public AjaxEvent(string target, string eventName, AjaxEventHandler handler) : this(target, handler)
        {
            this.EventName = eventName;
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string EventID
        {
            get
            {
                return string.Concat(this.Target.GetHashCode(), "_", string.IsNullOrEmpty(this.EventName) ? "click" : this.EventName);
            }
        }

        /// <summary>
        /// The target to attach this AjaxEvent to. The target can be an ID, an ID token (#{Button1}), or a Select token (${div.box}).
        /// </summary>
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The target to attach this AjaxEvent to. The target can be an ID, an ID token (#{Button1}), or a Select token (${div.box}).")]
        public string Target
        {
            get
            {
                return (string)this.ViewState["Target"] ?? "";
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        /// <summary>
        /// The name of the server-side Event to fire during the AjaxEvent.
        /// </summary>
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The name of the server-side Event to fire during the AjaxEvent.")]
        public string EventName
        {
            get
            {
                string o = this.ViewState["EventName"] as string;

                if(string.IsNullOrEmpty(o) && HttpContext.Current != null)
                {
                    return this.HtmlEvent.ToString().ToLower();
                }
                
                return o ?? "";
            }
            set
            {
                this.ViewState["EventName"] = value;
            }
        }

        /// <summary>
        /// The html event type to attach this AjaxEvent to. Example 'click'.
        /// </summary>
        [DefaultValue(HtmlEvent.Click)]
        [NotifyParentProperty(true)]
        [Description("The html event type to attach this AjaxEvent to. Example 'click'.")]
        public HtmlEvent HtmlEvent
        {
            get
            {
                object o = this.ViewState["HtmlEvent"];
                return o != null ? (HtmlEvent)o : HtmlEvent.Click;
            }
            set
            {
                this.ViewState["HtmlEvent"] = value;
            }
        }
    }

    public class AjaxEventCollection : StateManagedCollection<AjaxEvent> { }
}
