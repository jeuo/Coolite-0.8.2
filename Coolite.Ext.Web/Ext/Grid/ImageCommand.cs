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
    public abstract class ImageCommandBase : StateManagedItem
    {
        [ClientConfig("command")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string CommandName
        {
            get
            {
                return (string)this.ViewState["CommandName"] ?? "";
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool Hidden
        {
            get
            {
                object obj = this.ViewState["Hidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hidden"] = value;
            }
        }

        [DefaultValue(Icon.None)]
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
        [DefaultValue("")]
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
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Style
        {
            get
            {
                return (string)this.ViewState["Style"] ?? "";
            }
            set
            {
                this.ViewState["Style"] = value;
            }
        }

        //[ClientConfig]
        //[DefaultValue("")]
        //[NotifyParentProperty(true)]
        //public string Tooltip
        //{
        //    get
        //    {
        //        return (string)this.ViewState["Tooltip"] ?? "";
        //    }
        //    set
        //    {
        //        this.ViewState["Tooltip"] = value;
        //    }
        //}

        private SimpleToolTip toolTip;

        [ClientConfig("tooltip", JsonMode.Object)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public SimpleToolTip ToolTip
        {
            get
            {
                if (this.toolTip == null)
                {
                    this.toolTip = new SimpleToolTip();
                }

                return this.toolTip;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(HideMode.Display)]
        [NotifyParentProperty(true)]
        [Description("How this component should be hidden. Supported values are 'visibility' (css visibility), 'offsets' (negative offset position) and 'display' (css display) - defaults to 'display'.")]
        public virtual HideMode HideMode
        {
            get
            {
                object obj = this.ViewState["HideMode"];
                return (obj == null) ? HideMode.Display : (HideMode)obj;
            }
            set
            {
                this.ViewState["HideMode"] = value;
            }
        }
    }

    public class ImageCommand : ImageCommandBase
    {
    }

    public class ImageCommandCollection : StateManagedCollection<ImageCommand>
    {
    }

    public class GroupImageCommand : ImageCommandBase
    {
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool RightAlign
        {
            get
            {
                object obj = this.ViewState["RightAlign"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["RightAlign"] = value;
            }
        }
    }

    public class GroupImageCommandCollection : StateManagedCollection<GroupImageCommand>
    {
    }
}