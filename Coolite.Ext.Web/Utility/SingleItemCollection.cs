/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.ObjectModel;

namespace Coolite.Ext.Web
{
    public class SingleItemCollection<T> : Collection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            this.CheckCount();
            base.InsertItem(index, item);

            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
        }

        protected override void SetItem(int index, T item)
        {
            this.CheckCount();
            base.SetItem(index, item);

            if (this.AfterItemAdd != null)
            {
                this.AfterItemAdd(item);
            }
        }

        protected virtual string ExcessItemsMessage
        {
            get
            {
                return "Only one items available";
            }
        }

        private void CheckCount()
        {
            if (Count > 0)
            {
                throw new ExcessItemsException(ExcessItemsMessage);
            }
        }

        internal delegate void AfterItemAddHandler(T item);
        internal event AfterItemAddHandler AfterItemAdd;
    }

    public class ExcessItemsException : Exception
    {
        public ExcessItemsException(string message) : base(message)
        {
        }
    }
}