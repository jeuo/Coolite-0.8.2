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
    public class ColorPaletteAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent select;

        /// <summary>
        /// Fires when a color is selected
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("select", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a color is selected")]
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