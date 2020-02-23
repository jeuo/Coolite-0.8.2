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
    public class ButtonAjaxEvents : ComponentBaseAjaxEvents //ContainerAjaxEvents
    {
        private ComponentAjaxEvent click;

        /// <summary>
        /// Fires when this button is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "e", typeof(object), "The click event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this button is clicked.")]
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

        private ComponentAjaxEvent menuHide;

        /// <summary>
        /// If this button has a menu, this event fires when it is hidden.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "menu", typeof(object), "Menu")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("menuhide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("If this button has a menu, this event fires when it is hidden.")]
        public virtual ComponentAjaxEvent MenuHide
        {
            get
            {
                if (this.menuHide == null)
                {
                    this.menuHide = new ComponentAjaxEvent();
                }
                return this.menuHide;
            }
        }

        private ComponentAjaxEvent menuShow;

        /// <summary>
        /// If this button has a menu, this event fires when it is shown.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "menu", typeof(object), "Menu")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("menushow", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("If this button has a menu, this event fires when it is shown.")]
        public virtual ComponentAjaxEvent MenuShow
        {
            get
            {
                if (this.menuShow == null)
                {
                    this.menuShow = new ComponentAjaxEvent();
                }
                return this.menuShow;
            }
        }

        private ComponentAjaxEvent menuTriggerOut;

        /// <summary>
        /// If this button has a menu, this event fires when the mouse leaves the menu triggering element.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "menu", typeof(object), "Menu")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("menutriggerout", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("If this button has a menu, this event fires when the mouse leaves the menu triggering element.")]
        public virtual ComponentAjaxEvent MenuTriggerOut
        {
            get
            {
                if (this.menuTriggerOut == null)
                {
                    this.menuTriggerOut = new ComponentAjaxEvent();
                }
                return this.menuTriggerOut;
            }
        }

        private ComponentAjaxEvent menuTriggerOver;

        /// <summary>
        /// If this button has a menu, this event fires when the mouse enters the menu triggering element.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "menu", typeof(object), "Menu")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("menutriggerover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("If this button has a menu, this event fires when the mouse enters the menu triggering element.")]
        public virtual ComponentAjaxEvent MenuTriggerOver
        {
            get
            {
                if (this.menuTriggerOver == null)
                {
                    this.menuTriggerOver = new ComponentAjaxEvent();
                }
                return this.menuTriggerOver;
            }
        }

        private ComponentAjaxEvent mouseOut;

        /// <summary>
        /// Fires when the mouse exits the button.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "e", typeof(object), "The event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseout", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse exits the button.")]
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
        /// Fires when the mouse hovers over the button.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "e", typeof(object), "The event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the mouse hovers over the button.")]
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

        private ComponentAjaxEvent toggle;

        /// <summary>
        /// Fires when the 'pressed' state of this button changes (only if enableToggle = true).
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "pressed", typeof(bool), "Pressed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("toggle", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the 'pressed' state of this button changes (only if enableToggle = true).")]
        public virtual ComponentAjaxEvent Toggle
        {
            get
            {
                if (this.toggle == null)
                {
                    this.toggle = new ComponentAjaxEvent();
                }
                return this.toggle;
            }
        }
    }
}