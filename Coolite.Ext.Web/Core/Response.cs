/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class Response
    {
        private bool success;
        private string msg;
        private string data;

        public Response() { }

        public Response(bool success, string msg)
        {
            this.success = success;
            this.msg = msg;
        }

        public Response(bool success)
        {
            this.success = success;
        }

        [ClientConfig("Success")]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        [ClientConfig("Msg")]
        [DefaultValue(null)]
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        [ClientConfig("Data", JsonMode.Raw)]
        [DefaultValue(null)]
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        public void Write()
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(new ClientConfig().Serialize(this));
            HttpContext.Current.Response.End();
        }

        public virtual void Return()
        {
            CompressionUtils.GZipAndSend(new ClientConfig().Serialize(this));
        }
    }
}
