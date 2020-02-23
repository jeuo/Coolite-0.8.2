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
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [Designer(typeof(WebControlDesigner))]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public abstract partial class WebControl : System.Web.UI.WebControls.WebControl, ICompositeControlDesignerAccessor
    {
        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                this.IsGeneratedID = false;
                base.ID = value;
            }
        }

        public override string ClientID
        {
            get
            {
                switch (this.IDMode)
                {
                    case IDMode.Static:
                        return this.ID;
                    case IDMode.Explicit:
                        return this.IsGeneratedID ? base.ClientID : this.ID;
                    default:
                        return base.ClientID;
                }
            }
        }

        [ClientConfig("id")]
        [DefaultValue("")]
        protected virtual string ClientIDProxy
        {
            get
            {
                if (!this.IsIDRequired && (this.IDMode == IDMode.Ignore || (this.IDMode == IDMode.Explicit && this.IsGeneratedID)))
                {
                    return "";
                }

                return this.ClientID;
            }
        }

        /// <summary>
        /// Options for controlling how the .ClientID property is rendered in the client.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(IDMode.Inherit)]
        [Description("Options for controlling how the .ClientID property is rendered in the client.")]
        [NotifyParentProperty(true)]
        public virtual IDMode IDMode
        {
            get
            {
                object obj = this.ViewState["IDMode"];

                IDMode mode = IDMode.Inherit;

                if (obj != null)
                {
                    mode = (IDMode)obj;
                }
                else
                {
                    WebControl control = this.ParentWebControl;

                    if (control != null)
                    {
                        mode = control.IDMode;
                    }
                }

                if (mode == IDMode.Inherit)
                {
                    try
                    {
                        mode = this.ScriptManager.IDMode;
                    }
                    catch (InvalidOperationException)
                    {
                        /// TODO: should be catching ScriptManagerNotFoundException
                    }
                }

                return mode;
            }
            set
            {
                this.ViewState["IDMode"] = value;
            }
        }

        /// <summary>
        /// This Component's initial configuration specification. Read-only.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("This Component's initial configuration specification. Read-only.")]
        public virtual string InitialConfig
        {
            get
            {
                if (this is Observable)
                {
                    return (this.DesignMode) ? "" : new ClientConfig().Serialize(this);
                }
                return "";
            }
        }

        string clientInitScript = "";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string ClientInitScript
        {
            get
            {
                return (!Ext.IsAjaxRequest) ? (this.IsLazy) ? this.clientInitScript : string.Concat(this.before, this.clientInitScript, this.after) : "";
            }
        }

        protected virtual void OnClientInit()
        {
            this.EnsureChildControls();

            if (!this.DesignMode)
            {
                this.ScriptManager.RegisterInitID(this);

                if (this is Observable)
                {
                    this.clientInitScript = this.IsLazy ? ((Observable)this).InitialConfig : this.GetClientConstructor();
                }
            }
        }

        ScriptManager scriptManager = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScriptManager ScriptManager
        {
            get
            {
                if (this.scriptManager != null)
                {
                    return this.scriptManager;
                }

                if (!this.DesignMode && this.Page != null)
                {
                    this.scriptManager = ScriptManager.GetInstance(this.Page);
                    if (this.scriptManager != null)
                    {
                        return this.scriptManager;
                    }
                }

                if (this is ScriptManager)
                {
                    return this as ScriptManager;
                }

                if (this.Page != null)
                {
                    this.scriptManager = ControlUtils.FindControl(this.Page, "Coolite.Ext.Web.ScriptManagerProxy") as ScriptManager;

                    if (this.scriptManager != null)
                    {
                        return this.scriptManager;
                    }

                    this.scriptManager = (ScriptManager)ControlUtils.FindControl(this.Page, "Coolite.Ext.Web.ScriptManager");

                    if (this.DesignMode && this.scriptManager == null)
                    {
                        this.scriptManager = new ScriptManager();
                        this.Page.Controls.Add(this.scriptManager);
                    }
                }

                if (this.scriptManager == null)
                {
                    throw new InvalidOperationException(string.Concat("The Coolite ScriptManager Control is missing from this Page.",
                                                            Environment.NewLine,
                                                            Environment.NewLine,
                                                            "Please add the following ScriptManager tag inside the <body> or <form runat=\"server\"> of this Page.",
                                                            Environment.NewLine, 
                                                            Environment.NewLine,
                                                            "Example",
                                                            Environment.NewLine,
                                                            Environment.NewLine,
                                                            "    <ext:ScriptManager runat=\"server\" />"));
                }

                return this.scriptManager;
            }
        }

        private string postBackArgument = "";
        protected virtual string PostBackArgument
        {
            get
            {
                return this.postBackArgument;
            }
            set
            {
                this.postBackArgument = value;
            }
        }

        protected virtual string PostBackFunction
        {
            get
            {
                if (this is IAutoPostBack)
                {
                    IAutoPostBack control = (IAutoPostBack)this;

                    if (control.AutoPostBack)
                    {
                        if (control.CausesValidation)
                        {
                            PostBackOptions options = new PostBackOptions(this, this.PostBackArgument);
                            options.ValidationGroup = control.ValidationGroup;

                            options.AutoPostBack = control.AutoPostBack;
                            options.PerformValidation = control.CausesValidation;

                            if (control is Button)
                            {
                                Button button = (Button)control;

                                if (!string.IsNullOrEmpty(button.PostBackUrl))
                                {
                                    options.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(button.PostBackUrl));
                                }
                            }

                            if(Ext.IsIE)
                            {
                                string ps = this.Page.ClientScript.GetPostBackEventReference(options, false);
                                Regex re = new Regex("setTimeout\\('(.+)',(\\s*)\\d+\\)");
                                Match m = re.Match(ps);
                                if(m != null && m.Success)
                                {
                                    ps = m.Groups[1].Value;
                                }
                                ps = ps.Replace("'", "\\'");
                                return string.Format("window.location = 'javascript:{0}';", ps);
                            }
                            else
                            {
                                return string.Concat(this.Page.ClientScript.GetPostBackEventReference(options, false), ";");
                            }
                        }
                        else
                        {
                            return Ext.IsIE ? string.Format("window.location = 'javascript:{0}';", this.Page.ClientScript.GetPostBackEventReference(this, this.PostBackArgument).Replace("'", "\\'"))
                                            : string.Concat(this.Page.ClientScript.GetPostBackEventReference(this, this.PostBackArgument), ";");
                        }
                    }
                }

                return "";
            }
        }

        private SortedList<long,string> proxyScripts = null;
        private SortedList<long, string> ProxyScripts
        {
            get
            {
                if (this.proxyScripts == null)
                {
                    this.proxyScripts = new SortedList<long,string>();
                }
                return this.proxyScripts;
            }
        }

        /// <summary>
        /// Adds the script to be be called on the client.
        /// </summary>
        /// <param name="script">The script</param>
        [Description("Adds the script to be be called on the client.")]
        public virtual void AddScript(string script)
        {
            if (!string.IsNullOrEmpty(script) && !this.IsParentDeferredRender && this.Visible)
            {
                this.ProxyScripts.Add(ScriptManager.ScriptOrderNumber, TokenUtils.ReplaceRawToken(TokenUtils.ParseTokens(script, this)));
            }
        }

        /// <summary>
        /// Adds the script to be be called on the client. The script is formatted using the template and args.
        /// </summary>
        /// <param name="template">The script string template</param>
        /// <param name="args">The arguments to use with the template</param>
        [Description("Adds the script to be be called on the client. The script is formatted using the template and args.")]
        public virtual void AddScript(string template, params object[] args)
        {
            this.AddScript(string.Format(template, args));
        }

        string before = "";

        /// <summary>
        /// Adds the script directly before the ClientInitScript.
        /// </summary>
        /// <param name="script">The script</param>
        [Description("Adds the script directly before the ClientInitScript.")]
        public virtual void AddBeforeClientInitScript(string script)
        {
            this.before += script;
        }

        string after = "";

        /// <summary>
        /// Adds the script directly after the ClientInitScript.
        /// </summary>
        [Description("Adds the script directly after the ClientInitScript.")]
        public virtual void AddAfterClientInitScript(string script)
        {
            this.after += script;
        }

        /// <summary>
        /// Does this object currently represent it's default state.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Does this object currently represent it's default state.")]
        public virtual bool IsDefault
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsLazy
        {
            get
            {
                if (this is Observable && this.Parent is ILazyItems)
                {
                    return (this.Parent as ILazyItems).LazyItems.Contains((Observable)this);
                }
                return false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsLayout
        {
            get
            {
                return this is Layout;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ContainerID
        {
            get
            {
                return string.Concat(this.ClientID, "_Container");
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsInHead
        {
            get
            {
                return ReflectionUtils.IsInTypeOf(this, typeof(System.Web.UI.HtmlControls.HtmlHead));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsInUpdatePanelRefresh
        {
            get
            {
                if (this.MyUpdatePanel != null)
                {
                    return this.ScriptManager.UpdatePanelIDsToRefresh.Contains(this.MyUpdatePanel.UniqueID);
                }
                return false;
            }
        }

        Control myUpdatePanel = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Control MyUpdatePanel
        {
            get
            {
                if (this.myUpdatePanel == null)
                {
                    this.myUpdatePanel = ReflectionUtils.GetTypeOfParent(this, "System.Web.UI.UpdatePanel");
                }
                return this.myUpdatePanel;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsInLayout
        {
            get
            {
                return this.Parent is Layout;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsMVC
        {
            get
            {
                return ReflectionUtils.IsTypeOf(this.Page, "System.Web.Mvc.ViewPage");
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsInForm
        {
            get
            {
                return (this.ParentForm != null);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Web.UI.HtmlControls.HtmlForm ParentForm
        {
            get
            {
                return (HtmlForm)ReflectionUtils.GetTypeOfParent(this, typeof(System.Web.UI.HtmlControls.HtmlForm));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Component ParentComponent
        {
            get
            {
                return (Component)ReflectionUtils.GetTypeOfParent(this, typeof(Component));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WebControl ParentWebControl
        {
            get
            {
                return (WebControl)ReflectionUtils.GetTypeOfParent(this, typeof(WebControl));
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Component ParentComponentNotLazy
        {
            get
            {
                Component parent = this.ParentComponent;

                if (this.IsLazy)
                {
                    while (parent != null && parent.IsLazy)
                    {
                        parent = parent.ParentComponent;
                    }
                }

                return parent;
            }
        }

        internal virtual bool IsDeferredRender
        {
            get
            {
                return false;
            }
        }

        internal virtual bool IsParentDeferredRender
        {
            get
            {
                for (Control parent = this.Parent; parent != null; parent = parent.Parent)
                {
                    if (parent is WebControl)
                    {
                        if (((WebControl)parent).IsDeferredRender)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        protected virtual string ClientInitID
        {
            get
            {
                return string.Concat(this.ClientID, "_ClientInit");
            }
        }

        private bool autoDataBind;
        
        [DefaultValue(false)]
        public virtual bool AutoDataBind
        {
            get
            {
                return this.autoDataBind;
            }
            set
            {
                this.autoDataBind = value;
            }
        }

        public virtual bool HasLayout()
        {
            return false;
        }

        public virtual bool HasContent()
        {
            if (this is IContent)
            {
                return !string.IsNullOrEmpty(((IContent)this).ContentEl);
            }

            return true;
        }


        /*  Styles & Scripts
            -----------------------------------------------------------------------------------------------*/

        public virtual void SetResources()
        {
            if (!this.IsParentDeferredRender)
            {
                this.RegisterBeforeAfterScript();

                this.OnClientInit();

                bool isAsync = Ext.IsMicrosoftAjaxRequest;

                if (isAsync && this.IsInUpdatePanelRefresh && !(this is Layout))
                {
                    if (this is Observable && this.IsLazy)
                    {
                        this.ScriptManager.RegisterClientInitScript(this.ClientInitID, this.ClientInitScript);
                    }
                    else
                    {
                        string destroyCheck = "if(Ext.query(\"div#{0}_Container.AsyncRender\").length>0){{if(Ext.getCmp(\"{0}\") || !Ext.isEmpty(window.{0})){{{0}.destroy();}}{1}}}";
                        this.ScriptManager.RegisterClientInitScript(this.ClientInitID, string.Format(destroyCheck, this.ClientID, this.ClientInitScript));
                    }
                }

                if (!isAsync)
                {
                    if (!(this is Plugin))
                    {
                        this.ScriptManager.RegisterClientInitScript(this.ClientInitID, this.ClientInitScript);
                    }
                }

                if (this is IIcon)
                {
                    foreach (Icon icon in ((IIcon)this).Icons ?? new List<Icon>(0))
                    {
                        if (icon != Icon.None && this.ScriptManager.RenderStyles != ResourceLocationType.None)
                        {
                            this.ScriptManager.RegisterIcon(icon);
                        }
                    }
                }

                Theme theme = this.ScriptManager.Theme;

                foreach (ClientStyleAttribute item in this.GetThemes())
                {
                    if (item.Theme.Equals(theme))
                    {
                        switch (this.ScriptManager.RenderStyles)
                        {
                            case ResourceLocationType.Embedded:
                                this.ScriptManager.RegisterThemeIncludeInternal(item.Type, item.WebResource);
                                break;
                            case ResourceLocationType.File:
                                this.ScriptManager.RegisterThemeIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.FilePath));
                                break;
                            case ResourceLocationType.CacheFly:
                                if (string.IsNullOrEmpty(item.CacheFly))
                                {
                                    this.ScriptManager.RegisterThemeIncludeInternal(item.Type, item.WebResource);
                                }

                                this.ScriptManager.RegisterThemeIncludeInternal(item.WebResource, item.CacheFly);
                                break;
                            case ResourceLocationType.CacheFlyAndFile:
                                if (string.IsNullOrEmpty(item.CacheFly))
                                {
                                    this.ScriptManager.RegisterThemeIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.FilePath));
                                }

                                this.ScriptManager.RegisterThemeIncludeInternal(item.WebResource, item.CacheFly);
                                break;
                        }
                    }
                }

                this.RegisterStyles();
                this.RegisterScripts();
            }

            this.RegisterCustomScripts();
        }

        internal void RegisterStyles()
        {
            Theme theme = this.ScriptManager.Theme;
            foreach (ClientStyleAttribute item in this.GetStyles())
            {
                if (item.Theme.Equals(Theme.Default) && (!item.DefaultOnlyStyle || theme == Theme.Default))
                {
                    switch (this.ScriptManager.RenderStyles)
                    {
                        case ResourceLocationType.Embedded:
                            this.ScriptManager.RegisterClientStyleIncludeInternal(item.Type, item.WebResource);
                            break;
                        case ResourceLocationType.File:
                            this.ScriptManager.RegisterClientStyleIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.FilePath));
                            break;
                        case ResourceLocationType.CacheFly:
                            if (string.IsNullOrEmpty(item.CacheFly))
                            {
                                this.ScriptManager.RegisterClientStyleIncludeInternal(item.Type, item.WebResource);
                            }

                            this.ScriptManager.RegisterClientStyleIncludeInternal(item.WebResource, item.CacheFly);
                            break;
                        case ResourceLocationType.CacheFlyAndFile:
                            if (string.IsNullOrEmpty(item.CacheFly))
                            {
                                this.ScriptManager.RegisterClientStyleIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.FilePath));
                            }

                            this.ScriptManager.RegisterClientStyleIncludeInternal(item.WebResource, item.CacheFly);
                            break;
                    }
                }
            }
        }

        internal void RegisterScripts()
        {
            foreach (ClientScriptAttribute item in this.GetScripts())
            {
                if (this.ScriptManager.RenderScripts == ResourceLocationType.Embedded || this.ScriptManager.RenderScripts == ResourceLocationType.CacheFly)
                {
                    if (this.ScriptManager.ScriptMode == ScriptMode.Release || string.IsNullOrEmpty(item.WebResourceDebug))
                    {
                        this.ScriptManager.RegisterClientScriptIncludeInternal(item.Type, item.WebResource);
                    }
                    else
                    {
                        this.ScriptManager.RegisterClientScriptIncludeInternal(item.Type, item.WebResourceDebug);
                    }
                }
                else if (this.ScriptManager.RenderScripts == ResourceLocationType.File || this.ScriptManager.RenderScripts == ResourceLocationType.CacheFlyAndFile)
                {
                    if (this.ScriptManager.ScriptMode == ScriptMode.Release || string.IsNullOrEmpty(item.PathDebug))
                    {
                        this.ScriptManager.RegisterClientScriptIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.FilePath));
                    }
                    else
                    {
                        this.ScriptManager.RegisterClientScriptIncludeInternal(item.WebResource, string.Concat(this.ScriptManager.ResourcePath, item.PathDebug));
                    }
                }
            }
        }

        internal void RegisterCustomScripts()
        {
            foreach (KeyValuePair<long, string> proxyScript in this.ProxyScripts)
            {
                this.ScriptManager.RegisterOnReadyScript(proxyScript.Key, proxyScript.Value);
            }
        }

        public List<ClientStyleAttribute> GetStyles()
        {
            return this.GetCustomResourceAttributes<ClientStyleAttribute>();
        }

        public List<ClientStyleAttribute> GetThemes()
        {
            List<ClientStyleAttribute> styles = this.GetCustomResourceAttributes<ClientStyleAttribute>();
            List<ClientStyleAttribute> themes = new List<ClientStyleAttribute>(3);
            foreach (ClientStyleAttribute style in styles)
            {
                if (style.Theme != Theme.Default)
                {
                    themes.Add(style);
                }
            }

            return themes;
        }

        public List<ClientScriptAttribute> GetScripts()
        {
            return this.GetCustomResourceAttributes<ClientScriptAttribute>();
        }
    }
}