/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Web.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    public class PluginCollectionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(List<Plugin>).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            List<Plugin> items = (List<Plugin>)value;

            if (value != null && items.Count > 0)
            {
                if (items.Count == 1)
                {
                    Plugin plugin = items[0] as Plugin;

                    string ctor = this.GetCtr(plugin);

                    if (string.IsNullOrEmpty(ctor))
                    {
                        throw new ArgumentException(string.Format("The Plugin {0} does not contain an \"InstanceOfAttribute\" or the \"InstanceOf\" property has not been set.", plugin.ID));
                    }

                    writer.WriteRawValue(ctor);
                }
                else
                {
                    writer.WriteStartArray();

                    foreach (Plugin plugin in items)
                    {
                        writer.WriteRawValue(this.GetCtr(plugin));
                    }

                    writer.WriteEndArray();
                }
                return;
            }
            writer.WriteRawValue("null");
        }

        private string GetCtr(Plugin plugin)
        {
            if (plugin is GenericPlugin && !string.IsNullOrEmpty(((GenericPlugin)plugin).InstanceOf))
            {
                GenericPlugin gp = (GenericPlugin)plugin;

                return string.Concat("new ", gp.InstanceOf, "(", gp.CustomConfig.ToJson(), ")");
            }
            else if (plugin is OuterPlugin)
            {
                return string.Concat("this.", plugin.ClientID);
            }
            
            return plugin.GetClientConstructor(true, null);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}