/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;

namespace Coolite.Ext.Web
{
    // REFERENCE: http://msdn2.microsoft.com/en-us/library/ayybcxe5.aspx
    [ToolboxItem(false)]
    public class GenericConverter : ExpandableObjectConverter
    {
        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}