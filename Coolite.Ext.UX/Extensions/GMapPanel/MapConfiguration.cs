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
    public class MapConfiguration : StateManagedItem
    {
        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Enables the dragging of the map (enabled by default).")]
        public bool Dragging
        {
            get
            {
                object o = this.ViewState["Dragging"];
                return o != null ? (bool)o : true;
            }
            set
            {
                this.ViewState["Dragging"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Enables info window operations on the map (enabled by default).")]
        public bool InfoWindow
        {
            get
            {
                object o = this.ViewState["InfoWindow"];
                return o != null ? (bool)o : true;
            }
            set
            {
                this.ViewState["InfoWindow"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Enables double click to zoom in and out (disabled by default).")]
        public bool DoubleClickZoom
        {
            get
            {
                object o = this.ViewState["DoubleClickZoom"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["DoubleClickZoom"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Enables continuous smooth zooming for select browsers (disabled by default).")]
        public bool ContinuousZoom
        {
            get
            {
                object o = this.ViewState["ContinuousZoom"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["ContinuousZoom"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Enables the GoogleBar, an integrated search control, to the map. When enabled, this control takes the place of the default Powered By Google logo. Note that this control is not enabled by default.")]
        public bool GoogleBar
        {
            get
            {
                object o = this.ViewState["GoogleBar"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GoogleBar"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Enables zooming using a mouse's scroll wheel. Note: scroll wheel zoom is disabled by default.")]
        public bool ScrollWheelZoom
        {
            get
            {
                object o = this.ViewState["ScrollWheelZoom"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["ScrollWheelZoom"] = value;
            }
        }
    }
}
