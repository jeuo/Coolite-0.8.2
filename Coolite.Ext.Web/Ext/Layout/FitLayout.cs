/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// This is a base class for layouts that contain a single items that automatically expands to fill the layout's contentContainer. This class is intended to be extended or created via the layout:'fit' Ext.Container.layout config, and should generally not need to be created directly via the new keyword. FitLayout does not have any direct config options (other than inherited ones). To fit a panel to a contentContainer using FitLayout, simply set layout:'fit' on the contentContainer and add a single panel to it. If the contentContainer has multiple panels, only the first one will be displayed.
    /// </summary>
    [Layout("coolitefit")]
    [ToolboxData("<{0}:FitLayout runat=\"server\"><{0}:Panel runat=\"server\" Title=\"Title\"><Body></Body></{0}:Panel></{0}:FitLayout>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.FitLayout), "Build.Resources.ToolboxIcons.FitLayout.bmp")]
    [Description("This is a base class for layouts that contain a single items that automatically expands to fill the layout's contentContainer. This class is intended to be extended or created via the layout:'fit' Ext.Container.layout config, and should generally not need to be created directly via the new keyword. FitLayout does not have any direct config options (other than inherited ones). To fit a panel to a contentContainer using FitLayout, simply set layout:'fit' on the contentContainer and add a single panel to it. If the contentContainer has multiple panels, only the first one will be displayed.")]
    [Designer(typeof(FitLayoutDesigner))]
    public class FitLayout : ContainerLayout
    {
        protected override bool SingleItemMode
        {
            get
            {
                return true;
            }
        }
    }
}