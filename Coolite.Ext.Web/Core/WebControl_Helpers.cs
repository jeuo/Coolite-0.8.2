/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public partial class WebControl
    {
        /*  About
            -----------------------------------------------------------------------------------------------*/

        internal string Stamp
        {
            get
            {
                if (this.DesignMode)
                {
                    return "";
                }

                return string.Format("{0} [{1}]. Version {2}.", this.ProductName, this.VersionName, this.Version);
            }
        }

        /// <summary>
        /// The product name
        /// </summary>
        [Category("About")]
        public string ProductName
        {
            get { return "Coolite Toolkit for ASP.NET with ExtJS"; }
        }

        /// <summary>
        /// The Version number of this build
        /// </summary>
        [Category("About")]
        public virtual string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        internal bool IsPro
        {
            get { return true; }
        }

        /// <summary>
        /// The version name
        /// </summary>
        [Category("About")]
        public string VersionName
        {
            get { return "Professional Edition"; }
        }

        private bool IsValidLicenseKey
        {
            get { return true; }
        }


        /*  Helpers
            -----------------------------------------------------------------------------------------------*/

        protected bool IsDebugging
        {
            get
            {
                bool result = false;
                if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.IsDebuggingEnabled;
                }

                return result;
            }
        }

        public List<T> GetCustomResourceAttributes<T>() where T : ResourceAttribute
        {
            object[] items = this.GetType().GetCustomAttributes(typeof(T), true);
            List<T> list = new List<T>();

            int position = list.Count;

            foreach (object item in items)
            {
                // Little "sort" operation to ensure that 'Coolite' resources figure at top at partial dependencies list 
                if (((T)item).WebResource.Contains(ScriptManager.ASSEMBLYSLUG))
                {
                    position = 0;
                }

                list.Insert(position, (T)item);
            }


            foreach (object item in items)
            {
                list.Add((T)item);
            }
            return list;
        }

        internal string GetClientConstructor()
        {
            return this.GetClientConstructor(false, null);
        }

        internal string GetClientConstructor(bool instanceOnly)
        {
            return this.GetClientConstructor(instanceOnly, null);
        }

        public string InstanceOf
        {
            get
            {
                object[] attrs = this.GetType().GetCustomAttributes(typeof(InstanceOfAttribute), true);

                if (attrs.Length == 1)
                {
                    string instanceOf = ((InstanceOfAttribute)attrs[0]).ClassName;

                    if (!this.IsInForm)
                    {
                        string ctor = ((InstanceOfAttribute)attrs[0]).NoFormClassName;
                        if (!string.IsNullOrEmpty(ctor))
                        {
                            instanceOf = ctor;
                        }
                    }

                    return instanceOf;
                }

                return "";
            }
        }

        internal virtual string GetClientConstructor(bool instanceOnly, string body)
        {
            if (this is ICustomConfigSerialization)
            {
                Observable parent = this.ParentComponent;
                if (parent == null)
                {
                    parent = (Observable)ReflectionUtils.GetTypeOfParent(this, typeof(Observable));
                }
                return (this as ICustomConfigSerialization).Serialize(parent);
            }

            object[] attrs = this.GetType().GetCustomAttributes(typeof(InstanceOfAttribute), true);

            if (attrs.Length == 1)
            {
                string instanceOf = ((InstanceOfAttribute)attrs[0]).ClassName;

                if (!this.IsInForm)
                {
                    string ctor = ((InstanceOfAttribute)attrs[0]).NoFormClassName;
                    if (!string.IsNullOrEmpty(ctor))
                    {
                        instanceOf = ctor;
                    }
                }

                string template = (instanceOnly) ? "new {1}({2})" : string.Concat((this is Component) ? "" : "this.{0}=", "new {1}({2});");

                return string.Format(template, this.ClientID, instanceOf, body ?? this.InitialConfig);
            }

            return "";
        }

        internal string GetContainerStyleAttribute()
        {
            object[] attrs = this.GetType().GetCustomAttributes(typeof(ContainerStyleAttribute), true);

            if (attrs.Length == 1)
            {
                return ((ContainerStyleAttribute)attrs[0]).Style;
            }

            return "";
        }

        private bool cancelRenderToParameter;

        internal bool CancelRenderToParameter
        {
            get
            {
                return this.cancelRenderToParameter;
            }
            set
            {
                this.cancelRenderToParameter = value;
            }
        }

        internal Unit UnitPixelTypeCheck(object obj, Unit defaultValue, string propertyName)
        {
            Unit temp = (obj == null) ? defaultValue : (Unit)obj;

            if (temp.Type != UnitType.Pixel)
            {
                throw new InvalidCastException(string.Format("The Unit Type for the {0} {1} property must be of Type 'Pixel'. Example: Unit.Pixel(150) or '150px'.", this.ID, propertyName));
            }

            return temp;
        }

        protected virtual bool RemoveContainer
        {
            get
            {
                return false;
            }
        }

        protected virtual HtmlGenericControl CreateContainer()
        {
            return new HtmlGenericControl((this.DesignMode) ? "div" : "div:body");
        }

        private XmlNode submitConfig;

        protected internal XmlNode SubmitConfig
        {
            get
            {
                if (this.submitConfig == null)
                {
                    string submitAjaxEventConfig = this.Page.Request["submitAjaxEventConfig"];
                    if (!string.IsNullOrEmpty(submitAjaxEventConfig))
                    {
                        this.submitConfig = JsonConvert.DeserializeXmlNode(submitAjaxEventConfig);
                    }
                }

                return this.submitConfig;
            }
        }

        private readonly Dictionary<string, object> callbackValues = new Dictionary<string, object>();

        internal Dictionary<string, object> CallbackValues
        {
            get { return callbackValues; }
        }

        public string ResolveUrlLink(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }

            return (UrlUtils.IsUrl(url) || this.Page == null) ? url : this.Page.ResolveUrl(url);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAjaxRequestInitiator
        {
            get
            {
                if (!Ext.IsAjaxRequest)
                {
                    return false;
                }

                if(this.Page == null)
                {
                    return false;
                }

                string _ea = this.Page.Request["__EVENTARGUMENT"];

                if (string.IsNullOrEmpty(_ea))
                {
                    XmlNode eventArgumentNode = this.SubmitConfig.SelectSingleNode("config/__EVENTARGUMENT");
                    if (eventArgumentNode == null)
                    {
                        return false;
                    }

                    _ea = eventArgumentNode.InnerText;

                    if (string.IsNullOrEmpty(_ea))
                    {
                        return false;
                    }
                }

                string[] args = _ea.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (args.Length != 3)
                {
                    return false;
                }

                string controlID = args[0];

                return this.ClientID == controlID;
            }
        }

        protected string ParseTarget(string target)
        {
            string parsedTarget = TokenUtils.ParseTokens(target, this);

            if (TokenUtils.IsRawToken(parsedTarget))
            {
                return TokenUtils.ReplaceRawToken(parsedTarget);
            }

            return string.Concat("\"", parsedTarget, "\"");
        }
    }
}