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
    /// This is the layout style of choice for creating structural layouts in a multi-row format where the height of each row can be specified as a percentage or fixed height.
    /// </summary>
    [Layout("ux.row")]
    [ToolboxData("<{0}:RowLayout runat=\"server\"></{0}:RowLayout>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.RowLayout), "Build.Resources.ToolboxIcons.RowLayout.bmp")]
    [Description("This is the layout style of choice for creating structural layouts in a multi-row format where the height of each row can be specified as a percentage or fixed height.")]
    [Designer(typeof(EmptyDesigner))]
    [DefaultProperty("Rows")]
    [ParseChildren(true, "Rows")]
    public class RowLayout : ContainerLayout
    {
        private LayoutRowCollection rows;

        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Rows collection")]
        [ViewStateMember]
        public LayoutRowCollection Rows
        {
            get
            {
                if (this.rows == null)
                {
                    this.rows = new LayoutRowCollection();
                    this.rows.AfterItemAdd += Rows_AfterItemAdd;
                }
                return this.rows;
            }
        }

        void Rows_AfterItemAdd(LayoutRow item)
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
            base.OnPreRender(e);
            if (!this.DesignMode)
            {
                foreach (LayoutRow row in this.Rows)
                {
                    if (row.Items.Count == 0)
                    {
                        throw new InvalidOperationException("This Row must contain a Component");
                    }
                }
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

        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new RowLayoutConfigProxy(
                    this.Split,
                    this.Margin,
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }
    }

    [ToolboxItem(false)]
    public class RowLayoutConfigProxy : LayoutConfig
    {
        private readonly bool split;
        private readonly int margin;

        public RowLayoutConfigProxy(bool split, int margin, bool renderHidden, string extraCls)
            : base(renderHidden, extraCls)
        {
            this.split = split;
            this.margin = margin;
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

    public class LayoutRowCollection : StateManagedCollection<LayoutRow> { }

    public class LayoutRow : LayoutItem
    {
        [NotifyParentProperty(true)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The ColumnWidth property is always evaluated as a percentage, and must be a decimal value greater than 0 and less than or equal to 1.0.")]
        public virtual decimal RowHeight
        {
            get
            {
                object obj = this.ViewState["RowHeight"];
                return (obj == null) ? 0 : (decimal)obj;
            }
            set
            {
                if (value > 1 || value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The value must be greater than 0 and less than or equal to 1.0.");
                }
                this.ViewState["RowHeight"] = value;
            }
        }

        [ClientConfig("rowHeight", JsonMode.Raw)]
        [DefaultValue("0")]
        [Browsable(false)]
        internal string RowHeightProxy
        {
            get
            {
                NumberFormatInfo nf = new NumberFormatInfo();
                nf.CurrencyDecimalSeparator = ".";
                return RowHeight.ToString(nf);
            }
        }
    }
}