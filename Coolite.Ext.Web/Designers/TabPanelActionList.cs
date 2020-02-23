/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web 
{
    public class TabPanelActionList : WebControlActionList
    {
        protected readonly IDesigner designer;
        public TabPanelActionList(IDesigner designer) : base(designer.Component)
        {
            this.designer = designer;
        }

        private TabPanelDesigner Designer
        {
            get
            {
                return designer as TabPanelDesigner;
            }
        }


        public bool Plain
        {
            get
            {
                return ((TabPanel)this.Control).Plain;
            }
            set
            {
                this.GetPropertyByName("Plain").SetValue(this.Control, value);
            }
        }

        public bool Border
        {
            get
            {
                return ((TabPanel)this.Control).Border;
            }
            set
            {
                this.GetPropertyByName("Border").SetValue(this.Control, value);
            }
        }

        public bool AutoPostBack
        {
            get
            {
                return ((TabPanel) this.Control).AutoPostBack;
            }
            set
            {
                this.GetPropertyByName("AutoPostBack").SetValue(this.Control, value);
            }
        }

        public bool EnableTabScroll
        {
            get
            {
                return ((TabPanel)this.Control).EnableTabScroll;
            }
            set
            {
                this.GetPropertyByName("EnableTabScroll").SetValue(this.Control, value);
            }
        }

        public TabPosition TabPosition
        {
            get
            {
                return ((TabPanel)this.Control).TabPosition;
            }
            set
            {
                this.GetPropertyByName("TabPosition").SetValue(this.Control, value);
            }
        }

        public int ActiveTabIndex 
        {
            get
            {
                return ((TabPanel)this.Control).ActiveTabIndex;
            }
            set
            {
                this.GetPropertyByName("ActiveTabIndex").SetValue(this.Control, value);
            }
        }

        public Unit Height
        {
            get
            {
                return ((TabPanel)this.Control).Height;
            }
            set
            {
                this.GetPropertyByName("Height").SetValue(this.Control, value);
            }
        }

        public Unit Width
        {
            get
            {
                return ((TabPanel)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }


        public List<string> Tabs
        {
            get
            {
                List<string> tabsNames = new List<string>();

                //TabCollection tabs = ((TabPanel)this.Control).Tabs;
                //ItemsCollection<Tab> tabs = ((TabPanel)this.Control).Tabs;

                foreach (Tab tab in ((TabPanel)this.Control).Tabs)
                {
                    tabsNames.Add(tab.ID);
                }

                return tabsNames;
            }
            set
            {
                //TabPanel tabs = (TabPanel)this.Control;
                //Tab activeTab = tabs.Items[tabs.ActiveTabIndex];
                //TypeDescriptor.GetProperties(activeTab)["ID"].SetValue(activeTab, value);
                //ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        //public string[] Tabs
        //{
        //    get
        //    {
        //        string[] tabs = new string[] { };

        //        TabCollection items = ((TabPanel)this.Control).Items;

        //        for (int i = 0; i < items.Count; i++)
        //        {
        //            tabs[i] = items[i].ID;
        //        }

        //        return tabs;
        //    }
        //}

        //public TabCollection Tabs
        //{
        //    get
        //    {
        //        TabPanel tabs = (TabPanel)this.Control;
        //        return tabs.Items;
        //    }
        //}

        public string TabID
        {
            get
            {
                TabPanel tabs = (TabPanel)this.Control;
                return tabs.Tabs[tabs.ActiveTabIndex].ID;
            }
            set
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab activeTab = tabs.Tabs[tabs.ActiveTabIndex];
                TypeDescriptor.GetProperties(activeTab)["ID"].SetValue(activeTab, value);
                ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        public string Title
        {
            get
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab tab = tabs.Tabs[tabs.ActiveTabIndex];
                return tab != null ? tab.Title : "[No title]";
            }
            set
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab activeTab = tabs.Tabs[tabs.ActiveTabIndex];
                if(activeTab == null)
                {
                    return;
                }
                TypeDescriptor.GetProperties(activeTab)["Title"].SetValue(activeTab, value);
                ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        public Icon Icon
        {
            get
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab tab = tabs.Tabs[tabs.ActiveTabIndex];
                return tab.Icon;
            }
            set
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab activeTab = tabs.Tabs[tabs.ActiveTabIndex];
                if (activeTab == null)
                {
                    return;
                }
                TypeDescriptor.GetProperties(activeTab)["Icon"].SetValue(activeTab, value);
                ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        public bool Closable
        {
            get
            {
                TabPanel tabs = (TabPanel)this.Control;
                return tabs.Tabs[tabs.ActiveTabIndex].Closable;
            }
            set
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab activeTab = tabs.Tabs[tabs.ActiveTabIndex];
                if (activeTab == null)
                {
                    return;
                }

                TypeDescriptor.GetProperties(activeTab)["Closable"].SetValue(activeTab, value);
                ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        public bool Disabled
        {
            get
            {
                TabPanel tabs = (TabPanel)this.Control;
                return tabs.Tabs[tabs.ActiveTabIndex].Disabled;
            }
            set
            {
                TabPanel tabs = (TabPanel)this.Control;
                Tab activeTab = tabs.Tabs[tabs.ActiveTabIndex];
                if (activeTab == null)
                {
                    return;
                }

                TypeDescriptor.GetProperties(activeTab)["Disabled"].SetValue(activeTab, value);
                ControlDesigner.InvokeTransactedChange(this.Component, new TransactedChangeCallback(func), activeTab, "Desc");
            }
        }

        private static bool func(object o)
        {
            return true;
        }

        public void AddTabAtBegin()
        {
            Designer.AddTabAtBegin();
        }

        public void AddTabAtEnd()
        {
            Designer.AddTabAtEnd();
        }

        public void AddTabAfterActive()
        {
            Designer.AddTabAfterActive();
        }

        public void RemoveActiveTab()
        {
            Designer.RemoveActiveTab();
        }

        internal bool ActiveTabIndexIsValid
        {
            get
            {
                TabPanel tabPanelControl = (TabPanel) this.Control;
                return
                    tabPanelControl.ActiveTabIndex > -1 &&
                    tabPanelControl.ActiveTabIndex < tabPanelControl.Tabs.Count;
            }
        }


        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Border", "Border", "500", "Add/Remove Border from TabPanel"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Plain", "Plain", "500", "Render the TabPanel without a background image"));
            this.AddPropertyItem(new DesignerActionPropertyItem("AutoPostBack", "AutoPostBack", "500", "Automatically PostBack on Tab Changed"));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableTabScroll", "Enable Tab Scrolling", "500", "Enable scrolling to tabs that may be invisible"));
            this.AddPropertyItem(new DesignerActionPropertyItem("TabPosition", "Tab Position", "500", "The position where the Tabs should be rendered"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the TabPanel"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Height", "Height", "500", "Change the Height of the TabPanel"));
            this.AddPropertyItem(new DesignerActionPropertyItem("ActiveTabIndex", "Active Tab", "500", "The numeric index of the Tab that should be initially activated"));

            DesignerActionHeaderItem activeTabHeader = new DesignerActionHeaderItem("Edit Tab Properties", "600");
            DesignerActionPropertyItem tabs = new DesignerActionPropertyItem("Tabs", "Select Tab", "600");
            DesignerActionPropertyItem tabID = new DesignerActionPropertyItem("TabID", "ID", "600");
            DesignerActionPropertyItem title = new DesignerActionPropertyItem("Title", "Title", "600");
            DesignerActionPropertyItem iconCls = new DesignerActionPropertyItem("Icon", "Icon", "600", "The Icon to use");
            DesignerActionPropertyItem closable = new DesignerActionPropertyItem("Closable", "Closable", "600", "Check to disable Tab");
            DesignerActionPropertyItem disabled = new DesignerActionPropertyItem("Disabled", "Disabled", "600", "Allow the user to close the Tab");
            if (this.ActiveTabIndexIsValid)
            {
                this.AddHeaderItem(activeTabHeader);
                this.AddPropertyItem(tabID);
                this.AddPropertyItem(title);
                this.AddPropertyItem(iconCls);
                this.AddPropertyItem(closable);
                this.AddPropertyItem(disabled);
            }
            else
            {
                this.RemovePropertyItem(disabled);
                this.RemovePropertyItem(closable);
                this.RemovePropertyItem(iconCls);
                this.RemovePropertyItem(title);
                this.RemovePropertyItem(tabID);
                this.RemoveHeaderItem(activeTabHeader);
            }

            this.AddHeaderItem(new DesignerActionHeaderItem("Operations", "700"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "AddTabAtBegin", "Insert Tab at Start", "700", "Insert Tab at Start"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "AddTabAtEnd", "Insert Tab at End", "700", "Insert Tab at End"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "AddTabAfterActive", "Insert Tab after Active Tab", "700", "Insert Tab after Active Tab"));
            this.AddMethodItem(new DesignerActionMethodItem(this, "RemoveActiveTab", "Remove Active Tab", "700", "Remove Active Tab"));

            return base.GetSortedActionItems();
        }
    }
}