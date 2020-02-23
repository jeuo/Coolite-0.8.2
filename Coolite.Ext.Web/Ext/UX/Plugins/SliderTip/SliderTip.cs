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
    [InstanceOf(ClassName = "Ext.ux.SliderTip")]
    [ClientScript(Type = typeof(SliderTip), FilePath = "/ux/plugins/slidertip/slidertip.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.slidertip.slidertip.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.SliderTip), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:SliderTip runat=\"server\" />")]
    [Description("Simple plugin for using an Ext.Tip with a slider to show the slider value")]
    public class SliderTip : Plugin
    {
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("Override this function to apply custom tip text")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction GetText
        {
            get
            {
                object obj = ViewState["GetText"];
                return (obj == null) ? null : (JFunction)obj;
            }
            set
            {
                this.ViewState["GetText"] = value;
            }
        }
    }
}
