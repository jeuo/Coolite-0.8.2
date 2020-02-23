/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [Xtype("")]
    public class RadioColumn : Radio
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get { return base.Value; }
            set { base.Value = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string BoxLabel
        {
            get { return base.BoxLabel; }
            set { base.BoxLabel = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool Checked
        {
            get { return base.Checked; }
            set { base.Checked = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string CheckedCls
        {
            get { return base.CheckedCls; }
            set { base.CheckedCls = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string FocusCls
        {
            get { return base.FocusCls; }
            set { base.FocusCls = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal override string InputValue
        {
            get { return base.InputValue; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string MouseDownCls
        {
            get { return base.MouseDownCls; }
            set { base.MouseDownCls = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string OverCls
        {
            get { return base.OverCls; }
            set { base.OverCls = value; }
        }

        /// <summary>
        /// The ColumnWidth property is always evaluated as a percentage, and must be a decimal value greater than 0 and less than 1.
        /// </summary>
        [NotifyParentProperty(true)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The ColumnWidth property is always evaluated as a percentage, and must be a decimal value greater than 0 and less than 1.")]
        public virtual decimal ColumnWidth
        {
            get
            {
                object obj = this.ViewState["ColumnWidth"];
                return (obj == null) ? 0 : (decimal)obj;
            }
            set
            {
                if (value >= 1 || value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The value must be greater than 0 and less than 1");
                }
                this.ViewState["ColumnWidth"] = value;
            }
        }

        [ClientConfig("columnWidth")]
        [DefaultValue("0")]
        [Browsable(false)]
        internal string ColumnWidthConverter
        {
            get
            {
                NumberFormatInfo nf = new NumberFormatInfo();
                nf.CurrencyDecimalSeparator = ".";
                return ColumnWidth.ToString(nf);
            }
        }

        ItemsCollection<Component> items;

        /// <summary>
        /// Items collection
        /// </summary>
        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Items collection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual ItemsCollection<Component> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Component>();
                    this.items.AfterItemAdd += AfterItemAdd;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Component item)
        {
            if (!this.Controls.Contains(item))
            {
                this.Controls.Add(item);
            }

            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }
    }
}