/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This class represents the primary interface of a component based grid control.
    /// </summary>
    [Xtype("coolitegrid")]
    [InstanceOf(ClassName = "Coolite.Ext.GridPanel")]
    [ClientScript(FilePath = "/coolite/coolite-data.js", PathDebug = "/coolite/coolite-data-debug.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data.js", WebResourceDebug = "Coolite.Ext.Web.Build.Resources.Coolite.coolite.coolite-data-debug.js")]
    [ToolboxData("<{0}:GridPanel runat=\"server\"></{0}:GridPanel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.GridPanel), "Build.Resources.ToolboxIcons.GridPanel.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("This class represents the primary interface of a component based grid control.")]
    public class GridPanel : GridPanelBase
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);
            this.CheckColumns();
            if (this.SelectionMemoryProxy && string.IsNullOrEmpty(this.MemoryIDField) && !string.IsNullOrEmpty(this.StoreID))
            {
                Store store = ControlUtils.FindControl(this, this.StoreID) as Store;

                if(store != null && store.Reader.Count > 0)
                {
                    string id = store.Reader.Reader.ReaderID;
                    if(!string.IsNullOrEmpty(id))
                    {
                        this.MemoryIDField = id;
                    }
                }
            }
        }

        public void RegisterColumnPlugins()
        {
            Ext.EnsureAjaxEvent();
            this.AddScript("{0}.initColumnPlugins({1}, true);", this.ClientID, this.GetColumnPlugins());
        }

        private void CheckColumns()
        {
            string plugins = this.GetColumnPlugins();

            if (plugins.Length > 2)
            {
                this.CustomConfig.Add(new ConfigItem("columnPlugins", plugins, ParameterMode.Raw));
            }
        }

        private string GetColumnPlugins()
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < this.ColumnModel.Columns.Count; i++)
            {
                CommandColumn cmdCol = this.ColumnModel.Columns[i] as CommandColumn;
                ImageCommandColumn imgCmdCol = this.ColumnModel.Columns[i] as ImageCommandColumn;
                Column column = this.ColumnModel.Columns[i] as Column;

                if (column != null && column.Commands.Count > 0 && imgCmdCol == null)
                {
                    this.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.commandcolumn.commandcolumn.css");
                    continue;
                }

                if (cmdCol != null || imgCmdCol != null)
                {
                    sb.Append(i + ",");
                    this.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.commandcolumn.commandcolumn.css");
                    continue;
                }

                CheckColumn cc = this.ColumnModel.Columns[i] as CheckColumn;

                if (cc != null && cc.Editable)
                {
                    sb.Append(i + ",");
                    continue;
                }
            }

            if (sb[sb.Length - 1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("]");
            return sb.ToString();
        }

        protected override void OnAfterClientInit(Observable sender)
        {
            base.OnAfterClientInit(sender);

            CheckAutoExpand();
        }

        private void CheckAutoExpand()
        {
            if(!string.IsNullOrEmpty(this.AutoExpandColumn))
            {
                bool found = false;
                foreach (ColumnBase column in this.ColumnModel.Columns)
                {
                    if(column.ColumnID == this.AutoExpandColumn)
                    {
                        found = true;
                        break;
                    }
                }

                if(!found)
                {
                    throw new ArgumentException(string.Concat("The auto expand Column with ID='", this.AutoExpandColumn,"' is not found!"));
                }
            }
        }

        private GridPanelListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public GridPanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new GridPanelListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private GridPanelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public GridPanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new GridPanelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        protected override void PageLoadComplete(object sender, EventArgs e)
        {
            base.PageLoadComplete(sender, e);

            //Here is we must add view templates because InnerObservable incorrect hosts inner controls
            if (this.View.View != null)
            {
                this.Controls.Add(this.View.View.Templates.Header);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            bool result = base.LoadPostData(postDataKey, postCollection);
            string val = postCollection[string.Concat(this.ClientID, "_SM")];

            if (val != null && this.SelectionModel.Primary != null)
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                StringReader sr = new StringReader(val);
                
                if(this.SelectionModel.Primary is RowSelectionModel)
                {
                    SelectedRowCollection ids = (SelectedRowCollection)serializer.Deserialize(sr, typeof(SelectedRowCollection));
                    (this.SelectionModel.Primary as RowSelectionModel).SetSelection(ids);
                } 
                else if(this.SelectionModel.Primary is CellSelectionModel)
                {
                    SelectedCellSerializable cell = (SelectedCellSerializable)serializer.Deserialize(sr, typeof(SelectedCellSerializable));
                    if (cell != null)
                    {
                        CellSelectionModel sm = this.SelectionModel.Primary as CellSelectionModel;
                        sm.SelectedCell.RowIndex = cell.RowIndex;
                        sm.SelectedCell.ColIndex = cell.ColIndex;
                        sm.SelectedCell.RecordID = cell.RecordID;
                        sm.SelectedCell.Name = cell.Name;
                        sm.SelectedCell.Value = cell.Value;
                    }
                }
            }

            return result;
        }
    }
}
