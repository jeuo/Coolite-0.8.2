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
    public class DateFieldActionList : WebControlActionList
    {
        public DateFieldActionList(IComponent component) : base(component) { }

        public DateTime SelectedDate
        {
            get
            {
                return ((DateField)this.Control).SelectedDate;
            }
            set
            {
                this.GetPropertyByName("SelectedDate").SetValue(this.Control, value);
            }
        }

        public string Format
        {
            get
            {
                return ((DateField)this.Control).Format;
            }
            set
            {
                this.GetPropertyByName("Format").SetValue(this.Control, value);
            }
        }

        public bool AllowBlank
        {
            get
            {
                return ((DateField)this.Control).AllowBlank;
            }
            set
            {
                this.GetPropertyByName("AllowBlank").SetValue(this.Control, value);
            }
        }

        public Unit Width
        {
            get
            {
                return ((DateField)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("SelectedDate", "Selected Date", "500", "Change the Selected Date of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("AllowBlank", "AllowBlank", "500", "Ensure a Date has been selected"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Format", "Date Format", "500", "The default date format string"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the control"));
            
            return base.GetSortedActionItems();
        }
    }
}