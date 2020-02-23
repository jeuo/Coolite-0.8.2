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
    public class BeforeRecordDeletedEventArgs : RecordModifiedEventArgs
    {
		IDictionary keys;
        private ConfirmationRecord confirmation;
        private bool cancel;
        private bool cancelAll;
		
		public BeforeRecordDeletedEventArgs(XmlNode record) : base(record)
		{
		}

        internal BeforeRecordDeletedEventArgs(XmlNode record, IDictionary keys, ConfirmationRecord confirmation)
            : base(record)
		{
			this.keys = keys;
            this.confirmation = confirmation;
		}
		
		public IDictionary Keys {
			get { return keys; }
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
            get { return confirmation; }
        }
    }
}