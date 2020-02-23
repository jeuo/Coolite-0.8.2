/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class StateManagedCollection<T> : IList<T>, IStateManager, IList where T : StateManagedItem 
    {
        private Control owner;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The Owner Control for this collection.")]
        public Control Owner
        {
            get
            {
                return this.owner;
            }
            internal set
            {
                this.owner = value;
            }
        }

        protected virtual bool CreateOnLoading
        {
            get
            {
                return false;
            }
        }

        private readonly List<T> items = new List<T>();
        private bool tracking;

        public virtual int IndexOf(T item)
        {
            return this.items.IndexOf(item);
        }

        public virtual void Sort()
        {
            items.Sort();
        }

        public virtual void Sort(Comparison<T> comparison)
        {
            items.Sort(comparison);
        }

        public virtual void Sort(Comparer<T> comparer)
        {
            items.Sort(comparer);
        }

        public virtual void Insert(int index, T item)
        {
            if (this.Owner != null && !(this.Owner is Page))
            {
                item.Owner = this.Owner;
            }
            this.CheckCount();
            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
            this.items.Insert(index, item);
        }

        public virtual void RemoveAt(int index)
        {
            if (this.AfterItemRemove != null)
            {
                this.AfterItemRemove(this.items[index]);
            }
            this.items.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return this.items[index]; }
            set { this.items[index] = value; }
        }

        public virtual void Add(T item)
        {
            if (this.Owner != null && !(this.Owner is Page))
            {
                item.Owner = this.Owner;
            }

            this.CheckCount();
            
            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
            
            this.items.Add(item);
        }

        public virtual void AddRange(IEnumerable<T> collection)
        {
            if (this.Owner != null && !(this.Owner is Page))
            {
                foreach (T item in collection)
                {
                    item.Owner = this.Owner;
                }
            }
            this.CheckCount();
            if (this.AfterItemAdd != null)
            {
                foreach (T item in collection)
                {
                    this.AfterItemAdd(item);
                }
            }
            this.items.AddRange(collection);
        }


        public virtual void Clear()
        {
            this.items.Clear();
        }

        public virtual bool Contains(T item)
        {
            return this.items.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return this.items.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(T item)
        {
            if (this.AfterItemRemove != null)
            {
                this.AfterItemRemove(item);
            }
            return this.items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.items).GetEnumerator();
        }

        public virtual bool IsTrackingViewState
        {
            get { return tracking; }
        }

        public virtual void LoadViewState(object state)
        {
            if (state != null)
            {
                Pair p = state as Pair;
                if (p != null)
                {
                    int count = (int)p.First;
                    object[] savedItems = (object[])p.Second;

                    if(!this.CreateOnLoading)
                    {
                        for (int i = 0; i < this.items.Count; i++)
                        {
                            this.items[i].LoadViewState(savedItems[i]);
                        }
                    }
                    else
                    {
                        items.Clear();
                        foreach (object savedState in savedItems)
                        {
                            //T item = new T();
                            T item = Activator.CreateInstance<T>();
                            item.TrackViewState();
                            item.LoadViewState(savedState);
                            items.Add(item);
                            if (this.AfterItemAdd != null)
                            {
                                this.AfterItemAdd(item);
                            }
                        }
                    }
                    this.CheckCount();
                }
            }
        }

        public virtual object SaveViewState()
        {
            if(this.items.Count == 0)
            {
                return null;
            }
            
            object[] saveList = new object[this.items.Count];
            for (int i = 0; i < this.items.Count; i++)
            {
                T item = this.items[i];
                SetItemDirty(item);
                saveList[i] = item.SaveViewState();
            }
            return new Pair(this.items.Count, saveList);
        }

        public virtual void TrackViewState()
        {
            this.tracking = true;
            foreach (IStateManager item in this.items)
            {
                item.TrackViewState();
            }
        }

        public virtual void SetDirty()
        {
            foreach (T item in this.items)
            {
                SetItemDirty(item);
            }
        }

        protected virtual void SetItemDirty(T item)
        {
            item.SetDirty();
        }

        int IList.Add(object value)
        {
            Add(value as T);
            return Count - 1;
        }

        void IList.Clear()
        {
            Clear();
        }

        bool IList.Contains(object value)
        {
            return Contains(value as T);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf(value as T);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, value as T);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            Remove(value as T);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value as T; }
        }

        void ICollection.CopyTo(Array array, int index) { }

        int ICollection.Count
        {
            get { return Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }

        private bool singleItemMode = false;

        public virtual bool SingleItemMode
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

        private void CheckCount()
        {
            if (this.SingleItemMode && this.items.Count > 0)
            {
                throw new InvalidOperationException("Only one Control allowed in this collection");
            }
        }

        internal delegate void AfterItemAddHandler(T item);
        internal event AfterItemAddHandler AfterItemAdd;

        internal delegate void AfterItemRemoveHandler(T item);
        internal event AfterItemRemoveHandler AfterItemRemove;
    }
}