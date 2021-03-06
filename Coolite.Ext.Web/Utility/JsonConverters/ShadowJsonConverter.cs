/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class ShadowJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is ShadowMode)
            {
                ShadowMode shadow = (ShadowMode)value;
                switch (shadow)
                {
                    case ShadowMode.None:
                        writer.WriteValue(false);
                        break;
                    case ShadowMode.Sides:
                    case ShadowMode.Frame:
                    case ShadowMode.Drop:
                        writer.WriteValue(shadow.ToString().ToLowerInvariant());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ShadowMode).IsAssignableFrom(objectType);
        }
    }
}