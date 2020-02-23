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
    public class GridPanelListeners : PanelListeners
    {
        private ComponentListener bodyScroll;

        /// <summary>
        /// Fires when the body element is scrolled.
        /// </summary>
        [ListenerArgument(0, "scrollLeft")]
        [ListenerArgument(1, "scrollTop")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("bodyscroll", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the body element is scrolled.")]
        public virtual ComponentListener BodyScroll
        {
            get
            {
                if (this.bodyScroll == null)
                {
                    this.bodyScroll = new ComponentListener();
                }
                return this.bodyScroll;
            }
        }

        private ComponentListener cellClick;

        /// <summary>
        /// Fires when a cell is clicked. The data for the cell is drawn from the Record for this row.
        /// </summary>
        [ListenerArgument(0, "el",typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is clicked. The data for the cell is drawn from the Record for this row.")]
        public virtual ComponentListener CellClick
        {
            get
            {
                if (this.cellClick == null)
                {
                    this.cellClick = new ComponentListener();
                }
                return this.cellClick;
            }
        }

        private ComponentListener cellContextMenu;

        /// <summary>
        /// Fires when a cell is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "cellIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellcontextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is right clicked.")]
        public virtual ComponentListener CellContextMenu
        {
            get
            {
                if (this.cellContextMenu == null)
                {
                    this.cellContextMenu = new ComponentListener();
                }
                return this.cellContextMenu;
            }
        }

        private ComponentListener cellDblClick;

        /// <summary>
        /// Fires when a cell is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("celldblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is double clicked.")]
        public virtual ComponentListener CellDblClick
        {
            get
            {
                if (this.cellDblClick == null)
                {
                    this.cellDblClick = new ComponentListener();
                }
                return this.cellDblClick;
            }
        }

        private ComponentListener cellMouseDown;

        /// <summary>
        /// Fires before a cell is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "columnIndex")]
        [ListenerArgument(3, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellMouseDown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a cell is clicked.")]
        public virtual ComponentListener CellMouseDown
        {
            get
            {
                if (this.cellMouseDown == null)
                {
                    this.cellMouseDown = new ComponentListener();
                }
                return this.cellMouseDown;
            }
        }

        private ComponentListener click;

        /// <summary>
        /// The raw click event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("click", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw click event for the entire grid.")]
        public virtual ComponentListener Click
        {
            get
            {
                if (this.click == null)
                {
                    this.click = new ComponentListener();
                }
                return this.click;
            }
        }

        private ComponentListener columnMove;

        /// <summary>
        /// Fires when the user moves a column.
        /// </summary>
        [ListenerArgument(0, "oldIndex")]
        [ListenerArgument(1, "newIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnmove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user moves a column.")]
        public virtual ComponentListener ColumnMove
        {
            get
            {
                if (this.columnMove == null)
                {
                    this.columnMove = new ComponentListener();
                }
                return this.columnMove;
            }
        }

        private ComponentListener columnResize;

        /// <summary>
        /// Fires when the user resizes a column.
        /// </summary>
        [ListenerArgument(0, "columnIndex")]
        [ListenerArgument(1, "newSize")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("columnresize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the user resizes a column.")]
        public virtual ComponentListener ColumnResize
        {
            get
            {
                if (this.columnResize == null)
                {
                    this.columnResize = new ComponentListener();
                }
                return this.columnResize;
            }
        }

        private ComponentListener contextMenu;

        /// <summary>
        /// The raw contextmenu event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw contextmenu event for the entire grid.")]
        public virtual ComponentListener ContextMenu
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentListener();
                }
                return this.contextMenu;
            }
        }

        private ComponentListener dblClick;

        /// <summary>
        /// The raw dblclick event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw dblclick event for the entire grid.")]
        public virtual ComponentListener DblClick
        {
            get
            {
                if (this.dblClick == null)
                {
                    this.dblClick = new ComponentListener();
                }
                return this.dblClick;
            }
        }

        private ComponentListener headerClick;

        /// <summary>
        /// Fires when a header is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header is clicked.")]
        public virtual ComponentListener HeaderClick
        {
            get
            {
                if (this.headerClick == null)
                {
                    this.headerClick = new ComponentListener();
                }
                return this.headerClick;
            }
        }

        private ComponentListener headerContextMenu;

        /// <summary>
        /// Fires when a header is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headercontextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header is right clicked.")]
        public virtual ComponentListener HeaderContextMenu
        {
            get
            {
                if (this.headerContextMenu == null)
                {
                    this.headerContextMenu = new ComponentListener();
                }
                return this.headerContextMenu;
            }
        }

        private ComponentListener headerDblClick;

        /// <summary>
        /// Fires when a header cell is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headerdblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a header cell is double clicked.")]
        public virtual ComponentListener HeaderDblClick
        {
            get
            {
                if (this.headerDblClick == null)
                {
                    this.headerDblClick = new ComponentListener();
                }
                return this.headerDblClick;
            }
        }

        private ComponentListener headerMouseDown;

        /// <summary>
        /// Fires before a header is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "columnIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("headermousedown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a header is clicked.")]
        public virtual ComponentListener HeaderMouseDown
        {
            get
            {
                if (this.headerMouseDown == null)
                {
                    this.headerMouseDown = new ComponentListener();
                }
                return this.headerMouseDown;
            }
        }

        private ComponentListener keyDown;

        /// <summary>
        /// The raw keydown event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keydown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw keydown event for the entire grid.")]
        public virtual ComponentListener KeyDown
        {
            get
            {
                if (this.keyDown == null)
                {
                    this.keyDown = new ComponentListener();
                }
                return this.keyDown;
            }
        }

        private ComponentListener keyPress;

        /// <summary>
        /// The raw keypress event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("keypress", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [Description("The raw keypress event for the entire grid.")]
        public virtual ComponentListener KeyPress
        {
            get
            {
                if (this.keyPress == null)
                {
                    this.keyPress = new ComponentListener();
                }
                return this.keyPress;
            }
        }

        private ComponentListener mouseDown;

        /// <summary>
        /// The raw mousedown event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mousedown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mousedown event for the entire grid.")]
        public virtual ComponentListener MouseDown
        {
            get
            {
                if (this.mouseDown == null)
                {
                    this.mouseDown = new ComponentListener();
                }
                return this.mouseDown;
            }
        }

        private ComponentListener mouseOut;

        /// <summary>
        /// The raw mouseout event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseout", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseout event for the entire grid.")]
        public virtual ComponentListener MouseOut
        {
            get
            {
                if (this.mouseOut == null)
                {
                    this.mouseOut = new ComponentListener();
                }
                return this.mouseOut;
            }
        }

        private ComponentListener mouseOver;

        /// <summary>
        /// The raw mouseover event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseover", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseover event for the entire grid.")]
        public virtual ComponentListener MouseOver
        {
            get
            {
                if (this.mouseOver == null)
                {
                    this.mouseOver = new ComponentListener();
                }
                return this.mouseOver;
            }
        }

        private ComponentListener mouseUp;

        /// <summary>
        /// The raw mouseup event for the entire grid.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("mouseup", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The raw mouseup event for the entire grid.")]
        public virtual ComponentListener MouseUp
        {
            get
            {
                if (this.mouseUp == null)
                {
                    this.mouseUp = new ComponentListener();
                }
                return this.mouseUp;
            }
        }

        private ComponentListener rowClick;

        /// <summary>
        /// Fires when a row is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is clicked.")]
        public virtual ComponentListener RowClick
        {
            get
            {
                if (this.rowClick == null)
                {
                    this.rowClick = new ComponentListener();
                }
                return this.rowClick;
            }
        }

        private ComponentListener rowContextMenu;

        /// <summary>
        /// Fires when a row is right clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowcontextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is right clicked.")]
        public virtual ComponentListener RowContextMenu
        {
            get
            {
                if (this.rowContextMenu == null)
                {
                    this.rowContextMenu = new ComponentListener();
                }
                return this.rowContextMenu;
            }
        }

        private ComponentListener rowDblClick;

        /// <summary>
        /// Fires when a row is double clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowdblclick", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a row is double clicked.")]
        public virtual ComponentListener RowDblClick
        {
            get
            {
                if (this.rowDblClick == null)
                {
                    this.rowDblClick = new ComponentListener();
                }
                return this.rowDblClick;
            }
        }

        private ComponentListener rowMouseDown;

        /// <summary>
        /// Fires before a row is clicked.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowmousedown", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a row is clicked.")]
        public virtual ComponentListener RowMouseDown
        {
            get
            {
                if (this.rowMouseDown == null)
                {
                    this.rowMouseDown = new ComponentListener();
                }
                return this.rowMouseDown;
            }
        }

        private ComponentListener sortChange;

        /// <summary>
        /// Fires when the grid's store sort changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridPanel))]
        [ListenerArgument(1, "sortInfo")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("sortchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the grid's store sort changes.")]
        public virtual ComponentListener SortChange
        {
            get
            {
                if (this.sortChange == null)
                {
                    this.sortChange = new ComponentListener();
                }
                return this.sortChange;
            }
        }

        private ComponentListener afterEdit;

        /// <summary>
        /// Fires after a cell is edited.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afteredit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a cell is edited.")]
        public virtual ComponentListener AfterEdit
        {
            get
            {
                if (this.afterEdit == null)
                {
                    this.afterEdit = new ComponentListener();
                }
                return this.afterEdit;
            }
        }

        private ComponentListener beforeEdit;

        /// <summary>
        /// Fires after a cell is edited.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeedit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before cell editing is triggered.")]
        public virtual ComponentListener BeforeEdit
        {
            get
            {
                if (this.beforeEdit == null)
                {
                    this.beforeEdit = new ComponentListener();
                }
                return this.beforeEdit;
            }
        }

        private ComponentListener validateEdit;

        /// <summary>
        /// Fires after a cell is edited.
        /// </summary>
        [ListenerArgument(0, "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("validateedit", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a cell is edited, but before the value is set in the record. Return false to cancel the change.")]
        public virtual ComponentListener ValidateEdit
        {
            get
            {
                if (this.validateEdit == null)
                {
                    this.validateEdit = new ComponentListener();
                }
                return this.validateEdit;
            }
        }

        private ComponentListener command;

        /// <summary>
        /// Fires when the command is clicked.
        /// </summary>
        [ListenerArgument(0, "command")]
        [ListenerArgument(1, "record")]
        [ListenerArgument(2, "rowIndex")]
        [ListenerArgument(3, "colIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("command", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the command is clicked.")]
        public virtual ComponentListener Command
        {
            get
            {
                if (this.command == null)
                {
                    this.command = new ComponentListener();
                }
                return this.command;
            }
        }

        private ComponentListener groupCommand;

        /// <summary>
        /// Fires when the group command is clicked.
        /// </summary>
        [ListenerArgument(0, "command")]
        [ListenerArgument(1, "groupId")]
        [ListenerArgument(2, "records")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("groupcommand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the group command is clicked.")]
        public virtual ComponentListener GroupCommand
        {
            get
            {
                if (this.groupCommand == null)
                {
                    this.groupCommand = new ComponentListener();
                }
                return this.groupCommand;
            }
        }

        private ComponentListener filterUpdate;

        /// <summary>
        /// Fires when the grid's filter is updated.
        /// </summary>
        [ListenerArgument(0, "filtersPlugin")]
        [ListenerArgument(1, "filter")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("filterupdate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the grid's filter is updated.")]
        public virtual ComponentListener FilterUpdate
        {
            get
            {
                if (this.filterUpdate == null)
                {
                    this.filterUpdate = new ComponentListener();
                }
                return this.filterUpdate;
            }
        }
    }
}