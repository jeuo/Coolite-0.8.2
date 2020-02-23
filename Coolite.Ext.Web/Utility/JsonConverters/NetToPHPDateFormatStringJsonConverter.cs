/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class NetToPHPDateFormatStringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(string).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is string)
            {
                string temp = (string)value;
                if (!string.IsNullOrEmpty(temp))
                {
                    writer.WriteValue(DateTimeUtils.ConvertNetToPHP(temp));
                    return;
                }
            }
            writer.WriteRawValue(null);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}