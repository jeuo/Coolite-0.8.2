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
    public class TextFieldListeners : FieldListeners
    {
        private ComponentListener autoSize;

        /// <summary>
        /// Fires when the autosize function is triggered. The field may or may not have actually changed size according to the default logic, but this event provides a hook for the developer to apply additional logic at runtime to resize the field if needed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "width", typeof(int), "The new field width")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("autosize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the autosize function is triggered. The field may or may not have actually changed size according to the default logic, but this event provides a hook for the developer to apply additional logic at runtime to resize the field if needed.")]
        public virtual ComponentListener AutoSize
        {
            get
            {
                if (this.autoSize == null)
                {
                    this.autoSize = new ComponentListener();
                }
                return this.autoSize;
            }
        }

        private ComponentListener keyDown;

        /// <summary>
        /// Keydown input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keydown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keydown input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentListener KeyDown
        {
            get
            {
                if (this.keyDown == null)
                {
                    this.keyDown = new ComponentListener();
                }
                return this.keyDown;
            }
        }

        private ComponentListener keyPress;

        /// <summary>
        /// Keypress input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keypress", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keypress input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentListener KeyPress
        {
            get
            {
                if (this.keyPress == null)
                {
                    this.keyPress = new ComponentListener();
                }
                return this.keyPress;
            }
        }

        private ComponentListener keyUp;

        /// <summary>
        /// Keyup input field event. This event only fires if enableKeyEvents is set to true.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This text field")]
        [ListenerArgument(1, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keyup", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Keyup input field event. This event only fires if enableKeyEvents is set to true.")]
        public virtual ComponentListener KeyUp
        {
            get
            {
                if (this.keyUp == null)
                {
                    this.keyUp = new ComponentListener();
                }
                return this.keyUp;
            }
        }
    }
}