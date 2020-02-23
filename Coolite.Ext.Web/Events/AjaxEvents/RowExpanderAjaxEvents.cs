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
    public class RowExpanderAjaxEvents : ContainerAjaxEvents
    {
        private ComponentAjaxEvent beforeExpand;

        /// <summary>
        /// Fires before a row expand
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row expand")]
        public virtual ComponentAjaxEvent BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentAjaxEvent();
                }
                return this.beforeExpand;
            }
        }

        private ComponentAjaxEvent expand;

        /// <summary>
        /// Fires afyter a row expand
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires afyter a row expand")]
        public virtual ComponentAjaxEvent Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentAjaxEvent();
                }
                return this.expand;
            }
        }

        private ComponentAjaxEvent beforeCollapse;

        /// <summary>
        /// Fires before a row collapse
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row collapse")]
        public virtual ComponentAjaxEvent BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentAjaxEvent();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentAjaxEvent collapse;

        /// <summary>
        /// Fires after a row collapse
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a row collapse")]
        public virtual ComponentAjaxEvent Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentAjaxEvent();
                }
                return this.collapse;
            }
        }
    }
}
