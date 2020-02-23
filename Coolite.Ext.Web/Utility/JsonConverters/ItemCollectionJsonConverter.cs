/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Web.UI;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class ItemCollectionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type valueType)
        {
            return typeof(ItemCollection).IsAssignableFrom(valueType);
        }

        private static bool CanBeSingleItemArray(object item)
        {
            return (item is ButtonBase) || (item is BaseMenuItem) || (item is Checkbox) || (item is Radio);
        }

        public override void WriteJson(JsonWriter writer, object value)
        {
            IList items = (IList)value;

            if (value != null && items.Count > 0)
            {
                if (items.Count == 1 && !CanBeSingleItemArray(items[0]))
                {
                    Control item = (Control)items[0];
                    Tab tab = item as Tab;
                    if (!item.Visible || (tab != null && tab.Hidden))
                    {
                        writer.WriteNull();
                        return;
                    }

                    /// TODO: Check for ToolkbarBase because ExtJS can not render a Toolbar with xtype.
                    /// Make a "new" constructor instead of lazy instanciation.
                    if (items[0] is ToolbarBase)
                    {
                        object[] attrs = items[0].GetType().GetCustomAttributes(typeof(InstanceOfAttribute), true);

                        string instanceOf = "Ext.Toolbar";

                        if (attrs.Length == 1)
                        {
                            string temp = ((InstanceOfAttribute)attrs[0]).ClassName; 

                            if(!string.IsNullOrEmpty(temp))
                            {
                                instanceOf = temp;
                            }
                        }
                        
                        writer.WriteStartConstructor(instanceOf);
                        writer.WriteRawValue(this.Format(items[0] as Control));
                        writer.WriteEndConstructor();
                    }
                    else
                    {
                        writer.WriteRawValue(this.Format(items[0] as Control));
                    }
                }
                else
                {
                    bool visible = false;

                    foreach (Observable item in items)
                    {
                        Tab tab = item as Tab;
                        if (item.Visible && (tab == null || !tab.Hidden))
                        {
                            visible = true;
                        }
                    }

                    if (visible)
                    {
                        writer.WriteStartArray();

                        foreach (Observable item in items)
                        {
                            Tab tab = item as Tab;
                            if (item.Visible && (tab == null || !tab.Hidden))
                            {
                                writer.WriteRawValue(this.Format(item));
                            }
                        }

                        writer.WriteEndArray();
                    }
                    else
                    {
                        writer.WriteNull();
                    }
                }
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