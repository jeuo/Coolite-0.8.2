/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    public class HandlerConfig
    {
        public virtual string ToJsonString()
        {
            if (this.Args.Count == 0)
            {
                return new ClientConfig().Serialize(this);
            }
            
            return string.Format("{{{0},{1}}}", StringUtils.Chop(new ClientConfig().Serialize(this)), StringUtils.Chop(JSON.Serialize(this.Args)));
        }

        string scope = "this";

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("this")]
        [NotifyParentProperty(true)]
        [Description("The scope in which to execute the handler function. The handler function's 'this' context.")]
        public virtual string Scope
        {
            get
            {
                return this.scope;
            }
            set
            {
                this.scope = value;
            }
        }

        string _delegate = "";

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A simple selector to filter the target or look for a descendant of the target")]
        public virtual string Delegate
        {
            get
            {
                return this._delegate;
            }
            set
            {
                this._delegate = value;
            }
        }


        bool stopEvent = false;

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to stop the event. That is stop propagation, and prevent the default action.")]
        public virtual bool StopEvent
        {
            get
            {
                return this.stopEvent;
            }
            set
            {
                this.stopEvent = value;
            }
        }

        bool preventDefault = false;

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to prevent the default action.")]
        public virtual bool PreventDefault
        {
            get
            {
                return this.preventDefault;
            }
            set
            {
                this.preventDefault = value;
            }
        }

        bool stopPropagation = false;

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to prevent event propagation.")]
        public virtual bool StopPropagation
        {
            get
            {
                return this.stopPropagation;
            }
            set
            {
                this.stopPropagation = value;
            }
        }

        bool normalized = false;

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("False to pass a browser event to the handler function instead of an Ext.EventObject.")]
        public virtual bool Normalized
        {
            get
            {
                return this.normalized;
            }
            set
            {
                this.normalized = value;
            }
        }

        int delay = 0;

        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("The number of milliseconds to delay the invocation of the handler after the event fires.")]
        public virtual int Delay
        {
            get
            {
                return this.delay;
            }
            set
            {
                this.delay = value;
            }
        }

        bool single = false;

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to add a handler to handle just the next firing of the event, and then remove itself.")]
        public virtual bool Single
        {
            get
            {
                return this.single;
            }
            set
            {
                this.single = value;
            }
        }

        int buffer = 0;

        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("Causes the handler to be scheduled to run in an Ext.util.DelayedTask delayed by the specified number of milliseconds. If the event fires again within that time, the original handler is not invoked, but the new handler is scheduled in its place.")]
        public virtual int Buffer
        {
            get
            {
                return this.buffer;
            }
            set
            {
                this.buffer = value;
            }
        }

        Dictionary<string, object> args = new Dictionary<string, object>();

        [NotifyParentProperty(true)]
        [Description("Custom arguments passed to event handler.")]
        public virtual Dictionary<string, object> Args
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
    }
}