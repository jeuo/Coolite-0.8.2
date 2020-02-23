/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
    public class UpdateProxyCollection : SingleItemStateCollection<HttpWriteProxy>
    {
        [ClientConfig(JsonMode.Object)]
        public HttpWriteProxy Proxy
        {
            get
            {
                if (this.Count > 0)
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

        protected override void InsertItem(int index, HttpWriteProxy item)
        {
            base.InsertItem(index, item);
            item.Store = Store;
        }

        protected override void SetItem(int index, HttpWriteProxy item)
        {
            base.SetItem(index, item);
            item.Store = Store;
        }
    }
}
