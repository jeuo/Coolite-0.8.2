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
    public class WindowActionList : WebControlActionList
    {
        public WindowActionList(IComponent component) : base(component) { }

        public bool CenterOnLoad
        {
            get
            {
                return ((Window)this.Control).CenterOnLoad;
            }
            set
            {
                this.GetPropertyByName("CenterOnLoad").SetValue(this.Control, value);
            }
        }

        public bool ShowOnLoad
        {
            get
            {
                return ((Window)this.Control).ShowOnLoad;
            }
            set
            {
                this.GetPropertyByName("ShowOnLoad").SetValue(this.Control, value);
            }
        }

        public bool Closable
        {
            get
            {
                return ((Window)this.Control).Closable;
            }
            set
            {
                this.GetPropertyByName("Closable").SetValue(this.Control, value);
            }
        }

        public bool Collapsible
        {
            get
            {
                return ((Window)this.Control).Collapsible;
            }
            set
            {
                this.GetPropertyByName("Collapsible").SetValue(this.Control, value);
            }
        }

        public bool Maximizable
        {
            get
            {
                return ((Window)this.Control).Maximizable;
            }
            set
            {
                this.GetPropertyByName("Maximizable").SetValue(this.Control, value);
            }
        }

        public bool Modal
        {
            get
            {
                return ((Window)this.Control).Modal;
            }
            set
            {
                this.GetPropertyByName("Modal").SetValue(this.Control, value);
            }
        }

        public CloseAction CloseAction
        {
            get
            {
                return ((Window)this.Control).CloseAction;
            }
            set
            {
                this.GetPropertyByName("CloseAction").SetValue(this.Control, value);
            }
        }

        public string Title
        {
            get
            {
                return ((Window)this.Control).Title;
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
                return ((Window)this.Control).Height;
            }
            set
            {
                this.GetPropertyByName("Height").SetValue(this.Control, value);
            }
        }

        public Unit MinHeight
        {
            get
            {
                return ((Window)this.Control).MinHeight;
            }
            set
            {
                this.GetPropertyByName("MinHeight").SetValue(this.Control, value);
            }
        }

        public Unit Width
        {
            get
            {
                return ((Window)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public Unit MinWidth
        {
            get
            {
                return ((Window)this.Control).MinWidth;
            }
            set
            {
                this.GetPropertyByName("MinWidth").SetValue(this.Control, value);
            }
        }

        public Unit PageX
        {
            get
            {
                return ((Window)this.Control).PageX;
            }
            set
            {
                this.GetPropertyByName("PageX").SetValue(this.Control, value);
            }
        }

        public Unit PageY
        {
            get
            {
                return ((Window)this.Control).PageY;
            }
            set
            {
                this.GetPropertyByName("PageY").SetValue(this.Control, value);
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
            this.AddHeaderItem(new DesignerActionHeaderItem("Positioning", "600"));
            this.AddHeaderItem(new DesignerActionHeaderItem("Actions", "700"));

            this.AddPropertyItem(new DesignerActionPropertyItem("Title", "Title", "500", "Change the Title of the Window"));
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

            this.AddPropertyItem(new DesignerActionPropertyItem("ShowOnLoad", "Show on Page Load", "600", "Automatically show the window on Page Load"));
            this.AddPropertyItem(new DesignerActionPropertyItem("CenterOnLoad", "Center on Page Load", "600", "Centers this window in the viewport on Page Load"));

            DesignerActionPropertyItem pageX = new DesignerActionPropertyItem("PageX", "PageX", "600", "The position in pixels from the left of the view port (browser window)");
            DesignerActionPropertyItem pageY = new DesignerActionPropertyItem("PageY", "PageY", "600", "The position in pixels from the addToStart of the view port (browser window)");

            if (this.CenterOnLoad)
            {
                this.RemovePropertyItem(pageX);
                this.RemovePropertyItem(pageY);
            }
            else
            {
                this.AddPropertyItem(pageX);
                this.AddPropertyItem(pageY);
            }
            
            this.AddPropertyItem(new DesignerActionPropertyItem("Collapsible", "Collapsible", "700", "Allow the user to collapse the window"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Closable", "Closable", "700", "Allow the user to close the window"));

            DesignerActionPropertyItem closeAction = new DesignerActionPropertyItem("CloseAction", "Close Action", "700", "'Close' to destroy the Window, 'Hide' to simply hide the window");
            DesignerActionPropertyItem maximizable = new DesignerActionPropertyItem("Maximizable", "Maximizable", "700", "Allow the user to maximize the window");
            DesignerActionPropertyItem modal = new DesignerActionPropertyItem("Modal", "Modal", "700", "Make the window modal and mask everything behind it when displayed");

            this.RemovePropertyItem(closeAction);
            this.RemovePropertyItem(maximizable);
            this.RemovePropertyItem(modal);

            if (this.Closable)
            {
                this.AddPropertyItem(closeAction);
                this.AddPropertyItem(maximizable);
                this.AddPropertyItem(modal);
            }
            else
            {
                this.RemovePropertyItem(closeAction);
                this.AddPropertyItem(maximizable);
                this.AddPropertyItem(modal);
            }

            return base.GetSortedActionItems();
        }
    }
}