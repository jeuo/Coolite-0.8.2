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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class ClientConfig
    {
        StringBuilder sb;
        StringWriter sw;
        JsonTextWriter writer;
        private bool ignoreRenderTo = false;

        public ClientConfig() { }

        public ClientConfig(bool ignoreRenderTo)
        {
            this.ignoreRenderTo = ignoreRenderTo;
        }

        private Control owner = null;

        private List<string> exclude = new List<string>();

        public string Serialize(object obj)
        {
            return this.Serialize(obj, false);
        }

        public string Serialize(object obj, bool ignoreCustomSerialization)
        {
            if (this.owner == null)
            {
                if (obj is Control)
                {
                    this.owner = ((Control)obj);

                    if (obj is Observable)
                    {
                        foreach (ConfigItem item in ((Observable)obj).CustomConfig)
                        {
                            this.exclude.Add(item.Name);
                        }
                    }
                }
                else if (obj is StateManagedItem)
                {
                    this.owner = ((StateManagedItem)obj).Owner;
                }
            }

            if (obj is ICustomConfigSerialization && !ignoreCustomSerialization)
            {
                return (obj as ICustomConfigSerialization).Serialize(owner);
            }

            this.sb = new StringBuilder();
            this.sw = new StringWriter(sb);
            this.writer = new JsonTextWriter(sw);

            this.writer.QuoteName = false;

            if (this.owner is WebControl)
            {
                WebControl wc = (WebControl)this.owner;

                if (wc != null)
                {
                    ScriptManager sm = null;
                    try
                    {
                        sm = wc.ScriptManager;
                        if (sm != null && sm.SourceFormatting)
                        {
                            this.writer.Formatting = Formatting.Indented;
                        }
                    }
                    catch(InvalidOperationException)
                    {
                        /// TODO: should be catching ScriptManagerNotFoundException
                        // absorb exception. 
                    }
                }
            }

            this.writer.WriteStartObject();
            
            this.Process(obj);

            this.writer.WriteEndObject();
            this.writer.Flush();

            return sw.GetStringBuilder().ToString();
        }

        internal string SerializeInternal(object obj, Control owner)
        {
            this.owner = owner;
            return this.Serialize(obj);
        }

        private static ConfigProperties GetProperties(object obj)
        {
            string key = obj.GetType().FullName;
            ConfigProperties cahcheProperties = null;

            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                cahcheProperties = context.Cache[key] as ConfigProperties;
            }
            

            if (cahcheProperties != null)
            {
                return cahcheProperties;
            }
            else
            {
                PropertyInfo[] result = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                ConfigProperties list = new ConfigProperties();
                foreach (PropertyInfo propertyInfo in result)
                {
                    ClientConfigAttribute attr = ClientConfig.GetClientConfigAttribute(propertyInfo);
                    if (attr != null)
                    {
                        list.Add(new ConfigObject(propertyInfo, attr, ReflectionUtils.GetDefaultValue(propertyInfo)));    
                    }
                }

                list.Reverse();

                if (context != null)
                {
                    context.Cache.Insert(key, list);   
                }
                
                return list;
            }
        }

        private void Process(object obj)
        {
            WebControl control = obj as WebControl;
            foreach (ConfigObject property in GetProperties(obj))
            {
                try
                {
                    if (this.CheckSpecialCases(control, property.PropertyInfo))
                    {
                        this.ToExtConfig(property.PropertyInfo, obj, property.Attribute, property.DefaultValue);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error during ClientConfig initialization. " 
                                        + property.PropertyInfo.Name 
                                        + " - " 
                                        + ex.Message,ex);
                }
            }
        }

        private bool CheckSpecialCases(WebControl control, PropertyInfo property)
        {
            if (control != null)
            {
                if ((control.CancelRenderToParameter || this.ignoreRenderTo) && property.Name == "RenderToProxy")
                {
                    return false;
                }
                if (control.IsDeferredRender)
                {
                    object[] attrs = property.GetCustomAttributes(typeof(DeferredRenderAttribute), true);
                    if (attrs.Length == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static ClientConfigAttribute GetClientConfigAttribute(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(typeof(ClientConfigAttribute), true);
            
            if (attrs.Length == 1)
            {
                return (ClientConfigAttribute)attrs[0];
            }
            return null;
        }

        private InstanceOfAttribute GetInstanceOfAttribute(Type classType)
        {
            object[] attrs = classType.GetCustomAttributes(typeof(InstanceOfAttribute), true);

            if (attrs.Length == 1)
            {
                return (InstanceOfAttribute)attrs[0];
            }
            return null;
        }

        private void ToExtConfig(PropertyInfo property, object obj, ClientConfigAttribute attr, object defaultValue)
        {
            if (attr.Mode != JsonMode.Ignore)
            {
                object originalValue = property.GetValue(obj, null);

                if (!IsNullEmptyOrDefault(ref defaultValue, ref originalValue))
                {
                    if (originalValue.Equals("NULL"))
                    {
                        originalValue = null;
                    }

                    string name = StringUtils.ToLowerCamelCase(property.Name);

                    /// TODO: HACK: Need to figure out a better place to populate this 
                    /// Listener Arguments from list of ListenerArgumentAttribute's.
                    if (originalValue != null && originalValue is BaseListener)
                    {
                        ((BaseListener)originalValue).SetArgumentList(property);
                    }

                    if (!string.IsNullOrEmpty(attr.Name))
                    {
                        if (attr.Name.Contains(">"))
                        {
                            string[] parts = attr.Name.Split('>');
                            name = parts[0];
                            PropertyInfo subProp = originalValue.GetType().GetProperty(parts[1]);
                            ClientConfigAttribute subAttr = ClientConfig.GetClientConfigAttribute(subProp);

                            if (subAttr != null)
                            {
                                attr = subAttr;
                                originalValue = subProp.GetValue(originalValue, null);
                            }
                        }
                        else
                        {
                            name = attr.Name;
                        }
                    }

                    if (this.exclude.Contains(name))
                    {
                        return;
                    }

                    StringBuilder temp = new StringBuilder(128);

                    switch (attr.Mode)
                    {
                        case JsonMode.ToLower:
                            this.WriteValue(name, originalValue.ToString().ToLowerInvariant());
                            break;
                        case JsonMode.ToCamelLower:
                            this.WriteValue(name, StringUtils.ToLowerCamelCase(originalValue.ToString()));
                            break;
                        case JsonMode.Raw:
                            this.WriteRawValue(name, originalValue);
                            break;
                        case JsonMode.ObjectAllowEmpty:
                        case JsonMode.Object:
                            if(originalValue is InnerObservable)
                            {
                                InnerObservable obsr = originalValue as InnerObservable;
                                if(obsr.Visible)
                                {
                                    this.WriteRawValue(name, string.Concat("this.", obsr.ClientID));
                                }
                                
                                break;
                            }
                        
                            temp.Append(new ClientConfig().SerializeInternal(originalValue, this.owner));
                            
                            if (!IsEmptyObject(temp.ToString()) || attr.Mode == JsonMode.ObjectAllowEmpty)
                            {
                                InstanceOfAttribute type = this.GetInstanceOfAttribute(originalValue.GetType());

                                if (type != null && !string.IsNullOrEmpty(type.ClassName))
                                {
                                    this.WriteRawValue(name, string.Format("new {0}({1})",type.ClassName,temp.ToString()));
                                }
                                else
                                {
                                    this.WriteRawValue(name, temp.ToString());  
                                }
                            }
                            break;
                        case JsonMode.UnrollCollection:
                            IEnumerable si = (IEnumerable)originalValue;
                            foreach (object unrollingObject in si)
                            {
                                if (unrollingObject != null)
                                {
                                    this.Process(unrollingObject);
                                }
                            }
                            break;
                        case JsonMode.UnrollObject:
                            this.Process(originalValue);
                            break;
                        case JsonMode.Array:
                        case JsonMode.AlwaysArray:
                            if (originalValue is IEnumerable)
                            {
                                IList list = (IList)originalValue;
                                if (list.Count == 1 && attr.Mode != JsonMode.AlwaysArray)
                                {
                                    temp.Append(new ClientConfig().SerializeInternal(list[0], this.owner));
                                    if (!IsEmptyObject(temp.ToString()))
                                    {
                                        this.WriteRawValue(name, temp.ToString());
                                    }
                                }
                                else
                                {
                                    bool comma = false;
                                    temp.Append("[");
                                    foreach (object o in list)
                                    {
                                        if (comma)
                                        {
                                            temp.Append(",");
                                        }
                                        if(o.GetType().IsPrimitive || o is string)
                                        {
                                            temp.Append(JSON.Serialize(o));
                                        }
                                        else
                                        {
                                            temp.Append(new ClientConfig().SerializeInternal(o, this.owner));  
                                        }
                                        
                                        comma = true;
                                    }
                                    temp.Append("]");

                                    InstanceOfAttribute type = this.GetInstanceOfAttribute(originalValue.GetType());

                                    if (type != null)
                                    {
                                        this.WriteRawValue(name, string.Format("new {0}({1})", type.ClassName, temp.ToString()));
                                    }
                                    else
                                    {
                                        this.WriteRawValue(name, temp.ToString());
                                    }
                                }
                            }
                            break;
                        case JsonMode.ArrayToObject:
                            if (originalValue is IEnumerable)
                            {
                                IList list = (IList)originalValue;
                                
                                temp.Append("{");
                                bool comma = false;
                                foreach (object o in list)
                                {
                                    if(comma)
                                    {
                                        temp.Append(",");
                                    }
                                    temp.Append(o.ToString());
                                    comma = true;
                                }
                                temp.Append("}");

                                if (!IsEmptyObject(temp.ToString()))
                                {
                                    this.WriteRawValue(name, temp.ToString());
                                }
                            }
                            break;
                        case JsonMode.Custom:
                            if (originalValue != null)
                            {
                                if (originalValue is IList && ((IList)originalValue).Count == 0)
                                {
                                    break;
                                }
                                
                                if(name != "-")
                                {
                                    this.writer.WritePropertyName(name); 
                                }
                                
                                JsonConverter converter = (JsonConverter)Activator.CreateInstance(attr.JsonConverter);
                                PropertyInfo prop = converter.GetType().GetProperty("PropertyName");
                                if(prop != null)
                                {
                                    prop.SetValue(converter, name, null);
                                }

                                converter.WriteJson(this.writer, originalValue);
                            }
                            break;
                        case JsonMode.ToClientID:
                            Control control = ControlUtils.FindControl(this.owner, originalValue.ToString(), true);
                            if (control != null)
                            {
                                if(name.StartsWith("{raw}"))
                                {
                                    name = name.Substring(5);
                                    this.WriteValue(name, control.ClientID);
                                }
                                else
                                {
                                    this.WriteRawValue(name, control.ClientID);
                                }
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format("The Control with ID = '{0}' not found", originalValue.ToString()));
                            }
                            break;
                        case JsonMode.ToString:
                            this.WriteValue(name, originalValue.ToString());
                            break;
                        case JsonMode.Value:
                        default:
                            this.WriteValue(name, originalValue);
                            break;
                    }
                }
            }
        }

        private void WriteRawValue(string name, object value)
        {
            this.WriteRawValue(name, value, true);
        }

        private void WriteRawValue(string name, object value, bool parseTokens)
        {
            this.writer.WritePropertyName(name);

            if (value is string)
            {
                value = TokenUtils.ParseTokens(value.ToString(), this.owner);

                if (value.ToString().StartsWith("<raw>"))
                {
                    value = value.ToString().Substring(5);
                }
            }

            this.writer.WriteRawValue(value.ToString());
        }

        private void WriteValue(string name, object value)
        {
            if (value is string)
            {
                value = TokenUtils.ParseTokens(value.ToString(), this.owner);
                
                string temp = value.ToString();

                if (temp.StartsWith("<string>"))
                {
                    int count = 8;

                    if (temp.StartsWith("<string><raw>"))
                    {
                        count = 13;
                    }

                    this.writer.WritePropertyName(name);
                    this.writer.WriteValue(temp.Substring(count));
                    return;
                }

                if(temp.StartsWith("<raw>"))
                {
                    this.WriteRawValue(name, temp.Substring(5));
                    return;
                }
            }

            this.writer.WritePropertyName(name);

            if(value is Unit)
            {
                this.writer.WriteValue(Convert.ToInt32(((Unit)value).Value));
            }
            else if (value is Enum)
            { 
                this.writer.WriteValue(value.ToString());
            }
            else
            {
                this.writer.WriteValue(value);
            }
        }

        public bool IsNullEmptyOrDefault(ref object defaultValue, ref object originalValue)
        {
            if (defaultValue == null)
            {
                defaultValue = "NULL";
            }

            if (originalValue == null)
            {
                originalValue = "NULL";
            }
            else if (!(originalValue is string) && originalValue is IEnumerable)
            {
                if(!((IEnumerable)originalValue).GetEnumerator().MoveNext())
                {
                    originalValue = "NULL";
                }
            }
            else if (originalValue is DateTime)
            {
                DateTime t = (DateTime)originalValue;
                if (t.Equals(DateTime.MinValue) || t.Equals(DateTime.MaxValue))
                {
                    return true;
                }
            }
            else if (originalValue is Unit)
            {
                Unit defaultVal = (Unit)defaultValue;
                Unit originalVal = (Unit)originalValue;
                if (originalVal.IsEmpty || defaultVal.Equals(originalValue))
                {
                    return true;
                }
            }
            else if (originalValue is WebControl)
            {
                return ((WebControl)originalValue).IsDefault;
            }
            else if (originalValue is StateManagedItem)
            {
                return ((StateManagedItem)originalValue).IsDefault;
            }
            else if (originalValue is Margins)
            {
                return ((Margins)originalValue).IsDefault;
            }
            
            return defaultValue.Equals(originalValue);
        }

        public static bool IsEmptyObject(string value)
        {
            return (string.IsNullOrEmpty(value) || value.Equals("{}") || value.Equals("[]"));
        }
    }

    internal class ConfigObject
    {
        private PropertyInfo propertyInfo;
        private ClientConfigAttribute attr;
        private object defaultValue;

        public ConfigObject(PropertyInfo propertyInfo, ClientConfigAttribute attr, object defValue)
        {
            this.propertyInfo = propertyInfo;
            this.attr = attr;
            this.defaultValue = defValue;
        }

        public PropertyInfo PropertyInfo
        {
            get { return propertyInfo; }
            set { propertyInfo = value; }
        }

        public ClientConfigAttribute Attribute
        {
            get { return attr; }
            set { attr = value; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
    }

    internal class ConfigProperties : List<ConfigObject> { }
}
