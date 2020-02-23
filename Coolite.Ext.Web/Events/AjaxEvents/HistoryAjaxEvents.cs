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
    public class HistoryAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent change;

        /// <summary>
        /// Handle this change event in order to restore the UI to the appropriate history state
        /// </summary>
        [ListenerArgument(0, "token", typeof(string))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Handle this change event in order to restore the UI to the appropriate history state")]
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
    }
}