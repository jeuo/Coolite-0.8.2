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

namespace Coolite.Ext.Web
{
    public class BaseAjaxEvent : BaseListener
    {
        [DefaultValue(false)]
        [ClientConfig]
        [NotifyParentProperty(true)]
        [Description("Only extra params will be added to request. Useful if request has web-service Url")]
        public bool CleanRequest
        {
            get
            {
                bool defValue = this.ScriptManager != null ? (this.ScriptManager.IsMVC && !string.IsNullOrEmpty(this.Url)) : false;
                object obj = this.ViewState["CleanRequest"];
                return obj != null ? (bool)obj : defValue;
            }
            set
            {
                this.ViewState["CleanRequest"] = value;
            }
        }

        [DefaultValue(false)]
        [ClientConfig]
        [NotifyParentProperty(true)]
        [Description("True if the form object is a file upload")]
        public bool IsUpload
        {
            get
            {
                object obj = this.ViewState["IsUpload"];
                return obj != null ? (bool)obj : false;
            }
            set
            {
                this.ViewState["IsUpload"] = value;
            }
        }
        
        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(ViewStateMode.Default)]
        [NotifyParentProperty(true)]
        public ViewStateMode ViewStateMode
        {
            get
            {
                object obj = this.ViewState["ViewStateMode"];
                if (obj == null || ((ViewStateMode)obj) == ViewStateMode.Default)
                {
                    if(HttpContext.Current != null)
                    {
                        ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);
                        if(sm == null)
                        {
                            return ViewStateMode.Default;
                        }
                        return sm.AjaxViewStateMode;
                    }
                    return ViewStateMode.Default;
                }
                else
                {
                    return (ViewStateMode)obj;    
                }
            }
            set
            {
                this.ViewState["ViewStateMode"] = value;
            }
        }

        /// <summary>
        /// The type of AjaxEvent to perform. The 'Submit' type will submit the &lt;form> and 'Load' will make a POST request to url set in the .Url property, or the current url if the .Url property has not been set.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(AjaxEventType.Submit)]
        [NotifyParentProperty(true)]
        [Description("The type of AjaxEvent to perform. The 'Submit' type will submit the &lt;form> and 'Load' will make a POST request to url set in the .Url property, or the current url if the .Url property has not been set.")]
        public AjaxEventType Type
        {
            get
            {
                object obj = this.ViewState["Type"];
                return obj != null ? (AjaxEventType)obj : AjaxEventType.Submit;
            }
            set
            {
                this.ViewState["Type"] = value;
            }
        }

        /// <summary>
        /// The id of the form to submit. If this.ParentForm is not null then this.ParentForm.ClientID is used, else if FormID is empty then FormID is used, else try to find the form in dom tree hierarchy, otherwise the Url of current page is used.
        /// </summary>
        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [Description("The id of the form to submit. If this.ParentForm is not null then this.ParentForm.ClientID is used, else if FormID is empty then FormID is used, else try to find the form in dom tree hierarchy, otherwise the Url of current page is used.")]
        public string FormID
        {
            get
            {
                return (string)this.ViewState["FormID"] ?? "";
            }
            set
            {
                this.ViewState["FormID"] = value;
            }
        }

        /// <summary>
        /// The default URL to be used for requests to the server. (defaults to '')
        /// </summary>
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default URL to be used for requests to the server if AjaxEventType.Request. (defaults to '')")]
        public string Url
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

        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [ClientConfig("url")]
        [Description("The default URL to be used for requests to the server if AjaxEventType.Request. (defaults to '')")]
        internal string UrlProxy
        {
            get
            {
                if(string.IsNullOrEmpty(this.Url))
                {
                    return "";
                }
                return this.Owner.ResolveUrl(this.Url);
            }
        }

        [DefaultValue(HttpMethod.Default)]
        [NotifyParentProperty(true)]
        [Description("The HTTP method to use. Defaults to POST if params are present, or GET if not.")]
        public virtual HttpMethod Method
        {
            get
            {
                object obj = this.ViewState["Method"];
                return (obj == null) ? HttpMethod.Default : (HttpMethod)obj;
            }
            set
            {
                this.ViewState["Method"] = value;
            }
        }

        [DefaultValue(HttpMethod.Default)]
        [ClientConfig("method")]
        internal virtual HttpMethod MethodProxy
        {
            get
            {
                if (this.Owner != null && this.Owner is WebControl)
                {
                    WebControl control = (WebControl)this.Owner;

                    if (control != null)
                    {
                        if (this.Method == HttpMethod.Default && !control.IsInForm)
                        {
                            return HttpMethod.GET;
                        }
                    }
                }
                return this.Method;
            }
        }

        /// <summary>
        /// The timeout in milliseconds to be used for requests. (defaults to 30000)
        /// </summary>
        [ClientConfig]
        [NotifyParentProperty(true)]
        [DefaultValue(30000)]
        [Description("The timeout in milliseconds to be used for requests. (defaults to 30000)")]
        public int Timeout
        {
            get
            {
                object obj = this.ViewState["Timeout"];
                return obj == null ? 30000 : (int)this.ViewState["Timeout"];
            }
            set
            {
                this.ViewState["Timeout"] = value;
            }
        }

        [ClientConfig]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [DefaultValue("")]
        public string FormProxyArg
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }

                string formId = "";

                if (!string.IsNullOrEmpty(this.FormID))
                {
                    formId = this.FormID;
                }
                else if (this.Owner != null && this.Owner.Page.Form != null)
                {
                    formId = this.Owner.Page.Form.ClientID;
                }

                return formId;
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //[Browsable(false)]
        //[DefaultValue("")]
        //[ClientConfig]
        //public string UrlProxyArg
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //        {
        //            return "";
        //        }
        //        return !string.IsNullOrEmpty(this.Url) ? this.Url : HttpContext.Current.Request.Url.ToString();
        //    }
        //}

        private ParameterCollection userParams;

        [ClientConfig(JsonMode.ArrayToObject)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ParameterCollection ExtraParams
        {
            get
            {
                if (this.userParams == null)
                {
                    this.userParams = new ParameterCollection();
                    this.userParams.Owner = this.Owner;
                }
                return this.userParams;
            }
        }

        private EventMask eventMask;

        [ClientConfig(JsonMode.Object)]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public EventMask EventMask
        {
            get
            {
                if (this.eventMask == null)
                {
                    this.eventMask = new EventMask();
                }

                return this.eventMask;
            }
        }
    }


    /*  EventMask
    -----------------------------------------------------------------------------------------------*/

    public class EventMask : LoadMask
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Working...")]
        public override string Msg
        {
            get
            {
                return (string)this.ViewState["Msg"] ?? "Working...";
            }
            set
            {
                this.ViewState["Msg"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(MaskTarget.Page)]
        [NotifyParentProperty(true)]
        public MaskTarget Target
        {
            get
            {
                object obj = this.ViewState["Target"];
                return obj != null ? (MaskTarget)obj : MaskTarget.Page;
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string CustomTarget
        {
            get
            {
                return (string)this.ViewState["CustomTarget"] ?? "";
            }
            set
            {
                this.ViewState["CustomTarget"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        public virtual int MinDelay
        {
            get
            {
                object obj = this.ViewState["MinDelay"];
                return obj != null ? (int)obj : 0;
            }
            set
            {
                this.ViewState["MinDelay"] = value;
            }
        }
    }
}