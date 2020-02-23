/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Globalization;
using System.Reflection;

namespace Coolite.Ext.Web
{
    public class ChainingPropertyInfo : PropertyInfo
    {
        private readonly PropertyInfo _root;
        protected PropertyInfo Root { get { return _root; } }
        
        public ChainingPropertyInfo(PropertyInfo root)
        {
            _root = root;
        }

        public override bool Equals(object obj)
        {
            return Root.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Root.GetHashCode();
        }

        public virtual object GetValue(object component)
        {
            return Root.GetValue(component, null);
        }
        
        public override object GetValue(object component, object[] index)
        {
            return Root.GetValue(component, index);
        }

        public override string Name
        {
            get
            {
                return Root.Name;
            }
        }

        public override string ToString()
        {
            return Root.ToString();
        }

        public override Type DeclaringType
        {
            get
            {
                return Root.DeclaringType;
            }
        }

        public override Type ReflectedType
        {
            get
            {
                return Root.ReflectedType;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return Root.PropertyType;
            }
        }

        public override PropertyAttributes Attributes
        {
            get
            {
                return Root.Attributes;
            }
        }

        public override bool CanRead
        {
            get
            {
                return Root.CanRead;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return Root.CanWrite;
            }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return Root.GetCustomAttributes(inherit);
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            return Root.IsDefined(attributeType, inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return Root.GetCustomAttributes(attributeType, inherit);
        }

        public override ParameterInfo[] GetIndexParameters()
        {
            return GetIndexParameters();
        }

        public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
        {
            return Root.GetValue(obj, invokeAttr, binder, index, culture);
        }

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
        {
            Root.SetValue(obj, value, invokeAttr, binder, index, culture);
        }

        public override MethodInfo[] GetAccessors(bool nonPublic)
        {
            return Root.GetAccessors(nonPublic);
        }

        public override MethodInfo GetGetMethod(bool nonPublic)
        {
            return Root.GetGetMethod(nonPublic);
        }

        public override MethodInfo GetSetMethod(bool nonPublic)
        {
            return Root.GetSetMethod(nonPublic);
        }
    }
}