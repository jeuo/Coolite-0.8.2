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
    [ToolboxData("<{0}:ScriptContainer runat=\"server\" />")]
    [NonVisualControl]
    [Designer(typeof(ScriptContainerDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ScriptContainer), "Build.Resources.ToolboxIcons.ScriptContainer.bmp")]
    [Description("Simple Container Control to allow for custom placement of Scripts and Styles in the <head> of the Page by the ScriptManager. If the Page does not contain a <ext:ScriptContainer> control, the <script>'s and <style>'s will be added as the last items in the <head>. The ScriptContainer does not render any HTML to the Page.")]
    public class ScriptContainer : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.Write("<Coolite.Ext.Web.InitScriptPlaceholder />");
        }
    }
}