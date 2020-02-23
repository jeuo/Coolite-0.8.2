/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Web.UI;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class LazyControlJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(Control).IsAssignableFrom(valueType);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            Control control = value as Control;
            if (control != null)
            {
                if (!control.Visible)
                {
                    writer.WriteNull();
                    return;
                }

                writer.WriteRawValue(this.Format(control));
            }
        }

        string Format(Control control)
        {
            bool islazy = false;

            if (control != null && control is Observable)
            {
                islazy = (control as Observable).IsLazy;
            }

            string template = (islazy) ? "{{{0}_ClientInit}}" : "{0}";

            return string.Format(template, control.ClientID);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}