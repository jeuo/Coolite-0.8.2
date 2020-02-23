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
    /// <summary>
    /// Basic text field. Can be used as a direct replacement for traditional text inputs, or as the base class for more sophisticated input controls (like Ext.form.TextArea and Ext.form.ComboBox).
    /// </summary>
    [ToolboxData("<{0}:TextField runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("TextChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Text")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(TextFieldDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TextField), "Build.Resources.ToolboxIcons.TextField.bmp")]
    [Description("Basic text field. Can be used as a direct replacement for traditional text inputs, or as the base class for more sophisticated input controls (like Ext.form.TextArea and Ext.form.ComboBox).")]
    public class TextField : TextFieldBase, IEditableTextControl, ITextControl, IPostBackEventHandler 
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                this.On("change", new JFunction(this.PostBackFunction));
            }

            this.SetValue(this.Text);
        }

        /// <summary>
        /// The Text value to initialize this field with.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Localizable(true)]
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
                this.ViewState["Text"] = value;
            }
        }

        [Browsable(false)]
        [DefaultValue(null)]
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

        private TextFieldListeners listeners;

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
        public TextFieldListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TextFieldListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private TextFieldAjaxEvents ajaxEvents;

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
        public TextFieldAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TextFieldAjaxEvents();
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
        /// Fires when the Text property has been changed.
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Text property has been changed.")]
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
            if (val != null && this.Text != val)
            {
                try
                {
                    this.ViewState.Suspend();
                    this.Text = val.Equals(this.EmptyText) ? string.Empty : val;
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
    }
}