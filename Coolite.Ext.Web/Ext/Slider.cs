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
    /// Slider which supports vertical or horizontal orientation, keyboard adjustments, configurable snapping, axis clicking and animation.
    /// </summary>
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Slider), "Build.Resources.ToolboxIcons.Slider.bmp")]
    [ToolboxData("<{0}:Slider runat=\"server\" />")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Slider which supports vertical or horizontal orientation, keyboard adjustments, configurable snapping, axis clicking and animation.")]
    public class Slider : SliderBase, IPostBackDataHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            this.On("change", new JFunction("this.getValueField().setValue(newValue);", "slider", "newValue"));
        }
        
        private SliderListeners listeners;

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
        public SliderListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new SliderListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private SliderAjaxEvents ajaxEvents;

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
        public SliderAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new SliderAjaxEvents();
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
        /// Fires when the Value property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Value property has been changed")]
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

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[string.Concat(this.ClientID, "_Value")];

            if (string.IsNullOrEmpty(val))
            {
                return false;
            }

            int newValue = int.Parse(val);
            if (!this.Value.Equals(newValue))
            {
                this.ViewState.Suspend();
                this.Value = newValue;
                this.ViewState.Resume();
                return true;
            }

            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            this.OnValueChanged(EventArgs.Empty);
        }

        //private System.Web.UI.HtmlControls.HtmlInputHidden input;

        //protected override void CreateChildControls()
        //{
        //    base.CreateChildControls();

        //    this.input = new System.Web.UI.HtmlControls.HtmlInputHidden();
        //    this.Controls.Add(this.input);
        //    this.input.EnableViewState = false;
        //    this.input.ID = string.Concat(this.ID, "_Value");
        //}
    }
}
