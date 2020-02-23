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
    [Description("A menu object. This is the container to which you add all other menu items. Menu can also serve as a base class when you want a specialized menu based off of another component.")]
    [InstanceOf(ClassName = "Ext.menu.Menu")]
    [ToolboxData("<{0}:Menu runat=\"server\"><Items><{0}:MenuItem runat=\"server\" Text=\"Item1\" /><{0}:MenuItem runat=\"server\" Text=\"Item2\" /><{0}:MenuItem runat=\"server\" Text=\"Item3\" /></Items></ext:Menu>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Menu), "Build.Resources.ToolboxIcons.Menu.bmp")]
    [Designer(typeof(EmptyDesigner))]
    public class Menu : MenuBase
    {
        private MenuListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        [ViewStateMember]
        public MenuListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new MenuListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private MenuAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side AjaxEvent Handlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side AjaxEventHandlers")]
        [ViewStateMember]
        public MenuAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new MenuAjaxEvents();
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
}