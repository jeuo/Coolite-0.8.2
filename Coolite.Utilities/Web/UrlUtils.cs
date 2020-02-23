/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

namespace Coolite.Utilities
{
    public class UrlUtils
    {
        public static bool IsUrl(string url)
        {
            return (!string.IsNullOrEmpty(url) && url.IndexOf("://") >= 0);
        }
    }
}
