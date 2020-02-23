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
    public class PanelActionList : WebControlActionList
    {
        public PanelActionList(IComponent component) : base(component) { }

        public bool Collapsed
        {
            get
            {
                return ((Panel)this.Control).Collapsed;
            }
            set
            {
                this.GetPropertyByName("Collapsed").SetValue(this.Control, value);
            }
        }

        public bool Collapsible
        {
            get
            {
                return ((Panel)this.Control).Collapsible;
            }
            set
            {
                this.GetPropertyByName("Collapsible").SetValue(this.Control, value);
            }
        }

        public bool Border
        {
            get
            {
                return ((Panel)this.Control).Border;
            }
            set
            {
                this.GetPropertyByName("Border").SetValue(this.Control, value);
            }
        }

        public bool Frame
        {
            get
            {
                return ((Panel)this.Control).Frame;
            }
            set
            {
                this.GetPropertyByName("Frame").SetValue(this.Control, value);
            }
        }

        public string Title
        {
            get
            {
                return ((Panel)this.Control).Title;
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
                return ((Panel)this.Control).Height;
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
                return ((Panel)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public string BodyStyle
        {
            get
            {
                return ((PanelBase)this.Control).BodyStyle;
            }
            set
            {
                this.GetPropertyByName("BodyStyle").SetValue(this.Control, value);
            }
        }

        public Icon Icon
        {
            get
            {
                return ((PanelBase)this.Control).Icon;
            }
            set
            {
                this.GetPropertyByName("Icon").SetValue(this.Control, value);
            }
        }

        private string padding = "padding: 6px;";
        private bool paddingAdded = false;

        public void AddPadding()
        {
            string style = ((PanelBase)this.Control).BodyStyle;
            if (!this.paddingAdded || string.IsNullOrEmpty(style))
            {
                this.paddingAdded = true;
                this.GetPropertyByName("BodyStyle").SetValue(this.Control, style + padding);
            }
        }

        public void RemovePadding()
        {
            string style = ((PanelBase)this.Control).BodyStyle;
            if (this.paddingAdded || style.Contains(padding))
            {
                this.paddingAdded = false;
                this.GetPropertyByName("BodyStyle").SetValue(this.Control, style.Replace(padding, string.Empty));
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Border", "Border", "500", "Adds/Removes Border from Panel."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Collapsible", "Collapsible", "500", "Enable the Panel to be collapsible."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Collapsed", "Collapsed on Page Load", "500", "Set the Panel to be collapsed when the Page first loads."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Frame", "Frame", "500", "Render with custom rounded borders."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Title", "Title", "500", "Change the Title of the Panel"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Height", "Height", "500", "Change the Height of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Icon", "Icon", "500", "Set an icon to use in the Title bar"));
            this.AddPropertyItem(new DesignerActionPropertyItem("BodyStyle", "BodyStyle", "500", "Custom CSS styles to be applied to the body element"));

            DesignerActionMethodItem add = new DesignerActionMethodItem(this, "AddPadding", "Add Body Padding", "500", "Add 6px of padding to the Body");
            DesignerActionMethodItem remove = new DesignerActionMethodItem(this, "RemovePadding", "Remove Body Padding", "500", "Remove the 6px of padding from the Body");

            if (!this.paddingAdded && !((PanelBase)this.Control).BodyStyle.Contains(this.padding))
            {
                this.AddMethodItem(add);
                this.RemoveMethodItem(remove);
            }
            else
            {
                this.AddMethodItem(remove);
                this.RemoveMethodItem(add);
            }
           
            return base.GetSortedActionItems();
        }
    }
}