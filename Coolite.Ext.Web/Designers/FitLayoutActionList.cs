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
    public class FitLayoutActionList : WebControlActionList
    {
        protected readonly IDesigner designer;
        public FitLayoutActionList(IDesigner designer)
            : base(designer.Component)
        {
            this.designer = designer;
        }

        private FitLayoutDesigner Designer
        {
            get
            {
                return designer as FitLayoutDesigner;
            }
        }

        public void AddPanel()
        {
            Designer.AddItem(typeof(Panel));
        }

        public void AddTabPanel()
        {
            Designer.AddItem(typeof(TabPanel));
        }

        public void Clear()
        {
            Designer.Clear();
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddHeaderItem(new DesignerActionHeaderItem("Actions", "600"));

            DesignerActionMethodItem addPanel = new DesignerActionMethodItem(this, "AddPanel", "Add Panel", "600");
            DesignerActionMethodItem addTabPanel = new DesignerActionMethodItem(this, "AddTabPanel", "Add TabPanel", "600");
            
            //DesignerActionMethodItem clear = new DesignerActionMethodItem(this, "Clear", "Clear layout", "600", "Remove control from layout");

            if (((FitLayout)this.Control).Items.Count > 0)
            {
                //this.AddMethodItem(clear);
                this.RemoveMethodItem(addPanel);
                this.RemoveMethodItem(addTabPanel);
            }
            else
            {
                this.AddMethodItem(addPanel);
                this.AddMethodItem(addTabPanel);
                //this.RemoveMethodItem(clear);
            }

            return base.GetSortedActionItems();
        }
    }
}