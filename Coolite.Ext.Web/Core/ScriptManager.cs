/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    [ClientStyle(Type = typeof(ScriptManager), CacheFly = "http://extjs.cachefly.net/ext-2.2.1/resources/css/xtheme-gray.css", FilePath = "/extjs/resources/css/xtheme-gray-embedded.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.extjs.resources.css.xtheme-gray-embedded.css", Theme = Theme.Gray)]
    [ClientStyle(Type = typeof(ScriptManager), FilePath = "/extjs/resources/css/xtheme-slate-embedded.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.extjs.resources.css.xtheme-slate-embedded.css", Theme = Theme.Slate)]
    [ToolboxData("<{0}:ScriptManager runat=\"server\" />")]
    [Designer(typeof(ScriptManagerDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ScriptManager), "Build.Resources.ToolboxIcons.ScriptManager.bmp")]
    [Description("Coolite ScriptManger Control. Required on each Page.")]
    public partial class ScriptManager : WebControl, IPostBackEventHandler, IPostBackDataHandler, IVirtual
    {
        protected override bool RemoveContainer
        {
            get
            {
                return true;
            }
        }

        List<Control> allUpdatePanels = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Control> AllUpdatePanels
        {
            get
            {
                if (this.allUpdatePanels == null)
                {
                    this.allUpdatePanels = ControlUtils.FindControls<Control>(this.Page, "System.Web.UI.UpdatePanel", false);
                }
                return this.allUpdatePanels;
            }
        }

        List<Control> updatePanelsToRefresh = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Control> UpdatePanelsToRefresh
        {
            get
            {
                if (this.updatePanelsToRefresh == null)
                {
                    this.updatePanelsToRefresh = new List<Control>();
                }
                return this.updatePanelsToRefresh;
            }
        }

        List<string> updatePanelIDsToRefresh = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<string> UpdatePanelIDsToRefresh
        {
            get
            {
                if (this.updatePanelIDsToRefresh == null)
                {
                    this.updatePanelIDsToRefresh = new List<string>();
                }
                return this.updatePanelIDsToRefresh;
            }
        }

        Control triggerUpdatePanel = null;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Control TriggerUpdatePanel
        {
            get
            {
                return this.triggerUpdatePanel;
            }
        }

        string triggerUpdatePanelID = "";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string TriggerUpdatePanelID
        {
            get
            {
                return this.triggerUpdatePanelID;
            }
        }

        public virtual void AddUpdatePanelToRefresh(Control updatePanel)
        {
            if (ReflectionUtils.IsTypeOf(updatePanel, "System.Web.UI.UpdatePanel", false))
            {
                if (!this.UpdatePanelIDsToRefresh.Contains(updatePanel.UniqueID))
                {
                    this.UpdatePanelsToRefresh.Add(updatePanel);
                    this.UpdatePanelIDsToRefresh.Add(updatePanel.UniqueID);
                }
            }
        }

        public virtual void RemoveUpdatePanelToRefresh(Control updatePanel)
        {
            if (ReflectionUtils.IsTypeOf(updatePanel, "System.Web.UI.UpdatePanel", false))
            {
                if (this.UpdatePanelIDsToRefresh.Contains(updatePanel.UniqueID))
                {
                    this.UpdatePanelsToRefresh.Remove(updatePanel);
                    this.UpdatePanelIDsToRefresh.Remove(updatePanel.UniqueID);
                }
            }
        }

        private void SetUpdatePanels(Control updatePanel)
        {
            this.triggerUpdatePanel = updatePanel;

            if (this.TriggerUpdatePanel != null)
            {
                this.AddUpdatePanelToRefresh(this.triggerUpdatePanel);

                foreach (Control control in this.AllUpdatePanels)
                {
                    if (!control.UniqueID.Equals(this.TriggerUpdatePanel.UniqueID))
                    {
                        PropertyInfo updateMode = control.GetType().GetProperty("UpdateMode");
                        string mode = updateMode.GetValue(control, null).ToString();

                        if (mode.Equals("Always") || ControlUtils.IsChildOfParent(this.TriggerUpdatePanel, control))
                        {
                            this.AddUpdatePanelToRefresh(control);
                        }
                    }
                }
            }
        }

        private bool isValidationFixRegistered = false;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Ext.IsAjaxRequest && !this.isValidationFixRegistered)
            {
                if (this.Page.Form != null)
                {
                    List<BaseValidator> validators = ControlUtils.FindControls<BaseValidator>(this.Page.Form);

                    foreach (BaseValidator validator in validators)
                    {

                        if (validator.Visible && validator.Enabled)
                        {
                            if (ControlUtils.FindControl(this, validator.ControlToValidate) as WebControl != null)
                            {
                                this.AddScript(string.Concat("document.getElementById(\"", validator.ClientID, "\").enabled=true;"));
                                this.isValidationFixRegistered = true;
                            }
                        }
                    }

                    if (this.isValidationFixRegistered)
                    {
                        this.AddScript("window.ValidatorOnLoad();");
                    }
                }
            }

            foreach (AjaxEvent ajaxEvent in this.CustomAjaxEvents)
            {
                if (!string.IsNullOrEmpty(ajaxEvent.Target))
                {
                    this.CheckClientClick(TokenUtils.ExtractIDs(ajaxEvent.Target));
                }
            }
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            if (Ext.IsMicrosoftAjaxRequest && this.MicrosoftScriptManager != null)
            {
                this.triggerUpdatePanelID = StringUtils.LeftOf(postCollection[this.MicrosoftScriptManager.UniqueID], '|');

                this.SetUpdatePanels(ControlUtils.FindControl(this.Page.Form, this.TriggerUpdatePanelID));
            }

            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent() { }


        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        [Category("Config Options")]
        [DefaultValue(" ")]
        [Description("The message to display in the Window unload confirm dialog. Used in conjunction with WindowUnload Listener.")]
        public virtual string WindowUnloadMsg
        {
            get
            {
                return (string)this.ViewState["WindowUnloadMsg"] ?? " ";
            }
            set
            {
                this.ViewState["WindowUnloadMsg"] = value;
            }
        }


        /*  Design Time
            -----------------------------------------------------------------------------------------------*/

        private bool hideInDesign = false;

        [Category("Design Time")]
        [DefaultValue(false)]
        [Description("Hide the ScriptManager at Design Time.")]
        public virtual bool HideInDesign
        {
            get
            {
                return this.hideInDesign;
            }
            set
            {
                this.hideInDesign = value;
            }
        }

        bool log = false;

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool Log
        {
            get
            {
                return this.log;
            }
            set
            {
                this.log = value;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!this.DesignMode)
            {
                ScriptManager existingInstance = ScriptManager.GetInstance(this.Page);

                if (existingInstance != null && !DesignMode)
                {
                    throw new InvalidOperationException("Only one script manager is allowed");
                }

                this.Page.Items[typeof(ScriptManager)] = this;

                //don't remove it. Required for AjaxEvent
                if (!this.IsMVC)
                {
                    this.Page.ClientScript.GetPostBackEventReference(this, "");
                }
                else
                {
                    HttpContext.Current.Items[typeof (ScriptManager)] = this;
                }

                if (!Ext.IsAjaxRequest)
                {
                    this.Page.PreRenderComplete += Page_PreRenderComplete;
                }
            }
        }

        private bool hasRendered = false;

        protected virtual void Page_PreRenderComplete(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                if ((!this.IsInHead && (this.ScriptContainer == null)) && this.IsNeedRender && this.Page.Header != null)
                {
                    this.Page.Header.Controls.Add(new ScriptContainer());
                }

                if ((!this.IsInHead && (this.StyleContainer == null)) && this.IsNeedRender && this.Page.Header != null)
                {
                    this.Page.Header.Controls.AddAt(0, new StyleContainer());
                }
                this.SetIsLast();
            }
        }

        internal void BaseRenderAction()
        {
            if ((!this.IsInHead || this.ScriptContainer != null || this.StyleContainer != null) && this.IsNeedRender)
            {
                this.AddStyleItem(string.Format("{0}_StyleBlock", this.ClientID), this.BuildStyleBlock(), true);
                this.AddStyleItem(string.Format("{0}_Styles", this.ClientID), this.BuildStyles(), true);
                this.AddStyleItem(string.Format("{0}_Stamp", this.ClientID), string.Format(ScriptManager.CommentTemplate, this.Stamp), true);
                this.AddScriptItem(string.Format("{0}_linbreak", this.ClientID), "\n", false);

                if (!this.DesignMode)
                {
                    this.AddScript(string.Format("{0}_Scripts", this.ClientID), this.BuildScripts());

                    // TODO: COOLITE-INIT
                    if (this.InitScriptMode == InitScriptMode.Linked && !Ext.IsMicrosoftAjaxRequest && this.RenderScripts == ResourceLocationType.Embedded)
                    {
                        string guid = Guid.NewGuid().ToString("N");
                        HttpContext.Current.Session[guid] = this.BuildScriptBlock(false);
                        this.AddScriptItem(string.Format("{0}_ScriptBlock", this.ClientID), string.Format(ScriptManager.ScriptIncludeTemplate, this.ResolveUrl(string.Format("~/coolite/coolite-init-js/coolite.axd?{0}", guid))), false);
                    }
                    else
                    {
                        this.AddScript(string.Format("{0}_ScriptBlock", this.ClientID), this.BuildScriptBlock());
                    }
                }

                this.hasRendered = true;
            }
        }

        private void SetIsLast()
        {
            List<WebControl> widgets = ControlUtils.FindControls<WebControl>(this.Page);

            if (widgets.Count > 0)
            {
                int i = widgets.Count - 1;
                WebControl final = widgets[i--];

                while(!final.Visible && i >= 0)
                {
                    final = widgets[i--];
                }

                if(!final.Visible)
                {
                    return;
                }

                if (final.HasControls())
                {
                    // Might have to drill down and find last WebControl in the chain.
                }

                final.IsLast = true;
            }

            if(HttpContext.Current != null)
            {
                HttpContext.Current.Items[ScriptManager.FilterMarker] = true;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.SetIsLast();
            base.Render(writer);
        }

        internal void RenderAction(HtmlTextWriter writer)
        {
            if(this.DesignMode)
            {
                writer.Write(string.Concat(string.Format(ScriptManager.CommentTemplate, this.Stamp), this.BuildStyles(), this.BuildStyleBlock()));
                return;
            }

            if (!this.IsMVC)
            {
                this.Page.ClientScript.RegisterForEventValidation(this.UniqueID);
            }

            if (!this.hasRendered && this.IsNeedRender)
            {
                if (!Ext.IsAjaxRequest)
                {
                    writer.Write(this.BuildAll());
                }
                else
                {
                    HttpResponse response = HttpContext.Current.Response;

                    // Used to catch Response.Redirect() during a callback. If it is a redirect
                    // the response is converted back into a normal response and the appropriate
                    // javascript is returned to redirect the client.
                    if (Ext.IsAjaxRequest && response.StatusCode == 302)
                    {
                        string href = response.RedirectLocation.Replace("\\", "\\\\").Replace("'", "\\'");
                        response.RedirectLocation = string.Empty;
                        response.Clear();
                        response.StatusCode = 200;
                        this.AddScript("redirect", "window.location='" + href + "';");
                    }

                    writer.Write("<Coolite.AjaxResponse>");
                    writer.Write(this.BuildScriptBlock(false));
                    writer.Write("</Coolite.AjaxResponse>");
                }
            }
            else if (this.scriptBuilder.Length > 0 || this.styleBuilder.Length > 0)
            {
                writer.Write("<Coolite.InitScript>");
                writer.Write(this.scriptBuilder.ToString());
                writer.Write("</Coolite.InitScript>");
                writer.Write("<Coolite.InitStyle>");
                writer.Write(this.styleBuilder.ToString());
                writer.Write("</Coolite.InitStyle>");
                writer.Write(ScriptManager.WarningTemplate);
            }
        }

        private bool IsNeedRender
        {
            get
            {
                return !this.IsProxy || this.DesignMode;
            }
        }

        private bool IsProxy
        {
            get
            {
                return ReflectionUtils.IsTypeOf(this, "Coolite.Ext.Web.ScriptManagerProxy");
            }
        }


        /*  ScriptManager and ClientStyle Templates
            -----------------------------------------------------------------------------------------------*/

        public const string WarningTemplate = "<Coolite.InitScript.Warning><script type=\"text/javascript\">Ext.onReady(function() {Ext.Msg.show({title: 'Warning',msg: 'The <code>web.config</code> file for this project is missing the required AjaxRequestModule.<br /><br /><div style=\"margin-left:48px;\"><b>Example</b><br /><br /><code>&lt;system.web><br />&nbsp;&nbsp;&lt;httpModules><br />&nbsp;&nbsp;&nbsp;&nbsp;&lt;add name=\"AjaxRequestModule\" type=\"Coolite.Ext.Web.AjaxRequestModule, Coolite.Ext.Web\" /><br />&nbsp;&nbsp;&lt;/httpModules><br />&lt;/system.web></code><br /><br />More information available at \"<a href=\"http://examples.coolite.com/?/Getting_Started/Introduction/Overview/\">Getting Started</a>\".</div><br />',buttons: Ext.Msg.OK,icon: Ext.MessageBox.WARNING});});</script></Coolite.InitScript.Warning>";
        public const string OnReadyTemplate = "Ext.onReady(function(){{{0}}});";
        public const string OnTextResizeTemplate = "Ext.EventManager.onTextResize({0});";
        public const string OnWindowResizeTemplate = "Ext.EventManager.onWindowResize({0});";
        public const string ScriptBlockTemplate = "\t<script type=\"text/javascript\">\n\t//<![CDATA[\n\t\t{0}\n\t//]]>\n\t</script>\n";
        public const string SimpleScriptBlockTemplate = "<script type=\"text/javascript\">{0}</script>";
        public const string ScriptIncludeTemplate = "\t<script type=\"text/javascript\" src=\"{0}\"></script>\n";
        public const string StyleBlockTemplate = "\t<style type=\"text/css\">\n{0}\t</style>\n";
        public const string StyleBlockItemTemplate = "\t\t{0}\n";
        public const string StyleIncludeTemplate = "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />\n";
        public const string ThemeIncludeTemplate = "\t<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" id=\"ext-theme\" />\n";
        public const string CommentTemplate = "\n\t<!-- {0} -->\n";
        public const string FunctionTemplate = "function(){{{0}}}";
        public const string FunctionTemplateWithParams = "function({0}){{{1}}}";
        public const string FilterMarker = "CooliteInitScriptFilter";


        /*  InstanceScript
            -----------------------------------------------------------------------------------------------*/

        public const string INSTANCESCRIPT = "Ext.ScriptManager.InstanceScript";

        public static void AddInstanceScript(string script)
        {
            string temp = (HttpContext.Current.Items[ScriptManager.INSTANCESCRIPT] ?? "") as string;
            HttpContext.Current.Items[ScriptManager.INSTANCESCRIPT] = string.Concat(temp, script);
        }

        public static void AddInstanceScript(string template, params object[] args)
        {
            ScriptManager.AddInstanceScript(string.Format(template, args));
        }

        internal static string GetInstanceScript()
        {
            return (HttpContext.Current.Items[ScriptManager.INSTANCESCRIPT] ?? "") as string;
        }


        /*  Build
            -----------------------------------------------------------------------------------------------*/

        public string GetCacheFlyLink(string relativePath)
        {
            return string.Concat("http://extjs.cachefly.net/ext-2.2.1/", relativePath);
        }

        internal void RegisterAjaxEvents()
        {
            if (Ext.IsAjaxRequest)
            {
                return;
            }
            ScriptManager realManager = this;
            bool isProxy = false;
            if (realManager is ScriptManagerProxy)
            {
                realManager = this.ScriptManager;
                isProxy = true;
            }


            foreach (AjaxEvent ajaxEvent in this.CustomAjaxEvents)
            {
                string configObject = new ClientConfig().SerializeInternal(ajaxEvent, ajaxEvent.Owner);

                if (string.IsNullOrEmpty(ajaxEvent.Target))
                {
                    throw new InvalidOperationException("The Target should be defined for each AjaxEvent.");
                }

                string target = "";

                Control control = ControlUtils.FindControl(this, ajaxEvent.Target);

                if (control != null)
                {
                    target = control is Observable ? control.ClientID : string.Concat("Ext.get(\"", control.ClientID, "\")");
                }
                else
                {
                    string temp = TokenUtils.ParseAndNormalize(ajaxEvent.Target, this);
                    target = target.StartsWith("Ext.get(") ? temp : string.Concat("Ext.get(", temp, ")");
                }

                StringBuilder cfgObj = new StringBuilder(configObject.Length + 64);

                cfgObj.Append(configObject);
                cfgObj.Remove(cfgObj.Length - 1, 1);

                
                
                cfgObj.AppendFormat("{0}control:{1}, eventType: \"{2}\"", configObject.Length > 2 ? "," : "", target, isProxy ? AjaxRequestType.Proxy.ToString().ToLower() : AjaxRequestType.Custom.ToString().ToLower());

                if (isProxy)
                {
                    cfgObj.AppendFormat(",proxyId:\"{0}\"", this.ClientID);
                }

                if (ajaxEvent.EventID != "Click")
                {
                    cfgObj.AppendFormat(",action:\"{0}\"", ajaxEvent.EventID);
                }

                cfgObj.Append("}");

                JFunction jFunction = new JFunction(string.Format("var params=arguments;Coolite.AjaxEvent.request({0});", cfgObj.ToString()));

                HandlerConfig cfg = ajaxEvent.GetListenerConfig();
                string scope = string.IsNullOrEmpty(ajaxEvent.Scope) || ajaxEvent.Scope == "this" ? "undefined" : ajaxEvent.Scope;

                realManager.RegisterOnReadyScript(string.Concat(
                                                "Coolite.Ext.on(",
                                                target,
                                                ",\"",
                                                string.IsNullOrEmpty(ajaxEvent.EventName) ? "click" : ajaxEvent.EventName.ToLower(),
                                                "\",",
                                                jFunction.ToString(), ",", scope, ",\"ajax\",", cfg.ToJsonString(), ");"));
            }
        }

        private void CheckClientClick(List<string> targets)
        {
            foreach (string target in targets)
            {
                Control c = ControlUtils.FindControl(this, target, true);
                if (c == null)
                {
                    return;
                }

                PropertyInfo clientClick = c.GetType().GetProperty("OnClientClick", typeof(string));
                if (clientClick == null)
                {
                    return;
                }

                string currentValue = (clientClick.GetValue(c, null) as string) ?? "";

                if (currentValue.EndsWith("return false;"))
                {
                    return;
                }

                clientClick.SetValue(c, string.Concat(currentValue, (!string.IsNullOrEmpty(currentValue) && !currentValue.EndsWith(";")) ? ";" : "", "return false;"), null);
            }
        }

        internal JFunction GetAjaxEventJFunc(ComponentAjaxEvent ajaxEvent, string name)
        {
            string configObject = new ClientConfig().SerializeInternal(ajaxEvent, ajaxEvent.Owner);

            StringBuilder cfgObj = new StringBuilder(configObject.Length + 64);

            cfgObj.Append(configObject);
            cfgObj.Remove(cfgObj.Length - 1, 1);
            cfgObj.AppendFormat("{0}control:{2}, action:\"{1}\"", configObject.Length > 2 ? "," : "", name, this is ScriptManagerProxy ? "{proxyId:\"" + this.ClientID + "\"}" : "\"-\"");
            cfgObj.Append("}");

            return new JFunction(string.Concat("var params=arguments;Coolite.AjaxEvent.request(", cfgObj.ToString(), ");"));
        }

        private void RegisterScriptManagerAjaxEvents(ScriptManager manager, ScriptManager realManager)
        {
            if (!manager.AjaxEvents.DocumentReady.IsDefault)
            {
                JFunction jFunction = manager.GetAjaxEventJFunc(manager.AjaxEvents.DocumentReady, "DocumentReady");

                realManager.RegisterOnReadyScript(jFunction.Handler);
            }

            if (!manager.AjaxEvents.WindowScroll.IsDefault)
            {
                JFunction jFunction = manager.GetAjaxEventJFunc(manager.AjaxEvents.WindowScroll, "WindowScroll");

                realManager.RegisterClientScriptBlock(string.Format("{0}_WindowScroll", manager.ClientID), string.Format("Ext.EventManager.on(window,\"scroll\",function(e){{{0}}},window,{{buffer: 50}});", jFunction.ToString()));
            }

            if (!manager.AjaxEvents.WindowUnload.IsDefault)
            {
                JFunction jFunction = manager.GetAjaxEventJFunc(manager.AjaxEvents.WindowUnload, "WindowUnload");

                realManager.RegisterClientScriptBlock(string.Format("{0}_WindowUnload", manager.ClientID), string.Format("Ext.EventManager.on(window,\"beforeunload\",function(e){{var coolite_windowBeforeUnload=function(e){{{0}}};if(coolite_windowBeforeUnload()){{e.browserEvent.returnValue=\"{1}\";}}}},window);", jFunction.ToString(), manager.WindowUnloadMsg));
            }

            if (!manager.AjaxEvents.WindowResize.IsDefault)
            {
                JFunction jFunction = manager.GetAjaxEventJFunc(manager.AjaxEvents.WindowResize, "WindowResize");

                realManager.RegisterOnWindowResizeScript(string.Format("{0}_WindowResize", manager.ClientID), jFunction.ToString());
            }

            if (!manager.AjaxEvents.TextResize.IsDefault)
            {
                JFunction jFunction = manager.GetAjaxEventJFunc(manager.AjaxEvents.TextResize, "TextResize");

                realManager.RegisterOnTextResizeScript(string.Format("{0}_TextResize", manager.ClientID), jFunction.ToString());
            }
        }

        internal void RegisterCustomListeners()
        {
            if (Ext.IsAjaxRequest)
            {
                return;
            }

            ScriptManager realManager = this;
            if (realManager is ScriptManagerProxy)
            {
                realManager = this.ScriptManager;
            }

            foreach (Listener listener in this.CustomListeners)
            {
                string function;
                if (!listener.IsDefault)
                {
                    function = new ClientConfig().Serialize(listener);
                }
                else
                {
                    continue;
                }

                if (string.IsNullOrEmpty(listener.Target))
                {
                    throw new InvalidOperationException("The target should be define in custom listener event");
                }

                string target = TokenUtils.ParseAndNormalize(listener.Target, this);

                realManager.RegisterOnReadyScript(string.Format("Coolite.Ext.on({0},\"{1}\",{2}, this, \"client\");", target, string.IsNullOrEmpty(listener.EventName) ? "click" : listener.EventName.ToLower(), function));
            }
        }

        public virtual string BuildAll()
        {
            return string.Concat(string.Format(ScriptManager.CommentTemplate, this.Stamp), this.BuildStyles(), this.BuildStyleBlock(), this.BuildScripts(), this.BuildScriptBlock());
        }

        public virtual string BuildStyles()
        {
            StringBuilder source = new StringBuilder(256);

            if (!Ext.IsMicrosoftAjaxRequest)
            {
                ResourceLocationType type = this.RenderStyles;

                if (type != ResourceLocationType.None)
                {
                    if (type == ResourceLocationType.Embedded)
                    {
                        if (this.DesignMode)
                        {
                            source.Append(string.Format(ScriptManager.StyleBlockTemplate, this.ParseCssWebResourceUrls(this.GetWebResourceAsString(ScriptManager.ASSEMBLYSLUG + ".extjs.resources.css.ext-all-embedded.css"))));
                        }
                        else
                        {
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, this.GetWebResourceUrl(ScriptManager.ASSEMBLYSLUG + ".extjs.resources.css.ext-all-embedded.css")));
                        }

                        foreach (KeyValuePair<string, string> item in this.ThemeIncludeInternalBag)
                        {
                            if (this.DesignMode && item.Key.StartsWith(ScriptManager.ASSEMBLYSLUG))
                            {
                                source.Append(string.Format(ScriptManager.StyleBlockTemplate, this.ParseCssWebResourceUrls(this.GetWebResourceAsString(item.Key))));
                            }
                            else
                            {
                                source.Append(string.Format(ScriptManager.ThemeIncludeTemplate, item.Value));
                            }
                        }


                        foreach (KeyValuePair<string, string> item in this.ClientStyleIncludeInternalBag)
                        {
                            if (this.DesignMode && item.Key.StartsWith(ScriptManager.ASSEMBLYSLUG))
                            {
                                source.Append(string.Format(ScriptManager.StyleBlockTemplate, this.ParseCssWebResourceUrls(this.GetWebResourceAsString(item.Key))));
                            }
                            else
                            {
                                source.Append(string.Format(ScriptManager.StyleIncludeTemplate, item.Value));
                            }
                        }

                        foreach (KeyValuePair<string, string> item in this.ClientStyleIncludeBag)
                        {
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, item.Value));
                        }

                    }
                    else if (type == ResourceLocationType.File || type == ResourceLocationType.CacheFly || type == ResourceLocationType.CacheFlyAndFile)
                    {
                        if (type == ResourceLocationType.File)
                        {
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, this.ConvertToFilePath(ScriptManager.ASSEMBLYSLUG + ".extjs.resources.css.ext-all.css")));
                        }
                        else
                        {
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, this.GetCacheFlyLink("resources/css/ext-all.css")));
                        }

                        foreach (KeyValuePair<string, string> item in this.ThemeIncludeInternalBag)
                        {
                            string name = item.Value;
                            if (item.Value.EndsWith("-embedded.css"))
                            {
                                name = name.Replace("-embedded.css", ".css");
                            }
                            if (item.Value.StartsWith(ScriptManager.ASSEMBLYSLUG))
                            {
                                name = this.ConvertToFilePath(name);
                            }
                            source.Append(string.Format(ScriptManager.ThemeIncludeTemplate, name));
                        }

                        foreach (KeyValuePair<string, string> item in this.ClientStyleIncludeInternalBag)
                        {
                            string name = item.Value;
                            if (item.Value.EndsWith("-embedded.css"))
                            {
                                name = name.Replace("-embedded.css", ".css");
                            }
                            if (item.Value.StartsWith(ScriptManager.ASSEMBLYSLUG))
                            {
                                name = this.ConvertToFilePath(name);
                            }
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, name));
                        }

                        foreach (KeyValuePair<string, string> item in this.ClientStyleIncludeBag)
                        {
                            source.Append(string.Format(ScriptManager.StyleIncludeTemplate, item.Value));
                        }
                    }
                }
            }

            return source.ToString();
        }

        public virtual string BuildStyleBlock()
        {
            if (this.ClientStyleBlockBag.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder(256);

            foreach (KeyValuePair<string, string> item in this.ClientStyleBlockBag)
            {
                sb.Append(string.Format(ScriptManager.StyleBlockItemTemplate, item.Value));
            }

            return string.Format(ScriptManager.StyleBlockTemplate, sb.ToString());
        }

        public virtual string BuildScripts()
        {
            StringBuilder source = new StringBuilder(256);

            ResourceLocationType type = this.RenderScripts;

            if (type != ResourceLocationType.None)
            {
                List<string> items = new List<string>();

                if (this.DesignMode)
                {
                    items.Add(".coolite.intellisense.js");
                }

                if (!Ext.IsMicrosoftAjaxRequest)
                {
                    if (type == ResourceLocationType.CacheFly || type == ResourceLocationType.CacheFlyAndFile)
                    {
                        switch (this.ScriptAdapter)
                        {
                            case ScriptAdapter.Ext:
                                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("adapter/ext/ext-base.js")));
                                break;
                            case ScriptAdapter.jQuery:
                                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("adapter/jquery/ext-jquery-adapter.js")));
                                break;
                            case ScriptAdapter.Prototype:
                                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("adapter/prototype/ext-prototype-adapter.js")));
                                break;
                            case ScriptAdapter.YUI:
                                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("adapter/yui/ext-yui-adapter.js")));
                                break;
                        }

                        if (this.ScriptMode == ScriptMode.Debug)
                        {
                            source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("ext-all-debug.js")));
                        }
                        else
                        {
                            source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetCacheFlyLink("ext-all.js")));
                        }
                    }
                    else
                    {
                        switch (this.ScriptAdapter)
                        {
                            case ScriptAdapter.Ext:
                                items.Add(".extjs.adapter.ext.ext-base.js");
                                break;
                            case ScriptAdapter.jQuery:
                                items.Add(".extjs.adapter.jquery.ext-jquery-adapter.js");
                                break;
                            case ScriptAdapter.Prototype:
                                items.Add(".extjs.adapter.prototype.ext-prototype-adapter.js");
                                break;
                            case ScriptAdapter.YUI:
                                items.Add(".extjs.adapter.yui.ext-yui-adapter.js");
                                break;
                        }

                        if (this.ScriptMode == ScriptMode.Debug)
                        {
                            items.Add(".extjs.ext-all-debug.js");
                        }
                        else
                        {
                            items.Add(".extjs.ext-all.js");
                        }
                    }

                    if (this.ScriptMode == ScriptMode.Debug)
                    {
                        items.Add(".coolite.coolite-core-debug.js");
                    }
                    else
                    {
                        items.Add(".coolite.coolite-core.js");
                    }
                }

                switch (type)
                {
                    case ResourceLocationType.Embedded:
                    case ResourceLocationType.CacheFly:
                        foreach (string item in items)
                        {
                            source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetWebResourceUrl(ScriptManager.ASSEMBLYSLUG + item)));
                        }
                        break;
                    case ResourceLocationType.File:
                    case ResourceLocationType.CacheFlyAndFile:
                        foreach (string item in items)
                        {
                            source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.ConvertToFilePath(ScriptManager.ASSEMBLYSLUG + item)));
                        }
                        break;
                }
                
                this.RegisterLocale(source);
            }

            foreach (KeyValuePair<string, string> item in this.ClientScriptIncludeInternalBag)
            {
                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, item.Value));
            }

            foreach (KeyValuePair<string, string> item in this.ClientScriptIncludeBag)
            {
                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, item.Value));
            }

            return source.ToString();
        }

        private void RegisterLocale(StringBuilder source)
        {
            if(this.Locale.Equals("ignore", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (HttpContext.Current != null && this.Locale.Equals("client", StringComparison.InvariantCultureIgnoreCase))
            {
                string lang = HttpContext.Current.Request.UserLanguages[0];

                if (lang != null)
                {
                    if (lang.Length < 3)
                    {
                        lang = lang + "-" + lang.ToUpper();
                    }

                    this.Locale = lang;
                }
            }
            
            bool isParent;
            if (ScriptManager.IsSupportedCulture(this.Locale, out isParent))
            {
                if(this.Locale == "en" || this.Locale == "en-US")
                {
                    return;
                }
                string cultureName = isParent ? this.Locale.Split(new char[]{'-'})[0] : this.Locale;
                //string url = HttpUtility.HtmlAttributeEncode(this.Page.ClientScript.GetWebResourceUrl(this.GetType(), ScriptManager.ASSEMBLYSLUG + string.Concat(".extjs.locale.ext-lang-", cultureName, ".js")));
                source.Append(string.Format(ScriptManager.ScriptIncludeTemplate, this.GetWebResourceUrl(ScriptManager.ASSEMBLYSLUG + string.Concat(".extjs.locale.ext-lang-", cultureName, ".js"))));
            }
        }

        private class AjaxMethodList: List<AjaxMethod>
        {
        }

        public virtual string BuildAjaxMethodProxies()
        {
            HttpContext context = HttpContext.Current;
            Dictionary<string, Dictionary<string, AjaxMethodList>> methods = this.GroupAjaxMethodsByNamespace();
            StringBuilder sb = new StringBuilder(256);

            foreach (KeyValuePair<string, Dictionary<string, AjaxMethodList>> ns in methods)
            {
                string nsName = ns.Key;

                if (!nsName.Equals("Coolite.AjaxMethods"))
                {
                    sb.AppendFormat("Ext.ns(\"{0}\");", nsName);
                }
                
                Dictionary<string, AjaxMethodList> scopes = ns.Value;

                sb.Append(string.Concat("Ext.apply(", nsName, ", { "));

                foreach (KeyValuePair<string, AjaxMethodList> scope in scopes)
                {
                    string scopeName = scope.Key;
                    AjaxMethodList ajaxMethods = scope.Value;

                    if(!string.IsNullOrEmpty(scopeName))
                    {
                        sb.Append(scopeName);
                        sb.Append(":{");
                    }

                    bool needComma = false;
                    
                    foreach (AjaxMethod method in ajaxMethods)
                    {
                        if (needComma)
                        {
                            sb.Append(",");
                        }
                        method.GenerateProxy(sb, method.ControlID);
                        needComma = true;
                    }

                    if (!string.IsNullOrEmpty(scopeName))
                    {
                        sb.Append("}");
                    }

                    sb.Append(",");
                }
                
                if(sb[sb.Length-1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                
                sb.Append(" });");
            }

            return sb.ToString();
        }

        private Dictionary<string, Dictionary<string, AjaxMethodList>> GroupAjaxMethodsByNamespace()
        {
            Dictionary<string, Dictionary<string, AjaxMethodList>> methods = new Dictionary<string, Dictionary<string, AjaxMethodList>>();
            
            HttpContext context = HttpContext.Current;
            HandlerMethods handler = HandlerMethods.GetHandlerMethodsByType(context, this.Page.GetType(), this.Page.TemplateSourceDirectory, false);
            GroupAjaxMethodsControl(methods, handler, this.Page);

            List<UserControl> userControls = ControlUtils.FindControls<UserControl>(this.Page);
            if (userControls.Count > 0)
            {
                foreach (UserControl userControl in userControls)
                {
                    handler = HandlerMethods.GetHandlerMethodsByType(context, userControl.GetType(), userControl.TemplateSourceDirectory, false);
                    GroupAjaxMethodsControl(methods, handler, userControl);
                }
            }

            return methods;
        }

        private static void GroupAjaxMethodsControl(Dictionary<string, Dictionary<string, AjaxMethodList>> methods, HandlerMethods handler, Control control)
        {
            foreach (AjaxMethod method in handler.StaticMethods)
            {
                AddMethodToGroup(method, methods, control);
            }

            foreach (AjaxMethod method in handler.InstanceMethods)
            {
                AddMethodToGroup(method, methods, control);
            }
        }

        private static void AddMethodToGroup(AjaxMethod method, Dictionary<string, Dictionary<string, AjaxMethodList>> methods, Control control)
        {
            if (method.Attribute.ClientProxy == ClientProxy.Ignore)
            {
                return;
            }

            string ns = method.Attribute.Namespace;
            
            if(!methods.ContainsKey(ns))
            {
                methods[ns] = new Dictionary<string, AjaxMethodList>();
            }
            
            string name = GetControlIdentification(control) ?? "";
            
            if (!methods[ns].ContainsKey(name))
            {
                methods[ns][name] = new AjaxMethodList();
            }
            method.ControlID = control is Page ? null : control.ClientID;
            methods[ns][name].Add(method);
        }

        internal static string GetControlIdentification(Control control)
        {
            object[] attrs = control.GetType().GetCustomAttributes(typeof(AjaxMethodProxyIDAttribute), true);

            AjaxMethodProxyIDAttribute attr = null;
            
            if (attrs.Length == 1)
            {
                attr = (AjaxMethodProxyIDAttribute)attrs[0];
            }

            if (attr == null)
            {
                if (control is Page)
                {
                    return null;
                }

                return control.ClientID;
            }

            switch (attr.IDMode)
            {
                case AjaxMethodProxyIDMode.None:
                    return null;
                case AjaxMethodProxyIDMode.ClientID:
                    return control.ClientID;
                case AjaxMethodProxyIDMode.ID:
                    return control.ID;
                case AjaxMethodProxyIDMode.Alias:
                    return attr.Alias;
                case AjaxMethodProxyIDMode.AliasPlusID:
                    return attr.Alias + control.ID;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public virtual string BuildScriptBlock()
        {
            return this.BuildScriptBlock(true);
        }

        public virtual string BuildScriptBlock(bool withFunctionTemplate)
        {
            if (this.DesignMode)
            {
                return "";
            }

            StringBuilder source = new StringBuilder(256);
            StringBuilder onready = new StringBuilder(256);

            bool isAsync = Ext.IsMicrosoftAjaxRequest;

            int count = 0;

            if (!Ext.IsAjaxRequest)
            {
                this.RegisterEvents(this);
                onready.Append(this.BuildAjaxMethodProxies());

                if (this.ScriptBeforeClientInitBag.Count > 0)
                {
                    // Add all PreClientInit scripts. Combining nested Lazy Instantiation types as we go.
                    foreach (string item in this.ScriptBeforeClientInitBag)
                    {
                        onready.Append(item);
                    }
                }

                if (this.ScriptClientSpecialInitBag.Count > 0)
                {
                    // Add all ClientSpecialInitScript scripts. Combining nested Lazy Instantiation types as we go.
                    foreach (KeyValuePair<string, string> item in this.ScriptClientSpecialInitBag)
                    {
                        onready.Append(this.Combine(item.Key));
                    }
                }

                if (this.ScriptClientInitBag.Count > 0)
                {
                    // Add all ClientInitScript scripts. Combining nested Lazy Instantiation types as we go.
                    foreach (KeyValuePair<string, string> item in this.ScriptClientInitBag)
                    {
                        onready.Append(this.Combine(item.Key));
                    }
                }

                if (this.ScriptAfterClientInitBag.Count > 0)
                {
                    // Add all PostClientInit scripts. Combining nested Lazy Instantiation types as we go.
                    foreach (string item in this.ScriptAfterClientInitBag)
                    {
                        onready.Append(item);
                    }
                }

                // Build up var list of items that were Lazy Instantiated. 

                //if (this.LazyList.Count > 0)
                //{
                //    StringBuilder lazyItems = new StringBuilder(255);
                //    lazyItems.Append("Coolite.Ext.lazyInit([");
                //    if (this.LazyList.Count > 0)
                //    {
                //        foreach (string id in this.LazyList)
                //        {
                //            string lazyId = StringUtils.LeftOfRightmostOf(id, "_ClientInit");
                //            if(!this.InitList.Contains(lazyId) || this.ExcludeFromLazyInit.Contains(lazyId))
                //            {
                //                continue;
                //            }
                //            if (count > 0)
                //            {
                //                lazyItems.Append(",");
                //            }
                            
                //            lazyItems.AppendFormat("\"{0}\"", lazyId);
                //            count++;
                //        }
                //    }
                //    lazyItems.Append("]);");
                //    onready.Append(count>0 ? lazyItems.ToString() : "");
                //}
            }

            if (this.SetValuesBag.Count > 0)
            {
                count = 0;

                StringBuilder values = new StringBuilder(255);
                values.Append("Coolite.Ext.setValues([");
                foreach (string value in this.SetValuesBag)
                {
                    if (count > 0)
                    {
                        values.Append(",");
                    }
                    if (!this.ExcludeFromLazyInit.Contains(value.Substring(1, value.IndexOf(",") - 1)))
                    {
                        count++;
                        values.Append(value);
                    }
                }
                values.Append("]");
                if(Ext.IsAjaxRequest)
                {
                    values.Append(");");
                }
                else
                {
                    values.Append(",true);");  
                }
                
                if (count > 0)
                {
                    onready.Append(values.ToString());
                }
            }

            this.RegisterAjaxEvents();

            this.RegisterCustomListeners();

            foreach (KeyValuePair<long, string> script in this.ScriptOnReadyBag)
            {
                onready.Append(script.Value);
            }

            foreach (KeyValuePair<string, string> item in this.ScriptOnWindowResizeBag)
            {
                onready.AppendFormat(ScriptManager.OnWindowResizeTemplate, item.Value);
            }

            foreach (KeyValuePair<string, string> item in this.ScriptOnTextResizeBag)
            {
                onready.AppendFormat(ScriptManager.OnTextResizeTemplate, item.Value);
            }

            string instanceScript = ScriptManager.GetInstanceScript();

            if (!string.IsNullOrEmpty(instanceScript))
            {
                onready.Append(instanceScript);
            }

            source.AppendFormat((Ext.IsAjaxRequest) ? "{0}" : ScriptManager.OnReadyTemplate, onready.ToString());

            if (!Ext.IsAjaxRequest && !isAsync)
            {
                // Add QuickTips init
                if (this.QuickTips)
                {
                    //Ext.QuickTips.Init();
                    source.Append("Ext.QuickTips.init();");
                }

                source.AppendFormat("Coolite.ScriptManager=\"{0}\";", this.UniqueID);

                if (!string.IsNullOrEmpty(this.AjaxEventUrl))
                {
                    source.AppendFormat("Coolite.Ext.Url=\"{0}\";", this.ResolveUrl(this.AjaxEventUrl));
                }

                // Register BLANK_IMAGE_URL
                if (this.RenderScripts != ResourceLocationType.None)
                {
                    source.AppendFormat("Ext.BLANK_IMAGE_URL=\"{0}\";", this.BLANK_IMAGE_URL);
                }

                if (this.StateProvider == StateProvider.Cookie)
                {
                    source.Append("Ext.state.Manager.setProvider(new Ext.state.CookieProvider());");
                }
            }

            foreach (KeyValuePair<string, string> item in this.ClientScriptBlockBag)
            {
                source.Append(item.Value);
            }

            return withFunctionTemplate ? string.Format(isAsync ? ScriptManager.SimpleScriptBlockTemplate : ScriptManager.ScriptBlockTemplate, source.ToString()) : source.ToString();
        }

        internal void RegisterEvents(ScriptManager manager)
        {
            ScriptManager realManager = manager;
            if (manager is ScriptManagerProxy)
            {
                realManager = this.ScriptManager;
            }

            if (!manager.Listeners.DocumentReady.IsDefault)
            {
                string temp = manager.Listeners.DocumentReady.Fn;

                if (!string.IsNullOrEmpty(manager.Listeners.DocumentReady.Handler))
                {
                    temp = manager.Listeners.DocumentReady.Handler;
                }

                realManager.RegisterOnReadyScript(TokenUtils.ReplaceIDTokens(temp, this.Page));
            }

            if (!manager.Listeners.WindowScroll.IsDefault)
            {
                realManager.RegisterClientScriptBlock(string.Concat(manager.ClientID, "_WindowScroll"), string.Format("Ext.EventManager.on(window,\"scroll\",function(e){{{0}}},window,{{buffer: 50}});", TokenUtils.ParseTokens(manager.Listeners.WindowScroll.Handler, manager)));
            }

            if (!manager.Listeners.WindowUnload.IsDefault)
            {
                realManager.RegisterClientScriptBlock(string.Concat(manager.ClientID, "_WindowUnload"), string.Format("Ext.EventManager.on(window,\"beforeunload\",function(e){{var coolite_windowBeforeUnload=function(e){{{0}}};if(coolite_windowBeforeUnload()){{e.browserEvent.returnValue=\"{1}\";}}}},window);", TokenUtils.ParseTokens(manager.Listeners.WindowUnload.Handler, manager), manager.WindowUnloadMsg));
            }

            if (!manager.Listeners.WindowResize.IsDefault)
            {
                realManager.RegisterOnWindowResizeScript(string.Concat(manager.ClientID, "_WindowResize"), manager.Listeners.WindowResize.FnInternal);
            }

            if (!manager.Listeners.TextResize.IsDefault)
            {
                realManager.RegisterOnTextResizeScript(string.Concat(manager.ClientID, "_TextResize"), manager.Listeners.TextResize.FnInternal);
            }

            if(!manager.Listeners.BeforeAjaxRequest.IsDefault || 
               !manager.Listeners.AjaxRequestComplete.IsDefault ||
               !manager.Listeners.AjaxRequestException.IsDefault)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("{");

                if (!manager.Listeners.BeforeAjaxRequest.IsDefault)
                {
                    manager.Listeners.BeforeAjaxRequest.SetArgumentList(manager.Listeners.GetType().GetProperty("BeforeAjaxRequest"));
                    sb.Append("beforeajaxrequest:").Append(new ClientConfig().Serialize(manager.Listeners.BeforeAjaxRequest)).Append(",");
                }

                if (!manager.Listeners.AjaxRequestComplete.IsDefault)
                {
                    manager.Listeners.AjaxRequestComplete.SetArgumentList(manager.Listeners.GetType().GetProperty("AjaxRequestComplete"));
                    sb.Append("ajaxrequestcomplete:").Append(new ClientConfig().Serialize(manager.Listeners.AjaxRequestComplete)).Append(",");
                }

                if (!manager.Listeners.AjaxRequestException.IsDefault)
                {
                    manager.Listeners.AjaxRequestException.SetArgumentList(manager.Listeners.GetType().GetProperty("AjaxRequestException"));
                    sb.Append("ajaxrequestexception:").Append(new ClientConfig().Serialize(manager.Listeners.AjaxRequestException));
                }

                if(sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }

                sb.Append("}");

                realManager.RegisterBeforeClientInitScript(string.Concat("Coolite.AjaxEvent.on(",sb.ToString(),");"));
            }

            RegisterScriptManagerAjaxEvents(manager, realManager);
        }

        internal void RegisterInitID(WebControl control)
        {
            if(!control.IsIDRequired && (control.IDMode == IDMode.Ignore || (control.IDMode == IDMode.Explicit && control.IsGeneratedID)))
            {
                this.ExcludeFromLazyInit.Add(control.ClientID);
                return;
            }

            string id = control.ClientID;
            
            if (this.InitList.Contains(id))
            {
                string msg = "A Control with an ID of \"{1}\" has already been initialized. Please ensure that all Controls have a unique id.\n\nThe following Control has the same ID as at least one other Control on the Page. All Controls must have a unique ID.\n\n*************************\nControl Details\n*************************\n\nID: {0}.\nClientID: {1}\nType: {2}\n\n*************************\nParent Control Details\n*************************\n\nID: {3}\nClientID: {4}\nType: {5}";

                throw new ArgumentException(string.Format(msg, control.ID, control.ClientID, control.GetType().Name, control.Parent.ID, control.Parent.ClientID, control.Parent.GetType().Name));
            }
            
            this.InitList.Add(id);

            if(control.IsGeneratedID && control.IDMode == IDMode.Ignore)
            {
                this.ExcludeFromLazyInit.Add(id);
            }
        }

        private List<string> initList;

        private List<string> InitList
        {
            get
            {
                if (this.initList == null)
                {
                    this.initList = new List<string>();
                }
                return this.initList;
            }
        }

        private List<string> excludeFromLazyInit;
        private List<string> ExcludeFromLazyInit
        {
            get
            {
                if (this.excludeFromLazyInit == null)
                {
                    this.excludeFromLazyInit = new List<string>();
                }
                return this.excludeFromLazyInit;
            }
        }

        private List<string> lazyList;
        private List<string> LazyList
        {
            get
            {
                if (this.lazyList == null)
                {
                    this.lazyList = new List<string>();
                }
                return this.lazyList;
            }
        }

        private bool GetIsExcluded(string key)
        {
            return (this.LazyList.IndexOf(key) > -1);
        }

        private string Combine(string key)
        {
            string value = "";

            try
            {
                value = this.ScriptClientInitBag[key];

            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                try
                {
                    value = ScriptClientSpecialInitBag[key];
                }
                catch (System.Collections.Generic.KeyNotFoundException)
                {
                    return "";
                }
            }

            if (!string.IsNullOrEmpty(value))
            {
                MatchCollection matches = new Regex(@"({)([\w}]+)(_ClientInit})").Matches(value);


                if (this.GetIsExcluded(key))
                {
                    return "";
                }
                else if (matches.Count == 0)
                {
                    return value;
                }

                string id = "";

                foreach (Match match in matches)
                {
                    id = StringUtils.Chop(match.Value);
                    if (this.ScriptClientInitBag.ContainsKey(id) || this.ScriptClientSpecialInitBag.ContainsKey(id))
                    {
                        value = value.Replace(match.Value, this.Combine(id));
                        this.LazyList.Add(id);
                    }
                }
            }

            return value;
        }

        private string ConvertToFilePath(string resourceName)
        {
            string url = resourceName;
            if (resourceName.StartsWith(ScriptManager.ASSEMBLYSLUG))
            {
                url = this.ResolveUrl(this.ResourcePath + StringUtils.ReplaceLastInstanceOf(resourceName.Replace(ScriptManager.ASSEMBLYSLUG + ".", "").Replace("-embedded", "").Replace(".", "/"), "/", "."));
            }
            return url;
        }

        private string ConvertToCacheFly(string resourceName)
        {
            string url = resourceName;
            if (resourceName.StartsWith(ScriptManager.ASSEMBLYSLUG))
            {
                url = this.ResolveUrl(this.ResourcePath + StringUtils.ReplaceLastInstanceOf(resourceName.Replace(ScriptManager.ASSEMBLYSLUG + ".", "").Replace("-embedded", "").Replace(".", "/"), "/", "."));
            }
            return url;
        }


        /*  Add to Page
            --------------------------------------------------------------------------------------------*/

        internal void AddScript(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            Control sm = this.MicrosoftScriptManager;

            if (sm != null && Ext.IsMicrosoftAjaxRequest)
            {
                Type t = sm.GetType();

                if (sm.GetType().FullName.Contains("ToolkitScriptManager"))
                {
                    t = sm.GetType().BaseType;
                }

                try
                {
                    Type[] types = { typeof(Control), typeof(Type), typeof(string), typeof(string), typeof(bool) };
                    MethodInfo m = t.GetMethod("RegisterStartupScript", types);

                    object[] args = { this.Page, this.Page.GetType(), key, value, false };
                    m.Invoke(sm, args);
                }
                catch
                {
                    // Swallow the Exception
                }

                //this.Page.ClientScript.RegisterClientScriptBlock(t, key, value, false);
            }
            else
            {
                this.AddScriptItem(key, value, false);
            }
        }

        private readonly StringBuilder styleBuilder = new StringBuilder(256);

        private readonly StringBuilder scriptBuilder = new StringBuilder(256);

        internal void AddScriptItem(string key, string value, bool addToStart)
        {
            this.AddItem(scriptBuilder, key, value, addToStart);   
        }

        internal void AddStyleItem(string key, string value, bool addToStart)
        {
            this.AddItem(styleBuilder, key, value, addToStart);  
        }
        
        internal void AddItem(StringBuilder builder, string key, string value, bool addToStart)
        {
            if (!Ext.IsMicrosoftAjaxRequest)
            {
                if (this.Page.Header != null)
                {
                    if (addToStart)
                    {
                        builder.Insert(0, value);
                    }
                    else
                    {
                        builder.Append(value);
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), key, value, false);
                }
            }
        }

        internal void DesignAddItem(string key, string value, bool addToStart)
        {
            LiteralControl el = new LiteralControl(value);
            el.ID = key;
            
            if (this.Page.Header != null)
            {
                if (addToStart)
                {
                    this.Page.Header.Controls.AddAt(0, el);
                }
                else
                {
                    this.Page.Header.Controls.Add(el);
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), key, value, false);
            }
        }


        /*  Helpers
            -----------------------------------------------------------------------------------------------*/

        ScriptContainer scriptContainer = null;
        bool scriptContainerChecked = false;

        [Browsable(false)]
        public virtual ScriptContainer ScriptContainer
        {
            get
            {
                if (this.scriptContainer == null && !this.scriptContainerChecked)
                {
                    this.scriptContainer = (ScriptContainer)ControlUtils.FindControl(this.Page, typeof(ScriptContainer), true);
                    this.scriptContainerChecked = true;
                }
                return this.scriptContainer;
            }
        }

        StyleContainer styleContainer = null;
        bool styleContainerChecked = false;

        [Browsable(false)]
        public virtual StyleContainer StyleContainer
        {
            get
            {
                if (this.styleContainer == null && !this.styleContainerChecked)
                {
                    this.styleContainer = (StyleContainer)ControlUtils.FindControl(this.Page, typeof(StyleContainer), true);
                    this.styleContainerChecked = true;
                }
                return this.styleContainer;
            }
        }

        private Control microsoftScriptManager = null;
        private Control MicrosoftScriptManager
        {
            get
            {
                if (this.microsoftScriptManager == null)
                {
                    this.microsoftScriptManager = ControlUtils.FindControlByTypeName(this, "System.Web.UI.ScriptManager");
                }
                return this.microsoftScriptManager;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string ApplicationName
        {
            get
            {
                if (!this.DesignMode)
                {
                    return HttpContext.Current.Request.ApplicationPath.Substring(1, HttpContext.Current.Request.ApplicationPath.Length - 1);
                }
                return "";
            }
        }

        public const string ASSEMBLYSLUG = "Coolite.Ext.Web.Build.Resources.Coolite";

        public virtual string GetWebResourceUrl(string resourceName)
        {
            return this.GetWebResourceUrl(this.GetType(), resourceName);
        }

        string cacheBuster = "";
        internal string CacheBuster
        {
            get
            {
                if (string.IsNullOrEmpty(this.cacheBuster))
                {
                    this.cacheBuster = Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
                }
                return this.cacheBuster;
            }
        }

        public virtual string GetWebResourceUrl(Type type, string resourceName)
        {
            if (this.Page == null)
            {
                this.Page = new Page();
            }

            if (resourceName.StartsWith(ScriptManager.ASSEMBLYSLUG) && !this.DesignMode && this.CleanResourceUrl && ResourceManager.HasHandler())
            {
                string buster = (resourceName.EndsWith(".js") || resourceName.EndsWith(".css")) ? string.Concat("?v=", this.CacheBuster) : "";
                return this.ResolveUrl(string.Format("~/{0}/coolite.axd{1}", StringUtils.ReplaceLastInstanceOf(resourceName.Replace(ScriptManager.ASSEMBLYSLUG, "").Replace('.', '/'), "/", "-"), buster));
            }

            return HttpUtility.HtmlAttributeEncode(this.Page.ClientScript.GetWebResourceUrl(type, resourceName));
        }

        public virtual string GetWebResourceAsString(string resourceName)
        {
            return this.GetWebResourceAsString(this.GetType(), resourceName);
        }

        public virtual string GetWebResourceAsString(Type type, string resourceName)
        {
            string script = string.Empty;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(type.Assembly.GetManifestResourceStream(null, resourceName)))
            {
                script = reader.ReadToEnd();
                reader.Close();
            }
            return script;
        }

        public virtual string ParseCssWebResourceUrls(string src)
        {
            Regex regex = new Regex(@"<%=WebResource\("".*\.(gif|png)*""\)%>");
            MatchCollection matches = regex.Matches(src);
            foreach (Match match in matches)
            {
                string url = match.Value.Replace("<%=WebResource(\"", string.Empty).Replace("\")%>", string.Empty);
                src = src.Replace(match.Value, string.Format("{0}", this.GetWebResourceUrl(url)));
            }
            return src;
        }

        public virtual void RegisterIcon(Icon icon)
        {
            this.RegisterClientStyleBlock(icon.ToString(), this.GetIconClass(icon));
        }

        public static string GetIconClassName(Icon icon)
        {
            return (icon != Icon.None) ? string.Format("icon-{0}", icon.ToString().ToLower()) : "";
        }

        public virtual string GetIconClass(Icon icon)
        {
            if (icon != Icon.None)
            {
                return string.Format(".{0}{{background-image:url({1}) !important;}}", ScriptManager.GetIconClassName(icon), this.GetIconUrl(icon));
            }
            return "";
        }

        public virtual string GetIconUrl(Icon icon)
        {
            if (icon != Icon.None)
            {
                if (this.RenderStyles == ResourceLocationType.Embedded || this.RenderStyles == ResourceLocationType.CacheFly)
                {
                    return this.GetWebResourceUrl(string.Format(ScriptManager.ASSEMBLYSLUG + ".icons.{0}", StringUtils.ToCharacterSeparatedFileName(icon.ToString(), '_', "png")));
                }
                else if (this.RenderStyles == ResourceLocationType.File || this.RenderStyles == ResourceLocationType.CacheFlyAndFile)
                {
                    return this.ResolveUrl(this.ResourcePath + "icons/" + StringUtils.ToCharacterSeparatedFileName(icon.ToString(), '_', "png"));
                }
            }
            return "";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string BLANK_IMAGE_URL
        {
            get
            {
                string url = "";

                switch (this.RenderScripts)
                {
                    case ResourceLocationType.Embedded:
                        url = this.GetWebResourceUrl(string.Concat(ScriptManager.ASSEMBLYSLUG, ".extjs.resources.images.", this.Theme.ToString().ToLower(), ".s.gif"));
                        break;
                    case ResourceLocationType.File:
                        url = this.GetBlankImageFilePath();
                        break;
                    case ResourceLocationType.CacheFly:
                        if (this.Theme == Theme.Default || this.Theme == Theme.Gray)
                        {
                            url = this.GetCacheFlyLink(string.Concat("resources/images/", this.Theme.ToString().ToLower(), "/s.gif"));
                        }
                        else
                        {
                            url = this.GetWebResourceUrl(string.Concat(ScriptManager.ASSEMBLYSLUG, ".extjs.resources.images.", this.Theme.ToString().ToLower(), ".s.gif"));
                        }
                        break;
                    case ResourceLocationType.CacheFlyAndFile:
                        if (this.Theme == Theme.Default || this.Theme == Theme.Gray)
                        {
                            url = this.GetCacheFlyLink(string.Concat("resources/images/", this.Theme.ToString().ToLower(), "/s.gif"));
                        }
                        else
                        {
                            url = this.GetBlankImageFilePath();
                        }
                        break;
                }

                return url;
            }
        }

        private string GetBlankImageFilePath()
        {
            string url = string.Concat(ScriptManager.ASSEMBLYSLUG, ".extjs.resources.images.", this.Theme.ToString().ToLower(), ".s.gif");
            return this.ResolveUrl(this.ResourcePath + StringUtils.ReplaceLastInstanceOf(url.Replace(ScriptManager.ASSEMBLYSLUG + ".", "").Replace(".", "/"), "/", "."));
        }

        private ScriptManagerListeners listeners;

        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public virtual ScriptManagerListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ScriptManagerListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private ListenerCollection customListeners;

        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Custom Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public virtual ListenerCollection CustomListeners
        {
            get
            {
                if (this.customListeners == null)
                {
                    this.customListeners = new ListenerCollection();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.customListeners).TrackViewState();
                    }
                }
                return this.customListeners;
            }
        }

        private ScriptManagerAjaxEvents ajaxEvents;

        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public ScriptManagerAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ScriptManagerAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        private AjaxEventCollection customAjaxEvents;

        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Custom Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public virtual AjaxEventCollection CustomAjaxEvents
        {
            get
            {
                if (this.customAjaxEvents == null)
                {
                    this.customAjaxEvents = new AjaxEventCollection();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.customAjaxEvents).TrackViewState();
                    }
                }
                return this.customAjaxEvents;
            }
        }

        /*  ViewState
            -----------------------------------------------------------------------------------------------*/

        public static ScriptManager GetInstance(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("The Page object can not be found.");
            }

            return page.Items[typeof(ScriptManager)] as ScriptManager;
        }

        public static ScriptManager GetInstance(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("HttpContext is empty");
            }

            if (context.CurrentHandler is Page)
            {
                return ((Page)HttpContext.Current.CurrentHandler).Items[typeof(ScriptManager)] as ScriptManager;
            }

            return context.Items[typeof(ScriptManager)] as ScriptManager;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Ext.IsAjaxRequest && !this.Page.IsPostBack)
            {
                this.Page.LoadComplete += Page_AjaxLoadComplete;
            }
        }

        private bool postbackPerformed;

        protected void Page_AjaxLoadComplete(object sender, EventArgs e)
        {
            if (this.postbackPerformed)
            {
                return;
            }

            string _ea = this.Page.Request["__EVENTARGUMENT"];
            if (!string.IsNullOrEmpty(_ea))
            {
                string _et = this.Page.Request["__EVENTTARGET"];

                if (_et == this.UniqueID)
                {
                    this.RaisePostBackEvent(_ea);
                }

                return;
            }

            XmlNode eventArgumentNode = this.SubmitConfig.SelectSingleNode("config/__EVENTARGUMENT");
            if (eventArgumentNode == null)
            {
                throw new InvalidOperationException(
                    "Incorrect submit config - the '__EVENTARGUMENT' parameter is absent");
            }

            XmlNode eventTargetNode = this.SubmitConfig.SelectSingleNode("config/__EVENTTARGET");
            if (eventTargetNode == null)
            {
                throw new InvalidOperationException(
                    "Incorrect submit config - the '__EVENTTARGET' parameter is absent");
            }

            if (eventTargetNode.InnerText == this.UniqueID)
            {
                this.RaisePostBackEvent(eventArgumentNode.InnerText);
            }
        }

        internal void FireAsyncEvent(string eventName, ParameterCollection extraParams)
        {
            ComponentAjaxEvents listeners = this.AjaxEvents;

            PropertyInfo eventListenerInfo = listeners.GetType().GetProperty(eventName);
            if (eventListenerInfo.PropertyType != typeof(ComponentAjaxEvent))
            {
                throw new HttpException(string.Format("The ScriptManager has no listener with name '{0}'", eventName));
            }

            ComponentAjaxEvent listener = eventListenerInfo.GetValue(listeners, null) as ComponentAjaxEvent;
            if (listener == null || listener.IsDefault)
            {
                throw new HttpException(string.Format("The ScriptManager has no listener with name '{0}' or handler is absent", eventName));
            }
            AjaxEventArgs e = new AjaxEventArgs(extraParams);
            listener.OnEvent(e);
        }

        private static bool HasChildNodes(XmlNode node)
        {
            if (!node.HasChildNodes)
            {
                return false;
            }

            if(node.ChildNodes.Count == 1 && node.FirstChild.NodeType == XmlNodeType.Text)
            {
                return false;
            }

            return true;
        }

        public static ParameterCollection XmlToParams(XmlNode node)
        {
            ParameterCollection extraParams = new ParameterCollection();
            if (node != null)
            {
                foreach (XmlNode xmlParam in node.ChildNodes)
                {
                    Parameter newParam = new Parameter();
                    newParam.Name = HttpUtility.HtmlDecode(xmlParam.Name);
                    newParam.Value = HttpUtility.HtmlDecode(xmlParam.InnerXml);
                    if (ScriptManager.HasChildNodes(xmlParam))
                    {
                        newParam.Params.AddRange(ScriptManager.XmlToParams(xmlParam));
                    }

                    extraParams.Add(newParam);
                }
            }

            return extraParams;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            this.postbackPerformed = true;
            if (string.IsNullOrEmpty(eventArgument))
            {
                return;
            }

            string[] args = eventArgument.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (args.Length != 3)
            {
                return;
            }

            string requestTypeArg = (args[1].Equals("postback")) ? "PostBack" : StringUtils.ToCamelCase(args[1]);

            AjaxRequestType requestType = (AjaxRequestType)Enum.Parse(typeof(AjaxRequestType), requestTypeArg, true);

            string controlID = args[0];
            string controlEvent = args[2];

            if (!Enum.IsDefined(typeof(AjaxRequestType), requestType))
            {
                throw new HttpException(string.Format("Incorrect ajax request type - {0}", requestType));
            }

            Control ctrl = null;

            bool isCustomAjaxEvent = requestType == AjaxRequestType.Custom || requestType == AjaxRequestType.Proxy;
            bool isAjaxMethodCall = requestType == AjaxRequestType.Public;

            if (!isCustomAjaxEvent)
            {
                if (controlID == "-")
                {
                    if (isAjaxMethodCall)
                    {
                        ctrl = this.Page;
                    }
                    else
                    {
                        ctrl = this;
                    }
                }
                else
                {
                    ctrl = ControlUtils.FindControlByClientID(this, controlID, true, null);

                    if (ctrl == null)
                    {
                        throw new HttpException(string.Format("The control with ID '{0}' not found", controlID));
                    }
                }
            }

            bool returnViewState = false;
            ParameterCollection extraParams = new ParameterCollection();
            if (this.SubmitConfig != null)
            {
                XmlNode viewStateMode = this.SubmitConfig.SelectSingleNode("config/viewStateMode");
                if (viewStateMode != null)
                {
                    ViewStateMode mode = (ViewStateMode)Enum.Parse(typeof(ViewStateMode), viewStateMode.InnerText, true);
                    returnViewState = mode == ViewStateMode.Include;
                }

                XmlNode userParamsNode = this.SubmitConfig.SelectSingleNode("config/extraParams");
                if (userParamsNode != null)
                {
                    extraParams = ScriptManager.XmlToParams(userParamsNode);
                }
            }

            ScriptManager.ReturnViewState = returnViewState;

            switch (requestType)
            {
                case AjaxRequestType.Event:
                    Observable observable = ctrl as Observable;
                    if (observable == null)
                    {
                        if (ctrl is ScriptManagerProxy)
                        {
                            ((ScriptManagerProxy)ctrl).FireAsyncEvent(controlEvent, extraParams);
                        }
                        else if (ctrl is ScriptManager)
                        {
                            this.FireAsyncEvent(controlEvent, extraParams);
                        }
                        else
                        {
                            throw new HttpException(string.Format("The control with ID '{0}' is not Observable", controlID));
                        }
                    }
                    if (observable != null)
                    {
                        observable.FireAsyncEvent(controlEvent, extraParams);
                    }
                    break;
                case AjaxRequestType.Proxy:
                case AjaxRequestType.Custom:

                    ScriptManager sm = this;
                    if (requestType == AjaxRequestType.Proxy)
                    {
                        ctrl = ControlUtils.FindControlByClientID(this, controlID, true, null);

                        if (ctrl == null)
                        {
                            throw new HttpException(string.Format("The ScriptManagerProxy with ID '{0}' not found", controlID));
                        }

                        sm = (ScriptManagerProxy)ctrl;
                    }

                    foreach (AjaxEvent ajaxEvent in sm.CustomAjaxEvents)
                    {
                        if (ajaxEvent.EventID == controlEvent)
                        {
                            ajaxEvent.OnEvent(new AjaxEventArgs(extraParams));
                            break;
                        }
                    }
                    break;
                case AjaxRequestType.PostBack:
                    IAjaxPostBackEventHandler ajaxPostBackHandler = ctrl as IAjaxPostBackEventHandler;
                    
                    if (ajaxPostBackHandler != null)
                    {
                        ajaxPostBackHandler.RaiseAjaxPostBackEvent(controlEvent, extraParams);
                        break;
                    }


                    IPostBackEventHandler postbackHandler = ctrl as IPostBackEventHandler;
                    if (postbackHandler == null)
                    {
                        throw new HttpException(string.Format("The control with ID '{0}' is not IPostBackEventHandler", controlID));
                    }

                    postbackHandler.RaisePostBackEvent(controlEvent);
                    break;
                case AjaxRequestType.Public:
                    if (controlID != "-")
                    {
                        ctrl = null;
                        List<UserControl> userControls = ControlUtils.FindControls<UserControl>(this.Page);
                        
                        foreach (UserControl control in userControls)
                        {
                            if(control.ClientID == controlID)
                            {
                                ctrl = control;
                                break;
                            }
                        }
                    }

                    if (ctrl == null)
                    {
                        throw new HttpException(string.Format("The control '{0}' of ajax instanse method not found", controlID));
                    }

                    HttpContext context = HttpContext.Current;
                    HandlerMethods handler = HandlerMethods.GetHandlerMethodsByType(context, ctrl.GetType(), ctrl.TemplateSourceDirectory, false);

                    string methodName = controlEvent;

                    if (handler == null)
                    {
                        throw new Exception(string.Format("The handler '{0}' is absent!", context.Request.FilePath));
                    }

                    if (string.IsNullOrEmpty(methodName))
                    {
                        throw new Exception("The ajax method is not defined!");
                    }

                    AjaxMethod ajaxMethod = handler.GetInstanceMethod(methodName);

                    if (ajaxMethod == null)
                    {
                        throw new Exception(string.Format("The ajax instance method '{0}' is absent!", methodName));
                    }

                    try
                    {
                        object result = ajaxMethod.Invoke(ctrl, extraParams);
                        AjaxMethodResult = result;
                    }
                    catch (System.Reflection.TargetException)
                    {
                        ReInvokeAjaxMethod(ctrl, extraParams, context, methodName);
                    }
                    catch (TargetInvocationException e)
                    {
                        ScriptManager.AjaxSuccess = false;
                        ScriptManager.AjaxErrorMessage = this.IsDebugging ? e.InnerException.ToString() : e.InnerException.Message;
                        if(this.RethrowAjaxExceptions)
                        {
                            throw;
                        }
                        //HttpContext.Current.AddError(e);
                    }
                    catch (Exception e)
                    {
                        ScriptManager.AjaxSuccess = false;
                        ScriptManager.AjaxErrorMessage = this.IsDebugging ? e.ToString() : e.Message;
                        if (this.RethrowAjaxExceptions)
                        {
                            throw;
                        }
                        //HttpContext.Current.AddError(e);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(requestType.ToString());
            }
        }

        private void ReInvokeAjaxMethod(Control ctrl, ParameterCollection extraParams, HttpContext context, string methodName)
        {
            HandlerMethods handler;
            AjaxMethod ajaxMethod;
            
            try
            {
                handler = HandlerMethods.GetHandlerMethodsByType(context, ctrl.GetType(), ctrl.TemplateSourceDirectory, true);
                ajaxMethod = handler.GetInstanceMethod(methodName);
                object result = ajaxMethod.Invoke(ctrl, extraParams);
                AjaxMethodResult = result;
            }
            catch (Exception e)
            {
                ScriptManager.AjaxSuccess = false;
                ScriptManager.AjaxErrorMessage = this.IsDebugging ? e.ToString() : e.Message;
                if (this.RethrowAjaxExceptions)
                {
                    throw;
                }
                //HttpContext.Current.AddError(e);
            }
        }

        internal static bool ReturnViewState
        {
            get
            {
                object o = HttpContext.Current.Items["CooliteParam_ReturnViewState"];
                return o == null ? false : (bool)o;
            }
            set
            {
                HttpContext.Current.Items["CooliteParam_ReturnViewState"] = value;
            }
        }

        internal static bool RemoveViewStateStatic
        {
            get
            {
                object o = HttpContext.Current.Items["CooliteParam_RemoveViewStateStatic"];
                return o == null ? false : (bool)o;
            }
            set
            {
                HttpContext.Current.Items["CooliteParam_RemoveViewStateStatic"] = value;
            }
        }

        public static object ServiceResponse
        {
            get
            {
                return HttpContext.Current.Items["CooliteParam_ServiceResponse"];
            }
            set
            {
                HttpContext.Current.Items["CooliteParam_ServiceResponse"] = value;
            }
        }

        public static ParameterCollection ExtraParamsResponse
        {
            get
            {
                object o = HttpContext.Current.Items["CooliteParam_UserParamsResponse"];
                if (o == null)
                {
                    o = new ParameterCollection();
                    HttpContext.Current.Items["CooliteParam_UserParamsResponse"] = o;
                }

                return o as ParameterCollection;
            }
        }

        internal static object AjaxMethodResult
        {
            get
            {
                return HttpContext.Current.Items["CooliteParam_AjaxMethodResult"];
            }
            set
            {
                HttpContext.Current.Items["CooliteParam_AjaxMethodResult"] = value;
            }
        }

        public static bool AjaxSuccess
        {
            get
            {
                object o = HttpContext.Current.Items["AjaxSuccess"];
                if (o == null)
                {
                    return true;
                }

                return (bool)o;
            }
            set
            {
                HttpContext.Current.Items["AjaxSuccess"] = value;
            }
        }

        public static string AjaxErrorMessage
        {
            get
            {
                object o = HttpContext.Current.Items["AjaxErrorMessage"];

                return o as string;
            }
            set
            {
                HttpContext.Current.Items["AjaxErrorMessage"] = value;
            }
        }
    }
}