/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class ThreeStateBoolJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(ThreeStateBool).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            switch ((ThreeStateBool)value)
            {
                case ThreeStateBool.False:
                    writer.WriteRawValue("false");
                    return;
                case ThreeStateBool.True:
                    writer.WriteRawValue("true");
                    return;
            }

            writer.WriteRawValue("undefined");
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}