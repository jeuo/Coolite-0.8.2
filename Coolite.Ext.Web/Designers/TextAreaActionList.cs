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
    public class TextAreaActionList : WebControlActionList
    {
        public TextAreaActionList(IComponent component) : base(component) { }

        public string Text
        {
            get
            {
                return ((TextArea)this.Control).Text;
            }
            set
            {
                this.GetPropertyByName("Text").SetValue(this.Control, value);
            }
        }

        public string EmptyText
        {
            get
            {
                return ((TextArea)this.Control).EmptyText;
            }
            set
            {
                this.GetPropertyByName("EmptyText").SetValue(this.Control, value);
            }
        }

        public Unit Width
        {
            get
            {
                return ((TextArea)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public Unit Height
        {
            get
            {
                return ((TextArea)this.Control).Height;
            }
            set
            {
                this.GetPropertyByName("Height").SetValue(this.Control, value);
            }
        }

        public bool Grow
        {
            get
            {
                return ((TextArea)this.Control).Grow;
            }
            set
            {
                this.GetPropertyByName("Grow").SetValue(this.Control, value);
            }
        }

        public bool AllowBlank
        {
            get
            {
                return ((TextArea)this.Control).AllowBlank;
            }
            set
            {
                this.GetPropertyByName("AllowBlank").SetValue(this.Control, value);
            }
        }

        public Unit GrowMin
        {
            get
            {
                return ((TextArea)this.Control).GrowMin;
            }
            set
            {
                this.GetPropertyByName("GrowMin").SetValue(this.Control, value);
            }
        }

        public Unit GrowMax
        {
            get
            {
                return ((TextArea)this.Control).GrowMax;
            }
            set
            {
                this.GetPropertyByName("GrowMax").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Text", "Text", "500", "Text to display if the field is empty"));
            this.AddPropertyItem(new DesignerActionPropertyItem("EmptyText", "Empty Text", "500", "Change the Text of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("AllowBlank", "AllowBlank", "500", "Ensure the length of the text is > 0"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the TextArea"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Height", "Height", "500", "Change the Height of the TextArea"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Grow", "Grow", "500", "Change the TextArea to automatically grow and shrink to its content"));
            
            DesignerActionPropertyItem growmin = new DesignerActionPropertyItem("GrowMin", "Grow Min Size", "500", "The minimum width to allow when grow = true");
            DesignerActionPropertyItem growmax = new DesignerActionPropertyItem("GrowMax", "Grow Max Size", "500", "The maximum width to allow when grow = true");

            if (((TextArea)this.Control).Grow)
            {
                this.AddPropertyItem(growmin);
                this.AddPropertyItem(growmax);
            }
            else
            {
                this.RemovePropertyItem(growmin);
                this.RemovePropertyItem(growmax);
            }

            return base.GetSortedActionItems();
        }
    }
}