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
    public class NodeListeners : ComponentListeners
    {
        private ComponentListener append;

        /// <summary>
        /// Fires when a new child node is appended
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("append", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is appended")]
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
        /// Fires before a new child is appended, return false to cancel the append
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeappend", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is appended, return false to cancel the append")]
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

        private ComponentListener beforeInsert;

        /// <summary>
        /// Fires before a new child is inserted, return false to cancel the insert.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeinsert", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is inserted, return false to cancel the insert.")]
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

        private ComponentListener beforeMove;

        /// <summary>
        /// Fires before this node is moved to a new location in the tree. Return false to cancel the move.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforemove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is moved to a new location in the tree. Return false to cancel the move.")]
        public virtual ComponentListener BeforeMove
        {
            get
            {
                if (this.beforeMove == null)
                {
                    this.beforeMove = new ComponentListener();
                }
                return this.beforeMove;
            }
        }

        private ComponentListener beforeRemove;

        /// <summary>
        /// Fires before a child is removed, return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "thisNode", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a child is removed, return false to cancel the remove.")]
        public virtual ComponentListener BeforeRemove
        {
            get
            {
                if (this.beforeRemove == null)
                {
                    this.beforeRemove = new ComponentListener();
                }
                return this.beforeRemove;
            }
        }

        private ComponentListener insert;

        /// <summary>
        /// Fires when a new child node is inserted.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("insert", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is inserted.")]
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

        private ComponentListener move;

        /// <summary>
        /// Fires when this node is moved to a new location in the tree
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("move", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is moved to a new location in the tree")]
        public virtual ComponentListener Move
        {
            get
            {
                if (this.move == null)
                {
                    this.move = new ComponentListener();
                }
                return this.move;
            }
        }

        private ComponentListener remove;

        /// <summary>
        /// Fires when a child node is removed
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "thisNode", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a child node is removed")]
        public virtual ComponentListener Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new ComponentListener();
                }
                return this.remove;
            }
        }
    }
}