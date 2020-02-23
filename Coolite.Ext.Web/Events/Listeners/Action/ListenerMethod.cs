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
using System.Text;

namespace Coolite.Ext.Web
{
    public abstract class ListenerMethod : ListenerAction
    {
        public static ActionMethodArgumentAttribute GetActionMethodArgumentAttribute(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(typeof(ActionMethodArgumentAttribute), true);

            if (attrs.Length == 1)
            {
                return (ActionMethodArgumentAttribute)attrs[0];
            }
            return null;
        }

        protected virtual string BuildArgs()
        {
            PropertyInfo[] result = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            SortedList<int,PropertyInfo> argsProperties = new SortedList<int, PropertyInfo>();

            foreach (PropertyInfo property in result)
            {
                ActionMethodArgumentAttribute attr = GetActionMethodArgumentAttribute(property);

                if (attr != null)
                {
                    argsProperties.Add(attr.Position, property);
                }
            }

            string[] args = new string[argsProperties.Count];
            int lastNoneUnDefined = -1;
            foreach (KeyValuePair<int, PropertyInfo> argsProperty in argsProperties)
            {
                object obj = argsProperty.Value.GetValue(this, null);
                if(obj == null)
                {
                    args[argsProperty.Key] = "undefined";
                    continue;
                }

                /// TODO: need to think about more flexiable arguments
                string value = obj.ToString();
                if(obj is bool)
                {
                    value = value.ToLower();
                }
               
                args[argsProperty.Key] = value;

                if(value != "undefined")
                {
                    lastNoneUnDefined = argsProperty.Key;
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= lastNoneUnDefined; i++)
            {
                sb.Append(args[i]).Append(",");
            }

            if(sb.Length > 0 && sb[sb.Length-1] == ',')
            {
                sb.Remove(sb.Length-1, 1);
            }
            
            return sb.ToString();
        }

        public override string GetScript()
        {
            if(this.IsDefault)
            {
                return "";
            }

            this.CheckControlTypeIsAcceptable();

            return string.Concat(this.Selector, ".", this.Name, "(", this.BuildArgs(), ");");
        }
    }

    public class ActionMethodArgumentAttribute : Attribute
    {
        private readonly int position;

        public ActionMethodArgumentAttribute(int position)
        {
            this.position = position;
        }

        public int Position
        {
            get { return position; }
        }
    }
}
