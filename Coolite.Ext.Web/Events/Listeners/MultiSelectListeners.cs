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
    public class MultiSelectListeners : FieldListeners
    {
        private ComponentListener click;

        [ListenerArgument(0, "el", typeof(MultiSelect), "this")]
        [ListenerArgument(1, "e", typeof(object), "The click event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
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

        private ComponentListener dblClick;

        [ListenerArgument(0, "el", typeof(MultiSelect), "this")]
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

        private ComponentListener beforeDrop;

        [ListenerArgument(0, "view", typeof(object))]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "dd", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [ListenerArgument(4, "data", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeDrop
        {
            get
            {
                if (this.beforeDrop == null)
                {
                    this.beforeDrop = new ComponentListener();
                }
                return this.beforeDrop;
            }
        }

        private ComponentListener afterDrop;

        [ListenerArgument(0, "view", typeof(object))]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "dd", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [ListenerArgument(4, "data", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afterdrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener AfterDrop
        {
            get
            {
                if (this.afterDrop == null)
                {
                    this.afterDrop = new ComponentListener();
                }
                return this.afterDrop;
            }
        }
    }
}