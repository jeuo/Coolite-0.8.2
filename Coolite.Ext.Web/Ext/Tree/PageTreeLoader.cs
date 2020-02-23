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
    [InstanceOf(ClassName = "Coolite.Ext.PageTreeLoader")]
    public class PageTreeLoader : TreeLoader
    {
        private static readonly object EventNodeLoad = new object();
        public delegate void NodeLoadEventHandler(object sender, NodeLoadEventArgs e);

        [Category("Action")]
        public event NodeLoadEventHandler NodeLoad
        {
            add
            {
                this.Events.AddHandler(EventNodeLoad, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventNodeLoad, value);
            }
        }

        internal virtual void OnNodeLoad(NodeLoadEventArgs e)
        {
            NodeLoadEventHandler handler = (NodeLoadEventHandler)Events[EventNodeLoad];
            if (handler != null)
            {
                handler(this, e);
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
                    if (HttpContext.Current != null)
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

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(HttpMethod.Default)]
        [NotifyParentProperty(true)]
        [Description("The HTTP request method for loading data.")]
        public override HttpMethod RequestMethod
        {
            get
            {
                object obj = this.ViewState["RequestMethod"];
                return obj == null ? HttpMethod.Default : (HttpMethod)obj;
            }
            set
            {
                this.ViewState["RequestMethod"] = value;
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
                        if (this.RequestMethod == HttpMethod.Default && !control.IsInForm)
                        {
                            return HttpMethod.GET;
                        }
                    }
                }
                return this.RequestMethod;
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
}