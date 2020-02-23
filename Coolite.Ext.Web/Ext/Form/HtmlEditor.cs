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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Provides a lightweight HTML Editor component. NOTE: HtmlEditor can not be hidden on initial page load. If placing within a TabPanel, please ensure the correct .ActiveTabIndex is set. If placing within a Window, please ensure ShowOnLoad is 'true'.
    /// </summary>
    [Xtype("htmleditor")]
    [InstanceOf(ClassName = "Ext.form.HtmlEditor")]
    [ToolboxData("<{0}:HtmlEditor runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("TextChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Text")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(HtmlEditorDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.HtmlEditor), "Build.Resources.ToolboxIcons.HtmlEditor.bmp")]
    [Description("Provides a lightweight HTML Editor component. NOTE: HtmlEditor can not be hidden on initial page load. If placing within a TabPanel, please ensure the correct .ActiveTabIndex is set. If placing within a Window, please ensure ShowOnLoad is 'true'.")]
    public class HtmlEditor : Field, IEditableTextControl, ITextControl, IPostBackEventHandler
    {
        /// <summary>
        /// The Text value to initialize this field with.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("The Text value to initialize this field with.")]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value??"";
            }
        }

        [AjaxEventUpdate(MethodName = "SetValue")]
        [ClientConfig]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = (string)value;
            }
        }

        public virtual void Clear()
        {
            this.Text = "";
        }

        private EditorListeners listeners;

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
        public EditorListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new EditorListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private EditorAjaxEvents ajaxEvents;

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
        public EditorAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new EditorAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        private static readonly object EventTextChanged = new object();

        /// <summary>
        /// Fires when the Text property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Text property has been changed")]
        public event EventHandler TextChanged
        {
            add
            {
                Events.AddHandler(EventTextChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventTextChanged, value);
            }
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventTextChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[this.UniqueName];
            if (val != null && !this.ReadOnly )
            {
                if(this.EscapeValue)
                {
                    //val = this.Page.Server.UrlDecode(val);    
                    val = Coolite.Utilities.EscapeUtils.Unescape(val);
                }

                if(this.Text.Equals(val))
                {
                    return false; 
                }
                
                try
                {
                    this.ViewState.Suspend();
                    this.Text = val;
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return true;
            }
            return false;
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnTextChanged(EventArgs.Empty);
        }


        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// The default text for the create link prompt.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The default text for the create link prompt.")]
        public virtual string CreateLinkText
        {
            get
            {
                return (string)this.ViewState["CreateLinkText"] ?? "";
            }
            set
            {
                this.ViewState["CreateLinkText"] = value;
            }
        }

        /// <summary>
        /// The default value for the create link prompt (defaults to http://).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("http://")]
        [Description("The default value for the create link prompt (defaults to http://).")]
        public virtual string DefaultLinkValue
        {
            get
            {
                return (string)this.ViewState["DefaultLinkValue"] ?? "http://";
            }
            set
            {
                this.ViewState["DefaultLinkValue"] = value;
            }
        }

        /// <summary>
        /// Enable the left, center, right alignment buttons (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the left, center, right alignment buttons (defaults to true).")]
        public virtual bool EnableAlignments
        {
            get
            {
                object obj = this.ViewState["EnableAlignments"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableAlignments"] = value;
            }
        }

        /// <summary>
        /// Enable the fore/highlight color buttons (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the fore/highlight color buttons (defaults to true).")]
        public virtual bool EnableColors
        {
            get
            {
                object obj = this.ViewState["EnableColors"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableColors"] = value;
            }
        }

        /// <summary>
        /// Enable font selection. Not available in Safari. (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable font selection. Not available in Safari. (defaults to true).")]
        public virtual bool EnableFont
        {
            get
            {
                object obj = this.ViewState["EnableFont"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableFont"] = value;
            }
        }

        /// <summary>
        /// Enable the increase/decrease font size buttons (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the increase/decrease font size buttons (defaults to true).")]
        public virtual bool EnableFontSize
        {
            get
            {
                object obj = this.ViewState["EnableFontSize"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableFontSize"] = value;
            }
        }

        /// <summary>
        /// Enable the bold, italic and underline buttons (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the bold, italic and underline buttons (defaults to true).")]
        public virtual bool EnableFormat
        {
            get
            {
                object obj = this.ViewState["EnableFormat"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableFormat"] = value;
            }
        }

        /// <summary>
        /// Enable the create link button. Not available in Safari. (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the create link button. Not available in Safari. (defaults to true).")]
        public virtual bool EnableLinks
        {
            get
            {
                object obj = this.ViewState["EnableLinks"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableLinks"] = value;
            }
        }

        /// <summary>
        /// Enable the bullet and numbered list buttons. Not available in Safari. (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the bullet and numbered list buttons. Not available in Safari. (defaults to true).")]
        public virtual bool EnableLists
        {
            get
            {
                object obj = this.ViewState["EnableLists"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableLists"] = value;
            }
        }

        /// <summary>
        /// Enable the switch to source edit button. Not available in Safari. (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Enable the switch to source edit button. Not available in Safari. (defaults to true).")]
        public virtual bool EnableSourceEdit
        {
            get
            {
                object obj = this.ViewState["EnableSourceEdit"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableSourceEdit"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        public virtual bool EscapeValue
        {
            get
            {
                object obj = this.ViewState["EscapeValue"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EscapeValue"] = value;
            }
        }

        /// <summary>
        /// An array of available font families.
        /// </summary>
        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An array of available font families.")]
        public virtual string[] FontFamilies
        {
            get
            {
                object obj = this.ViewState["FontFamilies"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["FontFamilies"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Protected method that will not generally be called directly. If you need/want custom HTML cleanup, this is the method you should override.
        /// </summary>
        [Description("Protected method that will not generally be called directly. If you need/want custom HTML cleanup, this is the method you should override.")]
        public virtual void CleanHtml(string html)
        {
            string template = "{0}.cleanHtml({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(html));
        }

        /// <summary>
        /// Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.
        /// </summary>
        [Description("Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.")]
        public virtual void ExecCmd(string cmd, string value)
        {
            string template = "{0}.execCmd({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(cmd), JSON.Serialize(value));
        }

        /// <summary>
        /// Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.
        /// </summary>
        [Description("Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.")]
        public virtual void ExecCmd(string cmd, bool value)
        {
            string template = "{0}.execCmd({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(cmd), JSON.Serialize(value));
        }

        /// <summary>
        /// Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.
        /// </summary>
        [Description("Executes a Midas editor command directly on the editor document. For visual commands, you should use relayCmd instead. This should only be called after the editor is initialized.")]
        public virtual void InsertAtCursor(string text)
        {
            string template = "{0}.insertAtCursor({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(text));
        }

        /// <summary>
        /// Protected method that will not generally be called directly. Pushes the value of the textarea into the iframe editor.
        /// </summary>
        [Description("Protected method that will not generally be called directly. Pushes the value of the textarea into the iframe editor.")]
        public virtual void PushValue()
        {
            string template = "{0}.pushValue();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Executes a Midas editor command on the editor document and performs necessary focus and toolbar updates. This should only be called after the editor is initialized.
        /// </summary>
        [Description("Executes a Midas editor command on the editor document and performs necessary focus and toolbar updates. This should only be called after the editor is initialized.")]
        public virtual void RelayCmd(string cmd, string value)
        {
            string template = "{0}.relayCmd({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(cmd), JSON.Serialize(value));
        }

        /// <summary>
        /// Executes a Midas editor command on the editor document and performs necessary focus and toolbar updates. This should only be called after the editor is initialized.
        /// </summary>
        [Description("Executes a Midas editor command on the editor document and performs necessary focus and toolbar updates. This should only be called after the editor is initialized.")]
        public virtual void RelayCmd(string cmd, bool value)
        {
            string template = "{0}.relayCmd({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(cmd), JSON.Serialize(value));
        }

        /// <summary>
        /// Protected method that will not generally be called directly. Syncs the contents of the editor iframe with the textarea.
        /// </summary>
        [Description("Protected method that will not generally be called directly. Syncs the contents of the editor iframe with the textarea.")]
        public virtual void SyncValue()
        {
            string template = "{0}.syncValue();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Toggles the editor between standard and source edit mode.
        /// </summary>
        [Description("Toggles the editor between standard and source edit mode.")]
        public virtual void ToggleSourceEdit()
        {
            string template = "{0}.toggleSourceEdit();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Toggles the editor between standard and source edit mode.
        /// </summary>
        [Description("Toggles the editor between standard and source edit mode.")]
        public virtual void ToggleSourceEdit(bool sourceEdit)
        {
            string template = "{0}.toggleSourceEdit({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(sourceEdit));
        }

        /// <summary>
        /// Protected method that will not generally be called directly. It triggers a toolbar update by reading the markup state of the current selection in the editor.
        /// </summary>
        [Description("Protected method that will not generally be called directly. It triggers a toolbar update by reading the markup state of the current selection in the editor.")]
        public virtual void UpdateToolbar()
        {
            string template = "{0}.updateToolbar();";
            this.AddScript(template, this.ClientID);
        }
    }
}