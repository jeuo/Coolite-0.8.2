/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class StoreDataHandler
    {
        private string jsonData;
        private XmlDocument xmlData;
        private readonly HttpContext context;

        public StoreDataHandler(HttpContext context)
        {
            this.context = context;
        }

        public StoreDataHandler(string jsonData)
        {
            if(jsonData == null)
            {
                throw new ArgumentNullException("jsonData");
            }
            this.jsonData = jsonData;
        }

        public string JsonData
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

        public XmlDocument XmlData
        {
            get
            {
                if(xmlData == null)
                {
                    RecordsToXmlConverter converter = new RecordsToXmlConverter();
                    
                    xmlData = (XmlDocument)JsonConvert.DeserializeObject(JsonData, typeof(XmlDocument), converter);
                }
                return xmlData;
            }
        }

        public ChangeRecords<T> ObjectData<T>()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            StringReader sr = new StringReader(JsonData);
            ChangeRecords<T> data = (ChangeRecords<T>)serializer.Deserialize(sr, typeof(ChangeRecords<T>));
            return data;
        }

        public ConfirmationList BuildConfirmationList(string idColumnName)
        {
            if (string.IsNullOrEmpty(idColumnName))
            {
                return null;
            }

            ConfirmationList confirmationList = new ConfirmationList();

            XmlNodeList records = this.XmlData.SelectNodes("records/*/record");

            foreach (XmlNode node in records)
            {
                XmlNode keyNode = node.SelectSingleNode(idColumnName);
                if (string.IsNullOrEmpty(keyNode.InnerText))
                {
                    throw new InvalidOperationException("No id in submitted record");
                }
                confirmationList.Add(keyNode.InnerText, new ConfirmationRecord(false, keyNode.InnerText));
            }

            return confirmationList;
        }
    }
}
