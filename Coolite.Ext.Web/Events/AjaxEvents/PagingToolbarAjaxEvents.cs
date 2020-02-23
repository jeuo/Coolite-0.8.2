/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class PagingToolbarAjaxEvents : BoxComponentAjaxEvents
    {
        private ComponentAjaxEvent change;

        /// <summary>
        /// Fires after page changing
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "data", typeof(object), "{total, activePage, pages}")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after page changing")]
        public virtual ComponentAjaxEvent Change
        {
            get
            {
                if (this.change == null)
                {
                    this.change = new ComponentAjaxEvent();
                }
                return this.change;
            }
        }

        private ComponentAjaxEvent beforeChange;

        /// <summary>
        /// Fires before page changing
        /// </summary>
        [ListenerArgument(0, "el", typeof(Button), "this")]
        [ListenerArgument(1, "o", typeof(object), "{start, limit}")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before page changing")]
        public virtual ComponentAjaxEvent BeforeChange
        {
            get
            {
                if (this.beforeChange == null)
                {
                    this.beforeChange = new ComponentAjaxEvent();
                }
                return this.beforeChange;
            }
        }
    }
}