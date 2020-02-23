/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
    /// <summary>
    /// The data type for conversion to displayable value
    /// </summary>
    public enum RecordFieldType
    {
        /// <summary>
        /// (Default, implies no conversion)
        /// </summary>
        Auto,
        
        /// <summary>
        /// To string conversion
        /// </summary>
        String,

        /// <summary>
        /// To int conversion
        /// </summary>
        Int,

        /// <summary>
        /// To float conversion
        /// </summary>
        Float,

        /// <summary>
        /// To boolean conversion
        /// </summary>
        Boolean,

        /// <summary>
        /// To date conversion
        /// </summary>
        Date
    }
}