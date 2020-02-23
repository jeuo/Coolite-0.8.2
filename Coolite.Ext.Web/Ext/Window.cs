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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A specialized panel intended for use as an application window. Windows are floated and draggable by default, and also provide specific behavior like the ability to maximize and restore (with an event for minimizing, since the minimize behavior is application-specific). Windows can also be linked to a Ext.WindowGroup or managed by the Ext.WindowManager to provide grouping, activation, to front/back and other application-specific behavior.
    /// </summary>
    [ToolboxData("<{0}:Window runat=\"server\" Title=\"Title\" Collapsible=\"true\" Icon=\"Application\"><Body></Body></{0}:Window>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Window), "Build.Resources.ToolboxIcons.Window.bmp")]
    [Designer(typeof(WindowDesigner))]
    [Description("A specialized panel intended for use as an application window. Windows are floated and draggable by default, and also provide specific behavior like the ability to maximize and restore (with an event for minimizing, since the minimize behavior is application-specific). Windows can also be linked to a Ext.WindowGroup or managed by the Ext.WindowManager to provide grouping, activation, to front/back and other application-specific behavior.")]
    public class Window : WindowBase
    {
        public Window() { }

        public Window(string title) : this(title, Icon.None) { }

        public Window(string title, Icon icon)
        {
            this.Title = title;
            this.Icon = icon;
        }

        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            //Maximizable
            this.SetMaximizable();

            // CenterOnLoad
            if (this.CenterOnLoad && this.IsInForm && !string.IsNullOrEmpty(this.RenderTo) && this.X == 0 && this.Y == 0)
            {
                /// TODO: Add functionality so Window will maintain relative position if page is scrolled.
                
                //this.On("show", new JFunction("{0}.scrollAdj=Ext.getBody().getScroll();console.log(\"show\",{0}.scrollAdj);", this.ClientID));
                //this.On("move", new JFunction("{0}.scrollAdj=Ext.getBody().getScroll();console.log(\"move\",{0}.scrollAdj);", this.ClientID));
                
                string fn = string.Format("{0}.on(\"beforeshow\",function(el){{el.alignTo(Ext.getBody(),\"c-c\");}},this,{{single:true}});",this.ClientID);
                //string fn = string.Format("this.{0}.on(\"beforeshow\", function(el){{el.anchorTo(Ext.getBody(), \"c-c\",[0,0],true,true);}});", this.ClientID);
                //string fn = string.Concat("this.", this.ClientID, ".on(\"beforeshow\", function(el){el.anchorTo(Ext.getBody(),\"c-c\",[0,0],{easing:\"easeOut\"},true);});");

                string scroll = "";
                //string scroll = string.Concat("Ext.EventManager.on(window,\"scroll\",function(){this.getEl().move(\"b\",50,true)},", this.ClientID, ",{buffer: 50});");
                this.ScriptManager.RegisterAfterClientInitScript(string.Concat(fn, scroll));
            }

            if (this.LoadMask.ShowMask && !this.AutoLoad.IsDefault)
            {
                // Stomp on Show Listener if adding LoadMask.
                // Required because listener must be wired up during instantiation. 

                string loadMask = string.Concat("el.loadMask=new Ext.LoadMask(el.body,", new ClientConfig().Serialize(this.LoadMask), ");");
                this.Listeners.Show.Handler = string.Concat(StringUtils.EnsureSemiColon(this.Listeners.Show.Handler), loadMask);
            }

            if (this.AutoRender && this.ShowOnLoad)
            {
                this.ScriptManager.RegisterAfterClientInitScript(string.Format("{0}.show();", this.ClientID));
            }
        }

        protected virtual void SetMaximizable()
        {
            if (this.Maximizable)
            {
                string fn = "var v=Ext.getBody().getViewSize();el.setSize(v.width,v.height);";
                this.On("maximize", new JFunction(fn, "el"));
            }
        }

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

        /// <summary>
        /// Show this window in the viewport when the Page loads.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Show this window in the viewport when the Page loads.")]
        public virtual bool ShowOnLoad
        {
            get
            {
                object obj = this.ViewState["ShowOnLoad"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ShowOnLoad"] = value;
            }
        }

        /// <summary>
        /// Centers this window in the viewport when the Page loads.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Centers this window in the viewport when the Page loads.")]
        public virtual bool CenterOnLoad
        {
            get
            {
                object obj = this.ViewState["CenterOnLoad"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CenterOnLoad"] = value;
            }
        }

        private WindowListeners listeners;

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
        public WindowListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new WindowListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private WindowAjaxEvents ajaxEvents;

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
        public WindowAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new WindowAjaxEvents();
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