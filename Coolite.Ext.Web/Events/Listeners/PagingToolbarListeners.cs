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
    public class PagingToolbarListeners : BoxComponentListeners
    {
        private ComponentListener change;

        /// <summary>
        /// Fires after page changing
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "data", typeof(object), "{total, activePage, pages}")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after page changing")]
        public virtual ComponentListener Change
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

        private ComponentListener beforeChange;

        /// <summary>
        /// Fires before page changing
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "o", typeof(object), "{start, limit}")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before page changing")]
        public virtual ComponentListener BeforeChange
        {
            get
            {
                if (this.beforeChange == null)
                {
                    this.beforeChange = new ComponentListener();
                }
                return this.beforeChange;
            }
        }
    }
}