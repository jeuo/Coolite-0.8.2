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
    public class MenuListeners : ComponentListeners
    {
        private ComponentListener beforeHide;

        /// <summary>
        /// Fires before this menu is hidden.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforehide", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this menu is hidden.")]
        public virtual ComponentListener BeforeHide
        {
            get
            {
                if (this.beforeHide == null)
                {
                    this.beforeHide = new ComponentListener();
                }
                return this.beforeHide;
            }
        }

        private ComponentListener beforeShow;

        /// <summary>
        /// Fires before this menu is displayed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeshow", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this menu is displayed.")]
        public virtual ComponentListener BeforeShow
        {
            get
            {
                if (this.beforeShow == null)
                {
                    this.beforeShow = new ComponentListener();
                }
                return this.beforeShow;
            }
        }

        private ComponentListener click;

        /// <summary>
        /// Fires when this menu is clicked (or when the enter key is pressed while it is active)
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(1, "menuItem", typeof(object))]
        [ListenerArgument(2, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this menu is clicked (or when the enter key is pressed while it is active)")]
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

        private ComponentListener hide;

        /// <summary>
        /// Fires after this menu is hidden
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hide", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after this menu is hidden")]
        public virtual ComponentListener Hide
        {
            get
            {
                if (this.hide == null)
                {
                    this.hide = new ComponentListener();
                }
                return this.hide;
            }
        }

        private ComponentListener itemClick;

        /// <summary>
        /// Fires when a menu item contained in this menu is clicked
        /// </summary>
        [ListenerArgument(0, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("itemclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a menu item contained in this menu is clicked")]
        public virtual ComponentListener ItemClick
        {
            get
            {
                if (this.itemClick == null)
                {
                    this.itemClick = new ComponentListener();
                }
                return this.itemClick;
            }
        }

        private ComponentListener mouseOut;

        /// <summary>
        /// Fires when the mouse exits this menu
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(2, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseout", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse exits this menu")]
        public virtual ComponentListener MouseOut
        {
            get
            {
                if (this.mouseOut == null)
                {
                    this.mouseOut = new ComponentListener();
                }
                return this.mouseOut;
            }
        }

        private ComponentListener mouseOver;

        /// <summary>
        /// Fires when the mouse is hovering over this menu
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [ListenerArgument(2, "menuItem", typeof(object))]
        [ListenerArgument(1, "e", typeof(Menu), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseover", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse is hovering over this menu")]
        public virtual ComponentListener MouseOver
        {
            get
            {
                if (this.mouseOver == null)
                {
                    this.mouseOver = new ComponentListener();
                }
                return this.mouseOver;
            }
        }

        private ComponentListener show;

        /// <summary>
        /// Fires after this menu is displayed
        /// </summary>
        [ListenerArgument(0, "el", typeof(Menu), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("show", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after this menu is displayed")]
        public virtual ComponentListener Show
        {
            get
            {
                if (this.show == null)
                {
                    this.show = new ComponentListener();
                }
                return this.show;
            }
        }
    }
}