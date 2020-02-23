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
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;

namespace Coolite.Ext.Web
{
    public class WindowDesigner : PanelDesigner
    {
        private DesignerRegionCollection designerRegions;
        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            Window c = (Window)this.Control;
            designerRegions = regions;

            EditableDesignerRegion region = new EditableDesignerRegion(this, "Body", false);
            regions.Add(region);

            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            string width = string.Format(" width: {0};", c.Width.ToString());
            string height = string.Format(" height: {0}px;", (c.Height.Value - 30).ToString());

            string buttons = string.Empty;
            buttons += (c.Closable) ? "<div class=\"x-tool x-tool-close\">&nbsp;</div>" : string.Empty;
            buttons += (c.Maximizable) ? "<div class=\"x-tool x-tool-maximize\">&nbsp;</div>" : string.Empty;
            buttons += (c.Minimizable) ? "<div class=\"x-tool x-tool-minimize\">&nbsp;</div>" : string.Empty;
            buttons += (c.Collapsible) ? "<a {0}><div class=\"x-tool x-tool-toggle\">&nbsp;</div></a>" : string.Empty;


            if (c.Collapsible)
            {
                buttons = string.Format(buttons, GetDesignerRegionAttribute(WindowClickAction.Toggle));
            }
            else
            {
                // for prevent shifting regions
                designerRegions.Add(new DesignerRegion(this.CurrentDesigner, "Empty", false));
            }

            /*
             * 0 - ClientID
             * 1 - Title
             * 2 - Width
             * 3 - Height
             * 4 - Buttons
             * 5 - BodyStyle
             */

            object[] args = new object[9];
            args[0] = c.ClientID;
            args[1] = (string.IsNullOrEmpty(c.Title)) ? "&nbsp;" : c.Title;
            args[2] = width;
            args[3] = height;
            args[4] = buttons;
            args[5] = c.BodyStyle;
            args[6] = !string.IsNullOrEmpty(c.IconClsProxy) ? "x-panel-icon " + c.IconClsProxy : "";
            args[7] = c.Collapsed && c.Collapsible ? "x-panel-collapsed" : string.Empty;
            args[8] = (c.Collapsed && c.Collapsible) ? "display: none;" : "display: block;";
            // NOTE: Make sure you add to the object[SIZE] above if adding to the args array.

            this.AddIcon(c.Icon);

            LiteralControl topCtrl = new LiteralControl(string.Format(this.HtmlBegin, args) + this.GetIconStyleBlock());
  
            topCtrl.RenderControl(htmlWriter);

            if (!(c.Collapsed && c.Collapsible))
            {
                HtmlGenericControl div = (HtmlGenericControl)c.BodyContainer;
                div.Attributes[DesignerRegion.DesignerRegionAttributeName] = "0";
                div.Style["height"] = "100%";
                div.RenderControl(htmlWriter);
            }


            LiteralControl bottomCtrl = new LiteralControl(string.Format(this.HtmlEnd, args[8]));
            
            bottomCtrl.RenderControl(htmlWriter);

            string temp = writer.ToString();
            return temp;
        }

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new WindowActionList(this.Component));
                }
                return actionLists;
            }
        }

        private string GetDesignerRegionAttribute(WindowClickAction action)
        {
            designerRegions.Add(new DesignerRegion(this.CurrentDesigner, action.ToString(), false));
            return string.Format("{0}=\"{1}\"", DesignerRegion.DesignerRegionAttributeName, designerRegions.Count - 1);
        }

        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            WindowClickAction action =
                (WindowClickAction)Enum.Parse(typeof(WindowClickAction), e.Region.Name);

            switch (action)
            {
                case WindowClickAction.Toggle:
                    ToggleWindow();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ToggleWindow()
        {
            Window window = (Window)this.Control;

            PropertyDescriptor collapsed = TypeDescriptor.GetProperties(window)["Collapsed"];
            bool value = (bool)collapsed.GetValue(window);
            collapsed.SetValue(window, !value);
            window.Collapsed = !value;
        }

        enum WindowClickAction
        {
            Toggle
        }

        /*
         * 0 - ClientID
         * 1 - Title
         * 2 - Width
         * 3 - Height
         * 4 - Buttons
         * 5 - BodyStyle
         * 6 - IconCls
         * 7 - Collapsed style
         * 8  - Collapsed  display: block;
         */

        public override string HtmlBegin
        {
            get
            {
                return @"<div style=""display: block;{2}"" class=""x-window x-resizable-pinned {7}"">
    <div class=""x-window-tl"">
      <div class=""x-window-tr"">
        <div class=""x-window-tc"">
          <div style=""-moz-user-select: none;"" class=""x-window-header x-unselectable x-window-draggable {6}"">
            {4}
            <span class=""x-window-header-text"">{1}</span>
          </div>
        </div>
      </div>
    </div>
    <div class=""x-window-bwrap"" >
      <div class=""x-window-ml"" style=""{8}"">
        <div class=""x-window-mr"">
          <div class=""x-window-mc"">
            <div style=""{5}{3}"" class=""x-window-body"">
               <div>";
            }
        }

        public override string HtmlEnd
        {
            get
            {
                return
    @"       </div>
            </div>
           </div>
          </div>
         </div>
      <div class=""x-window-bl x-panel-nofooter"" style=""{0}"">
        <div class=""x-window-br"">
          <div class=""x-window-bc""></div>
        </div>
      </div>
    </div>
  </div>";
            }
        }
    }
}