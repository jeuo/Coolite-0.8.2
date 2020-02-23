/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class InitScriptFilter : BaseFilter
    {
        private readonly Stream response;
        private readonly StringBuilder html;
        private const string openScriptTag = "<Coolite.InitScript>";
        private const string closeScriptTag = "</Coolite.InitScript>";
        private const string openStyleTag = "<Coolite.InitStyle>";
        private const string closeStyleTag = "</Coolite.InitStyle>";
        private const string openWarningTag = "<Coolite.InitScript.Warning>";
        private const string closeWarningTag = "</Coolite.InitScript.Warning>";
        private const string initScriptPlaceholder = "<Coolite.Ext.Web.InitScriptPlaceholder />";
        private const string initStylePlaceholder = "<Coolite.Ext.Web.InitStylePlaceholder />";
        private const string removeViewStatePattern = "<div>[\\r|\\t|\\s]*<input.*name=\"__EVENTVALIDATION\"[^>].*/>[\\r|\\t|\\s]*</div>|<input.*name=\"__(VIEWSTATE|VIEWSTATEENCRYPTED)\"[^>].*/>|<script[^>]*>[\\w|\\t|\\r|\\W]*?function __doPostBack[\\w|\\t|\\r|\\W]*?</script>";

        public InitScriptFilter(Stream stream)
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
            if (this.html.Length == 0)
            {
                this.response.Flush();
                return;  
            }

            if (ScriptManager.RemoveViewStateStatic)
            {
                this.RemoveViewState();
            }

            this.RemoveWarning();
            this.ReplacePlaceHolder(InitScriptFilter.initScriptPlaceholder, openScriptTag, closeScriptTag);
            this.ReplacePlaceHolder(InitScriptFilter.initStylePlaceholder, openStyleTag, closeStyleTag);

            byte[] data = System.Text.Encoding.UTF8.GetBytes(this.html.ToString());
            this.response.Write(data, 0, data.Length);
            this.response.Flush();
        }

        private void RemoveViewState()
        {
            MatchCollection matches = new Regex(InitScriptFilter.removeViewStatePattern, RegexOptions.IgnoreCase).Matches(this.html.ToString());

            foreach (Match match in matches)
            {
                this.html.Replace(match.Value, "");
            }
        }

        private void RemoveWarning()
        {
            int start = this.html.ToString().IndexOf(openWarningTag);
            if(start >= 0)
            {
                int end = this.html.ToString().IndexOf(closeWarningTag) + closeWarningTag.Length;
                this.html.Remove(start, end - start);
            }
        }

        private void ReplacePlaceHolder(string placeHolderMarker, string openTag, string closeTag)
        {
            string script = StringUtils.LeftOf(StringUtils.RightOf(this.html.ToString(), openTag), closeTag);
            if(!string.IsNullOrEmpty(script))
            {
                int start = this.html.ToString().IndexOf(openTag);
                int end = this.html.ToString().IndexOf(closeTag) + closeTag.Length;
                this.html.Remove(start, end - start);
                this.html.Replace(placeHolderMarker, script);
            }
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