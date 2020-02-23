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
    /// <summary>
    /// A split button that provides a built-in dropdown arrow that can fire an event separately from the default click event of the button. Typically this would be used to display a dropdown menu that provides additional options to the primary button action, but any custom handler can provide the arrowclick implementation.
    /// </summary>
    [Description("A split button that provides a built-in dropdown arrow that can fire an event separately from the default click event of the button. Typically this would be used to display a dropdown menu that provides additional options to the primary button action, but any custom handler can provide the arrowclick implementation.")]
    [Xtype("splitbutton")]
    [ToolboxData("<{0}:SplitButton runat=\"server\" Text=\"Split Button\" />")]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.SplitButton), "Build.Resources.ToolboxIcons.SplitButton.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [InstanceOf(ClassName = "Ext.SplitButton")]
    public class SplitButton : SplitButtonBase
    {
        private SplitButtonListeners listeners;

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
        public SplitButtonListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new SplitButtonListeners();
                    this.listeners.InitOwners(this);
                }
                return this.listeners;
            }
        }

        private SplitButtonAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public SplitButtonAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new SplitButtonAjaxEvents();
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