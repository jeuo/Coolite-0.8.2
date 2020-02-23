/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:ColorPalette runat=\"server\"></{0}:ColorPalette>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ColorPalette), "Build.Resources.ToolboxIcons.ColorPalette.bmp")]
    [InstanceOf(ClassName = "Ext.ColorPalette")]
    [Description("Simple color palette class for choosing colors.")]
    [Xtype("colorpalette")]
    [DefaultProperty("Value")]
    [DefaultEvent("SelectionChanged")]
    [ValidationProperty("Value")]
    [ControlValueProperty("Value")]
    [SupportsEventValidation]
    public class ColorPalette : Component, IAutoPostBack, IPostBackDataHandler, IPostBackEventHandler
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If set to true then reselecting a color that is already selected fires the select event")]
        public virtual bool AllowReselect
        {
            get
            {
                object obj = this.ViewState["AllowReselect"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AllowReselect"] = value;
            }
        }

        private readonly string[] predefinedColors = new string[]{
            "000000", "993300", "333300", "003300", "003366", "000080", "333399", "333333",
            "800000", "FF6600", "808000", "008000", "008080", "0000FF", "666699", "808080",
            "FF0000", "FF9900", "99CC00", "339966", "33CCCC", "3366FF", "800080", "969696",
            "FF00FF", "FFCC00", "FFFF00", "00FF00", "00FFFF", "00CCFF", "993366", "C0C0C0",
            "FF99CC", "FFCC99", "FFFF99", "CCFFCC", "CCFFFF", "99CCFF", "CC99FF", "FFFFFF"
        };

        
        public string[] PredefinedColors
        {
            get
            {
                return (string[])this.predefinedColors.Clone();
            }
        }

        private string[] colors;

        [ClientConfig(JsonMode.AlwaysArray)]
        [DefaultValue(null)]
        [Description("An array of 6-digit color hex code strings (without the # symbol).")]
        public virtual string[] Colors
        {
            get
            {
                return this.colors;
            }
            set
            {
                if(value != null)
                {
                    value = Array.ConvertAll(value, delegate(string item) { return item.ToUpperInvariant(); });
                }
                
                this.colors = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The CSS class to apply to the containing element (defaults to \"x-color-palette\")")]
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

        private XTemplate template;

        [Category("Config Options")]
        [Description("An existing XTemplate instance to be used in place of the default template for rendering the component.")]
        [ClientConfig("tpl", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual XTemplate Template
        {
            get
            {
                if (this.template == null)
                {
                    this.template = new XTemplate();
                    this.Controls.Add(this.template);
                }

                return this.template;
            }
        }


        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The initial color to highlight (should be a valid 6-digit color hex code without the # symbol). Note that the hex codes are case-sensitive.")]
        [AjaxEventUpdate(MethodName = "SilentSelect")]
        public virtual string Value
        {
            get
            {
                return (string)this.ViewState["Value"] ?? "";
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        [Description("Selects the specified color in the palette (fires the select event)")]
        public virtual void Select(string value)
        {
            string template = "{0}.select({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }

        [Description("Selects the specified color in the palette (doesn't fire the select event)")]
        public virtual void SilentSelect(string value)
        {
            string template = "{0}.silentSelect({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }


        protected override void OnBeforeClientInit(Observable sender)
        {
            string fn = "this.getColorField().setValue(color);";

            this.On("select", new JFunction(fn, "cp", "color"));

            if (this.AutoPostBack)
            {
                this.On("select", new JFunction(this.PostBackFunction));
            }
        }

        /// <summary>
        /// AutoPostBack
        /// </summary>
        /// <value><c>true</c> if [auto post back]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [Description("AutoPostBack")]
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
        [DefaultValue(false)]
        [Themeable(false)]
        [Description("Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.")]
        public virtual bool CausesValidation
        {
            get
            {
                object obj = this.ViewState["CausesValidation"];
                return (obj != null && (bool)obj);
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

        private static readonly object EventColorChanged = new object();

        /// <summary>
        /// Fires when the Item property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the color has been changed")]
        public event EventHandler ColorChanged
        {
            add
            {
                Events.AddHandler(EventColorChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventColorChanged, value);
            }
        }

        protected virtual void OnColorChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventColorChanged];
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
            string val = postCollection[string.Concat(this.ClientID, "_Color")];
            if (!string.IsNullOrEmpty(val))
            {
                try
                {
                    this.ViewState.Suspend();
                    this.Value = val;
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return true;
            }
            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.RaisePostDataChangedEvent();
        }

        protected virtual void RaisePostDataChangedEvent() { }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnColorChanged(EventArgs.Empty);
        }

        private ColorPaletteListeners listeners;

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
        public ColorPaletteListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ColorPaletteListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private ColorPaletteAjaxEvents ajaxEvents;

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
        public ColorPaletteAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ColorPaletteAjaxEvents();
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