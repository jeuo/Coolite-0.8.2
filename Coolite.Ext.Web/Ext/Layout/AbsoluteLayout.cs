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
    /// This layout adds the ability for x/y positioning using the standard x and y component config options.
    /// </summary>
    [Layout("absolute")]
    [ToolboxData("<{0}:AbsoluteLayout id=\"AbsoluteLayout1\" runat=\"server\"><{0}:Panel runat=\"server\" Title=\"Panel 1\" X=\"50\" Y=\"50\" Width=\"200\" Height=\"100\"Frame=\"true\" BodyStyle=\"padding:15px;\"><Body>Positioned at x:50, y:50</Body></{0}:Panel><{0}:Panel runat=\"server\" Title=\"Panel 2\" X=\"125\" Y=\"125\" Width=\"200\"Height=\"100\" Frame=\"true\" BodyStyle=\"padding:15px;\"><Body>Positioned at x:125, y:125</Body></{0}:Panel></{0}:AbsoluteLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.AbsoluteLayout), "Build.Resources.ToolboxIcons.AbsoluteLayout.bmp")]
    [Description("This layout adds the ability for x/y positioning using the standard x and y component config options.")]
    public class AbsoluteLayout : AnchorLayout { }
}