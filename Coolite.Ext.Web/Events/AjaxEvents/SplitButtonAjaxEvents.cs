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
    public class SplitButtonAjaxEvents : ButtonAjaxEvents
    {
        private ComponentAjaxEvent arrowClick;

        /// <summary>
        /// Fires when this button's arrow is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(SplitButton), "this")]
        [ListenerArgument(1, "e", typeof(object), "The click event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("arrowclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this button's arrow is clicked.")]
        public virtual ComponentAjaxEvent ArrowClick
        {
            get
            {
                if (this.arrowClick == null)
                {
                    this.arrowClick = new ComponentAjaxEvent();
                }
                return this.arrowClick;
            }
        }
    }
}