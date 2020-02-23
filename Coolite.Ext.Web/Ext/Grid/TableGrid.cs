/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A Grid which creates itself from an existing HTML table element.
    /// </summary>
    [InstanceOf(ClassName = "Ext.grid.TableGrid")]
    [Xtype("tablegrid")]
    [ClientScript(FilePath = "/coolite/coolite-data.js", PathDebug = "/coolite/coolite-data-debug.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data.js", WebResourceDebug = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data-debug.js")]
    [ToolboxData("<{0}:TableGrid runat=\"server\"></{0}:TableGrid>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TableGrid), "Build.Resources.ToolboxIcons.TableGrid.bmp")]
    [Description("A Grid which creates itself from an existing HTML table element.")]
    public class TableGrid : GridPanel
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if(!this.RenderingOnDemand)
            {
                this.AddScript(string.Concat(this.ClientID,".render();"));
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        [ClientConfig("table", JsonMode.Raw)]
        internal string TableProxy
        {
            get
            {
                string parsedTable = TokenUtils.ParseTokens(this.Table, this);

                if (TokenUtils.IsRawToken(parsedTable))
                {
                    return TokenUtils.ReplaceRawToken(parsedTable);
                }

                return string.Concat("\"", parsedTable, "\"");
            }
        }

        /// <summary>
        /// The table element from which this grid will be created.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The table element from which this grid will be created.")]
        public virtual string Table
        {
            get
            {
                return (string)this.ViewState["Table"] ?? "";
            }
            set
            {
                this.ViewState["Table"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool RenderingOnDemand
        {
            get
            {
                object obj = this.ViewState["RenderingOnDemand"];
                return obj == null ? false : (bool) obj;
            }
            set
            {
                this.ViewState["RenderingOnDemand"] = value;
            }
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        public override ColumnModel ColumnModel
        {
            get
            {
                return base.ColumnModel;
            }
        }

        private ColumnCollection columns;

        /// <summary>
        /// The columns to use when rendering the grid (required).
        /// </summary>
        [ClientConfig("columns", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("The columns to use when rendering the grid (required).")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = new ColumnCollection();
                }
                return this.columns;
            }
        }

        private RecordFieldCollection fields;

        /// <summary>
        /// An array of field definition objects
        /// </summary>
        [ClientConfig("fields", JsonMode.AlwaysArray)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Either a Collection of RecordField definition objects")]
        [NotifyParentProperty(true)]
        public RecordFieldCollection Fields
        {
            get
            {
                if (fields == null)
                {
                    fields = new RecordFieldCollection();
                }
                return fields;
            }
        }
    }
}
