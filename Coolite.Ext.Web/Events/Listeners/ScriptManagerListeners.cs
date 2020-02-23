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
    public class ScriptManagerListeners : ComponentListeners
    {
        private ComponentListener documentReady;

        /// <summary>
        /// Fires when the document is ready (before onload and before images are loaded). Can be accessed shorthanded as Ext.onReady().
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the document is ready (before onload and before images are loaded). Can be accessed shorthanded as Ext.onReady().")]
        public virtual ComponentListener DocumentReady
        {
            get
            {
                if (this.documentReady == null)
                {
                    this.documentReady = new ComponentListener();
                }
                return this.documentReady;
            }
        }

        private ComponentListener textResize;

        /// <summary>
        /// Fires when the user changes the active text size. Handler gets called with 2 params, the old size and the new size.
        /// </summary>
        [ListenerArgument(0, "oldSize", typeof(int), "Old text size")]
        [ListenerArgument(1, "newSize", typeof(int), "New text size")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user changes the active text size. Handler gets called with 2 params, the old size and the new size.")]
        public virtual ComponentListener TextResize
        {
            get
            {
                if (this.textResize == null)
                {
                    this.textResize = new ComponentListener();
                }
                return this.textResize;
            }
        }

        private ComponentListener windowResize;

        /// <summary>
        /// Fires when the window is resized and provides resize event buffering (50 milliseconds), passes new viewport width and height to handlers.
        /// </summary>
        [ListenerArgument(0, "width", typeof(int), "New viewport width")]
        [ListenerArgument(1, "height", typeof(int), "New viewport height")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the window is resized and provides resize event buffering (50 milliseconds), passes new viewport width and height to handlers.")]
        public virtual ComponentListener WindowResize
        {
            get
            {
                if (this.windowResize == null)
                {
                    this.windowResize = new ComponentListener();
                }
                return this.windowResize;
            }
        }

        private ComponentListener windowUnload;

        /// <summary>
        /// Fires when the browser window is unloaded. Return 'true' to prompt the message, or 'false' to cancel the unload.
        /// </summary>
        [ListenerArgument(0, "e", typeof(object), "The browser unload event object")]
        [ClientConfig("beforeunload", typeof(ListenerJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the browser window is unloaded. Return 'true' to prompt the message, or 'false' to cancel the unload.")]
        public virtual ComponentListener WindowUnload
        {
            get
            {
                if (this.windowUnload == null)
                {
                    this.windowUnload = new ComponentListener();
                }
                return this.windowUnload;
            }
        }

        private ComponentListener windowScroll;

        /// <summary>
        /// Fires when the browser window is scrolled.
        /// </summary>
        [ListenerArgument(0, "e", typeof(object), "The browser scroll event object")]
        [ListenerArgument(1, "document", typeof(object), "The browser document object")]
        [ListenerArgument(2, "config", typeof(object), "The event configuration object passed to listener")]
        [ClientConfig("scroll", typeof(ListenerJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the browser window is scrolled.")]
        public virtual ComponentListener WindowScroll
        {
            get
            {
                if (this.windowScroll == null)
                {
                    this.windowScroll = new ComponentListener();
                }
                return this.windowScroll;
            }
        }

        private ComponentListener beforeAjaxRequest;

        /// <summary>
        /// Fires before each ajax request
        /// </summary>
        [ListenerArgument(0, "el", typeof(object), "The browser scroll event object")]
        [ListenerArgument(1, "eventType", typeof(object), "Event type")]
        [ListenerArgument(2, "action", typeof(object), "Type of action")]
        [ListenerArgument(3, "extraParams", typeof(object), "Extra parameters of request")]
        [ListenerArgument(4, "o", typeof(object), "Request object")]
        [ClientConfig("beforeajaxrequest", typeof(ListenerJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before each ajax request.")]
        public virtual ComponentListener BeforeAjaxRequest
        {
            get
            {
                if (this.beforeAjaxRequest == null)
                {
                    this.beforeAjaxRequest = new ComponentListener();
                }
                return this.beforeAjaxRequest;
            }
        }

        private ComponentListener ajaxRequestComplete;

        /// <summary>
        /// Fires if the ajax request was successfully completed.
        /// </summary>
        [ListenerArgument(0, "response", typeof(object), "The reponse object")]
        [ListenerArgument(1, "result", typeof(object), "")]
        [ListenerArgument(2, "el", typeof(object), "The browser scroll event object")]
        [ListenerArgument(3, "eventType", typeof(object), "Event type")]
        [ListenerArgument(4, "action", typeof(object), "Type of action")]
        [ListenerArgument(5, "extraParams", typeof(object), "Extra parameters of request")]
        [ListenerArgument(6, "o", typeof(object), "")]
        [ClientConfig("ajaxrequestcomplete", typeof(ListenerJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires if the ajax request was successfully completed.")]
        public virtual ComponentListener AjaxRequestComplete
        {
            get
            {
                if (this.ajaxRequestComplete == null)
                {
                    this.ajaxRequestComplete = new ComponentListener();
                }
                return this.ajaxRequestComplete;
            }
        }

        private ComponentListener ajaxRequestException;

        /// <summary>
        /// Fires if the ajax request was failed.
        /// </summary>
        [ListenerArgument(0, "response", typeof(object), "The reponse object")]
        [ListenerArgument(1, "result", typeof(object), "")]
        [ListenerArgument(2, "el", typeof(object), "The browser scroll event object")]
        [ListenerArgument(3, "eventType", typeof(object), "Event type")]
        [ListenerArgument(4, "action", typeof(object), "Type of action")]
        [ListenerArgument(5, "extraParams", typeof(object), "Extra parameters of request")]
        [ListenerArgument(6, "o", typeof(object), "")]
        [ClientConfig("ajaxrequestexception", typeof(ListenerJsonConverter))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires if the ajax request was failed.")]
        public virtual ComponentListener AjaxRequestException
        {
            get
            {
                if (this.ajaxRequestException == null)
                {
                    this.ajaxRequestException = new ComponentListener();
                }
                return this.ajaxRequestException;
            }
        }
    }
}