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
    public class ColumnAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent columnMoved;

        /// <summary>
        /// Fires when a column is moved.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Column), "ColumnModel")]
        [ListenerArgument(1, "oldIndex", typeof(int), "Old index")]
        [ListenerArgument(2, "newIndex", typeof(int), "New index")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnmoved", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a column is moved.")]
        public virtual ComponentAjaxEvent ColumnMoved 
        {
            get
            {
                if (this.columnMoved == null)
                {
                    this.columnMoved = new ComponentAjaxEvent();
                }
                return this.columnMoved;
            }
        }

        private ComponentAjaxEvent configChange;

        /// <summary>
        /// Fires when the configuration is changed
        /// </summary>
        [ListenerArgument(0, "el", typeof(Column), "ColumnModel")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("configchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the configuration is changed.")]
        public virtual ComponentAjaxEvent ConfigChange
        {
            get
            {
                if (this.configChange == null)
                {
                    this.configChange = new ComponentAjaxEvent();
                }
                return this.configChange;
            }
        }

        private ComponentAjaxEvent headerChange;

        /// <summary>
        /// Fires when the text of a header changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Column), "ColumnModel")]
        [ListenerArgument(1, "columnIndex", typeof(int), "The column index")]
        [ListenerArgument(2, "newText", typeof(string), "The new header text")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text of a header changes.")]
        public virtual ComponentAjaxEvent HeaderChange
        {
            get
            {
                if (this.headerChange == null)
                {
                    this.headerChange = new ComponentAjaxEvent();
                }
                return this.headerChange;
            }
        }

        private ComponentAjaxEvent hiddenChange;

        /// <summary>
        /// Fires when a column is hidden or "unhidden".
        /// </summary>
        [ListenerArgument(0, "el", typeof(Column), "ColumnModel")]
        [ListenerArgument(1, "columnIndex", typeof(int), "The column index")]
        [ListenerArgument(2, "hidden", typeof(bool), "true if hidden, false otherwise")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hiddenchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a column is hidden or 'unhidden'.")]
        public virtual ComponentAjaxEvent HiddenChange
        {
            get
            {
                if (this.hiddenChange == null)
                {
                    this.hiddenChange = new ComponentAjaxEvent();
                }
                return this.hiddenChange;
            }
        }

        private ComponentAjaxEvent widthChange;

        /// <summary>
        /// Fires when the width of a column changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Column), "ColumnModel")]
        [ListenerArgument(1, "columnIndex", typeof(int), "The column index")]
        [ListenerArgument(2, "newWidth", typeof(int), "The new width")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("widthchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the width of a column changes.")]
        public virtual ComponentAjaxEvent WidthChange
        {
            get
            {
                if (this.widthChange == null)
                {
                    this.widthChange = new ComponentAjaxEvent();
                }
                return this.widthChange;
            }
        }
    }
}