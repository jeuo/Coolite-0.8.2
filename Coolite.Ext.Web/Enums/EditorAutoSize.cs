/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/namespace Coolite.Ext.Web
{
    /// <summary>
    /// Size for the editor to automatically adopt the size of the underlying field, "Width" to adopt the width only, or "Height" to adopt the height only (defaults to Disable)
    /// </summary>
    public enum EditorAutoSize
    {
        /// <summary>
        /// Disable auto size
        /// </summary>
        Disable,
        /// <summary>
        /// Fits the editor to automatically adopt the size of the underlying field
        /// </summary>
        Fit,
        /// <summary>
        /// "Width" to adopt the width only
        /// </summary>
        Width,
        /// <summary>
        /// "Height" to adopt the height only
        /// </summary>
        Height
    }
}