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
    public class ButtonActionList : WebControlActionList
    {
        public ButtonActionList(IComponent component) : base(component) { }

        public bool AutoPostBack
        {
            get
            {
                return ((Button)this.Control).AutoPostBack;
            }
            set
            {
                this.GetPropertyByName("AutoPostBack").SetValue(this.Control, value);
            }
        }

        public string Text
        {
            get
            {
                return ((ButtonBase)this.Control).Text;
            }
            set
            {
                this.GetPropertyByName("Text").SetValue(this.Control, value);
            }
        }

        public Icon Icon
        {
            get
            {
                return ((ButtonBase)this.Control).Icon;
            }
            set
            {
                this.GetPropertyByName("Icon").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("AutoPostBack", "AutoPostBack", "500", "Set the Button to AutoPostBack when clicked."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Text", "Text", "500", "Set the text of the Button."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Icon", "Icon", "500", "Set an icon to use in the Title bar"));

            return base.GetSortedActionItems();
        }
    }
}