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
    [ToolboxData("<{0}:MenuPanel runat=\"server\" Title=\"Menu\" Height=\"300\" Width=\"185\"><Menu><Items><ext:MenuItem runat=\"server\" Text=\"Item1\"><Menu><ext:Menu runat=\"server\"><Items><ext:MenuItem runat=\"server\" Text=\"SubItem1\" /><ext:MenuItem runat=\"server\" Text=\"SubItem2\" /></Items></ext:Menu></Menu></ext:MenuItem><ext:MenuItem runat=\"server\" Text=\"Item2\" /><ext:MenuItem runat=\"server\" Text=\"Item3\" /><ext:MenuItem runat=\"server\" Text=\"Item4\" /></Items></Menu></{0}:MenuPanel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.MenuPanel), "Build.Resources.ToolboxIcons.MenuPanel.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Xtype("coolitemenupanel")]
    [InstanceOf(ClassName = "Coolite.Ext.MenuPanel")]
    public class MenuPanel : PanelBase
    {
        private static readonly object EventSelectedItemChanged = new object();

        [Category("Action")]
        [Description("Fires when the selected Item has been changed")]
        public event EventHandler SelectedItemChanged
        {
            add
            {
                Events.AddHandler(EventSelectedItemChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventSelectedItemChanged, value);
            }
        }

        protected virtual void OnSelectedItemChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventSelectedItemChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void RaisePostDataChangedEvent()
        {
            if (this.baseLoadPostData)
            {
                base.RaisePostDataChangedEvent();    
            }
            
            if(this.thisLoadPostData)
            {
                this.OnSelectedItemChanged(EventArgs.Empty);
            }
        }

        private bool baseLoadPostData;
        private bool thisLoadPostData;
        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            this.baseLoadPostData = base.LoadPostData(postDataKey, postCollection);

            string index = postCollection[string.Concat(this.ClientID, "_SelIndex")] ?? "";

            if (!string.IsNullOrEmpty(index))
            {
                try
                {
                    this.ViewState.Suspend();
                    int tmpIndex;
                    if (int.TryParse(index, out tmpIndex))
                    {
                        if (tmpIndex != this.SelectedIndex)
                        {
                            this.SelectedIndex = tmpIndex;
                            this.thisLoadPostData = true;
                            return true;
                        }
                    }
                }
                finally
                {
                    this.ViewState.Resume();
                }
            }

            return this.baseLoadPostData;
        }
        
        private Menu menu;

        [ClientConfig("menu", typeof(LazyControlJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Standard menu attribute consisting of a reference to a menu object, a menu id or a menu config blob")]
        public virtual Menu Menu
        {
            get
            {
                if (this.menu == null)
                {
                    this.menu = new Menu();
                    this.Controls.Add(this.menu);
                }

                return this.menu;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Save selection after click")]
        [NotifyParentProperty(true)]
        public virtual bool SaveSelection
        {
            get
            {
                object obj = this.ViewState["SaveSelection"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["SaveSelection"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Fit menu's height")]
        [NotifyParentProperty(true)]
        public virtual bool FitHeight
        {
            get
            {
                object obj = this.ViewState["FitHeight"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["FitHeight"] = value;
            }
        }

        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetSelectedIndex")]
        [Category("Config Options")]
        [DefaultValue(-1)]
        [Description("Index of selected item")]
        [NotifyParentProperty(true)]
        public virtual int SelectedIndex
        {
            get
            {
                object obj = this.ViewState["SelectedIndex"];
                return (obj == null) ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["SelectedIndex"] = value;
            }
        }

        private PanelListeners listeners;

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
        public PanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private PanelAjaxEvents ajaxEvents;

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
        public PanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PanelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        public void SetSelectedIndex(int index)
        {
            string template = "{0}.setSelectedIndex({1});";
            this.AddScript(template, this.ClientID, index);
        }

    }
}