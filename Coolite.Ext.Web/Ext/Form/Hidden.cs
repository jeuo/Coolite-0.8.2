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
using System.Collections.Generic;
using Newtonsoft.Json;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A basic hidden field for storing hidden values in forms that need to be passed in the form submit. Can be used as a direct replacement for the traditional <asp:Hidden> Web Control.
    /// </summary>
    [Xtype("hidden")]
    [InstanceOf(ClassName = "Ext.form.Hidden")]
    [ToolboxData("<{0}:Hidden runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("ValueChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Text")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [NonVisualControl]
    [Designer(typeof(HiddenFieldDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Hidden), "Build.Resources.ToolboxIcons.Hidden.bmp")]
    [Description("A basic hidden field for storing hidden values in forms that need to be passed in the form submit. Can be used as a direct replacement for the traditional <asp:Hidden> Web Control.")]
    public class Hidden : Field
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            this.SetValue(this.Value);
        }

        /// <summary>
        /// Set the Value of this Hidden
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [DefaultValue("")]
        [Description("Set the Value of this Hidden")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get
            {
                return (string)this.ViewState["Value"] ?? "";
            }
            set
            {
                this.ViewState["Value"] = value != null ? value.ToString() : string.Empty;
            }
        }

        public virtual string Text
        {
            get
            {
                return (string)this.Value ?? "";
            }
            set
            {
                this.Value = value;
            }
        }

        public virtual void Clear()
        {
            this.SetValue("");
        }

        private bool hideInDesign = false;

        /// <summary>
        /// Hide this Control at Design Time.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Hide this Control at Design Time.")]
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

        private FieldListeners listeners;

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
        public FieldListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new FieldListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private FieldAjaxEvents ajaxEvents;

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
        public FieldAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new FieldAjaxEvents();
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

        private static readonly object EventValueChanged = new object();

        /// <summary>
        /// Fires when the Text property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Text property has been changed")]
        public event EventHandler ValueChanged
        {
            add
            {
                Events.AddHandler(EventValueChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventValueChanged, value);
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventValueChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[this.UniqueName];
            if (val != null && !this.ReadOnly && !this.Value.Equals(val))
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
    }
}