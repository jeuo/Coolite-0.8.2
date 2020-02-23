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
    public class FieldSetActionList : WebControlActionList
    {
        public FieldSetActionList(IComponent component) : base(component) { }

        public bool CheckboxToggle
        {
            get
            {
                return ((FieldSet)this.Control).CheckboxToggle;
            }
            set
            {
                this.GetPropertyByName("CheckboxToggle").SetValue(this.Control, value);
            }
        }

        public bool Collapsible
        {
            get
            {
                return ((FieldSet)this.Control).Collapsible;
            }
            set
            {
                this.GetPropertyByName("Collapsible").SetValue(this.Control, value);
            }
        }

        public string Title
        {
            get
            {
                return ((FieldSet)this.Control).Title;
            }
            set
            {
                this.GetPropertyByName("Title").SetValue(this.Control, value);
            }
        }

        public Unit Height
        {
            get
            {
                return ((FieldSet)this.Control).Height;
            }
            set
            {
                this.GetPropertyByName("Height").SetValue(this.Control, value);
            }
        }

        public Unit Width
        {
            get
            {
                return ((FieldSet)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("CheckboxToggle", "Checkbox Toggle", "500", "Add a checkbox into the fieldset frame just in from of the legend."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Collapsible", "Collapsible", "500", "Make the fieldset collapsible and have the expand/collapse toggle button automatically rendered into the legend"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Title", "Title", "500", "Change the Title of the FieldSet"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Height", "Height", "500", "Change the Height of the control"));

            return base.GetSortedActionItems();
        }
    }
}