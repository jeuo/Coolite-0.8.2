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
    public class ComboBoxListeners : TriggerFieldListeners
    {
        private ComponentListener beforeQuery;

        /// <summary>
        /// Fires before all queries are processed. Return false to cancel the query or set the queryEvent's cancel property to true.
        /// </summary>
        [ListenerArgument(0, "queryEvent", typeof(object), "An object that includes combo (This combo box), query (The query), forceAll (True to force 'all' query) and cancel (Set to true to cancel the query).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforequery", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before all queries are processed. Return false to cancel the query or set the queryEvent's cancel property to true.")]
        public virtual ComponentListener BeforeQuery 
        {
            get
            {
                if (this.beforeQuery == null)
                {
                    this.beforeQuery = new ComponentListener();
                }
                return this.beforeQuery;
            }
        }

        private ComponentListener beforeSelect;

        /// <summary>
        /// Fires before a list items is selected. Return false to cancel the selection.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [ListenerArgument(1, "record", typeof(object), "The data record returned from the underlying store")]
        [ListenerArgument(2, "index", typeof(int), "The index of the selected item in the dropdown list")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeselect", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a list items is selected. Return false to cancel the selection.")]
        public virtual ComponentListener BeforeSelect
        {
            get
            {
                if (this.beforeSelect == null)
                {
                    this.beforeSelect = new ComponentListener();
                }
                return this.beforeSelect;
            }
        }

        private ComponentListener collapse;

        /// <summary>
        /// Fires when the dropdown list is collapsed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the dropdown list is collapsed.")]
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

        private ComponentListener expand;

        /// <summary>
        /// Fires when the dropdown list is expanded.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the dropdown list is expanded.")]
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

        private ComponentListener select;

        /// <summary>
        /// Fires when a list items is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [ListenerArgument(1, "record", typeof(object), "The data record returned from the underlying store")]
        [ListenerArgument(2, "index", typeof(int), "The index of the selected item in the dropdown list")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("select", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a list items is selected.")]
        public virtual ComponentListener Select
        {
            get
            {
                if (this.select == null)
                {
                    this.select = new ComponentListener();
                }
                return this.select;
            }
        }
    }
}