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
    public class ScriptManagerAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent documentReady;

        /// <summary>
        /// Fires when the document is ready (before onload and before images are loaded). Can be accessed shorthanded as Ext.onReady().
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the document is ready (before onload and before images are loaded). Can be accessed shorthanded as Ext.onReady().")]
        public virtual ComponentAjaxEvent DocumentReady
        {
            get
            {
                if (this.documentReady == null)
                {
                    this.documentReady = new ComponentAjaxEvent();
                }
                return this.documentReady;
            }
        }

        private ComponentAjaxEvent textResize;

        /// <summary>
        /// Fires when the user changes the active text size. Handler gets called with 2 params, the old size and the new size.
        /// </summary>
        [ListenerArgument(0, "oldSize", typeof(int), "Old text size")]
        [ListenerArgument(1, "newSize", typeof(int), "New text size")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user changes the active text size. Handler gets called with 2 params, the old size and the new size.")]
        public virtual ComponentAjaxEvent TextResize
        {
            get
            {
                if (this.textResize == null)
                {
                    this.textResize = new ComponentAjaxEvent();
                }
                return this.textResize;
            }
        }

        private ComponentAjaxEvent windowResize;

        /// <summary>
        /// Fires when the window is resized and provides resize event buffering (50 milliseconds), passes new viewport width and height to handlers.
        /// </summary>
        [ListenerArgument(0, "width", typeof(int), "New viewport width")]
        [ListenerArgument(1, "height", typeof(int), "New viewport height")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the window is resized and provides resize event buffering (50 milliseconds), passes new viewport width and height to handlers.")]
        public virtual ComponentAjaxEvent WindowResize
        {
            get
            {
                if (this.windowResize == null)
                {
                    this.windowResize = new ComponentAjaxEvent();
                }
                return this.windowResize;
            }
        }

        private ComponentAjaxEvent windowUnload;

        /// <summary>
        /// Fires when the browser window is unloaded. Return 'true' to prompt the message, or 'false' to cancel the unload.
        /// </summary>
        [ListenerArgument(0, "e", typeof(object), "The browser unload event object")]
        [ClientConfig("beforeunload", typeof(AjaxEventJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the browser window is unloaded. Return 'true' to prompt the message, or 'false' to cancel the unload.")]
        public virtual ComponentAjaxEvent WindowUnload
        {
            get
            {
                if (this.windowUnload == null)
                {
                    this.windowUnload = new ComponentAjaxEvent();
                }
                return this.windowUnload;
            }
        }

        private ComponentAjaxEvent windowScroll;

        /// <summary>
        /// Fires when the browser window is scrolled.
        /// </summary>
        [ListenerArgument(0, "e", typeof(object), "The browser scroll event object")]
        [ListenerArgument(0, "document", typeof(object), "The browser document object")]
        [ListenerArgument(0, "config", typeof(object), "The event configuration object passed to listener")]
        [ClientConfig("scroll", typeof(AjaxEventJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the browser window is scrolled.")]
        public virtual ComponentAjaxEvent WindowScroll
        {
            get
            {
                if (this.windowScroll == null)
                {
                    this.windowScroll = new ComponentAjaxEvent();
                }
                return this.windowScroll;
            }
        }
    }
}