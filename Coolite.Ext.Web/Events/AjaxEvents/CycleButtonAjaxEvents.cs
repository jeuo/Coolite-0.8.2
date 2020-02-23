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
    public class CycleButtonAjaxEvents : SplitButtonAjaxEvents
    {
        private ComponentAjaxEvent change;

        /// <summary>
        /// Fires after the button's active menu items has changed. Note that if a changeHandler function is set on this CycleButton, it will be called instead on active items change and this change event will not be fired.
        /// </summary>
        [ListenerArgument(0, "el", typeof(CycleButton), "this")]
        [ListenerArgument(1, "item")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the button's active menu items has changed. Note that if a changeHandler function is set on this CycleButton, it will be called instead on active items change and this change event will not be fired.")]
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