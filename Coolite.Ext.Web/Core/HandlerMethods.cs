/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;

namespace Coolite.Ext.Web
{
    internal class HandlerMethods
    {
        private AjaxMethod[] staticMethods;
        private AjaxMethod[] instanceMethods;
        private readonly Type handlerType;

        public HandlerMethods(Type handlerType)
        {
            this.handlerType = handlerType;
        }

        internal AjaxMethod[] StaticMethods
        {
            get
            {
                if(this.staticMethods == null)
                {
                    this.staticMethods = this.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static);
                }

                return this.staticMethods;
            }
        }

        internal AjaxMethod[] InstanceMethods
        {
            get
            {
                if (this.instanceMethods == null)
                {
                    this.instanceMethods = this.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                }

                return this.instanceMethods;
            }
        }

        public AjaxMethod GetStaticMethod(string name)
        {
            foreach (AjaxMethod method in this.StaticMethods)
            {
                if(method.Name == name)
                {
                    return method;
                }
            }

            this.staticMethods = null;

            foreach (AjaxMethod method in this.StaticMethods)
            {
                if (method.Name == name)
                {
                    return method;
                }
            }

            return null;
        }

        public AjaxMethod GetInstanceMethod(string name)
        {
            foreach (AjaxMethod method in this.InstanceMethods)
            {
                if (method.Name == name)
                {
                    return method;
                }
            }

            this.instanceMethods = null;

            foreach (AjaxMethod method in this.InstanceMethods)
            {
                if (method.Name == name)
                {
                    return method;
                }
            }

            return null;
        }
        
        private AjaxMethod[] GetMethods(BindingFlags bindingAttrs)
        {
            List<AjaxMethod> list = new List<AjaxMethod>();
            Type tmpType = this.handlerType;

            while (tmpType != null)
            {
                MethodInfo[] methodInfos = tmpType.GetMethods(bindingAttrs);

                foreach (MethodInfo method in methodInfos)
                {
                    object[] attrs = method.GetCustomAttributes(typeof (AjaxMethodAttribute), true);
                
                    if (attrs.Length > 0)
                    {
                        list.Add(new AjaxMethod(method, (AjaxMethodAttribute)attrs[0]));
                    }
                }

                tmpType = tmpType.BaseType;
            }

            return list.ToArray();
        }

        public static string GetMethodName(HttpContext context)
        {
            return context.Request["_methodName_"] ?? "";
        }

        private static bool IsDebugging
        {
            get
            {
                bool result = false;
                if(HttpContext.Current != null)
                {
                    result = HttpContext.Current.IsDebuggingEnabled;
                }

                return result;
            }
        }

        public static HandlerMethods GetHandlerMethods(HttpContext context, string requestPath)
        {
            string path = VirtualPathUtility.ToAbsolute(requestPath);

            string cacheKey = "CooliteHandlerMethods_" + path;
            HandlerMethods handlerMethods = null;
            
            if (!IsDebugging)
            {
                handlerMethods = context.Cache[cacheKey] as HandlerMethods;
            }

            if(handlerMethods == null)
            {
                Type requestedType = BuildManager.GetCompiledType(path);

                if (requestedType == null)
                {
                    requestedType = BuildManager.CreateInstanceFromVirtualPath(path, typeof(System.Web.UI.Page)).GetType();
                }
                handlerMethods = new HandlerMethods(requestedType);

                if (!IsDebugging)
                {
                    PutToCache(path, context, cacheKey, handlerMethods);
                }
            }

            return handlerMethods;
        }

        public static HandlerMethods GetHandlerMethodsByType(HttpContext context, Type type, string requestPath, bool ignoreCache)
        {
            string cacheKey = string.Concat("CooliteHandlerMethods_", requestPath, type.Namespace, type.Name);
            HandlerMethods handlerMethods = null;
            if (!IsDebugging && !ignoreCache)
            {
                handlerMethods = context.Cache[cacheKey] as HandlerMethods;
            }
            if (handlerMethods == null)
            {
                handlerMethods = new HandlerMethods(type);

                if (!IsDebugging && !ignoreCache)
                {
                    if (string.IsNullOrEmpty(requestPath) || !Directory.Exists(requestPath))
                    {
                        context.Cache.Insert(cacheKey, handlerMethods);
                    }
                    else
                    {
                        context.Cache.Insert(cacheKey, handlerMethods, new CacheDependency(context.Server.MapPath(requestPath)));
                    }
                }
            }

            return handlerMethods;
        }

        private static void PutToCache(string path, HttpContext context, string cacheKey, HandlerMethods handlerMethods)
        {
            BuildDependencySet dependencySet = BuildManager.GetCachedBuildDependencySet(context, path);
            if (dependencySet != null)
            {
                IEnumerable virtualPaths = dependencySet.VirtualPaths;
                if (virtualPaths != null)
                {
                    List<string> paths = new List<string>();
                    foreach (string _path in virtualPaths)
                    {
                        paths.Add(context.Server.MapPath(_path));
                    }
                    context.Cache.Insert(cacheKey, handlerMethods, new CacheDependency(paths.ToArray()));
                    return;
                }
            }

            context.Cache.Insert(cacheKey, handlerMethods);
        }
    }
}