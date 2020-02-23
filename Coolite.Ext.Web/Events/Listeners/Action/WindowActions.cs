/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public abstract class WindowAction : ListenerMethod
    {
        protected override Type[] AllowedTypes
        {
            get
            {
                return new Type[] { typeof(Window) };
            }
        }
    }

    public class WindowClose : WindowAction
    {
        protected override string Name
        {
            get
            {
                return "close";
            }
        }
    }

    public class WindowShow : WindowAction
    {
        protected override string Name
        {
            get 
            {
                return "show";
            }
        }

        [DefaultValue("undefined")]
        [NotifyParentProperty(true)]
        [ActionMethodArgument(0)]
        public string AnimateTarget
        {
            get
            {
                return (string)this.ViewState["AnimateTarget"] ?? "undefined";
            }
            set
            {
                this.ViewState["AnimateTarget"] = value;
            }
        }

        [DefaultValue("undefined")]
        [NotifyParentProperty(true)]
        [ActionMethodArgument(1)]
        public string Callback
        {
            get
            {
                return (string)this.ViewState["Callback"] ?? "undefined";
            }
            set
            {
                this.ViewState["Callback"] = value;
            }
        }

        [DefaultValue("undefined")]
        [NotifyParentProperty(true)]
        [ActionMethodArgument(2)]
        public string Scope
        {
            get
            {
                return (string)this.ViewState["Scope"] ?? "undefined";
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }
    }

    public class WindowHide : WindowShow
    {
        protected override string Name
        {
            get
            {
                return "hide";
            }
        }
    }

    public class WindowCollapse : WindowAction
    {
        protected override string Name
        {
            get
            {
                return "collapse";
            }
        }

        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [ActionMethodArgument(0)]
        public bool Animate
        {
            get
            {
                object o = this.ViewState["Animate"];
                return o == null ? true : (bool)o;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }
    }

    public class WindowExpand : WindowCollapse
    {
        protected override string Name
        {
            get
            {
                return "expand";
            }
        }
    }
}
