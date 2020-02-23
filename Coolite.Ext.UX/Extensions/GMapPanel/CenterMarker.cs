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
using Coolite.Ext.Web;

namespace Coolite.Ext.UX
{
    public class CenterMarker : Marker
    {
        [ClientConfig("geoCodeAddr")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Sends a request to Google servers to geocode the specified address.")]
        public string GeoCodeAddress
        {
            get
            {
                object o = this.ViewState["GeoCodeAddress"];
                return o != null ? (string)o : "";
            }
            set
            {
                this.ViewState["GeoCodeAddress"] = value;
            }
        }
    }
}
