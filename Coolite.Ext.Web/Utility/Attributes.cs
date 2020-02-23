/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Reflection;
using System.Text;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContainerStyleAttribute : System.Attribute
    {
        private readonly string style = string.Empty;

        public ContainerStyleAttribute(string style)
        {
            this.style = style;
        }

        public string Style
        {
            get { return this.style; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class XtypeAttribute : System.Attribute
    {
        private readonly string name = string.Empty;

        public XtypeAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LayoutAttribute : System.Attribute
    {
        private readonly string name = string.Empty;

        public LayoutAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ClientConfigAttribute : System.Attribute
    {
        private readonly JsonMode mode = JsonMode.Value;
        private readonly string name = string.Empty;
        private readonly Type jsonConverter;

        public ClientConfigAttribute() { }

        public ClientConfigAttribute(string name)
        {
            this.name = name;
        }

        public ClientConfigAttribute(JsonMode mode)
        {
            this.mode = mode;
        }

        public ClientConfigAttribute(string name, JsonMode mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public ClientConfigAttribute(Type jsonConverter)
        {
            this.name = "";
            if (!jsonConverter.IsSubclassOf(typeof(JsonConverter)))
            {
                throw new ArgumentException("Parameter must be subclass of JsonConverter", "jsonConverter");
            }

            this.jsonConverter = jsonConverter;
            this.mode = JsonMode.Custom;
        }

        public ClientConfigAttribute(string name, Type jsonConverter)
        {
            this.name = name;
            if (!jsonConverter.IsSubclassOf(typeof(JsonConverter)))
            {
                throw new ArgumentException("Parameter must be subclass of JsonConverter", "jsonConverter");
            }

            this.jsonConverter = jsonConverter;
            this.mode = JsonMode.Custom;
        }

        public Type JsonConverter
        {
            get { return jsonConverter; }
        }

        public JsonMode Mode
        {
            get { return this.mode; }
        }

        public string Name
        {
            get { return this.name; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class InstanceOfAttribute : System.Attribute
    {
        private string className;
        public string ClassName
        {
            get { return this.className; }
            set { this.className = value; }
        }

        private string noFormClassName;
        public string NoFormClassName
        {
            get { return this.noFormClassName; }
            set { this.noFormClassName = value; }
        }
    }

    public sealed class ClientStyleAttribute : ResourceAttribute
    {
        private Theme theme;
        public Theme Theme
        {
            get { return this.theme; }
            set { this.theme = value; }
        }

        private bool defaultOnlyStyle;
        public bool DefaultOnlyStyle
        {
            get { return this.defaultOnlyStyle; }
            set { this.defaultOnlyStyle = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class ClientScriptAttribute : ResourceAttribute
    {
        private string webResourceDebug;
        public string WebResourceDebug
        {
            get { return this.webResourceDebug; }
            set { this.webResourceDebug = value; }
        }

        private string pathDebug = "";
        public string PathDebug
        {
            get
            {
                if (string.IsNullOrEmpty(this.pathDebug))
                {
                    this.pathDebug = StringUtils.ReplaceLastInstanceOf(this.WebResourceDebug.Replace(ScriptManager.ASSEMBLYSLUG, "").Replace('.', '/'), "/", ".");
                }
                return this.pathDebug;
            }
            set
            {
                this.pathDebug = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class ResourceAttribute : System.Attribute
    {
        private Type type = typeof(ScriptManager);
        private string webResource;
        private string filePath = "";
        private string cacheFly;

        public Type Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string WebResource
        {
            get { return this.webResource; }
            set { this.webResource = value; }
        }

        public string CacheFly
        {
            get
            {
                return this.cacheFly;
            }
            set
            {
                this.cacheFly = value;
            }
        }

        public string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(this.filePath))
                {
                    this.filePath = StringUtils.ReplaceLastInstanceOf(this.WebResource.Replace(ScriptManager.ASSEMBLYSLUG, "").Replace('.', '/'), "/", ".");
                }
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class ListenerArgumentAttribute : System.Attribute
    {
        public ListenerArgumentAttribute(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        public ListenerArgumentAttribute(int index, string name, Type type)
        {
            this.index = index;
            this.name = name;
            this.type = type;
        }

        public ListenerArgumentAttribute(int index, string name, Type type, string description)
        {
            this.index = index;
            this.name = name;
            this.type = type;
            this.description = description;
        }

        private int index = 0;
        public int Index
        {
            get
            {
                return this.index;
            }
        }

        private string name = "el";
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        private Type type = typeof(object);
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        private string description = "";
        public string Description
        {
            get
            {
                return this.description;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AjaxEventUpdateAttribute : System.Attribute
    {
        private string script;
        public string Script
        {
            get { return this.script; }
            set { this.script = value; }
        }

        private string methodName;
        public string MethodName
        {
            get { return this.methodName; }
            set { this.methodName = value; }
        }

        private AutoGeneratingScript generateMode = AutoGeneratingScript.Simple;
        public AutoGeneratingScript GenerateMode
        {
            get { return this.generateMode; }
            set { this.generateMode = value; }
        }

        //if no Script or Method then simple script generating - {0}.set[MethodName]({1})
        //{0} - control ID
        //{1} - value

        public const string AutoGenerateFormat = "{0}.{2}={1};";

        public string GetScript(WebControl c, PropertyInfo property)
        {
            StringBuilder sb = new StringBuilder();

            if (!c.CallbackValues.ContainsKey(property.Name))
            {
                return null;
            }

            object value = c.CallbackValues[property.Name];

            if (!string.IsNullOrEmpty(this.Script))
            {
                sb.AppendFormat(this.Script, c.ClientID, JSON.Serialize(value));
            }
            else if (!string.IsNullOrEmpty(this.MethodName))
            {
                MethodInfo method = c.GetType().GetMethod(this.MethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { property.PropertyType }, null);
                if (method != null)
                {
                    method.Invoke(c, new object[] { value });
                }
            }
            else //simple script generating
            {
                switch (this.GenerateMode)
                {
                    case AutoGeneratingScript.Simple:
                        sb.AppendFormat(AjaxEventUpdateAttribute.AutoGenerateFormat, c.ClientID, JSON.Serialize(value), StringUtils.ToLowerCamelCase(property.Name));
                        break;
                    case AutoGeneratingScript.WithSet:
                        sb.AppendFormat("{0}.set{2}({1});", c.ClientID, JSON.Serialize(value), property.Name);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return sb.ToString();
        }

        public void RegisterScript(WebControl c, PropertyInfo property)
        {
            c.AddScript(this.GetScript(c, property));
        }
    }

    public enum AutoGeneratingScript
    {
        Simple,
        WithSet
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DeferredRenderAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ViewStateMemberAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxMethodProxyIDAttribute : System.Attribute
    {
        private AjaxMethodProxyIDMode idMode = AjaxMethodProxyIDMode.ClientID;
        private string alias;

        public AjaxMethodProxyIDMode IDMode
        {
            get { return this.idMode; }
            set { this.idMode = value; }
        }

        public string Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }
    }

    public enum AjaxMethodProxyIDMode
    {
        None,
        ClientID,
        ID,
        Alias,
        AliasPlusID
    }
}