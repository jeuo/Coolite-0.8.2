/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [DefaultProperty("Items")]
    [ParseChildren(true, "Items")]
    public class BorderLayoutRegion : StateManagedItem
    {
        private readonly RegionPosition region;
        private readonly Layout layout;

        public BorderLayoutRegion(Layout layout, RegionPosition region)
        {
            this.region = region;
            this.layout = layout;
        }

        private ItemsCollection<Component> items;

        [Category("Config Options")]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Region items")]
        [NotifyParentProperty(true)]
        public virtual ItemsCollection<Component> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Component>();
                    this.items.AfterItemAdd += AfterItemAdd;
                    this.items.SingleItemMode = true;
                }
                return this.items;
            }
        }

        protected void AfterItemAdd(Component item)
        {
            item.AdditionalConfig = this;
            layout.Items.Add(item);
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        public RegionPosition Region
        {
            get { return this.region; }
        }

        /// <summary>
        /// When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel that will close again once the user mouses out of that panel (or clicks out if AutoHide = false). Setting animFloat to false will prevent the open and close of these floated panels from being animated (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel that will close again once the user mouses out of that panel (or clicks out if AutoHide = false). Setting animFloat to false will prevent the open and close of these floated panels from being animated (defaults to true).")]
        [NotifyParentProperty(true)]
        public virtual bool AnimFloat
        {
            get
            {
                object obj = this.ViewState["AnimFloat"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AnimFloat"] = value;
            }
        }

        /// <summary>
        /// When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel. If autoHide is true, the panel will automatically hide after the user mouses out of the panel. If autoHide is false, the panel will continue to display until the user clicks outside of the panel (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("When a collapsed region's bar is clicked, the region's panel will be displayed as a floated panel. If autoHide is true, the panel will automatically hide after the user mouses out of the panel. If autoHide is false, the panel will continue to display until the user clicks outside of the panel (defaults to true).")]
        [NotifyParentProperty(true)]
        public virtual bool AutoHide
        {
            get
            {
                object obj = this.ViewState["AutoHide"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoHide"] = value;
            }
        }

        /// <summary>
        /// A string containing margins to apply to the region's collapsed element. Example '5 0 5 0' (addToStart, Right, Bottom, Left)
        /// </summary>
        [ClientConfig("cmargins")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A string containing margins to apply to the region's collapsed element. Example '5 0 5 0' (addToStart, Right, Bottom, Left)")]
        public string CMarginsSummary
        {
            get
            {
                object obj = this.ViewState["CMarginsSummary"];

                string temp = (obj == null) ? "" : (string)obj;

                if (!string.IsNullOrEmpty(temp))
                {
                    this.CMargins.Clear();
                }
                return temp;
            }
            set
            {
                this.ViewState["CMarginsSummary"] = value;
            }
        }

        private Margins cMargins;

        /// <summary>
        /// An object containing margins to apply to the region's collapsed element.
        /// </summary>
        [ClientConfig("cmargins", typeof(MarginsJsonConverter))]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("An object containing margins to apply to the region's collapsed element.")]
        [DefaultValue("-1 -1 -1 -1")]
        public Margins CMargins
        {
            get
            {
                if (this.cMargins == null)
                {
                    this.cMargins = new Margins(-1, -1, -1, -1);
                }

                return this.cMargins;
            }
        }

        /// <summary>
        /// By default, collapsible regions are collapsed by clicking the expand/collapse tool button that renders into the region's title bar. Optionally, when collapseMode is set to 'mini' the region's split bar will also display a small collapse button in the center of the bar. In 'mini' mode the region will collapse to a thinner bar than in normal mode. By default collapseMode is undefined, and the only two supported values are undefined and 'mini'. Note that if a collapsible region does not have a title bar, then collapseMode must be set to 'mini' in order for the region to be collapsible by the user as the tool button will not be rendered.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(CollapseMode.Default)]
        [Description("By default, collapsible regions are collapsed by clicking the expand/collapse tool button that renders into the region's title bar. Optionally, when collapseMode is set to 'mini' the region's split bar will also display a small collapse button in the center of the bar. In 'mini' mode the region will collapse to a thinner bar than in normal mode. By default collapseMode is undefined, and the only two supported values are undefined and 'mini'. Note that if a collapsible region does not have a title bar, then collapseMode must be set to 'mini' in order for the region to be collapsible by the user as the tool button will not be rendered.")]
        [NotifyParentProperty(true)]
        public CollapseMode CollapseMode
        {
            get
            {
                object obj = this.ViewState["CollapseMode"];
                return obj == null ? CollapseMode.Default : (CollapseMode)obj;
            }
            set 
            {
                ViewState["CollapseMode"] = value; 
            }
        }

        /// <summary>
        /// True to allow the user to collapse this region (defaults to false). If true, an expand/collapse tool button will automatically be rendered into the title bar of the region, otherwise the button will not be shown. Note that a title bar is required to display the toggle button -- if no region title is specified, the region will only be collapsible if CollapseMode is set to 'Mini'.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow the user to collapse this region (defaults to false). If true, an expand/collapse tool button will automatically be rendered into the title bar of the region, otherwise the button will not be shown. Note that a title bar is required to display the toggle button -- if no region title is specified, the region will only be collapsible if CollapseMode is set to 'Mini'.")]
        [NotifyParentProperty(true)]
        public virtual bool Collapsible
        {
            get
            {
                object obj = this.ViewState["Collapsible"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Collapsible"] = value;
            }
        }

        /// <summary>
        /// True to allow clicking a collapsed region's bar to display the region's panel floated above the layout, false to force the user to fully expand a collapsed region by clicking the expand button to see it again (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to allow clicking a collapsed region's bar to display the region's panel floated above the layout, false to force the user to fully expand a collapsed region by clicking the expand button to see it again (defaults to true).")]
        public virtual bool Floatable
        {
            get
            {
                object obj = this.ViewState["Floatable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Floatable"] = value;
            }
        }

        /// <summary>
        /// An string containing margins to apply to the region. Example '5 0 5 0' (addToStart, Right, Bottom, Left)
        /// </summary>
        [ClientConfig("margins")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("An string containing margins to apply to the region. Example '5 0 5 0' (addToStart, Right, Bottom, Left)")]
        public string MarginsSummary
        {
            get
            {
                object obj = this.ViewState["MarginsSummary"];

                string temp = (obj == null) ? "" : (string)obj;

                if(!string.IsNullOrEmpty(temp))
                {
                    this.Margins.Clear();
                }
                return temp;
            }
            set
            {
                this.ViewState["MarginsSummary"] = value;
            }
        }

        private Margins margins;

        /// <summary>
        /// An object containing margins to apply to the region.
        /// </summary>
        [NotifyParentProperty(true)]
        [ClientConfig("margins", typeof(MarginsJsonConverter))]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("An object containing margins to apply to the region.")]
        [DefaultValue("-1 -1 -1 -1")]
        public Margins Margins
        {
            get
            {
                if (this.margins == null)
                {
                    this.margins = new Margins(-1, -1, -1, -1);
                }
                return this.margins;
            }
        }

        /// <summary>
        /// The minimum allowable height in pixels for this region (defaults to 50)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "50")]
        [Description("The minimum allowable height in pixels for this region (defaults to 50)")]
        [NotifyParentProperty(true)]
        public virtual Unit MinHeight
        {
            get
            {
                object obj = this.ViewState["MinHeight"];
                return (obj == null) ? Unit.Pixel(50) : (Unit)obj;
            }
            set
            {
                this.ViewState["MinHeight"] = value;
            }
        }

        /// <summary>
        /// The maximum allowable height in pixels for this region
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The maximum allowable height in pixels for this region")]
        [NotifyParentProperty(true)]
        public virtual Unit MaxHeight
        {
            get
            {
                object obj = this.ViewState["MaxHeight"];
                return (obj == null) ? Unit.Empty : (Unit)obj;
            }
            set
            {
                this.ViewState["MaxHeight"] = value;
            }
        }

        /// <summary>
        /// The maximum allowable width in pixels for this region.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The maximum allowable width in pixels for this region.")]
        [NotifyParentProperty(true)]
        public virtual Unit MaxWidth
        {
            get
            {
                object obj = this.ViewState["MaxWidth"];
                return (obj == null) ? Unit.Empty : (Unit)obj;
            }
            set
            {
                this.ViewState["MaxWidth"] = value;
            }
        }

        /// <summary>
        /// The minimum allowable width in pixels for this region (defaults to 50)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "50")]
        [Description("The minimum allowable width in pixels for this region (defaults to 50)")]
        [NotifyParentProperty(true)]
        public virtual Unit MinWidth
        {
            get
            {
                object obj = this.ViewState["MinWidth"];
                return (obj == null) ? Unit.Pixel(50) : (Unit)obj;
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        /// <summary>
        /// True to display a tooltip when the user hovers over a region's split bar (defaults to false). The tooltip text will be the value of either SplitTip or CollapsibleSplitTip as appropriate.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display a tooltip when the user hovers over a region's split bar (defaults to false). The tooltip text will be the value of either SplitTip or CollapsibleSplitTip as appropriate.")]
        [NotifyParentProperty(true)]
        public virtual bool UseSplitTips
        {
            get
            {
                object obj = this.ViewState["UseSplitTips"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["UseSplitTips"] = value;
            }
        }

        /// <summary>
        /// The tooltip to display when the user hovers over a collapsible region's split bar. Only applies if UseSplitTips = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Drag to resize. Double click to hide.")]
        [Description("The tooltip to display when the user hovers over a collapsible region's split bar. Only applies if UseSplitTips = true.")]
        [NotifyParentProperty(true)]
        public virtual string CollapsibleSplitTip
        {
            get
            {
                return (string)this.ViewState["CollapsibleSplitTip"] ?? "Drag to resize. Double click to hide.";
            }
            set
            {
                this.ViewState["CollapsibleSplitTip"] = value;
            }
        }

        /// <summary>
        /// True to display a SplitBar between this region and its neighbor, allowing the user to resize the regions dynamically (defaults to false). When split = true, it is common to specify a minSize and maxSize for the region.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display a SplitBar between this region and its neighbor, allowing the user to resize the regions dynamically (defaults to false). When split = true, it is common to specify a minSize and maxSize for the region.")]
        [NotifyParentProperty(true)]
        public virtual bool Split
        {
            get
            {
                object obj = this.ViewState["Split"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Split"] = value;
            }
        }

        /// <summary>
        /// The tooltip to display when the user hovers over a non-collapsible region's split bar. Only applies if UseSplitTips = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Drag to resize.")]
        [Description("The tooltip to display when the user hovers over a non-collapsible region's split bar. Only applies if UseSplitTips = true.")]
        [NotifyParentProperty(true)]
        public virtual string SplitTip
        {
            get
            {
                return (string)this.ViewState["SplitTip"] ?? "Drag to resize.";
            }
            set
            {
                this.ViewState["SplitTip"] = value;
            }
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}