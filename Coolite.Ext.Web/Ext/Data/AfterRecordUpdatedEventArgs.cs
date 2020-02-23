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
    public class AfterRecordUpdatedEventArgs : RecordModifiedEventArgs
    {
        private int rowsAffected;
        private Exception e;
        private bool exceptionHandled;
        IDictionary keys;
        IDictionary newValues;
		IDictionary oldValues;
        private ConfirmationRecord confirmation;

        public AfterRecordUpdatedEventArgs (XmlNode record) : base(record)
		{
		}

        public AfterRecordUpdatedEventArgs(XmlNode record, int rowsAffected, Exception e, IDictionary keys, IDictionary newValues, IDictionary oldValues, ConfirmationRecord confirmation)
            : base(record)
        {
            this.rowsAffected = rowsAffected;
            this.e = e;
            this.keys = keys;
            this.newValues = newValues;
            this.oldValues = oldValues;
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

		public IDictionary NewValues {
			get { return newValues; }
		}

		public IDictionary OldValues {
			get { return oldValues; }
		}

        public ConfirmationRecord Confirmation
        {
            get
            {
                return confirmation;
            }
        }
    }
}