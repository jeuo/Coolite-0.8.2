/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.app.App")]
    [ClientScript(Type = typeof(Desktop), FilePath = "/ux/extensions/desktop/js/Desktop.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.desktop.js.Desktop.js")]
    [ClientStyle(Type = typeof(Desktop), FilePath = "/ux/extensions/desktop/css/desktop.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.desktop.css.desktop-embedded.css")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Desktop), "Build.Resources.ToolboxIcons.Desktop.bmp")]
    [ToolboxData("<{0}:Desktop runat=\"server\"></{0}:Desktop>")]
    public class Desktop: Component
    {
        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.SimpleRender(writer);
            writer.Write("<div id=\"x-desktop\">");

            if(this.Shortcuts.Count>0)
            {
                writer.Write("<dl id=\"x-shortcuts\">");
            }

            foreach (DesktopShortcut shortcut in this.Shortcuts)
            {
                if (string.IsNullOrEmpty(shortcut.ModuleID) && string.IsNullOrEmpty(shortcut.ShortcutID))
                {
                    throw new ArgumentNullException("Shortcut", "You must specify ModuleID or ShortcutID for shortcut");
                }
                string sUrl =
                    ScriptManager.GetWebResourceUrl(
                        "Coolite.Ext.Web.Build.Resources.Coolite.extjs.resources.images.default.s.gif");
                writer.Write("<dt id=\"");
                writer.Write(string.IsNullOrEmpty(shortcut.ModuleID) ? shortcut.ShortcutID : shortcut.ModuleID);
                writer.Write("-shortcut\"");

                if (!string.IsNullOrEmpty(shortcut.ShortcutID))
                {
                    writer.Write(" ext:custom=\"true\"");
                }

                if (!string.IsNullOrEmpty(shortcut.X) && !string.IsNullOrEmpty(shortcut.Y))
                {
                    writer.Write(" ext:X=\"");
                    writer.Write(shortcut.X);
                    writer.Write("\"");

                    writer.Write(" ext:Y=\"");
                    writer.Write(shortcut.Y);
                    writer.Write("\"");
                }

                writer.Write(">");
                writer.Write("<a href=\"#\"><img src=\"");
                writer.Write(sUrl);
                writer.Write("\"");
                if (!string.IsNullOrEmpty(shortcut.IconCls))
                {
                    writer.Write(" class=\"");
                    writer.Write(shortcut.IconCls);
                    writer.Write("\"");
                }
                writer.Write("/><div class=\"x-shortcut-text\">");
                writer.Write(shortcut.Text);
                writer.Write("</div></a></dt>");
            }

            if (this.Shortcuts.Count > 0)
            {
                writer.Write("</dl>");
            }

            foreach (Control control in this.Controls)
            {
                control.RenderControl(writer);
            }

            writer.Write("</div>");
            writer.Write("<div id=\"ux-taskbar\"><div id=\"ux-taskbar-start\"></div><div id=\"ux-taskbuttons-panel\"></div><div class=\"x-clear\"></div></div>");
        }

        private ITemplate body = null;

        [TemplateInstance(TemplateInstance.Single), MergableProperty(false),
         PersistenceMode(PersistenceMode.InnerDefaultProperty), Browsable(false)]
        public ITemplate Body
        {
            get { return body; }
            set { body = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!this.DesignMode)
            {
                Desktop existingInstance = Desktop.GetCurrent(this.Page);

                if (existingInstance != null && !DesignMode)
                {
                    throw new InvalidOperationException("Only one desktop is allowed");
                }

                this.Page.Items[typeof(Desktop)] = this;

                if (body != null)
                    body.InstantiateIn(this);
            }
        }

        public static Desktop GetCurrent(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return page.Items[typeof(Desktop)] as Desktop;
        }

        private DesktopModulesCollection modules;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public DesktopModulesCollection Modules
        {
            get
            {
                if (this.modules == null)
                {
                    this.modules = new DesktopModulesCollection();
                }

                return this.modules;
            }
        }

        protected override void PageLoadComplete(object sender, EventArgs e)
        {
            base.PageLoadComplete(sender, e);

            foreach (DesktopModule module in this.Modules)
            {
                if(!string.IsNullOrEmpty(module.Launcher.Text))
                {
                    this.Controls.Add(module.Launcher);
                    this.LazyItems.Add(module.Launcher);
                }
            }
        }

        [ClientConfig("getModules", JsonMode.Raw)]
        internal string ModulesProxy
        {
            get
            {
                StringBuilder sb = new StringBuilder(256);
                sb.Append("function(){return [");

                bool commaNeed = false;
                foreach (DesktopModule module in this.Modules)
                {
                    if(commaNeed)
                    {
                        sb.Append(",");
                    }
                    commaNeed = true;

                    sb.Append("new Ext.app.Module(");
                    sb.Append(new ClientConfig().Serialize(module));
                    sb.Append(")");
                }
                    
                sb.Append("];}");

                return sb.ToString();
            }
        }

        private StartMenuConfig startMenu;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public StartMenuConfig StartMenu
        {
            get
            {
                if (this.startMenu == null)
                {
                    this.startMenu = new StartMenuConfig(this);
                }

                return this.startMenu;
            }
        }

        [ClientConfig("getStartConfig", JsonMode.Raw)]
        internal string StartMenuProxy
        {
            get
            {
                if(this.StartMenu.Icon != Icon.None)
                {
                    this.ScriptManager.RegisterIcon(this.StartMenu.Icon);
                }

                StringBuilder sb = new StringBuilder(256);
                sb.Append("function(){return ");

                sb.Append(new ClientConfig().Serialize(this.StartMenu));
                
                sb.Append(";}");

                return sb.ToString();
            }
        }

        private StartButtonConfig startButton;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public StartButtonConfig StartButton
        {
            get
            {
                if (this.startButton == null)
                {
                    this.startButton = new StartButtonConfig();
                }

                return this.startButton;
            }
        }

        [ClientConfig("getStartButtonConfig", JsonMode.Raw)]
        internal string StartButtonProxy
        {
            get
            {
                if (this.StartButton.Icon != Icon.None)
                {
                    this.ScriptManager.RegisterIcon(this.StartButton.Icon);
                }

                StringBuilder sb = new StringBuilder(256);
                sb.Append("function(){return ");

                sb.Append(new ClientConfig().Serialize(this.StartButton));

                sb.Append(";}");

                return sb.ToString();
            }
        }

        private DesktopShortcuts shortcuts;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public DesktopShortcuts Shortcuts
        {
            get
            {
                if (this.shortcuts == null)
                {
                    this.shortcuts = new DesktopShortcuts();
                }

                return this.shortcuts;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetBackgroundColor")]
        public virtual string BackgroundColor
        {
            get
            {
                return (string)this.ViewState["BackgroundColor"] ?? "";
            }
            set
            {
                this.ViewState["BackgroundColor"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetShortcutTextColor")]
        public string ShortcutTextColor
        {
            get
            {
                return (string)this.ViewState["ShortcutTextColor"] ?? "";
            }
            set
            {
                this.ViewState["ShortcutTextColor"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetWallpaper")]
        public string Wallpaper
        {
            get
            {
                return (string)this.ViewState["Wallpaper"] ?? "";
            }
            set
            {
                this.ViewState["Wallpaper"] = value;
            }
        }

        protected virtual void SetWallpaper(string url)
        {
            url = string.IsNullOrEmpty(url) ? "''" : JSON.Serialize(this.Page.ResolveUrl(url));
            this.AddScript("{0}.getDesktop().setWallpaper({1});", this.ClientID, url);
        }

        protected virtual void SetBackgroundColor(string color)
        {
            this.AddScript("{0}.getDesktop().setBackgroundColor({1});", this.ClientID, JSON.Serialize(color));
        }

        protected virtual void SetShortcutTextColor(string color)
        {
            this.AddScript("{0}.getDesktop().setFontColor({1});", this.ClientID, JSON.Serialize(color));
        }

        private DesktopListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public DesktopListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new DesktopListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private DesktopAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side AjaxEvent Handlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side AjaxEventHandlers")]
        [ViewStateMember]
        public DesktopAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new DesktopAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
    }
}