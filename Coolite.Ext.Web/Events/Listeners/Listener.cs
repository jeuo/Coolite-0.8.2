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
    [DefaultProperty("Handler")]
    [TypeConverter(typeof(ListenerConverter))]
    [ToolboxItem(false)]
    public class Listener : ComponentListener
    {
        public Listener() { }

        public Listener(string handler)
        {
            this.Handler = handler;
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
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

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string EventName
        {
            get
            {
                string o = this.ViewState["EventName"] as string;

                if (string.IsNullOrEmpty(o) && HttpContext.Current != null)
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

        [DefaultValue(HtmlEvent.Click)]
        [NotifyParentProperty(true)]
        public HtmlEvent HtmlEvent
        {
            get
            {
                object o = this.ViewState["PredefinedEvent"];
                return o != null ? (HtmlEvent)o : HtmlEvent.Click;
            }
            set
            {
                this.ViewState["PredefinedEvent"] = value;
            }
        }
    }

    public class ListenerCollection : StateManagedCollection<Listener> { }
}