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
    public class RadioGroupListeners : CheckboxGroupListeners
    {
        private ComponentListener change;

        /// <summary>
        /// Fires when the state of a child radio changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(RadioGroup), "this")]
        [ListenerArgument(1, "checked", typeof(Radio), "The checked radio")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the state of a child radio changes.")]
        public override ComponentListener Change
        {
            get
            {
                if (this.change == null)
                {
                    this.change = new ComponentListener();
                }
                return this.change;
            }
        }
    }
}