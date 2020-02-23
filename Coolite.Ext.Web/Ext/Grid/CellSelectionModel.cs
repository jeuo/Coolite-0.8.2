/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This class provides the basic implementation for single cell selection in a grid.
    /// The object stored as the selection and returned by getSelectedCell contains the following properties:
    ///     record : Ext.data.record
    ///         The Record which provides the data for the row containing the selection
    ///     cell : Ext.data.record
    ///         An object containing the following properties:
    ///              rowIndex : Number
    ///                 The index of the selected row
    ///             cellIndex : Number
    ///                 The index of the selected cell
    ///  Note that due to possible column reordering, the cellIndex should not be used as an index into
    ///  the Record's data. Instead, the name of the selected field should be determined in order to
    ///  retrieve the data value from the record by name:
    ///
    ///     var fieldName = grid.getColumnModel().getDataIndex(cellIndex);
    ///     var data = record.get(fieldName);
    /// </summary>
    [Description("This class provides the basic implementation for single cell selection in a grid. The object stored as the selection and returned by getSelectedCell contains the following properties:")]
    [InstanceOf(ClassName = "Ext.grid.CellSelectionModel")]
    [ToolboxItem(false)]
    public class CellSelectionModel : AbstractSelectionModel
    {
        private CellSelectionModelListeners listeners;

        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        public CellSelectionModelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new CellSelectionModelListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private CellSelectionModelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public CellSelectionModelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new CellSelectionModelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        private SelectedCell selectedCell;

        /// <summary>
        /// Selected cell
        /// </summary>
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ClientConfig("selectedData", JsonMode.Object)]
        [ViewStateMember]
        [Description("Selected cell")]
        public SelectedCell SelectedCell
        {
            get
            {
                if (this.selectedCell == null)
                {
                    this.selectedCell = new SelectedCell();
                }
                return this.selectedCell;
            }
        }

        public void Clear()
        {
            this.SelectedCell.RowIndex = -1;
            this.SelectedCell.ColIndex = -1;
            this.SelectedCell.RecordID = "";
            this.SelectedCell.Name = "";
        }

        public override void UpdateSelection()
        {
            if(this.SelectedCell.RowIndex<0 &&
               this.SelectedCell.ColIndex < 0 &&
               string.IsNullOrEmpty(this.SelectedCell.RecordID) &&
               string.IsNullOrEmpty(this.SelectedCell.Name))
            {
                this.AddScript(string.Concat(this.ClientID,".clearSelections();"));
                this.AddScript(string.Concat(this.ClientID, ".grid.clearMemory();"));
            }
            else
            {
                string sc = new ClientConfig().Serialize(this.SelectedCell);
                this.AddScript(string.Format("{0}.selectedData={1};{0}.grid.doSelection();",this.ClientID, sc));
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Clears all selections.
        /// </summary>
        [Description("Clears all selections.")]
        public virtual void ClearSelections()
        {
            this.AddScript("{0}.clearSelections();", this.ClientID);
        }

        /// <summary>
        /// Clears all selections.
        /// </summary>
        /// <param name="notify">true to prevent the gridview from being notified about the change.</param>
        [Description("Clears all selections.")]
        public virtual void ClearSelections(bool notify)
        {
            this.AddScript("{0}.clearSelections({1});", this.ClientID, notify.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Selects a cell.
        /// </summary>
        /// <param name="rowIndex">The row index of the cell</param>
        /// <param name="collIndex ">The column index of the cell</param>
        [Description("Selects a range of rows. All rows in between startRow and endRow are also selected.")]
        public virtual void Select(int rowIndex, int collIndex)
        {
            this.AddScript("{0}.select({1},{2},{3});", this.ClientID, rowIndex, collIndex);
        }
    }

    public class SelectedCell : StateManagedItem
    {
        public SelectedCell() { }

        public SelectedCell(int rowIndex, int colIndex)
        {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
        }

        public SelectedCell(string recordID, string name)
        {
            this.RecordID = recordID;
            this.Name = name;
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

        [NotifyParentProperty(true)]
        [DefaultValue(-1)]
        [ClientConfig]
        public int ColIndex
        {
            get
            {
                object o = this.ViewState["ColIndex"];
                return o == null ? -1 : (int)o;
            }
            set
            {
                this.ViewState["ColIndex"] = value;
            }
        }

        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [ClientConfig]
        public string RecordID
        {
            get
            {
                object o = this.ViewState["RecordID"];
                return o == null ? "" : (string)o;
            }
            set
            {
                this.ViewState["RecordID"] = value;
            }
        }

        [NotifyParentProperty(true)]
        [DefaultValue("")]
        [ClientConfig]
        public string Name
        {
            get
            {
                object o = this.ViewState["Name"];
                return o == null ? "" : (string)o;
            }
            set
            {
                this.ViewState["Name"] = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string Value
        {
            get
            {
                object o = this.ViewState["Value"];
                return o == null ? null : (string)o;
            }
            internal set
            {
                this.ViewState["Value"] = value;
            }
        }
    }

    public class SelectedCellSerializable : SelectedCell
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubmittedValue
        {
            get { return this.Value; }
            set { this.Value = value; }
        }
    }
}