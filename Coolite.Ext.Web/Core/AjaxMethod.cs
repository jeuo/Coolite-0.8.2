/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    internal class AjaxMethod
    {
        private readonly MethodInfo method;
        private ParameterInfo[] methodParams;
        private string name;
        private readonly AjaxMethodAttribute attribute;
        private string controlID;

        public AjaxMethod(MethodInfo method, AjaxMethodAttribute attribute)
        {
            this.method = method;
            this.attribute = attribute;
        }

        public MethodInfo Method
        {
            get { return method; }
        }

        public ParameterInfo[] Params
        {
            get
            {
                if (this.methodParams == null)
                {
                    this.methodParams = method.GetParameters();
                }
                return methodParams;
            }
        }

        public string Name
        {
            get
            {
                if (this.name == null)
                {
                    this.name = this.Method.Name;
                }
                return name;
            }
        }

        public string ControlID
        {
            get
            {
                return this.controlID;
            }
            set
            {
                this.controlID = value;
            }
        }

        public AjaxMethodAttribute Attribute
        {
            get { return attribute; }
        }

        public object Invoke()
        {
            return this.Invoke(null, HttpContext.Current, null);
        }

        public object Invoke(object target)
        {
            return this.Invoke(target, HttpContext.Current, null);
        }

        public object Invoke(object target, ParameterCollection args)
        {
            return this.Invoke(target, HttpContext.Current, args);
        }

        public object Invoke(object target, HttpContext context, ParameterCollection args)
        {
            object[] parameters = new object[this.Params.Length];
            int index = 0;

            foreach (ParameterInfo param in this.Params)
            {
                string paramValue = args != null ? args[param.Name] : context.Request[param.Name];

                if (paramValue == null)
                {
                    throw new ArgumentException(string.Format("The parameter '{0}' is null", param.Name));
                }

                if (param.ParameterType == typeof(string))
                {
                    parameters[index++] = paramValue;                    
                }
                else
                {
                    switch(param.ParameterType.Name)
                    {
                        case "Guid":
                            parameters[index++] = new Guid(paramValue);
                            break;
                        default:
                            parameters[index++] = JSON.Deserialize(paramValue, param.ParameterType);
                            break;
                    }
                }
            }

            return method.Invoke(target, parameters);
        }

        internal static bool IsStaticMethodRequest(HttpRequest request)
        {
            string[] values = request.Headers.GetValues("X-Coolite");
            if (values != null)
            {
                foreach (string value in values)
                {
                    if (value.ToLower().Contains("staticmethod=true"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void GenerateProxy(StringBuilder sb, string controlID)
        {
            sb.Append(this.Name);
            sb.Append(":function(");
            
            foreach (ParameterInfo parameterInfo in this.Params)
            {
                sb.Append(parameterInfo.Name);
                sb.Append(",");
            }
            sb.Append("config");
            sb.Append("){");
            sb.Append("Coolite.AjaxMethod.request(\"");
            sb.Append(this.Name);
            sb.Append("\",Ext.apply(config || {}, {");

            int index = 0;
            bool needComma = false;

            if (this.Params.Length>0)
            {
                sb.Append("params:{");
                foreach (ParameterInfo parameterInfo in this.Params)
                {
                    sb.Append(parameterInfo.Name);
                    sb.Append(":");
                    sb.AppendFormat(parameterInfo.Name);
                    index++;
                    if(index < this.Params.Length)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("}");
                needComma = true;
            }

            if(this.Method.IsStatic)
            {
                sb.Append(needComma ? "," : "");
                sb.Append("specifier:\"static\"");
                needComma = true;
            }
            
            if(!string.IsNullOrEmpty(controlID))
            {
                sb.Append(needComma ? "," : "");
                sb.AppendFormat("control:\"{0}\"", controlID);
                needComma = true;
            }

            if(this.Attribute.Method == HttpMethod.GET)
            {
                sb.Append(needComma ? "," : "");
                sb.Append("method:\"GET\"");
                needComma = true;
            }

            if (this.Attribute.ShowMask)
            {
                sb.Append(needComma ? "," : "");
                sb.Append("eventMask:{showMask:true");
                
                if(!string.IsNullOrEmpty(this.Attribute.Msg))
                {
                    sb.Append(",msg:").Append(JSON.Serialize(this.Attribute.Msg));
                }

                if (!string.IsNullOrEmpty(this.Attribute.MsgCls))
                {
                    sb.Append(",msgCls:").Append(JSON.Serialize(this.Attribute.MsgCls));
                }

                if (!string.IsNullOrEmpty(this.Attribute.CustomTarget))
                {
                    this.Attribute.Target = MaskTarget.CustomTarget;
                }

                if (this.Attribute.Target != MaskTarget.Page)
                {
                    sb.Append(",target:").Append(JSON.Serialize(this.Attribute.Target.ToString().ToLower()));
                }

                if (this.Attribute.Target == MaskTarget.CustomTarget && !string.IsNullOrEmpty(this.Attribute.CustomTarget))
                {
                    ScriptManager sm = null;

                    if (HttpContext.Current != null)
                    {
                        sm = ScriptManager.GetInstance(HttpContext.Current);
                    }

                    string script = TokenUtils.ReplaceRawToken((sm != null) ? TokenUtils.ParseTokens(this.Attribute.CustomTarget, sm) : TokenUtils.ParseAndNormalize(this.Attribute.CustomTarget));

                    sb.Append(",customTarget:").Append(script);

                    //sb.Append(",customTarget:").Append(JSON.Serialize(this.Attribute.CustomTarget));
                }

                sb.Append("}");
                needComma = true;
            }

            if (this.Attribute.Type == AjaxEventType.Load)
            {
                sb.Append(needComma ? "," : "");
                sb.Append("type:\"load\"");
                needComma = true;
            }

            if (this.Attribute.ViewStateMode != ViewStateMode.Default)
            {
                sb.Append(needComma ? "," : "");
                sb.AppendFormat("viewStateMode:\"{0}\"", this.Attribute.ViewStateMode.ToString().ToLowerInvariant());
                needComma = true;
            }

            if (this.Attribute.Timeout != 30000)
            {
                sb.Append(needComma ? "," : "");
                sb.AppendFormat("timeout:{0}", this.Attribute.Timeout);
                needComma = true;
            }

            if (!string.IsNullOrEmpty(this.Attribute.SuccessFn))
            {
                sb.Append(needComma ? "," : "");
                sb.AppendFormat("success:{0}", this.Attribute.SuccessFn);
                needComma = true;
            }

            if (!string.IsNullOrEmpty(this.Attribute.FailureFn))
            {
                sb.Append(needComma ? "," : "");
                sb.AppendFormat("failure:{0}", this.Attribute.FailureFn);
            }
            
            sb.Append("}));}");
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AjaxMethodAttribute : Attribute
    {
        private string failureFn;
        private HttpMethod method = HttpMethod.POST;
        private ClientProxy proxyCreation;
        private string successFn;
        private AjaxEventType type = AjaxEventType.Submit;
        private string msg;
        private string msgCls;
        private MaskTarget target = MaskTarget.Page;
        private string customTarget;
        private bool showMask;
        private ViewStateMode viewStateMode = ViewStateMode.Default;
        private string ajaxMethodNamespace;
        private int timeout = 30000;
        
        public string FailureFn
        {
            get { return this.failureFn; }
            set { this.failureFn = value; }
        }

        public HttpMethod Method
        {
            get { return this.method; }
            set { this.method = value; }
        }

        /// <summary>
        /// The timeout in milliseconds to be used for requests. (defaults to 30000)
        /// </summary>
        public int Timeout
        {
            get
            {
                return this.timeout;
            }
            set
            {
                this.timeout = value;
            }
        }

        public ClientProxy ClientProxy
        {
            get
            {
                if (this.proxyCreation == ClientProxy.Default)
                {
                    if (HttpContext.Current != null)
                    {
                        ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);
                        return sm.AjaxMethodProxy;
                    }
                    return ClientProxy.Default;
                }

                return this.proxyCreation;
            }
            set { proxyCreation = value; }
        }

        public string SuccessFn
        {
            get { return successFn; }
            set { successFn = value; }
        }

        public AjaxEventType Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool ShowMask
        {
            get
            {
                return this.showMask;
            }
            set
            {
                this.showMask = value;
            }
        }

        public string Msg
        {
            get
            {
                return this.msg;
            }
            set
            {
                this.msg = value;
            }
        }

        public string MsgCls
        {
            get
            {
                return this.msgCls;
            }
            set
            {
                this.msgCls = value;
            }
        }

        public MaskTarget Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.target = value;
            }
        }

        public string CustomTarget
        {
            get
            {
                return this.customTarget;
            }
            set
            {
                this.customTarget = value;
            }
        }

        public ViewStateMode ViewStateMode
        {
            get
            {
                if (this.viewStateMode == ViewStateMode.Default)
                {
                    if(HttpContext.Current != null)
                    {
                        ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);
                        if(sm == null)
                        {
                            return ViewStateMode.Default;
                        }
                        return sm.AjaxViewStateMode;
                    }
                    return ViewStateMode.Default;
                }
                else
                {
                    return this.viewStateMode;    
                }
            }
            set
            {
                this.viewStateMode = value;
            }
        }

        public string Namespace
        {
            get
            {
                if (string.IsNullOrEmpty(this.ajaxMethodNamespace))
                {
                    string defaultAjaxMethodNamespace = "Coolite.AjaxMethods";

                    if (HttpContext.Current != null)
                    {
                        ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);
                        
                        if (sm == null)
                        {
                            return defaultAjaxMethodNamespace;
                        }
                        
                        string smValue = sm.AjaxMethodNamespace;
                        
                        return string.IsNullOrEmpty(smValue) ? defaultAjaxMethodNamespace : smValue;
                    }
                    return defaultAjaxMethodNamespace;
                }
                
                return this.ajaxMethodNamespace;
            }
            set
            {
                this.ajaxMethodNamespace = value;
            }
        }
    }

    public enum ClientProxy
    {
        Default,
        Ignore,
        Include
    }
}