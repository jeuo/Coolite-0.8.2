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
    public class ColumnListeners : ComponentBaseListeners
    {
        private ComponentListener columnMoved;

        /// <summary>
        /// Fires when a column is moved.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnmoved", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a column is moved.")]
        [ListenerArgument(0,"el")]
        [ListenerArgument(1, "oldIndex")]
        [ListenerArgument(2, "newIndex")]
        public virtual ComponentListener ColumnMoved 
        {
            get
            {
                if (this.columnMoved == null)
                {
                    this.columnMoved = new ComponentListener();
                }
                return this.columnMoved;
            }
        }

        private ComponentListener configChanged;

        /// <summary>
        /// Fires when the configuration is changed
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("configchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the configuration is changed.")]
        [ListenerArgument(0, "el")]
        public virtual ComponentListener ConfigChange
        {
            get
            {
                if (this.configChanged == null)
                {
                    this.configChanged = new ComponentListener();
                }
                return this.configChanged;
            }
        }

        private ComponentListener headerChange;

        /// <summary>
        /// Fires when the text of a header changes.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the text of a header changes.")]
        [ListenerArgument(0, "el")]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "newText")]
        public virtual ComponentListener HeaderChange
        {
            get
            {
                if (this.headerChange == null)
                {
                    this.headerChange = new ComponentListener();
                }
                return this.headerChange;
            }
        }

        private ComponentListener hiddenChange;

        /// <summary>
        /// Fires when a column is hidden or "unhidden".
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hiddenchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a column is hidden or 'unhidden'.")]
        [ListenerArgument(0, "el")]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "hidden")]
        public virtual ComponentListener HiddenChange
        {
            get
            {
                if (this.hiddenChange == null)
                {
                    this.hiddenChange = new ComponentListener();
                }
                return this.hiddenChange;
            }
        }

        private ComponentListener widthChange;

        /// <summary>
        /// Fires when the width of a column changes.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("widthchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the width of a column changes.")]
        [ListenerArgument(0, "el")]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "newWidth")]
        public virtual ComponentListener WidthChange
        {
            get
            {
                if (this.widthChange == null)
                {
                    this.widthChange = new ComponentListener();
                }
                return this.widthChange;
            }
        }
    }
}