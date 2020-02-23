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
    public class InlineEditorListeners : ComponentBaseListeners
    {
        private ComponentListener beforestartedit;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "boundEl")]
        [ListenerArgument(2, "value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestartedit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeStartEdit
        {
            get
            {
                if (this.beforestartedit == null)
                {
                    this.beforestartedit = new ComponentListener();
                }
                return this.beforestartedit;
            }
        }

        private ComponentListener canceledit;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "value")]
        [ListenerArgument(2, "startValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("canceledit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener CancelEdit
        {
            get
            {
                if (this.canceledit == null)
                {
                    this.canceledit = new ComponentListener();
                }
                return this.canceledit;
            }
        }

        private ComponentListener complete;

        [ListenerArgument(0, "editor")]
        [ListenerArgument(1, "value")]
        [ListenerArgument(2, "startValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("complete", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener Complete
        {
            get
            {
                if (this.complete == null)
                {
                    this.complete = new ComponentListener();
                }
                return this.complete;
            }
        }

        private ComponentListener specialkey;

        [ListenerArgument(0, "field")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("specialkey", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener SpecialKey
        {
            get
            {
                if (this.specialkey == null)
                {
                    this.specialkey = new ComponentListener();
                }
                return this.specialkey;
            }
        }

        private ComponentListener startedit;

        [ListenerArgument(0, "boundEl")]
        [ListenerArgument(1, "value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("startedit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener StartEdit
        {
            get
            {
                if (this.startedit == null)
                {
                    this.startedit = new ComponentListener();
                }
                return this.startedit;
            }
        }
    }
}