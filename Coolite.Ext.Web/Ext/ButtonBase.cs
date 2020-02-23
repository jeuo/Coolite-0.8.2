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
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [ContainerStyle("display:inline;")]
    public abstract partial class ButtonBase : Component, IIcon, IAutoPostBack, IPostBackEventHandler, IPostBackDataHandler, IButtonControl
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (!string.IsNullOrEmpty(this.OnClientClick))
            {
                this.On("click", new JFunction(TokenUtils.ParseTokens(this.OnClientClick, this), "el", "e"));
            }

            string fn = this.PostBackFunction;

            if (this.ParentForm != null && !string.IsNullOrEmpty(fn))
            {
                this.On("click", new JFunction(fn));
            }

            if(this.EnableToggle || !string.IsNullOrEmpty(this.ToggleGroup))
            {
                fn = "this.getPressedField().setValue(pressed ? \"true\" : \"false\");";
                this.On("toggle", new JFunction(fn, "b", "pressed"));
            }
        }

        internal void InitLazyMenu(Observable parent)
        {
            if(this.Menu.Primary != null)
            {
                parent.BeforeClientInit += delegate
                {
                    this.Menu.Primary.EnsureScriptRegistering(false);
                };
            }
        }

        private static readonly object EventPressedChanged = new object();

        [Category("Action")]
        public event EventHandler PressedChanged
        {
            add
            {
                this.Events.AddHandler(EventPressedChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPressedChanged, value);
            }
        }

        protected virtual void OnPressedChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventPressedChanged];
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
            string val = postCollection[string.Concat(this.ClientID, "_Pressed")];
            if (!string.IsNullOrEmpty(val))
            {
                bool pressedState;
                if (bool.TryParse(val, out pressedState))
                {
                    if (this.Pressed != pressedState)
                    {
                        try
                        {
                            this.ViewState.Suspend();
                            this.Pressed = pressedState;
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
            this.OnPressedChanged(EventArgs.Empty);
        }

        /*  PostBack
            -----------------------------------------------------------------------------------------------*/

        [DefaultValue("")]
        [UrlProperty("*.aspx")]
        [Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
        public virtual string PostBackUrl
        {
            get
            {
                return (string)this.ViewState["PostBackUrl"] ?? "";
            }
            set
            {
                this.ViewState["PostBackUrl"] = value;
            }
        }


        /*  EventClick
            -----------------------------------------------------------------------------------------------*/

        private static readonly object EventClick = new object();

        /// <summary>
        /// Fires when the button has been clicked
        /// </summary>
        [Category("Action")]
        [Description("Fires when the button has been clicked")]
        public event EventHandler Click
        {
            add
            {
                this.Events.AddHandler(EventClick, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventClick, value);
            }
        }

        protected virtual void OnClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)this.Events[EventClick];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void RaisePostBackEvent(string eventArgument)
        {
            if (this.CausesValidation)
            {
                this.Page.Validate(this.ValidationGroup);
            }
            this.OnClick(EventArgs.Empty);
            this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.RaisePostBackEvent(eventArgument);
        }


        /*  EventCommand
            -----------------------------------------------------------------------------------------------*/

        private static readonly object EventCommand = new object();

        [Category("Action")]
        public event CommandEventHandler Command
        {
            add
            {
                base.Events.AddHandler(EventCommand, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventCommand, value);
            }
        }

        protected virtual void OnCommand(CommandEventArgs e)
        {
            CommandEventHandler handler = (CommandEventHandler)base.Events[EventCommand];
            if (handler != null)
            {
                handler(this, e);
            }
            base.RaiseBubbleEvent(this, e);
        }

        public string CommandName
        {
            get
            {
                string str = (string)this.ViewState["CommandName"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }

        public string CommandArgument
        {
            get
            {
                string str = (string)this.ViewState["CommandArgument"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["CommandArgument"] = value;
            }
        }

        /// <summary>
        /// The JavaScript to execute when the Button is clicked. Or, please use the <Listeners> for more flexibility.
        /// </summary>
        [Category("Config Options")]
        [Description("The JavaScript to execute when the Button is clicked. Or, please use the <Listeners> for more flexibility.")]
        public virtual string OnClientClick
        {
            get
            {
                return (string)this.ViewState["OnClientClick"] ?? "";
            }
            set
            {
                this.ViewState["OnClientClick"] = value;
            }
        }

        /// <summary>
        /// False to not allow a pressed Button to be depressed (defaults to true). Only valid when enableToggle is true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to not allow a pressed Button to be depressed (defaults to true). Only valid when enableToggle is true.")]
        public virtual bool AllowDepress
        {
            get
            {
                object obj = this.ViewState["AllowDepress"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDepress"] = value;
            }
        }

        /// <summary>
        /// The type of event to map to the button's event handler (defaults to 'click').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("click")]
        [Description("The type of event to map to the button's event handler (defaults to 'click').")]
        public virtual string ClickEvent
        {
            get
            {
                return (string)this.ViewState["ClickEvent"] ?? "click";
            }
            set
            {
                this.ViewState["ClickEvent"] = value;
            }
        }

        /// <summary>
        /// True to enable pressed/not pressed toggling (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable pressed/not pressed toggling (defaults to false).")]
        public virtual bool EnableToggle
        {
            get
            {
                object obj = this.ViewState["EnableToggle"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableToggle"] = value;
            }
        }

        /// <summary>
        /// False to disable visual cues on mouseover, mouseout and mousedown (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable visual cues on mouseover, mouseout and mousedown (defaults to true).")]
        public virtual bool HandleMouseEvents
        {
            get
            {
                object obj = this.ViewState["HandleMouseEvents"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HandleMouseEvents"] = value;
            }
        }

        /// <summary>
        /// A function called when the button is clicked (can be used instead of click event).
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetHandler")]
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A function called when the button is clicked (can be used instead of click event).")]
        public virtual string Handler
        {
            get
            {
                return (string)this.ViewState["Handler"] ?? "";
            }
            set
            {
                this.ViewState["Handler"] = value;
            }
        }

        /// <summary>
        /// The icon to use in the Button. See also, IconCls to set an icon with a custom Css class.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Description("The icon to use in the Button. See also, IconCls to set an icon with a custom Css class.")]
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
        /// A css class which sets a background image to be used as the icon for this button.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class which sets a background image to be used as the icon for this button.")]
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

        /// <summary>
        /// False to hide the Menu arrow drop down arrow (defaults to true).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "ToggleMenuArrow")]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to hide the Menu arrow drop down arrow (defaults to true).")]
        public virtual bool MenuArrow
        {
            get
            {
                object obj = this.ViewState["MenuArrow"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MenuArrow"] = value;
            }
        }

        private MenuCollection menu;

        [ClientConfig("menu", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Standard menu attribute consisting of a reference to a menu object, a menu id or a menu config blob")]
        public virtual MenuCollection Menu
        {
            get
            {
                if (this.menu == null)
                {
                    this.menu = new MenuCollection();
                    this.menu.AfterItemAdd += this.AfterItemAdd;
                }

                return this.menu;
            }
        }

        protected virtual void AfterItemAdd(MenuBase item)
        {
            this.Controls.Add(item);
        }

        /// <summary>
        /// The position to align the menu to (see Ext.Element.alignTo for more details, defaults to 'tl-bl?').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("tl-bl")]
        [Description("The position to align the menu to (see Ext.Element.alignTo for more details, defaults to 'tl-bl?').")]
        public virtual string MenuAlign
        {
            get
            {
                return (string)this.ViewState["MenuAlign"] ?? "tl-bl";
            }
            set
            {
                this.ViewState["MenuAlign"] = value;
            }
        }

        /// <summary>
        /// The minimum width for this button (used to give a set of buttons a common width).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "16")]
        [Description("The minimum width for this button (used to give a set of buttons a common width).")]
        public virtual Unit MinWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinWidth"], Unit.Pixel(16), "MinWidth");
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        /// <summary>
        /// True to addToStart pressed (only if enableToggle = true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to addToStart pressed (only if enableToggle = true).")]
        public virtual bool Pressed
        {
            get
            {
                object obj = this.ViewState["Pressed"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Pressed"] = value;
            }
        }

        /// <summary>
        /// True to repeat fire the click event while the mouse is down. (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to repeat fire the click event while the mouse is down. (defaults to false).")]
        public virtual bool Repeat
        {
            get
            {
                object obj = this.ViewState["Repeat"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Repeat"] = value;
            }
        }

        /// <summary>
        /// The scope of the handler.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("The scope of the handler.")]
        public virtual object Scope
        {
            get
            {
                object obj = this.ViewState["Scope"];
                return (obj == null) ? null : obj;
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        /// <summary>
        /// Set a DOM tabIndex for this button (defaults to undefined).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(-1)]
        [Description("Set a DOM tabIndex for this button (defaults to undefined).")]
        new public virtual int TabIndex
        {
            get
            {
                object obj = this.ViewState["TabIndex"];
                return (obj == null) ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["TabIndex"] = value;
            }
        }

        /// <summary>
        /// The position to align the menu to (see Ext.Element.alignTo for more details, defaults to 'tl-bl?').
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The position to align the menu to (see Ext.Element.alignTo for more details, defaults to 'tl-bl?').")]
        [AjaxEventUpdate(GenerateMode = AutoGeneratingScript.WithSet)]
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

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Function called when a Button with enableToggle set to true is clicked.")]
        public virtual string ToggleHandler
        {
            get
            {
                return (string)this.ViewState["ToggleHandler"] ?? "";
            }
            set
            {
                this.ViewState["ToggleHandler"] = value;
            }
        }


        /// <summary>
        /// The group this toggle button is a member of (only 1 per group can be pressed, only applies if enableToggle = true).
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The group this toggle button is a member of (only 1 per group can be pressed, only applies if enableToggle = true).")]
        public virtual string ToggleGroup
        {
            get
            {
                return (string)this.ViewState["ToggleGroup"] ?? "";
            }
            set
            {
                this.ViewState["ToggleGroup"] = value;
            }
        }

        /// <summary>
        /// The tooltip for the button - can be a string or QuickTips config object.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue(ButtonType.Button)]
        [Description("submit, reset or button - defaults to 'button'.")]
        public virtual ButtonType Type
        {
            get
            {
                object obj = this.ViewState["Type"];
                return (obj == null) ? ButtonType.Button : (ButtonType)obj;
            }
            set
            {
                this.ViewState["Type"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control state automatically posts back to the server when button clicked.
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [Description("Gets or sets a value indicating whether the control state automatically posts back to the server when button clicked.")]
        public virtual bool AutoPostBack
        {
            get
            {
                object obj = this.ViewState["AutoPostBack"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Themeable(false)]
        [Description("Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.")]
        public virtual bool CausesValidation
        {
            get
            {
                object obj = this.ViewState["CausesValidation"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Controls ValidationGroup
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [Description("Gets or Sets the Controls ValidationGroup")]
        public virtual string ValidationGroup
        {
            get
            {
                return (string)this.ViewState["ValidationGroup"] ?? "";
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }

        /// <summary>
        /// True to apply a flat style.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to apply a flat style.")]
        public virtual bool Flat
        {
            get
            {
                object obj = this.ViewState["Flat"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Flat"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Focus the button
        /// </summary>
        [Description("Focus the button")]
        public override void Focus()
        {
            base.Focus();
        }

        /// <summary>
        /// Hide this button's menu (if it has one)
        /// </summary>
        [Description("Hide this button's menu (if it has one)")]
        public virtual void HideMenu()
        {
            this.AddScript("{0}.hideMenu();", this.ClientID);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        [Description("initComponent")]
        public virtual void InitComponent()
        {
            this.AddScript("{0}.initComponent();", this.ClientID);
        }

        /// <summary>
        /// Assigns this button's click handler
        /// </summary>
        [Description("Assigns this button's click handler")]
        protected virtual void SetHandler(string function)
        {
            this.AddScript("{0}.setHandler({1});", this.ClientID, function);
        }

        /// <summary>
        /// Assigns this button's click handler
        /// </summary>
        [Description("Assigns this button's click handler")]
        public virtual void SetHandler(string function, string scope)
        {
            this.AddScript("{0}.setHandler({1},{2});", this.ClientID, function, scope);
        }

        /// <summary>
        /// Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.
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
        /// Sets this button's text
        /// </summary>
        [Description("Sets this button's text")]
        protected virtual void SetText(string text)
        {
            this.AddScript("{0}.setText({1});", this.ClientID, JSON.Serialize(text));
        }

        /// <summary>
        /// Show this button's menu (if it has one)
        /// </summary>
        [Description("Show this button's menu (if it has one)")]
        public virtual void ShowMenu()
        {
            this.AddScript("{0}.showMenu();", this.ClientID);
        }

        /// <summary>
        /// If a state it passed, it becomes the pressed state otherwise the current state is toggled.
        /// </summary>
        [Description("If a state it passed, it becomes the pressed state otherwise the current state is toggled.")]
        public virtual void Toggle()
        {
            this.AddScript("{0}.toggle();", this.ClientID);
        }

        /// <summary>
        /// If a state it passed, it becomes the pressed state otherwise the current state is toggled.
        /// </summary>
        [Description("If a state it passed, it becomes the pressed state otherwise the current state is toggled.")]
        public virtual void Toggle(bool state)
        {
            this.AddScript("{0}.toggle({1});", this.ClientID, state.ToString().ToLower());
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

        protected virtual void ShowMenuArrow()
        {
            this.AddScript("{0}.showMenuArrow();", this.ClientID);
        }

        protected virtual void HideMenuArrow()
        {
            this.AddScript("{0}.hideMenuArrow();", this.ClientID);
        }

        /// <summary>
        /// If a state it passed, it becomes the pressed state otherwise the current state is toggled.
        /// </summary>
        [Description("If a state it passed, it becomes the pressed state otherwise the current state is toggled.")]
        public virtual void ToggleMenuArrow()
        {
            this.AddScript("{0}.toggleMenuArrow();", this.ClientID);
        }

        /// <summary>
        /// If a state it passed, it becomes the pressed state otherwise the current state is toggled.
        /// </summary>
        [Description("If a state it passed, it becomes the pressed state otherwise the current state is toggled.")]
        public virtual void ToggleMenuArrow(bool state)
        {
            if (state)
            {
                this.ShowMenuArrow();
            }
            else
            {
                this.HideMenuArrow();
            }
        }
    }
}