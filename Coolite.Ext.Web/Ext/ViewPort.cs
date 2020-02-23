/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A specialized contentContainer representing the viewable application area (the browser viewport).
    /// </summary>
    [Xtype("cooliteviewport")]
    [InstanceOf(ClassName = "Coolite.Ext.Viewport", NoFormClassName = "Ext.Viewport")]
    [ToolboxData("<{0}:ViewPort runat=\"server\"><Body><{0}:BorderLayout runat=\"server\"><North Collapsible=\"True\" Split=\"True\"><{0}:Panel runat=\"server\" Height=\"100\" Title=\"North\"><Body></Body></{0}:Panel></North><East Collapsible=\"true\" Split=\"true\"><{0}:Panel runat=\"server\" Title=\"East\" Width=\"175\"><Body><{0}:FitLayout runat=\"server\"><{0}:TabPanel runat=\"server\" ActiveTabIndex=\"0\" Title=\"Title\" TabPosition=\"Bottom\" Border=\"false\"><Tabs><{0}:Tab runat=\"server\" Title=\"Tab 1\"><Body></Body></{0}:Tab><{0}:Tab runat=\"server\" Title=\"Tab 2\"><Body></Body></{0}:Tab></Tabs></{0}:TabPanel></{0}:FitLayout></Body></{0}:Panel></East><South Collapsible=\"true\" Split=\"true\"><{0}:Panel runat=\"server\" Height=\"100\" Title=\"South\"><Body></Body></{0}:Panel></South><West Collapsible=\"true\" Split=\"true\"><{0}:Panel runat=\"server\" Title=\"West\" Width=\"175\"><Body><{0}:Accordion runat=\"server\" Animate=\"true\"><{0}:Panel runat=\"server\" Border=\"false\" Title=\"Item 1\" Collapsed=\"True\" Icon=\"FolderGo\"><Body></Body></{0}:Panel><{0}:Panel runat=\"server\" Border=\"false\" Title=\"Item 2\" Collapsed=\"true\" Icon=\"FolderWrench\"><Body></Body></{0}:Panel></{0}:Accordion></Body></{0}:Panel></West><Center><{0}:Panel runat=\"server\" Title=\"Center\"><Body><{0}:FitLayout runat=\"server\"><{0}:TabPanel runat=\"server\" ActiveTabIndex=\"0\" Title=\"Center\" Border=\"false\"><Tabs><{0}:Tab runat=\"server\" Title=\"Tab 1\" Closable=\"true\"><Body></Body></{0}:Tab><{0}:Tab runat=\"server\" Title=\"Tab 2\"><Body></Body></{0}:Tab></Tabs></{0}:TabPanel></{0}:FitLayout></Body></{0}:Panel></Center></{0}:BorderLayout></Body></{0}:ViewPort>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ViewPort), "Build.Resources.ToolboxIcons.ViewPort.bmp")]
    [Designer(typeof(ViewPortDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultProperty("Body")]
    [Description("A specialized contentContainer representing the viewable application area (the browser viewport).")]
    public class ViewPort : Container, IContent
    {
        [DefaultValue("")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The id of the node, a DOM node or an existing Element that will be the contentContainer to render this component into.")]
        public override string RenderTo
        {
            get
            {
                return this.IsInForm ? this.ParentForm.ClientID : "={Ext.getBody()}";
            }
            internal set
            {
                base.RenderTo = value;
            }
        }

        protected override bool RemoveContainer
        {
            get
            {
                return true;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [NotifyParentProperty(true)]
        [ReadOnly(true)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [NotifyParentProperty(true)]
        [ReadOnly(true)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        private ContainerListeners listeners;

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
        public ContainerListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ContainerListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private ContainerAjaxEvents ajaxEvents;

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
        public ContainerAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ContainerAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }


        /*  IContent
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// The id of an existing HTML node to use as the panel's body content (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id of an existing HTML node to use as the panel's body content (defaults to '').")]
        public virtual string ContentEl
        {
            get
            {
                return "";
            }
        }

        private ITemplate body;

        [DefaultValue(null)]
        [Browsable(false)]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ITemplate Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
                if (value != null)
                {
                    value.InstantiateIn(this.BodyContainer);
                }
            }
        }

        private HtmlGenericControl bodyContainer;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HtmlGenericControl BodyContainer
        {
            get
            {
                if (this.bodyContainer == null)
                {
                    this.bodyContainer = this.CreateContainer();
                }
                return this.bodyContainer;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.BodyContainer != null && !this.DesignMode)
            {
                this.BodyContainer.ID = string.Concat(this.ID, "_Body");
            }

            base.OnPreRender(e);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlCollection BodyControls
        {
            get
            {
                return this.BodyContainer.Controls;
            }
        }
    }
}