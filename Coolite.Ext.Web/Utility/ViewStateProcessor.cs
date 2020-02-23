/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    class ViewStateProcessor
    {
        private static readonly Dictionary<string, List<PropertyInfo>> propertiesCache = new Dictionary<string, List<PropertyInfo>>();
        private static readonly object syncObj = new object();

        private static List<PropertyInfo> GetProperties(object obj)
        {
            
            string fullName = obj.GetType().FullName;
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

                List<PropertyInfo> list = new List<PropertyInfo>();
                PropertyInfo[] result = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (PropertyInfo property in result)
                {
                    if (property.GetCustomAttributes(typeof(ViewStateMemberAttribute), true).Length == 1)
                    {
                        list.Add(property);
                    }
                }

                propertiesCache.Add(fullName, list);

                return list;
            }
        }

        public static object SaveViewState(object obj)
        {
            List<PropertyInfo> result = GetProperties(obj);
            List<object> states = new List<object>();
            
            foreach (PropertyInfo property in result)
            {
                try
                {
                    object value = property.GetValue(obj, null);
                   
                    if(value == null)
                    {
                        continue;
                    }

                    IStateManager sm = value as IStateManager;
                    if (sm == null)
                    {
                        throw new InvalidOperationException(string.Concat("The property ", property, " in the class ", obj.GetType().Name, " is not IStateManager but marked as ViewStateMember"));
                    }

                    object propertyState = ((IStateManager)value).SaveViewState();

                    if(propertyState != null)
                    {
                        states.Add(new Pair(property.Name, propertyState));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during SaveViewState initialization. "
                                        + property.Name
                                        + " - "
                                        + ex.Message, ex);
                }
            }

            return states.Count == 0 ? null : states.ToArray();
        }

        public static void LoadViewState(object obj, object state)
        {
            object[] states = state as object[];

            if (states != null)
            {
                foreach (Pair pair in states)
                {
                    string propertyName = (string)pair.First;
                    object propertyState = pair.Second;

                    PropertyInfo property = obj.GetType().GetProperty(propertyName);

                    if (property == null)
                    {
                        throw new InvalidOperationException(string.Format("Can't find the property '{0}' in class '{1}'", propertyName, obj.GetType().Name));
                    }

                    object value = property.GetValue(obj, null);
                   
                    IStateManager stateProperty = (IStateManager)value;
                    if (stateProperty != null)
                    {
                        stateProperty.LoadViewState(propertyState);
                    }
                }
            }
        }
    }
}
