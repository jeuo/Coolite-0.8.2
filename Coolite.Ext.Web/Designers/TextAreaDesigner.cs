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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class TextAreaDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            TextArea c = (TextArea)this.Control;

            string width = (c.Width != Unit.Empty) ? string.Format(" width: {0};", c.Width.ToString()) : string.Empty;
            string height = (c.Height != Unit.Empty) ? string.Format(" height: {0};", c.Height.ToString()) : string.Empty;

            object[] args = new object[7];
            args[0] = c.ClientID;
            args[1] = (string.IsNullOrEmpty(c.Text)) ? c.EmptyText : c.Text;
            args[2] = width;
            args[3] = height;
            args[4] = c.StyleSpec;
            args[5] = "x-form-textarea x-form-field " + ((string.IsNullOrEmpty(c.Text)) ? "x-form-empty-field " : string.Empty) + c.Cls;

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
                 * 1 - Text
                 * 2 - Width
                 * 3 - Height
                 * 4 - Style
                 * 5 - Class
                 * 6 - InputType
                 */
                return @"<textarea style=""{4}{3}{2}"" class=""{5}"">{1}</textarea>";
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
                    actionLists.Add(new TextAreaActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}