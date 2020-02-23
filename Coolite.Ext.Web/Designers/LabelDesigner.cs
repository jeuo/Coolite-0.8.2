/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel.Design;
using System.Text;

namespace Coolite.Ext.Web
{
    public class LabelDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            Label l = (Label) this.Control;
            
            if(!string.IsNullOrEmpty(l.Text))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<span");

                if (!string.IsNullOrEmpty(l.Cls))
                {
                    sb.Append(" class=\"").Append(l.Cls).Append("\"");
                }

                if (!string.IsNullOrEmpty(l.StyleSpec))
                {
                    sb.Append(" style=\"").Append(l.StyleSpec).Append("\"");
                }

                sb.Append(">");
                sb.Append(l.Text);
                sb.Append("</span>");

                return sb.ToString();
            }
            
            return !string.IsNullOrEmpty(l.Html) ? l.Html : "[No Text]";
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new LabelActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}
