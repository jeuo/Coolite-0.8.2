/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Xml;

namespace Coolite.Ext.Web
{
    public class BeforeAjaxEventArgs : EventArgs
    {
        private readonly string action;
        private readonly string data;
        private readonly XmlNode parameters;

        public BeforeAjaxEventArgs() { }

        internal BeforeAjaxEventArgs(string action, string data, XmlNode parameters)
        {
            this.action = action;
            this.data = data;
            this.parameters = parameters;
        }

        public string Action
        {
            get { return action; }
        }

        private StoreDataHandler dataHandler;
        public StoreDataHandler Data
        {
            get
            {
                if (dataHandler == null)
                {
                    dataHandler = new StoreDataHandler(data);
                }
                return dataHandler;
            }
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

                p = new ParameterCollection();

                if (this.parameters == null)
                {
                    return p;
                }

                p = ScriptManager.XmlToParams(this.parameters);

                //foreach (XmlNode param in this.parameters.ChildNodes)
                //{
                //    p.Add(new Parameter(param.Name, param.InnerXml));
                //}

                return p;
            }
        }
    }
}