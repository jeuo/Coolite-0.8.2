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
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This is a base class for layouts that contain a single items that automatically expands to fill the layout's contentContainer. This class is intended to be extended or created via the layout:'fit' Ext.Container.layout config, and should generally not need to be created directly via the new keyword. FitLayout does not have any direct config options (other than inherited ones). To fit a panel to a contentContainer using FitLayout, simply set layout:'fit' on the contentContainer and add a single panel to it. If the contentContainer has multiple panels, only the first one will be displayed.
    /// </summary>
    [Layout("accordion")]
    [ToolboxData("<{0}:Accordion runat=\"server\" Animate=\"true\"><{0}:Panel runat=\"server\" Border=\"false\" Title=\"Item 1\"><Body></Body></{0}:Panel><{0}:Panel runat=\"server\" Border=\"false\" Title=\"Item 2\" Collapsed=\"true\"><Body></Body></{0}:Panel></{0}:Accordion>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Accordion), "Build.Resources.ToolboxIcons.Accordion.bmp")]
    [Designer(typeof(AccordionDesigner))]
    [Description("This is a base class for layouts that contain a single items that automatically expands to fill the layout's contentContainer. This class is intended to be extended or created via the layout:'fit' Ext.Container.layout config, and should generally not need to be created directly via the new keyword. FitLayout does not have any direct config options (other than inherited ones). To fit a panel to a contentContainer using FitLayout, simply set layout:'fit' on the contentContainer and add a single panel to it. If the contentContainer has multiple panels, only the first one will be displayed.")]
    public class Accordion : ContainerLayout
    {
        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new AccordionConfigProxy(
                    this.ActiveOnTop,
                    this.Animate,
                    this.AutoWidth,
                    this.CollapseFirst,
                    this.Fill,
                    this.HideCollapseTool,
                    this.Sequence,
                    this.TitleCollapse,
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }

        /// <summary>
        /// True to swap the position of each panel as it is expanded so that it becomes the first items in the contentContainer, false to keep the panels in the rendered order. This is NOT compatible with 'animate:true' (defaults to false).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to swap the position of each panel as it is expanded so that it becomes the first items in the contentContainer, false to keep the panels in the rendered order. This is NOT compatible with 'animate:true' (defaults to false).")]
        public virtual bool ActiveOnTop
        {
            get
            {
                object obj = this.ViewState["ActiveOnTop"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ActiveOnTop"] = value;
            }
        }

        /// <summary>
        /// True to slide the contained panels open and closed during expand/collapse using animation, false to open and close directly with no animation (defaults to false). Note: to defer to the specific config setting of each contained panel for this property, set this to undefined at the layout level.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to slide the contained panels open and closed during expand/collapse using animation, false to open and close directly with no animation (defaults to false). Note: to defer to the specific config setting of each contained panel for this property, set this to undefined at the layout level.")]
        public virtual bool Animate
        {
            get
            {
                object obj = this.ViewState["Animate"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }

        /// <summary>
        /// True to set each contained items's width to 'auto', false to use the items's current width (defaults to true).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to set each contained items's width to 'auto', false to use the items's current width (defaults to true).")]
        public virtual bool AutoWidth
        {
            get
            {
                object obj = this.ViewState["AutoWidth"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoWidth"] = value;
            }
        }

        /// <summary>
        /// True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the contained panels' title bars, false to render it last (defaults to false).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to make sure the collapse/expand toggle button always renders first (to the left of) any other tools in the contained panels' title bars, false to render it last (defaults to false).")]
        public virtual bool CollapseFirst
        {
            get
            {
                object obj = this.ViewState["CollapseFirst"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CollapseFirst"] = value;
            }
        }

        /// <summary>
        /// True to adjust the active items's height to fill the available space in the contentContainer, false to use the items's current height, or auto height if not explicitly set (defaults to true).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to adjust the active items's height to fill the available space in the contentContainer, false to use the items's current height, or auto height if not explicitly set (defaults to true).")]
        public virtual bool Fill
        {
            get
            {
                object obj = this.ViewState["Fill"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Fill"] = value;
            }
        }

        /// <summary>
        /// True to hide the contained panels' collapse/expand toggle buttons, false to display them (defaults to false). When set to true, titleCollapse should be true also.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the contained panels' collapse/expand toggle buttons, false to display them (defaults to false). When set to true, titleCollapse should be true also.")]
        public virtual bool HideCollapseTool
        {
            get
            {
                object obj = this.ViewState["HideCollapseTool"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideCollapseTool"] = value;
            }
        }

        /// <summary>
        /// Experimental. If animate is set to true, this will result in each animation running in sequence.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Experimental. If animate is set to true, this will result in each animation running in sequence.")]
        public virtual bool Sequence
        {
            get
            {
                object obj = this.ViewState["Sequence"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Sequence"] = value;
            }
        }

        /// <summary>
        /// True to allow expand/collapse of each contained panel by clicking anywhere on the title bar, false to allow expand/collapse only when the toggle tool button is clicked (defaults to true). When set to false, hideCollapseTool should be false also.
        /// </summary>
        /// <value><c>true</c> if [title collapse]; otherwise, <c>false</c>.</value>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to allow expand/collapse of each contained panel by clicking anywhere on the title bar, false to allow expand/collapse only when the toggle tool button is clicked (defaults to true). When set to false, hideCollapseTool should be false also.")]
        public virtual bool TitleCollapse
        {
            get
            {
                object obj = this.ViewState["TitleCollapse"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["TitleCollapse"] = value;
            }
        }

        public virtual int ExpandedPanelIndex
        {
            get
            {
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (!((PanelBase)this.Items[i]).Collapsed)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }

        public virtual void ExpandPanel(int index)
        {
            if (index >= this.Items.Count || index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            foreach (PanelBase panel in this.Items)
            {
                if (!panel.Collapsed)
                {
                    panel.Collapsed = true;
                }
            }

            ((PanelBase)this.Items[index]).Collapsed = false;
        }
    }

    [ToolboxItem(false)]
    public class AccordionConfigProxy : LayoutConfig
    {
        private readonly bool activeOnTop;
        private readonly bool animate;
        private readonly bool autoWidth;
        private readonly bool collapseFirst;
        private readonly bool fill;
        private readonly bool hideCollapseTool;
        private readonly bool sequence;
        private readonly bool titleCollapse;

        public AccordionConfigProxy(bool activeOnTop, bool animate, bool autoWidth, bool collapseFirst, bool fill, bool hideCollapseTool, bool sequence, bool titleCollapse, bool renderHidden, string extraCls)
            : base(renderHidden, extraCls)
        {
            this.activeOnTop = activeOnTop;
            this.animate = animate;
            this.autoWidth = autoWidth;
            this.collapseFirst = collapseFirst;
            this.fill = fill;
            this.hideCollapseTool = hideCollapseTool;
            this.sequence = sequence;
            this.titleCollapse = titleCollapse;
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool ActiveOnTop
        {
            get
            {
                return this.activeOnTop;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool Animate
        {
            get
            {
                return this.animate;
            }
        }

        [ClientConfig]
        [DefaultValue(true)]
        public virtual bool AutoWidth
        {
            get
            {
                return this.autoWidth;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool CollapseFirst
        {
            get
            {
                return this.collapseFirst;
            }
        }

        [ClientConfig]
        [DefaultValue(true)]
        public virtual bool Fill
        {
            get
            {
                return this.fill;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool HideCollapseTool
        {
            get
            {
                return this.hideCollapseTool;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool Sequence
        {
            get
            {
                return this.sequence;
            }
        }

        [ClientConfig]
        [DefaultValue(true)]
        public virtual bool TitleCollapse
        {
            get
            {
                return this.titleCollapse;
            }
        }
    }
}