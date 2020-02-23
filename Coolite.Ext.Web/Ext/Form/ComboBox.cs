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
using System.Drawing;
using System.Web.UI;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A combobox control with support for autocomplete, remote-loading, paging and many other features.
    /// </summary>
    [ToolboxData("<{0}:ComboBox runat=\"server\"></{0}:ComboBox>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [ValidationProperty("SelectedItem")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ComboBox), "Build.Resources.ToolboxIcons.ComboBox.bmp")]
    [InstanceOf(ClassName = "Coolite.Ext.TriggerComboBox")]
    [Description("A combobox control with support for autocomplete, remote-loading, paging and many other features.")]
    public class ComboBox : ComboBoxBase<ListItem>, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (!this.ClientID.Equals(this.UniqueName))
            {
                this.CustomConfig.Add(new ConfigItem("uniqueName", this.UniqueName, ParameterMode.Value));
            }

            if (this.TriggerAutoPostBack)
            {
                this.PostBackArgument = "_index_";
                string replace = string.Concat("'", this.PostBackArgument, "'");
                this.On("triggerclick", new JFunction(this.PostBackFunction.Replace(replace, "index"), "el", "t", "index"));
            }


            if (this.AutoPostBack)
            {
                EventHandler handler = (EventHandler)Events[EventItemSelected];
                if (handler != null)
                {
                    this.PostBackArgument = "select";

                    this.On("select", new JFunction(this.PostBackFunction));
                }
                else
                {
                    HandlerConfig config = new HandlerConfig();
                    config.Delay = 10;

                    this.PostBackArgument = "change";
                    this.On("blur", new JFunction(this.PostBackFunction), "this", config);
                }
            }

            if(!string.IsNullOrEmpty(this.StoreID))
            {
                Store store = ControlUtils.FindControl<Store>(this, this.StoreID, true);
                if(store == null)
                {
                    throw new InvalidOperationException(string.Format("The Control '{0}' could not find the StoreID of '{1}'.", this.ID, this.StoreID));
                }
            }
            else
            {
                this.TriggerAction = TriggerAction.All;
                this.Mode = DataLoadMode.Local;
            }

            if(!string.IsNullOrEmpty(this.SelectedItem.Value))
            {
                this.Value = this.SelectedItem.Value;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (!this.DesignMode)
            {
                this.Controls.Add(this.Template);
            }
        }

        private static readonly object EventValueChanged = new object();
        private static readonly object EventItemSelected = new object();

        /// <summary>
        /// Fires when the Item property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Item property has been changed")]
        public event EventHandler ValueChanged
        {
            add
            {
                this.Events.AddHandler(EventValueChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventValueChanged, value);
            }
        }

        [Category("Action")]
        [Description("Fires when the Item property has been selected")]
        public event EventHandler ItemSelected
        {
            add
            {
                this.Events.AddHandler(EventItemSelected, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventItemSelected, value);
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)this.Events[EventValueChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnItemSelected(EventArgs e)
        {
            EventHandler handler = (EventHandler)this.Events[EventItemSelected];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string value = postCollection[string.Concat(this.UniqueName, "_Value")];
            string text = postCollection[this.UniqueName];
            string index = postCollection[string.Concat(this.UniqueName, "_SelIndex")] ?? "";

            if(value == null && text == null)
            {
                return false;
            }

            if (!text.Equals(this.EmptyText) && (!this.SelectedItem.Value.Equals(value) || !this.SelectedItem.Text.Equals(text)))
            {
                try
                {
                    this.ViewState.Suspend();
                    this.SelectedItem.Text = text;
                    this.SelectedItem.Value = value;
                    int tmpIndex;
                    if(int.TryParse(index, out tmpIndex))
                    {
                        this.SelectedIndex = tmpIndex;
                    }
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return true;
            }
            else
            {
                if(text.Equals(this.EmptyText) && (!string.IsNullOrEmpty(this.SelectedItem.Value) || !string.IsNullOrEmpty(this.SelectedItem.Text)))
                {
                    try
                    {
                        this.ViewState.Suspend();
                        this.SelectedItem.Text = "";
                        this.SelectedItem.Value = "";
                        this.SelectedIndex = -1;
                    }
                    finally
                    {
                        this.ViewState.Resume();
                    }
                    return true;
                }
            }

            return false;
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            switch (eventArgument)
            {
                case "select":
                    this.OnItemSelected(EventArgs.Empty);
                    break;
                case "change":
                    this.OnValueChanged(EventArgs.Empty);
                    break;
                default:
                    int index;

                    if (int.TryParse(eventArgument, out index))
                    {
                        this.OnTriggerClicked(new TriggerEventArgs(index));
                    }
                    break;
            }
        }

        private ComboBoxListeners listeners;

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
        public ComboBoxListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ComboBoxListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private ComboBoxAjaxEvents ajaxEvents;

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
        public ComboBoxAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ComboBoxAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        [Description("Sets a data value into the field and validates it. To set the value directly without validation see setRawValue.")]
        public virtual void SetValueAndFireSelect(object value)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setValueAndFireSelect({1});{0}.clearInvalid();";

            this.AddScript(template, this.ClientID, JSON.Serialize(value, converters));
        }

        [Description("Set init value. If the store is not loaded yet then value will be setted after the store's loading")]
        public virtual void SetInitValue(object value)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setInitValue({1});";

            this.AddScript(template, this.ClientID, JSON.Serialize(value, converters));
        }
    }
}