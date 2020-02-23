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
using System.Web;
using System.Web.UI;
using Coolite.Ext.Web;

namespace Coolite.Ext.UX
{
    public abstract class GMapPanelBase : Panel
    {
        [Browsable(false)]
        public override ITemplate Body
        {
            get { return null; }
            set { base.Body = value; }
        }

        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string Html
        {
            get { return base.Html; }
            set { base.Html = value; }
        }

        [Browsable(false)]
        public override void UpdateBody(string text)
        {
            base.UpdateBody(text);
        }

        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string ContentEl
        {
            get { return base.ContentEl; }
        }

        [ClientConfig]
        [DefaultValue(3)]
        [NotifyParentProperty(true)]
        [Description("The zoom level of the blowup map in the info window.")]
        public int ZoomLevel
        {
            get
            {
                object o = this.ViewState["ZoomLevel"];
                return o != null ? (int) o : 3; 
            }
            set
            {
                this.ViewState["ZoomLevel"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(180)]
        [NotifyParentProperty(true)]
        [Description("Used by street view. The camera yaw in degrees relative to true north. True north is 0 degrees, east is 90 degrees, south is 180 degrees, west is 270 degrees.")]
        public int Yaw
        {
            get
            {
                object o = this.ViewState["Yaw"];
                return o != null ? (int)o : 180;
            }
            set
            {
                this.ViewState["Yaw"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("Used by street view. The camera pitch in degrees, relative to the street view vehicle. Ranges from 90 degrees (directly upwards) to -90 degrees (directly downwards).")]
        public int Pitch
        {
            get
            {
                object o = this.ViewState["Pitch"];
                return o != null ? (int)o : 0;
            }
            set
            {
                this.ViewState["Pitch"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        [Description("Used by street view. The zoom level. Fully zoomed-out is level 0, zooming in increases the zoom level.")]
        public int Zoom
        {
            get
            {
                object o = this.ViewState["Pitch"];
                return o != null ? (int)o : 0;
            }
            set
            {
                this.ViewState["Pitch"] = value;
            }
        }

        [ClientConfig("gmapType", JsonMode.ToLower)]
        [DefaultValue(GMapType.Map)]
        [NotifyParentProperty(true)]
        [Description("GMap type")]
        public GMapType GMapType
        {
            get
            {
                object o = this.ViewState["GMapType"];
                return o != null ? (GMapType)o : GMapType.Map;
            }
            set
            {
                this.ViewState["GMapType"] = value;
            }
        }

        private MapConfiguration mapConfiguration;

        [ClientConfig("mapConfOpts",typeof(MapPropertiesJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public MapConfiguration MapConfiguration
        {
            get
            {
                if(this.mapConfiguration == null)
                {
                    this.mapConfiguration = new MapConfiguration();
                    this.mapConfiguration.TrackViewState();
                }

                return this.mapConfiguration;
            }
        }

        private MapControls mapControls;

        [ClientConfig("mapControls",typeof(MapPropertiesJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public MapControls MapControls
        {
            get
            {
                if (this.mapControls == null)
                {
                    this.mapControls = new MapControls();
                    this.mapControls.TrackViewState();
                }

                return this.mapControls;
            }
        }

        private CenterMarker centerMarker;

        [ClientConfig("setCenter", JsonMode.Object)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public CenterMarker CenterMarker
        {
            get
            {
                if (this.centerMarker == null)
                {
                    this.centerMarker = new CenterMarker();
                    this.centerMarker.TrackViewState();
                }

                return this.centerMarker;
            }
        }

        private MarkerCollection markers;

        [ClientConfig("markers", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public MarkerCollection Markers
        {
            get
            {
                if (this.markers == null)
                {
                    this.markers = new MarkerCollection();
                    this.markers.TrackViewState();
                }

                return this.markers;
            }
        }

        [DefaultValue("ABQIAAAAJDLv3q8BFBryRorw-851MRT2yXp_ZAY8_ufC3CFXhHIE1NvwkxTyuslsNlFqyphYqv1PCUD8WrZA2A")]
        [NotifyParentProperty(true)]
        [Description("GMaps API Key. Default key -  GMaps API Key that works for localhost")]
        public string APIKey
        {
            get
            {
                return (string)this.ViewState["ApiKey"] ?? "ABQIAAAAJDLv3q8BFBryRorw-851MRT2yXp_ZAY8_ufC3CFXhHIE1NvwkxTyuslsNlFqyphYqv1PCUD8WrZA2A";
            }
            set
            {
                this.ViewState["ApiKey"] = value;
                if(!this.DesignMode)
                {
                    HttpContext.Current.Items["GMapApiKey"] = value;
                }
            }
        }

        [DefaultValue("http://maps.google.com/maps?file=api&amp;v=2.x&amp;key={0}")]
        [NotifyParentProperty(true)]
        [Description("GMaps API Key. Default key -  GMaps API Key that works for localhost")]
        public string APIBaseUrl
        {
            get
            {
                return (string)this.ViewState["APIBaseUrl"] ?? "http://maps.google.com/maps?file=api&amp;v=2.x&amp;key={0}";
            }
            set
            {
                this.ViewState["APIBaseUrl"] = value;
            }
        }
    }
}
