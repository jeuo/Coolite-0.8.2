/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class HiddenFieldDesigner : WebControlDesigner
    {
        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }

        public override string GetDesignTimeHtml()
        {
            if (((Hidden)this.Control).HideInDesign)
            {
                return string.Empty;
            }
            return base.CreatePlaceHolderDesignTimeHtml();
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new HiddenFieldActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}