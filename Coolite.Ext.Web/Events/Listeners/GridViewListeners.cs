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
    public class GridViewListeners : ComponentBaseListeners
    {
        private ComponentListener beforeRefresh;

        /// <summary>
        /// Internal UI Event. Fired before the view is refreshed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerefresh", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before the view is refreshed.")]
        public virtual ComponentListener BeforeRefresh
        {
            get
            {
                if (this.beforeRefresh == null)
                {
                    this.beforeRefresh = new ComponentListener();
                }
                return this.beforeRefresh;
            }
        }

        private ComponentListener beforeRowRemoved;

        /// <summary>
        /// Internal UI Event. Fired before a row is removed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerowremoved", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before a row is removed.")]
        public virtual ComponentListener BeforeRowRemoved
        {
            get
            {
                if (this.beforeRowRemoved == null)
                {
                    this.beforeRowRemoved = new ComponentListener();
                }
                return this.beforeRowRemoved;
            }
        }

        private ComponentListener beforeRowsInserted;

        /// <summary>
        /// Internal UI Event. Fired before rows are inserted.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "firstRow")]
        [ListenerArgument(2, "lastRow")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerowsinserted", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before rows are inserted.")]
        public virtual ComponentListener BeforeRowsInserted
        {
            get
            {
                if (this.beforeRowsInserted == null)
                {
                    this.beforeRowsInserted = new ComponentListener();
                }
                return this.beforeRowsInserted;
            }
        }

        private ComponentListener refresh;

        /// <summary>
        /// Internal UI Event. Fired after the GridView's body has been refreshed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("refresh", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after the GridView's body has been refreshed.")]
        public virtual ComponentListener Refresh
        {
            get
            {
                if (this.refresh == null)
                {
                    this.refresh = new ComponentListener();
                }
                return this.refresh;
            }
        }

        private ComponentListener rowRemoved;

        /// <summary>
        /// Internal UI Event. Fired after a row is removed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowremoved", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after a row is removed.")]
        public virtual ComponentListener RowRemoved
        {
            get
            {
                if (this.rowRemoved == null)
                {
                    this.rowRemoved = new ComponentListener();
                }
                return this.rowRemoved;
            }
        }

        private ComponentListener rowsInserted;

        /// <summary>
        /// Internal UI Event. Fired after rows are inserted.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "firstRow")]
        [ListenerArgument(2, "lastRow")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowsinserted", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after rows are inserted.")]
        public virtual ComponentListener RowsInserted
        {
            get
            {
                if (this.rowsInserted == null)
                {
                    this.rowsInserted = new ComponentListener();
                }
                return this.rowsInserted;
            }
        }

        private ComponentListener rowUpdated;

        /// <summary>
        /// Internal UI Event. Fired after a row has been updated.
        /// </summary>
        [ListenerArgument(0, "el", typeof(GridView))]
        [ListenerArgument(1, "rowIndex")]
        [ListenerArgument(2, "record")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("rowupdated", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired after a row has been updated.")]
        public virtual ComponentListener RowUpdated
        {
            get
            {
                if (this.rowUpdated == null)
                {
                    this.rowUpdated = new ComponentListener();
                }
                return this.rowUpdated;
            }
        }
    }
}