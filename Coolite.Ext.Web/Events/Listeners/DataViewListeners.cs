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
    public class DataViewListeners : BoxComponentListeners
    {
        private ComponentListener beforeClick;

        /// <summary>
        /// Fires before a click is processed. Returns false to cancel the default action.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a click is processed. Returns false to cancel the default action.")]
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

        private ComponentListener beforeSelect;

        /// <summary>
        /// Fires before a selection is made. If any handlers return false, the selection is cancelled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "selections", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeselect", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a selection is made. If any handlers return false, the selection is cancelled.")]
        public virtual ComponentListener BeforeSelect
        {
            get
            {
                if (this.beforeSelect == null)
                {
                    this.beforeSelect = new ComponentListener();
                }
                return this.beforeSelect;
            }
        }

        private ComponentListener click;

        /// <summary>
        /// Fires when a template node is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is clicked.")]
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

        private ComponentListener containerClick;

        /// <summary>
        /// Fires when a click occurs and it is not on a template node.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("containerclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a click occurs and it is not on a template node.")]
        public virtual ComponentListener ContainerClick
        {
            get
            {
                if (this.containerClick == null)
                {
                    this.containerClick = new ComponentListener();
                }
                return this.containerClick;
            }
        }

        private ComponentListener contextMenu;

        /// <summary>
        /// Fires when a template node is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is right clicked.")]
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
        /// Fires when a template node is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is double clicked.")]
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

        private ComponentListener mouseEnter;

        /// <summary>
        /// Fires when the mouse enters a template node. trackOver:true or an overCls must be set to enable this event.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseenter", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse enters a template node. trackOver:true or an overCls must be set to enable this event.")]
        public virtual ComponentListener MouseEnter
        {
            get
            {
                if (this.mouseEnter == null)
                {
                    this.mouseEnter = new ComponentListener();
                }
                return this.mouseEnter;
            }
        }

        private ComponentListener mouseLeave;

        /// <summary>
        /// Fires when the mouse leaves a template node. trackOver:true or an overCls must be set to enable this event.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseleave", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse leaves a template node. trackOver:true or an overCls must be set to enable this event.")]
        public virtual ComponentListener MouseLeave
        {
            get
            {
                if (this.mouseLeave == null)
                {
                    this.mouseLeave = new ComponentListener();
                }
                return this.mouseLeave;
            }
        }

        private ComponentListener selectionChange;

        /// <summary>
        /// Fires when the selected nodes change.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DataView), "this")]
        [ListenerArgument(1, "selections", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("selectionchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the selected nodes change.")]
        public virtual ComponentListener SelectionChange
        {
            get
            {
                if (this.selectionChange == null)
                {
                    this.selectionChange = new ComponentListener();
                }
                return this.selectionChange;
            }
        }
    }
}