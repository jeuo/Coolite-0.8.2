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
    public abstract class BaseMenuItem : Component
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The CSS class to use when the item becomes activated (defaults to \"x-menu-item-active\").")]
        public virtual string ActiveClass
        {
            get
            {
                return (string)this.ViewState["ActiveClass"] ?? "";
            }
            set
            {
                this.ViewState["ActiveClass"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True if this item can be visually activated (defaults to false).")]
        public virtual bool CanActivate
        {
            get
            {
                object obj = this.ViewState["CanActivate"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CanActivate"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A function that will handle the click event of this menu item (defaults to undefined).")]
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

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(100)]
        [Description("Length of time in milliseconds to wait before hiding after a click (defaults to 100).")]
        [NotifyParentProperty(true)]
        public virtual int HideDelay
        {
            get
            {
                object obj = this.ViewState["HideDelay"];
                return (obj == null) ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["HideDelay"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to hide the containing menu after this item is clicked (defaults to true).")]
        [NotifyParentProperty(true)]
        public virtual bool HideOnClick
        {
            get
            {
                object obj = this.ViewState["HideOnClick"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HideOnClick"] = value;
            }
        }
    }
}