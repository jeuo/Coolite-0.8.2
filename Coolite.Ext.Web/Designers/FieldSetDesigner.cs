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
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;

namespace Coolite.Ext.Web
{
    public class FieldSetDesigner : PanelDesigner
    {
        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            EditableDesignerRegion region = new EditableDesignerRegion(this, "Body", false);
            regions.Add(region);

            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            FieldSet c = (FieldSet)this.Control;

            string width = string.Format(" width: {0};", c.Width.ToString());
            string height = string.Format(" height: {0}px;", (c.Height.Value - 39).ToString());

            string buttons = string.Empty;
            buttons += (c.CheckboxToggle && !c.Collapsible) ? "<input name=\"ext-comp-1002-checkbox\" type=\"checkbox\">" : string.Empty;
            buttons += (c.Collapsible && !c.CheckboxToggle) ? "<div class=\"x-tool x-tool-toggle\">&nbsp;</div>" : string.Empty;
            
            /*
             * 0 - ClientID
             * 1 - Title
             * 2 - Width
             * 3 - Height
             * 4 - Buttons
             * 5 - BodyStyle
             */

            object[] args = new object[6];
            args[0] = c.ClientID;
            args[1] = c.Title;
            args[2] = width;
            args[3] = height;
            args[4] = buttons;
            args[5] = c.BodyStyle;

            LiteralControl topCtrl = new LiteralControl(string.Format(this.HtmlBegin, args));
            topCtrl.RenderControl(htmlWriter);

            HtmlGenericControl div = (HtmlGenericControl)c.BodyContainer;
            div.Attributes[DesignerRegion.DesignerRegionAttributeName] = "0";
            div.InnerHtml = this.GetEditableDesignerRegionContent(region);
            div.RenderControl(htmlWriter);

            LiteralControl bottomCtrl = new LiteralControl(this.HtmlEnd);
            bottomCtrl.RenderControl(htmlWriter);

            return writer.ToString();
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new FieldSetActionList(this.Component));
                }
                return actionLists;
            }
        }

        /*
         * 0 - ClientID
         * 1 - Title
         * 2 - Width
         * 3 - Height
         * 4 - Buttons
         * 5 - BodyStyle
         */

        public override string HtmlBegin
        {
            get
            {
                return
@"<fieldset style=""{2}"" class=""x-fieldset"">
		<legend style=""-moz-user-select: none;"" class=""x-fieldset-header x-unselectable"">
			{4}
			<span class=""x-fieldset-header-text"">{1}</span>
		</legend>
		<div class=""x-fieldset-bwrap"">
			<div style=""{5}{3}"" class=""x-fieldset-body"">";
            }
        }

        public override string HtmlEnd
        {
            get
            {
                return
@"		</div>
	</div>
</fieldset>";
            }
        }
    }
}