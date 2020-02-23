/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Text;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class StoreResponseData
    {
        private string data;
        private int count;
        private ConfirmationList confirmation;

        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        public ConfirmationList Confirmation
        {
            get { return confirmation; }
            set { confirmation = value; }
        }

        public int TotalCount
        {
            get { return count; }
            set { count = value; }
        }

        public virtual void Return()
        {
            CompressionUtils.GZipAndSend(this);
        }

        public override string ToString()
        {
            if(string.IsNullOrEmpty(this.Data) && (this.Confirmation == null || this.Confirmation.Count==0))
            {
                return null;
            }
            
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if(!string.IsNullOrEmpty(this.Data))
            {
                sb.AppendFormat("data:{0}, totalCount: {1},", this.Data, this.TotalCount);
            }
            
            string returnConfirmation = "";
            if (this.Confirmation != null && this.Confirmation.Count>0)
            {
                returnConfirmation = this.Confirmation.ToJson();
            }

            if (!string.IsNullOrEmpty(returnConfirmation))
            {
                sb.AppendFormat("confirm:{0}", returnConfirmation);
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
