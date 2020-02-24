/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// The default SelectionModel used by Ext.grid.GridPanel. It supports multiple
    /// selections and keyboard selection/navigation. The objects stored as selections
    /// and returned by getSelected, and getSelections are the Records which provide
    /// the data for the selected rows.
    /// </summary>
    [InstanceOf(ClassName = "Ext.grid.RowSelectionModel")]
    [ToolboxItem(false)]
    [Description("The default SelectionModel used by Ext.grid.Grid. It supports multiple selections and keyboard selection/navigation. The objects stored as selections and returned by getSelected, and getSelections are the Records which provide the data for the selected rows.")]
    public class RowSelectionModel : AbstractSelectionModel
    {
        /// <summary>
        /// True to allow selection of only one row at a time (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow selection of only one row at a time (defaults to false).")]
        public virtual bool SingleSelect
        {
            get
            {
                object obj = this.ViewState["SingleSelect"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SingleSelect"] = value;
            }
        }

        /// <summary>
        /// False to turn off moving the editor to the next cell when the enter key is pressed
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to turn off moving the editor to the next cell when the enter key is pressed.")]
        public virtual bool MoveEditorOnEnter
        {
            get
            {
                object obj = this.ViewState["MoveEditorOnEnter"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MoveEditorOnEnter"] = value;
            }
        }

        private RowSelectionModelListeners listeners;

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
        public RowSelectionModelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new RowSelectionModelListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private RowSelectionModelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public RowSelectionModelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new RowSelectionModelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        private SelectedRowCollection selectedRows;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ClientConfig("selectedData",JsonMode.AlwaysArray)]
        [ViewStateMember]
        public SelectedRowCollection SelectedRows
        {
            get
            {
                if (this.selectedRows == null)
                {
                    this.selectedRows = new SelectedRowCollection();
                }
                return this.selectedRows;
            }
        }

        internal void SetSelection(SelectedRowCollection rows)
        {
            this.selectedRows = rows;
        }

        public SelectedRow SelectedRow
        {
            get
            {
                if (this.SelectedRows.Count > 0)
                {
                    return this.SelectedRows[0];
                }
                return null;

            }
            set
            {
                foreach (var selectedrow in this.SelectedRows)
                {
                    if (selectedrow.RowIndex == value.RowIndex)
                    {
                        return;
                    }
                }
                if (this.SingleSelect)
                {
                    this.SelectedRows.Clear();
                }
                this.SelectedRows.Add(value);
            }
        }

        public int SelectedIndex
        {
            get
            {
                if(this.SelectedRows.Count > 0)
                {
                    return this.SelectedRows[0].RowIndex;
                }
                return -1;
                
            }
            set
            {
                foreach(var selectedrow in this.SelectedRows)
                {
                    if(selectedrow.RowIndex == value)
                    {
                        return;
                    }
                }
                if(this.SingleSelect)
                {
                    this.SelectedRows.Clear();
                }
                this.SelectedRows.Add(new SelectedRow(value));
            }
        }

        public string SelectedRecordID
        {
            get
            {
                if (this.SelectedRows.Count > 0)
                {
                    return this.SelectedRows[0].RecordID;
                }
                return "";
            }
            set
            {
                foreach (var selectedrow in this.SelectedRows)
                {
                    if (selectedrow.RecordID == value)
                    {
                        return;
                    }
                }
                if (this.SingleSelect)
                {
                    this.SelectedRows.Clear();
                }
                this.SelectedRows.Add(new SelectedRow(value));
            }
        }

        public List<string> SelectedRecordIDs
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (var selectedrow in this.SelectedRows)
                {
                    lst.Add(selectedrow.RecordID);
                }
                return lst;
            }
            set
            {
                this.SelectedRows.Clear();
                if (value.Count > 0)
                {
                    if (this.SingleSelect)
                    {
                        this.SelectedRows.Add(new SelectedRow(value[0]));
                    }
                    else
                    {
                        foreach(string recordid in value)
                        {
                            this.SelectedRows.Add(new SelectedRow(recordid));
                        }
                    }
                }
            }
        }

        public List<int> SelectedIndexs
        {
            get
            {
                List<int> lst = new List<int>();
                foreach (var selectedrow in this.SelectedRows)
                {
                    lst.Add(selectedrow.RowIndex);
                }
                return lst;
            }
            set
            {
                this.SelectedRows.Clear();
                if (value.Count > 0)
                {
                    if (this.SingleSelect)
                    {
                        this.SelectedRows.Add(new SelectedRow(value[0]));
                    }
                    else
                    {
                        foreach (int recordid in value)
                        {
                            this.SelectedRows.Add(new SelectedRow(recordid));
                        }
                    }
                }
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        public override void UpdateSelection()
        {
            if (this.SelectedRows.Count == 0)
            {
                this.AddScript(string.Concat(this.ClientID, ".clearSelections();"));
                this.AddScript(string.Concat(this.ClientID, ".grid.clearMemory();"));
            }
            else
            {
                bool comma = false;
                StringBuilder temp = new StringBuilder();
                temp.Append("[");
                foreach (SelectedRow row in this.SelectedRows)
                {
                    if (comma)
                    {
                        temp.Append(",");
                    }

                    temp.Append(new ClientConfig().Serialize(row));

                    comma = true;
                }
                temp.Append("]");
                
                this.AddScript(string.Format("{0}.selectedData={1};{0}.grid.doSelection();", this.ClientID, temp.ToString()));
            }
        }

        /// <summary>
        /// Clears all selections.
        /// </summary>
        [Description("Clears all selections.")]
        public virtual void ClearSelections()
        {
            this.AddScript("{0}.clearSelections();", this.ClientID);
        }

        /// <summary>
        /// Deselects a range of rows. All rows in between startRow and endRow are also deselected.
        /// </summary>
        /// <param name="startRow">The index of the first row in the range</param>
        /// <param name="endRow">The index of the last row in the range</param>
        [Description("Deselects a range of rows. All rows in between startRow and endRow are also deselected.")]
        public virtual void DeselectRange(int startRow, int endRow)
        {
            this.AddScript("{0}.deselectRange({1}, 2{});", this.ClientID, startRow, endRow);
        }

        /// <summary>
        /// Deselects a row.
        /// </summary>
        /// <param name="row">The index of the row to deselect</param>
        [Description("Deselects a row.")]
        public virtual void DeselectRow(int row)
        {
            this.AddScript("{0}.deselectRow({1});", this.ClientID, row);
        }

        /// <summary>
        /// Deselects a row.
        /// </summary>
        [Description("Deselects a row.")]
        public virtual void SelectAll()
        {
            this.AddScript("{0}.selectAll();", this.ClientID);
        }

        /// <summary>
        /// Selects the first row in the grid.
        /// </summary>
        [Description("Selects the first row in the grid.")]
        public virtual void SelectFirstRow()
        {
            this.AddScript("{0}.selectFirstRow();", this.ClientID);
        }

        /// <summary>
        /// Select the last row.
        /// </summary>
        [Description("Select the last row.")]
        public virtual void SelectLastRow()
        {
            this.AddScript("{0}.selectLastRow();", this.ClientID);
        }

        /// <summary>
        /// Select the last row.
        /// </summary>
        /// <param name="keepExisting">True to keep existing selections</param>
        [Description("Select the last row.")]
        public virtual void SelectLastRow(bool keepExisting)
        {
            this.AddScript("{0}.selectLastRow({1});", this.ClientID, keepExisting.ToString().ToLowerInvariant());
        }
        
        /// <summary>
        /// Selects the row immediately following the last selected row.
        /// </summary>
        [Description("Selects the row immediately following the last selected row.")]
        public virtual void SelectNext()
        {
            this.AddScript("{0}.selectNext();", this.ClientID);
        }

        /// <summary>
        /// Selects the row immediately following the last selected row.
        /// </summary>
        /// <param name="keepExisting">True to keep existing selections</param>
        [Description("Selects the row immediately following the last selected row.")]
        public virtual void SelectNext(bool keepExisting)
        {
            this.AddScript("{0}.selectLastRow({1});", this.ClientID, keepExisting.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Selects the row that precedes the last selected row.
        /// </summary>
        [Description("Selects the row that precedes the last selected row.")]
        public virtual void SelectPrevious()
        {
            this.AddScript("{0}.selectPrevious();", this.ClientID);
        }

        /// <summary>
        /// Selects the row that precedes the last selected row.
        /// </summary>
        /// <param name="keepExisting">Selects the row that precedes the last selected row.</param>
        [Description("Selects the row immediately following the last selected row.")]
        public virtual void SelectPrevious(bool keepExisting)
        {
            this.AddScript("{0}.selectPrevious({1});", this.ClientID, keepExisting.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Selects a range of rows. All rows in between startRow and endRow are also selected.
        /// </summary>
        /// <param name="startRow">The index of the first row in the range</param>
        /// <param name="endRow">The index of the last row in the range</param>
        [Description("Selects a range of rows. All rows in between startRow and endRow are also selected.")]
        public virtual void SelectRange(int startRow, int endRow)
        {
            this.AddScript("{0}.selectRange({1},{2});", this.ClientID, startRow, endRow);
        }

        /// <summary>
        /// Selects a range of rows. All rows in between startRow and endRow are also selected.
        /// </summary>
        /// <param name="startRow">The index of the first row in the range</param>
        /// <param name="endRow">The index of the last row in the range</param>
        /// <param name="keepExisting">True to retain existing selections</param>
        [Description("Selects a range of rows. All rows in between startRow and endRow are also selected.")]
        public virtual void SelectRange(int startRow, int endRow, bool keepExisting)
        {
            this.AddScript("{0}.selectRange({1},{2},{3});", this.ClientID, startRow, endRow, keepExisting.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Select records.
        /// </summary>
        /// <param name="records">The records to select</param>
        /// <param name="keepExisting">True to keep existing selections</param>
        [Description("Select records.")]
        public virtual void SelectRecords(int[] records, bool keepExisting)
        {
            this.AddScript("{0}.selectRecords({1},{2});", this.ClientID, JSON.Serialize(records), keepExisting.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Selects a row.
        /// </summary>
        /// <param name="row">The index of the row to select</param>
        [Description("Selects a row.")]
        public virtual void SelectRow(int row)
        {
            this.AddScript("{0}.selectRow({1});", this.ClientID, row);
        }

        /// <summary>
        /// Selects a row.
        /// </summary>
        /// <param name="row">The index of the row to select</param>
        /// <param name="keepExisting">True to keep existing selections</param>
        [Description("Selects a row.")]
        public virtual void SelectRow(int row, bool keepExisting)
        {
            this.AddScript("{0}.selectRow({1},{2});", this.ClientID, row, keepExisting.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Selects multiple rows.
        /// </summary>
        /// <param name="records">Array of the indexes of the row to select</param>
        /// <param name="keepExisting">True to keep existing selections (defaults to false)</param>
        [Description("Selects multiple rows.")]
        public virtual void SelectRows(int[] records, bool keepExisting)
        {
            this.AddScript("{0}.selectRows({1},{2});", this.ClientID, JSON.Serialize(records), keepExisting.ToString().ToLowerInvariant());
        }
    }

    public class SelectedRowCollection : StateManagedCollection<SelectedRow>
    {
        protected override bool CreateOnLoading
        {
            get
            {
                return true;
            }
        }
    }

    public class SelectedRow : StateManagedItem
    {
        public SelectedRow() { }

        public SelectedRow(string recordID)
        {
            this.RecordID = recordID;
        }

        public SelectedRow(int rowIndex)
        {
            this.RowIndex = rowIndex;
        }

        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [ClientConfig]
        public string RecordID
        {
            get
            {
                return (string)this.ViewState["RecordID"] ?? "";
            }
            set
            {
                this.ViewState["RecordID"] = value;
            }
        }

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        [ClientConfig]
        public int RowIndex
        {
            get
            {
                object o = this.ViewState["RowIndex"];
                return o == null ? -1 : (int)o;
            }
            set
            {
                this.ViewState["RowIndex"] = value;
            }
        }
    }
}