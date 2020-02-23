/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class InlineEditorAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent beforestartedit;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "boundEl")]
        [ListenerArgument(2, "value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestartedit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeStartEdit
        {
            get
            {
                if (this.beforestartedit == null)
                {
                    this.beforestartedit = new ComponentAjaxEvent();
                }
                return this.beforestartedit;
            }
        }

        private ComponentAjaxEvent canceledit;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "value")]
        [ListenerArgument(2, "startValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("canceledit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent CancelEdit
        {
            get
            {
                if (this.canceledit == null)
                {
                    this.canceledit = new ComponentAjaxEvent();
                }
                return this.canceledit;
            }
        }

        private ComponentAjaxEvent complete;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "value")]
        [ListenerArgument(2, "startValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("complete", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent Complete
        {
            get
            {
                if (this.complete == null)
                {
                    this.complete = new ComponentAjaxEvent();
                }
                return this.complete;
            }
        }

        private ComponentAjaxEvent specialkey;

        [ListenerArgument(0, "field")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("specialkey", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent SpecialKey
        {
            get
            {
                if (this.specialkey == null)
                {
                    this.specialkey = new ComponentAjaxEvent();
                }
                return this.specialkey;
            }
        }

        private ComponentAjaxEvent startedit;

        [ListenerArgument(0, "boundEl")]
        [ListenerArgument(1, "value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("startedit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent StartEdit
        {
            get
            {
                if (this.startedit == null)
                {
                    this.startedit = new ComponentAjaxEvent();
                }
                return this.startedit;
            }
        }
    }
}