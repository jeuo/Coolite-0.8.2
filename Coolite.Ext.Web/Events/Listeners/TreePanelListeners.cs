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
    public class TreePanelListeners : PanelListeners
    {
        private ComponentListener append;

        /// <summary>
        /// Fires when a new child node is appended to a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree", typeof(TreePanel))]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("append", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is appended to a node in this tree.")]
        public virtual ComponentListener Append
        {
            get
            {
                if (this.append == null)
                {
                    this.append = new ComponentListener();
                }
                return this.append;
            }
        }

        private ComponentListener beforeAppend;

        /// <summary>
        /// Fires before a new child is appended to a node in this tree, return false to cancel the append.
        /// </summary>
        [ListenerArgument(0, "tree", typeof(TreePanel))]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeappend", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is appended to a node in this tree, return false to cancel the append.")]
        public virtual ComponentListener BeforeAppend
        {
            get
            {
                if (this.beforeAppend == null)
                {
                    this.beforeAppend = new ComponentListener();
                }
                return this.beforeAppend;
            }
        }

        private ComponentListener beforeChildRenrendered;

        /// <summary>
        /// Fires right before the child nodes for a node are rendered
        /// </summary>
        [ListenerArgument(0, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechildrenrendered", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires right before the child nodes for a node are rendered")]
        public virtual ComponentListener BeforeChildRenrendered
        {
            get
            {
                if (this.beforeChildRenrendered == null)
                {
                    this.beforeChildRenrendered = new ComponentListener();
                }
                return this.beforeChildRenrendered;
            }
        }

        private ComponentListener beforeClick;

        /// <summary>
        /// Fires before click processing on a node. Return false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before click processing on a node. Return false to cancel the default action.")]
        public virtual ComponentListener BeforeClick
        {
            get
            {
                if (this.beforeClick == null)
                {
                    this.beforeClick = new ComponentListener();
                }
                return this.beforeClick;
            }
        }

        private ComponentListener beforeCollapseNode;

        /// <summary>
        /// Fires before a node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapsenode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is collapsed, return false to cancel.")]
        public virtual ComponentListener BeforeCollapseNode
        {
            get
            {
                if (this.beforeCollapseNode == null)
                {
                    this.beforeCollapseNode = new ComponentListener();
                }
                return this.beforeCollapseNode;
            }
        }

        private ComponentListener beforeExpandNode;

        /// <summary>
        /// Fires before a node is expanded, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpandnode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is expanded, return false to cancel.")]
        public virtual ComponentListener BeforeExpandNode
        {
            get
            {
                if (this.beforeExpandNode == null)
                {
                    this.beforeExpandNode = new ComponentListener();
                }
                return this.beforeExpandNode;
            }
        }

        private ComponentListener beforeInsert;

        /// <summary>
        /// Fires before a new child is inserted in a node in this tree, return false to cancel the insert.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeinsert", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is inserted in a node in this tree, return false to cancel the insert.")]
        public virtual ComponentListener BeforeInsert
        {
            get
            {
                if (this.beforeInsert == null)
                {
                    this.beforeInsert = new ComponentListener();
                }
                return this.beforeInsert;
            }
        }

        private ComponentListener beforeLoad;

        /// <summary>
        /// Fires before a node is loaded, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeload", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is loaded, return false to cancel.")]
        public virtual ComponentListener BeforeLoad
        {
            get
            {
                if (this.beforeLoad == null)
                {
                    this.beforeLoad = new ComponentListener();
                }
                return this.beforeLoad;
            }
        }

        private ComponentListener beforeMoveNode;

        /// <summary>
        /// Fires before a node is moved to a new location in the tree. Return false to cancel the move.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforemovenode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a node is moved to a new location in the tree. Return false to cancel the move.")]
        public virtual ComponentListener BeforeMoveNode
        {
            get
            {
                if (this.beforeMoveNode == null)
                {
                    this.beforeMoveNode = new ComponentListener();
                }
                return this.beforeMoveNode;
            }
        }

        private ComponentListener beforeNodeDrop;

        /// <summary>
        /// Fires when a DD object is dropped on a node in this tree for preprocessing. Return false to cancel the drop.
        /// </summary>
        [ListenerArgument(0, "dropEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforenodedrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a DD object is dropped on a node in this tree for preprocessing. Return false to cancel the drop.")]
        public virtual ComponentListener BeforeNodeDrop
        {
            get
            {
                if (this.beforeNodeDrop == null)
                {
                    this.beforeNodeDrop = new ComponentListener();
                }
                return this.beforeNodeDrop;
            }
        }

        private ComponentListener beforeRemoveNode;

        /// <summary>
        /// Fires before a child is removed from a node in this tree, return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a child is removed from a node in this tree, return false to cancel the remove.")]
        public virtual ComponentListener BeforeRemoveNode
        {
            get
            {
                if (this.beforeRemoveNode == null)
                {
                    this.beforeRemoveNode = new ComponentListener();
                }
                return this.beforeRemoveNode;
            }
        }

        private ComponentListener checkChange;

        /// <summary>
        /// Fires when a node with a checkbox's checked property changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node with a checkbox's checked property changes")]
        public virtual ComponentListener CheckChange
        {
            get
            {
                if (this.checkChange == null)
                {
                    this.checkChange = new ComponentListener();
                }
                return this.checkChange;
            }
        }

        private ComponentListener click;

        /// <summary>
        /// Fires when a node is clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is clicked")]
        public virtual ComponentListener Click
        {
            get
            {
                if (this.click == null)
                {
                    this.click = new ComponentListener();
                }
                return this.click;
            }
        }

        private ComponentListener collapseNode;

        /// <summary>
        /// Fires when a node is collapsed
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapsenode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is collapsed")]
        public virtual ComponentListener CollapseNode
        {
            get
            {
                if (this.collapseNode == null)
                {
                    this.collapseNode = new ComponentListener();
                }
                return this.collapseNode;
            }
        }

        private ComponentListener contextMenu;

        /// <summary>
        /// Fires when a node is right clicked.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is right clicked.")]
        public virtual ComponentListener ContextMenu
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentListener();
                }
                return this.contextMenu;
            }
        }

        private ComponentListener dblClick;

        /// <summary>
        /// Fires when a node is double clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is double clicked")]
        public virtual ComponentListener DblClick
        {
            get
            {
                if (this.dblClick == null)
                {
                    this.dblClick = new ComponentListener();
                }
                return this.dblClick;
            }
        }

        private ComponentListener disabledChange;

        /// <summary>
        /// Fires when the disabled status of a node changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "disabled")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disabledchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the disabled status of a node changes")]
        public virtual ComponentListener DisabledChange
        {
            get
            {
                if (this.disabledChange == null)
                {
                    this.disabledChange = new ComponentListener();
                }
                return this.disabledChange;
            }
        }

        private ComponentListener dragDrop;

        /// <summary>
        /// Fires when a dragged node is dropped on a valid DD target
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "dd")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragdrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a dragged node is dropped on a valid DD target")]
        public virtual ComponentListener DragDrop
        {
            get
            {
                if (this.dragDrop == null)
                {
                    this.dragDrop = new ComponentListener();
                }
                return this.dragDrop;
            }
        }

        private ComponentListener endDrag;

        /// <summary>
        /// Fires when a drag operation is complete
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("enddrag", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a drag operation is complete")]
        public virtual ComponentListener EndDrag
        {
            get
            {
                if (this.endDrag == null)
                {
                    this.endDrag = new ComponentListener();
                }
                return this.endDrag;
            }
        }

        private ComponentListener expandNode;

        /// <summary>
        /// Fires when a node is expanded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expandnode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is expanded")]
        public virtual ComponentListener ExpandNode
        {
            get
            {
                if (this.expandNode == null)
                {
                    this.expandNode = new ComponentListener();
                }
                return this.expandNode;
            }
        }

        private ComponentListener insert;

        /// <summary>
        /// Fires when a new child node is inserted in a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [ListenerArgument(3, "refNode", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("insert", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is inserted in a node in this tree.")]
        public virtual ComponentListener Insert
        {
            get
            {
                if (this.insert == null)
                {
                    this.insert = new ComponentListener();
                }
                return this.insert;
            }
        }

        private ComponentListener load;

        /// <summary>
        /// Fires when a node is loaded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("load", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is loaded")]
        public virtual ComponentListener Load
        {
            get
            {
                if (this.load == null)
                {
                    this.load = new ComponentListener();
                }
                return this.load;
            }
        }

        private ComponentListener moveNode;

        /// <summary>
        /// Fires when a node is moved to a new location in the tree
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("movenode", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node is moved to a new location in the tree")]
        public virtual ComponentListener MoveNode
        {
            get
            {
                if (this.moveNode == null)
                {
                    this.moveNode = new ComponentListener();
                }
                return this.moveNode;
            }
        }

        private ComponentListener nodeDragOver;

        /// <summary>
        /// Fires when a tree node is being targeted for a drag drop, return false to signal drop not allowed.
        /// </summary>
        [ListenerArgument(0, "dragOverEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("nodedragover", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a tree node is being targeted for a drag drop, return false to signal drop not allowed.")]
        public virtual ComponentListener NodeDragOver
        {
            get
            {
                if (this.nodeDragOver == null)
                {
                    this.nodeDragOver = new ComponentListener();
                }
                return this.nodeDragOver;
            }
        }

        private ComponentListener nodeDrop;

        /// <summary>
        /// Fires after a DD object is dropped on a node in this tree.
        /// </summary>
        [ListenerArgument(0, "dropEvent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("nodedrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a DD object is dropped on a node in this tree.")]
        public virtual ComponentListener NodeDrop
        {
            get
            {
                if (this.nodeDrop == null)
                {
                    this.nodeDrop = new ComponentListener();
                }
                return this.nodeDrop;
            }
        }


        private ComponentListener removeNode;

        /// <summary>
        /// Fires when a child node is removed from a node in this tree.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "parent", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a child node is removed from a node in this tree.")]
        public virtual ComponentListener RemoveNode
        {
            get
            {
                if (this.removeNode == null)
                {
                    this.removeNode = new ComponentListener();
                }
                return this.removeNode;
            }
        }

        private ComponentListener startDrag;

        /// <summary>
        /// Fires when a node starts being dragged
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("startdrag", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node starts being dragged")]
        public virtual ComponentListener StartDrag
        {
            get
            {
                if (this.startDrag == null)
                {
                    this.startDrag = new ComponentListener();
                }
                return this.startDrag;
            }
        }

        private ComponentListener textChange;

        /// <summary>
        /// Fires when the text for a node is changed
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "text")]
        [ListenerArgument(2, "oldText")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("textchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text for a node is changed")]
        public virtual ComponentListener TextChange
        {
            get
            {
                if (this.textChange == null)
                {
                    this.textChange = new ComponentListener();
                }
                return this.textChange;
            }
        }
    }
}