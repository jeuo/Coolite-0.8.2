/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class CustomConfigJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            ConfigItemCollection items = (ConfigItemCollection)value;
            
            if (value != null && items.Count > 0)
            {
                foreach (ConfigItem item in items)
                {
                    writer.WritePropertyName(item.Name);
                    writer.WriteRawValue(item.ValueToString());
                }
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ConfigItemCollection).IsAssignableFrom(objectType);
        }
    }
}
