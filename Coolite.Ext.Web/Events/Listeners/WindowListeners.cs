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
    public class WindowListeners : PanelListeners
    {
        private ComponentListener maximize;

        /// <summary>
        /// Fires after the window has been maximized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Window), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("maximize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the window has been maximized.")]
        public virtual ComponentListener Maximize
        {
            get
            {
                if (this.maximize == null)
                {
                    this.maximize = new ComponentListener();
                }
                return this.maximize;
            }
        }

        private ComponentListener minimize;

        /// <summary>
        /// Fires after the window has been minimized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Window), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("minimize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the window has been minimized.")]
        public virtual ComponentListener Minimize
        {
            get
            {
                if (this.minimize == null)
                {
                    this.minimize = new ComponentListener();
                }
                return this.minimize;
            }
        }

        private ComponentListener restore;

        /// <summary>
        /// Fires after the window has been restored to its original size after being maximized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Window), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("restore", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the window has been restored to its original size after being maximized.")]
        public virtual ComponentListener Restore
        {
            get
            {
                if (this.restore == null)
                {
                    this.restore = new ComponentListener();
                }
                return this.restore;
            }
        }
    }
}