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
    public class CheckMenuItemAjaxEvents : BaseMenuItemAjaxEvents
    {
        private ComponentAjaxEvent beforeCheckChange;

        /// <summary>
        /// Fires before the checked value is set, providing an opportunity to cancel if needed
        /// </summary>
        [ListenerArgument(0, "el", typeof(CheckMenuItem), "this")]
        [ListenerArgument(1, "checked", typeof(bool), "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecheckchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the checked value is set, providing an opportunity to cancel if needed")]
        public virtual ComponentAjaxEvent BeforeCheckChange
        {
            get
            {
                if (this.beforeCheckChange == null)
                {
                    this.beforeCheckChange = new ComponentAjaxEvent();
                }
                return this.beforeCheckChange;
            }
        }

        private ComponentAjaxEvent checkChange;

        /// <summary>
        /// Fires after the checked value has been set
        /// </summary>
        [ListenerArgument(0, "el", typeof(CheckMenuItem), "this")]
        [ListenerArgument(1, "checked", typeof(bool), "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the checked value has been set")]
        public virtual ComponentAjaxEvent CheckChange
        {
            get
            {
                if (this.checkChange == null)
                {
                    this.checkChange = new ComponentAjaxEvent();
                }
                return this.checkChange;
            }
        }
    }
}