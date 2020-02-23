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
    public class LoadMaskJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            LoadMask mask = value as LoadMask;
            if (mask != null && mask.ShowMask)
            {
                string jsonMask = new ClientConfig().Serialize(mask);
                if (string.IsNullOrEmpty(jsonMask) || jsonMask.Equals("{}"))
                {
                    writer.WriteValue(true);
                }
                else
                {
                    writer.WriteRawValue(jsonMask);
                }
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(string).IsAssignableFrom(objectType);
        }
    }
}