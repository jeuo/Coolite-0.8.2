/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Coolite.Utilities
{
    //public static class TypeUtils
    //{
    //    /// <summary>
    //    /// Get a Property value from a Type. 
    //    /// </summary>
    //    /// <typeparam name="X">The Type</typeparam>
    //    /// <typeparam name="T">The Type of Property to return</typeparam>
    //    /// <param name="self">The Type of object to extend</param>
    //    /// <param name="name">The name of the Property to get.</param>
    //    /// <returns>A Function delegate</returns>
    //    public static Func<X, T> GetProperty<X, T>(this Type self, string name)
    //    {
    //        ParameterExpression parameter = Expression.Parameter(typeof(X), "obj");
    //        MemberExpression property = Expression.Property(parameter, name);
    //        return (Func<X, T>)Expression.Lambda(typeof(Func<X, T>), property, parameter).Compile();
    //    }
    //}

    public class ReflectionUtils
    {
        /// <summary>
        /// Get a Property value from a Type. 
        /// </summary>
        /// <typeparam name="X">The Type</typeparam>
        /// <typeparam name="T">The Type of Property to return</typeparam>
        /// <param name="name">The name of the Property to get.</param>
        /// <returns>A Function delegate</returns>
        // public static Func<X, T> GetProperty<X, T>(this Type self, string name)
        public static Func<X, T> GetProperty<X, T>(string name)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(X), "obj");
            MemberExpression property = Expression.Property(parameter, name);
            return (Func<X, T>)Expression.Lambda(typeof(Func<X, T>), property, parameter).Compile();
        }

        public static object GetDefaultValue(PropertyInfo property)
        {
            object[] att = property.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            if (att.Length > 0)
                return ((DefaultValueAttribute)att[0]).Value;
            else
                return string.Empty;
        }

        public static bool IsTypeOf(Object obj, Type type)
        {
            return IsTypeOf(obj, type.FullName, false);
        }

        public static bool IsTypeOf(Object obj, Type type, bool shallow)
        {
            return IsTypeOf(obj, type.FullName, shallow);
        }

        public static bool IsTypeOf(Object obj, string typeFullName)
        {
            return IsTypeOf(obj, typeFullName, false);
        }

        public static bool IsTypeOf(Object obj, string typeFullName, bool shallow)
        {
            if (obj != null)
            {
                if (shallow)
                {
                    return obj.GetType().FullName.Equals(typeFullName);
                }
                else
                {
                    Type type = obj.GetType();
                    string fullName = type.FullName;

                    while (!fullName.Equals("System.Object"))
                    {
                        if (fullName.Equals(typeFullName))
                        {
                            return true;
                        }
                        type = type.BaseType;
                        fullName = type.FullName;
                    }
                }
            }
            return false;
        }

        public static bool IsInTypeOf(Control control, Type type)
        {
            return IsInTypeOf(control, type.FullName);
        }

        public static bool IsInTypeOf(Control control, string typeFullName)
        {
            Control temp = GetTypeOfParent(control, typeFullName);

            return (temp != null);
        }

        public static Control GetTypeOfParent(Control control, Type type)
        {
            return GetTypeOfParent(control, type.FullName);
        }

        public static Control GetTypeOfParent(Control control, string typeFullName)
        {
            for (Control parent = control.Parent; parent != null; parent = parent.Parent)
            {
                if (ReflectionUtils.IsTypeOf(parent, typeFullName))
                {
                    return parent;
                }
            }
            return null;
        }
    }
}