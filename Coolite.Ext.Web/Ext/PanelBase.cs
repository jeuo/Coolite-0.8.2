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
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [Xtype("panel")]
    [InstanceOf(ClassName = "Ext.Panel")]
    public abstract class PanelBase : Container, IIcon, IPostBackDataHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (!this.IsInLayout && this.HasLayout() && !(this is Window))
            {
                /// HACK: Avoids adding value to ViewState when it's not really necessary
                this.CustomConfig.Add(new ConfigItem("monitorResize", "true", ParameterMode.Raw));
            }

            if (this.LoadMask.ShowMask && !this.AutoLoad.IsDefault)
            {
                if (!this.Page.IsPostBack)
                {
                    string handler = string.Format("this.loadMask=new Ext.LoadMask(this.body, {0});", new ClientConfig().Serialize(this.LoadMask));
                }
            }
        }

        protected virtual bool IsCollapsible
        {
            get
            {
                if (this.Collapsible)
                {
                    return true;
                }

                if (this.Parent is Accordion)
                {
                    return true;
                }

                if (this.Parent is BorderLayout)
                {
                    return ((BorderLayoutRegion)this.AdditionalConfig).Collapsible;
                }

                return false;
            }
        }

        private static readonly object EventPanelStateChanged = new object();

        /// <summary>
        /// Fires when the panels state is changed.
        /// </summary>
        [Category("Action")]
        [Description("Fires when the panels state is changed.")]
        public event EventHandler StateChanged
        {
            add
            {
                this.Events.AddHandler(EventPanelStateChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPanelStateChanged, value);
            }
        }

        protected virtual void OnStateChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)this.Events[EventPanelStateChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return this.LoadPostData(postDataKey, postCollection);
        }

        protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[string.Concat(this.ClientID, "_Collapsed")];
            if (!string.IsNullOrEmpty(val))
            {
                bool collapsedState;
                if (bool.TryParse(val, out collapsedState))
                {
                    if (this.Collapsed != collapsedState)
                    {
                        try
                        {
                            this.ViewState.Suspend();
                            this.Collapsed = collapsedState;
                        }
                        finally
                        {
                            this.ViewState.Resume();
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.RaisePostDataChangedEvent();
        }

        protected virtual void RaisePostDataChangedEvent()
        {
            this.OnStateChanged(EventArgs.Empty);
        }
        
        //[AjaxEventUpdate(MethodName = "SetTitle")] //method which register script
        //[AjaxEventUpdate(Script = "{0}.setTitle({1});")] // predefined script
        //[AjaxEventUpdate] //autogenerate
        //[AjaxEventUpdate(Script = "{0}.animCollapse={1};")]
        /// <summary>
        /// True to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext.Fx class is available, otherwise false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext.Fx class is available, otherwise false).")]
        [NotifyParentProperty(true)]    
        public virtual bool AnimCollapse
        {
            get
            {
                object obj = this.ViewState["AnimCollapse"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AnimCollapse"] = value;
            }
        }

        private LoadConfig autoLoad;

        /// <summary>
        /// A valid url spec according to the UpdateOptions Ext.UpdateOptions.update method. If autoLoad is not null, the panel will attempt to load its contents immediately upon render. The URL will become the default URL for this panel's body element, so it may be refreshed at any time.
        /// </summary>
        [AjaxEventUpdate(MethodName = "LoadContent")]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("A valid url spec according to the UpdateOptions Ext.UpdateOptions.update method. If autoLoad is not null, the panel will attempt to load its contents immediately upon render. The URL will become the default URL for this panel's body element, so it may be refreshed at any time.")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual LoadConfig AutoLoad
        {
            get
            {
                if (this.autoLoad == null)
                {
                    this.autoLoad = new LoadConfig();
                    this.autoLoad.TrackViewState();
                }

                return this.autoLoad;
            }
        }

        [ClientConfig("autoLoad", JsonMode.Raw)]
        [DefaultValue("")]
        internal string AutoLoadProxy
        {
            get
            {
                if (!this.AutoLoad.IsDefault)
                {
                    return new ClientConfig().Serialize(this.AutoLoad);
                }
                return "";
            }
        }

        /// <summary>
        /// True to use overflow:'auto' on the panel's body element and show scroll bars automatically when necessary, false to clip any overflowing content (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to use overflow:'auto' on the panel's body element and show scroll bars automatically when necessary, false to clip any overflowing content (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool AutoScroll
        {
            get
            {
                object obj = this.ViewState["AutoScroll"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoScroll"] = value;
            }
        }

        /// <summary>
        /// The base CSS class to apply to this panel's element (defaults to 'x-panel').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The base CSS class to apply to this panel's element (defaults to 'x-panel').")]
        [NotifyParentProperty(true)]    
        public virtual string BaseCls
        {
            get
            {
                return (string)this.ViewState["BaseCls"] ?? "";
            }
            set
            {
                this.ViewState["BaseCls"] = value;
            }
        }

        private ToolbarCollection bottomBar;

        /// <summary>
        /// The bottom toolbar of the panel.
        /// </summary>
        [ClientConfig("bbar", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The bottom toolbar of the panel.")]
        public virtual ToolbarCollection BottomBar
        {
            get
            {
                if (this.bottomBar == null)
                {
                    this.bottomBar = new ToolbarCollection();
                    this.bottomBar.AfterItemAdd += this.AfterItemAdd;
                }

                return this.bottomBar;
            }
        }

        private ToolbarCollection topBar;

        [ClientConfig("tbar", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The top toolbar of the panel.")]
        public virtual ToolbarCollection TopBar
        {
            get
            {
                if (this.topBar == null)
                {
                    this.topBar = new ToolbarCollection();
                    this.topBar.AfterItemAdd += this.AfterItemAdd;
                }

                return this.topBar;
            }
        }

        /// <summary>
        /// True to display an interior border on the body element of the panel, false to hide it (defaults to true). This only applies when border == true. If border == true and bodyBorder == false, the border will display as a 1px wide inset border, giving the entire body element an inset appearance.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display an interior border on the body element of the panel, false to hide it (defaults to true). This only applies when border == true. If border == true and bodyBorder == false, the border will display as a 1px wide inset border, giving the entire body element an inset appearance.")]
        [NotifyParentProperty(true)]    
        public virtual bool BodyBorder
        {
            get
            {
                object obj = this.ViewState["BodyBorder"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["BodyBorder"] = value;
            }
        }

        /// <summary>
        /// Custom CSS styles to be applied to the body element in the format expected by Ext.Element.applyStyles (defaults to null).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "ApplyBodyStyles")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Custom CSS styles to be applied to the body element in the format expected by Ext.Element.applyStyles (defaults to null).")]
        [NotifyParentProperty(true)]    
        public virtual string BodyStyle
        {
            get
            {
                string style = (string)this.ViewState["BodyStyle"] ?? "";
                if (!string.IsNullOrEmpty(style))
                {
                    if (!style.EndsWith(";"))
                    {
                        style += ";";
                    }
                }
                return style;
            }
            set
            {
                this.ViewState["BodyStyle"] = value;
            }
        }

        /// <summary>
        /// True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the border is a 2px wide inset border, but this can be further altered by setting bodyBorder to false.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the borders of the panel's body element, false to hide them (defaults to true). By default, the border is a 2px wide inset border, but this can be further altered by setting bodyBorder to false.")]
        [NotifyParentProperty(true)]    
        public virtual bool Border
        {
            get
            {
                object obj = this.ViewState["Border"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Border"] = value;
            }
        }

        [ClientConfig("border")]
        [DefaultValue(true)]
        internal virtual bool BorderProxy
        {
            get
            {
                return !this.BodyBorder ? false : this.Border;
            }
        }

        /// <summary>
        /// Valid values are "left", "center" and "right" (defaults to "right").
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(Alignment.Right)]
        [Description("Valid values are \"left\", \"center\" and \"right\" (defaults to \"right\").")]
        [NotifyParentProperty(true)]    
        public virtual Alignment ButtonAlign
        {
            get
            {
                object obj = this.ViewState["ButtonAlign"];
                return (obj == null) ? Alignment.Right : (Alignment)obj;
            }
            set
            {
                this.ViewState["ButtonAlign"] = value;
            }
        }

        private ItemsCollection<ButtonBase> buttons;

        /// <summary>
        /// A collection of buttons configs used to add buttons to the footer of this panel
        /// </summary>
        [ClientConfig("buttons", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("A collection of buttons configs used to add buttons to the footer of this panel.")]
        public virtual ItemsCollection<ButtonBase> Buttons
        {
            get
            {
                if (this.buttons == null)
                {
                    this.buttons = new ItemsCollection<ButtonBase>();
                    this.buttons.AfterItemAdd += this.AfterItemAdd;
                }

                return this.buttons;
            }
        }

        protected new virtual void AfterItemAdd(Component item)
        {
            this.Controls.Add(item);
            
            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

        /// <summary>
        /// True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the panel's title bar, false to render it last (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the panel's title bar, false to render it last (defaults to true).")]
        [NotifyParentProperty(true)]    
        public virtual bool CollapseFirst
        {
            get
            {
                object obj = this.ViewState["CollapseFirst"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CollapseFirst"] = value;
            }
        }

        /// <summary>
        /// True to render the panel collapsed, false to render it expanded (defaults to false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "CollapsedProxy")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render the panel collapsed, false to render it expanded (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool Collapsed
        {
            get
            {
                object obj = this.ViewState["Collapsed"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Collapsed"] = value;
            }
        }

        /// <summary>
        /// A CSS class to add to the panel's element after it has been collapsed (defaults to 'x-panel-collapsed').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class to add to the panel's element after it has been collapsed (defaults to 'x-panel-collapsed').")]
        [NotifyParentProperty(true)]    
        public virtual string CollapsedCls
        {
            get
            {
                return (string)this.ViewState["CollapsedCls"] ?? "";
            }
            set
            {
                this.ViewState["CollapsedCls"] = value;
            }
        }

        /// <summary>
        /// True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header tool button area, false to keep the panel statically sized with no button (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header tool button area, false to keep the panel statically sized with no button (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool Collapsible
        {
            get
            {
                object obj = this.ViewState["Collapsible"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Collapsible"] = value;
            }
        }

        /// <summary>
        /// True to enable dragging of this Panel (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable dragging of this Panel (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool Draggable
        {
            get
            {
                object obj = this.ViewState["Draggable "];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Draggable "] = value;
            }
        }

        /// <summary>
        /// A comma-delimited list of panel elements to initialize when the panel is rendered.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A comma-delimited list of panel elements to initialize when the panel is rendered.")]
        [NotifyParentProperty(true)]    
        public virtual string Elements
        {
            get
            {
                return (string)this.ViewState["Elements"] ?? "";
            }
            set
            {
                this.ViewState["Elements"] = value;
            }
        }

        /// <summary>
        /// True to float the panel (absolute position it with automatic shimming and shadow), false to display it inline where it is rendered (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to float the panel (absolute position it with automatic shimming and shadow), false to display it inline where it is rendered (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool Floating
        {
            get
            {
                object obj = this.ViewState["Floating"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Floating "] = value;
            }
        }

        /// <summary>
        /// True to create the footer element explicitly, false to skip creating it. By default, when footer is not specified, if one or more buttons have been added to the panel the footer will be created automatically, otherwise it will not.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to create the footer element explicitly, false to skip creating it. By default, when footer is not specified, if one or more buttons have been added to the panel the footer will be created automatically, otherwise it will not.")]
        [NotifyParentProperty(true)]    
        public virtual bool Footer
        {
            get
            {
                object obj = this.ViewState["Footer"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Footer"] = value;
            }
        }

        /// <summary>
        /// True to render the panel with custom rounded borders, false to render with plain 1px square borders (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render the panel with custom rounded borders, false to render with plain 1px square borders (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool Frame
        {
            get
            {
                object obj = this.ViewState["Frame"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Frame"] = value;
            }
        }

        /// <summary>
        /// True to create the header element explicitly, false to skip creating it. By default, when header is not specified, if a title is set the header will be created automatically, otherwise it will not. If a title is set but header is explicitly set to false, the header will not be rendered.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to create the header element explicitly, false to skip creating it. By default, when header is not specified, if a title is set the header will be created automatically, otherwise it will not. If a title is set but header is explicitly set to false, the header will not be rendered.")]
        [NotifyParentProperty(true)]    
        public virtual bool Header
        {
            get
            {
                object obj = this.ViewState["Header"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Header"] = value;
            }
        }

        /// <summary>
        /// True to display the panel title in the header, false to hide it (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the panel title in the header, false to hide it (defaults to true).")]
        [NotifyParentProperty(true)]    
        public virtual bool HeaderAsText
        {
            get
            {
                object obj = this.ViewState["HeaderAsText"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HeaderAsText"] = value;
            }
        }

        /// <summary>
        /// True to hide the expand/collapse toggle button when collapsible = true, false to display it (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the expand/collapse toggle button when collapsible = true, false to display it (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool HideCollapseTool
        {
            get
            {
                object obj = this.ViewState["HideCollapseTool"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideCollapseTool"] = value;
            }
        }

        /// <summary>
        /// An HTML fragment, or a DomHelper specification to use as the panel's body content (defaults to '').
        /// </summary>
        [AjaxEventUpdate(MethodName = "Update")]
        [ClientConfig("html")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An HTML fragment, or a DomHelper specification to use as the panel's body content (defaults to '').")]
        [NotifyParentProperty(true)]    
        public virtual string Html
        {
            get
            {
                return (string)this.ViewState["Html"] ?? "";
            }
            set
            {
                this.ViewState["Html"] = value;
            }
        }

        /// <summary>
        /// The icon to use in the Title bar. See also, IconCls to set an icon with a custom Css class.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Description("The icon to use in the Title bar. See also, IconCls to set an icon with a custom Css class.")]
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
        /// A CSS class that will provide a background image to be used as the panel header icon (defaults to '').
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class that will provide a background image to be used as the panel header icon (defaults to '').")]
        [NotifyParentProperty(true)]
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

        private KeyBindingCollection keyMap;
        /// <summary>
        /// A KeyMap config object (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to this panel (defaults to null).
        /// </summary>
        [ClientConfig("keys", JsonMode.Array)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("A KeyMap config object (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to this panel (defaults to null).")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual KeyBindingCollection KeyMap
        {
            get
            {
                if(this.keyMap == null)
                {
                    this.keyMap = new KeyBindingCollection();
                    this.keyMap.AfterItemAdd += this.AfterKeyBindingAdd;
                }

                return this.keyMap;
            }
        }

        protected virtual void AfterKeyBindingAdd(KeyBinding keyBinding)
        {
            keyBinding.Owner = this;
            keyBinding.Listeners.Event.Owner = this;
        }

        /// <summary>
        /// True to mask the panel when it is disabled, false to not mask it (defaults to true). Either way, the panel will always tell its contained elements to disable themselves when it is disabled, but masking the panel can provide an additional visual cue that the panel is disabled.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to mask the panel when it is disabled, false to not mask it (defaults to true). Either way, the panel will always tell its contained elements to disable themselves when it is disabled, but masking the panel can provide an additional visual cue that the panel is disabled.")]
        [NotifyParentProperty(true)]    
        public virtual bool MaskDisabled
        {
            get
            {
                object obj = this.ViewState["MaskDisabled"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MaskDisabled"] = value;
            }
        }

        /// <summary>
        /// Minimum width in pixels of all buttons in this panel (defaults to 75).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "75")]
        [Description("Minimum width in pixels of all buttons in this panel (defaults to 75).")]
        [NotifyParentProperty(true)]    
        public virtual Unit MinButtonWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinButtonWidth"], Unit.Pixel(75), "MinButtonWidth");
            }
            set
            {
                this.ViewState["MinButtonWidth"] = value;
            }
        }

        /// <summary>
        /// ShadowMode to display a shadow behind the panel. Note that this option only applies when floating = true.
        /// </summary>
        [ClientConfig(typeof(ShadowJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ShadowMode.Sides)]
        [Description("ShadowMode to display a shadow behind the panel. Note that this option only applies when floating = true.")]
        [NotifyParentProperty(true)]    
        public virtual ShadowMode Shadow
        {
            get
            {
                object obj = this.ViewState["Shadow"];
                return (obj == null) ? ShadowMode.Sides : (ShadowMode)obj;
            }
            set
            {
                this.ViewState["Shadow"] = value;
            }
        }

        /// <summary>
        /// The number of pixels to offset the shadow if displayed (defaults to 4). Note that this option only applies when floating = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(4)]
        [Description("The number of pixels to offset the shadow if displayed (defaults to 4). Note that this option only applies when floating = true.")]
        [NotifyParentProperty(true)]    
        public virtual int ShadowOffset
        {
            get
            {
                object obj = this.ViewState["ShadowOffset"];
                return (obj == null) ? 4 : (int)obj;
            }
            set
            {
                this.ViewState["ShadowOffset"] = value;
            }
        }

        /// <summary>
        /// False to disable the iframe shim in browsers which need one (defaults to true). Note that this option only applies when floating = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable the iframe shim in browsers which need one (defaults to true). Note that this option only applies when floating = true.")]
        [NotifyParentProperty(true)]
        public virtual bool Shim
        {
            get
            {
                object obj = this.ViewState["Shim"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Shim"] = value;
            }
        }

        /// <summary>
        /// Adds a tooltip when mousing over the tab of a Ext.Panel which is an item of a Ext.TabPanel. Ext.QuickTips.init() must be called in order for the tips to render.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Adds a tooltip when mousing over the tab of a Ext.Panel which is an item of a Ext.TabPanel. Ext.QuickTips.init() must be called in order for the tips to render.")]
        [NotifyParentProperty(true)]
        public virtual string TabTip
        {
            get
            {
                return (string)this.ViewState["TabTip"] ?? "";
            }
            set
            {
                this.ViewState["TabTip"] = value;
            }
        }

        private LoadMask loadMask;

        /// <summary>
        /// An Ext.LoadMask to mask the Panel while loading (defaults to false).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An Ext.LoadMask to mask the Panel while loading (defaults to false).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual LoadMask LoadMask
        {
            get
            {
                if (this.loadMask == null)
                {
                    this.loadMask = new LoadMask();
                    this.loadMask.TrackViewState();
                }

                return this.loadMask;
            }
        }

        /// <summary>
        /// The title text to display in the panel header (defaults to ''). When a title is specified the header element will automatically be created and displayed unless header is explicitly set to false. If you don't want to specify a title at config time, but you may want one later, you must either specify a non-empty title (a blank space ' ' will do) or header:true so that the contentContainer element will get created.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetTitle")] //method which register script
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("The title text to display in the panel header (defaults to ''). When a title is specified the header element will automatically be created and displayed unless header is explicitly set to false. If you don't want to specify a title at config time, but you may want one later, you must either specify a non-empty title (a blank space ' ' will do) or header:true so that the contentContainer element will get created.")]
        public virtual string Title
        {
            get
            {
                return (string)this.ViewState["Title"] ?? "";
            }
            set
            {
                this.ViewState["Title"] = value;
            }
        }

        /// <summary>
        /// True to allow expanding and collapsing the panel (when collapsible = true) by clicking anywhere in the header bar, false to allow it only by clicking to tool button (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to allow expanding and collapsing the panel (when collapsible = true) by clicking anywhere in the header bar, false to allow it only by clicking to tool button (defaults to false).")]
        public virtual bool TitleCollapse
        {
            get
            {
                object obj = this.ViewState["TitleCollapse"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TitleCollapse"] = value;
            }
        }

        private ToolsCollection tools;

        /// <summary>
        /// An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element referenced by a public property called tools.<tool-type>.
        /// </summary>
        [ClientConfig(JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element referenced by a public property called tools.<tool-type>.")]
        public virtual ToolsCollection Tools
        {
            get
            {
                if (this.tools == null)
                {
                    this.tools = new ToolsCollection();
                }

                return this.tools;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.
        /// </summary>
        /// <param name="animate">if set to <c>true</c> [animate].</param>
        [Description("Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.")]
        public virtual void ApplyBodyStyles(string style)
        {
            string template = "{0}.getBody().applyStyles({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(style));
        }

        /// <summary>
        /// Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.
        /// </summary>
        [Description("Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.")]
        public virtual void Collapse()
        {
            this.Collapse(this.AnimCollapse);
        }

        /// <summary>
        /// Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.
        /// </summary>
        /// <param name="animate">if set to <c>true</c> [animate].</param>
        [Description("Collapses the panel body so that it becomes hidden. Fires the beforecollapse event which will cancel the collapse action if it returns false.")]
        public virtual void Collapse(bool animate)
        {
            this.AddScript("{0}.collapse({1});", this.ClientID, animate.ToString().ToLower());
        }

        /// <summary>
        /// Expands the panel body so that it becomes visible. Fires the beforeexpand event which will cancel the expand action if it returns false.
        /// </summary>
        [Description("Expands the panel body so that it becomes visible. Fires the beforeexpand event which will cancel the expand action if it returns false.")]
        public virtual void Expand()
        {
            this.AddScript("{0}.expand();", this.ClientID);
        }

        /// <summary>
        /// Expands the panel body so that it becomes visible. Fires the beforeexpand event which will cancel the expand action if it returns false.
        /// </summary>
        [Description("Expands the panel body so that it becomes visible. Fires the beforeexpand event which will cancel the expand action if it returns false.")]
        public virtual void Expand(bool animate)
        {
            this.AddScript("{0}.expand({1});", this.ClientID, animate.ToString().ToLower());
        }

        /// <summary>
        /// Clear loaded content
        /// </summary>
        [Description("Clear loaded content.")]
        public virtual void ClearContent()
        {
            string template = "{0}.clearContent();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Loads this content panel immediately with content returned from an XHR call.
        /// </summary>
        [Description("Loads this content panel immediately with content returned from an XHR call.")]
        public virtual void LoadContent()
        {
            string template = "{0}.load({1});";
            this.AddScript(template, this.ClientID, this.AutoLoad.ToJsonString());
        }

        /// <summary>
        /// Loads this content panel immediately with content returned from an XHR call.
        /// </summary>
        [Description("Loads this content panel immediately with content returned from an XHR call.")]
        public virtual void LoadContent(string url)
        {
            this.AddScript("{0}.load({1});", this.ClientID, new LoadConfig(url).ToJsonString());
        }

        /// <summary>
        /// Loads this content panel immediately with content returned from an XHR call.
        /// </summary>
        [Description("Loads this content panel immediately with content returned from an XHR call.")]
        public virtual void LoadContent(string url, bool noCache)
        {
            this.AddScript("{0}.load({1});", this.ClientID, new LoadConfig(url, LoadMode.Merge, noCache).ToJsonString());
        }

        /// <summary>
        /// Loads this content panel immediately with content returned from an XHR call.
        /// </summary>
        [Description("Loads this content panel immediately with content returned from an XHR call.")]
        public virtual void LoadContent(LoadConfig config)
        {
            this.AddScript("{0}.load({1});", this.ClientID, config.ToJsonString());
        }

        /// <summary>
        /// Loads this content panel immediately with content returned from an XHR call.
        /// </summary>
        [Description("Loads this content panel immediately with content returned from an XHR call.")]
        public virtual void LoadContent(JFunction fn)
        {
            this.AddScript("{0}.load({1});", this.ClientID, fn.ToString());
        }

        /// <summary>
        /// Reloads the content panel based on the current LoadConfig.
        /// </summary>
        [Description("Reloads the content panel based on the current LoadConfig.")]
        public virtual void Reload()
        {
            this.AddScript("{0}.reload();", this.ClientID);
        }

        /// <summary>
        /// Updates the content of the Panel body with the supplied string ('html') value.
        /// </summary>
        [Description("Updates the content of the Panel body with the supplied string ('html') value.")]
        protected virtual void SetHtml(string html)
        {
            this.Update(html);
        }

        /// <summary>
        /// Sets the CSS class that provides the icon image for this panel. This method will replace any existing icon class if one has already been set.
        /// </summary>
        [Description("Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.")]
        protected virtual void SetIconClass(string cls)
        {
            this.AddScript("{0}.setIconClass({1});", this.ClientID, JSON.Serialize(cls));
        }

        /// <summary>
        /// Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.
        /// </summary>
        protected virtual void SetIconClass(Icon icon)
        {
            if (this.Icon != Icon.None)
            {
                this.SetIconClass(ScriptManager.GetIconClassName(icon)); 
            }
            else
            {
                this.SetIconClass(""); 
            }
        }

        /// <summary>
        /// Sets the title text for the panel and optionally the icon class.
        /// </summary>
        [Description("Sets the title text for the panel and optionally the icon class.")]
        public virtual void SetTitle(string title)
        {
            string template = "{0}.setTitle({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(title));
        }

        /// <summary>
        /// Sets the title text for the panel and optionally the icon class.
        /// </summary>
        [Description("Sets the title text for the panel and optionally the icon class.")]
        public virtual void SetTitle(string title, string cls)
        {
            string template = "{0}.setTitle({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(title), JSON.Serialize(cls));
        }

        /// <summary>
        /// Shortcut for performing an expand or collapse based on the current state of the panel.
        /// </summary>
        [Description("Shortcut for performing an expand or collapse based on the current state of the panel.")]
        public virtual void ToggleCollapse()
        {
            this.ToggleCollapse(true);
        }

        /// <summary>
        /// Shortcut for performing an expand or collapse based on the current state of the panel.
        /// </summary>
        [Description("Shortcut for performing an expand or collapse based on the current state of the panel.")]
        public virtual void ToggleCollapse(bool animate)
        {
            string template = "{0}.toggleCollapse({1});";
            this.AddScript(template, this.ClientID, animate.ToString().ToLower());
        }

        /// <summary>
        /// AjaxEvent proxy method for .Collapsed property.
        /// </summary>
        [Description("AjaxEvent proxy method for .Collapsed property.")]
        protected virtual void CollapsedProxy(bool collapsed)
        {
            if (collapsed)
            {
                this.Collapse();
            }
            else
            {
                this.Expand();
            }
        }

        /// <summary>
        /// Update the html of the Body, optionally searching for and processing scripts.
        /// </summary>
        [Description("Update the html of the Body, optionally searching for and processing scripts.")]
        public virtual void Update(string html)
        {
            string template = "if({0}.rendered){{{0}.body.update({1});}}else{{{0}.html={1};}}";
            this.AddScript(template, this.ClientID, JSON.Serialize(html));
        }

        /// <summary>
        /// Update the html of the Body, optionally searching for and processing scripts.
        /// </summary>
        [Description("Update the html of the Body, optionally searching for and processing scripts.")]
        public virtual void Update(string html, bool loadScripts)
        {
            string template = "{0}.body.update({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(html), loadScripts.ToString().ToLower());
        }

        /// <summary>
        /// Update the html of the Body, optionally searching for and processing scripts.
        /// </summary>
        [Description("Update the html of the Body, optionally searching for and processing scripts.")]
        public virtual void Update(string html, bool loadScripts, string callback)
        {
            string template = "{0}.body.update({1},{2},{3});";
            this.AddScript(
                    template, 
                    this.ClientID, 
                    JSON.Serialize(html), 
                    loadScripts.ToString().ToLower(),
                    string.Format(ScriptManager.FunctionTemplate, callback));
        }

        /// <summary>
        /// Update the html of the Body, optionally searching for and processing scripts.
        /// </summary>
        [Description("Update the html of the Body, optionally searching for and processing scripts.")]
        public virtual void Update(string html, bool loadScripts, JFunction callback)
        {
            string template = "{0}.body.update({1},{2},{3});";
            this.AddScript(
                    template,
                    this.ClientID,
                    JSON.Serialize(html),
                    loadScripts.ToString().ToLower(),
                    callback.ToString());
        }

        List<Icon> IIcon.Icons
        {
            get
            {
                List<Icon> icons = new List<Icon>(1);
                icons.Add(this.Icon);
                return icons;
            }
        }
    }
}