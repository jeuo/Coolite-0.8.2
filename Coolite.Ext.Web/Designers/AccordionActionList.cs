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
    public class AccordionActionList : WebControlActionList
    {
        protected readonly IDesigner designer;
        public AccordionActionList(IDesigner designer)
            : base(designer.Component)
        {
            this.designer = designer;
        }

        private AccordionDesigner Designer
        {
            get
            {
                return designer as AccordionDesigner;
            }
        }

        public void AddPanel()
        {
            Designer.AddPanel();
        }

        public void RemovePanel()
        {
            Designer.RemovePanel();
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddHeaderItem(new DesignerActionHeaderItem("Actions", "600"));

            DesignerActionMethodItem add = new DesignerActionMethodItem(this, "AddPanel", "Add Panel", "600");
            DesignerActionMethodItem remove = new DesignerActionMethodItem(this, "RemovePanel", "Remove panel", "600", "Remove active panel");
            Accordion layout = (Accordion)this.Control;
            this.AddMethodItem(add);
            if (layout.ExpandedPanelIndex > -1)
            {
                this.AddMethodItem(remove);
            }
            else
            {
                this.RemoveMethodItem(remove);
            }

            return base.GetSortedActionItems();
        }
    }
}