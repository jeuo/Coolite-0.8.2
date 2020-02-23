/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.Text;

namespace Coolite.Ext.Web
{
    public class ConfirmationRecord
    {
        private bool confirm;
        private string oldId;
        private string newId;

        public ConfirmationRecord(bool confirm, string oldId)
        {
            this.confirm = confirm;
            this.oldId = oldId;
        }

        public ConfirmationRecord()
        {
        }

        public bool Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }

        public string OldId
        {
            get { return oldId; }
            set { oldId = value; }
        }

        public string NewId
        {
            get { return newId; }
            set { newId = value; }
        }

        public void ConfirmRecord(string newId)
        {
            this.Confirm = true;
            this.NewId = newId;
        }

        public void ConfirmRecord()
        {
            this.Confirm = true;
            this.NewId = this.oldId;
        }

        public void UnConfirmRecord()
        {
            this.Confirm = false;
            this.NewId = null;
        }
    }

    public class ConfirmationList : SortedList<string, ConfirmationRecord>
    {
        public void ConfirmRecord(string oldId, string newId)
        {
            this[oldId].ConfirmRecord(newId);
        }

        public void ConfirmRecord(string oldId)
        {
            this[oldId].ConfirmRecord();
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (KeyValuePair<string, ConfirmationRecord> pair in this)
            {
                sb.AppendFormat("{{s:{0},oldId:{1},newId:{2}}},", pair.Value.Confirm.ToString().ToLower(), JSON.Serialize(pair.Value.OldId), JSON.Serialize(pair.Value.NewId ?? pair.Value.OldId));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return sb.ToString();
        }
    }
}
