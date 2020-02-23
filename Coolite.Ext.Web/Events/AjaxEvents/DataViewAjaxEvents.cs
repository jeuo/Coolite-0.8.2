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
    public class DataViewAjaxEvents : ContainerAjaxEvents
    {
        private ComponentAjaxEvent beforeClick;

        /// <summary>
        /// Fires before a click is processed. Returns false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a click is processed. Returns false to cancel the default action.")]
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

        private ComponentAjaxEvent beforeSelect;

        /// <summary>
        /// Fires before a selection is made. If any handlers return false, the selection is cancelled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "selections", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeselect", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a selection is made. If any handlers return false, the selection is cancelled.")]
        public virtual ComponentAjaxEvent BeforeSelect
        {
            get
            {
                if (this.beforeSelect == null)
                {
                    this.beforeSelect = new ComponentAjaxEvent();
                }
                return this.beforeSelect;
            }
        }

        private ComponentAjaxEvent click;

        /// <summary>
        /// Fires when a template node is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is clicked.")]
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

        private ComponentAjaxEvent containerClick;

        /// <summary>
        /// Fires when a click occurs and it is not on a template node.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("containerclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a click occurs and it is not on a template node.")]
        public virtual ComponentAjaxEvent ContainerClick
        {
            get
            {
                if (this.containerClick == null)
                {
                    this.containerClick = new ComponentAjaxEvent();
                }
                return this.containerClick;
            }
        }

        private ComponentAjaxEvent contextMenu;

        /// <summary>
        /// Fires when a template node is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is right clicked.")]
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
        /// Fires when a template node is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is double clicked.")]
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

        private ComponentAjaxEvent mouseEnter;

        /// <summary>
        /// Fires when the mouse enters a template node. trackOver:true or an overCls must be set to enable this event.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseenter", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse enters a template node. trackOver:true or an overCls must be set to enable this event.")]
        public virtual ComponentAjaxEvent MouseEnter
        {
            get
            {
                if (this.mouseEnter == null)
                {
                    this.mouseEnter = new ComponentAjaxEvent();
                }
                return this.mouseEnter;
            }
        }

        private ComponentAjaxEvent mouseLeave;

        /// <summary>
        /// Fires when the mouse leaves a template node. trackOver:true or an overCls must be set to enable this event.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseleave", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse leaves a template node. trackOver:true or an overCls must be set to enable this event.")]
        public virtual ComponentAjaxEvent MouseLeave
        {
            get
            {
                if (this.mouseLeave == null)
                {
                    this.mouseLeave = new ComponentAjaxEvent();
                }
                return this.mouseLeave;
            }
        }

        private ComponentAjaxEvent selectionChange;

        /// <summary>
        /// Fires when the selected nodes change.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "selections", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("selectionchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the selected nodes change.")]
        public virtual ComponentAjaxEvent SelectionChange
        {
            get
            {
                if (this.selectionChange == null)
                {
                    this.selectionChange = new ComponentAjaxEvent();
                }
                return this.selectionChange;
            }
        }
    }
}