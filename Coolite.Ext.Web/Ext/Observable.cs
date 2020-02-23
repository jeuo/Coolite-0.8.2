/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Abstract base class that provides a common interface for publishing events
    /// </summary>
    [Description("Abstract base class that provides a common interface for publishing events")]
    public abstract class Observable : WebControl, ILazyItems
    {
        /*  ILazyItems
           -----------------------------------------------------------------------------------------------*/

        List<Observable> lazyItems;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Observable> LazyItems
        {
            get
            {
                if (this.lazyItems == null)
                {
                    this.lazyItems = new List<Observable>();
                }
                return this.lazyItems;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual bool SingleItemMode
        {
            get
            {
                return false;
            }
        }
        
        private ConfigItemCollection customConfig;

        [ClientConfig("-", typeof(CustomConfigJsonConverter))]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Collection of custom js config")]
        public virtual ConfigItemCollection CustomConfig
        {
            get
            {
                if (this.customConfig == null)
                {
                    this.customConfig = new ConfigItemCollection();
                }

                return this.customConfig;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [Category("Config Options")]
        [Description("The registered xtype to create. This config option is not used when passing a config object into a constructor. This config option is used only when lazy instantiation is being used, and a child items of a Container is being specified not as a fully instantiated Component, but as a Component config object. The xtype will be looked up at render time up to determine what type of child Component to create.")]
        public virtual string Xtype
        {
            get
            {
                if (this.IsLazy || this.DesignMode)
                {
                    object[] attributes = this.GetType().GetCustomAttributes(typeof(XtypeAttribute), true);
                    if (attributes != null && attributes.Length > 0)
                    {
                        string defaultType = "";
                        string xtype = ((XtypeAttribute)attributes[0]).Name;
                        if (this is Container)
                        {
                            defaultType = ((Container)this).DefaultType;
                        }
                        return (xtype.Equals(defaultType)) ? "" : xtype;
                    }
                }
                return "";
            }
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddEvents(string events)
        {
            string template = "{0}.addEvents({1});";
            this.AddScript(template, this.ClientID, events);
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, JFunction handler)
        {
            this.AddListener(eventName, handler.ToString());
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, JFunction handler, string scope)
        {
            this.AddListener(eventName, handler.ToString(), scope);
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, JFunction handler, string scope, HandlerConfig options)
        {
            this.AddListener(eventName, handler.ToString(), scope, options);
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, string handler)
        {
            string template = "{0}.on(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, eventName.ToLower(), TokenUtils.ParseAndNormalize(handler, this));
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, string handler, string scope)
        {
            string template = "{0}.on(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, eventName.ToLower(), TokenUtils.ParseAndNormalize(handler, this), scope);
        }

        /// <summary>
        /// Appends an event handler to this component
        /// </summary>
        [Description("Appends an event handler to this component")]
        public virtual void AddListener(string eventName, string handler, string scope, HandlerConfig options)
        {
            string template = "{0}.on(\"{1}\",{2},{3},{4});";
            this.AddScript(template, this.ClientID, eventName, TokenUtils.ParseAndNormalize(handler, this), scope, options.ToJsonString());
        }

        /// <summary>
        /// Fires the specified event with the passed parameters (minus the event name)
        /// </summary>
        [Description("Fires the specified event with the passed parameters (minus the event name)")]
        public virtual void FireEvent(string eventName, Dictionary<string, object> args)
        {
            string template = "{0}.fireEvent(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, eventName, JSON.Serialize(args));
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, string handler)
        {
            this.AddListener(eventName, handler);
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, string handler, string scope)
        {
            this.AddListener(eventName, handler, scope);
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, string handler, string scope, HandlerConfig options)
        {
            this.AddListener(eventName, handler, scope, options);
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, JFunction handler)
        {
            this.AddListener(eventName, handler.ToString());
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, JFunction handler, string scope)
        {
            this.AddListener(eventName, handler.ToString(), scope);
        }

        /// <summary>
        /// Appends an event handler to this element (shorthand for addListener)
        /// </summary>
        [Description("Appends an event handler to this element (shorthand for addListener)")]
        public virtual void On(string eventName, JFunction handler, string scope, HandlerConfig options)
        {
            this.AddListener(eventName, handler.ToString(), scope, options);
        }

        /// <summary>
        /// Removes all listeners for this object
        /// </summary>
        [Description("Removes all listeners for this object")]
        public virtual void PurgeListeners()
        {
            string template = "{0}.purgeListeners();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Removes a listener
        /// </summary>
        [Description("Removes a listener")]
        public virtual void RemoveListener(string eventName, string handler)
        {
            string template = "{0}.un(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, eventName.ToLower(), handler);
        }

        /// <summary>
        /// Removes a listener
        /// </summary>
        [Description("Removes a listener")]
        public virtual void RemoveListener(string eventName, string handler, string scope)
        {
            string template = "{0}.un(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, eventName.ToLower(), handler, scope);
        }

        /// <summary>
        /// Resume firing events. (see suspendEvents)
        /// </summary>
        [Description("Resume firing events. (see suspendEvents)")]
        public virtual void ResumeEvents()
        {
            string template = "{0}.resumeEvents();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Suspend the firing of all events. (see resumeEvents)
        /// </summary>
        [Description("Suspend the firing of all events. (see resumeEvents)")]
        public virtual void SuspendEvents()
        {
            string template = "{0}.suspendEvents();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Removes a listener (shorthand for removeListener)
        /// </summary>
        [Description("Removes a listener (shorthand for removeListener)")]
        public virtual void Un(string eventName, string handler)
        {
            this.RemoveListener(eventName, handler);
        }

        /// <summary>
        /// Removes a listener (shorthand for removeListener)
        /// </summary>
        [Description("Removes a listener (shorthand for removeListener)")]
        public virtual void Un(string eventName, string handler, string scope)
        {
            this.RemoveListener(eventName, handler, scope);
        }


        /*  Client Initialization
            -----------------------------------------------------------------------------------------------*/

        protected virtual void OnBeforeClientInit(Observable sender) { }

        protected virtual void OnAfterClientInit(Observable sender) { }

        public delegate void OnBeforeClientInitializedHandler(Observable sender);
        public delegate void OnAfterClientInitializedHandler(Observable sender);

        public event OnBeforeClientInitializedHandler BeforeClientInit;
        public event OnAfterClientInitializedHandler AfterClientInit;

        protected virtual void OnBeforeClientInitHandler()
        {
            if (this.BeforeClientInit != null)
            {
                this.BeforeClientInit(this);
            }
        }

        protected virtual void OnAfterClientInitHandler()
        {
            if (this.AfterClientInit != null)
            {
                this.AfterClientInit(this);
            }
        }

        protected override void OnClientInit()
        {
            this.OnBeforeClientInitHandler();
            base.OnClientInit();
            this.OnAfterClientInitHandler();
        }

        private const string AjaxEventsKey = "AjaxEvents";

        private ComponentAjaxEvents GetAjaxEvents()
        {
            // assumption: server side listeners class should have name 'AjaxEvents'
            PropertyInfo ssl = this.GetType().GetProperty(Observable.AjaxEventsKey);

            if (ssl == null)
            {
                return null;
            }

            return ssl.GetValue(this, null) as ComponentAjaxEvents;
        }

        internal void FireAsyncEvent(string eventName, ParameterCollection extraParams)
        {
            ComponentAjaxEvents ajaxevents = this.GetAjaxEvents();

            if (ajaxevents == null)
            {
                throw new HttpException("The control has no AjaxEvents");
            }

            PropertyInfo eventListenerInfo = ajaxevents.GetType().GetProperty(eventName);
            if (eventListenerInfo.PropertyType != typeof(ComponentAjaxEvent))
            {
                throw new HttpException(string.Format("The control '{1}' does not have an AjaxEvent with the name '{0}'", eventName, this.ClientID));
            }

            ComponentAjaxEvent ajaxevent = eventListenerInfo.GetValue(ajaxevents, null) as ComponentAjaxEvent;
            if (ajaxevent == null || ajaxevent.IsDefault)
            {
                throw new HttpException(string.Format("The control '{1}' does not have an AjaxEvent with the name '{0}' or the handler is absent", eventName, this.ClientID));
            }
            AjaxEventArgs e = new AjaxEventArgs(extraParams);
            ajaxevent.OnEvent(e);
        }

        [ClientConfig("ajaxEvents", JsonMode.Raw)]
        [DefaultValue("")]
        internal string AjaxEventsProxy
        {
            get
            {
                if (this.DesignMode || Ext.IsAjaxRequest)
                {
                    return "";
                }

                ComponentAjaxEvents componentAjaxEvents = this.GetAjaxEvents();

                if (componentAjaxEvents == null)
                {
                    return "";
                }

                StringBuilder sb = new StringBuilder(256);
                sb.Append("{");

                List<AjaxEventTriplet> serverEventsProperties = componentAjaxEvents.AjaxEvents;

                foreach (AjaxEventTriplet triplet in serverEventsProperties)
                {
                    ComponentAjaxEvent ajaxEvent = triplet.AjaxEvent;
                    if (ajaxEvent != null && !ajaxEvent.IsDefault)
                    {
                        string eventName;
                        ClientConfigAttribute config = triplet.Attribute;

                        if (config != null && !string.IsNullOrEmpty(config.Name))
                        {
                            eventName = config.Name;
                        }
                        else
                        {
                            eventName = StringUtils.ToLowerCamelCase(triplet.Name);
                        }

                        string configObject = new ClientConfig().SerializeInternal(ajaxEvent, ajaxEvent.Owner);

                        StringBuilder cfgObj = new StringBuilder(configObject.Length + 64);

                        cfgObj.Append(configObject);
                        cfgObj.Remove(cfgObj.Length - 1, 1);
                        cfgObj.AppendFormat("{0}control:this", configObject.Length > 2 ? "," : "");

                        if (triplet.Name != "Click")
                        {
                            cfgObj.AppendFormat(",action:'{0}'", triplet.Name);
                        }
                        
                        cfgObj.Append("}");

                        ajaxEvent.SetArgumentList(triplet.PropertyInfo);

                        JFunction jFunction = new JFunction(string.Concat("var params=arguments;Coolite.AjaxEvent.confirmRequest(", cfgObj.ToString(), ");"), ajaxEvent.ArgumentList.ToArray());
                        HandlerConfig cfg = ajaxEvent.GetListenerConfig();
                        string scope = string.IsNullOrEmpty(ajaxEvent.Scope) || ajaxEvent.Scope == "this" ? "" : ajaxEvent.Scope;

                        sb.Append(eventName);
                        sb.Append(":{");

                        sb.Append("fn:").Append(jFunction.ToString()).Append(",");

                        if (scope.Length > 0)
                        {
                            sb.Append("scope:").Append(scope).Append(",");    
                        }

                        string cfgStr = cfg.ToJsonString();
                        if(cfgStr != "{}")
                        {
                            sb.Append(StringUtils.Chop(cfgStr));    
                        }
                        
                        if(sb[sb.Length-1] == ',')
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        
                        sb.Append("},");
                    }
                }

                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                sb.Append("}");

                if(sb.Length > 2)
                {
                    return sb.ToString();
                }

                return "";
            }
        }

        private const string ListenersKey = "Listeners";
        private bool eventsInit = false;

        protected override void OnPreRender(EventArgs e)
        {
            if(!Ext.IsAjaxRequest)
            {
                if (!this.eventsInit && (!Ext.IsMicrosoftAjaxRequest || this.IsInUpdatePanelRefresh))
                {
                    this.BeforeClientInit += OnBeforeClientInit;
                    this.AfterClientInit += OnAfterClientInit;
                    this.InitEventsOwner();
                    this.eventsInit = true;
                }
            }

            base.OnPreRender(e);
        }

        protected override void PreRenderAction()
        {
            if (!Ext.IsAjaxRequest && !this.eventsInit && (this.Visible && !Ext.IsMicrosoftAjaxRequest || this.IsInUpdatePanelRefresh))
            {
                this.BeforeClientInit += OnBeforeClientInit;
                this.AfterClientInit += OnAfterClientInit;
                this.InitEventsOwner();
                this.eventsInit = true;
            }

            base.PreRenderAction();
        }

        private void InitEventsOwner()
        {
            PropertyInfo componentListeners = this.GetType().GetProperty(Observable.ListenersKey);

            if (componentListeners != null)
            {
                ComponentListeners listeners = componentListeners.GetValue(this, null) as ComponentListeners;
                List<ListenerTriplet> properties = listeners.Listeners;

                foreach (ListenerTriplet property in properties)
                {
                    ComponentListener listener = property.Listener;
                    if (listener != null)
                    {
                        listener.Owner = this;
                    }
                }
            }

            PropertyInfo ajaxEvents = this.GetType().GetProperty(Observable.AjaxEventsKey);

            if (ajaxEvents != null)
            {
                ComponentAjaxEvents events = ajaxEvents.GetValue(this, null) as ComponentAjaxEvents;
                List<AjaxEventTriplet> properties = events.AjaxEvents;

                foreach (AjaxEventTriplet property in properties)
                {
                    ComponentAjaxEvent ajaxEvent = property.AjaxEvent;
                    if (ajaxEvent != null)
                    {
                        ajaxEvent.Owner = this;
                        ajaxEvent.ExtraParams.Owner = this;
                        foreach (Parameter param in ajaxEvent.ExtraParams)
                        {
                            param.Owner = this;
                        }
                    }

                    if(!ajaxEvent.IsDefault)
                    {
                        this.ForceIDRendering = true;
                    }
                }
            }
        }
    }
}
