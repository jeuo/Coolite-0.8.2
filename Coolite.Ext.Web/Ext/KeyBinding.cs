/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class KeyBinding : StateManagedItem
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to handle key only when shift is pressed (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool Shift
        {
            get
            {
                object obj = this.ViewState["Shift"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Shift"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to handle key only when ctrl is pressed (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool Ctrl
        {
            get
            {
                object obj = this.ViewState["Ctrl"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Ctrl"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to handle key only when alt is pressed (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool Alt
        {
            get
            {
                object obj = this.ViewState["Alt"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Alt"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to stop the event.")]
        [NotifyParentProperty(true)]
        public virtual bool StopEvent
        {
            get
            {
                object obj = this.ViewState["StopEvent"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StopEvent"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The scope of the callback function")]
        [NotifyParentProperty(true)]
        public virtual string Scope
        {
            get
            {
                object obj = this.ViewState["Scope"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        private KeyListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public KeyListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new KeyListeners();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        [DefaultValue("")]
        [ClientConfig("fn", JsonMode.Raw)]
        internal string ListenerProxy
        {
            get
            {
                if (!this.Listeners.Event.IsDefault)
                {
                    this.Listeners.Event.ArgumentList.Clear();
                    this.Listeners.Event.ArgumentList.Add("key");
                    this.Listeners.Event.ArgumentList.Add("e");
                    return this.Listeners.Event.ToString();
                }

                return "";
            }
        }

        private KeyCollection keys;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public KeyCollection Keys
        {
            get
            {
                if (this.keys == null)
                {
                    this.keys = new KeyCollection();
                }

                return this.keys;
            }
        }

        [ClientConfig("key", JsonMode.Raw)]
        [DefaultValue("")]
        internal string KeysProxy
        {
            get
            {
                if(this.Keys.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    bool needComma = false;
                    sb.Append("[");
                    foreach (Key key in this.Keys)
                    {
                        if (key.Code == KeyCode.None)
                        {
                            continue;
                        }
                        if(needComma)
                        {
                            sb.Append(",");
                        }
                        needComma = true;
                        sb.Append((int)key.Code);
                    }
                    sb.Append("]");

                    return sb.ToString();
                }

                return "";
            }
        }

        public override void LoadViewState(object state)
        {
            object[] states = state as object[];
            if(states != null)
            {
                base.LoadViewState(states[0]);
                if (states.Length > 1 && states[1] != null)
                {
                    this.Keys.LoadViewState(states[1]);    
                }
            }
        }

        public override object SaveViewState()
        {
            object[] states = new object[this.Keys.Count > 0 ? 2 : 1];
            states[0] = base.SaveViewState();
            if (this.Keys.Count > 0)
            {
                states[1] = this.Keys.SaveViewState();
            }
            
            
            return states;
        }
    }

    public class KeyBindingCollection : StateManagedCollection<KeyBinding>
    {
        protected override bool CreateOnLoading
        {
            get
            {
                return true;
            }
        }
    }

    public class Key: StateManagedItem
    {
        [Category("Config Options")]
        [DefaultValue(KeyCode.None)]
        [Description("Key code")]
        [NotifyParentProperty(true)]
        public virtual KeyCode Code
        {
            get
            {
                object obj = this.ViewState["Code"];
                return (obj == null) ? KeyCode.None : (KeyCode)obj;
            }
            set
            {
                this.ViewState["Code"] = value;
            }
        }
    }

    public class KeyCollection : StateManagedCollection<Key>
    {
        protected override bool CreateOnLoading
        {
            get
            {
                return true;
            }
        }
    }

    public class KeyListeners : StateManagedItem
    {
        private SimpleListener _event;

        [ClientConfig("fn", JsonMode.Raw)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description(" The function to call when KeyMap finds the expected key combination.")]
        public virtual SimpleListener Event
        {
            get
            {
                if (this._event == null)
                {
                    this._event = new SimpleListener();
                }
                return this._event;
            }
        }

        public override object SaveViewState()
        {
            if(this.Event.IsDefault)
            {
                return null;
            }
            return this.Event.SaveViewState();
        }

        public override void LoadViewState(object state)
        {
            this.Event.LoadViewState(state);
        }
    }
}