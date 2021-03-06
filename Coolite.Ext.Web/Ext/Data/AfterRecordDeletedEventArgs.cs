/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Xml;

namespace Coolite.Ext.Web
{
    public class AfterRecordDeletedEventArgs : RecordModifiedEventArgs
    {
        private int rowsAffected;
        private Exception e;
        private bool exceptionHandled;
        IDictionary keys;
        private ConfirmationRecord confirmation;
		
		public AfterRecordDeletedEventArgs (XmlNode record) : base(record)
		{
		}

        public AfterRecordDeletedEventArgs(XmlNode record, int rowsAffected, Exception e, IDictionary keys, ConfirmationRecord confirmation) : base(record)
        {
            this.rowsAffected = rowsAffected;
            this.e = e;
            this.keys = keys;
            this.confirmation = confirmation;
        }

        public int AffectedRows
        {
            get { return rowsAffected; }
        }

        public Exception Exception
        {
            get { return e; }
        }

        public bool ExceptionHandled
        {
            get { return exceptionHandled; }
            set { exceptionHandled = value; }
        }

        public IDictionary Keys {
			get { return keys; }
		}

        public ConfirmationRecord Confirmation
        {
            get { return confirmation; }
        }
    }
}