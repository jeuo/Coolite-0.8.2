/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Text;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class ListItemCollectionJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            ListItemCollection<ListItem> items = value as ListItemCollection<ListItem>;

            StringBuilder sb = new StringBuilder("new Ext.data.SimpleStore({fields:[\"text\",\"value\"],data :[");
            if (items != null && items.Count > 0)
            {
                foreach (ListItem item in items)
                {
                    sb.Append("[");
                    sb.Append(JSON.Serialize(item.Text));
                    sb.Append(",");
                    sb.Append(JSON.Serialize(item.Value));
                    sb.Append("],");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            
            sb.Append("]})");

            writer.WriteRawValue(sb.ToString());
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ListItemCollection<ListItem>).IsAssignableFrom(objectType);
        }
    }
}
