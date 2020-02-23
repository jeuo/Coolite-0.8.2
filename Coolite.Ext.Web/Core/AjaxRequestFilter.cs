/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.IO;
using System.Text;
using System.Web;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public abstract class BaseFilter : Stream
    {
        protected const string VIEWSTATE = "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"";
        protected const string VIEWSTATEENCRYPTED = "<input type=\"hidden\" name=\"__VIEWSTATEENCRYPTED\" id=\"__VIEWSTATEENCRYPTED\" value=\"";
        protected const string EVENTVALIDATION = "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"";
    }

    public class AjaxRequestFilter : BaseFilter
    {
        private readonly Stream response;
        private readonly StringBuilder html;

        private static string GetHiddenInputValue(string html, string marker)
        {
            string value = null;
            int i = html.IndexOf(marker);
            if (i != -1)
            {
                value = html.Substring(i + marker.Length);
                value = value.Substring(0, value.IndexOf('\"'));
            }
            return value;
        }

        public AjaxRequestFilter(Stream stream)
        {
            this.response = stream;
            this.html = new StringBuilder();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.html.Append(HttpContext.Current.Response.ContentEncoding.GetString(buffer, offset, count));
        }

        public override void Flush()
        {
            if(this.html.ToString().StartsWith("<Coolite.ManualAjaxResponse>"))
            {
                string script = StringUtils.LeftOf(StringUtils.RightOf(this.html.ToString(), "<Coolite.ManualAjaxResponse>"), "</Coolite.ManualAjaxResponse>");
                byte[] rsp = System.Text.Encoding.UTF8.GetBytes(script);
                this.response.Write(rsp, 0, rsp.Length);
                this.response.Flush();
                return;
            }
            
            AjaxResponse ajaxResponse = new AjaxResponse(true);
            HttpContext context = HttpContext.Current;

            string error = context == null ? null : (context.Error != null ? context.Error.ToString() : null);
            
            if(!ScriptManager.AjaxSuccess || !string.IsNullOrEmpty(error))
            {
                ajaxResponse.Success = false;
                if (!string.IsNullOrEmpty(error))
                {
                    ajaxResponse.ErrorMessage = error; 
                }
                else
                {
                    ajaxResponse.ErrorMessage = ScriptManager.AjaxErrorMessage; 
                }
            }
            else
            {
                if (ScriptManager.ReturnViewState)
                {
                    ajaxResponse.ViewState = AjaxRequestFilter.GetHiddenInputValue(this.html.ToString(), VIEWSTATE);
                    ajaxResponse.ViewStateEncrypted = AjaxRequestFilter.GetHiddenInputValue(this.html.ToString(), VIEWSTATEENCRYPTED);
                    ajaxResponse.EventValidation = AjaxRequestFilter.GetHiddenInputValue(this.html.ToString(), EVENTVALIDATION);
                }

                object o = ScriptManager.ServiceResponse;

                if (o is Response)
                {
                    ajaxResponse.ServiceResponse = new ClientConfig().Serialize(o);
                }
                else
                {
                    ajaxResponse.ServiceResponse = o != null ? JSON.Serialize(o) : null;
                }

                if (ScriptManager.ExtraParamsResponse.Count > 0)
                {
                    ajaxResponse.ExtraParamsResponse = ScriptManager.ExtraParamsResponse.ToJson();
                }

                if(ScriptManager.AjaxMethodResult != null)
                {
                    ajaxResponse.Result = ScriptManager.AjaxMethodResult;
                }

                string script = StringUtils.LeftOf(StringUtils.RightOf(this.html.ToString(), "<Coolite.AjaxResponse>"), "</Coolite.AjaxResponse>");
                if(!string.IsNullOrEmpty(script))
                {
                    ajaxResponse.Script = string.Concat("<string>", script);    
                }
            }

            bool isUpload = context != null && Ext.HasInputFieldMarker(context.Request);

            byte[] data = System.Text.Encoding.UTF8.GetBytes((isUpload ? "<textarea>":"") + ajaxResponse.ToString() + (isUpload ? "</textarea>":"") );
            this.response.Write(data, 0, data.Length);
            this.response.Flush();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Close()
        {
            this.response.Close();
        }

        public override long Length
        {
            get { return 0; }
        }

        private long position;

        public override long Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.response.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            this.response.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.response.Read(buffer, offset, count);
        }
    }
}