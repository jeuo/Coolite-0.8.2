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
    public class FieldAjaxEvents : BoxComponentAjaxEvents
    {
        private ComponentAjaxEvent blur;

        /// <summary>
        /// Fires when this field loses input focus.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("blur", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this field loses input focus.")]
        public virtual ComponentAjaxEvent Blur
        {
            get
            {
                if (this.blur == null)
                {
                    this.blur = new ComponentAjaxEvent();
                }
                return this.blur;
            }
        }

        private ComponentAjaxEvent change;

        /// <summary>
        /// Fires just before the field blurs if the field value has changed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [ListenerArgument(1, "newValue", typeof(object), "The new value")]
        [ListenerArgument(2, "oldValue", typeof(object), "The original value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires just before the field blurs if the field value has changed.")]
        public virtual ComponentAjaxEvent Change
        {
            get
            {
                if (this.change == null)
                {
                    this.change = new ComponentAjaxEvent();
                }
                return this.change;
            }
        }

        private ComponentAjaxEvent focus;

        /// <summary>
        /// Fires when this field receives input focus.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("focus", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this field receives input focus.")]
        public virtual ComponentAjaxEvent Focus
        {
            get
            {
                if (this.focus == null)
                {
                    this.focus = new ComponentAjaxEvent();
                }
                return this.focus;
            }
        }

        private ComponentAjaxEvent invalid;

        /// <summary>
        /// Fires after the field has been marked as invalid.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [ListenerArgument(1, "msg", typeof(string), "the validation message")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("invalid", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the field has been marked as invalid.")]
        public virtual ComponentAjaxEvent Invalid
        {
            get
            {
                if (this.invalid == null)
                {
                    this.invalid = new ComponentAjaxEvent();
                }
                return this.invalid;
            }
        }

        private ComponentAjaxEvent specialKey;

        /// <summary>
        /// Fires when any key related to navigation (arrows, tab, enter, esc, etc.) is pressed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [ListenerArgument(1, "e", typeof(object), "The event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("specialkey", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when any key related to navigation (arrows, tab, enter, esc, etc.) is pressed.")]
        public virtual ComponentAjaxEvent SpecialKey
        {
            get
            {
                if (this.specialKey == null)
                {
                    this.specialKey = new ComponentAjaxEvent();
                }
                return this.specialKey;
            }
        }

        private ComponentAjaxEvent valid;

        /// <summary>
        /// Fires after the field has been validated with no errors.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("valid", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the field has been validated with no errors.")]
        public virtual ComponentAjaxEvent Valid
        {
            get
            {
                if (this.valid == null)
                {
                    this.valid = new ComponentAjaxEvent();
                }
                return this.valid;
            }
        }
    }
}