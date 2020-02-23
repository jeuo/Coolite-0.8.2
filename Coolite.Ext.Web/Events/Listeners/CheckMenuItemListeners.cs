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
    public class CheckMenuItemListeners : BaseMenuItemListeners
    {
        private ComponentListener beforeCheckChange;

        /// <summary>
        /// Fires before the checked value is set, providing an opportunity to cancel if needed
        /// </summary>
        [ListenerArgument(0, "el", typeof(CheckMenuItem), "this")]
        [ListenerArgument(1, "checked", typeof(bool), "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecheckchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the checked value is set, providing an opportunity to cancel if needed")]
        public virtual ComponentListener BeforeCheckChange
        {
            get
            {
                if (this.beforeCheckChange == null)
                {
                    this.beforeCheckChange = new ComponentListener();
                }
                return this.beforeCheckChange;
            }
        }

        private ComponentListener checkChange;

        /// <summary>
        /// Fires after the checked value has been set
        /// </summary>
        [ListenerArgument(0, "el", typeof(CheckMenuItem), "this")]
        [ListenerArgument(1, "checked", typeof(bool), "checked")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("checkchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the checked value has been set")]
        public virtual ComponentListener CheckChange
        {
            get
            {
                if (this.checkChange == null)
                {
                    this.checkChange = new ComponentListener();
                }
                return this.checkChange;
            }
        }
    }
}