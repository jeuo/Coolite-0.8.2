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
using System.Reflection.Emit;
using System.Threading;

namespace Coolite.Ext.Web
{
    internal class FastAccessor
    {
        static readonly ModuleBuilder moduleBuilder;
        static int counter;
        public static readonly Dictionary<PropertyInfo, PropertyInfo> properties = new Dictionary<PropertyInfo, PropertyInfo>();
        static FastAccessor()
        {
            AssemblyName an = new AssemblyName("Coolite.ComponentModel.dynamic");
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            moduleBuilder = ab.DefineDynamicModule("Coolite.ComponentModel.dynamic.dll");
        }

        internal static bool TryCreatePropertyDescriptor(ref PropertyInfo property)
        {
            try
            {
                if (property == null) return false;
                PropertyInfo origProperty = property;

                lock (properties)
                {
                    PropertyInfo foundBuiltAlready;
                    if (properties.TryGetValue(property, out foundBuiltAlready))
                    {
                        property = foundBuiltAlready;
                        return true;
                    }

                    string name = "_c" + Interlocked.Increment(ref counter).ToString();
                    TypeBuilder tb = moduleBuilder.DefineType(name, TypeAttributes.Sealed | TypeAttributes.NotPublic | TypeAttributes.Class | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoClass | TypeAttributes.Public, typeof(ChainingPropertyInfo));

                    // ctor calls base
                    ConstructorBuilder cb = tb.DefineConstructor(MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[] { typeof(PropertyInfo) });
                    ILGenerator il = cb.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Call, typeof(ChainingPropertyInfo).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(PropertyInfo) }, null));
                    il.Emit(OpCodes.Ret);

                    MethodBuilder mb;
                    MethodInfo baseMethod;
                    if (property.CanRead)
                    {
                        // obtain the implementation that we want to override
                        baseMethod = typeof(ChainingPropertyInfo).GetMethod("GetValue", new Type[] { typeof(object) });
                        // create a new method that accepts an object and returns an object (as per the base)
                        mb = tb.DefineMethod(baseMethod.Name,
                            MethodAttributes.HideBySig | MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final,
                            baseMethod.CallingConvention, baseMethod.ReturnType, new Type[] { typeof(object) });
                        // start writing IL into the method
                        il = mb.GetILGenerator();
                        if (property.DeclaringType.IsValueType)
                        {
                            // upbox the object argument into our known (instance) struct type
                            LocalBuilder lb = il.DeclareLocal(property.DeclaringType);
                            il.Emit(OpCodes.Ldarg_1);
                            il.Emit(OpCodes.Unbox_Any, property.DeclaringType);
                            il.Emit(OpCodes.Stloc_0);
                            il.Emit(OpCodes.Ldloca_S, lb);
                        }
                        else
                        {
                            // cast the object argument into our known class type
                            il.Emit(OpCodes.Ldarg_1);
                            il.Emit(OpCodes.Castclass, property.DeclaringType);
                        }

                        // call the "get" method
                        il.Emit(OpCodes.Callvirt, property.GetGetMethod());

                        if (property.PropertyType.IsValueType)
                        {
                            // box it from the known (value) struct type
                            il.Emit(OpCodes.Box, property.PropertyType);
                        }
                        // return the value
                        il.Emit(OpCodes.Ret);
                        // signal that this method should override the base
                        tb.DefineMethodOverride(mb, baseMethod);
                    }

                    PropertyInfo newDesc = tb.CreateType().GetConstructor(new Type[] { typeof(PropertyInfo) }).Invoke(new object[] { property }) as PropertyInfo;
                    if (newDesc == null)
                    {
                        return false;
                    }
                    property = newDesc;
                    properties.Add(origProperty, property);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}