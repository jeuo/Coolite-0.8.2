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
    // ISODateTimeJsonConverter based on Newtonsoft.Json.Converters.IsoDateTimeConverter
    // The DateTimeFormat const was changed to remove the millisecond format specifier. 
    // Copyright (c) 2007 James Newton-King

    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from the ISO 8601 date format (e.g. 2008-04-12T12:53Z).
    /// </summary>
    public class ISODateTimeJsonConverter : JsonConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";

        private DateTimeStyles dateTimeStyles = DateTimeStyles.RoundtripKind;

        /// <summary>
        /// Gets or sets the date time styles used when converting a date to and from JSON.
        /// </summary>
        /// <value>The date time styles used when converting a date to and from JSON.</value>
        public DateTimeStyles DateTimeStyles
        {
            get { return dateTimeStyles; }
            set { dateTimeStyles = value; }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
        {
            string text;

            if (value is DateTime)
            {
                DateTime dateTime = (DateTime)value;

                if ((dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                  || (dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                {
                    dateTime = dateTime.ToUniversalTime();
                }

                text = dateTime.ToString(ISODateTimeJsonConverter.DateTimeFormat, CultureInfo.InvariantCulture);
            }
            else
            {
                DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
                if ((dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                  || (dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                    dateTimeOffset = dateTimeOffset.ToUniversalTime();

                text = dateTimeOffset.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
            }

            writer.WriteValue(text);
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

            string dateText = reader.Value.ToString();

            if (objectType == typeof(DateTimeOffset))
                return DateTimeOffset.Parse(dateText, CultureInfo.InvariantCulture, dateTimeStyles);

            return DateTime.Parse(dateText, CultureInfo.InvariantCulture, dateTimeStyles);
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