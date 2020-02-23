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
    public class BaseMenuItemListeners : ComponentBaseListeners
    {
        private ComponentListener activate;

        /// <summary>
        /// Fires when this item is activated
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is activated")]
        public virtual ComponentListener Activate
        {
            get
            {
                if (this.activate == null)
                {
                    this.activate = new ComponentListener();
                }
                return this.activate;
            }
        }

        private ComponentListener click;

        /// <summary>
        /// Fires when this item is clicked
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [ListenerArgument(1, "e", typeof(BaseMenuItem), "Ext.EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is clicked")]
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

        private ComponentListener deactivate;

        /// <summary>
        /// Fires when this item is deactivated
        /// </summary>
        [ListenerArgument(0, "el", typeof(BaseMenuItem), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this item is deactivated")]
        public virtual ComponentListener Deactivate
        {
            get
            {
                if (this.deactivate == null)
                {
                    this.deactivate = new ComponentListener();
                }
                return this.deactivate;
            }
        }
    }
}