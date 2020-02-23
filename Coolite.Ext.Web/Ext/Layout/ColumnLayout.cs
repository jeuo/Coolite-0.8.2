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
using System.Globalization;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This is the layout style of choice for creating structural layouts in a multi-column format where the width of each column can be specified as a percentage or fixed width, but the height is allowed to vary based on the content. 
    /// </summary>
    [Layout("coolitecolumn")]
    [ToolboxData("<{0}:ColumnLayout runat=\"server\"><{0}:LayoutColumn><{0}:Panel runat=\"server\" Title=\"Column 1 (125px)\" Width=\"125\" Height=\"200\" /></{0}:LayoutColumn><{0}:LayoutColumn ColumnWidth=\"0.8\"><{0}:Panel runat=\"server\" Title=\"Column 2 (80% of remainder)\" Height=\"200\" /></{0}:LayoutColumn><{0}:LayoutColumn ColumnWidth=\"0.2\"><{0}:Panel runat=\"server\" Title=\"Column 3 (20% of remainder)\" Height=\"200\" /></{0}:LayoutColumn></{0}:ColumnLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ColumnLayout), "Build.Resources.ToolboxIcons.ColumnLayout.bmp")]
    [DefaultProperty("Columns")]
    [ParseChildren(true, "Columns")]
    [Description("This is the layout style of choice for creating structural layouts in a multi-column format where the width of each column can be specified as a percentage or fixed width, but the height is allowed to vary based on the content. ")]
    public class ColumnLayout : ContainerLayout
    {
        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new ColumnLayoutConfigProxy(
                    this.FitHeight,
                    this.Split,
                    this.Margin,
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }
        
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to fit the column elements height-wise into the Container. Defaults to false.")]
        [NotifyParentProperty(true)]
        public virtual bool FitHeight
        {
            get
            {
                object obj = this.ViewState["FitHeight"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["FitHeight"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow resizing of the columns using a SplitBar. Defaults to false.")]
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

        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("Width of margin between columns in pixels. Overrides any style settings. Defaults to 0.")]
        [NotifyParentProperty(true)]
        public virtual int Margin
        {
            get
            {
                object obj = this.ViewState["Margin"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Margin"] = value;
            }
        }
        
        private LayoutColumnCollection columns;

        /// <summary>
        /// Columns collection
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Columns collection")]
        [ViewStateMember]
        public LayoutColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new LayoutColumnCollection();
                    this.columns.AfterItemAdd += Columns_AfterItemAdd;
                }
                return this.columns;
            }
        }

        void Columns_AfterItemAdd(LayoutColumn item)
        {
            if (item.Control != null)
            {
                this.Items.Add((Component)item.Control);
                item.Items[0].AdditionalConfig = item;
            }
            item.Items.AfterItemAdd += delegate(Component cItem)
                                           {
                                               this.Items.Add(cItem);
                                               cItem.AdditionalConfig = item;
                                           };
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                foreach (LayoutColumn column in this.Columns)
                {
                    if (column.Items.Count == 0)
                    {
                        throw new InvalidOperationException("This Column must contain a Component");
                    }
                }
            }

            base.OnPreRender(e);
        }
    }

    [ToolboxItem(false)]
    public class ColumnLayoutConfigProxy : LayoutConfig
    {
        private readonly bool fitHeight;
        private readonly bool split;
        private readonly int margin;

        public ColumnLayoutConfigProxy(bool fitHeight, bool split, int margin, bool renderHidden, string extraCls)
            : base(renderHidden, extraCls)
        {
            this.fitHeight = fitHeight;
            this.split = split;
            this.margin = margin;
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool FitHeight
        {
            get
            {
                return this.fitHeight;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool Split
        {
            get
            {
                return this.split;
            }
        }
       
        [ClientConfig]
        [DefaultValue(0)]
        public virtual int Margin
        {
            get
            {
                return this.margin;
            }
        }
    }

    public class LayoutColumnCollection : StateManagedCollection<LayoutColumn> { }

    public class LayoutColumn : LayoutItem
    {
        /// <summary>
        /// The ColumnWidth property is always evaluated as a percentage, and must be a decimal value greater than 0 and less than 1.
        /// </summary>
        [NotifyParentProperty(true)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The ColumnWidth property is always evaluated as a percentage, and must be a decimal value greater than 0 and less than or equal to 1.0.")]
        public virtual decimal ColumnWidth
        {
            get
            {
                object obj = this.ViewState["ColumnWidth"];
                return (obj == null) ? 0 : (decimal)obj;
            }
            set
            {
                if (value > 1 || value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The value must be greater than 0 and less than or equal to 1.0.");
                }
                this.ViewState["ColumnWidth"] = value;
            }
        }

        [ClientConfig("columnWidth", JsonMode.Raw)]
        [DefaultValue("0")]
        [Browsable(false)]
        public string ColumnWidthConverter
        {
            get
            {
                NumberFormatInfo nf = new NumberFormatInfo();
                nf.CurrencyDecimalSeparator = ".";
                return ColumnWidth.ToString(nf);
            }
        }
    }
}