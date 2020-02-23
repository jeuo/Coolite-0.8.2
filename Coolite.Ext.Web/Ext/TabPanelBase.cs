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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [Xtype("tabpanel")]
    [InstanceOf(ClassName = "Ext.TabPanel")]
    public abstract class TabPanelBase : PanelBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(!this.DesignMode)
            {
                this.Page.PreRender += new EventHandler(Page_PreRender);
            }
        }

        private void Page_PreRender(object sender, EventArgs e)
        {
            foreach (Tab tab in this.Tabs)
            {
                if(tab.Hidden)
                {
                    if (this.LazyItems.Contains(tab))
                    {
                        this.LazyItems.Remove(tab);
                    }
                }
            }
        }

        public override bool HasLayout()
        {
            return true;
        }

        /// <summary>
        /// The numeric index of the tab that should be initially activated on render.
        /// </summary>
        [Browsable(false)]
        [Description("The numeric index of the tab that should be initially activated on render.")]
        public virtual Tab ActiveTab
        {
            get
            {
                if (this.ActiveTabIndex > this.Tabs.Count - 1)
                {
                    return this.Tabs[this.Tabs.Count - 1];
                }
                return this.Tabs[this.ActiveTabIndex];
            }
            set
            {
                this.ActiveTabIndex = this.Tabs.IndexOf(value);
            }
        }

        /// <summary>
        /// The numeric index of the tab that should be initially activated on render.
        /// </summary>
        [AjaxEventUpdate(MethodName="SetActiveTab")]
        [Category("Config Options")]
        [DefaultValue(-1)]
        [Description("The numeric index of the tab that should be initially activated on render.")]
        [NotifyParentProperty(true)]
        public virtual int ActiveTabIndex
        {
            get
            {
                object obj = this.ViewState["ActiveTabIndex"];
                return (obj == null) ? (this.Tabs.Count == 0) ? -1 : 0 : (int)obj;
            }
            set
            {
                this.ViewState["ActiveTabIndex"] = value;
                this.CheckTabVisible();
            }
        }
        
        [ClientConfig("activeTab")]
        [DefaultValue(-1)]
        internal int VisibleIndex
        {
            get
            {
                int i = this.ActiveTabIndex;
                int correction = 0;
                for (int ind = 0; ind < Math.Min(i, this.Tabs.Count); ind++)
                {
                    if (!this.Tabs[ind].Visible || this.Tabs[ind].Hidden)
                    {
                        correction++;
                    }
                }

                return i - correction;
            }
        }

        protected internal bool HasVisibleTab
        {
            get
            {
                foreach (Tab tab in this.Tabs)
                {
                    if (tab.Visible == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        protected internal void CheckTabVisible()
        {
            TabPanel tp = (TabPanel)this;
            if (tp.AutoPostBack && tp.DeferredRender)
            {
                for (int i = 0; i < tp.Tabs.Count; i++)
                {
                    if (!tp.Tabs[i].HasLayout() || (tp.Tabs[i].HasLayout() && tp.ActiveTabIndex == i))
                    {
                        tp.Tabs[i].BodyContainer.Visible = (tp.ActiveTabIndex == i);
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.CheckTabVisible();
        }

        /// <summary>
        /// True to animate tab scrolling so that hidden tabs slide smoothly into view (defaults to true). Only applies when EnableTabScroll = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to animate tab scrolling so that hidden tabs slide smoothly into view (defaults to true). Only applies when EnableTabScroll = true.")]
        public virtual bool AnimScroll 
        {
            get
            {
                object obj = this.ViewState["AnimScroll"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AnimScroll"] = value;
            }
        }

        /// <summary>
        /// The base CSS class applied to the panel (defaults to 'x-tab-panel').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-tab-panel")]
        [NotifyParentProperty(true)]
        [Description("The base CSS class applied to the panel (defaults to 'x-tab-panel').")]
        public override string BaseCls
        {
            get
            {
                return (string)this.ViewState["BaseCls"] ?? "x-tab-panel";
            }
            set
            {
                this.ViewState["BaseCls"] = value;
            }
        }

        /// <summary>
        /// Determining whether or not each tab is rendered only when first accessed (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Determining whether or not each tab is rendered only when first accessed (defaults to true).")]
        public virtual bool DeferredRender
        {
            get
            {
                object obj = this.ViewState["DeferredRender"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["DeferredRender"] = value;
            }
        }

        /// <summary>
        /// True to enable scrolling to tabs that may be invisible due to overflowing the overall TabPanel width. Only available with tabs on addToStart. (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to enable scrolling to tabs that may be invisible due to overflowing the overall TabPanel width. Only available with tabs on addToStart. (defaults to false).")]
        public virtual bool EnableTabScroll 
        {
            get
            {
                object obj = this.ViewState["EnableTabScroll"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableTabScroll"] = value;
                if(value)
                {
                    this.TabPosition = TabPosition.Top;
                }
            }
        }

        /// <summary>
        /// Set to true to do a layout of tab items as tabs are changed.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Set to true to do a layout of tab items as tabs are changed.")]
        public virtual bool LayoutOnTabChange 
        {
            get
            {
                object obj = this.ViewState["LayoutOnTabChange"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["LayoutOnTabChange"] = value;
            }
        }

        /// <summary>
        /// The minimum width in pixels for each tab when ResizeTabs = true (defaults to 30).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "30")]
        [NotifyParentProperty(true)]
        [Description("The minimum width in pixels for each tab when ResizeTabs = true (defaults to 30).")]
        public virtual Unit MinTabWidth 
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinTabWidth"], Unit.Pixel(30), "MinTabWidth");
            }
            set
            {
                this.ViewState["MinTabWidth"] = value;
            }
        }

        /// <summary>
        /// True to automatically monitor window resize events and rerender the layout on browser resize (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to automatically monitor window resize events and rerender the layout on browser resize (defaults to true).")]
        public override bool MonitorResize 
        {
            get
            {
                object obj = this.ViewState["MonitorResize"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MonitorResize"] = value;
            }
        }

        /// <summary>
        /// True to render the tab strip without a background contentContainer image (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to render the tab strip without a background contentContainer image (defaults to false).")]
        public bool Plain
        {
            get
            {
                object obj = this.ViewState["Plain"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Plain"] = value;
            }
        }

        /// <summary>
        /// True to automatically resize each tab so that the tabs will completely fill the tab strip (defaults to false). Setting this to true may cause specific widths that might be set per tab to be overridden in order to fit them all into view (although MinTabWidth will always be honored).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to automatically resize each tab so that the tabs will completely fill the tab strip (defaults to false). Setting this to true may cause specific widths that might be set per tab to be overridden in order to fit them all into view (although MinTabWidth will always be honored).")]
        public bool ResizeTabs
        {
            get
            {
                object obj = this.ViewState["ResizeTabs"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ResizeTabs"] = value;
            }
        }

        /// <summary>
        /// The number of milliseconds that each scroll animation should last (defaults to .35). Only applies when AnimScroll = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0.35f)]
        [NotifyParentProperty(true)]
        [Description("The number of milliseconds that each scroll animation should last (defaults to .35). Only applies when AnimScroll = true.")]
        public virtual float ScrollDuration 
        {
            get
            {
                object obj = this.ViewState["ScrollDuration"];
                return (obj == null) ? 0.35f : (float)obj;
            }
            set
            {
                this.ViewState["ScrollDuration"] = value;
            }
        }

        /// <summary>
        /// The number of pixels to scroll each time a tab scroll button is pressed (defaults to 100, or if ResizeTabs = true, the calculated tab width). Only applies when EnableTabScroll = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(100)]
        [NotifyParentProperty(true)]
        [Description("The number of pixels to scroll each time a tab scroll button is pressed (defaults to 100, or if ResizeTabs = true, the calculated tab width). Only applies when EnableTabScroll = true.")]
        public virtual int ScrollIncrement 
        {
            get
            {
                object obj = this.ViewState["ScrollIncrement"];
                return (obj == null) ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["ScrollIncrement"] = value;
            }
        }

        /// <summary>
        /// Number of milliseconds between each scroll while a tab scroll button is continuously pressed (defaults to 400).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(400)]
        [NotifyParentProperty(true)]
        [Description("Number of milliseconds between each scroll while a tab scroll button is continuously pressed (defaults to 400).")]
        public virtual int ScrollRepeatInterval 
        {
            get
            {
                object obj = this.ViewState["ScrollRepeatInterval"];
                return (obj == null) ? 400 : (int)obj;
            }
            set
            {
                this.ViewState["ScrollRepeatInterval"] = value;
            }
        }

        private ItemsCollection<Tab> tabs;

        /// <summary>
        /// Tabs Collection
        /// </summary>
        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Tabs Collection")]
        public virtual ItemsCollection<Tab> Tabs
        {
            get
            {
                if (this.tabs == null)
                {
                    this.tabs = new ItemsCollection<Tab>();
                    this.tabs.AfterItemAdd += this.AfterItemAdd;
                    this.tabs.AfterItemRemove += this.AfterItemRemove;
                }

                return this.tabs;
            }
        }

        /// <summary>
        /// The number of pixels of space to calculate into the sizing and scrolling of tabs. If you change the margin in CSS, you will need to update this value so calculations are correct with either resizeTabs or scrolling tabs. (defaults to 2)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "2")]
        [NotifyParentProperty(true)]
        [Description("The number of pixels of space to calculate into the sizing and scrolling of tabs. If you change the margin in CSS, you will need to update this value so calculations are correct with either resizeTabs or scrolling tabs. (defaults to 2)")]
        public virtual Unit TabMargin 
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["TabMargin"], Unit.Pixel(2), "TabMargin");
            }
            set
            {
                this.ViewState["TabMargin"] = value;
            }
        }

        /// <summary>
        /// The alignment of the Tabs (defaults to 'Left'). Other option includes 'Right'. Note that tab scrolling is only supported for TabAlign='Left'. Note that when 'Right', the Tabs will be rendered in reverse order.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(TabAlign.Left)]
        [NotifyParentProperty(true)]
        [Description("The alignment of the Tabs (defaults to 'Left'). Other option includes 'Right'. Note that tab scrolling is only supported for TabAlign='Left'. Note that when 'Right', the Tabs will be rendered in reverse order.")]
        public virtual TabAlign TabAlign
        {
            get
            {
                object obj = this.ViewState["TabAlign"];
                return (obj == null) ? TabAlign.Left : (TabAlign)obj;
            }
            set
            {
                this.ViewState["TabAlign"] = value;
                if (value == TabAlign.Right)
                {
                    this.EnableTabScroll = false;
                }
            }
        }

        [ClientConfig("tabAlign", JsonMode.ToLower)]
        [DefaultValue(TabAlign.Left)]
        protected virtual TabAlign TabAlignProxy
        {
            get
            {
                if (this.TabAlign == TabAlign.Right)
                {
                    string styles = string.Concat("#", this.ClientID, " ul.x-tab-strip {width:100%;}#", this.ClientID, " ul.x-tab-strip li {float:right;margin:0 2px 0 0;}");
                    this.ScriptManager.RegisterClientStyleBlock(string.Concat(this.ClientID, "_TabAlign"), styles);
                }
                return this.TabAlign;
            }
        }

        /// <summary>
        /// The position where the tab strip should be rendered (defaults to 'addToStart'). The only other supported value is 'Bottom'. Note that tab scrolling is only supported for position 'addToStart'.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(TabPosition.Top)]
        [NotifyParentProperty(true)]
        [Description("The position where the tab strip should be rendered (defaults to 'addToStart'). The only other supported value is 'Bottom'. Note that tab scrolling is only supported for position 'addToStart'.")]
        public virtual TabPosition TabPosition 
        {
            get
            {
                object obj = this.ViewState["TabPosition"];
                return (obj == null) ? TabPosition.Top : (TabPosition)obj;
            }
            set
            {
                this.ViewState["TabPosition"] = value;
                if(value == TabPosition.Bottom)
                {
                    this.EnableTabScroll = false;
                }
            }
        }

        /// <summary>
        /// The initial width in pixels of each new tab (defaults to 120).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "120")]
        [NotifyParentProperty(true)]
        [Description("The initial width in pixels of each new tab (defaults to 120).")]
        public virtual Unit TabWidth 
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["TabWidth"], Unit.Pixel(120), "TabWidth");
            }
            set
            {
                this.ViewState["TabWidth"] = value;
            }
        }

        /// <summary>
        /// For scrolling tabs, the number of pixels to increment on mouse wheel scrolling (defaults to 20).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(20)]
        [NotifyParentProperty(true)]
        [Description("For scrolling tabs, the number of pixels to increment on mouse wheel scrolling (defaults to 20).")]
        public virtual int WheelIncrement
        {
            get
            {
                object obj = this.ViewState["WheelIncrement"];
                return (obj == null) ? 20 : (int)obj;
            }
            set
            {
                this.ViewState["WheelIncrement"] = value;
            }
        }

        /// <summary>
        /// The title text to display in the panel header (defaults to ''). When a title is specified the header element will automatically be created and displayed unless header is explicitly set to false. If you don't want to specify a title at config time, but you may want one later, you must either specify a non-empty title (a blank space ' ' will do) or header:true so that the contentContainer element will get created.
        /// </summary>
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The title text to display in the panel header (defaults to ''). When a title is specified the header element will automatically be created and displayed unless header is explicitly set to false. If you don't want to specify a title at config time, but you may want one later, you must either specify a non-empty title (a blank space ' ' will do) or header:true so that the contentContainer element will get created.")]
        public override string Title
        {
            get
            {
                return (string)this.ViewState["Title"] ?? this.ID;
            }
            set
            {
                this.ViewState["Title"] = value;
            }
        }

        
        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.
        /// </summary>
        [Description("Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.")]
        public virtual void Activate(Tab tab)
        {
            string template = "{0}.activate(\"{1}\");";
            this.AddScript(template, this.ClientID, tab.ClientID);
        }

        /// <summary>
        /// Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.
        /// </summary>
        [Description("Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.")]
        public virtual void Activate(string tab)
        {
            string template = "{0}.activate(\"{1}\");";
            this.AddScript(template, this.ClientID, tab);
        }

        /// <summary>
        /// Suspends any internal calculations or scrolling while doing a bulk operation. See endUpdate
        /// </summary>
        [Description("Suspends any internal calculations or scrolling while doing a bulk operation. See endUpdate")]
        public virtual void BeginUpdate()
        {
            string template = "{0}.beginUpdate();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Suspends any internal calculations or scrolling while doing a bulk operation. See endUpdate
        /// </summary>
        [Description("Suspends any internal calculations or scrolling while doing a bulk operation. See endUpdate")]
        public virtual void EndUpdate()
        {
            string template = "{0}.endUpdate();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Hides the tab strip item for the passed tab
        /// </summary>
        [Description("Hides the tab strip item for the passed tab")]
        public virtual void HideTabStripItem(int item)
        {
            string template = "{0}.hideTabStripItem({1});";
            this.AddScript(template, this.ClientID, item);
        }

        /// <summary>
        /// Hides the tab strip item for the passed tab
        /// </summary>
        [Description("Hides the tab strip item for the passed tab")]
        public virtual void HideTabStripItem(PanelBase item)
        {
            this.HideTabStripItem(item.ClientID);
        }

        /// <summary>
        /// Hides the tab strip item for the passed tab
        /// </summary>
        [Description("Hides the tab strip item for the passed tab")]
        public virtual void HideTabStripItem(string item)
        {
            string template = "{0}.hideTabStripItem(\"{1}\");";
            this.AddScript(template, this.ClientID, item);
        }

        /// <summary>
        /// True to scan the markup in this tab panel for autoTabs using the autoTabSelector
        /// </summary>
        [Description("True to scan the markup in this tab panel for autoTabs using the autoTabSelector")]
        public virtual void ReadTabs(bool removeExisting)
        {
            string template = "{0}.hideTabStripItem({1});";
            this.AddScript(template, this.ClientID, removeExisting.ToString().ToLower());
        }

        /// <summary>
        /// Scrolls to a particular tab if tab scrolling is enabled
        /// </summary>
        [Description("Scrolls to a particular tab if tab scrolling is enabled")]
        public virtual void ScrollToTab(Tab tab, bool animate)
        {
            string template = "{0}.scrollToTab({1},{2});";
            this.AddScript(template, this.ClientID, tab.ClientID, animate.ToString().ToLower());
        }

        /// <summary>
        /// Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.
        /// </summary>
        [Description("Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.")]
        public virtual void SetActiveTab(int index)
        {
            int correction = 0;
            for (int ind = 0; ind < Math.Min(index, this.Tabs.Count); ind++)
            {
                if (!this.Tabs[ind].Visible)
                {
                    correction++;
                }
            }

            string template = "{0}.setActiveTab({1});";
            this.AddScript(template, this.ClientID, (index - correction).ToString());
        }

        /// <summary>
        /// Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.
        /// </summary>
        [Description("Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.")]
        public virtual void SetActiveTab(Tab tab)
        {
            this.SetActiveTab(tab.ID);
        }

        /// <summary>
        /// Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.
        /// </summary>
        /// <param name="id">The id.</param>
        [Description("Sets the specified tab as the active tab. This method fires the beforetabchange event which can return false to cancel the tab change.")]
        public virtual void SetActiveTab(string id)
        {
            bool found = false;
            for (int i = 0; i < this.tabs.Count; i++)
            {
                if (this.tabs[i].ID == id)
                {
                    this.ActiveTab = this.tabs[i];
                    found = true;
                    break;
                }
            }

            if(!found)
            {
                throw new InvalidOperationException(string.Format("The '{0}' Tab does not exist with the '{1}' TabPanel.", id, this.ID));
            }

            string template = "{0}.setActiveTab(\"{1}\");";
            this.AddScript(template, this.ClientID, this.ActiveTab.ClientID);
        }

        /// <summary>
        /// Unhides the tab strip item for the passed tab
        /// </summary>
        [Description("Unhides the tab strip item for the passed tab")]
        public virtual void UnhideTabStripItem(int index)
        {
            string template = "{0}.unhideTabStripItem({1});";
            this.AddScript(template, this.ClientID, index);
        }

        /// <summary>
        /// Unhides the tab strip item for the passed tab
        /// </summary>
        [Description("Unhides the tab strip item for the passed tab")]
        public virtual void UnhideTabStripItem(Tab tab)
        {
            this.UnhideTabStripItem(tab.ClientID);
        }

        /// <summary>
        /// Unhides the tab strip item for the passed tab
        /// </summary>
        [Description("Unhides the tab strip item for the passed tab")]
        public virtual void UnhideTabStripItem(string id)
        {
            Tab tab = Coolite.Utilities.ControlUtils.FindControl(this, id) as Tab;

            if (tab == null)
            {
                throw new InvalidOperationException(string.Format("The '{0}' Tab does not exist with the '{1}' TabPanel.", id, this.ID));
            }

            string template = "{0}.unhideTabStripItem(\"{1}\");";
            this.AddScript(template, this.ClientID, tab.ClientID);
        }
    }
}