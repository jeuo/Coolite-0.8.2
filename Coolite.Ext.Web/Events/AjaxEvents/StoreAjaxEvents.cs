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
    [TypeConverter(typeof(AjaxEventsConverter))]
    public class StoreAjaxEvents : ComponentBaseAjaxEvents
    {
        private ComponentAjaxEvent add;

        /// <summary>
        /// Fires when Records have been added to the Store
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "records", typeof(object), "The array of Records added")]
        [ListenerArgument(1, "index", typeof(object), "The index at which the record(s) were added")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("add", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when Records have been added to the Store")]
        public virtual ComponentAjaxEvent Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new ComponentAjaxEvent();
                }
                return this.add;
            }
        }

        private ComponentAjaxEvent beforeLoad;

        /// <summary>
        /// Fires before a request is made for a new data object. If the beforeload handler returns false the load action will be canceled
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "options", typeof(object), "The loading options that were specified (see load for details)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeload", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a request is made for a new data object. If the beforeload handler returns false the load action will be canceled")]
        public virtual ComponentAjaxEvent BeforeLoad
        {
            get
            {
                if (this.beforeLoad == null)
                {
                    this.beforeLoad = new ComponentAjaxEvent();
                }
                return this.beforeLoad;
            }
        }

        private ComponentAjaxEvent clear;

        /// <summary>
        /// Fires when the data cache has been cleared.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("clear", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the data cache has been cleared.")]
        public virtual ComponentAjaxEvent Clear
        {
            get
            {
                if (this.clear == null)
                {
                    this.clear = new ComponentAjaxEvent();
                }
                return this.clear;
            }
        }

        private ComponentAjaxEvent dataChanged;

        /// <summary>
        /// Fires when the data cache has changed, and a widget which is using this Store as a Record cache should refresh its view.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("datachanged", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the data cache has changed, and a widget which is using this Store as a Record cache should refresh its view.")]
        public virtual ComponentAjaxEvent DataChanged
        {
            get
            {
                if (this.dataChanged == null)
                {
                    this.dataChanged = new ComponentAjaxEvent();
                }
                return this.dataChanged;
            }
        }

        private ComponentAjaxEvent load;

        /// <summary>
        /// Fires after a new set of Records has been loaded.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "records", typeof(Store), "The Records that were loaded")]
        [ListenerArgument(2, "options", typeof(Store), "The loading options that were specified (see load for details)")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("load", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after a new set of Records has been loaded.")]
        public virtual ComponentAjaxEvent Load
        {
            get
            {
                if (this.load == null)
                {
                    this.load = new ComponentAjaxEvent();
                }
                return this.load;
            }
        }

        private ComponentAjaxEvent loadException;

        /// <summary>
        /// Fires if an exception occurs in the Proxy during loading. Called with the signature of the Proxy's \"loadexception\" event.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "options", typeof(DataProxy), "options")]
        [ListenerArgument(2, "response", typeof(DataProxy), "response")]
        [ListenerArgument(3, "e", typeof(DataProxy), "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("loadexception", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires if an exception occurs in the Proxy during loading. Called with the signature of the Proxy's \"loadexception\" event.")]
        public virtual ComponentAjaxEvent LoadException
        {
            get
            {
                if (this.loadException == null)
                {
                    this.loadException = new ComponentAjaxEvent();
                }
                return this.loadException;
            }
        }

        private ComponentAjaxEvent metaChange;

        /// <summary>
        /// Fires when this store's reader provides new metadata (fields). This is currently only supported for JsonReaders.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "meta", typeof(Store), "The JSON metadata")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("metachange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this store's reader provides new metadata (fields). This is currently only supported for JsonReaders.")]
        public virtual ComponentAjaxEvent MetaChange
        {
            get
            {
                if (this.metaChange == null)
                {
                    this.metaChange = new ComponentAjaxEvent();
                }
                return this.metaChange;
            }
        }

        private ComponentAjaxEvent remove;

        /// <summary>
        /// Fires when a Record has been removed from the Store
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "records", typeof(object), "The Record that was removed")]
        [ListenerArgument(2, "index", typeof(object), "The index at which the record was removed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a Record has been removed from the Store")]
        public virtual ComponentAjaxEvent Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new ComponentAjaxEvent();
                }
                return this.remove;
            }
        }

        private ComponentAjaxEvent update;

        /// <summary>
        /// Fires when a Record has been updated
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "records", typeof(object), "The Record that was updated")]
        [ListenerArgument(2, "operation", typeof(object), "The update operation being performed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("update", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when a Record has been updated")]
        public virtual ComponentAjaxEvent Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new ComponentAjaxEvent();
                }
                return this.update;
            }
        }

        private ComponentAjaxEvent beforeSave;

        /// <summary>
        /// Fires before a network request is made to save a data object. If the beforesave handler returns false the save action will be canceled
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "params", typeof(object), "The saving params that were specified")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforesave", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a network request is made to save a data object. If the beforesave handler returns false the save action will be canceled")]
        public virtual ComponentAjaxEvent BeforeSave
        {
            get
            {
                if (this.beforeSave == null)
                {
                    this.beforeSave = new ComponentAjaxEvent();
                }
                return this.beforeSave;
            }
        }

        private ComponentAjaxEvent save;

        /// <summary>
        /// Fires before the save method's callback is called.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "o", typeof(DataProxy), "o")]
        [ListenerArgument(2, "arg", typeof(DataProxy), "arg")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("save", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the save method's callback is called.")]
        public virtual ComponentAjaxEvent Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new ComponentAjaxEvent();
                }
                return this.save;
            }
        }

        private ComponentAjaxEvent saveException;

        /// <summary>
        /// Fires if an exception occurs in the Proxy during writing.
        /// </summary>
        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "o", typeof(DataProxy), "o")]
        [ListenerArgument(2, "response", typeof(DataProxy), "response")]
        [ListenerArgument(3, "e", typeof(DataProxy), "e")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("saveexception", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires if an exception occurs in the Proxy during writing.")]
        public virtual ComponentAjaxEvent SaveException
        {
            get
            {
                if (this.saveException == null)
                {
                    this.saveException = new ComponentAjaxEvent();
                }
                return this.saveException;
            }
        }

        private ComponentAjaxEvent commitDone;

        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "options", typeof(DataProxy), "options")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("commitdone", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent CommitDone
        {
            get
            {
                if (this.commitDone == null)
                {
                    this.commitDone = new ComponentAjaxEvent();
                }
                return this.commitDone;
            }
        }

        private ComponentAjaxEvent commitFailed;

        [ListenerArgument(0, "store", typeof(Store), "this")]
        [ListenerArgument(1, "msg", typeof(DataProxy), "error message")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("commitfailed", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent CommitFailed
        {
            get
            {
                if (this.commitFailed == null)
                {
                    this.commitFailed = new ComponentAjaxEvent();
                }
                return this.commitFailed;
            }
        }
    }
}