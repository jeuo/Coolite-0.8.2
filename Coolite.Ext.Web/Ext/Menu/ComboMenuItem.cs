/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Coolite.Ext.ComboMenuItem")]
    public class ComboMenuItem : Adapter, ICustomConfigSerialization
    {
        public ComboMenuItem()
        {
            this.combobox = new ComboBox();
            this.combobox.LazyInit = false;
            this.Controls.Add(this.combobox);
            this.LazyItems.Add(this.combobox);
        }


        public Menu GetParentMenu(WebControl control)
        {
            return (Menu)ReflectionUtils.GetTypeOfParent(this, typeof(Menu));
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Menu ParentMenuNotLazy
        {
            get
            {
                Menu parent = this.GetParentMenu(this);
                while (parent != null && parent.IsLazy)
                {
                    parent = this.GetParentMenu(parent);
                }

                return parent;
            }
        }


        protected override void PageLoadComplete(object sender, EventArgs e)
        {
            base.PageLoadComplete(sender, e);

            if(!string.IsNullOrEmpty(this.combobox.Template.Text))
            {
                Menu parent = this.ParentMenuNotLazy;
                if(parent != null)
                {
                    parent.BeforeClientInit += delegate { this.combobox.Template.EnsureScriptRegistering(false); };
                }
                else
                {
                    this.combobox.Template.EnsureScriptRegistering(true);    
                }
            }
        }

        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if(!string.IsNullOrEmpty(this.combobox.Template.Text))
            {
                this.combobox.Template.EnsureScriptRegistering(true);
            }
        }

        private ComboBox combobox;

        [ClientConfig("combobox", typeof(LazyControlJsonConverter))]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The ComboBox object")]
        public ComboBox ComboBox
        {
            get
            {
                return this.combobox;
            }
        }

        private BaseMenuItemListeners listeners;

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
        public BaseMenuItemListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new BaseMenuItemListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private BaseMenuItemAjaxEvents ajaxEvents;

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
        public BaseMenuItemAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new BaseMenuItemAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        public string Serialize(Control owner)
        {
            return string.Concat("new Coolite.Ext.ComboMenuItem(", new ClientConfig(true).Serialize(this, true), ")");
        }
    }
}
