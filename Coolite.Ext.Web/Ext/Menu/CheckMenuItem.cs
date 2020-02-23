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
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [Description("Adds a menu item that contains a checkbox by default, but can also be part of a radio group.")]
    [InstanceOf(ClassName = "Ext.menu.CheckItem")]
    public class CheckMenuItem : MenuItemBase, IPostBackDataHandler, ICustomConfigSerialization
    {
        protected override void OnBeforeClientInitHandler()
        {
            base.OnBeforeClientInitHandler();

            if (!Ext.IsAjaxRequest)
            {
                string fn = "this.getCheckedField().setValue(checked ? \"true\" : \"false\");";
                this.On("checkchange", new JFunction(fn, "item", "checked"));
            }
        }

        private static readonly object EventCheckedChanged = new object();

        [Category("Action")]
        public event EventHandler CheckedChanged
        {
            add
            {
                this.Events.AddHandler(EventCheckedChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventCheckedChanged, value);
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

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[string.Concat(this.ClientID, "_Checked")];
            if (!string.IsNullOrEmpty(val))
            {
                bool checkedState;
                if (bool.TryParse(val, out checkedState))
                {
                    if (this.Checked != checkedState)
                    {
                        try
                        {
                            this.ViewState.Suspend();
                            this.Checked = checkedState;
                        }
                        finally
                        {
                            this.ViewState.Resume();
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.OnCheckedChanged(EventArgs.Empty);
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to initialize this checkbox as checked (defaults to false). Note that if this checkbox is part of a radio group (group = true) only the last item in the group that is initialized with checked = true will be rendered as checked.")]
        [AjaxEventUpdate(MethodName = "SetChecked")]
        public virtual bool Checked
        {
            get
            {
                object obj = this.ViewState["Checked"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Checked"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("All check items with the same group name will automatically be grouped into a single-select radio button group (defaults to '').")]
        public virtual string Group
        {
            get
            {
                return (string)this.ViewState["Group"] ?? "";
            }
            set
            {
                this.ViewState["Group"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default CSS class to use for radio group check items (defaults to \"x-menu-group-item\")")]
        public virtual string GroupClass
        {
            get
            {
                return (string)this.ViewState["GroupClass"] ?? "";
            }
            set
            {
                this.ViewState["GroupClass"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A function that handles the checkchange event.")]
        public virtual string CheckHandler
        {
            get
            {
                return (string)this.ViewState["CheckHandler"] ?? "";
            }
            set
            {
                this.ViewState["CheckHandler"] = value;
            }
        }

        private CheckMenuItemListeners listeners;

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
        public CheckMenuItemListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new CheckMenuItemListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private CheckMenuItemAjaxEvents ajaxEvents;

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
        public CheckMenuItemAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new CheckMenuItemAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        [Description("Set the checked state of this item.")]
        public virtual void SetChecked(bool value, bool suppressEvent)
        {
            string template = "{0}.setChecked({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value), JSON.Serialize(suppressEvent));
        }

        [Description("Set the checked state of this item.")]
        public virtual void SetChecked(bool value)
        {
            this.SetChecked(value, true);
        }

        public string Serialize(Control owner)
        {
            return string.Concat("new Ext.menu.CheckItem(", new ClientConfig(true).Serialize(this, true), ")");
        }
    }
}