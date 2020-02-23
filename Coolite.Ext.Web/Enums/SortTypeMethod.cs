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
    /// Sort types
    /// </summary>
    public enum SortTypeMethod
    {
        /// <summary>
        /// Default sort that does nothing
        /// </summary>
        None,

        /// <summary>
        /// Date sorting
        /// </summary>
        AsDate,

        /// <summary>
        /// Float sorting
        /// </summary>
        AsFloat,

        /// <summary>
        /// Integer sorting
        /// </summary>
        AsInt,

        /// <summary>
        /// Strips all HTML tags to sort on text only
        /// </summary>
        AsText,

        /// <summary>
        /// Case insensitive string
        /// </summary>
        AsUCString,

        /// <summary>
        /// Strips all HTML tags to sort on text only - Case insensitive
        /// </summary>
        AsUCText
    }
}