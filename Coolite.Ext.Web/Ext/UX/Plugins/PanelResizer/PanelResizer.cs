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
    [InstanceOf(ClassName = "Ext.ux.PanelResizer")]
    [ClientScript(Type = typeof(PanelResizer), FilePath = "/ux/plugins/panelresizer/panelresizer.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.panelresizer.panelresizer.js")]
    [ClientStyle(Type = typeof(PanelResizer), FilePath = "/ux/plugins/panelresizer/css/panelresizer.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.panelresizer.css.panelresizer-embedded.css")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.PanelResizer), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:PanelResizer runat=\"server\" />")]
    public class PanelResizer : Plugin
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        public int MinHeight
        {
            get
            {
                object obj = this.ViewState["MinHeight"];
                return obj == null ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["MinHeight"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(10000000)]
        [NotifyParentProperty(true)]
        public int MaxHeight
        {
            get
            {
                object obj = this.ViewState["MaxHeight"];
                return obj == null ? 10000000 : (int)obj;
            }
            set
            {
                this.ViewState["MaxHeight"] = value;
            }
        }
    }
}
