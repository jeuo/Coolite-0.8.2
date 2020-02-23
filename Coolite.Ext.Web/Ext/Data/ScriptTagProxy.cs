/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// An implementation of DataProxy that reads a data object from a URL which
    /// may be in a domain other than the originating domain of the running page.
    /// 
    /// Note that if you are retrieving data from a page that is in a domain that
    /// is NOT the same as the originating domain of the running page, you must
    /// use this class, rather than HttpProxy.
    /// 
    /// The content passed back from a server resource requested by a ScriptTagProxy
    /// must be executable JavaScript source code because it is used as the source
    /// inside a <script> tag.
    /// 
    /// In order for the browser to process the returned data, the server must wrap
    /// the data object with a call to a callback function, the name of which is
    /// passed as a parameter by the ScriptTagProxy. Below is a Java example for a
    /// servlet which returns data for either a ScriptTagProxy, or an HttpProxy
    /// depending on whether the callback name was passed:
    /// </summary>
    [InstanceOf(ClassName = "Ext.data.ScriptTagProxy")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ScriptTagProxy : DataProxy
    {
        /// <summary>
        /// (Optional) The name of the parameter to pass to the server which tells the server
        /// the name of the callback function set up by the load call to process the returned
        /// data object. Defaults to "callback".
        /// The server-side processing must read this parameter value, and generate javascript
        /// output which calls this named function passing the data object as its only parameter.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("callback")]
        [NotifyParentProperty(true)]
        [Description("The name of the parameter to pass to the server which tells the server the name of the callback function set up by the load call to process the returned data object.")]
        public virtual string CallbackParam
        {
            get
            {
                object obj = this.ViewState["CallbackParam"];
                return obj == null ? "callback" : (string)obj;
            }
            set
            {
                this.ViewState["CallbackParam"] = value;
            }
        }

        /// <summary>
        /// (optional) Defaults to true. Disable caching by adding a unique parameter name to the request.
        /// </summary>
        [ClientConfig("nocache")]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Whether a new request should abort any pending requests. (defaults to false)")]
        public virtual bool NoCache
        {
            get
            {
                object obj = this.ViewState["NoCache"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["NoCache"] = value;
            }
        }

        /// <summary>
        /// (optional) The number of milliseconds to wait for a response. Defaults to 30 seconds.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(30000)]
        [NotifyParentProperty(true)]
        [Description("(optional) The number of milliseconds to wait for a response. Defaults to 30 seconds.")]
        public virtual int Timeout
        {
            get
            {
                object obj = this.ViewState["Timeout"];
                return obj == null ? 30000 : (int)obj;
            }
            set
            {
                this.ViewState["Timeout"] = value;
            }
        }

        /// <summary>
        /// The URL from which to request the data object.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The URL from which to request the data object.")]
        [Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public virtual string Url
        {
            get
            {
                return (string)this.ViewState["Url"] ?? "";
            }
            set
            {
                this.ViewState["Url"] = value;
            }
        }
    }
}
