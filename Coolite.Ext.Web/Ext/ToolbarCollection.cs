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
    public class ToolbarCollection : ItemsCollection<ToolbarBase>
    {
        [ClientConfig(JsonMode.Object)]
        public ToolbarBase Toolbar
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

        public ToolbarCollection()
        {
            this.SingleItemMode = true;
        }
    }

    public class ToolbarCollectionEditor : CollectionEditor
    {
        public ToolbarCollectionEditor(Type type) : base(type) { }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
              {
                typeof(Toolbar),
                typeof(PagingToolbar)
              };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(Toolbar);
        }
    }
}
