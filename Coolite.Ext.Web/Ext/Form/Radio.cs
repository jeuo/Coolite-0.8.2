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
    /// Single radio field. Can be used as a direct replacement for traditional Radio controls.
    /// </summary>
    [Xtype("radio")]
    [InstanceOf(ClassName = "Ext.form.Radio")]
    [ToolboxData("<{0}:Radio runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("CheckedChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Checked")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(RadioDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Radio), "Build.Resources.ToolboxIcons.Radio.bmp")]
    [Description("Single radio field. Can be used as a direct replacement for traditional Radio controls.")]
    public class Radio : CheckboxBase, IPostBackEventHandler, ICheckBoxControl
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                this.On("check", new JFunction(this.PostBackFunction));
            }
        }

        protected override string UniqueName
        {
            get
            {
                return !string.IsNullOrEmpty(this.GroupName) ? this.GroupName : this.ClientID;
            }
        }

        /// <summary>
        /// The field's HTML name attribute.
        /// </summary>
        [ClientConfig("name")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The field's HTML name attribute.")]
        public virtual string GroupName
        {
            get
            {
                return (string)this.ViewState["GroupName"] ?? "";
            }
            set
            {
                this.ViewState["GroupName"] = value;
            }
        }

        [AjaxEventUpdate(MethodName = "SetValue")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get
            {
                return this.Checked;
            }
            set
            {
                this.Checked = Convert.ToBoolean(value);
            }
        }

        private RadioListeners listeners;

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
        public RadioListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new RadioListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private RadioAjaxEvents ajaxEvents;

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
        public RadioAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new RadioAjaxEvents();
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

        private static readonly object EventCheckedChanged = new object();

        /// <summary>
        /// Fires when the Checked property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Checked property has been changed")]
        public event EventHandler CheckedChanged
        {
            add
            {
                Events.AddHandler(EventCheckedChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventCheckedChanged, value);
            }
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventCheckedChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool isChecked = false;
            
            string val = postCollection[this.UniqueName];

            if (!string.IsNullOrEmpty(val) && val.Equals(this.InputValue))
            {

                try
                {
                    this.ViewState.Suspend();
                    if (!this.Checked)
                    {
                        this.Checked = true;
                        isChecked = true;
                    }
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return isChecked;
            }

            try
            {
                this.ViewState.Suspend();
                if (this.Checked)
                {
                    this.Checked = false;
                }
            }
            finally
            {
                this.ViewState.Resume();
            }

            return isChecked;
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnCheckedChanged(EventArgs.Empty);
        }
    }
}