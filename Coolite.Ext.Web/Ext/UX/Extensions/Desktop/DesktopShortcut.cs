/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public class DesktopShortcut : StateManagedItem
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string ModuleID
        {
            get
            {
                return (string)this.ViewState["ModuleID"] ?? "";
            }
            set
            {
                this.ViewState["ModuleID"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string ShortcutID
        {
            get
            {
                return (string)this.ViewState["ShortcutID"] ?? "";
            }
            set
            {
                this.ViewState["ShortcutID"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string X
        {
            get
            {
                return (string)this.ViewState["X"] ?? "";
            }
            set
            {
                this.ViewState["X"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Y
        {
            get
            {
                return (string)this.ViewState["Y"] ?? "";
            }
            set
            {
                this.ViewState["Y"] = value;
            }
        }
    }

    public class DesktopShortcuts : StateManagedCollection<DesktopShortcut> { }
}