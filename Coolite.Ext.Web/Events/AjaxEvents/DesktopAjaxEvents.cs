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
    public class DesktopAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent shortcutClick;

        [ListenerArgument(0, "id")]
        [ListenerArgument(1, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("shortcutclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent ShortcutClick
        {
            get
            {
                if (this.shortcutClick == null)
                {
                    this.shortcutClick = new ComponentAjaxEvent();
                }
                return this.shortcutClick;
            }
        }
    }
}