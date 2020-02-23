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
    public class RowExpanderListeners : ComponentBaseListeners
    {
        private ComponentListener beforeExpand;

        /// <summary>
        /// Fires before a row expand
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row expand")]
        public virtual ComponentListener BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentListener();
                }
                return this.beforeExpand;
            }
        }

        private ComponentListener expand;

        /// <summary>
        /// Fires afyter a row expand
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires afyter a row expand")]
        public virtual ComponentListener Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentListener();
                }
                return this.expand;
            }
        }

        private ComponentListener beforeCollapse;

        /// <summary>
        /// Fires before a row collapse
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row collapse")]
        public virtual ComponentListener BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentListener();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentListener collapse;

        /// <summary>
        /// Fires after a row collapse
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowExpander), "this")]
        [ListenerArgument(1, "record", typeof(object))]
        [ListenerArgument(2, "body", typeof(object))]
        [ListenerArgument(3, "rowIndex", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a row collapse")]
        public virtual ComponentListener Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentListener();
                }
                return this.collapse;
            }
        }
    }
}