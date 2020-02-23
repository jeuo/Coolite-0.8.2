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
    public class BeforeRecordUpdatedEventArgs : RecordModifiedEventArgs
    {
		IDictionary keys;
		IDictionary newValues;
		IDictionary oldValues;
        private bool cancel;
        private bool cancelAll;
        private ConfirmationRecord confirmation;
		
		public BeforeRecordUpdatedEventArgs (XmlNode record) : base(record)
		{
		}

        internal BeforeRecordUpdatedEventArgs(XmlNode record, IDictionary keys, IDictionary newValues, IDictionary oldValues, ConfirmationRecord confirmation)
            : base(record)
		{
			this.keys = keys;
			this.newValues = newValues;
			this.oldValues = oldValues;
            this.confirmation = confirmation;
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

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public bool CancelAll
        {
            get { return cancelAll; }
            set { cancelAll = value; }
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