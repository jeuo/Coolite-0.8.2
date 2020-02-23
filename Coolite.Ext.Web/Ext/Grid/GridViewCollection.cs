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
    public class GridViewCollection : SingleItemCollection<GridView>
    {
        [ClientConfig(JsonMode.ObjectAllowEmpty)]
        public GridView View
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
    }

    public class GridViewCollectionEditor : CollectionEditor
    {
        public GridViewCollectionEditor(Type type) : base(type) { }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
              {
                typeof(GridView),
                typeof(GroupingView)
              };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(GridView);
        }
    }
}
