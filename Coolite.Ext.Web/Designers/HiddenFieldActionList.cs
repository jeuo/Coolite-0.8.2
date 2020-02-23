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
    public class HiddenFieldActionList : WebControlActionList
    {
        public HiddenFieldActionList(IComponent component) : base(component) { }
        
        public string Value
        {
            get
            {
                return (string)((Hidden)this.Control).Value;
            }
            set
            {
                this.GetPropertyByName("Value").SetValue(this.Control, value);
            }
        }

        public bool HideInDesign
        {
            get
            {
                return ((Hidden)this.Control).HideInDesign;
            }
            set
            {
                this.GetPropertyByName("HideInDesign").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Value", "Value", "500", "Set the Value of this Hidden"));
            this.AddPropertyItem(new DesignerActionPropertyItem("HideInDesign", "Hide", "500", "Hide this control at Design Time"));

            return base.GetSortedActionItems();
        }
    }
}