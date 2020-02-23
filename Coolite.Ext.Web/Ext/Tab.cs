/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    public class Tab : ContentPanel
    {
        public Tab()
        {
            this.Title = "Tab";
        }

        public Tab(string title)
        {
            this.Title = title;
        }

        public Tab(string id, string title)
        {
            this.ID = id;
            this.Title = title;
        }

        public Tab(string id, string title, string html)
        {
            this.ID = id;
            this.Title = title;
            this.Html = html;
        }

        internal override bool IsDeferredRender
        {
            get
            {
                if (this.Visible && this.ParentComponent is TabPanel)
                {
                    TabPanel tp = (TabPanel)this.ParentComponent;
                    {
                        if (tp.AutoPostBack && tp.DeferredRender && tp.Tabs[tp.ActiveTabIndex].ID != this.ID)
                        {
                            this.BodyContainer.Visible = false;
                            return true;
                        }
                        else
                        {
                            this.BodyContainer.Visible = true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// True to display the 'close' button and allow the user to close the tab, false to hide the button and disallow closing the tab (default to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to display the 'close' button and allow the user to close the tab, false to hide the button and disallow closing the tab (default to false).")]
        public virtual bool Closable 
        {
            get
            {
                object obj = this.ViewState["Closable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Closable"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(CloseAction.Close)]
        [Description("The action to take when the close button is clicked. The default action is 'close' which will actually remove the tab from the DOM and destroy it. The other valid option is 'hide' which will simply hide the tab by setting visibility to hidden and applying negative offsets, keeping the tab available to be redisplayed via the show method.")]
        public virtual CloseAction CloseAction
        {
            get
            {
                object obj = this.ViewState["CloseAction"];
                return (obj == null) ? CloseAction.Close : (CloseAction)obj;
            }
            set
            {
                this.ViewState["CloseAction"] = value;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.BodyContainer != null && !this.DesignMode)
            {
                this.BodyContainer.Attributes.Add("class", "x-hidden");
            }

            this.CssClass = "x-hidden";
            base.Render(writer);
        }

        private TabListeners listeners;

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
        public TabListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TabListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private TabAjaxEvents ajaxEvents;

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
        public TabAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TabAjaxEvents();
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