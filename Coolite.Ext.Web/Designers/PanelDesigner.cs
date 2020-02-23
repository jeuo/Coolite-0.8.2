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
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class PanelDesigner : ContentPanelDesigner
    {
        private DesignerRegionCollection designerRegions;

        public override string GetDesignTimeHtml(DesignerRegionCollection regions)
        {
            designerRegions = regions;
            Panel c = (Panel)this.Control;
            
            StringWriter writer = new StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            string width = string.Format(" width: {0};", (c.Width == Unit.Empty) ? "auto" : c.Width.ToString());
            string height = string.Format(" height: {0};", (c.Height == Unit.Empty) ? "auto" : (c.Height.Value - 27).ToString() + "px");
            
            string buttons = string.Empty;
            buttons += (c.Collapsible) ? "<a {0}><div class=\"x-tool x-tool-toggle\">&nbsp;</div></a>" : string.Empty;

            if (this.Layout.HasValue && this.Layout.Value != LayoutType.Border || !this.Layout.HasValue)
            {
                if(!string.IsNullOrEmpty(buttons))
                {
                    buttons = string.Format(buttons, GetDesignerRegionAttribute(PanelClickAction.Toggle));
                }
                else
                {
                    // for prevent shifting regions
                    designerRegions.Add(new DesignerRegion(CurrentDesigner, "Empty", false));
                }
            }
            
            if (this.Layout.HasValue)
            {
                if (!Width.IsEmpty)
                {
                    width = string.Format(" width: {0};", (Width == Unit.Empty) ? "auto" : Width.ToString());
                }

                if (!Height.IsEmpty)
                {
                    if(Height.Type == UnitType.Pixel)
                    {
                        height = string.Format(" height: {0}px;", Height.Value - 27);  
                    }
                    else
                    {
                        height = string.Format(" height: {0};", Height);  
                    }
                }

                if (this.Layout.Value == LayoutType.Border)
                {
                    if (BorderRegion.Collapsible && BorderRegion.Region != RegionPosition.Center)
                    {
                        buttons = string.Format("<a {1}><div class=\"x-tool x-tool-toggle x-tool-collapse-{0}\">&nbsp;</div></a>", BorderRegion.Region.ToString().ToLower(), GetDesignerRegionAttribute(BorderRegion.Region, BorderLayoutDesigner.BorderLayoutClickAction.Collapse));
                    }
                    else
                    {
                        // for prevent shifting regions
                        designerRegions.Add(new DesignerRegion(CurrentDesigner, "Empty", false));
                        buttons = string.Empty;
                    }
                }
            }

            string iconCls = string.Empty;

            this.AddIcon(c.Icon);

            if(!string.IsNullOrEmpty(c.IconCls))
            {
                if (c.Frame)
                {
                    iconCls = "x-panel-icon " + c.IconCls;
                }
                else
                {
                    iconCls = string.Format("<img src=\"{0}\" class=\"x-panel-inline-icon {1}\" />", c.ScriptManager.BLANK_IMAGE_URL, c.IconCls);
                }
            }

            string header = "";

            if (c.Header)
            {
                /*
                 * 0  - x-panel-header-noborder
                 * 1  - IconCls
                 * 2  - Title
                 * 3  - Buttons
                 */

                object[] headerArgs = new object[4];
                headerArgs[0] = !c.Border ? "x-panel-header-noborder" : string.Empty;
                headerArgs[1] = iconCls;
                headerArgs[2] = c.Title;
                headerArgs[3] = buttons;

                header = string.Format(this.HtmlHeader, headerArgs);
            }

            /*
             0  - Width
             1  - x-panel-noborder
             2  - Collapsed style
             3  - Collapsed  display: block;
             4  - BodyStyle
             5  - Height
             6  - x-panel-body-noborder
             7  - HEADER 
             */

            object[] args = new object[8];
            args[0] = width;
            args[1] = !c.Border ? "x-panel-noborder" : string.Empty;
            args[2] = c.Collapsed && c.Collapsible ? "x-panel-collapsed" : string.Empty;
            args[3] = (c.Collapsed) ? "display: none;" : "display: block;";
            args[4] = c.BodyStyle;
            args[5] = height;
            args[6] = !c.Border ? "x-panel-body-noborder" : string.Empty;
            args[7] = header;

            LiteralControl topCtrl = new LiteralControl(string.Format(this.HtmlBegin, args) + this.GetIconStyleBlock());
            topCtrl.RenderControl(htmlWriter);

            HtmlGenericControl div = (HtmlGenericControl)c.BodyContainer;
            EditableDesignerRegion region = new EditableDesignerRegion(CurrentDesigner, ContentRegionName, false);
            designerRegions.Add(region);
            if ((!c.Collapsible) || (c.Collapsible && !c.Collapsed) || (this.Layout.HasValue && this.Layout.Value == LayoutType.Border))
            {
                div.Attributes[DesignerRegion.DesignerRegionAttributeName] = (designerRegions.Count - 1).ToString();
                div.Style["height"] = "100%";
                div.Style["overflow"] = "hidden";
                div.RenderControl(htmlWriter);
            }
            
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
                    actionLists.Add(new PanelActionList(this.Component));
                }
                return actionLists;
            }
        }

        private string GetDesignerRegionAttribute(RegionPosition region, BorderLayoutDesigner.BorderLayoutClickAction action)
        {
            string name = string.Format("{0}_{1}", region, action);
            designerRegions.Add(new DesignerRegion(CurrentDesigner, name, false));
            return string.Format("{0}=\"{1}\"", DesignerRegion.DesignerRegionAttributeName, designerRegions.Count-1);
        }

        private string GetDesignerRegionAttribute(PanelClickAction action)
        {
            designerRegions.Add(new DesignerRegion(CurrentDesigner, action.ToString(), false));
            return string.Format("{0}=\"{1}\"", DesignerRegion.DesignerRegionAttributeName, designerRegions.Count - 1);
        }

        enum PanelClickAction
        {
            Toggle
        }


        protected override void OnClick(DesignerRegionMouseEventArgs e)
        {
            PanelClickAction action =
                (PanelClickAction)Enum.Parse(typeof(PanelClickAction), e.Region.Name);

            switch(action)
            {
                case PanelClickAction.Toggle:
                    TogglePanel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TogglePanel()
        {
            Panel panel = (Panel) this.Control;

            PropertyDescriptor collapsed = TypeDescriptor.GetProperties(panel)["Collapsed"];
            bool value = (bool) collapsed.GetValue(panel);
            collapsed.SetValue(panel, !value);
            panel.Collapsed = !value;
        }


        /*
         * 0  - x-panel-header-noborder
         * 1  - IconCls
         * 2  - Title
         * 3  - Buttons
         */

        public virtual string HtmlHeader
        {
            get
            {
                return (((Panel)this.Control).Frame) ?
@"<div class=""x-panel-tl"">
        <div class=""x-panel-tr"">
            <div class=""x-panel-tc"">
                <div style=""-moz-user-select: none;"" class=""x-panel-header {0} x-unselectable {1}"">
                    <table cellpading=""0"" cellspacing=""0"" border=""0"" style=""width:auto"">
                        <tr>
                            <td style=""width:1%"" align=""left"" nowrap=""nowrap"">
                                <span class=""x-panel-header-text"">{2}</span>
                            </td>
                            <td align=""right"">
                                {3}
                            </td>
                        </tr>
                    </table>	
                </div>
            </div>
        </div>
    </div>" :

      @"<div style=""-moz-user-select: none;"" class=""x-panel-header {0} x-unselectable"">
			<table cellpading=""0"" cellspacing=""0"" border=""0"" style=""width:auto"">
                <tr>
                    <td style=""width:1%"" align=""left"" nowrap=""nowrap"">{1}<span class=""x-panel-header-text"" style=""white-space:nowrap;"">{2}</span></td>
                    <td align=""right"">
                        {3}
                    </td>
                </tr>
            </table>	
		</div>";
            }
        }

        /*
         0  - Width
         1  - x-panel-noborder
         2  - Collapsed style
         3  - Collapsed  display: block;
         4  - BodyStyle
         5  - Height
         6  - x-panel-body-noborder
         7  - HEADER 
         */

        public override string HtmlBegin
        {
            get
            {
                return (((Panel)this.Control).Frame) ?
@"<div style=""{0}"" class=""x-panel {1} {2}"">
    {7}
    <div style=""{3}"" class=""x-panel-bwrap"">
      <div class=""x-panel-ml"">
        <div class=""x-panel-mr"">
          <div class=""x-panel-mc"">
            <div style=""{4}{5}"" class=""x-panel-body {6}"">" :

@"<div style=""{0}"" class=""x-panel {1} {2}"">
		{7}
		<div style=""position: static; visibility: visible; {3} left: auto;startp: auto; z-index: auto;"" class=""x-panel-bwrap"">
			<div style=""{4}{5}"" class=""x-panel-body {6}"">";
            }
        }

        public override string HtmlEnd
        {
            get
            {
                return (((Panel)this.Control).Frame) ?
@"            </div>
          </div>
        </div>
      </div>
      <div class=""x-panel-bl x-panel-nofooter"">
        <div class=""x-panel-br"">
          <div class=""x-panel-bc""></div>
        </div>
      </div>
    </div>
  </div>" :
          
@"		</div>
	</div>
</div>";
            }
        }
    }
}