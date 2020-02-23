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
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.grid.GroupSummary")]
    [ClientScript(Type = typeof(Plugin), FilePath = "/ux/plugins/groupingsummary/groupingsummary.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.groupingsummary.groupingsummary.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.GroupingSummary), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:SliderTip runat=\"server\" />")]
    [Description("Grouping summary plugin for GridPanel")]
    public class GroupingSummary : Plugin { }
}
