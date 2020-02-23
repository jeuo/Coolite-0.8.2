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
    public class ResizableListeners : ComponentListeners
    {
        private ComponentListener beforeResize;

        [ListenerArgument(0, "el", typeof(Resizable), "this")]
        [ListenerArgument(1, "e", typeof(object), "The mousedown event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeresize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired before resize is allowed. Set enabled to false to cancel resize.")]
        public virtual ComponentListener BeforeResize
        {
            get
            {
                if (this.beforeResize == null)
                {
                    this.beforeResize = new ComponentListener();
                }
                return this.beforeResize;
            }
        }

        private ComponentListener resize;

        [ListenerArgument(0, "el", typeof(Resizable), "this")]
        [ListenerArgument(1, "width", typeof(int), "The new width")]
        [ListenerArgument(2, "height", typeof(int), "The new height")]
        [ListenerArgument(3, "e", typeof(object), "The mousedown event")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("resize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired after a resize.")]
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