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
    public class GridViewAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent beforeRefresh;

        /// <summary>
        /// Internal UI Event. Fired before the view is refreshed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerefresh", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before the view is refreshed.")]
        public virtual ComponentAjaxEvent BeforeRefresh
        {
            get
            {
                if (this.beforeRefresh == null)
                {
                    this.beforeRefresh = new ComponentAjaxEvent();
                }
                return this.beforeRefresh;
            }
        }

        private ComponentAjaxEvent beforeRowRemoved;

        /// <summary>
        /// Internal UI Event. Fired before a row is removed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerowremoved", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before a row is removed.")]
        public virtual ComponentAjaxEvent BeforeRowRemoved
        {
            get
            {
                if (this.beforeRowRemoved == null)
                {
                    this.beforeRowRemoved = new ComponentAjaxEvent();
                }
                return this.beforeRowRemoved;
            }
        }

        private ComponentAjaxEvent beforeRowsInserted;

        /// <summary>
        /// Internal UI Event. Fired before rows are inserted.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "firstRow")]
        [ListenerArgument(2, "lastRow")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerowsinserted", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before rows are inserted.")]
        public virtual ComponentAjaxEvent BeforeRowsInserted
        {
            get
            {
                if (this.beforeRowsInserted == null)
                {
                    this.beforeRowsInserted = new ComponentAjaxEvent();
                }
                return this.beforeRowsInserted;
            }
        }

        private ComponentAjaxEvent refresh;

        /// <summary>
        /// Internal UI Event. Fired after the GridView's body has been refreshed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("refresh", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after the GridView's body has been refreshed.")]
        public virtual ComponentAjaxEvent Refresh
        {
            get
            {
                if (this.refresh == null)
                {
                    this.refresh = new ComponentAjaxEvent();
                }
                return this.refresh;
            }
        }

        private ComponentAjaxEvent rowRemoved;

        /// <summary>
        /// Internal UI Event. Fired after a row is removed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowremoved", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after a row is removed.")]
        public virtual ComponentAjaxEvent RowRemoved
        {
            get
            {
                if (this.rowRemoved == null)
                {
                    this.rowRemoved = new ComponentAjaxEvent();
                }
                return this.rowRemoved;
            }
        }

        private ComponentAjaxEvent rowsInserted;

        /// <summary>
        /// Internal UI Event. Fired after rows are inserted.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "firstRow")]
        [ListenerArgument(2, "lastRow")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowsinserted", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after rows are inserted.")]
        public virtual ComponentAjaxEvent RowsInserted
        {
            get
            {
                if (this.rowsInserted == null)
                {
                    this.rowsInserted = new ComponentAjaxEvent();
                }
                return this.rowsInserted;
            }
        }

        private ComponentAjaxEvent rowUpdated;

        /// <summary>
        /// Internal UI Event. Fired after a row has been updated.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowupdated", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after a row has been updated.")]
        public virtual ComponentAjaxEvent RowUpdated
        {
            get
            {
                if (this.rowUpdated == null)
                {
                    this.rowUpdated = new ComponentAjaxEvent();
                }
                return this.rowUpdated;
            }
        }
    }
}