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
    public class ComboBoxAjaxEvents : TriggerFieldAjaxEvents
    {
        private ComponentAjaxEvent beforeQuery;

        /// <summary>
        /// Fires before all queries are processed. Return false to cancel the query or set the queryEvent's cancel property to true.
        /// </summary>
        [ListenerArgument(0, "queryEvent", typeof(object), "An object that includes combo (This combo box), query (The query), forceAll (True to force 'all' query) and cancel (Set to true to cancel the query).")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforequery", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before all queries are processed. Return false to cancel the query or set the queryEvent's cancel property to true.")]
        public virtual ComponentAjaxEvent BeforeQuery 
        {
            get
            {
                if (this.beforeQuery == null)
                {
                    this.beforeQuery = new ComponentAjaxEvent();
                }
                return this.beforeQuery;
            }
        }

        private ComponentAjaxEvent beforeSelect;

        /// <summary>
        /// Fires before a list items is selected. Return false to cancel the selection.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [ListenerArgument(1, "record", typeof(object), "The data record returned from the underlying store")]
        [ListenerArgument(2, "index", typeof(int), "The index of the selected item in the dropdown list")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeselect", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a list items is selected. Return false to cancel the selection.")]
        public virtual ComponentAjaxEvent BeforeSelect
        {
            get
            {
                if (this.beforeSelect == null)
                {
                    this.beforeSelect = new ComponentAjaxEvent();
                }
                return this.beforeSelect;
            }
        }

        private ComponentAjaxEvent collapse;

        /// <summary>
        /// Fires when the dropdown list is collapsed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the dropdown list is collapsed.")]
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

        private ComponentAjaxEvent expand;

        /// <summary>
        /// Fires when the dropdown list is expanded.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the dropdown list is expanded.")]
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

        private ComponentAjaxEvent select;

        /// <summary>
        /// Fires when a list items is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This combo box")]
        [ListenerArgument(1, "record", typeof(object), "The data record returned from the underlying store")]
        [ListenerArgument(2, "index", typeof(int), "The index of the selected item in the dropdown list")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("select", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a list items is selected.")]
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