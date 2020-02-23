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
using System.Web.UI;
using Coolite.Ext.Web;

namespace Coolite.Ext.UX
{
    public class Marker : StateManagedItem
    {
        [ClientConfig]
        [DefaultValue(0.0)]
        [NotifyParentProperty(true)]
        [Description("The latitude coordinate")]
        public double Lat
        {
            get
            {
                object o = this.ViewState["Lat"];
                return o != null ? (double)o : 0.0;
            }
            set
            {
                this.ViewState["Lat"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(0.0)]
        [NotifyParentProperty(true)]
        [Description("The longitude  coordinate")]
        public double Lng
        {
            get
            {
                object o = this.ViewState["Lng"];
                return o != null ? (double)o : 0.0;
            }
            set
            {
                this.ViewState["Lng"] = value;
            }
        }

        private MarkerOptions markerOptions;

        [ClientConfig("marker",JsonMode.Object)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public MarkerOptions Options
        {
            get
            {
                if (this.markerOptions == null)
                {
                    this.markerOptions = new MarkerOptions();
                    this.markerOptions.TrackViewState();
                }

                return this.markerOptions;
            }
        }

        private MarkerListeners listeners;

        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public MarkerListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new MarkerListeners();

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }
    }

    public class MarkerCollection : StateManagedCollection<Marker> { }
}
