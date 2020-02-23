/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class AjaxResponse
    {
        private bool success = true;
        private string error;
        private string script;
        private string viewState;
        private string viewStateEncrypted;
        private string eventValidation;
        private string serviceResponse;
        private string userResponse;
        private readonly bool internalUsing;
        private object result;

        public AjaxResponse() { }

        internal AjaxResponse(bool internalUsing)
        {
            this.internalUsing = internalUsing;
        }

        public AjaxResponse(string script)
        {
            this.Script = script;
        }

        public override string ToString()
        {
            if (!this.internalUsing && HttpContext.Current != null && HttpContext.Current.CurrentHandler is Page)
            {
                return string.Concat("<Coolite.ManualAjaxResponse>", new ClientConfig().Serialize(this), "</Coolite.ManualAjaxResponse>");
            }

            return new ClientConfig().Serialize(this);
        }

        public virtual void Return()
        {
            CompressionUtils.GZipAndSend(this);
        }

        [ClientConfig]
        [DefaultValue(true)]
        public bool Success
        {
            get { return this.success; }
            set { this.success = value; }
        }

        [ClientConfig]
        [DefaultValue(null)]
        public string ErrorMessage
        {
            get { return this.error; }
            set { this.error = value; }
        }

        [ClientConfig]
        [DefaultValue(null)]
        public string Script
        {
            get { return this.script; }
            set { this.script = value; }
        }

        [ClientConfig]
        [DefaultValue(null)]
        public string ViewState
        {
            get { return this.viewState; }
            set { this.viewState = value; }
        }

        [ClientConfig]
        [DefaultValue(null)]
        public string ViewStateEncrypted
        {
            get { return this.viewStateEncrypted; }
            set { this.viewStateEncrypted = value; }
        }

        [ClientConfig]
        [DefaultValue(null)]
        public string EventValidation
        {
            get { return this.eventValidation; }
            set { this.eventValidation = value; }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue(null)]
        public string ServiceResponse
        {
            get { return this.serviceResponse; }
            set { this.serviceResponse = value; }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue(null)]
        public string ExtraParamsResponse
        {
            get { return this.userResponse; }
            set { this.userResponse = value; }
        }

        [DefaultValue(null)]
        public object Result
        {
            get { return this.result; }
            set { this.result = value; }
        }

        [ClientConfig("result", JsonMode.Raw)]
        [DefaultValue(null)]
        protected object ResultProxy
        {
            get
            {
                return (this.Result != null) ? JSON.Serialize(this.Result) : null;
            }
        }
    }
}