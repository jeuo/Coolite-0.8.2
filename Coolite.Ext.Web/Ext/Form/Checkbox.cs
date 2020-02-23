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
    /// Single checkbox field. Can be used as a direct replacement for traditional Checkbox controls.
    /// </summary>
    [Xtype("checkbox")]
    [InstanceOf(ClassName = "Ext.form.Checkbox")]
    [ToolboxData("<{0}:Checkbox runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("CheckedChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Checked")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(CheckboxDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Checkbox), "Build.Resources.ToolboxIcons.Checkbox.bmp")]
    [Description("Single checkbox field. Can be used as a direct replacement for traditional Checkbox controls.")]
    public class Checkbox : CheckboxBase, IPostBackEventHandler, ICheckBoxControl
    {
        public Checkbox() { }

        public Checkbox(bool check) 
        {
            this.Checked = check;
        }

        public Checkbox(bool check, string boxLabel)
        {
            this.Checked = check;
            this.BoxLabel = boxLabel;
        }

        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                this.On("check", new JFunction(this.PostBackFunction));
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

        private CheckboxListeners listeners;

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
        public CheckboxListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new CheckboxListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private CheckboxAjaxEvents ajaxEvents;

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
        public CheckboxAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new CheckboxAjaxEvents();
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
            string val = postCollection[this.UniqueName];

            try
            {
                this.ViewState.Suspend();
                this.Checked = !string.IsNullOrEmpty(val);
            }
            finally
            {
                this.ViewState.Resume();
            }
            return true; 
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnCheckedChanged(EventArgs.Empty);
        }
    }
}