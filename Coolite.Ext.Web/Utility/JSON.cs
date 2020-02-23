/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    // Comments are Copyright (c) 2007 James Newton-King
    /// <summary>
    /// Convenience wrappers for Json.NET
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// Serializes the specified object to a Json object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="converters">A List of JsonConverter objects to customize serialization.</param>
        /// <param name="quoteName">Gets or Sets a value indicating whether object names will be surrounded with quotes.</param>
        /// <returns>A Json string representation of the object.</returns>
        public static string Serialize(object obj, List<JsonConverter> converters, bool quoteName)
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            if (!quoteName)
            {
                writer.QuoteName = false;
            }
            
            JsonSerializer serializer = new JsonSerializer();

            if (converters != null)
            {
                foreach (JsonConverter converter in converters)
                {
                    serializer.Converters.Add(converter);
                }
            }

            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            serializer.Serialize(writer, obj);

            return sw.GetStringBuilder().ToString();
        }

        /// <summary>
        /// Serializes the Json object to a specific object.
        /// </summary>
        /// <param name="value">Json object</param>
        /// <param name="type">Object's type</param>
        /// <param name="converters">A List of JsonConverter objects to customize deserialization.</param>
        /// <returns>Object</returns>
        public static object Deserialize(string value, Type type, List<JsonConverter> converters)
        {
            StringReader sr = new StringReader(value);
            JsonTextReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new JsonSerializer();

            if (converters == null)
            {
                converters = JSON.Converters;
            }

            foreach (JsonConverter converter in converters)
            {
                serializer.Converters.Add(converter);
            }

            return serializer.Deserialize(reader, type);
        }

        /// <summary>
        /// Serializes the specified object to a Json object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="converters">A List of JsonConverter objects to customize serialization.</param>
        /// <returns>A Json string representation of the object.</returns>
        public static string Serialize(object obj, List<JsonConverter> converters)
        {
            return JSON.Serialize(obj, converters, true);
        }

        /// <summary>
        /// Serializes the specified object to a Json object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A Json string representation of the object.</returns>
        public static string Serialize(object obj)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new JSONDateTimeJsonConverter());
            converters.Add(new EnumJsonConverter());

            return JSON.Serialize(obj, converters, true);
        }

        public static List<JsonConverter> Converters
        {
            get
            {
                List<JsonConverter> converters = new List<JsonConverter>();
                converters.Add(new JSONDateTimeJsonConverter());
                converters.Add(new EnumJsonConverter());
                converters.Add(new GuidJsonConverter());

                return converters;
            }
        }

        /// <summary>
        /// Deserializes the specified object to a Json object.
        /// </summary>
        /// <param name="value">The object to deserialize.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static object Deserialize(string value)
        {
            return JSON.Deserialize(value, null, null);
        }

        /// <summary>
        /// Deserializes the specified object to a Json object.
        /// </summary>
        /// <param name="value">The object to deserialize.</param>
        /// <param name="type">The <see cref="Type"/> of object being deserialized.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static object Deserialize(string value, Type type)
        {
            return JSON.Deserialize(value, type, null);
        }

        /// <summary>
        /// Deserializes the specified object to a Json object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="value">The object to deserialize.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static T Deserialize<T>(string value)
        {
            return (T)JSON.Deserialize(value, typeof(T), null);
        }

        /// <summary>
        /// Deserializes the specified object to a Json object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="value">The object to deserialize.</param>
        /// <param name="converters">A List of JsonConverter objects to customize serialization.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static T Deserialize<T>(string value, List<JsonConverter> converters)
        {
            return (T)JSON.Deserialize(value, typeof(T), converters);
        }

        /// <summary>
        /// Deserializes the specified object to a Json object.
        /// </summary>
        /// <param name="value">The object to deserialize.</param>
        /// <returns>The deserialized object from the Json string.</returns>
        public static XmlNode DeserializeXmlNode(string value)
        {
            return JsonConvert.DeserializeXmlNode(value);
        }
    }
}