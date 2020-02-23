/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.ux.plugins.GridPanelMaintainScrollPositionOnRefresh")]
    [ClientScript(Type = typeof(GridPanelMaintainScrollPositionOnRefresh), FilePath = "/ux/plugins/gridpanelmaintainscrollpositiononrefresh/gridpanelmaintainscrollpositiononrefresh.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.gridpanelmaintainscrollpositiononrefresh.gridpanelmaintainscrollpositiononrefresh.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.GridPanelMaintainScrollPositionOnRefresh), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:GridPanelMaintainScrollPositionOnRefresh runat=\"server\" />")]
    public class GridPanelMaintainScrollPositionOnRefresh : Plugin { }
}
