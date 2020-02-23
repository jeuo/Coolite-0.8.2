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

namespace Coolite.Ext.Web
{
    public class LabelActionList : WebControlActionList
    {
        public LabelActionList(IComponent component) : base(component) { }

        public string Text
        {
            get
            {
                return ((Label)this.Control).Text;
            }
            set
            {
                this.GetPropertyByName("Text").SetValue(this.Control, value);
            }
        }

        public string Html
        {
            get
            {
                return ((Label)this.Control).Html;
            }
            set
            {
                this.GetPropertyByName("Html").SetValue(this.Control, value);
            }
        }

        public string Cls
        {
            get
            {
                return ((Label)this.Control).Cls;
            }
            set
            {
                this.GetPropertyByName("Cls").SetValue(this.Control, value);
            }
        }

        public string StyleSpec
        {
            get
            {
                return ((Label)this.Control).StyleSpec;
            }
            set
            {
                this.GetPropertyByName("StyleSpec").SetValue(this.Control, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            this.AddPropertyItem(new DesignerActionPropertyItem("Text", "Text", "500", "Set the text of the Label."));
            this.AddPropertyItem(new DesignerActionPropertyItem("Html", "Html", "500", "Set the text of the Label."));
            //this.AddPropertyItem(new DesignerActionPropertyItem("Cls", "Class", "500", "Set the class of the Label."));
            //this.AddPropertyItem(new DesignerActionPropertyItem("StyleSpec", "Style", "500", "Set the style of the Label."));

            return base.GetSortedActionItems();
        }
    }
}
