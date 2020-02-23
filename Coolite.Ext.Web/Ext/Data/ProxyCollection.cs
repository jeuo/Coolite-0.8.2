/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class ProxyCollection : SingleItemStateCollection<DataProxy>
    {
        [ClientConfig(JsonMode.ObjectAllowEmpty)]
        public DataProxy Proxy
        {
            get
            {
                if(this.Count>0)
                {
                    return this[0];
                }

                return null;
            }
        }

        private StoreBase store;

        internal StoreBase Store
        {
            get { return store; }
            set { store = value; }
        }

        protected override void InsertItem(int index, DataProxy item)
        {
            base.InsertItem(index, item);
            item.Store = Store;
        }

        protected override void SetItem(int index, DataProxy item)
        {
            base.SetItem(index, item);
            item.Store = Store;
        }
    }

    public class ProxyCollectionEditor : CollectionEditor
    {
        public ProxyCollectionEditor(Type type) : base(type) { }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
              {
                typeof(HttpProxy),
                typeof(ScriptTagProxy)
              };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(HttpProxy);
        }
    }
}
