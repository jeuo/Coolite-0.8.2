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
    /// A specialized SplitButton that contains a menu of Ext.menu.CheckItem elements. The button automatically cycles through each menu items on click, raising the button's change event (or calling the button's changeHandler function, if supplied) for the active menu items. Clicking on the arrow section of the button displays the dropdown menu just like a normal SplitButton.
    /// </summary>
    [Xtype("cycle")]
    [Description("A specialized SplitButton that contains a menu of Ext.menu.CheckItem elements. The button automatically cycles through each menu items on click, raising the button's change event (or calling the button's changeHandler function, if supplied) for the active menu items. Clicking on the arrow section of the button displays the dropdown menu just like a normal SplitButton.")]
    [ToolboxData("<{0}:CycleButton runat=\"server\" Text=\"Cycle Button\" />")]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.CycleButton), "Build.Resources.ToolboxIcons.CycleButton.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [InstanceOf(ClassName = "Ext.CycleButton")]
    public class CycleButton : SplitButtonBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(this.DesignMode)
            {
                return;
            }

            if (!Ext.IsAjaxRequest)
            {
                if(this.Menu.Primary != null)
                {
                    foreach (CheckMenuItem item in this.Menu.Primary.Items)
                    {
                        if(string.IsNullOrEmpty(item.Group))
                        {
                            item.Group = this.ClientID;
                        }
                    }
                }

                if(this.Menu.Primary != null)
                {
                    this.Menu.Primary.Items.AfterItemAdd += MenuItems_AfterItemAdd;
                }
            
                this.Menu.AfterItemAdd += Menu_AfterItemAdd;
            }    
        }

        void Menu_AfterItemAdd(MenuBase item)
        {
            item.Items.AfterItemAdd += MenuItems_AfterItemAdd;
        }

        void MenuItems_AfterItemAdd(BaseMenuItem item)
        {
            CheckMenuItem ci = (CheckMenuItem)item;
            if (string.IsNullOrEmpty(ci.Group))
            {
                ci.Group = this.ClientID;
            }
        }

        /// <summary>
        /// A callback function that will be invoked each time the active menu items in the button's menu has changed. If this callback is not supplied, the SplitButton will instead fire the change event on active items change. The changeHandler function will be called with the following argument list: (SplitButton this, Ext.menu.CheckItem items).
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A callback function that will be invoked each time the active menu items in the button's menu has changed. If this callback is not supplied, the SplitButton will instead fire the change event on active items change. The changeHandler function will be called with the following argument list: (SplitButton this, Ext.menu.CheckItem items).")]
        public virtual string ChangeHandler
        {
            get
            {
                object obj = this.ViewState["ChangeHandler"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ChangeHandler"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class which sets an image to be used as the static icon for this button. This icon will always be displayed regardless of which item is selected in the dropdown list. This overrides the default behavior of changing the button's icon to match the selected item's icon on change.")]
        public virtual string ForceIcon
        {
            get
            {
                object obj = this.ViewState["ForceIcon"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ForceIcon"] = value;
            }
        }

        /// <summary>
        /// A static string to prepend before the active items's text when displayed as the button's text (only applies when showText = true, defaults to '')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A static string to prepend before the active items's text when displayed as the button's text (only applies when showText = true, defaults to '')")]
        public virtual string PrependText
        {
            get
            {
                object obj = this.ViewState["PrependText"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["PrependText"] = value;
            }
        }

        /// <summary>
        /// True to display the active items's text as the button text (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display the active items's text as the button text (defaults to false).")]
        public virtual bool ShowText
        {
            get
            {
                object obj = this.ViewState["ShowText"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ShowText"] = value;
            }
        }

        /// <summary>
        /// False to prevent change active item after button click (defaults to true).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to prevent change active item after button click (defaults to true).")]
        public virtual bool ToggleOnClick
        {
            get
            {
                object obj = this.ViewState["ToggleOnClick"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ToggleOnClick"] = value;
            }
        }

        [ClientConfig("toggleSelected", JsonMode.Raw)]
        [DefaultValue("")]
        internal string ToggleOnClickProxy
        {
            get
            {
                if(!this.ToggleOnClick)
                {
                    return "Ext.emptyFn";
                }

                return "";
            }
        }

        private CycleButtonListeners listeners;

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
        public CycleButtonListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new CycleButtonListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private CycleButtonAjaxEvents ajaxEvents;

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
        public CycleButtonAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new CycleButtonAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        [DefaultValue(-1)]
        public int ActiveItemIndex
        {
            get
            {
                if(this.Menu.Primary == null)
                {
                    return -1;
                }

                for (int i = 0; i < this.Menu.Primary.Items.Count; i++)
                {
                    CheckMenuItem item = (CheckMenuItem)this.Menu.Primary.Items[i];
                    if(item.Checked)
                    {
                        return i;
                    }
                }

                return -1;
            }
            set
            {
                if (this.Menu.Primary == null)
                {
                    throw new Exception("The menu can't be null");
                }

                try
                {
                    this.ViewState.Suspend();
                    ((CheckMenuItem)this.Menu.Primary.Items[value]).Checked = true;
                    
                    if(Ext.IsAjaxRequest)
                    {
                        this.AddScript("{0}.setActiveItem({1});", this.ClientID, value);    
                    }
                }
                finally
                {
                    this.ViewState.Resume();    
                }
            }
        }

        public CheckMenuItem ActiveItem
        {
            get
            {
                if (this.Menu.Primary == null)
                {
                    return null;
                }

                for (int i = 0; i < this.Menu.Primary.Items.Count; i++)
                {
                    CheckMenuItem item = (CheckMenuItem)this.Menu.Primary.Items[i];
                    if (item.Checked)
                    {
                        return item;
                    }
                }

                return null;
            }
            set
            {
                if(this.Menu.Primary == null)
                {
                    throw new Exception("The CycleButton menu is null");
                }

                if(value == null)
                {
                    throw new Exception("The value can't be null");
                }


                for (int i = 0; i < this.Menu.Primary.Items.Count; i++)
                {
                    CheckMenuItem item = (CheckMenuItem)this.Menu.Primary.Items[i];
                    item.Checked = false;
                }

                try
                {
                    this.ViewState.Suspend();
                    value.Checked = true;

                    if (Ext.IsAjaxRequest)
                    {
                        this.AddScript("{0}.setActiveItem(Ext.getCmp('{1}'));", this.ClientID, value.ClientID);
                    }
                }
                finally
                {
                    this.ViewState.Resume();
                }
            }
        }

        /// <summary>
        /// This is normally called internally on button click, but can be called externally to advance the button's active item programmatically to the next one in the menu. If the current item is the last one in the menu the active item will be set to the first item in the menu.
        /// </summary>
        [Description("This is normally called internally on button click, but can be called externally to advance the button's active item programmatically to the next one in the menu. If the current item is the last one in the menu the active item will be set to the first item in the menu.")]
        public virtual void ToggleSelected()
        {
            this.AddScript("{0}.toggleSelected();", this.ClientID);
        }
    }
}