/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Basic status bar component that can be used as the bottom toolbar of any Ext.Panel.
    /// </summary>
    /// 
    [Xtype("statusbar")]
    [InstanceOf(ClassName = "Ext.StatusBar")]
    [ToolboxData("<{0}:StatusBar runat=\"server\"><Items></Items></{0}:StatusBar>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.StatusBar), "Build.Resources.ToolboxIcons.StatusBar.bmp")]
    [Description("Basic status bar component that can be used as the bottom toolbar of any Ext.Panel.")]
    public class StatusBar : ToolbarBase
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            this.RegisterStatusIcon(this.DefaultIcon);
            this.RegisterStatusIcon(this.BusyIcon);
            this.RegisterStatusIcon(this.Icon);
        }

        private void RegisterStatusIcon(Icon icon)
        {
            if (icon != Icon.None)
            {
                this.ScriptManager.RegisterClientStyleBlock(icon.ToString() + "-sbar", this.GetIconClass(icon));
            }
        }

        public static string GetIconClassName(Icon icon)
        {
            return (icon != Icon.None) ? string.Format("icon-{0}-sbar", icon.ToString().ToLower()) : "";
        }

        public virtual string GetIconClass(Icon icon)
        {
            if (icon != Icon.None)
            {
                return string.Format(".{0}{{background-image:url({1}) !important;padding-left: 25px !important;background-repeat: no-repeat;background-position: 3px 3px;}}", ScriptManager.GetIconClassName(icon), this.ScriptManager.GetIconUrl(icon));
            }
            return "";
        }

        /// <summary>
        /// The number of milliseconds to wait after setting the status via setStatus before automatically clearing the status text and icon (defaults to 5000). Note that this only applies when passing the clear argument to setStatus since that is the only way to defer clearing the status. This can be overridden by specifying a different wait value in setStatus. Calls to clearStatus always clear the status bar immediately and ignore this value.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(5000)]
        [NotifyParentProperty(true)]
        [Description("The number of milliseconds to wait after setting the status via setStatus before automatically clearing the status text and icon (defaults to 5000). Note that this only applies when passing the clear argument to setStatus since that is the only way to defer clearing the status. This can be overridden by specifying a different wait value in setStatus. Calls to clearStatus always clear the status bar immediately and ignore this value.")]
        public virtual int AutoClear
        {
            get
            {
                object obj = this.ViewState["AutoClear"];
                return (obj == null) ? 5000 : (int)obj;
            }
            set
            {
                this.ViewState["AutoClear"] = value;
            }
        }

        /// <summary>
        /// The default Icon applied when calling showBusy (defaults to 'Icon.None'). It can be overridden at any time by passing the iconCls argument into showBusy. See the Icon or IconCls docs for additional details about customizing the icon.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The default Icon applied when calling showBusy (defaults to 'Icon.None'). It can be overridden at any time by passing the iconCls argument into showBusy. See the Icon or IconCls docs for additional details about customizing the icon.")]
        public virtual Icon BusyIcon
        {
            get
            {
                object obj = this.ViewState["BusyIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["BusyIcon"] = value;
            }
        }

        [ClientConfig("busyIconCls")]
        [DefaultValue("")]
        internal virtual string BusyIconClsProxy
        {
            get
            {
                if (this.BusyIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.BusyIcon);
                }
                return (!this.BusyIconCls.Equals("x-status-busy")) ? this.BusyIconCls : "";
            }
        }

        /// <summary>
        /// The default iconCls applied when calling showBusy (defaults to 'x-status-busy'). It can be overridden at any time by passing the iconCls argument into showBusy. See the iconCls docs for additional details about customizing the icon.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-status-busy")]
        [NotifyParentProperty(true)]
        [Description("The default iconCls applied when calling showBusy (defaults to 'x-status-busy'). It can be overridden at any time by passing the iconCls argument into showBusy. See the iconCls docs for additional details about customizing the icon.")]
        public virtual string BusyIconCls
        {
            get
            {
                return (string)this.ViewState["BusyIconCls"] ?? "x-status-busy";
            }
            set
            {
                this.ViewState["BusyIconCls"] = value;
            }
        }

        /// <summary>
        /// The default text applied when calling showBusy (defaults to 'Loading...'). It can be overridden at any time by passing the text argument into showBusy.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Loading...")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Description("The default text applied when calling showBusy (defaults to 'Loading...'). It can be overridden at any time by passing the text argument into showBusy.")]
        public virtual string BusyText
        {
            get
            {
                return (string)this.ViewState["BusyText"] ?? "Loading...";
            }
            set
            {
                this.ViewState["BusyText"] = value;
            }
        }

        /// <summary>
        /// The base class applied to the containing element for this component on render (defaults to 'x-statusbar')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-statusbar")]
        [NotifyParentProperty(true)]
        [Description("The base class applied to the containing element for this component on render (defaults to 'x-statusbar')")]
        public override string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "x-statusbar";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        /// <summary>
        /// The default Icon (see the Icon or IconCls docs for additional details about customizing the icon). This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to 'Icon.None').
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The default Icon (see the Icon or IconCls docs for additional details about customizing the icon). This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to 'Icon.None').")]
        public virtual Icon DefaultIcon
        {
            get
            {
                object obj = this.ViewState["DefaultIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["DefaultIcon"] = value;
            }
        }

        [ClientConfig("defaultIconCls")]
        [DefaultValue("")]
        internal virtual string DefaultIconClsProxy
        {
            get
            {
                if (this.DefaultIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.DefaultIcon);
                }
                return this.DefaultIconCls;
            }
        }

        /// <summary>
        /// The default iconCls value (see the iconCls docs for additional details about customizing the icon). This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default iconCls value (see the iconCls docs for additional details about customizing the icon). This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to '').")]
        public virtual string DefaultIconCls
        {
            get
            {
                return (string)this.ViewState["DefaultIconCls"] ?? "";
            }
            set
            {
                this.ViewState["DefaultIconCls"] = value;
            }
        }

        /// <summary>
        /// The default text value. This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("~")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Description("The default text value. This will be used anytime the status bar is cleared with the useDefaults:true option (defaults to '').")]
        public virtual string DefaultText
        {
            get
            {
                return (string)this.ViewState["DefaultText"] ?? "";
            }
            set
            {
                this.ViewState["DefaultText"] = value;
            }
        }

        /// <summary>
        /// An Icon that will be applied to the status element and is expected to provide a background image that will serve as the status bar icon (defaults to 'Icon.None'). The Icons is applied directly to the div that also contains the status text, so the rule should provide the appropriate padding on the div to make room for the image.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("An Icon that will be applied to the status element and is expected to provide a background image that will serve as the status bar icon (defaults to 'Icon.None'). The Icons is applied directly to the div that also contains the status text, so the rule should provide the appropriate padding on the div to make room for the image.")]
        [AjaxEventUpdate(MethodName="SetIcon")]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        [ClientConfig("iconCls")]
        [DefaultValue("")]
        internal virtual string IconClsProxy
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.Icon);
                }
                return this.IconCls;
            }
        }

        /// <summary>
        /// A CSS class that will be applied to the status element and is expected to provide a background image that will serve as the status bar icon (defaults to ''). The class is applied directly to the div that also contains the status text, so the rule should provide the appropriate padding on the div to make room for the image.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A CSS class that will be applied to the status element and is expected to provide a background image that will serve as the status bar icon (defaults to ''). The class is applied directly to the div that also contains the status text, so the rule should provide the appropriate padding on the div to make room for the image.")]
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        public virtual string IconCls
        {
            get
            {
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        /// <summary>
        /// The alignment of the status element within the overall StatusBar layout. When the StatusBar is rendered, it creates an internal div containing the status text and icon. Any additional Toolbar items added in the StatusBar's items config, or added via add or any of the supported add* methods, will be rendered, in added order, to the opposite side. The status element is greedy, so it will automatically expand to take up all sapce left over by any other items.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(StatusAlign.Left)]
        [Description("The alignment of the status element within the overall StatusBar layout. When the StatusBar is rendered, it creates an internal div containing the status text and icon. Any additional Toolbar items added in the StatusBar's items config, or added via add or any of the supported add* methods, will be rendered, in added order, to the opposite side. The status element is greedy, so it will automatically expand to take up all sapce left over by any other items.")]
        [NotifyParentProperty(true)]
        public virtual StatusAlign StatusAlign
        {
            get
            {
                object obj = this.ViewState["StatusAlign"];
                return (obj == null) ? StatusAlign.Left : (StatusAlign)obj;
            }
            set
            {
                this.ViewState["StatusAlign"] = value;
            }
        }

        /// <summary>
        /// A string that will be rendered into the status element as the status message (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Description("A string that will be rendered into the status element as the status message (defaults to '').")]
        [AjaxEventUpdate(MethodName = "SetText")]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }


        /*  Listeners and AjaxEvents
            -----------------------------------------------------------------------------------------------*/

        private ToolbarListeners listeners;

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
        public ToolbarListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ToolbarListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private StatusBarAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public StatusBarAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new StatusBarAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Clears the status text and iconCls. Also supports clearing via an optional fade out animation.
        /// </summary>
        [Description("Clears the status text and iconCls. Also supports clearing via an optional fade out animation.")]
        public virtual void ClearStatus()
        {
            string template = "{0}.clearStatus();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Clears the status text and iconCls. Also supports clearing via an optional fade out animation.
        /// </summary>
        [Description("Clears the status text and iconCls. Also supports clearing via an optional fade out animation.")]
        public virtual void ClearStatus(StatusBarClearStatusConfig config)
        {
            string template = "{0}.clearStatus({1});";
            this.AddScript(template, this.ClientID, config.ToJsonString());
        }

        /// <summary>
        /// Convenience method for setting the status icon directly. For more flexible options see setStatus. See Icon or IconCls for complete details about customizing the icon. If empty string any iconCls will be cleared. 
        /// </summary>
        [Description("Convenience method for setting the status icon directly. For more flexible options see setStatus. See Icon or IconCls for complete details about customizing the icon. If empty string any iconCls will be cleared.")]
        protected virtual void SetIcon(Icon icon)
        {
            this.SetIconClass(ScriptManager.GetIconClassName(icon));
        }

        /// <summary>
        /// Convenience method for setting the status icon directly. For more flexible options see setStatus. See Icon or IconCls for complete details about customizing the icon. If empty string any iconCls will be cleared. 
        /// </summary>
        [Description("Convenience method for setting the status icon directly. For more flexible options see setStatus. See Icon or IconCls for complete details about customizing the icon. If empty string any iconCls will be cleared.")]
        protected virtual void SetIconClass(string iconCls)
        {
            string template = "{0}.setIcon(\"{1}\");";
            this.AddScript(template, this.ClientID, iconCls);
        }

        /// <summary>
        /// Sets the status text and/or iconCls. Also supports automatically clearing the status that was set after a specified interval.
        /// </summary>
        [Description("Sets the status text and/or iconCls. Also supports automatically clearing the status that was set after a specified interval.")]
        public virtual void SetStatus(string text)
        {
            string template = "{0}.setStatus(\"{1}\");";
            this.AddScript(template, this.ClientID, text);
        }

        /// <summary>
        /// Sets the status text and/or iconCls. Also supports automatically clearing the status that was set after a specified interval.
        /// </summary>
        [Description("Sets the status text and/or iconCls. Also supports automatically clearing the status that was set after a specified interval.")]
        public virtual void SetStatus(StatusBarStatusConfig config)
        {
            string template = "{0}.setStatus({1});";
            this.AddScript(template, this.ClientID, config.Serialize());
        }

        /// <summary>
        /// Convenience method for setting the status text directly. For more flexible options see setStatus.
        /// </summary>
        [Description("Convenience method for setting the status text directly. For more flexible options see setStatus.")]
        protected virtual void SetText(string text)
        {
            string template = "{0}.setText(\"{1}\");";
            this.AddScript(template, this.ClientID, text);
        }

        /// <summary>
        /// Convenience method for setting the status text directly. For more flexible options see setStatus.
        /// </summary>
        /// <param name="text">A string to use as the status text (in which case all other options for setStatus will be defaulted)</param>
        [Description("Convenience method for setting the status text and icon to special values that are pre-configured to indicate a 'busy' state, usually for loading or processing activities.")]
        public virtual void ShowBusy(string text)
        {
            string template = "{0}.showBusy(\"{1}\");";
            this.AddScript(template, this.ClientID, text);
        }
    }

    [ToolboxItem(false)]
    [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
    public class StatusBarStatusConfig
    {
        public StatusBarStatusConfig() { }

        public StatusBarStatusConfig(string text) 
        {
            this.Text = text;
        }

        public StatusBarStatusConfig(string text, string iconCls)
        {
            this.Text = text;
            this.IconCls = iconCls;
        }

        public StatusBarStatusConfig(string text, Icon icon)
        {
            this.Text = text;
            this.Icon = icon;
        }

        public StatusBarStatusConfig(string text, StatusBarClearConfig config)
        {
            this.Text = text;
            this.Clear = config;
        }

        public virtual string Serialize()
        {
            return new ClientConfig().Serialize(this);
        }

        string text = "";

        /// <summary>
        /// The status text to display. If not specified, any current status text will remain unchanged.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetText")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The status text to display. If not specified, any current status text will remain unchanged.")]
        public virtual string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        Icon icon = Icon.None;

        /// <summary>
        /// An Icon that will be applied to the status element and is expected to provide a background image that will serve as the status bar icon (defaults to 'Icon.None'). The Icons is applied directly to the div that also contains the status text, so the rule should provide the appropriate padding on the div to make room for the image.
        /// </summary>
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetIcon")]
        [NotifyParentProperty(true)]
        [Description("The status text to display. If not specified, any current status text will remain unchanged.")]
        public virtual Icon Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }

        [ClientConfig("iconCls")]
        [DefaultValue("")]
        internal virtual string IconClsProxy
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.Icon);
                }
                return this.IconCls;
            }
        }

        string iconCls = "";

        /// <summary>
        /// The CSS class used to customize the status icon (see iconCls for details). If not specified, any current iconCls will remain unchanged.
        /// </summary>
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [NotifyParentProperty(true)]
        [Description("The CSS class used to customize the status icon (see iconCls for details). If not specified, any current iconCls will remain unchanged.")]
        public virtual string IconCls
        {
            get
            {
                return this.iconCls;
            }
            set
            {
                this.iconCls = value;
            }
        }

        [ClientConfig("clear", JsonMode.Raw)]
        [DefaultValue("")]
        internal virtual string ClearProxy
        {
            get
            {
                if (this.Clear != null)
                {
                    return this.Clear.ToJsonString();
                }
                if (this.Clear2)
                {
                    return JSON.Serialize(this.Clear2);
                }
                if (this.Clear3 != -1)
                {
                    return JSON.Serialize(this.Clear3);
                }
                return "";
            }
        }

        StatusBarClearConfig clear = null;

        /// <summary>
        /// Allows you to set an internal callback that will automatically clear the status text and iconCls after a specified amount of time has passed. If clear is not specified, the new status will not be auto-cleared and will stay until updated again or cleared using clearStatus.
        /// </summary>
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        [Description("Allows you to set an internal callback that will automatically clear the status text and iconCls after a specified amount of time has passed. If clear is not specified, the new status will not be auto-cleared and will stay until updated again or cleared using clearStatus.")]
        public virtual StatusBarClearConfig Clear
        {
            get
            {
                return this.clear;
            }
            set
            {
                this.clear = value;
            }
        }

        bool clearBoolean = false;

        /// <summary>
        /// If true is passed, the status will be cleared using autoClear, defaultText and defaultIconCls via a fade out animation.
        /// </summary>
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("If true is passed, the status will be cleared using autoClear, defaultText and defaultIconCls via a fade out animation.")]
        public virtual bool Clear2
        {
            get
            {
                return this.clearBoolean;
            }
            set
            {
                this.clearBoolean = value;
            }
        }

        int clearInt = -1;

        /// <summary>
        /// If a numeric value is passed, it will be used as the callback interval (in milliseconds), overriding the autoClear value.
        /// </summary>
        [DefaultValue(-1)]
        [NotifyParentProperty(true)]
        [Description("If a numeric value is passed, it will be used as the callback interval (in milliseconds), overriding the autoClear value.")]
        public virtual int Clear3
        {
            get
            {
                return this.clearInt;
            }
            set
            {
                this.clearInt = value;
            }
        }
    }


    [ToolboxItem(false)]
    [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
    public class StatusBarClearStatusConfig 
    {
        public StatusBarClearStatusConfig() { }

        public StatusBarClearStatusConfig(bool anim)
        {
            this.Anim = anim;
        }

        public StatusBarClearStatusConfig(bool anim, bool useDefaults) 
        {
            this.Anim = anim;
            this.UseDefaults = useDefaults;
        }

        public virtual string ToJsonString()
        {
            return new ClientConfig().Serialize(this);
        }

        bool anim = false;

        /// <summary>
        /// True to clear the status by fading out the status element (defaults to false which clears immediately)
        /// </summary>
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to clear the status by fading out the status element (defaults to false which clears immediately)")]
        public virtual bool Anim
        {
            get
            {
                return this.anim;
            }
            set
            {
                this.anim = value;
            }
        }

        bool useDefaults = false;

        /// <summary>
        /// True to reset the text and icon using defaultText and defaultIconCls (defaults to false which sets the text to '' and removes any existing icon class).
        /// </summary>
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to reset the text and icon using defaultText and defaultIconCls (defaults to false which sets the text to '' and removes any existing icon class).")]
        public virtual bool UseDefaults
        {
            get
            {
                return this.useDefaults;
            }
            set
            {
                this.useDefaults = value;
            }
        }
    }

    [ToolboxItem(false)]
    [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
    public class StatusBarClearConfig
    {
        public StatusBarClearConfig() { }

        public StatusBarClearConfig(bool anim) 
        {
            this.Anim = anim;
        }

        public StatusBarClearConfig(bool anim, bool useDefaults)
        {
            this.Anim = anim;
            this.UseDefaults = useDefaults;
        }

        public StatusBarClearConfig(bool anim, bool useDefaults, int wait)
        {
            this.Anim = anim;
            this.UseDefaults = useDefaults;
            this.Wait = wait;
        }

        public virtual string ToJsonString()
        {
            return (!this.IsDefault) ? new ClientConfig().Serialize(this) : "";
        }

        public virtual bool IsDefault
        {
            get
            {
                return (this.Wait == -1 && !this.Anim && this.UseDefaults);
            }
        }

        bool anim = false;

        /// <summary>
        /// False to clear the status immediately once the callback executes (defaults to true which fades the status out).
        /// </summary>
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("False to clear the status immediately once the callback executes (defaults to true which fades the status out).")]
        public virtual bool Anim 
        {
            get
            {
                return this.anim;
            }
            set
            {
                this.anim = value;
            }
        }

        bool useDefaults = true;

        /// <summary>
        /// False to completely clear the status text and iconCls (defaults to true which uses defaultText and defaultIconCls).
        /// </summary>
        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("False to completely clear the status text and iconCls (defaults to true which uses defaultText and defaultIconCls).")]
        public virtual bool UseDefaults
        {
            get
            {
                return this.useDefaults;
            }
            set
            {
                this.useDefaults = value;
            }
        }

        int wait = -1;

        /// <summary>
        /// The number of milliseconds to wait before clearing (defaults to autoClear).
        /// </summary>
        [ClientConfig]
        [DefaultValue(-1)]
        [NotifyParentProperty(true)]
        [Description("The number of milliseconds to wait before clearing (defaults to autoClear).")]
        public virtual int Wait
        {
            get
            {
                return this.wait;
            }
            set
            {
                this.wait = value;
            }
        }
    }
}