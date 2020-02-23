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
    public class StartButtonConfig : StateManagedItem
    {
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The icon to use for the start button. See also, IconCls to set an icon with a custom Css class.")]
        [NotifyParentProperty(true)]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class to be added to the start button icon element for applying css background images")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return string.Format("icon-{0}", this.Icon.ToString().ToLower());
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

    }
}