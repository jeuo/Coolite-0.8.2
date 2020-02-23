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
    [InstanceOf(ClassName = "Ext.ux.plugins.TabFx")]
    [ClientScript(Type = typeof(PanelResizer), FilePath = "/ux/plugins/tabfx/tabfx.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.tabfx.tabfx.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TabFx), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:TabFx runat=\"server\" />")]
    public class TabFx : Plugin
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Frame")]
        [NotifyParentProperty(true)]
        public virtual string Name
        {
            get
            {
                return (string)this.ViewState["Name"] ?? "Frame";
            }
            set
            {
                this.ViewState["Name"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Args
        {
            get
            {
                return (string)this.ViewState["Args"] ?? "";
            }
            set
            {
                this.ViewState["Args"] = value;
            }
        }
    }
}