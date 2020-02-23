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
    public class CtorDateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(DateTime).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;

                if (date.Equals(DateTime.MinValue))
                {
                    writer.WriteRawValue("null");
                }
                else
                {
                    string template = (date.TimeOfDay == new TimeSpan(0, 0, 0)) ? "{0},{1},{2}" : "{0},{1},{2},{3},{4},{5}";

                    writer.WriteStartConstructor("Date");
                    writer.WriteRawValue(
                        string.Format(template, date.Year, date.Month - 1, date.Day,
                                      date.Hour, date.Minute, date.Second));
                    writer.WriteEndConstructor();
                }
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}