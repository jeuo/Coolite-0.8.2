/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class TreeNodeBase : Node, IIcon
    {
        private TreeNodeBase parentNode;
        public TreeNodeBase ParentNode
        {
            get { return this.parentNode; }
            internal set { this.parentNode = value; }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to not allow this node to have child nodes (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool AllowChildren
        {
            get
            {
                object obj = this.ViewState["AllowChildren"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowChildren"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to make this node undraggable if draggable = true (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool AllowDrag
        {
            get
            {
                object obj = this.ViewState["AllowDrag"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDrag"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False if this node cannot have child nodes dropped on it (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool AllowDrop
        {
            get
            {
                object obj = this.ViewState["AllowDrop"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDrop"] = value;
            }
        }

        [ClientConfig(typeof(ThreeStateBoolJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ThreeStateBool.Undefined)]
        [Description("True to render a checked checkbox for this node, false to render an unchecked checkbox (defaults to undefined with no checkbox rendered)")]
        [NotifyParentProperty(true)]
        public virtual ThreeStateBool Checked
        {
            get
            {
                object obj = this.ViewState["Checked"];
                return (obj == null) ? ThreeStateBool.Undefined : (ThreeStateBool)obj;
            }
            set
            {
                this.ViewState["Checked"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class to be added to the node.")]
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
        [Description("True to start the node disabled")]
        [NotifyParentProperty(true)]
        public virtual bool Disabled
        {
            get
            {
                object obj = this.ViewState["Disabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Disabled"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to make this node draggable (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Draggable
        {
            get
            {
                object obj = this.ViewState["Draggable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Draggable"] = value;
            }
        }

        [ClientConfig(typeof(ThreeStateBoolJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ThreeStateBool.Undefined)]
        [Description("If set to true, the node will always show a plus/minus icon, even when empty")]
        [NotifyParentProperty(true)]
        public virtual ThreeStateBool Expandable
        {
            get
            {
                object obj = this.ViewState["Expandable"];
                return (obj == null) ? ThreeStateBool.Undefined : (ThreeStateBool)obj;
            }
            set
            {
                this.ViewState["Expandable"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to start the node expanded")]
        [NotifyParentProperty(true)]
        public virtual bool Expanded
        {
            get
            {
                object obj = this.ViewState["Expanded"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Expanded"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("#")]
        [Description("URL of the link used for the node (defaults to #)")]
        [NotifyParentProperty(true)]
        public virtual string Href
        {
            get
            {
                return (string)this.ViewState["Href"] ?? "#";
            }
            set
            {
                this.ViewState["Href"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Target frame for the link")]
        [NotifyParentProperty(true)]
        public virtual string HrefTarget
        {
            get
            {
                return (string)this.ViewState["HrefTarget"] ?? "";
            }
            set
            {
                this.ViewState["HrefTarget"] = value;
            }
        }

        [ClientConfig("icon")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The path to an icon for the node. The preferred way to do this is to use the cls or iconCls attributes and add the icon via a CSS background image.")]
        [NotifyParentProperty(true)]
        public virtual string IconFile
        {
            get
            {
                return (string)this.ViewState["IconFile"] ?? "";
            }
            set
            {
                this.ViewState["IconFile"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The icon to use for the Node. See also, IconCls to set an icon with a custom Css class.")]
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

        [ClientConfig("iconCls")]
        [DefaultValue("")]
        internal virtual string IconClsProxy
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.Icon);
                }
                return this.IconCls;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class to be added to the nodes icon element for applying css background images")]
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
        [DefaultValue(true)]
        [Description("False to not allow this node to act as a drop target (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool IsTarget
        {
            get
            {
                object obj = this.ViewState["IsTarget"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["IsTarget"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An Ext QuickTip for the node")]
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

        //private ToolTip qtipConfig;

        //[ClientConfig("qtipCfg",JsonMode.Object)]
        //[Category("Config Options")]
        //[Description("An Ext QuickTip config for the node (used instead of qtip)")]
        //[NotifyParentProperty(true)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        ////[ViewStateMember]
        //public virtual ToolTip QtipConfig
        //{
        //    get
        //    {
        //        if (this.qtipConfig == null)
        //        {
        //            this.qtipConfig = new ToolTip();
        //            this.qtipConfig.Visible = false;
        //        }
        //        return this.qtipConfig;
        //    }
        //}

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True for single click expand on this node")]
        [NotifyParentProperty(true)]
        public virtual bool SingleClickExpand
        {
            get
            {
                object obj = this.ViewState["SingleClickExpand"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SingleClickExpand"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text for this node")]
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

        private ConfigItemCollection customAttributes;

        [ClientConfig("-", typeof(CustomConfigJsonConverter))]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Collection of custom node attributes")]
        public virtual ConfigItemCollection CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                {
                    this.customAttributes = new ConfigItemCollection();
                }

                return this.customAttributes;
            }
        }

        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool EnforceNodeType
        {
            get
            {
                object obj = this.ViewState["EnforceNodeType"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnforceNodeType"] = value;
            }
        }

        List<Icon> IIcon.Icons
        {
            get 
            {
                List<Icon> icons = new List<Icon>(1);
                icons.Add(this.Icon);
                return icons;
            }
        }
    }
}