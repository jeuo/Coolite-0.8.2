/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class FormPanelBase : ContentPanel
    {
        /// <summary>
        /// Valid values are "left", "center" and "right" (defaults to "center").
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(Alignment.Center)]
        [Description("Valid values are \"left\", \"center\" and \"right\" (defaults to \"center\").")]
        [NotifyParentProperty(true)]
        public override Alignment ButtonAlign
        {
            get
            {
                object obj = this.ViewState["ButtonAlign"];
                return (obj == null) ? Alignment.Center : (Alignment)obj;
            }
            set
            {
                this.ViewState["ButtonAlign"] = value;
            }
        }

        /// <summary>
        /// (optional) The id of the FORM tag (defaults to an auto-generated id).
        /// </summary>
        [ClientConfig("formId")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) The id of the FORM tag (defaults to an auto-generated id).")]
        [NotifyParentProperty(true)]
        public virtual string FormID
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

        protected virtual string FormIdProxy
        {
            get
            {
                return string.IsNullOrEmpty(this.FormID) ? this.ClientID : this.FormID;
            }
        }

        /// <summary>
        /// A css class to apply to the x-form-item of fields. This property cascades to child containers.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class to apply to the x-form-item of fields. This property cascades to child containers.")]
        [NotifyParentProperty(true)]
        public override string ItemCls
        {
            get
            {
                return (string)this.ViewState["ItemCls"] ?? "";
            }
            set
            {
                this.ViewState["ItemCls"] = value;
            }
        }

        /// <summary>
        /// The default label alignment. The default value is empty string '' for left alignment, but specifying 'top' will align the labels above the fields.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(LabelAlign.Left)]
        [NotifyParentProperty(true)]
        [Description("The default label alignment. The default value is empty string '' for left alignment, but specifying 'top' will align the labels above the fields.")]
        public virtual LabelAlign LabelAlign
        {
            get
            {
                object obj = this.ViewState["LabelAlign"];
                return (obj == null) ? LabelAlign.Left : (LabelAlign)obj;
            }
            set
            {
                this.ViewState["LabelAlign"] = value;
            }
        }

        /// <summary>
        /// The default width in pixels of field labels (defaults to 100).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(100)]
        [NotifyParentProperty(true)]
        [Description("The default width in pixels of field labels (defaults to 100).")]
        public virtual int LabelWidth
        {
            get
            {
                object obj = this.ViewState["LabelWidth"];
                return (obj == null) ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["LabelWidth"] = value;
            }
        }

        /// <summary>
        /// The milliseconds to poll valid state, ignored if monitorValid is not true (defaults to 200)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(200)]
        [NotifyParentProperty(true)]
        [Description("The milliseconds to poll valid state, ignored if monitorValid is not true (defaults to 200)")]
        public virtual int MonitorPoll
        {
            get
            {
                object obj = this.ViewState["MonitorPoll"];
                return (obj == null) ? 200 : (int)obj;
            }
            set
            {
                this.ViewState["MonitorPoll"] = value;
            }
        }

        /// <summary>
        /// If true the form monitors its valid state client-side and fires a looping event with that state. This is required to bind buttons to the valid state using the config value formBind:true on the button.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If true the form monitors its valid state client-side and fires a looping event with that state. This is required to bind buttons to the valid state using the config value formBind:true on the button.")]
        [NotifyParentProperty(true)]
        public virtual bool MonitorValid
        {
            get
            {
                object obj = this.ViewState["MonitorValid"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["MonitorValid"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public override bool IsInForm
        {
            get
            {
                return base.IsInForm;
            }
        }

        /*---- BasicForm properties -------*/

        private ParameterCollection baseParams;

        /// <summary>
        /// Parameters to pass with all requests. e.g. baseParams: {id: '123', foo: 'bar'}.
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Parameters to pass with all requests. e.g. baseParams: {id: '123', foo: 'bar'}.")]
        [ViewStateMember]
        public virtual ParameterCollection BaseParams
        {
            get
            {
                if (this.baseParams == null)
                {
                    this.baseParams = new ParameterCollection();
                    this.baseParams.Owner = this;
                }

                return this.baseParams;
            }
        }

        private ReaderCollection errorReader;

        /// <summary>
        /// An Ext.data.DataReader (e.g. Ext.data.XmlReader) to be used to read data when reading validation errors on "submit" actions. This is completely optional as there is built-in support for processing JSON.
        /// </summary>
        [ClientConfig("reader>Reader")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An Ext.data.DataReader (e.g. Ext.data.XmlReader) to be used to read data when reading validation errors on \"submit\" actions. This is completely optional as there is built-in support for processing JSON.")]
        [ViewStateMember]
        public virtual ReaderCollection ErrorReader
        {
            get
            {
                if (this.errorReader == null)
                {
                    this.errorReader = new ReaderCollection();
                }
                return this.errorReader;
            }
        }

        /// <summary>
        /// Set to true if this form is a file upload.
        /// </summary>
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Set to true if this form is a file upload.")]
        public bool FileUpload
        {
            get
            {
                object obj = this.ViewState["FileUpload"];
                return obj != null ? (bool)obj : false;
            }
            set
            {
                this.ViewState["FileUpload"] = value;
            }
        }

        /// <summary>
        /// The HTTP method to use. Defaults to POST if params are present, or GET if not.
        /// </summary>
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

        [ClientConfig("method")]
        [DefaultValue(HttpMethod.Default)]
        internal virtual HttpMethod MethodProxy
        {
            get
            {
                if (this.Method == HttpMethod.Default && !this.IsInForm)
                {
                    return HttpMethod.POST;
                }
                  
                return this.Method;
            }
        }

        private ReaderCollection reader;

        /// <summary>
        /// An Ext.data.DataReader (e.g. Ext.data.XmlReader) to be used to read data when executing "load" actions. This is optional as there is built-in support for processing JSON.
        /// </summary>
        [ClientConfig("reader>Reader")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An Ext.data.DataReader (e.g. Ext.data.XmlReader) to be used to read data when executing \"load\" actions. This is optional as there is built-in support for processing JSON.")]
        [ViewStateMember]
        public virtual ReaderCollection Reader
        {
            get
            {
                if (this.reader == null)
                {
                    this.reader = new ReaderCollection();
                }
                return this.reader;
            }
        }

        /// <summary>
        /// If set to true, standard HTML form submits are used instead of XHR (Ajax) style form submissions. (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If set to true, standard HTML form submits are used instead of XHR (Ajax) style form submissions. (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool StandardSubmit
        {
            get
            {
                object obj = this.ViewState["StandardSubmit"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StandardSubmit"] = value;
            }
        }

        /// <summary>
        /// Timeout for form actions in seconds (default is 30 seconds).
        /// </summary>
        [ClientConfig]
        [NotifyParentProperty(true)]
        [DefaultValue(30)]
        [Description("Timeout for form actions in seconds (default is 30 seconds).")]
        public int Timeout
        {
            get
            {
                object obj = this.ViewState["Timeout"];
                return obj == null ? 30 : (int)this.ViewState["Timeout"];
            }
            set
            {
                this.ViewState["Timeout"] = value;
            }
        }

        /// <summary>
        /// If set to true, form.reset() resets to the last loaded or setValues() data instead of when the form was first created.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If set to true, form.reset() resets to the last loaded or setValues() data instead of when the form was first created.")]
        [NotifyParentProperty(true)]
        public virtual bool TrackResetOnLoad
        {
            get
            {
                object obj = this.ViewState["TrackResetOnLoad"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TrackResetOnLoad"] = value;
            }
        }

        /// <summary>
        /// The URL to use for form actions if one isn't supplied in the action options.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The URL to use for form actions if one isn't supplied in the action options.")]
        [NotifyParentProperty(true)]
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

        [ClientConfig("url")]
        [DefaultValue("")]
        internal virtual string UrlProxy
        {
            get
            {
                if (string.IsNullOrEmpty(this.Url))
                {
                    if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.RawUrl))
                    {
                        return HttpContext.Current.Request.RawUrl;
                    }
                    
                    return "";
                }

                return (Coolite.Utilities.UrlUtils.IsUrl(this.Url) || this.Page == null) ? this.Url : this.Page.ResolveUrl(this.Url);
            }
        }

        /// <summary>
        /// A CSS style specification string to add to each field element in this layout (defaults to '').
        /// </summary>
        [NotifyParentProperty(true)]
        [Description("A CSS style specification string to add to each field element in this layout (defaults to '').")]
        public virtual string ElementStyle
        {
            get
            {
                return (string)this.ViewState["ElementStyle"] ?? "";
            }
            set
            {
                this.ViewState["ElementStyle"] = value;
            }
        }

        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        [DefaultValue(null)]
        internal FormLayoutProxy LayoutConfig
        {
            get
            {
                return new FormLayoutProxy(this.ElementStyle, this.LabelSeparator, this.LabelStyle,false,"");
            }
        }

        /// <summary>
        /// True to hide field labels by default (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide field labels by default (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool HideLabels
        {
            get
            {
                object obj = this.ViewState["HideLabels"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideLabels"] = value;
            }
        }

        /// <summary>
        /// The default padding in pixels for field labels (defaults to 5). labelPad only applies if labelWidth is also specified, otherwise it will be ignored.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(5)]
        [NotifyParentProperty(true)]
        [Description("The default padding in pixels for field labels (defaults to 5). labelPad only applies if labelWidth is also specified, otherwise it will be ignored.")]
        public virtual int LabelPad
        {
            get
            {
                object obj = this.ViewState["LabelPad"];
                return (obj == null) ? 5 : (int)obj;
            }
            set
            {
                this.ViewState["LabelPad"] = value;
            }
        }
    }
}