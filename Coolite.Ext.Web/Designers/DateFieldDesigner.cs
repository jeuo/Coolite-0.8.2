/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class DateFieldDesigner : WebControlDesigner
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
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            DateField c = (DateField)this.Control;

            string width = (c.Width != Unit.Empty) ? string.Format(" width: {0};", c.Width.ToString()) : string.Empty;
            string height = (c.Height != Unit.Empty) ? string.Format(" height: {0};", c.Height.ToString()) : string.Empty;

            object[] args = new object[9];
            args[0] = c.ClientID;

            if (c.SelectedDate != DateTime.MinValue && c.SelectedDate != DateTime.MaxValue)
            {
                args[1] = c.SelectedDate.ToString(c.Format);
            }
            else
            {
                args[1] = "";
            }
            
            args[2] = width;
            args[3] = height;
            args[4] = c.StyleSpec;
            args[5] = "x-form-text x-form-field " + ((c.IsNull) ? "x-form-empty-field " : string.Empty) + c.Cls;
            args[6] = "x-form-trigger x-form-date-trigger " + c.TriggerClass;

            ScriptManager sm = c.ScriptManager;

            if (sm != null)
            {
                args[7] = c.ScriptManager.BLANK_IMAGE_URL;
            }
            else
            {
                args[7] = "";
            }
            args[8] = string.Format(" width: {0};", (c.Width != Unit.Empty) ? (c.Width.Value + 20) + "px" : "144px");

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
                 * 6 - Image Class
                 * 7 - Image Url
                 * 8 - Wrapper Width
                 */
                return @"<div style=""{8}"" class=""x-form-field-wrap""><input value=""{1}"" type=""text"" style=""{4}{3}{2}"" class=""{5}"" /><img src=""{7}"" class=""{6}""></div>";
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
                    actionLists.Add(new DateFieldActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}