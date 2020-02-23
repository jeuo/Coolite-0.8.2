/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web;

namespace Coolite.Ext.Web
{
    public partial class ScriptManager
    {
        private static object Session(string name)
        {
            if (HttpContext.Current.Session != null)
            {
                return HttpContext.Current.Session[name];
            }

            return null;
        }
        
        private string ajaxEventUrl = null;

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Gets or Set the default Url to make all AjaxEvent requests to if no <form> is available on the Page, or AjaxEvent Type='Load'. Can be set at Page level in ScriptManager, Session[\"Coolite.AjaxEventUrl\"], Application[\"Coolite.AjaxEventUrl\"] and web.config.")]
        public virtual string AjaxEventUrl
        {
            get
            {
                if (this.ajaxEventUrl != null)
                {
                    return this.ajaxEventUrl;
                }
                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.AjaxEventUrl";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is string)
                    {
                        return (string)obj;
                    }
                }

                return GlobalConfig.Settings.AjaxEventUrl;
            }
            set
            {
                this.ajaxEventUrl = value;
            }
        }

        private object ajaxViewStateMode = null;

        [Category("Config Options")]
        [DefaultValue(ViewStateMode.Default)]
        [Description("Specifies whether the ViewState should be returned and updated on the client during an AjaxEvent. The Default value is to Exclude the ViewState from the Response. Can be set at Page level in ScriptManager, Session[\"Coolite.AjaxViewStateMode\"], Application[\"Coolite.AjaxViewStateMode\"] and web.config.")]
        public virtual ViewStateMode AjaxViewStateMode
        {
            get
            {
                if (this.ajaxViewStateMode != null)
                {
                    return (ViewStateMode)this.ajaxViewStateMode;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.AjaxViewStateMode";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ViewStateMode)
                    {
                        return (ViewStateMode)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetAjaxViewStateFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.AjaxViewStateMode;
            }
            set
            {
                this.ajaxViewStateMode = value;
            }
        }

        private object ajaxMethodProxy = null;

        [Category("Config Options")]
        [DefaultValue(ClientProxy.Default)]
        [Description("Specifies ajax method proxies creation. The Default value is to Create the proxy for each ajax method. Can be set at Page level in ScriptManager, Session[\"Coolite.AjaxMethodProxy\"], Application[\"Coolite.AjaxMethodProxy\"] and web.config.")]
        public virtual ClientProxy AjaxMethodProxy
        {
            get
            {
                if (this.ajaxMethodProxy != null)
                {
                    return (ClientProxy)this.ajaxMethodProxy;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.AjaxMethodProxy";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ClientProxy)
                    {
                        return (ClientProxy)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetAjaxMethodProxyFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.AjaxMethodProxy;
            }
            set
            {
                this.ajaxMethodProxy = value;
            }
        }
        
        private object idMode = null;

        [Category("Config Options")]
        [DefaultValue(IDMode.Legacy)]
        [Description("Gets or Sets the IDMode. Can be set at Page level in ScriptManager, Session[\"Coolite.IDMode\"], Application[\"Coolite.IDMode\"] and web.config.")]
        public override IDMode IDMode
        {
            get
            {
                if (this.idMode != null)
                {
                    return (IDMode)this.idMode;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.IDMode";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is IDMode)
                    {
                        return (IDMode)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetIDModeFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.IDMode;
            }
            set
            {
                this.idMode = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Specifies whether the Coolite ScriptManager will output GZip Embedded JavaScript and Css Resources. Default is 'True'. Can be set within Session[\"Coolite.GZip\"], Application[\"Coolite.GZip\"] and web.config.")]
        public virtual bool GZip
        {
            get
            {
                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.GZip";
                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.GZip;
            }
        }

        private bool? cleanResourceUrl;

        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Specifies whether the Coolite ScriptManager will output 'clean' Url's when linking to Embedded Resources. Default is 'True'. Can be set at Page level in ScriptManager, Session[\"Coolite.CleanResourceUrl\"], Application[\"Coolite.CleanResourceUrl\"] and web.config.")]
        public virtual bool CleanResourceUrl
        {
            get
            {
                if (this.cleanResourceUrl != null)
                {
                    return (bool)this.cleanResourceUrl;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.CleanResourceUrl";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.CleanResourceUrl;
            }
            set
            {
                this.cleanResourceUrl = value;
            }
        }


        private object initScriptMode = null;

        [Category("Config Options")]
        [DefaultValue(InitScriptMode.Inline)]
        [Description("Gets or Sets the InitScriptMode. Can be set at Page level in ScriptManager, Session[\"Coolite.InitScriptMode\"], Application[\"Coolite.InitScriptMode\"] and web.config.")]
        public virtual InitScriptMode InitScriptMode
        {
            get
            {
                if (this.initScriptMode != null)
                {
                    return (InitScriptMode)this.initScriptMode;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.InitScriptMode";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is InitScriptMode)
                    {
                        return (InitScriptMode)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetInitScriptModeFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.InitScriptMode;
            }
            set
            {
                this.initScriptMode = value;
            }
        }

        private object renderScripts = null;

        [Category("Config Options")]
        [DefaultValue(ResourceLocationType.Embedded)]
        [Description("Determines how or if the required Scripts should be rendered to the Page. Can be set at Page level in ScriptManager, Session[\"Coolite.RenderScripts\"], Application[\"Coolite.RenderScripts\"] and web.config.")]
        public virtual ResourceLocationType RenderScripts
        {
            get
            {
                if (this.renderScripts != null)
                {
                    return (ResourceLocationType)this.renderScripts;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.RenderScripts";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ResourceLocationType)
                    {
                        return (ResourceLocationType)obj;
                    }
                }

                return GlobalConfig.Settings.RenderScripts;
            }
            set
            {
                this.renderScripts = value;
            }
        }

        private object renderStyles = null;

        [Category("Config Options")]
        [DefaultValue(ResourceLocationType.Embedded)]
        [Description("Determines how or if the required Styles should be rendered to the Page. Can be set at Page level in ScriptManager, Session[\"Coolite.RenderStyles\"], Application[\"Coolite.RenderStyles\"] and web.config.")]
        public virtual ResourceLocationType RenderStyles
        {
            get
            {
                if (this.renderStyles != null)
                {
                    return (ResourceLocationType)this.renderStyles;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.RenderStyles";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ResourceLocationType)
                    {
                        return (ResourceLocationType)obj;
                    }
                }

                return GlobalConfig.Settings.RenderStyles;
            }
            set
            {
                this.renderStyles = value;
            }
        }

        private string resourcePath = null;

        [Category("Config Options")]
        [DefaultValue("~/Coolite/")]
        [Description("Gets the prefix of the Url path to the base ~/Coolite/ folder containing the resources files for this project. The path can be Absolute or Relative. Can be set at Page level in ScriptManager, Session[\"Coolite.ResourcePath\"], Application[\"Coolite.ResourcePath\"] and web.config.")]
        public virtual string ResourcePath
        {
            get
            {
                if (this.resourcePath != null)
                {
                    return (string)this.resourcePath;
                }
                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.ResourcePath";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is string)
                    {
                        return (string)obj;
                    }
                }

                return GlobalConfig.Settings.ResourcePath;
            }
            set
            {
                this.resourcePath = value;
            }
        }

        private object scriptMode = null;

        [Category("Config Options")]
        [DefaultValue(ScriptMode.Release)]
        [Description("Specifies whether the Scripts should be rendered in Release or Debug mode. Default is Release. Can be set at Page level in ScriptManager, Session[\"Coolite.ScriptMode\"], Application[\"Coolite.ScriptMode\"] and web.config.")]
        public virtual ScriptMode ScriptMode
        {
            get
            {
                if (this.scriptMode != null)
                {
                    return (ScriptMode)this.scriptMode;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.ScriptMode";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ScriptMode)
                    {
                        return (ScriptMode)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetScriptModeFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.ScriptMode;
            }
            set
            {
                this.scriptMode = value;
            }
        }

        private bool? sourceFormatting;

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Specifies whether the scripts rendered to the page should be formatted. 'True' = formatting, 'False' = minified/compressed. Default is 'False'. Can be set at Page level in ScriptManager, Session[\"Coolite.SourceFormatting\"], Application[\"Coolite.SourceFormatting\"] and web.config.")]
        public virtual bool SourceFormatting
        {
            get
            {
                if (this.sourceFormatting != null)
                {
                    return (bool)this.sourceFormatting;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.SourceFormatting";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.SourceFormatting;
            }
            set
            {
                this.sourceFormatting = value;
            }
        }

        public virtual string GetThemeUrl(Theme theme)
        {
            ResourceLocationType type = this.RenderStyles;

            if(theme == Theme.Default)
            {
                switch (type)
                {
                    case ResourceLocationType.Embedded:
                        return this.GetWebResourceUrl(ScriptManager.ASSEMBLYSLUG + ".extjs.resources.css.ext-all-embedded.css");
                    case ResourceLocationType.File:
                        return this.ConvertToFilePath(ScriptManager.ASSEMBLYSLUG + ".extjs.resources.css.ext-all.css");
                    case ResourceLocationType.CacheFly:
                    case ResourceLocationType.CacheFlyAndFile:
                        return this.GetCacheFlyLink("resources/css/ext-all.css");
                }
            }
            
            foreach (ClientStyleAttribute item in this.GetStyles())
            {
                if (item.Theme.Equals(theme))
                {
                    switch (type)
                    {
                        case ResourceLocationType.Embedded:
                            return this.GetWebResourceUrl(item.Type, item.WebResource);
                        case ResourceLocationType.File:
                            return string.Concat(this.ResourcePath, item.FilePath);
                        case ResourceLocationType.CacheFly:
                            if(string.IsNullOrEmpty(item.CacheFly))
                            {
                                return this.GetWebResourceUrl(item.Type, item.WebResource);
                            }

                            return item.CacheFly;
                        case ResourceLocationType.CacheFlyAndFile:
                            if (string.IsNullOrEmpty(item.CacheFly))
                            {
                                return string.Concat(this.ResourcePath, item.FilePath);
                            }

                            return item.CacheFly;
                    }
                }
            }
            return "";
        }

        public void SetTheme(Theme theme)
        {
            this.Theme = theme;
            base.AddScript("Coolite.Ext.setTheme(\"{0}\");", this.GetThemeUrl(theme));
        }

        private object theme = null;

        [Category("Config Options")]
        [DefaultValue(Theme.Default)]
        [Description("Gets or Sets the current Theme. Can be set at Page level in ScriptManager, Session[\"Coolite.Theme\"], Application[\"Coolite.Theme\"] and web.config.")]
        public virtual Theme Theme
        {
            get
            {
                if (this.theme != null)
                {
                    return (Theme)this.theme;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.Theme";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is Theme)
                    {
                        return (Theme)obj;
                    }
                }

                return GlobalConfig.Settings.Theme;
            }
            set
            {
                this.theme = value;
            }
        }

        private object scriptAdapter = null;

        [Category("Config Options")]
        [DefaultValue(ScriptAdapter.Ext)]
        [Description("Gets or Sets the current script Adapter. Can be set at Page level in ScriptManager, Session[\"Coolite.ScriptAdapter\"], Application[\"Coolite.ScriptAdapter\"] and web.config.")]
        public virtual ScriptAdapter ScriptAdapter
        {
            get
            {
                if (this.scriptAdapter != null)
                {
                    return (ScriptAdapter)this.scriptAdapter;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.ScriptAdapter";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is ScriptAdapter)
                    {
                        return (ScriptAdapter)obj;
                    }
                }
                else
                {
                    return WebConfigUtils.GetScriptAdapterFromWebConfig(this.Site);
                }

                return GlobalConfig.Settings.ScriptAdapter;
            }
            set
            {
                this.scriptAdapter = value;
            }
        }

        private object stateProvider = null;

        [Category("Config Options")]
        [DefaultValue(StateProvider.PostBack)]
        [Description("Gets or Sets the current script Adapter. Can be set at Page level in ScriptManager, Session[\"Coolite.ScriptAdapter\"], Application[\"Coolite.ScriptAdapter\"] and web.config.")]
        public virtual StateProvider StateProvider
        {
            get
            {
                if (this.stateProvider != null)
                {
                    return (StateProvider)this.stateProvider;
                }

                if (this.IsMVC && (this.stateProvider == null || (StateProvider)this.stateProvider == StateProvider.PostBack))
                {
                    this.stateProvider = StateProvider.None;
                    return (StateProvider)this.stateProvider;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.StateProvider";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is StateProvider)
                    {
                        this.stateProvider = (StateProvider)obj;
                        return (StateProvider)this.stateProvider;
                    }
                }
                else
                {
                    this.stateProvider = WebConfigUtils.GetStateProviderFromWebConfig(this.Site);
                    return (StateProvider)this.stateProvider;
                }

                this.stateProvider = GlobalConfig.Settings.StateProvider;
                return (StateProvider)this.stateProvider;
            }
            set
            {
                this.stateProvider = value;
            }
        }

        private bool? quickTips;

        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Specifies whether to render the QuickTips. Provides attractive and customizable tooltips for any element. 'True' = QuickTips enabled, 'False' = QuickTips disabled. Default is 'True'. Can be set at Page level in ScriptManager, Session[\"Coolite.QuickTips\"], Application[\"Coolite.QuickTips\"] and web.config.")]
        public virtual bool QuickTips
        {
            get
            {
                if (this.quickTips != null)
                {
                    return (bool)this.quickTips;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.QuickTips";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.QuickTips;
            }
            set
            {
                this.quickTips = value;
            }
        }

        private string locale;

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Specifies language of the ExtJS resources to use.")]
        public virtual string Locale
        {
            get
            {
                if (this.locale != null)
                {
                    return this.locale;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.Locale";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is string)
                    {

                        return (string)obj;
                    }
                }

                string cfgLocale = GlobalConfig.Settings.Locale;
                if(string.IsNullOrEmpty(cfgLocale))
                {
                    return this.Page != null ? System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() : "";
                }

                return cfgLocale;
            }
            set
            {
                this.locale = value;
            }
        }

        private string ajaxMethodNamespace;

        [Category("Config Options")]
        [DefaultValue("Coolite.AjaxMethods")]
        [Description("Specifies a custom namespace prefix to use for the AjaxMethods.")]
        public virtual string AjaxMethodNamespace
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ajaxMethodNamespace))
                {
                    return this.ajaxMethodNamespace;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.AjaxMethodNamespace";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is string)
                    {
                        return (string)obj;
                    }
                }

                return GlobalConfig.Settings.AjaxMethodNamespace;
            }
            set
            {
                this.ajaxMethodNamespace = value;
            }
        }

        private bool? removeViewState;

        [Category("Config Options")]
        [DefaultValue("Coolite.RemoveViewState")]
        [Description("Remove ViewState data from page's rendering.")]
        public virtual bool RemoveViewState
        {
            get
            {
                if (this.removeViewState != null)
                {
                    return (bool)this.removeViewState;
                }

                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.RemoveViewState";

                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.RemoveViewState;
            }
            set
            {
                this.removeViewState = value;
                if(!this.DesignMode)
                {
                    ScriptManager.RemoveViewStateStatic = value;
                }
            }
        }

        private bool? rethrowAjaxExceptions;

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Rethrow ajax exceptions from catch sections. Default is 'False'. Can be set within Session[\"Coolite.RethrowAjaxExceptions\"], Application[\"Coolite.RethrowAjaxExceptions\"] and web.config.")]
        public virtual bool RethrowAjaxExceptions
        {
            get
            {
                if (this.rethrowAjaxExceptions != null)
                {
                    return (bool)this.rethrowAjaxExceptions;
                }
                if (!this.DesignMode && HttpContext.Current != null)
                {
                    string token = "Coolite.RethrowAjaxExceptions";
                    object obj = HttpContext.Current.Application[token];

                    if (obj == null)
                    {
                        obj = Session(token);
                    }

                    if (obj != null && obj is bool)
                    {
                        return (bool)obj;
                    }
                }

                return GlobalConfig.Settings.RethrowAjaxExceptions;
            }
            set
            {
                this.rethrowAjaxExceptions = value;
            }
        }

    }
}