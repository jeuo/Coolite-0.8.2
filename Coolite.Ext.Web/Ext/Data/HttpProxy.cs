/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// An implementation of DataProxy that reads a data object from a object
    /// configured to reference a certain URL.
    /// 
    /// Note that this class cannot be used to retrieve data from a domain other
    /// than the domain from which the running page was served.
    ///
    /// For cross-domain access to remote data, use a ScriptTagProxy.
    /// 
    /// Be aware that to enable the browser to parse an XML document,
    /// the server must set the Content-Type header in the HTTP response to "text/xml".
    /// </summary>
    [InstanceOf(ClassName = "Ext.data.HttpProxy")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HttpProxy : DataProxy
    {
        /// <summary>
        /// Whether a new request should abort any pending requests. (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Whether a new request should abort any pending requests. (defaults to false)")]
        public virtual bool AutoAbort
        {
            get
            {
                object obj = this.ViewState["AutoAbort"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoAbort"] = value;
            }
        }

        private ParameterCollection headers;

        /// <summary>
        /// An object containing request headers which are added to each request made by this object.
        /// </summary>
        [ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object containing request headers which are added to each request made by this object.")]
        public virtual ParameterCollection Headers
        {
            get
            {
                if (this.headers == null)
                {
                    this.headers = new ParameterCollection();
                }

                return this.headers;
            }
        }

        /// <summary>
        /// True to add a unique cache-buster param to GET requests. (defaults to true)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to add a unique cache-buster param to GET requests. (defaults to true)")]
        public virtual bool DisableCaching
        {
            get
            {
                object obj = this.ViewState["DisableCaching"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["DisableCaching"] = value;
            }
        }

        private ParameterCollection extraParams;

        /// <summary>
        /// An object containing properties which are used as extra parameters to each request made by this object.
        /// </summary>
        [ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object containing properties which are used as extra parameters to each request made by this object.")]
        public virtual ParameterCollection ExtraParams
        {
            get
            {
                if (this.extraParams == null)
                {
                    this.extraParams = new ParameterCollection();
                }

                return this.extraParams;
            }
        }

        /// <summary>
        /// The default HTTP method to be used for requests.
        /// (defaults to undefined; if not set but params are present will use "POST," otherwise "GET.")
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(HttpMethod.Default)]
        [NotifyParentProperty(true)]
        [Description("The default HTTP method to be used for requests.")]
        public virtual HttpMethod Method
        {
            get
            {
                object obj = this.ViewState["Method"];
                return obj == null ? HttpMethod.Default : (HttpMethod)obj;
            }
            set
            {
                this.ViewState["Method"] = value;
            }
        }

        /// <summary>
        /// The timeout in milliseconds to be used for requests. (defaults to 30000)
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(30000)]
        [NotifyParentProperty(true)]
        [Description("The timeout in milliseconds to be used for requests. (defaults to 30000)")]
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
        /// The default URL to be used for requests to the server.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default URL to be used for requests to the server.")]
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

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Send params as JSON object")]
        public virtual bool Json
        {
            get
            {
                object obj = this.ViewState["Json"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Json"] = value;
            }
        }
    }
}
