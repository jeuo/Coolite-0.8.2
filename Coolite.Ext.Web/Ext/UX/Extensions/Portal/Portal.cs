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
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    [Xtype("portal")]
    [InstanceOf(ClassName = "Ext.ux.Portal")]
    [ClientScript(Type = typeof(Portal), FilePath="/ux/extensions/portal/portal.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.portal.portal.js")]
    [ClientStyle(Type = typeof(Portal), FilePath="/ux/extensions/portal/css/portal.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.portal.css.portal-embedded.css")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Portal), "Build.Resources.ToolboxIcons.Portal.bmp")]
    [ToolboxData("<{0}:Portal runat=\"server\" Title=\"Portal\"><Body><{0}:ColumnLayout runat=\"server\"><{0}:LayoutColumn ColumnWidth=\".33\"><{0}:PortalColumn runat=\"server\"StyleSpec=\"padding:10px 0 10px 10px\"><Body><{0}:AnchorLayout runat=\"server\"><{0}:Anchor Horizontal=\"100%\"><{0}:Portlet runat=\"server\" Title=\"Portlet 1\"/></{0}:Anchor><{0}:Anchor Horizontal=\"100%\"><{0}:Portlet runat=\"server\" Title=\"Portlet 2\"/></{0}:Anchor></{0}:AnchorLayout></Body></{0}:PortalColumn></{0}:LayoutColumn><{0}:LayoutColumn ColumnWidth=\".33\"><{0}:PortalColumn runat=\"server\"StyleSpec=\"padding:10px 0 10px 10px\"><Body><{0}:AnchorLayout runat=\"server\"><{0}:Anchor Horizontal=\"100%\"><{0}:Portlet runat=\"server\" Title=\"Portlet 3\"/></{0}:Anchor></{0}:AnchorLayout></Body></{0}:PortalColumn></{0}:LayoutColumn><{0}:LayoutColumn ColumnWidth=\".33\"><{0}:PortalColumn runat=\"server\" StyleSpec=\"padding:10px\"><Body><{0}:AnchorLayout runat=\"server\"><{0}:Anchor Horizontal=\"100%\"><{0}:Portlet Title=\"Portlet 4\" runat=\"server\" /></{0}:Anchor></{0}:AnchorLayout></Body></{0}:PortalColumn></{0}:LayoutColumn></{0}:ColumnLayout></Body></{0}:Portal>")]
    [Description("Portlet")]
    public class Portal : ContentPanel
    {
        private PortalListeners listeners;

        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public PortalListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PortalListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private PortalAjaxEvents ajaxEvents;

        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public PortalAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PortalAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
    }

    [Xtype("portalcolumn")]
    [InstanceOf(ClassName = "Ext.ux.PortalColumn")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.PortalColumn), "Build.Resources.ToolboxIcons.PortalColumn.bmp")]
    [ToolboxData("<{0}:PortalColumn runat=\"server\" StyleSpec=\"padding:10px 0 10px 10px\"><Body><{0}:AnchorLayout ID=\"AnchorLayout1\" runat=\"server\"><{0}:Anchor Horizontal=\"100%\"><{0}:Portlet Title=\"Portlet\" runat=\"server\" /></{0}:Anchor></{0}:AnchorLayout></Body></{0}:PortalColumn>")]
    [Description("Portlet")]
    public class PortalColumn : ContentPanel { }

    [Xtype("portlet")]
    [InstanceOf(ClassName = "Ext.ux.Portlet")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Portlet), "Build.Resources.ToolboxIcons.Portlet.bmp")]
    [ToolboxData("<{0}:Portlet runat=\"server\" Title=\"Portlet\" />")]
    [Description("Portlet")]
    public class Portlet : Panel
    {
        /// <summary>
        /// True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header tool button area, false to keep the panel statically sized with no button (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to make the panel collapsible and have the expand/collapse toggle button automatically rendered into the header tool button area, false to keep the panel statically sized with no button (defaults to false).")]
        [NotifyParentProperty(true)]
        public override bool Collapsible
        {
            get
            {
                object obj = this.ViewState["Collapsible"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Collapsible"] = value;
            }
        }

        /// <summary>
        /// True to enable dragging of this Panel (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to enable dragging of this Panel (defaults to false).")]
        [NotifyParentProperty(true)]
        public override bool Draggable
        {
            get
            {
                object obj = this.ViewState["Draggable "];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Draggable "] = value;
            }
        }
    }
}