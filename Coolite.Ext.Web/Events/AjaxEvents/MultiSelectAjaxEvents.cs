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
    public class MultiSelectAjaxEvents : FieldAjaxEvents
    {
        private ComponentAjaxEvent click;

        [ListenerArgument(0, "el", typeof(MultiSelect), "this")]
        [ListenerArgument(1, "e", typeof(object), "The click event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
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

        private ComponentAjaxEvent dblClick;

        [ListenerArgument(0, "el", typeof(MultiSelect), "this")]
        [ListenerArgument(1, "index", typeof(object))]
        [ListenerArgument(2, "node", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a template node is double clicked.")]
        public virtual ComponentAjaxEvent DblClick
        {
            get
            {
                if (this.dblClick == null)
                {
                    this.dblClick = new ComponentAjaxEvent();
                }
                return this.dblClick;
            }
        }

        private ComponentAjaxEvent beforeDrop;

        [ListenerArgument(0, "view", typeof(object))]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "dd", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [ListenerArgument(4, "data", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeDrop
        {
            get
            {
                if (this.beforeDrop == null)
                {
                    this.beforeDrop = new ComponentAjaxEvent();
                }
                return this.beforeDrop;
            }
        }

        private ComponentAjaxEvent afterDrop;

        [ListenerArgument(0, "view", typeof(object))]
        [ListenerArgument(1, "node", typeof(object))]
        [ListenerArgument(2, "dd", typeof(object))]
        [ListenerArgument(3, "e", typeof(object))]
        [ListenerArgument(4, "data", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afterdrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent AfterDrop
        {
            get
            {
                if (this.afterDrop == null)
                {
                    this.afterDrop = new ComponentAjaxEvent();
                }
                return this.afterDrop;
            }
        }
    }
}