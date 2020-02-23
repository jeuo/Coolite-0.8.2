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
    public class HtmlEditorActionList : WebControlActionList
    {
        public HtmlEditorActionList(IComponent component) : base(component) { }

        public bool EnableAlignments
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableAlignments;
            }
            set
            {
                this.GetPropertyByName("EnableAlignments").SetValue(this.Control, value);
            }
        }

        public bool EnableColors
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableColors;
            }
            set
            {
                this.GetPropertyByName("EnableColors").SetValue(this.Control, value);
            }
        }

        public bool EnableFont
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableFont;
            }
            set
            {
                this.GetPropertyByName("EnableFont").SetValue(this.Control, value);
            }
        }

        public bool EnableFontSize
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableFontSize;
            }
            set
            {
                this.GetPropertyByName("EnableFontSize").SetValue(this.Control, value);
            }
        }

        public bool EnableFormat
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableFormat;
            }
            set
            {
                this.GetPropertyByName("EnableFormat").SetValue(this.Control, value);
            }
        }

        public bool EnableLinks
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableLinks;
            }
            set
            {
                this.GetPropertyByName("EnableLinks").SetValue(this.Control, value);
            }
        }

        public bool EnableLists
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableLists;
            }
            set
            {
                this.GetPropertyByName("EnableLists").SetValue(this.Control, value);
            }
        }

        public bool EnableSourceEdit
        {
            get
            {
                return ((HtmlEditor)this.Control).EnableSourceEdit;
            }
            set
            {
                this.GetPropertyByName("EnableSourceEdit").SetValue(this.Control, value);
            }
        }

        public Unit Height
        {
            get
            {
                return ((HtmlEditor)this.Control).Height;
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
                return ((HtmlEditor)this.Control).Width;
            }
            set
            {
                this.GetPropertyByName("Width").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableFont", "Enable Fonts", "500", "Enable font selection. Not available in Safari."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableFormat", "Enable Formatting", "500", "Enable the bold, italic and underline buttons."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableFontSize", "Enable FontSize", "500", "Enable the increase/decrease font size buttons."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableColors", "Enable Colors", "500", "Enable the fore/highlight color buttons."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableAlignments", "Enable Alignments", "500", "Enable the left, center, right alignment buttons."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableLinks", "Enable Hyperlinks", "500", "Enable the create link button."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableLists", "Enable Lists", "500", "Enable the bullet and numbered list buttons."));
            this.AddPropertyItem(new DesignerActionPropertyItem("EnableSourceEdit", "Enable Source Editing", "500", "Enable the switch to source edit button."));

            this.AddPropertyItem(new DesignerActionPropertyItem("Width", "Width", "500", "Change the Width of the control"));
            this.AddPropertyItem(new DesignerActionPropertyItem("Height", "Height", "500", "Change the Height of the control"));

            return base.GetSortedActionItems();
        }
    }
}