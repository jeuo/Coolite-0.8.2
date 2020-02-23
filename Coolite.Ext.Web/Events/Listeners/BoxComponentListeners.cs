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
    public class BoxComponentListeners : ComponentBaseListeners
    {
        private ComponentListener move;

        /// <summary>
        /// Fires after the component is resized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "x", typeof(int), "The new x position")]
        [ListenerArgument(2, "y", typeof(int), "The new y position")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("move", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is resized.")]
        public virtual ComponentListener Move
        {
            get
            {
                if (this.move == null)
                {
                    this.move = new ComponentListener();
                }
                return this.move;
            }
        }

        private ComponentListener resize;

        /// <summary>
        /// Fires after the component is resized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "adjWidth", typeof(int), "The box-adjusted width that was set")]
        [ListenerArgument(2, "adjHeight", typeof(Component), "The box-adjusted height that was set")]
        [ListenerArgument(3, "rawWidth", typeof(Component), "The width that was originally specified")]
        [ListenerArgument(4, "rawHeight", typeof(Component), "The height that was originally specified")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("resize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is resized.")]
        public virtual ComponentListener Resize
        {
            get
            {
                if (this.resize == null)
                {
                    this.resize = new ComponentListener();
                }
                return this.resize;
            }
        }
    }
}