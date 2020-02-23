/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class SubmitHandler
    {
        private string jsonData;
        private readonly HttpContext context;

        public SubmitHandler(HttpContext context)
        {
            this.context = context;
        }


        public SubmitHandler(string jsonData)
        {
            if(jsonData == null)
            {
                throw new ArgumentNullException("jsonData");
            }
            this.jsonData = jsonData;
        }

        public string Json
        {
            get
            {
                if (jsonData == null)
                {
                    jsonData = context.Request["data"];
                }
                return jsonData;
            }
        }

        public XmlNode Xml
        {
            get
            {
                return JsonConvert.DeserializeXmlNode("{records:{record:" + this.Json + "}}");
            }
        }

        public List<T> Object<T>()
        {
            return JsonConvert.DeserializeObject<List<T>>(this.Json);
        }
    }
}