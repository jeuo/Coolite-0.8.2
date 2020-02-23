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

namespace Coolite.Ext.Web
{
    public class BaseListener : StateManagedItem
    {
        /// <summary>
        /// The scope in which to execute the handler function. The handler function's 'this' context.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("this")]
        [NotifyParentProperty(true)]
        [Description("The scope in which to execute the handler function. The handler function's 'this' context.")]
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

        /// <summary>
        /// The number of milliseconds to delay the invocation of the handler after the event fires.
        /// </summary>
        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("The number of milliseconds to delay the invocation of the handler after the event fires.")]
        public virtual int Delay
        {
            get
            {
                object obj = this.ViewState["Delay"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Delay"] = value;
            }
        }

        /// <summary>
        /// True to add a handler to handle just the next firing of the event, and then remove itself.
        /// </summary>
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to add a handler to handle just the next firing of the event, and then remove itself.")]
        public virtual bool Single
        {
            get
            {
                object obj = this.ViewState["Single"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Single"] = value;
            }
        }

        /// <summary>
        /// Causes the handler to be scheduled to run in an Ext.util.DelayedTask delayed by the specified number of milliseconds. If the event fires again within that time, the original handler is not invoked, but the new handler is scheduled in its place.
        /// </summary>
        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("Causes the handler to be scheduled to run in an Ext.util.DelayedTask delayed by the specified number of milliseconds. If the event fires again within that time, the original handler is not invoked, but the new handler is scheduled in its place.")]
        public virtual int Buffer
        {
            get
            {
                object obj = this.ViewState["Buffer"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Buffer"] = value;
            }
        }

        public HandlerConfig GetListenerConfig()
        {
            HandlerConfig cfg = new HandlerConfig();
            cfg.Scope = this.Scope;
            cfg.Buffer = this.Buffer;
            cfg.Delay = this.Delay;
            cfg.Single = this.Single;

            return cfg;
        }

        public class ListenerArgumentAttributeComparer : IComparer<ListenerArgumentAttribute>
        {
            public int Compare(ListenerArgumentAttribute obj1, ListenerArgumentAttribute obj2)
            {
                return obj1.Index.CompareTo(obj2.Index);
            }
        }

        internal void SetArgumentList(PropertyInfo property)
        {
            List<ListenerArgumentAttribute> attrs = new List<ListenerArgumentAttribute>();

            foreach (ListenerArgumentAttribute a in property.GetCustomAttributes(typeof(ListenerArgumentAttribute), false))
            {
                attrs.Add(a);
            }

            attrs.Sort(new ListenerArgumentAttributeComparer());

            List<string> args = new List<string>();

            foreach (ListenerArgumentAttribute a in attrs)
            {
                args.Add(a.Name);
            }

            this.argumentList = args;
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