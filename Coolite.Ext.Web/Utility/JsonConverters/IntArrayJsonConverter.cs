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
    public class IntArrayJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(string[]).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is int[])
            {
                writer.WriteRawValue(string.Format("[{0}]", StringUtils.Concat((int[])value, ",", "{0}")));
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}