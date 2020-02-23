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
    public class PropertyGridListeners : GridPanelListeners
    {
        private ComponentListener beforePropertyChange;

        /// <summary>
        /// Fires before a property value changes. Handlers can return false to cancel the property change (this will internally call Ext.data.Record.reject on the property's record).
        /// </summary>
        [ListenerArgument(0, "source")]
        [ListenerArgument(1, "recordId")]
        [ListenerArgument(2, "value")]
        [ListenerArgument(3, "oldValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforepropertychange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a property value changes. Handlers can return false to cancel the property change (this will internally call Ext.data.Record.reject on the property's record).")]
        public virtual ComponentListener BeforePropertyChange
        {
            get
            {
                if (this.beforePropertyChange == null)
                {
                    this.beforePropertyChange = new ComponentListener();
                }
                return this.beforePropertyChange;
            }
        }

        private ComponentListener propertyChange;

        /// <summary>
        /// Fires after a property value has changed.
        /// </summary>
        [ListenerArgument(0, "source")]
        [ListenerArgument(1, "recordId")]
        [ListenerArgument(2, "value")]
        [ListenerArgument(3, "oldValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("propertychange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a property value has changed.")]
        public virtual ComponentListener PropertyChange
        {
            get
            {
                if (this.propertyChange == null)
                {
                    this.propertyChange = new ComponentListener();
                }
                return this.propertyChange;
            }
        }

        private ComponentListener beforeSave;

        /// <summary>
        /// Fires before ajax save.
        /// </summary>
        [ListenerArgument(0, "el", typeof(PropertyGrid))]
        [ListenerArgument(1, "options")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforesave", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before ajax save.")]
        public virtual ComponentListener BeforeSave
        {
            get
            {
                if (this.beforeSave == null)
                {
                    this.beforeSave = new ComponentListener();
                }
                return this.beforeSave;
            }
        }

        private ComponentListener save;

        /// <summary>
        /// Fires after successful ajax save.
        /// </summary>
        [ListenerArgument(0, "el", typeof(PropertyGrid))]
        [ListenerArgument(1, "response")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("save", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after successful ajax save.")]
        public virtual ComponentListener Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new ComponentListener();
                }
                return this.save;
            }
        }

        private ComponentListener saveException;

        /// <summary>
        /// Fires ajax save failed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(PropertyGrid))]
        [ListenerArgument(1, "response")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("saveexception", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires ajax save failed.")]
        public virtual ComponentListener SaveException
        {
            get
            {
                if (this.saveException == null)
                {
                    this.saveException = new ComponentListener();
                }
                return this.saveException;
            }
        }
    }
}