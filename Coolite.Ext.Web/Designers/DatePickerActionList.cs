/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class DatePickerActionList : WebControlActionList
    {
        public DatePickerActionList(IComponent component) : base(component) { }

        public DateTime SelectedDate
        {
            get
            {
                return ((DatePicker)this.Control).SelectedDate;
            }
            set
            {
                this.GetPropertyByName("SelectedDate").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("SelectedDate", "Selected Date", "500", "Change the Selected Date of the control"));
            
            return base.GetSortedActionItems();
        }
    }
}