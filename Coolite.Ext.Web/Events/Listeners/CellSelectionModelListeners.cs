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
    public class CellSelectionModelListeners : ComponentBaseListeners
    {
        private ComponentListener beforeCellSelect;

        /// <summary>
        /// Fires before a cell is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(CellSelectionModel),"this")]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "colIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecellselect", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a cell is selected.")]
        public virtual ComponentListener BeforeCellSelect
        {
            get
            {
                if (this.beforeCellSelect == null)
                {
                    this.beforeCellSelect = new ComponentListener();
                }
                return this.beforeCellSelect;
            }
        }

        private ComponentListener cellSelect;

        /// <summary>
        /// Fires when a cell is selected.
        /// </summary>
        [ListenerArgument(0, "el", typeof(CellSelectionModel), "this")]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "colIndex")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("cellselect", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a cell is selected.")]
        public virtual ComponentListener CellSelect
        {
            get
            {
                if (this.cellSelect == null)
                {
                    this.cellSelect = new ComponentListener();
                }
                return this.cellSelect;
            }
        }

        private ComponentListener selectionChange;

        /// <summary>
        /// Fires when the active selection changes.
        /// </summary>
        [ListenerArgument(0, "el", typeof(CellSelectionModel), "this")]
        [ListenerArgument(1, "selection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("selectionchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the active selection changes.")]
        public virtual ComponentListener SelectionChange
        {
            get
            {
                if (this.selectionChange == null)
                {
                    this.selectionChange = new ComponentListener();
                }
                return this.selectionChange;
            }
        }
    }
}