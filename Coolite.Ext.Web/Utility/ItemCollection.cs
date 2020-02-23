/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class ItemCollection : Collection<Component>
    {
        protected override void InsertItem(int index, Component item)
        {
            this.CheckItem(item);
            base.InsertItem(index, item);
            if(this.AfterInsert != null)
            {
                this.AfterInsert(item); 
            }
        }

        protected override void SetItem(int index, Component item)
        {
            this.CheckItem(item);
            base.SetItem(index, item);
            if (this.AfterInsert != null)
            {
                this.AfterInsert(item);
            }
        }

        private void CheckItem(Component item)
        {
            if(this.SingleItemMode && this.Count>0)
            {
                throw new InvalidOperationException("Only one Component allowed in the Items Collection");
            }
            
            item.CancelRenderToParameter = true;
            if(item is ViewPort || item is Window)
            {
                throw new InvalidCastException(string.Format("Invalid type of Control ({0}). This type can not be added to Items Collection", item.GetType()));
            }
        }

        private bool singleItemMode;
        public bool SingleItemMode
        {
            get 
            { 
                return this.singleItemMode; 
            }
            internal set
            {
                this.singleItemMode = value;
            }
        }

        internal delegate void AfterInsertHandler(Component item);
        internal event AfterInsertHandler AfterInsert;
    }

    public class ItemCollectionEditor : CollectionEditor
    {
        public ItemCollectionEditor(Type type) : base(type) {}

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
              {
                typeof(Panel),
                typeof(TabPanel)
              };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(Panel);
        }
    }
}