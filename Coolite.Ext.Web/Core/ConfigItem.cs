/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Text;

namespace Coolite.Ext.Web
{
    public class ConfigItem : BaseParameter
    {
        public ConfigItem() { }

        public ConfigItem(string name, string value) : base(name, value) { }

        public ConfigItem(string name, string value, ParameterMode mode) : base(name, value, mode) { }

        public ConfigItem(string name, string value, bool encode) : base(name, value, encode) { }

        public ConfigItem(string name, string value, ParameterMode mode, bool encode) : base(name, value, mode, encode) { }

        protected override ParameterMode DefaultMode
        {
            get
            {
                return ParameterMode.Raw;
            }
        }
    }

    public class ConfigItemCollection : StateManagedCollection<ConfigItem>
    {
        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if (this.Count > 0)
            {
                bool needComma = false;
                foreach (ConfigItem item in this)
                {
                    if(needComma)
                    {
                        sb.Append(",");
                    }
                    
                    sb.Append(item.Name);
                    sb.Append(":");
                    sb.Append(item.ValueToString());
                    needComma = true;
                }
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}