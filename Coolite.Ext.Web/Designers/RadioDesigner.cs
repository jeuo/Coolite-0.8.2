/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class RadioDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            Radio c = (Radio)this.Control;

            object[] args = new object[7];
            args[0] = c.ClientID;
            args[1] = c.Checked.ToString().ToLower();
            args[5] = c.StyleSpec;
            args[6] = "x-form-radio x-form-field " + c.Cls;
            
            LiteralControl ctrl = new LiteralControl(string.Format(this.Html, args));
            ctrl.RenderControl(htmlWriter);

            return writer.ToString();
        }

        public virtual string Html
        {
            get
            {
                /*
                 * 0 - ClientID
                 * 1 - Checked
                 * 2 - Style
                 * 3 - Class
                 */
                return @"<input checked=""{1}"" style=""{2}"" class=""{3}"" type=""radio"">";
            }
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new RadioActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}