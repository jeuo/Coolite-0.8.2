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
    public class NodeAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent append;

        /// <summary>
        /// Fires when a new child node is appended
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("append", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is appended")]
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
        /// Fires before a new child is appended, return false to cancel the append
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeappend", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is appended, return false to cancel the append")]
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

        private ComponentAjaxEvent beforeInsert;

        /// <summary>
        /// Fires before a new child is inserted, return false to cancel the insert.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeinsert", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a new child is inserted, return false to cancel the insert.")]
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

        private ComponentAjaxEvent beforeMove;

        /// <summary>
        /// Fires before this node is moved to a new location in the tree. Return false to cancel the move.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforemove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is moved to a new location in the tree. Return false to cancel the move.")]
        public virtual ComponentAjaxEvent BeforeMove
        {
            get
            {
                if (this.beforeMove == null)
                {
                    this.beforeMove = new ComponentAjaxEvent();
                }
                return this.beforeMove;
            }
        }

        private ComponentAjaxEvent beforeRemove;

        /// <summary>
        /// Fires before a child is removed, return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "thisNode", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a child is removed, return false to cancel the remove.")]
        public virtual ComponentAjaxEvent BeforeRemove
        {
            get
            {
                if (this.beforeRemove == null)
                {
                    this.beforeRemove = new ComponentAjaxEvent();
                }
                return this.beforeRemove;
            }
        }

        private ComponentAjaxEvent insert;

        /// <summary>
        /// Fires when a new child node is inserted.
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "newNode", typeof(Node))]
        [ListenerArgument(3, "refNode")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("insert", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a new child node is inserted.")]
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

        private ComponentAjaxEvent move;

        /// <summary>
        /// Fires when this node is moved to a new location in the tree
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "oldParent", typeof(Node))]
        [ListenerArgument(3, "newParent", typeof(Node))]
        [ListenerArgument(4, "index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("move", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is moved to a new location in the tree")]
        public virtual ComponentAjaxEvent Move
        {
            get
            {
                if (this.move == null)
                {
                    this.move = new ComponentAjaxEvent();
                }
                return this.move;
            }
        }

        private ComponentAjaxEvent remove;

        /// <summary>
        /// Fires when a child node is removed
        /// </summary>
        [ListenerArgument(0, "tree")]
        [ListenerArgument(1, "thisNode", typeof(Node))]
        [ListenerArgument(2, "node", typeof(Node))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a child node is removed")]
        public virtual ComponentAjaxEvent Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new ComponentAjaxEvent();
                }
                return this.remove;
            }
        }
    }
}