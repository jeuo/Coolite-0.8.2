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
using System.Drawing.Design;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This layout allows you to easily render content into an HTML table. The total number of columns can be specified, and rowspan and colspan can be used to create complex layouts within the table.
    /// </summary>
    [Layout("table")]
    [ToolboxData("<{0}:TableLayout runat=\"server\" Columns=\"2\"><{0}:Cell><{0}:Panel runat=\"server\" Title=\"Panel 1\" Frame=\"true\" Width=\"200\" Height=\"200\" StyleSpec=\"padding: 5px;\" /></{0}:Cell><{0}:Cell><{0}:Panel runat=\"server\" Title=\"Panel 2\" Frame=\"true\" Width=\"200\" Height=\"200\"StyleSpec=\"padding: 5px 0;\" /></{0}:Cell><{0}:Cell ColSpan=\"2\"><{0}:Panel runat=\"server\" Title=\"Panel 3\" Frame=\"true\" Width=\"400\" Height=\"200\"StyleSpec=\"padding: 0 0 5px 5px\" /></{0}:Cell></{0}:TableLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TableLayout), "Build.Resources.ToolboxIcons.TableLayout.bmp")]
    [DefaultProperty("Cells")]
    [ParseChildren(true, "Cells")]
    public class TableLayout : ContainerLayout
    {
        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new TableLayoutProxy(
                    this.Columns, 
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }

        /// <summary>
        /// The total number of columns to create in the table for this layout. If not specified, all panels added to this layout will be rendered into a single row using a column per panel.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The total number of columns to create in the table for this layout. If not specified, all panels added to this layout will be rendered into a single row using a column per panel.")]
        public virtual int Columns
        {
            get
            {
                object obj = this.ViewState["Columns"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Columns"] = value;
            }
        }

        private CellCollection cells;

        /// <summary>
        /// Cells collection
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Cells collection")]
        [ViewStateMember]
        public CellCollection Cells
        {
            get
            {
                if (this.cells == null)
                {
                    this.cells = new CellCollection();
                    this.cells.AfterItemAdd += Cells_AfterItemAdd;
                }
                return this.cells;
            }
        }

        private void Cells_AfterItemAdd(Cell item)
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
                foreach (Cell column in this.Cells)
                {
                    if (column.Items.Count == 0)
                    {
                        throw new InvalidOperationException("Cell in TableLayout must contain any container");
                    }
                }
            }

            base.OnPreRender(e);
        }
    }

    [ToolboxItem(false)]
    public class TableLayoutProxy : LayoutConfig
    {
        private readonly int columns;

        public TableLayoutProxy(int columns, bool renderHidden, string extraCls)
            : base(renderHidden, extraCls)
        {
            this.columns = columns;
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue(0)]
        public int Columns
        {
            get { return columns; }
        }
    }

    public class CellCollection : StateManagedCollection<Cell> { }

    public class Cell : LayoutItem
    {
        [ClientConfig("rowspan", JsonMode.Raw)]
        [NotifyParentProperty(true)]
        [Category("Config Options")]
        [DefaultValue(0)]
        public virtual int RowSpan
        {
            get
            {
                object obj = this.ViewState["RowSpan"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["RowSpan"] = value;
            }
        }

        [ClientConfig("colspan", JsonMode.Raw)]
        [NotifyParentProperty(true)]
        [Category("Config Options")]
        [DefaultValue(0)]
        public virtual int ColSpan
        {
            get
            {
                object obj = this.ViewState["ColSpan"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["ColSpan"] = value;
            }
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string CellCls
        {
            get
            {
                return (string)this.ViewState["CellCls"] ?? "";
            }
            set
            {
                this.ViewState["CellCls"] = value;
            }
        }
    }
}