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
    public class RowSelectionModelAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent beforeRowSelect;

        /// <summary>
        /// Fires when a row is being selected, return false to cancel.
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowSelectionModel), "SelectionModel")]
        [ListenerArgument(1, "rowIndex", typeof(int), "The selected index")]
        [ListenerArgument(2, "keepExisting", typeof(bool), "False if other selections will be cleared")]
        [ListenerArgument(3, "record", typeof(object), "The record to be selected")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerowselect", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is being selected, return false to cancel.")]
        public virtual ComponentAjaxEvent BeforeRowSelect
        {
            get
            {
                if (this.beforeRowSelect == null)
                {
                    this.beforeRowSelect = new ComponentAjaxEvent();
                }
                return this.beforeRowSelect;
            }
        }

        private ComponentAjaxEvent rowDeselect;

        /// <summary>
        /// Fires when a row is deselected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowSelectionModel), "SelectionModel")]
        [ListenerArgument(1, "rowIndex", typeof(int), "The selected index")]
        [ListenerArgument(2, "record", typeof(object), "The selected record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowdeselect", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is deselected.")]
        public virtual ComponentAjaxEvent RowDeselect
        {
            get
            {
                if (this.rowDeselect == null)
                {
                    this.rowDeselect = new ComponentAjaxEvent();
                }
                return this.rowDeselect;
            }
        }

        private ComponentAjaxEvent rowSelect;

        /// <summary>
        /// Fires when a row is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowSelectionModel), "SelectionModel")]
        [ListenerArgument(1, "rowIndex", typeof(int), "The selected index")]
        [ListenerArgument(2, "record", typeof(object), "The selected record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowselect", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is selected.")]
        public virtual ComponentAjaxEvent RowSelect
        {
            get
            {
                if (this.rowSelect == null)
                {
                    this.rowSelect = new ComponentAjaxEvent();
                }
                return this.rowSelect;
            }
        }

        private ComponentAjaxEvent selectionChange;

        /// <summary>
        /// Fires when the selection changes
        /// </summary>
        [ListenerArgument(0, "el", typeof(RowSelectionModel), "SelectionModel")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("selectionchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the selection changes.")]
        public virtual ComponentAjaxEvent SelectionChange
        {
            get
            {
                if (this.selectionChange == null)
                {
                    this.selectionChange = new ComponentAjaxEvent();
                }
                return this.selectionChange;
            }
        }
    }
}