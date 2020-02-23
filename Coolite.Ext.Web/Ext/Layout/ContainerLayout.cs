/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Every layout is composed of one or more Ext.Container elements internally, and ContainerLayout provides the basic foundation for all other layout classes in Ext. It is a non-visual class that simply provides the base logic required for a Container to function as a layout. 
    /// </summary>
    [Layout("container")]
    [ToolboxData("<{0}:ContainerLayout runat=\"server\"></{0}:ContainerLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ContainerLayout), "Build.Resources.ToolboxIcons.ContainerLayout.bmp")]
    [DefaultProperty("Items")]
    [ParseChildren(true,"Items")]
    public class ContainerLayout : Layout
    {
        [ClientConfig(JsonMode.Object)]
        internal virtual LayoutConfig LayoutConfig
        {
            get
            {
                return new LayoutConfig(this.RenderHidden, this.ExtraCls);
            }
        }
    }
}