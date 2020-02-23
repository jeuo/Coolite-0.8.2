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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A basic tab contentContainer. Tab panels can be used exactly like a standard Ext.Panel for layout purposes, but also have special support for containing child Panels that get automatically converted into tabs.
    /// </summary>
    [DefaultEvent("TabChanged")]
    [ToolboxData("<{0}:TabPanel runat=\"server\" ActiveTabIndex=\"0\" Height=\"300\"><Tabs><{0}:Tab runat=\"server\" Title=\"Tab 1\"><Body></Body></{0}:Tab><{0}:Tab runat=\"server\" Title=\"Tab 2\"><Body></Body></{0}:Tab><{0}:Tab runat=\"server\" Title=\"Tab 3\"><Body></Body></{0}:Tab></Tabs></{0}:TabPanel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TabPanel), "Build.Resources.ToolboxIcons.TabPanel.bmp")]
    [Designer(typeof(TabPanelDesigner))]
    [Description("A basic tab contentContainer. Tab panels can be used exactly like a standard Ext.Panel for layout purposes, but also have special support for containing child Panels that get automatically converted into tabs.")]
    public class TabPanel : TabPanelBase, IAutoPostBack, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (this.Tabs.Count > 0)
            {
                string fn = string.Format("this.getActiveTabField().setValue(newTab.id + ':' + el.items.indexOf(newTab));{0}{1}", this.PostBackFunction, this.AutoPostBack ? "return false;" : "");
                this.On("beforetabchange", new JFunction(fn, "el", "newTab"));
            }

            if(!this.IsLazy)
            {
                this.AddAfterClientInitScript(string.Concat(this.ClientID, ".doLayout();"));    
            }
            else
            {
                this.DoLayout();    
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control state automatically posts back to the server when tab changed.
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets a value indicating whether the control state automatically posts back to the server when tab changed.")]
        public virtual bool AutoPostBack
        {
            get
            {
                object obj = this.ViewState["AutoPostBack"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [Description("Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.")]
        public virtual bool CausesValidation
        {
            get
            {
                object obj = this.ViewState["CausesValidation"];
                return (obj != null && (bool)obj);
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Controls ValidationGroup
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Gets or Sets the Controls ValidationGroup")]
        public virtual string ValidationGroup
        {
            get
            {
                return (string)this.ViewState["ValidationGroup"] ?? "";
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }

        private TabPanelListeners listeners;

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
        public TabPanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TabPanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private TabPanelAjaxEvents ajaxEvents;

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
        public TabPanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TabPanelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.ActiveTabIndex >= this.Tabs.Count && this.Page != null && !this.Page.IsPostBack && !Ext.IsAjaxRequest)
            {
                string msg = string.Format("The .ActiveTabIndex value was set to the index of a Tab which does not exist in the '{0}' Tabs Collection.{3}The .ActiveTabIndex can not be set to a value > {1}.{3}The actual .ActiveTabIndex Value attempted was {2}.{3}{3}Are you dynamically creating Tab Controls and attempting to set the .ActiveTabIndex property to the index of a Tab which was dynamically created? If yes, please ensure your Tab Controls are recreated on each request.{3}{3}COMMON CAUSE: Setting the .ActiveTabIndex to the index of a Tab which was created during a previous Button Click server-side Event.",
                        this.ID,
                        (this.Tabs.Count - 1).ToString(),
                        this.ActiveTabIndex.ToString(),
                        Environment.NewLine);
                throw new IndexOutOfRangeException(msg);
            }

            base.Render(writer);
        }


        /*  IPostBackDataHandler + IPostBackEventHandler
            -----------------------------------------------------------------------------------------------*/

        private static readonly object EventTabChanged = new object();

        /// <summary>
        /// Fires when the SelectedDate property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the SelectedDate property has been changed")]
        public event EventHandler TabChanged
        {
            add
            {
                this.Events.AddHandler(EventTabChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventTabChanged, value);
            }
        }

        protected virtual void OnTabChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventTabChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private bool baseLoadPostData;
        private bool thisLoadPostData;
        private bool eventWasRaised;
        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            baseLoadPostData = base.LoadPostData(postDataKey, postCollection);
            string val = postCollection[string.Concat(this.ClientID, "_ActiveTab")];
            if (!string.IsNullOrEmpty(val))
            {
                int activeTabNum;
                string[] at = val.Split(':');
                if (int.TryParse(at.Length >1 ? at[1] : at[0], out activeTabNum))
                {
                    int index = this.Tabs.FindIndex(delegate(Tab tab) { return tab.ClientID == at[0]; });
                    if (index >=0)
                    {
                        activeTabNum = index;
                    }
                    else
                    {
                        if (this.Visible)
                        {
                            for (int i = 0; i <= activeTabNum; i++ )
                            {
                                if(i < this.Tabs.Count && (!this.Tabs[i].Visible || this.Tabs[i].Hidden))
                                {
                                    activeTabNum++;
                                }
                            }
                        }
                    }

                    if (activeTabNum > -1 && this.ActiveTabIndex != activeTabNum)
                    {
                        this.ViewState.Suspend();
                        this.ActiveTabIndex = activeTabNum;
                        this.ViewState.Resume();
                        thisLoadPostData = true;
                        return true;
                    }
                }
            }

            return false || baseLoadPostData; 
        }

        protected override void RaisePostDataChangedEvent()
        {
            if (baseLoadPostData)
            {
                base.RaisePostDataChangedEvent();
            }

            if (thisLoadPostData)
            {
                if (!eventWasRaised)
                {
                    this.OnTabChanged(EventArgs.Empty);
                    eventWasRaised = false;
                }
            }
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            if (!eventWasRaised)
            {
                this.OnTabChanged(EventArgs.Empty);
                eventWasRaised = false;
            }
        }

        void TabPanel_TabChanged(object sender, EventArgs e)
        {
            this.OnTabChanged(e);
            CommandEventArgs args = new CommandEventArgs("TabChanged", ((Coolite.Ext.Web.TabPanel)sender).UniqueID);
            base.RaiseBubbleEvent(this, args);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.AutoPostBack && this.DeferredRender)
            {
                for (int i = 0; i < this.Tabs.Count; i++)
                {
                    if (!this.Tabs[i].HasLayout() || (this.Tabs[i].HasLayout() && this.ActiveTabIndex == i))
                    {
                        this.Tabs[i].BodyContainer.Visible = (this.ActiveTabIndex == i);
                    }
                }
            }

            base.OnPreRender(e);
        }
    }
}