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
    public class MenuAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent beforeHide;

        /// <summary>
        /// Fires before this menu is hidden.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforehide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this menu is hidden.")]
        public virtual ComponentAjaxEvent BeforeHide
        {
            get
            {
                if (this.beforeHide == null)
                {
                    this.beforeHide = new ComponentAjaxEvent();
                }
                return this.beforeHide;
            }
        }

        private ComponentAjaxEvent beforeShow;

        /// <summary>
        /// Fires before this menu is displayed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeshow", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this menu is displayed.")]
        public virtual ComponentAjaxEvent BeforeShow
        {
            get
            {
                if (this.beforeShow == null)
                {
                    this.beforeShow = new ComponentAjaxEvent();
                }
                return this.beforeShow;
            }
        }

        private ComponentAjaxEvent click;

        /// <summary>
        /// Fires when this menu is clicked (or when the enter key is pressed while it is active)
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(1, "menuItem", typeof(object))]
        [ListenerArgument(2, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this menu is clicked (or when the enter key is pressed while it is active)")]
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

        private ComponentAjaxEvent hide;

        /// <summary>
        /// Fires after this menu is hidden
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after this menu is hidden")]
        public virtual ComponentAjaxEvent Hide
        {
            get
            {
                if (this.hide == null)
                {
                    this.hide = new ComponentAjaxEvent();
                }
                return this.hide;
            }
        }

        private ComponentAjaxEvent itemClick;

        /// <summary>
        /// Fires when a menu item contained in this menu is clicked
        /// </summary>
        [ListenerArgument(0, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("itemclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a menu item contained in this menu is clicked")]
        public virtual ComponentAjaxEvent ItemClick
        {
            get
            {
                if (this.itemClick == null)
                {
                    this.itemClick = new ComponentAjaxEvent();
                }
                return this.itemClick;
            }
        }

        private ComponentAjaxEvent mouseOut;

        /// <summary>
        /// Fires when the mouse exits this menu
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(2, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseout", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse exits this menu")]
        public virtual ComponentAjaxEvent MouseOut
        {
            get
            {
                if (this.mouseOut == null)
                {
                    this.mouseOut = new ComponentAjaxEvent();
                }
                return this.mouseOut;
            }
        }

        private ComponentAjaxEvent mouseOver;

        /// <summary>
        /// Fires when the mouse is hovering over this menu
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(2, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse is hovering over this menu")]
        public virtual ComponentAjaxEvent MouseOver
        {
            get
            {
                if (this.mouseOver == null)
                {
                    this.mouseOver = new ComponentAjaxEvent();
                }
                return this.mouseOver;
            }
        }

        private ComponentAjaxEvent show;

        /// <summary>
        /// Fires after this menu is displayed
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("show", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after this menu is displayed")]
        public virtual ComponentAjaxEvent Show
        {
            get
            {
                if (this.show == null)
                {
                    this.show = new ComponentAjaxEvent();
                }
                return this.show;
            }
        }
    }
}