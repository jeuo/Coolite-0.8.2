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
    public class AjaxEventHandlerJsonConverter : JsonConverter
    {
        private string propertyName;

        public string PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            if (value is string)
            {
                string prms = this.PropertyName == "before"
                                  ? "el, type, action, extraParams"
                                  : "response, result, el, type, action, extraParams";

                writer.WriteRawValue(string.Concat("function(", prms, "){", (string)value,"}")); 
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(string).IsAssignableFrom(objectType);
        }
    }
}
