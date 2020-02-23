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
    [Xtype("coolitetrigger")]
    [InstanceOf(ClassName = "Coolite.Ext.TriggerField")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TriggerField), "Build.Resources.ToolboxIcons.TriggerField.bmp")]
    [ClientStyle(Type = typeof(TriggerField), DefaultOnlyStyle = true, FilePath = "/ux/extensions/triggerfield/css/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.triggerfield-embedded.css")]
    [ClientStyle(Type = typeof(TriggerField), Theme = Theme.Gray, FilePath = "/ux/extensions/triggerfield/css/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.triggerfield-embedded.css")]
    [ClientStyle(Type = typeof(TriggerField), Theme = Theme.Slate, FilePath = "/ux/extensions/triggerfield/css/slate/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.slate.triggerfield-embedded.css")]
    [DefaultProperty("Text")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Text")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    public class TriggerField : TriggerFieldBase, IEditableTextControl, IPostBackEventHandler 
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                string replace = string.Concat("'", this.PostBackArgument, "'");
                this.On("triggerclick", new JFunction(this.PostBackFunction.Replace(replace,"index"), "el","t","index"));
            }

            this.SetValue(this.Text);
        }

        protected override string PostBackArgument
        {
            get
            {
                return "_index_";
            }
        }

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

        private static readonly object EventTextChanged = new object();
        private static readonly object EventTriggerClicked = new object();

        public delegate void TriggerClickedHandler(object sender, TriggerEventArgs e);

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

        [Category("Action")]
        [Description("Fires when a trigger has been clicked")]
        public event TriggerClickedHandler TriggerClicked
        {
            add
            {
                Events.AddHandler(EventTriggerClicked, value);
            }
            remove
            {
                Events.RemoveHandler(EventTriggerClicked, value);
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

        protected virtual void OnTriggerClicked(TriggerEventArgs e)
        {
            TriggerClickedHandler handler = (TriggerClickedHandler)Events[EventTriggerClicked];
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

        protected override void RaisePostDataChangedEvent()
        {
            this.OnTextChanged(EventArgs.Empty);
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            int index;

            if(int.TryParse(eventArgument, out index))
            {
                this.OnTriggerClicked(new TriggerEventArgs(index));   
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Triggers.Count == 0)
            {
                this.Triggers.Add(new FieldTrigger());
            }

            base.OnPreRender(e);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string TriggerClass
        {
            get { return base.TriggerClass; }
            set { base.TriggerClass = value; }
        }

        private FieldTrigerCollection triggers;

        [ClientConfig("triggersConfig", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual FieldTrigerCollection Triggers
        {
            get
            {
                if (this.triggers == null)
                {
                    this.triggers = new FieldTrigerCollection();
                }
                return this.triggers;
            }
        }

        /// <summary>
        /// Shows a trigger
        /// </summary>
        /// <param name="index">The index of the trigger element.</param>
        [Description("Shows a trigger")]
        public virtual void ShowTrigger(int index)
        {
            this.Triggers[index].HideTrigger = false;
            string template = "{0}.triggers[{1}].show();";
            this.AddScript(template, this.ClientID, index);
        }

        /// <summary>
        /// Hides a trigger.
        /// </summary>
        /// <param name="index">The index of the trigger element.</param>
        [Description("Hides a trigger")]
        public new virtual void HideTrigger(int index)
        {
            this.Triggers[index].HideTrigger = true;
            string template = "{0}.triggers[{1}].hide();";
            this.AddScript(template, this.ClientID, index);
        }

        private TriggerFieldListeners listeners;

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
        public TriggerFieldListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TriggerFieldListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private TriggerFieldAjaxEvents ajaxEvents;

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
        public TriggerFieldAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TriggerFieldAjaxEvents();
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

    public class TriggerEventArgs : EventArgs
    {
        private readonly int index;

        public TriggerEventArgs(int index)
        {
            this.index = index;
        }

        public int Index
        {
            get
            {
                return index;
            }
        }
    }

    public class FieldTrigerCollection : StateManagedCollection<FieldTrigger> { }
}