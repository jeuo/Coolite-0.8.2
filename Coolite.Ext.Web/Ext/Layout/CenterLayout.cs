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
    /// <summary>
    /// This is a very simple layout style used to center contents within a container.
    /// </summary>
    [Layout("ux.center")]
    [ToolboxData("<{0}:CenterLayout runat=\"server\"><{0}:Panel runat=\"server\" Title=\"Title\"><Body></Body></{0}:Panel></{0}:CenterLayout>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.CenterLayout), "Build.Resources.ToolboxIcons.CenterLayout.bmp")]
    [Description("This is a very simple layout style used to center contents within a container.")]
    [Designer(typeof(EmptyDesigner))]
    public class CenterLayout : ContainerLayout
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            string css = ".ux-layout-center-item {margin:0 auto;text-align:left;}.ux-layout-center .x-panel-body,body.ux-layout-center,form.ux-layout-center{text-align:center;}";
            this.ScriptManager.RegisterClientStyleBlock("CenterLayoutCss", css);
        }

        protected override bool SingleItemMode
        {
            get
            {
                return true;
            }
        }
    }
}