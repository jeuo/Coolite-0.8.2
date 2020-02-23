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
    public class TriggerFieldListeners : TextFieldListeners
    {
        private ComponentListener triggerClick;

        [ListenerArgument(0, "el", typeof(Field), "This trigger field")]
        [ListenerArgument(1, "trigger", typeof(object), "trigger")]
        [ListenerArgument(2, "index", typeof(int), "trigger index")]
        [ListenerArgument(3, "e", typeof(int), "click event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("triggerclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener TriggerClick
        {
            get
            {
                if (this.triggerClick == null)
                {
                    this.triggerClick = new ComponentListener();
                }
                return this.triggerClick;
            }
        }
    }
}