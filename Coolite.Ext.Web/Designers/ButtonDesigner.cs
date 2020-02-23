/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class ButtonDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            return BuildButton((ButtonBase)this.Control);
        }

        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }

        public string BuildButton(ButtonBase button)
        {
            this.AddIcon(button.Icon);

            object[] args = new object[7];
            args[0] = button.EnableToggle && button.Pressed ? pressed : "";
            args[1] = !string.IsNullOrEmpty(button.IconCls) ? textIcon : "";
            args[2] = button.Disabled ? disabled : "";
            args[3] = button.Text;
            args[4] = button.IconCls;
            args[5] = this.GetIconStyleBlock();
            args[6] = button.StyleSpec;

            return string.Format(buttonTemplate, args);
        }

        private const string buttonTemplate = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: auto;{6}\" class=\"x-btn-wrap x-btn{0}{1}{2}\"><tbody><tr><td class=\"x-btn-left\">{5}<i>&nbsp;</i></td><td class=\"x-btn-center\"><em unselectable=\"on\"><button class=\"x-btn-text {4}\" style=\"width:auto;\" type=\"button\">{3}</button></em></td><td class=\"x-btn-right\"><i>&nbsp;</i></td></tr></tbody></table>";
        
        private const string pressed = " x-btn-pressed";
        private const string textIcon = " x-btn-text-icon";
        private const string disabled = " x-items-disabled";

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ButtonActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}