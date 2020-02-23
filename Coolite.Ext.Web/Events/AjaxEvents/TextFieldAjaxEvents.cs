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
    public class TextFieldAjaxEvents : FieldAjaxEvents
    {
        private ComponentAjaxEvent autoSize;

        /// <summary>
        /// Fires when the autosize function is triggered. The field may or may not have actually changed size according to the default logic, but this event provides a hook for the developer to apply additional logic at runtime to resize the field if needed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "width", typeof(int), "The new field width")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("autosize", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the autosize function is triggered. The field may or may not have actually changed size according to the default logic, but this event provides a hook for the developer to apply additional logic at runtime to resize the field if needed.")]
        public virtual ComponentAjaxEvent AutoSize
        {
            get
            {
                if (this.autoSize == null)
                {
                    this.autoSize = new ComponentAjaxEvent();
                }
                return this.autoSize;
            }
        }

        private ComponentAjaxEvent keyDown;

        /// <summary>
        /// Keydown input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keydown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keydown input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentAjaxEvent KeyDown
        {
            get
            {
                if (this.keyDown == null)
                {
                    this.keyDown = new ComponentAjaxEvent();
                }
                return this.keyDown;
            }
        }

        private ComponentAjaxEvent keyPress;

        /// <summary>
        /// Keypress input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keypress", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keypress input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentAjaxEvent KeyPress
        {
            get
            {
                if (this.keyPress == null)
                {
                    this.keyPress = new ComponentAjaxEvent();
                }
                return this.keyPress;
            }
        }

        private ComponentAjaxEvent keyUp;

        /// <summary>
        /// Keyup input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keyup", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keyup input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentAjaxEvent KeyUp
        {
            get
            {
                if (this.keyUp == null)
                {
                    this.keyUp = new ComponentAjaxEvent();
                }
                return this.keyUp;
            }
        }
    }
}