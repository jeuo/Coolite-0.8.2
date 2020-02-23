/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class ProgressBarAjaxEvents : BoxComponentAjaxEvents
    {
        private ComponentAjaxEvent update;

        /// <summary>
        /// Fires after each update interval
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "value", typeof(int), "current progress value")]
        [ListenerArgument(2, "text", typeof(int), "current progress text")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("update", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after each update interval")]
        public virtual ComponentAjaxEvent Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new ComponentAjaxEvent();
                }
                return this.update;
            }
        }
    }
}