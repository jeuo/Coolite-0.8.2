/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    // JSONDateTimeJsonConverter based on Newtonsoft.Json.Converters.IsoDateTimeConverter
    // The JSONDateTimeJsonConverter returns the server's local time.
    // Copyright (c) 2007 James Newton-King

    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from the ISO 8601 date format (e.g. 2008-04-12T12:53) using the server
    /// time. Does not adjust for timezone.
    /// </summary>
    public class JSONDateTimeJsonConverter : JsonConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
        {
            if (value is DateTime)
            {
                DateTime date = (DateTime) value;
                if(date != DateTime.MinValue)
                {
                    writer.WriteValue(date.ToString(DateTimeFormat, CultureInfo.InvariantCulture));
                }
                else
                {
                    writer.WriteRawValue("null");
                }
                return;
            }

            writer.WriteRawValue("null");
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            if (reader.TokenType != JsonToken.String)
                throw new Exception(string.Format("Unexpected token parsing date. Expected String, got {0}.", reader.TokenType));

            if (string.IsNullOrEmpty(reader.Value.ToString()))
            {
                return null;
            }

            return DateTime.Parse(reader.Value.ToString(), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return (typeof(DateTime).IsAssignableFrom(objectType)
              || typeof(DateTimeOffset).IsAssignableFrom(objectType));
        }
    }
}