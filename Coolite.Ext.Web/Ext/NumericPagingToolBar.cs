/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [Xtype("numpaging")]
    [InstanceOf(ClassName = "Ext.ux.NumericPagingToolbar")]
    [ToolboxItem(false)]
    [ToolboxData("<{0}:NumericPagingToolBar runat=\"server\"></{0}:NumericPagingToolBar>")]
    [Description("A specialized toolbar that is bound to a Ext.data.Store and provides automatic paging controls.")]
    public class NumericPagingToolBar : PagingToolbar { }
}