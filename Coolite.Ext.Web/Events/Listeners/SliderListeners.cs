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
    public class SliderListeners : BoxComponentListeners
    {
        private ComponentListener beforeChange;

        /// <summary>
        /// Fires before the slider value is changed. By returning false from an event handler, you can cancel the event and prevent the slider from changing.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "newValue", typeof(int), "The new value which the slider is being changed to.")]
        [ListenerArgument(2, "oldValue", typeof(int), "The old value which the slider was previously.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforechange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the slider value is changed. By returning false from an event handler, you can cancel the event and prevent the slider from changing.")]
        public virtual ComponentListener BeforeChange
        {
            get
            {
                if (this.beforeChange == null)
                {
                    this.beforeChange = new ComponentListener();
                }
                return this.beforeChange;
            }
        }

        private ComponentListener change;

        /// <summary>
        /// Fires when the slider value is changed.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "newValue", typeof(int), "The new value which the slider has been changed to.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("change", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the slider value is changed.")]
        public virtual ComponentListener Change
        {
            get
            {
                if (this.change == null)
                {
                    this.change = new ComponentListener();
                }
                return this.change;
            }
        }

        private ComponentListener changeComplete;

        /// <summary>
        /// Fires when the slider value is changed by the user and any drag operations have completed.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "newValue", typeof(int), "The new value which the slider has been changed to.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("changecomplete", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the slider value is changed by the user and any drag operations have completed.")]
        public virtual ComponentListener ChangeComplete
        {
            get
            {
                if (this.changeComplete == null)
                {
                    this.changeComplete = new ComponentListener();
                }
                return this.changeComplete;
            }
        }

        private ComponentListener drag;

        /// <summary>
        /// Fires continuously during the drag operation while the mouse is moving.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "e", typeof(object), " Ext.EventObject - The event fired from Ext.dd.DragTracker")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("drag", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires continuously during the drag operation while the mouse is moving.")]
        public virtual ComponentListener Drag
        {
            get
            {
                if (this.drag == null)
                {
                    this.drag = new ComponentListener();
                }
                return this.drag;
            }
        }

        private ComponentListener dragEnd;

        /// <summary>
        /// Fires after the drag operation has completed.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "e", typeof(object), " Ext.EventObject - The event fired from Ext.dd.DragTracker")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragend", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the drag operation has completed.")]
        public virtual ComponentListener DragEnd
        {
            get
            {
                if (this.dragEnd == null)
                {
                    this.dragEnd = new ComponentListener();
                }
                return this.dragEnd;
            }
        }

        private ComponentListener dragStart;

        /// <summary>
        /// Fires after a drag operation has started.
        /// </summary>
        [ListenerArgument(0, "slider", typeof(Slider), "this")]
        [ListenerArgument(1, "e", typeof(object), " Ext.EventObject - The event fired from Ext.dd.DragTracker")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragstart", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a drag operation has started.")]
        public virtual ComponentListener DragStart
        {
            get
            {
                if (this.dragStart == null)
                {
                    this.dragStart = new ComponentListener();
                }
                return this.dragStart;
            }
        }
    }
}