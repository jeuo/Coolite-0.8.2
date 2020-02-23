/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;
using Coolite.Ext.Web;
using System.Drawing;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.ux.SlidingPager")]
    [ClientScript(Type = typeof(SliderTip), FilePath = "/ux/plugins/slidertip/slidertip.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.slidertip.slidertip.js")]
    [ClientScript(Type = typeof(SlidingPager), FilePath = "/ux/plugins/slidingpager/slidingpager.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.slidingpager.slidingpager.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.SlidingPager), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:SlidingPager runat=\"server\" />")]
    public class SlidingPager : Plugin
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
