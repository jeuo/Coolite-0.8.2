/******** 
 * This file is part of the Coolite UX Toolkit.

 * The Coolite UX Toolkit is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * The Coolite UX Toolkit is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the Coolite Toolkit.  If not, see <http://www.gnu.org/licenses/>.
 */

/*
* @version:		0.6.0 Preview-1
* @author:		Coolite Inc. http://www.coolite.com/
* @date:		2008-08-05
* @copyright:	Copyright (c) 2006-2008, Coolite Inc, or as noted within each 
* 				applicable file LICENSE.txt file
* @license:		LGPL 3.0 License
* @website:		http://www.coolite.com/
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using Coolite.Ext.Web;

[assembly: WebResource("Coolite.Ext.UX.Extensions.GMapPanel.resources.GMapPanel.js", "text/javascript")]

namespace Coolite.Ext.UX
{
    [Designer(typeof(EmptyDesigner))]
    [DefaultProperty("")]
    [Xtype("gmappanel")]
    [InstanceOf(ClassName = "Ext.ux.GMapPanel")]
    [ClientScript(Type = typeof(GMapPanel), WebResource = "Coolite.Ext.UX.Extensions.GMapPanel.resources.GMapPanel.js", FilePath = "ux/extensions/gmappanel/gmappanel.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.UX.GMapPanel), "Extensions.GMapPanel.GMapPanel.bmp")]
    [ToolboxData("<{0}:GMapPanel runat=\"server\" Title=\"Google Map\" Height=\"300\"></{0}:GMapPanel>")]
    [Description("GMap Panel")]
    public class GMapPanel : GMapPanelBase
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            string apiKey = HttpContext.Current.Items["GMapApiKey"] as string;
            this.ScriptManager.RegisterClientScriptInclude("GMapApiKey", string.Format(this.APIBaseUrl, apiKey ?? this.APIKey));
        }
    }
}
