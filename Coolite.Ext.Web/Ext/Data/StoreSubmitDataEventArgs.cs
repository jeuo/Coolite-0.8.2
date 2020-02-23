/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class StoreSubmitDataEventArgs : EventArgs
    {
        private readonly XmlNode parameters;
        private readonly string json;

        public StoreSubmitDataEventArgs()
        {
        }

        public StoreSubmitDataEventArgs(string json, XmlNode parameters)
        {
            this.parameters = parameters;
            this.json = json;
        }

        public string Json
        {
            get
            {
                return this.json;
            }
        }

        public XmlNode Xml
        {
            get
            {
                return JsonConvert.DeserializeXmlNode("{records:{record:" + json + "}}");
            }
        }

        public List<T> Object<T>()
        {
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        private ParameterCollection p;
        public ParameterCollection Parameters
        {
            get
            {
                if (p != null)
                {
                    return p;
                }

                if (this.parameters == null)
                {
                    return new ParameterCollection();
                }

                p = ScriptManager.XmlToParams(this.parameters);

                return p;
            }
        }
    }
}