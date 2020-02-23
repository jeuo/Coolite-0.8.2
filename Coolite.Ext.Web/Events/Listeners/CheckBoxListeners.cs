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
    public class CheckboxListeners : FieldListeners
    {
        private ComponentListener check;

        /// <summary>
        /// Fires when the Checkbox is checked or unchecked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Checkbox), "this")]
        [ListenerArgument(1, "checked", typeof(bool), "The new checked value")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("check", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the Checkbox is checked or unchecked.")]
        public virtual ComponentListener Check
        {
            get
            {
                if (this.check == null)
                {
                    this.check = new ComponentListener();
                }
                return this.check;
            }
        }
    }
}