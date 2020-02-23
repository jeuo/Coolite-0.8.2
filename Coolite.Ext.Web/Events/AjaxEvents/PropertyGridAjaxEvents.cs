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
    public class PropertyGridAjaxEvents : GridPanelAjaxEvents
    {
        private ComponentAjaxEvent beforePropertyChange;

        /// <summary>
        /// Fires before a property value changes. Handlers can return false to cancel the property change (this will internally call Ext.data.Record.reject on the property's record).
        /// </summary>
        [ListenerArgument(0, "source")]
        [ListenerArgument(1, "recordId")]
        [ListenerArgument(2, "value")]
        [ListenerArgument(3, "oldValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforepropertychange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a property value changes. Handlers can return false to cancel the property change (this will internally call Ext.data.Record.reject on the property's record).")]
        public virtual ComponentAjaxEvent BeforePropertyChange
        {
            get
            {
                if (this.beforePropertyChange == null)
                {
                    this.beforePropertyChange = new ComponentAjaxEvent();
                }
                return this.beforePropertyChange;
            }
        }

        private ComponentAjaxEvent propertyChange;

        /// <summary>
        /// Fires after a property value has changed.
        /// </summary>
        [ListenerArgument(0, "source")]
        [ListenerArgument(1, "recordId")]
        [ListenerArgument(2, "value")]
        [ListenerArgument(3, "oldValue")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("propertychange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a property value has changed.")]
        public virtual ComponentAjaxEvent PropertyChange
        {
            get
            {
                if (this.propertyChange == null)
                {
                    this.propertyChange = new ComponentAjaxEvent();
                }
                return this.propertyChange;
            }
        }
    }
}