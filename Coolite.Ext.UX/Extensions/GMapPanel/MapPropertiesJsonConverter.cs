/******** 
 * This file is part of the Coolite UX Toolkit.

 * The Coolite UX Toolkit is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * The Coolite UX Toolkit is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the Coolite Toolkit.  If not, see <http://www.gnu.org/licenses/>.
 */

/*
* @version:		0.6.0 Preview-1
* @author:		Coolite Inc. http://www.coolite.com/
* @date:		2008-08-05
* @copyright:	Copyright (c) 2006-2008, Coolite Inc, or as noted within each 
* 				applicable file LICENSE.txt file
* @license:		LGPL 3.0 License
* @website:		http://www.coolite.com/
 ********/

using System;
using System.Reflection;
using System.Text;
using Coolite.Ext.Web;
using Coolite.Utilities;
using Newtonsoft.Json;
using JsonReader=Newtonsoft.Json.JsonReader;

namespace Coolite.Ext.UX
{
    public class MapPropertiesJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value != null)
            {
                bool isControls = value is MapControls;
                PropertyInfo[] properties = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                StringBuilder sb = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    ClientConfigAttribute attr = ClientConfig.GetClientConfigAttribute(property);

                    if (attr != null && property.PropertyType == typeof(bool))
                    {
                        object prValue = property.GetValue(value, null);
                        object defaultValue = ReflectionUtils.GetDefaultValue(property);
                        if((bool)prValue)
                        {
                            if(!isControls)
                            {
                                if(!(bool)defaultValue)
                                {
                                    sb.Append(string.Concat("'enable", property.Name, "',")); 
                                }
                            }
                            else
                            {
                                sb.Append(string.Concat("'", property.Name,"',"));
                            }
                        }
                        else
                        {
                            if (!isControls)
                            {
                                if ((bool)defaultValue)
                                {
                                    sb.Append(string.Concat("'disable", property.Name, "',"));
                                }
                            }
                        }
                    }
                }

                if(sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    writer.WriteStartArray();
                    writer.WriteRaw(sb.ToString());
                    writer.WriteEndArray();
                    return;
                }
            }
            writer.WriteStartArray();
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(MapConfiguration).IsAssignableFrom(objectType) || typeof(MapControls).IsAssignableFrom(objectType);
        }
    }
}
