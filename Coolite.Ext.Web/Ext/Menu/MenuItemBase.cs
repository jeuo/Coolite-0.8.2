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
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public abstract class MenuItemBase : BaseMenuItem, IIcon, IAutoPostBack, IPostBackEventHandler, IButtonControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(!this.DesignMode)
            {
                this.Page.LoadComplete += new EventHandler(Page_LoadComplete);
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            MenuBase parent = this.ParentMenu;

            //if (parent == null)
            //{
            //    throw new InvalidOperationException("The MenuItem can be apllyed for Menu only!");
            //}

            if (parent != null)
            {
                parent.BeforeClientInit += Menu_BeforeClientInit;
            }
        }

        protected override void OnBeforeClientInitHandler()
        {
            base.OnBeforeClientInitHandler();

            if (!string.IsNullOrEmpty(this.OnClientClick))
            {
                this.On("click", new JFunction(TokenUtils.ParseTokens(this.OnClientClick, this), "el", "e"));
            }

            string fn = this.PostBackFunction;

            if (this.ParentForm != null && !string.IsNullOrEmpty(fn))
            {
                this.On("click", new JFunction(fn));
            }
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

        [Category("Config Options")]
        [Description("The JavaScript to execute when the item is clicked. Or, please use the <Listeners> for more flexibility.")]
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
        
        public MenuBase ParentMenu
        {
            get
            {
                return (MenuBase)ReflectionUtils.GetTypeOfParent(this, typeof(MenuBase));
            }
        }

        internal Desktop ParentDesktop
        {
            get
            {
                return (Desktop)ReflectionUtils.GetTypeOfParent(this, typeof(Desktop));
            }
        }

        private void Menu_BeforeClientInit(Observable sender)
        {
            if (this.Menu.Count > 0)
            {
                this.Menu.Primary.EnsureScriptRegistering(false);
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        public override string Xtype
        {
            get { return base.Xtype; }
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True if this item can be visually activated (defaults to true).")]
        public override bool CanActivate
        {
            get
            {
                object obj = this.ViewState["CanActivate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CanActivate"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The href attribute to use for the underlying anchor link (defaults to '#').")]
        public virtual string Href
        {
            get
            {
                return (string)this.ViewState["Href"] ?? "";
            }
            set
            {
                this.ViewState["Href"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The target attribute to use for the underlying anchor link (defaults to '').")]
        public virtual string HrefTarget
        {
            get
            {
                return (string)this.ViewState["HrefTarget"] ?? "";
            }
            set
            {
                this.ViewState["HrefTarget"] = value;
            }
        }

        [ClientConfig("icon")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The path to an icon to display in this item (defaults to Ext.BLANK_IMAGE_URL). If icon is specified iconCls should not be.")]
        public virtual string IconUrl
        {
            get
            {
                return (string)this.ViewState["IconUrl"] ?? "";
            }
            set
            {
                this.ViewState["IconUrl"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A CSS class that specifies a background image that will be used as the icon for this item (defaults to ''). If iconCls is specified icon should not be.")]
        [AjaxEventUpdate(Script = "{0}.setIconClass({1});")]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return string.Format("icon-{0}", this.Icon.ToString().ToLower());
                }
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default CSS class to use for menu items (defaults to 'x-menu-item')")]
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

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(200)]
        [Description("Length of time in milliseconds to wait before showing this item (defaults to 200)")]
        [NotifyParentProperty(true)]
        public virtual int ShowDelay
        {
            get
            {
                object obj = this.ViewState["ShowDelay"];
                return (obj == null) ? 200 : (int)obj;
            }
            set
            {
                this.ViewState["ShowDelay"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The text to display in this item (defaults to '').")]
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

        [Category("Config Options")]
        [DefaultValue(Icon.None)]
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