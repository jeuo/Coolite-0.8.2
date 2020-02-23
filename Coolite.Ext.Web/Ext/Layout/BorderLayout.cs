/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    [Layout("border")]
    [ParseChildren(true)]
    [ToolboxData("<{0}:BorderLayout runat=\"server\"><West Split=\"true\" Collapsible=\"true\"><{0}:Panel runat=\"server\" Title=\"West\" Width=\"175\" /></West><Center><{0}:Panel runat=\"server\" Title=\"Center\" /></Center><East Split=\"true\" Collapsible=\"true\"><{0}:Panel runat=\"server\" Title=\"East\" Width=\"175\" /></East><South Split=\"true\" Collapsible=\"true\"><{0}:Panel runat=\"server\" Title=\"South\" Height=\"150\" /></South></{0}:BorderLayout>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.BorderLayout), "Build.Resources.ToolboxIcons.BorderLayout.bmp")]
    [Designer(typeof(BorderLayoutDesigner))]
    public class BorderLayout : ContainerLayout
    {
        private BorderLayoutRegion north;
        private BorderLayoutRegion south;
        private BorderLayoutRegion west;
        private BorderLayoutRegion east;
        private BorderLayoutRegion center;

        /// <summary>
        /// Represent options of north region
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Represent options of north region")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        public BorderLayoutRegion North
        {
            get
            {
                if(this.north == null)
                {
                    this.north = new BorderLayoutRegion(this, RegionPosition.North);
                    if (this.IsTrackingViewState)
                    {
                        this.north.TrackViewState();
                    }
                }
                return this.north;
            }
        }

        /// <summary>
        /// Represent options of south region
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Represent options of south region")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        public BorderLayoutRegion South
        {
            get
            {
                if (this.south == null)
                {
                    this.south = new BorderLayoutRegion(this, RegionPosition.South);
                    if (this.IsTrackingViewState)
                    {
                        this.south.TrackViewState();
                    }
                }
                return this.south;
            }
        }

        /// <summary>
        /// Represent options of west region
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Represent options of west region")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        public BorderLayoutRegion West
        {
            get
            {
                if (this.west == null)
                {
                    this.west = new BorderLayoutRegion(this, RegionPosition.West);
                    if (this.IsTrackingViewState)
                    {
                        this.west.TrackViewState();
                    }
                }
                return this.west;
            }
        }

        /// <summary>
        /// Represent options of east region
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Represent options of east region")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        public BorderLayoutRegion East
        {
            get
            {
                if (this.east == null)
                {
                    this.east = new BorderLayoutRegion(this, RegionPosition.East);
                    if (this.IsTrackingViewState)
                    {
                        this.east.TrackViewState();
                    }
                }
                return this.east;
            }
        }

        /// <summary>
        /// Represent options of center region
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Description("Represent options of center region")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        public BorderLayoutRegion Center
        {
            get
            {
                if (this.center == null)
                {
                    this.center = new BorderLayoutRegion(this, RegionPosition.Center);
                    if (this.IsTrackingViewState)
                    {
                        this.center.TrackViewState();
                    }
                }
                return this.center;
            }
        }

        List<BorderLayoutRegion> regions = null;

        internal List<BorderLayoutRegion> Regions
        {
            get
            {
                if (this.regions != null)
                {
                    return this.regions;
                }
                this.regions = new List<BorderLayoutRegion>();
                this.regions.AddRange(new BorderLayoutRegion[] {
                        this.North, 
                        this.East,
                        this.South,
                        this.West,
                        this.Center
                });
                return this.regions;
            }
        }

        internal void ResetRegion(RegionPosition region)
        {
            switch (region)
            {
                case RegionPosition.North:
                    this.north = null;
                    break;
                case RegionPosition.South:
                    this.south = null;
                    break;
                case RegionPosition.East:
                    this.east = null;
                    break;
                case RegionPosition.West:
                    this.west = null;
                    break;
                case RegionPosition.Center:
                    this.center = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("region");
            }
        }
    }
}