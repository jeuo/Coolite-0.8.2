/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class RecordModifiedEventArgs : EventArgs
    {
        public RecordModifiedEventArgs(XmlNode record)
        {
            this.record = record;
        }

        private XmlNode record;
        public XmlNode Record
        {
            get { return record; }
        }

        public T Object<T>()
        {
            string json = JsonConvert.SerializeXmlNode(this.Record);
            json = json.Substring(10, json.Length - 11);

            JsonSerializer serializer = new JsonSerializer();
            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            StringReader sr = new StringReader(json);
            T obj = (T)serializer.Deserialize(sr, typeof(T));
            return obj;
        }
    }
}
