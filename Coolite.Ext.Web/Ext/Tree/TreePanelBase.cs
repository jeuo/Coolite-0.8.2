/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class TreePanelBase : PanelBase
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("true to enable animated expand/collapse (defaults to the value of Ext.enableFx)")]
        [NotifyParentProperty(true)]
        public virtual bool Animate
        {
            get
            {
                object obj = this.ViewState["Animate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to register this container with ScrollManager")]
        [NotifyParentProperty(true)]
        public virtual bool ContainerScroll
        {
            get
            {
                object obj = this.ViewState["ContainerScroll"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ContainerScroll"] = value;
            }
        }

        [ClientConfig("ddAppendOnly")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if the tree should only allow append drops (use for trees which are sorted)")]
        [NotifyParentProperty(true)]
        public virtual bool DDAppendOnly
        {
            get
            {
                object obj = this.ViewState["DDAppendOnly"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DDAppendOnly"] = value;
            }
        }

        [ClientConfig("ddGroup")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The DD group this TreePanel belongs to")]
        [NotifyParentProperty(true)]
        public virtual string DDGroup
        {
            get
            {
                return (string)this.ViewState["DDGroup"] ?? "";
            }
            set
            {
                this.ViewState["DDGroup"] = value;
            }
        }

        [ClientConfig("ddScroll")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable body scrolling")]
        [NotifyParentProperty(true)]
        public virtual bool DDScroll
        {
            get
            {
                object obj = this.ViewState["DDScroll"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DDScroll"] = value;
            }
        }

        //TODO: DragConfig
        //TODO: DropConfig

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable drag and drop")]
        [NotifyParentProperty(true)]
        public virtual bool EnableDD
        {
            get
            {
                object obj = this.ViewState["EnableDD"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableDD"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable just drag")]
        [NotifyParentProperty(true)]
        public virtual bool EnableDrag
        {
            get
            {
                object obj = this.ViewState["EnableDrag"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableDrag"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable just drop")]
        [NotifyParentProperty(true)]
        public virtual bool EnableDrop
        {
            get
            {
                object obj = this.ViewState["EnableDrop"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableDrop"] = value;
            }
        }

        [ClientConfig("hlColor")]
        [Category("Config Options")]
        [DefaultValue("C3DAF9")]
        [Description("The color of the node highlight (defaults to C3DAF9)")]
        [NotifyParentProperty(true)]
        public virtual string HighlightColor
        {
            get
            {
                return (string)this.ViewState["HighlightColor"] ?? "C3DAF9";
            }
            set
            {
                this.ViewState["HighlightColor"] = value;
            }
        }

        [ClientConfig("hlDrop")]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable node highlight on drop (defaults to the value of Ext.enableFx)")]
        [NotifyParentProperty(true)]
        public virtual bool HighlightDrop
        {
            get
            {
                object obj = this.ViewState["HighlightDrop"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HighlightDrop"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable tree lines (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool Lines
        {
            get
            {
                object obj = this.ViewState["Lines"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Lines"] = value;
            }
        }

        /// TODO: Loader

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("/")]
        [Description("The token used to separate sub-paths in path strings (defaults to '/')")]
        [NotifyParentProperty(true)]
        public virtual string PathSeparator
        {
            get
            {
                return (string)this.ViewState["PathSeparator"] ?? "/";
            }
            set
            {
                this.ViewState["PathSeparator"] = value;
            }
        }

        private TreeNodeCollection root;

        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The root node for the tree.")]
        public virtual TreeNodeCollection Root
        {
            get
            {
                if (this.root == null)
                {
                    this.root = new TreeNodeCollection();
                    this.root.Owner = this;
                }

                return this.root;
            }
        }

        private TreeLoaderCollection treeLoader;

        [ClientConfig("loader>Primary")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The root node for the tree.")]
        public virtual TreeLoaderCollection Loader
        {
            get
            {
                if (this.treeLoader == null)
                {
                    this.treeLoader = new TreeLoaderCollection();
                }

                return this.treeLoader;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("false to hide the root node (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool RootVisible
        {
            get
            {
                object obj = this.ViewState["RootVisible"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["RootVisible"] = value;
            }
        }

        //TODO: SelModel

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("true if only 1 node per branch may be expanded")]
        [NotifyParentProperty(true)]
        public virtual bool SingleExpand
        {
            get
            {
                object obj = this.ViewState["SingleExpand"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SingleExpand"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable mouse over highlighting")]
        [NotifyParentProperty(true)]
        public virtual bool TrackMouseOver
        {
            get
            {
                object obj = this.ViewState["TrackMouseOver"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["TrackMouseOver"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to use Vista-style arrows in the tree (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool UseArrows
        {
            get
            {
                object obj = this.ViewState["UseArrows"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["UseArrows"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("")]
        internal string Nodes
        {
            get
            {
                if (this.Root.Count > 0)
                {
                    Icon icon = this.Root.Primary.Icon;

                    if (icon != Icon.None)
                    {
                        this.ScriptManager.RegisterIcon(icon);
                    }

                    if (this.Root.Primary is TreeNode)
                    {
                        TreeNode rootNode = (TreeNode) this.Root.Primary;
                        rootNode.Owner = this;

                        StringBuilder sb = new StringBuilder(256);
                        sb.Append("[");
                        this.AddChildes(rootNode, sb);
                        sb.Append("]");

                        return sb.ToString();
                    }
                    else
                    {
                        return string.Concat("[", new ClientConfig().Serialize(this.Root.Primary), "]");
                    }
                }

                return "";
            }
        }

        private void AddChildes(TreeNode parent, StringBuilder sb)
        {
            string cfg = new ClientConfig().Serialize(parent);

            if (parent.Nodes.Count > 0)
            {
                int index = cfg.LastIndexOf("}");
                sb.Append(cfg.Substring(0, index));

                sb.Append(",children:[");
                foreach (TreeNodeBase node in parent.Nodes)
                {
                    node.Owner = this;
                    
                    if (node is TreeNode)
                    {
                        TreeNode treeNode = (TreeNode) node;
                        this.AddChildes(treeNode, sb);
                    }
                    else
                    {
                        sb.Append(new ClientConfig().Serialize(node));
                    }

                    Icon icon = node.Icon;

                    if (icon != Icon.None)
                    {
                        this.ScriptManager.RegisterIcon(icon);
                    }

                    sb.Append(",");
                }

                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("");
                sb.Append("]}");
            }
            else
            {
                sb.Append(cfg);
            }
        }
    }
}