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
    [TypeConverter(typeof(ListenersConverter))]
    public abstract class ComponentListeners : StateManagedItem
    {
        private static readonly Dictionary<string, List<ListenerPropertyInfo>> propertiesCache = new Dictionary<string, List<ListenerPropertyInfo>>();
        private static readonly object syncObj = new object();

        public virtual void ClearListeners()
        {
            foreach (ListenerTriplet listener in this.Listeners)
            {
                listener.Listener.Clear();
            }
        }

        private List<ListenerPropertyInfo> ListenerProperties
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
                        if (property.PropertyType == typeof(ComponentListener))
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

        private List<ListenerTriplet> listeners;

        public virtual List<ListenerTriplet> Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new List<ListenerTriplet>();
                    foreach (ListenerPropertyInfo property in this.ListenerProperties)
                    {
                        ComponentListener value =  property.PropertyInfo.GetValue(this, null) as ComponentListener;
                        if(value != null)
                        {
                            this.listeners.Add(new ListenerTriplet(property.PropertyInfo.Name, value, property.Attribute));    
                        }
                    }
                }

                return this.listeners;
            }
        }

        internal void InitOwners(Control owner)
        {
            if(owner == null)
            {
                return;
            }

            this.Owner = owner;
            foreach (ListenerTriplet listener in this.Listeners)
            {
                listener.Listener.Owner = owner;
            }

            //owner.Load += new EventHandler(Owner_Load);

            owner.Load += delegate(object sender, EventArgs e)
            {
                this.Owner = owner;
                foreach (ListenerTriplet listener in this.Listeners)
                {
                    listener.Listener.Owner = owner;
                }
            };
        }

        //void Owner_Load(object sender, EventArgs e)
        //{
        //    WebControl owner = (WebControl)sender;
        //    foreach (ListenerTriplet listener in this.Listeners)
        //    {
        //        listener.Listener.Owner = owner;
        //    }
        //}

        public override object SaveViewState()
        {
            List<object> states = new List<object>();
            object baseState = base.SaveViewState();
            if(baseState != null)
            {
                states.Add(new Pair("base", baseState)); 
            }

            foreach (ListenerTriplet triplet in this.Listeners)
            {
                object listenerState = triplet.Listener.SaveViewState();
                if (listenerState != null)
                {
                    states.Add(new Pair(triplet.Name, listenerState));  
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
                    string listenerName = (string)pair.First;
                    object listenerState = pair.Second;

                    if(listenerName == "base")
                    {
                        base.LoadViewState(listenerState);
                    }
                    else
                    {
                        PropertyInfo property = this.GetType().GetProperty(listenerName);

                        if (property == null)
                        {
                            throw new InvalidOperationException(string.Format("Can't find the property '{0}'", listenerName));
                        }

                        ComponentListener componentListener = (ComponentListener)property.GetValue(this, null);
                        if(componentListener != null)
                        {
                            componentListener.LoadViewState(listenerState); 
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

    public class ListenerTriplet
    {
        private readonly string name;
        private readonly ComponentListener listener;
        private readonly ClientConfigAttribute attribute;

        public ListenerTriplet(string name, ComponentListener listener, ClientConfigAttribute attribute)
        {
            this.name = name;
            this.listener = listener;
            this.attribute = attribute;
        }

        public string Name
        {
            get { return name; }
        }

        public ComponentListener Listener
        {
            get { return listener; }
        }

        public ClientConfigAttribute Attribute
        {
            get { return attribute; }
        }
    }

    public class ListenerPropertyInfo
    {
        private readonly PropertyInfo propertyInfo;
        private readonly ClientConfigAttribute attribute;

        public ListenerPropertyInfo(PropertyInfo propertyInfo, ClientConfigAttribute attribute)
        {
            this.propertyInfo = propertyInfo;
            this.attribute = attribute;
        }

        public PropertyInfo PropertyInfo
        {
            get { return propertyInfo; }
        }

        public ClientConfigAttribute Attribute
        {
            get { return attribute; }
        }
    }
}