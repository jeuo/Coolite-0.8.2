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
    public class BeforeStoreChangedEventArgs : EventArgs
    {
        private readonly string jsonData;
        private bool cancel;
        private readonly XmlNode ajaxPostBackParams;
        private ConfirmationList confirmationList;

        public BeforeStoreChangedEventArgs(string jsonData, ConfirmationList confirmationList)
        {
            this.jsonData = jsonData;
            this.cancel = false;
            this.confirmationList = confirmationList;
        }

        public BeforeStoreChangedEventArgs(string jsonData, ConfirmationList confirmationList, XmlNode callbackParams)
            : this(jsonData, confirmationList)
        {
            this.ajaxPostBackParams = callbackParams;
        }

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public XmlNode AjaxPostBackParams
        {
            get
            {
                return this.ajaxPostBackParams;
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

                if (this.ajaxPostBackParams == null)
                {
                    return new ParameterCollection();
                }

                p = ScriptManager.XmlToParams(this.ajaxPostBackParams);

                return p;
            }
        }

        public ConfirmationList ConfirmationList
        {
            get
            {
                return confirmationList;
            }
            internal set
            {
                confirmationList = value;
            }
        }

        private StoreDataHandler dataHandler;

        public StoreDataHandler DataHandler
        {
            get
            {
                if (dataHandler == null)
                {
                    dataHandler = new StoreDataHandler(jsonData);
                }
                return dataHandler;
            }
        }
    }
}