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
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [TypeConverter(typeof(AjaxEventsConverter))]
    public class ComponentAjaxEvents : StateManagedItem 
    {
        private static readonly Dictionary<string, List<ListenerPropertyInfo>> propertiesCache = new Dictionary<string, List<ListenerPropertyInfo>>();
        private static readonly object syncObj = new object();

        public virtual void ClearAjaxEvents()
        {
            foreach (AjaxEventTriplet ajaxEvent in this.AjaxEvents)
            {
                ajaxEvent.AjaxEvent.Clear();
            }
        }

        private List<ListenerPropertyInfo> AjaxEventProperties
        {
            get
            {
                string fullName = this.GetType().FullName;
                if (propertiesCache.ContainsKey(fullName))
                {
                    return propertiesCache[fullName];
                }

                lock (syncObj)
                {
                    if (propertiesCache.ContainsKey(fullName))
                    {
                        return propertiesCache[fullName];
                    }

                    List<ListenerPropertyInfo> list = new List<ListenerPropertyInfo>();
                    PropertyInfo[] result = this.GetType().GetProperties();
                    foreach (PropertyInfo property in result)
                    {
                        if (property.PropertyType == typeof(ComponentAjaxEvent))
                        {
                            ClientConfigAttribute config = ClientConfig.GetClientConfigAttribute(property);
                            list.Add(new ListenerPropertyInfo(property, config));
                        }
                    }

                    propertiesCache.Add(fullName, list);

                    return list;
                }
            }
        }

        private List<AjaxEventTriplet> ajaxEvents;
        public virtual List<AjaxEventTriplet> AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new List<AjaxEventTriplet>();
                    foreach (ListenerPropertyInfo property in this.AjaxEventProperties)
                    {
                        ComponentAjaxEvent value = property.PropertyInfo.GetValue(this, null) as ComponentAjaxEvent;
                        if (value != null)
                        {
                            this.ajaxEvents.Add(new AjaxEventTriplet(property.PropertyInfo.Name, value, property.Attribute, property.PropertyInfo));
                        }
                    }
                }

                return this.ajaxEvents;
            }
        }

        internal void InitOwners(WebControl owner)
        {
            if(owner != null)
            {
                owner.Load += Owner_Load;
            }
            
        }

        void Owner_Load(object sender, EventArgs e)
        {
            WebControl owner = (WebControl)sender;
            foreach (AjaxEventTriplet ajaxEvent in this.AjaxEvents)
            {
                ComponentAjaxEvent ae = ajaxEvent.AjaxEvent;
                ae.Owner = owner;
                ae.ExtraParams.Owner = owner;
                foreach (Parameter param in ae.ExtraParams)
                {
                    param.Owner = owner;
                }
            }
        }

        public override object SaveViewState()
        {
            List<object> states = new List<object>();
            object baseState = base.SaveViewState();
            if(baseState != null)
            {
                states.Add(new Pair("base", baseState)); 
            }

            foreach (AjaxEventTriplet triplet in this.AjaxEvents)
            {
                object ajaxEventState = triplet.AjaxEvent.SaveViewState();
                if (ajaxEventState != null)
                {
                    states.Add(new Pair(triplet.Name, ajaxEventState));  
                }
            }

            return states.Count == 0 ? null : states.ToArray();
        }

        public override void LoadViewState(object state)
        {
            object[] states = state as object[];

            if (states != null)
            {
                foreach (Pair pair in states)
                {
                    string ajaxEventName = (string)pair.First;
                    object ajaxEventState = pair.Second;

                    if(ajaxEventName == "base")
                    {
                        base.LoadViewState(ajaxEventState);
                    }
                    else
                    {
                        PropertyInfo property = this.GetType().GetProperty(ajaxEventName);

                        if (property == null)
                        {
                            throw new InvalidOperationException(string.Format("Can't find the property '{0}'", ajaxEventName));
                        }

                        ComponentAjaxEvent componentAjaxEvent = (ComponentAjaxEvent)property.GetValue(this, null);
                        if(componentAjaxEvent != null)
                        {
                            componentAjaxEvent.LoadViewState(ajaxEventState); 
                        }
                    }
                }
            }
            else
            {
                base.LoadViewState(state);  
            }
        }
    }

    public class AjaxEventTriplet
    {
        private readonly string name;
        private readonly ComponentAjaxEvent ajaxEvent;
        private readonly ClientConfigAttribute attribute;
        private readonly PropertyInfo propertyInfo;

        public AjaxEventTriplet(string name, ComponentAjaxEvent ajaxEvent, ClientConfigAttribute attribute, PropertyInfo propertyInfo)
        {
            this.name = name;
            this.ajaxEvent = ajaxEvent;
            this.attribute = attribute;
            this.propertyInfo = propertyInfo;
        }

        public string Name
        {
            get { return name; }
        }

        public ComponentAjaxEvent AjaxEvent
        {
            get { return ajaxEvent; }
        }

        public ClientConfigAttribute Attribute
        {
            get { return attribute; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return propertyInfo; }
        }
    }
}
