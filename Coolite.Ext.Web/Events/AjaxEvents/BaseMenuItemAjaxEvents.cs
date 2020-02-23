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
    public class BaseMenuItemAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent activate;

        /// <summary>
        /// Fires when this item is activated
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is activated")]
        public virtual ComponentAjaxEvent Activate
        {
            get
            {
                if (this.activate == null)
                {
                    this.activate = new ComponentAjaxEvent();
                }
                return this.activate;
            }
        }

        private ComponentAjaxEvent click;

        /// <summary>
        /// Fires when this item is clicked
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [ListenerArgument(1, "e", typeof(object), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is clicked")]
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

        private ComponentAjaxEvent deactivate;

        /// <summary>
        /// Fires when this item is deactivated
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is deactivated")]
        public virtual ComponentAjaxEvent Deactivate
        {
            get
            {
                if (this.deactivate == null)
                {
                    this.deactivate = new ComponentAjaxEvent();
                }
                return this.deactivate;
            }
        }
    }
}