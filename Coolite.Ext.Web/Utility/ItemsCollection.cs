/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class ItemsCollection<T> : List<T> where T : Observable
    {
        new public void Add(T item)
        {
            this.CheckItem(item);
            base.Add(item);

            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
        }

        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);

            foreach (T item in collection)
            {
                if (this.AfterItemAdd != null)
                {
                    this.AfterItemAdd(item);
                }
            }
        }

        new public void Insert(int index, T item)
        {
            this.CheckItem(item);
            base.Insert(index, item);

            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
        }

        new public void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);

            foreach (T item in collection)
            {
                if (this.AfterItemAdd != null)
                {
                    this.AfterItemAdd(item);
                }
            }
        }

        new public void Clear()
        {
            foreach (T item in this)
            {
                if (this.AfterItemRemove != null)
                {
                    this.AfterItemRemove(item);
                }
            }
            base.Clear();
        }

        new public void Remove(T item)
        {
            base.Remove(item);

            if (this.AfterItemRemove != null)
            {
                this.AfterItemRemove(item);
            }
        }

        private void CheckItem(T item)
        {
            if (this.SingleItemMode && this.Count > 0)
            {
                throw new InvalidOperationException("Only one Control allowed in this collection");
            }

            item.CancelRenderToParameter = true;

            if (item is ViewPort || item is Window)
            {
                throw new InvalidCastException(string.Format("Invalid type of Control ({0}). This type can not be added to Items collection", item.GetType()));
            }
        }

        private bool singleItemMode = false;

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

        internal delegate void AfterItemAddHandler(T item);
        internal event AfterItemAddHandler AfterItemAdd;

        internal delegate void AfterItemRemoveHandler(T item);
        internal event AfterItemRemoveHandler AfterItemRemove;
    }

    public class ItemTCollectionEditor : CollectionEditor
    {
        public ItemTCollectionEditor(Type type) : base(type) { }

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