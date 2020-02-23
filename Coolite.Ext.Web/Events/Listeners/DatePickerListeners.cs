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
    public class DatePickerListeners : ComponentBaseListeners
    {
        private ComponentListener select;

        /// <summary>
        /// Fires when a date is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(DatePicker), "this")]
        [ListenerArgument(1, "date", typeof(DateTime), "The selected date")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("select", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a date is selected.")]
        public virtual ComponentListener Select
        {
            get
            {
                if (this.select == null)
                {
                    this.select = new ComponentListener();
                }
                return this.select;
            }
        }
    }
}