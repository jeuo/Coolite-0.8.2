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
    public class TreePanelAjaxEvents : PanelAjaxEvents
    {
        private ComponentAjaxEvent append;

        /// <summary>
        /// Fires when a new child node is appended to a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree", typeof(TreePanel))]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("append", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is appended to a node in this tree.")]
        public virtual ComponentAjaxEvent Append
        {
            get
            {
                if (this.append == null)
                {
                    this.append = new ComponentAjaxEvent();
                }
                return this.append;
            }
        }

        private ComponentAjaxEvent beforeAppend;

        /// <summary>
        /// Fires before a new child is appended to a node in this tree, return false to cancel the append.
        /// </summary>
        [ListenerArgument(0, "tree", typeof(TreePanel))]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeappend", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is appended to a node in this tree, return false to cancel the append.")]
        public virtual ComponentAjaxEvent BeforeAppend
        {
            get
            {
                if (this.beforeAppend == null)
                {
                    this.beforeAppend = new ComponentAjaxEvent();
                }
                return this.beforeAppend;
            }
        }

        private ComponentAjaxEvent beforeChildRenrendered;

        /// <summary>
        /// Fires right before the child nodes for a node are rendered
        /// </summary>
        [ListenerArgument(0, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechildrenrendered", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires right before the child nodes for a node are rendered")]
        public virtual ComponentAjaxEvent BeforeChildRenrendered
        {
            get
            {
                if (this.beforeChildRenrendered == null)
                {
                    this.beforeChildRenrendered = new ComponentAjaxEvent();
                }
                return this.beforeChildRenrendered;
            }
        }

        private ComponentAjaxEvent beforeClick;

        /// <summary>
        /// Fires before click processing on a node. Return false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before click processing on a node. Return false to cancel the default action.")]
        public virtual ComponentAjaxEvent BeforeClick
        {
            get
            {
                if (this.beforeClick == null)
                {
                    this.beforeClick = new ComponentAjaxEvent();
                }
                return this.beforeClick;
            }
        }

        private ComponentAjaxEvent beforeCollapseNode;

        /// <summary>
        /// Fires before a node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapsenode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is collapsed, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeCollapseNode
        {
            get
            {
                if (this.beforeCollapseNode == null)
                {
                    this.beforeCollapseNode = new ComponentAjaxEvent();
                }
                return this.beforeCollapseNode;
            }
        }

        private ComponentAjaxEvent beforeExpandNode;

        /// <summary>
        /// Fires before a node is expanded, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpandnode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is expanded, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeExpandNode
        {
            get
            {
                if (this.beforeExpandNode == null)
                {
                    this.beforeExpandNode = new ComponentAjaxEvent();
                }
                return this.beforeExpandNode;
            }
        }

        private ComponentAjaxEvent beforeInsert;

        /// <summary>
        /// Fires before a new child is inserted in a node in this tree, return false to cancel the insert.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeinsert", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is inserted in a node in this tree, return false to cancel the insert.")]
        public virtual ComponentAjaxEvent BeforeInsert
        {
            get
            {
                if (this.beforeInsert == null)
                {
                    this.beforeInsert = new ComponentAjaxEvent();
                }
                return this.beforeInsert;
            }
        }

        private ComponentAjaxEvent beforeLoad;

        /// <summary>
        /// Fires before a node is loaded, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeload", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is loaded, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeLoad
        {
            get
            {
                if (this.beforeLoad == null)
                {
                    this.beforeLoad = new ComponentAjaxEvent();
                }
                return this.beforeLoad;
            }
        }

        private ComponentAjaxEvent beforeMoveNode;

        /// <summary>
        /// Fires before a node is moved to a new location in the tree. Return false to cancel the move.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforemovenode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is moved to a new location in the tree. Return false to cancel the move.")]
        public virtual ComponentAjaxEvent BeforeMoveNode
        {
            get
            {
                if (this.beforeMoveNode == null)
                {
                    this.beforeMoveNode = new ComponentAjaxEvent();
                }

                return this.beforeMoveNode;
            }
        }

        private ComponentAjaxEvent beforeNodeDrop;

        /// <summary>
        /// Fires when a DD object is dropped on a node in this tree for preprocessing. Return false to cancel the drop.
        /// </summary>
        [ListenerArgument(0, "dropEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforenodedrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a DD object is dropped on a node in this tree for preprocessing. Return false to cancel the drop.")]
        public virtual ComponentAjaxEvent BeforeNodeDrop
        {
            get
            {
                if (this.beforeNodeDrop == null)
                {
                    this.beforeNodeDrop = new ComponentAjaxEvent();
                }
                return this.beforeNodeDrop;
            }
        }

        private ComponentAjaxEvent beforeRemoveNode;

        /// <summary>
        /// Fires before a child is removed from a node in this tree, return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a child is removed from a node in this tree, return false to cancel the remove.")]
        public virtual ComponentAjaxEvent BeforeRemoveNode
        {
            get
            {
                if (this.beforeRemoveNode == null)
                {
                    this.beforeRemoveNode = new ComponentAjaxEvent();
                }
                return this.beforeRemoveNode;
            }
        }

        private ComponentAjaxEvent checkChange;

        /// <summary>
        /// Fires when a node with a checkbox's checked property changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node with a checkbox's checked property changes")]
        public virtual ComponentAjaxEvent CheckChange
        {
            get
            {
                if (this.checkChange == null)
                {
                    this.checkChange = new ComponentAjaxEvent();
                }
                return this.checkChange;
            }
        }

        private ComponentAjaxEvent click;

        /// <summary>
        /// Fires when a node is clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is clicked")]
        public virtual ComponentAjaxEvent Click
        {
            get
            {
                if (this.click == null)
                {
                    this.click = new ComponentAjaxEvent();
                }
                return this.click;
            }
        }

        private ComponentAjaxEvent collapseNode;

        /// <summary>
        /// Fires when a node is collapsed
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapsenode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is collapsed")]
        public virtual ComponentAjaxEvent CollapseNode
        {
            get
            {
                if (this.collapseNode == null)
                {
                    this.collapseNode = new ComponentAjaxEvent();
                }
                return this.collapseNode;
            }
        }

        private ComponentAjaxEvent contextMenu;

        /// <summary>
        /// Fires when a node is right clicked.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is right clicked.")]
        public virtual ComponentAjaxEvent ContextMenu
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentAjaxEvent();
                }
                return this.contextMenu;
            }
        }

        private ComponentAjaxEvent dblClick;

        /// <summary>
        /// Fires when a node is double clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is double clicked")]
        public virtual ComponentAjaxEvent DblClick
        {
            get
            {
                if (this.dblClick == null)
                {
                    this.dblClick = new ComponentAjaxEvent();
                }
                return this.dblClick;
            }
        }

        private ComponentAjaxEvent disabledChange;

        /// <summary>
        /// Fires when the disabled status of a node changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "disabled")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disabledchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the disabled status of a node changes")]
        public virtual ComponentAjaxEvent DisabledChange
        {
            get
            {
                if (this.disabledChange == null)
                {
                    this.disabledChange = new ComponentAjaxEvent();
                }
                return this.disabledChange;
            }
        }

        private ComponentAjaxEvent dragDrop;

        /// <summary>
        /// Fires when a dragged node is dropped on a valid DD target
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "dd")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragdrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a dragged node is dropped on a valid DD target")]
        public virtual ComponentAjaxEvent DragDrop
        {
            get
            {
                if (this.dragDrop == null)
                {
                    this.dragDrop = new ComponentAjaxEvent();
                }
                return this.dragDrop;
            }
        }

        private ComponentAjaxEvent endDrag;

        /// <summary>
        /// Fires when a drag operation is complete
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("enddrag", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a drag operation is complete")]
        public virtual ComponentAjaxEvent EndDrag
        {
            get
            {
                if (this.endDrag == null)
                {
                    this.endDrag = new ComponentAjaxEvent();
                }
                return this.endDrag;
            }
        }

        private ComponentAjaxEvent expandNode;

        /// <summary>
        /// Fires when a node is expanded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expandnode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is expanded")]
        public virtual ComponentAjaxEvent ExpandNode
        {
            get
            {
                if (this.expandNode == null)
                {
                    this.expandNode = new ComponentAjaxEvent();
                }
                return this.expandNode;
            }
        }

        private ComponentAjaxEvent insert;

        /// <summary>
        /// Fires when a new child node is inserted in a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "refNode", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("insert", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is inserted in a node in this tree.")]
        public virtual ComponentAjaxEvent Insert
        {
            get
            {
                if (this.insert == null)
                {
                    this.insert = new ComponentAjaxEvent();
                }
                return this.insert;
            }
        }

        private ComponentAjaxEvent load;

        /// <summary>
        /// Fires when a node is loaded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("load", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is loaded")]
        public virtual ComponentAjaxEvent Load
        {
            get
            {
                if (this.load == null)
                {
                    this.load = new ComponentAjaxEvent();
                }
                return this.load;
            }
        }

        private ComponentAjaxEvent moveNode;

        /// <summary>
        /// Fires when a node is moved to a new location in the tree
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("movenode", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is moved to a new location in the tree")]
        public virtual ComponentAjaxEvent MoveNode
        {
            get
            {
                if (this.moveNode == null)
                {
                    this.moveNode = new ComponentAjaxEvent();
                }
                return this.moveNode;
            }
        }

        private ComponentAjaxEvent nodeDragOver;

        /// <summary>
        /// Fires when a tree node is being targeted for a drag drop, return false to signal drop not allowed.
        /// </summary>
        [ListenerArgument(0, "dragOverEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("nodedragover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a tree node is being targeted for a drag drop, return false to signal drop not allowed.")]
        public virtual ComponentAjaxEvent NodeDragOver
        {
            get
            {
                if (this.nodeDragOver == null)
                {
                    this.nodeDragOver = new ComponentAjaxEvent();
                }
                return this.nodeDragOver;
            }
        }

        private ComponentAjaxEvent nodeDrop;

        /// <summary>
        /// Fires after a DD object is dropped on a node in this tree.
        /// </summary>
        [ListenerArgument(0, "dropEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("nodedrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a DD object is dropped on a node in this tree.")]
        public virtual ComponentAjaxEvent NodeDrop
        {
            get
            {
                if (this.nodeDrop == null)
                {
                    this.nodeDrop = new ComponentAjaxEvent();
                }
                return this.nodeDrop;
            }
        }

        private ComponentAjaxEvent removeNode;

        /// <summary>
        /// Fires when a child node is removed from a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a child node is removed from a node in this tree.")]
        public virtual ComponentAjaxEvent RemoveNode
        {
            get
            {
                if (this.removeNode == null)
                {
                    this.removeNode = new ComponentAjaxEvent();
                }
                return this.removeNode;
            }
        }

        private ComponentAjaxEvent startDrag;

        /// <summary>
        /// Fires when a node starts being dragged
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("startdrag", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node starts being dragged")]
        public virtual ComponentAjaxEvent StartDrag
        {
            get
            {
                if (this.startDrag == null)
                {
                    this.startDrag = new ComponentAjaxEvent();
                }
                return this.startDrag;
            }
        }

        private ComponentAjaxEvent textChange;

        /// <summary>
        /// Fires when the text for a node is changed
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "text")]
        [ListenerArgument(2, "oldText")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("textchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text for a node is changed")]
        public virtual ComponentAjaxEvent TextChange
        {
            get
            {
                if (this.textChange == null)
                {
                    this.textChange = new ComponentAjaxEvent();
                }
                return this.textChange;
            }
        }
    }
}