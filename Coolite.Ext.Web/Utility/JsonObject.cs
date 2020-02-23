/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    public class JsonObject : Dictionary<string, object> 
    {
        public virtual string ToJson()
        {
            return JSON.Serialize(this);
        }

        public virtual string ToJson(List<JsonConverter> converters)
        {
            return this.ToJson(converters, true);
        }

        public virtual string ToJson(List<JsonConverter> converters, bool quoteName)
        {
            return JSON.Serialize(this, converters, quoteName);
        }
    }
}