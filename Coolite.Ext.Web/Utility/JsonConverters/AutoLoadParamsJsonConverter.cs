/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Text;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class AutoLoadParamsJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            ParameterCollection parameters = value as ParameterCollection;
            if(parameters != null)
            {
                StringBuilder sb = new StringBuilder("{params:{");

                foreach (Parameter parameter in parameters)
                {
                    sb.Append(parameter.ToString()).Append(",");
                }
                if(sb[sb.Length-1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("}}");

                writer.WriteRawValue(sb.ToString());
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (ParameterCollection).IsAssignableFrom(objectType);
        }
    }
}
