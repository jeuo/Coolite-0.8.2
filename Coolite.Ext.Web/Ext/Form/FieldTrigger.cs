/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class FieldTrigger : StateManagedItem
    {
        /// <summary>
        /// True to hide the trigger element and display only the base text field (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the trigger element and display only the base text field (defaults to false).")]
        public virtual bool HideTrigger
        {
            get
            {
                object obj = this.ViewState["HideTrigger"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideTrigger"] = value;
            }
        }

        /// <summary>
        /// A CSS class to apply to the trigger.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class to apply to the trigger.")]
        public virtual string TriggerCls
        {
            get
            {
                return (string)this.ViewState["TriggerCls"] ?? "";
            }
            set
            {
                this.ViewState["TriggerCls"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(TemplateTriggerIcon.Ellipsis)]
        [Description("The icon to use in the trigger. See also, IconCls to set an icon with a custom Css class.")]
        public virtual TemplateTriggerIcon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? TemplateTriggerIcon.Ellipsis : (TemplateTriggerIcon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        /// <summary>
        /// A css class which sets a background image to be used as the icon for this button.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class which sets a background image to be used as the icon for this button.")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != TemplateTriggerIcon.Ellipsis)
                {
                    return string.Concat("x-form-", this.Icon.ToString().ToLower(), "-trigger");
                }
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
        [Description("Quick tip.")]
        [NotifyParentProperty(true)]
        public virtual string Qtip
        {
            get
            {
                return (string)this.ViewState["Qtip"] ?? "";
            }
            set
            {
                this.ViewState["Qtip"] = value;
            }
        }
    }

    public enum TemplateTriggerIcon
    {
        Empty,
        Ellipsis,
        Date,
        Clear,
        Search,
        Combo
    }
}