/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using Newtonsoft.Json;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class RendererJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(Renderer).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value != null && value is Renderer)
            {
                string temp = ((Renderer)value).ToConfigString();

                if (!string.IsNullOrEmpty(temp))
                {
                    writer.WriteRawValue(temp);
                    return;
                }
            }
            writer.WriteRawValue("null");
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}