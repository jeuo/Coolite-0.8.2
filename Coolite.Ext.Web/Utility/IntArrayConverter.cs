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
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    // REFERENCE: http://msdn2.microsoft.com/en-us/library/ayybcxe5.aspx
    class IntArrayConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            if (value is string)
            {
                string[] list = ((string)value).Split(',');
                int[] items = new int[list.Length];

                for(int i = 0; i < list.Length; i++)
                {
                    items[i] = Convert.ToInt32(list[i]);
                }
                return items;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw base.GetConvertToException(value, destinationType);
            }
            if (value == null)
            {
                return null;
            }

            return StringUtils.Concat((int[])value);
        }
    }
}