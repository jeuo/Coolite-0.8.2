/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;

namespace Coolite.Ext.Web
{
    public class BorderLayoutActionList : WebControlActionList
    {
        public BorderLayoutActionList(IDesigner designer) : base(designer.Component)
        {
            this.designer = designer;
        }

        private readonly IDesigner designer;

        private BorderLayoutDesigner Designer
        {
            get
            {
                return designer as BorderLayoutDesigner;
            }
        }

        public bool SchemeMode
        {
            get
            {
                return (bool)this.GetPropertyByName("SchemeMode").GetValue(this.Control);
            }
            set
            {
                this.GetPropertyByName("SchemeMode").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("SchemeMode", "Show layout as scheme", "500"));

            return base.GetSortedActionItems();
        }
    }
}