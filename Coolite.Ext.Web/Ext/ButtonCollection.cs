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
    public class ButtonCollection : Collection<Button>
    {
        protected override void InsertItem(int index, Button item)
        {
            //item.Slave = true;
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, Button item)
        {
            //item.Slave = true;
            base.SetItem(index, item);
        }
    }

    public class ButtonCollectionEditor : CollectionEditor
    {
        public ButtonCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
              {
                typeof(Button)
                //typeof(Button),
                //typeof(SplitButton)
              };
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(Button);
        }
    }
}