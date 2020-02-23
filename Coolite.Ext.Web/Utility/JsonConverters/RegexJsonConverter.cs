/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class RegexJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(string).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is string)
            {
                string re = (string)value;

                if (!re.StartsWith("/", StringComparison.InvariantCulture))
                {
                    re = string.Format("/{0}", re);
                }

                if (!re.EndsWith("/", StringComparison.InvariantCulture))
                {
                    re = string.Format("{0}/", re);
                }

                writer.WriteRawValue(re);
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}