/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Web.UI;
using Coolite.Ext.Web;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.grid.GridFilters")]
    [ClientScript(Type = typeof(GridFilters), FilePath = "/ux/plugins/gridfilters/gridfilters.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.gridfilters.gridfilters.js")]
    [ClientStyle(Type = typeof(GridFilters), FilePath ="/ux/plugins/gridfilters/css/gridfilters.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.gridfilters.css.gridfilters-embedded.css")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.GridFilters), "Build.Resources.ToolboxIcons.GridFilters.bmp")]
    [ToolboxData("<{0}:GridFilters runat=\"server\" />")]
    [Description("GridFilters plugin used for filter by columns")]
    public class GridFilters : Plugin
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if(!this.DesignMode)
            {
                this.Page.LoadComplete += Page_LoadComplete;
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            GridPanel grid = this.ParentComponent as GridPanel;

            foreach (GridFilter filter in this.Filters)
            {
                filter.ParentGrid = grid;
            }

            if (!Ext.IsAjaxRequest && grid != null)
            {
                PagingToolbar pBar = null;
                if (grid.BottomBar.Count > 0 && grid.BottomBar[0] is PagingToolbar)
                {
                    pBar = grid.BottomBar[0] as PagingToolbar;
                }
                else if (grid.TopBar.Count > 0 && grid.TopBar[0] is PagingToolbar)
                {
                    pBar = grid.TopBar[0] as PagingToolbar;
                }

                if (!string.IsNullOrEmpty(grid.StoreID) && pBar != null)
                {
                    Store store = ControlUtils.FindControl(this, grid.StoreID) as Store;
                    if (store != null && store.Proxy.Count == 0)
                    {
                        this.Local = false;
                    }
                }
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(500)]
        [NotifyParentProperty(true)]
        [Description("Number of milisecond to defer store updates since the last filter change.")]
        public int UpdateBuffer
        {
            get
            {
                object obj = this.ViewState["UpdateBuffer"];
                return obj == null ? 500 : (int)obj;
            }
            set
            {
                this.ViewState["UpdateBuffer"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("gridfilters")]
        [NotifyParentProperty(true)]
        [Description("The url parameter prefix for the filters.")]
        public string ParamPrefix
        {
            get
            {
                object obj = this.ViewState["ParamPrefix"];
                return obj == null ? "gridfilters" : (string)obj;
            }
            set
            {
                this.ViewState["ParamPrefix"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("ux-filtered-column")]
        [NotifyParentProperty(true)]
        [Description("The css class to be applied to column headers that active filters. Defaults to 'ux-filterd-column'.")]
        public string FilterCls
        {
            get
            {
                object obj = this.ViewState["FilterCls"];
                return obj == null ? "ux-filtered-column" : (string)obj;
            }
            set
            {
                this.ViewState["FilterCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to use Ext.data.Store filter functions instead of server side filtering.")]
        public bool Local
        {
            get
            {
                object obj = this.ViewState["Local"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Local"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to automagicly reload the datasource when a filter change happens.")]
        public bool AutoReload
        {
            get
            {
                object obj = this.ViewState["AutoReload"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoReload"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to show the filter menus.")]
        public bool ShowMenu
        {
            get
            {
                object obj = this.ViewState["ShowMenu"];
                return obj == null ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ShowMenu"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Filters")]
        [NotifyParentProperty(true)]
        [Description("The text displayed for the 'Filters' menu item.")]
        public string FiltersText
        {
            get
            {
                object obj = this.ViewState["FiltersText"];
                return obj == null ? "Filters" : (string)obj;
            }
            set
            {
                this.ViewState["FiltersText"] = value;
            }
        }

        private GridFilterCollection filters;

        [ClientConfig("filters", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual GridFilterCollection Filters
        {
            get
            {
                if (this.filters == null)
                {
                    this.filters = new GridFilterCollection();
                    this.filters.AfterItemAdd += Filters_AfterItemAdd;
                }
                return this.filters;
            }
        }

        void Filters_AfterItemAdd(GridFilter item)
        {
            item.ParentGrid = this.ParentComponent as GridPanel;
        }

        public void ClearFilters()
        {
            Ext.EnsureAjaxEvent();
            GridPanel grid = this.ParentComponent as GridPanel;
            if(grid != null)
            {
                grid.AddScript("{0}.getFilterPlugin().clearFilters();", grid.ClientID);
            }
        }
    }
}
