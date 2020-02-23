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
    /// Panel is a contentContainer that has specific functionality and structural components that make it the perfect building block for application-oriented user interfaces.
    /// </summary>
    [ToolboxData("<{0}:Panel runat=\"server\" Title=\"Title\" Height=\"300\"><Body></Body></{0}:Panel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Panel), "Build.Resources.ToolboxIcons.Panel.bmp")]
    [Designer(typeof(PanelDesigner))]
    [Description("Panel is a contentContainer that has specific functionality and structural components that make it the perfect building block for application-oriented user interfaces.")]
    public class Panel : ContentPanel
    {
        public Panel() { }

        public Panel(string title) { this.Title = title; }

        public Panel(string title, Icon icon) 
        {
            this.Title = title;
            this.Icon = icon;
        }

        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (this.FormGroup)
            {
                if (this.ScriptManager.RenderStyles == ResourceLocationType.Embedded || this.ScriptManager.RenderStyles == ResourceLocationType.CacheFly)
                {
                    this.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.formgroup.css.formgroup-embedded.css");
                }
                else if (this.ScriptManager.RenderStyles == ResourceLocationType.File || this.ScriptManager.RenderStyles == ResourceLocationType.CacheFlyAndFile)
                {
                    this.ScriptManager.RegisterClientStyleInclude("Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.formgroup.css.formgroup.css");
                }

                this.Collapsible = true;
                this.AnimCollapse = false;
                this.TitleCollapse = true;
                this.HideCollapseTool = true;
                this.BaseCls = "x-form-group";
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to animate the transition when the panel is collapsed, false to skip the animation (defaults to true if the Ext.Fx class is available, otherwise false).")]
        [NotifyParentProperty(true)]
        public virtual bool FormGroup
        {
            get
            {
                object obj = this.ViewState["FormGroup"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["FormGroup"] = value;
            }
        }

        
        private PanelListeners listeners;

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
        public PanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private PanelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public PanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PanelAjaxEvents();
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