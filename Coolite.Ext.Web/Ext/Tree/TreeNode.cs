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
    [InstanceOf(ClassName = "Ext.tree.TreeNode")]
    [ToolboxItem(false)]
    public class TreeNode : TreeNodeBase
    {
        public TreeNode() { }

        public TreeNode(string text)
        {
            this.Text = text;
        }

        public TreeNode(string text, Icon icon)
        {
            this.Text = text;
            this.Icon = icon;
        }

        public TreeNode(string id, string text, Icon icon)
        {
            this.NodeID = id;
            this.Text = text;
            this.Icon = icon;
        }

        private TreeNodeCollection nodes;

        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The root node for the tree.")]
        public virtual TreeNodeCollection Nodes
        {
            get
            {
                if (this.nodes == null)
                {
                    this.nodes = new TreeNodeCollection(false);
                    this.nodes.AfterItemAdd += Nodes_AfterItemAdd;
                }

                return this.nodes;
            }
        }

        void Nodes_AfterItemAdd(TreeNodeBase item)
        {
            item.ParentNode = this;
        }
        
        [ClientConfig]
        [DefaultValue("")]
        internal string NodeType
        {
            get
            {
                return this.EnforceNodeType ? "node" : "";
            }
        }

        private TreeNodeListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public TreeNodeListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TreeNodeListeners();
                    this.listeners.InitOwners(this.Owner);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }
    }
}