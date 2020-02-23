/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class GridPanelAjaxEvents : PanelAjaxEvents
    {
        private ComponentAjaxEvent bodyScroll;

        /// <summary>
        /// Fires when the body element is scrolled.
        /// </summary>
        [ListenerArgument(0, "scrollLeft")]
        [ListenerArgument(1, "scrollTop")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("bodyscroll", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the body element is scrolled.")]
        public virtual ComponentAjaxEvent BodyScroll
        {
            get
            {
                if (this.bodyScroll == null)
                {
                    this.bodyScroll = new ComponentAjaxEvent();
                }
                return this.bodyScroll;
            }
        }

        private ComponentAjaxEvent cellClick;

        /// <summary>
        /// Fires when a cell is clicked. The data for the cell is drawn from the Record for this row.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is clicked. The data for the cell is drawn from the Record for this row.")]
        public virtual ComponentAjaxEvent CellClick
        {
            get
            {
                if (this.cellClick == null)
                {
                    this.cellClick = new ComponentAjaxEvent();
                }
                return this.cellClick;
            }
        }

        private ComponentAjaxEvent cellContextMenu;

        /// <summary>
        /// Fires when a cell is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "cellIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellcontextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is right clicked.")]
        public virtual ComponentAjaxEvent CellContextMenu
        {
            get
            {
                if (this.cellContextMenu == null)
                {
                    this.cellContextMenu = new ComponentAjaxEvent();
                }
                return this.cellContextMenu;
            }
        }

        private ComponentAjaxEvent cellDblClick;

        /// <summary>
        /// Fires when a cell is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("celldblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is double clicked.")]
        public virtual ComponentAjaxEvent CellDblClick
        {
            get
            {
                if (this.cellDblClick == null)
                {
                    this.cellDblClick = new ComponentAjaxEvent();
                }
                return this.cellDblClick;
            }
        }

        private ComponentAjaxEvent cellMouseDown;

        /// <summary>
        /// Fires before a cell is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellMouseDown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a cell is clicked.")]
        public virtual ComponentAjaxEvent CellMouseDown
        {
            get
            {
                if (this.cellMouseDown == null)
                {
                    this.cellMouseDown = new ComponentAjaxEvent();
                }
                return this.cellMouseDown;
            }
        }

        private ComponentAjaxEvent click;

        /// <summary>
        /// The raw click event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw click event for the entire grid.")]
        public virtual ComponentAjaxEvent Click
        {
            get
            {
                if (this.click == null)
                {
                    this.click = new ComponentAjaxEvent();
                }
                return this.click;
            }
        }

        private ComponentAjaxEvent columnMove;

        /// <summary>
        /// Fires when the user moves a column.
        /// </summary>
        [ListenerArgument(0, "oldIndex")]
        [ListenerArgument(1, "newIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnmove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user moves a column.")]
        public virtual ComponentAjaxEvent ColumnMove
        {
            get
            {
                if (this.columnMove == null)
                {
                    this.columnMove = new ComponentAjaxEvent();
                }
                return this.columnMove;
            }
        }

        private ComponentAjaxEvent columnResize;

        /// <summary>
        /// Fires when the user resizes a column.
        /// </summary>
        [ListenerArgument(0, "columnIndex")]
        [ListenerArgument(1, "newSize")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnresize", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user resizes a column.")]
        public virtual ComponentAjaxEvent ColumnResize
        {
            get
            {
                if (this.columnResize == null)
                {
                    this.columnResize = new ComponentAjaxEvent();
                }
                return this.columnResize;
            }
        }

        private ComponentAjaxEvent contextMenu;

        /// <summary>
        /// The raw contextmenu event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw contextmenu event for the entire grid.")]
        public virtual ComponentAjaxEvent ContextMenu
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentAjaxEvent();
                }
                return this.contextMenu;
            }
        }

        private ComponentAjaxEvent dblClick;

        /// <summary>
        /// The raw dblclick event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw dblclick event for the entire grid.")]
        public virtual ComponentAjaxEvent DblClick
        {
            get
            {
                if (this.dblClick == null)
                {
                    this.dblClick = new ComponentAjaxEvent();
                }
                return this.dblClick;
            }
        }

        private ComponentAjaxEvent headerClick;

        /// <summary>
        /// Fires when a header is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header is clicked.")]
        public virtual ComponentAjaxEvent HeaderClick
        {
            get
            {
                if (this.headerClick == null)
                {
                    this.headerClick = new ComponentAjaxEvent();
                }
                return this.headerClick;
            }
        }

        private ComponentAjaxEvent headerContextMenu;

        /// <summary>
        /// Fires when a header is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headercontextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header is right clicked.")]
        public virtual ComponentAjaxEvent HeaderContextMenu
        {
            get
            {
                if (this.headerContextMenu == null)
                {
                    this.headerContextMenu = new ComponentAjaxEvent();
                }
                return this.headerContextMenu;
            }
        }

        private ComponentAjaxEvent headerDblClick;

        /// <summary>
        /// Fires when a header cell is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerdblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header cell is double clicked.")]
        public virtual ComponentAjaxEvent HeaderDblClick
        {
            get
            {
                if (this.headerDblClick == null)
                {
                    this.headerDblClick = new ComponentAjaxEvent();
                }
                return this.headerDblClick;
            }
        }

        private ComponentAjaxEvent headerMouseDown;

        /// <summary>
        /// Fires before a header is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headermousedown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a header is clicked.")]
        public virtual ComponentAjaxEvent HeaderMouseDown
        {
            get
            {
                if (this.headerMouseDown == null)
                {
                    this.headerMouseDown = new ComponentAjaxEvent();
                }
                return this.headerMouseDown;
            }
        }

        private ComponentAjaxEvent keyDown;

        /// <summary>
        /// The raw keydown event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keydown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw keydown event for the entire grid.")]
        public virtual ComponentAjaxEvent KeyDown
        {
            get
            {
                if (this.keyDown == null)
                {
                    this.keyDown = new ComponentAjaxEvent();
                }
                return this.keyDown;
            }
        }

        private ComponentAjaxEvent keyPress;

        /// <summary>
        /// The raw keypress event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keypress", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [Description("The raw keypress event for the entire grid.")]
        public virtual ComponentAjaxEvent KeyPress
        {
            get
            {
                if (this.keyPress == null)
                {
                    this.keyPress = new ComponentAjaxEvent();
                }
                return this.keyPress;
            }
        }

        private ComponentAjaxEvent mouseDown;

        /// <summary>
        /// The raw mousedown event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mousedown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mousedown event for the entire grid.")]
        public virtual ComponentAjaxEvent MouseDown
        {
            get
            {
                if (this.mouseDown == null)
                {
                    this.mouseDown = new ComponentAjaxEvent();
                }
                return this.mouseDown;
            }
        }

        private ComponentAjaxEvent mouseOut;

        /// <summary>
        /// The raw mouseout event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseout", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseout event for the entire grid.")]
        public virtual ComponentAjaxEvent MouseOut
        {
            get
            {
                if (this.mouseOut == null)
                {
                    this.mouseOut = new ComponentAjaxEvent();
                }
                return this.mouseOut;
            }
        }

        private ComponentAjaxEvent mouseOver;

        /// <summary>
        /// The raw mouseover event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseover event for the entire grid.")]
        public virtual ComponentAjaxEvent MouseOver
        {
            get
            {
                if (this.mouseOver == null)
                {
                    this.mouseOver = new ComponentAjaxEvent();
                }
                return this.mouseOver;
            }
        }

        private ComponentAjaxEvent mouseUp;

        /// <summary>
        /// The raw mouseup event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseup", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseup event for the entire grid.")]
        public virtual ComponentAjaxEvent MouseUp
        {
            get
            {
                if (this.mouseUp == null)
                {
                    this.mouseUp = new ComponentAjaxEvent();
                }
                return this.mouseUp;
            }
        }

        private ComponentAjaxEvent rowClick;

        /// <summary>
        /// Fires when a row is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is clicked.")]
        public virtual ComponentAjaxEvent RowClick
        {
            get
            {
                if (this.rowClick == null)
                {
                    this.rowClick = new ComponentAjaxEvent();
                }
                return this.rowClick;
            }
        }

        private ComponentAjaxEvent rowContextMenu;

        /// <summary>
        /// Fires when a row is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowcontextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is right clicked.")]
        public virtual ComponentAjaxEvent RowContextMenu
        {
            get
            {
                if (this.rowContextMenu == null)
                {
                    this.rowContextMenu = new ComponentAjaxEvent();
                }
                return this.rowContextMenu;
            }
        }

        private ComponentAjaxEvent rowDblClick;

        /// <summary>
        /// Fires when a row is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowdblclick", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is double clicked.")]
        public virtual ComponentAjaxEvent RowDblClick
        {
            get
            {
                if (this.rowDblClick == null)
                {
                    this.rowDblClick = new ComponentAjaxEvent();
                }
                return this.rowDblClick;
            }
        }

        private ComponentAjaxEvent rowMouseDown;

        /// <summary>
        /// Fires before a row is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowmousedown", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row is clicked.")]
        public virtual ComponentAjaxEvent RowMouseDown
        {
            get
            {
                if (this.rowMouseDown == null)
                {
                    this.rowMouseDown = new ComponentAjaxEvent();
                }
                return this.rowMouseDown;
            }
        }

        private ComponentAjaxEvent sortChange;

        /// <summary>
        /// Fires when the grid's store sort changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "sortInfo")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("sortchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the grid's store sort changes.")]
        public virtual ComponentAjaxEvent SortChange
        {
            get
            {
                if (this.sortChange == null)
                {
                    this.sortChange = new ComponentAjaxEvent();
                }
                return this.sortChange;
            }
        }

        private ComponentAjaxEvent afterEdit;

        /// <summary>
        /// Fires after a cell is edited.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afteredit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a cell is edited.")]
        public virtual ComponentAjaxEvent AfterEdit
        {
            get
            {
                if (this.afterEdit == null)
                {
                    this.afterEdit = new ComponentAjaxEvent();
                }
                return this.afterEdit;
            }
        }

        private ComponentAjaxEvent beforeEdit;

        /// <summary>
        /// Fires before cell editing is triggered.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeedit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before cell editing is triggered.")]
        public virtual ComponentAjaxEvent BeforeEdit
        {
            get
            {
                if (this.beforeEdit == null)
                {
                    this.beforeEdit = new ComponentAjaxEvent();
                }
                return this.beforeEdit;
            }
        }

        private ComponentAjaxEvent validateEdit;

        /// <summary>
        /// Fires after a cell is edited, but before the value is set in the record. Return false to cancel the change.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("validateedit", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a cell is edited, but before the value is set in the record. Return false to cancel the change.")]
        public virtual ComponentAjaxEvent ValidateEdit
        {
            get
            {
                if (this.validateEdit == null)
                {
                    this.validateEdit = new ComponentAjaxEvent();
                }
                return this.validateEdit;
            }
        }

        private ComponentAjaxEvent command;

        /// <summary>
        /// Fires when the command is clicked.
        /// </summary>
        [ListenerArgument(0, "command")]
        [ListenerArgument(1, "record")]
        [ListenerArgument(2, "rowIndex")]
        [ListenerArgument(3, "colIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("command", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the command is clicked.")]
        public virtual ComponentAjaxEvent Command
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new ComponentAjaxEvent();
                }
                return this.command;
            }
        }

        private ComponentAjaxEvent groupCommand;

        /// <summary>
        /// Fires when the group command is clicked.
        /// </summary>
        [ListenerArgument(0, "command")]
        [ListenerArgument(1, "groupId")]
        [ListenerArgument(2, "records")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("groupcommand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the group command is clicked.")]
        public virtual ComponentAjaxEvent GroupCommand
        {
            get
            {
                if (this.groupCommand == null)
                {
                    this.groupCommand = new ComponentAjaxEvent();
                }
                return this.groupCommand;
            }
        }

        private ComponentAjaxEvent filterUpdate;

        /// <summary>
        /// Fires when the grid's filter is updated.
        /// </summary>
        [ListenerArgument(0, "filtersPlugin")]
        [ListenerArgument(1, "filter")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("filterupdate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the grid's filter is updated.")]
        public virtual ComponentAjaxEvent FilterUpdate
        {
            get
            {
                if (this.filterUpdate == null)
                {
                    this.filterUpdate = new ComponentAjaxEvent();
                }
                return this.filterUpdate;
            }
        }
    }
}