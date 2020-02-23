/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class RadioActionList : WebControlActionList
    {
        public RadioActionList(IComponent component) : base(component) { }
        
        public bool Checked
        {
            get
            {
                return ((Radio)this.Control).Checked;
            }
            set
            {
                this.GetPropertyByName("Checked").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Checked", "Checked", "500", "Change the Radio to Checked"));
    
            return base.GetSortedActionItems();
        }
    }
}