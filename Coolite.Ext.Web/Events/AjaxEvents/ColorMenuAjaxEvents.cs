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
    public class ColorMenuAjaxEvents : MenuAjaxEvents
    {
        private ComponentAjaxEvent select;

        [ListenerArgument(0, "el", typeof(ColorPalette), "palette")]
        [ListenerArgument(1, "color", typeof(object), "color")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("select", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent Select
        {
            get
            {
                if (this.select == null)
                {
                    this.select = new ComponentAjaxEvent();
                }
                return this.select;
            }
        }
    }
}