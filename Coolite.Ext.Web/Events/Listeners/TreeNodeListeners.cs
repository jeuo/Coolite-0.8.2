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
    public class TreeNodeListeners : NodeListeners
    {
        private ComponentListener beforeChildRenrendered;

        /// <summary>
        /// Fires right before the child nodes for this node are rendered
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechildrenrendered", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires right before the child nodes for this node are rendered")]
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
        /// Fires before click processing. Return false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before click processing. Return false to cancel the default action.")]
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

        private ComponentListener beforeCollapse;

        /// <summary>
        /// Fires before this node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is collapsed, return false to cancel.")]
        public virtual ComponentListener BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentListener();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentListener beforeExpand;

        /// <summary>
        /// Fires before this node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is collapsed, return false to cancel.")]
        public virtual ComponentListener BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentListener();
                }
                return this.beforeExpand;
            }
        }

        private ComponentListener checkChange;

        /// <summary>
        /// Fires when a node with a checkbox's checked property changes.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node with a checkbox's checked property changes.")]
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
        /// Fires when this node is clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is clicked")]
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

        private ComponentListener collapse;

        /// <summary>
        /// Fires when this node is collapsed
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is collapsed")]
        public virtual ComponentListener Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentListener();
                }
                return this.collapse;
            }
        }

        private ComponentListener contextMenu;

        /// <summary>
        /// Fires when this node is right clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is right clicked")]
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
        /// Fires when this node is double clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is double clicked")]
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
        /// Fires when the disabled status of this node changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "disabled")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disabledchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the disabled status of this node changes")]
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

        private ComponentListener expand;

        /// <summary>
        /// Fires when this node is expanded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is expanded")]
        public virtual ComponentListener Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentListener();
                }
                return this.expand;
            }
        }

        private ComponentListener textChange;

        /// <summary>
        /// Fires when the text for this node is changed
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "text")]
        [ListenerArgument(2, "oldText")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("textchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text for this node is changed")]
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