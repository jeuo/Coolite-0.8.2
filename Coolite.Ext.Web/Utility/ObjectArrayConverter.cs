/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Coolite.Ext.Web
{
    // REFERENCE: http://msdn2.microsoft.com/en-us/library/ayybcxe5.aspx
    class ObjectArrayConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            if (value is string)
            {
                string[] list = ((string)value).Split(',');
                object[] items = new object[list.Length];

                for(int i = 0; i < list.Length; i++)
                {
                    items[i] = (object)list[i];
                }
                return items;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is string[] && destinationType == typeof(string))
                return string.Join(",", ((string[])value));

            return base.ConvertTo(context, culture, value, destinationType);
        }

    }
}