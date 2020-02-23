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
    public class TreeNodeAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent beforeChildRenrendered;

        /// <summary>
        /// Fires right before the child nodes for this node are rendered
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechildrenrendered", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires right before the child nodes for this node are rendered")]
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
        /// Fires before click processing. Return false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before click processing. Return false to cancel the default action.")]
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

        private ComponentAjaxEvent beforeCollapse;

        /// <summary>
        /// Fires before this node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is collapsed, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentAjaxEvent();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentAjaxEvent beforeExpand;

        /// <summary>
        /// Fires before this node is collapsed, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "deep")]
        [ListenerArgument(2, "anim")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is collapsed, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentAjaxEvent();
                }
                return this.beforeExpand;
            }
        }

        private ComponentAjaxEvent checkChange;

        /// <summary>
        /// Fires when a node with a checkbox's checked property changes.
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a node with a checkbox's checked property changes.")]
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
        /// Fires when this node is clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is clicked")]
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

        private ComponentAjaxEvent collapse;

        /// <summary>
        /// Fires when this node is collapsed
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is collapsed")]
        public virtual ComponentAjaxEvent Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentAjaxEvent();
                }
                return this.collapse;
            }
        }

        private ComponentAjaxEvent contextMenu;

        /// <summary>
        /// Fires when this node is right clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is right clicked")]
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
        /// Fires when this node is double clicked
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is double clicked")]
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
        /// Fires when the disabled status of this node changes
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "disabled")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disabledchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the disabled status of this node changes")]
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

        private ComponentAjaxEvent expand;

        /// <summary>
        /// Fires when this node is expanded
        /// </summary>
        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is expanded")]
        public virtual ComponentAjaxEvent Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentAjaxEvent();
                }
                return this.expand;
            }
        }

        private ComponentAjaxEvent textChange;

        /// <summary>
        /// Fires when the text for this node is changed
        /// </summary>
        [ListenerArgument(0, "node")]
        [ListenerArgument(1, "text")]
        [ListenerArgument(2, "oldText")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("textchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text for this node is changed")]
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