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
    public class MapControls : StateManagedItem
    {
        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a control with buttons to pan in four directions, and zoom in and zoom out.")]
        public bool GSmallMapControl
        {
            get
            {
                object o = this.ViewState["GSmallMapControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GSmallMapControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a control with buttons to pan in four directions, and zoom in and zoom out, and a zoom slider.")]
        public bool GLargeMapControl
        {
            get
            {
                object o = this.ViewState["GLargeMapControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GLargeMapControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a control with buttons to zoom in and zoom out.")]
        public bool GSmallZoomControl
        {
            get
            {
                object o = this.ViewState["GSmallZoomControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GSmallZoomControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a control that displays the map scale.")]
        public bool GScaleControl
        {
            get
            {
                object o = this.ViewState["GScaleControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GScaleControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a standard map type control for selecting and switching between supported map types via buttons.")]
        public bool GMapTypeControl
        {
            get
            {
                object o = this.ViewState["GMapTypeControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GMapTypeControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a drop-down map type control for switching between supported map types.")]
        public bool GMenuMapTypeControl
        {
            get
            {
                object o = this.ViewState["GMenuMapTypeControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GMenuMapTypeControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a 'nested' map type control for selecting and switching between supported map types via buttons and nested checkboxes.")]
        public bool GHierarchicalMapTypeControl
        {
            get
            {
                object o = this.ViewState["GHierarchicalMapTypeControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GHierarchicalMapTypeControl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Creates a collapsible overview mini-map in the corner of the main map for reference location and navigation (through dragging). The GOverviewMapControl creates an overview map with a one-pixel black border.")]
        public bool GOverviewMapControl
        {
            get
            {
                object o = this.ViewState["GOverviewMapControl"];
                return o != null ? (bool)o : false;
            }
            set
            {
                this.ViewState["GOverviewMapControl"] = value;
            }
        }
    }
}
