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
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// History management component that allows you to register arbitrary tokens that signify application history state on navigation actions.
    /// </summary>
    [ToolboxData("<{0}:History runat=\"server\"></{0}:History>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.History), "Build.Resources.ToolboxIcons.History_New.bmp")]
    [InstanceOf(ClassName = "Ext.History")]
    [Description("History management component that allows you to register arbitrary tokens that signify application history state on navigation actions.")]
    public class History : Observable, ICustomConfigSerialization, IVirtual
    {
        public string Serialize(Control owner)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Ext.apply(Ext.History, {0});", new ClientConfig(true).Serialize(this, true));
            if(this.IsIDRequired)
            {
                sb.AppendFormat("this.{0} = Ext.History;", this.ClientID);
            }
            
            sb.Append("Ext.History.init();");

            return sb.ToString();
        }

        public static History GetCurrent(Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            return page.Items[typeof(History)] as History;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!this.DesignMode)
            {
                History existingInstance = History.GetCurrent(this.Page);

                if (existingInstance != null && !DesignMode)
                {
                    throw new InvalidOperationException("Only one History control is allowed");
                }

                this.Page.Items[typeof(History)] = this;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to don't render form tags. By default check ASP.NET form and if it is absent then render form.")]
        public virtual bool RenderForm
        {
            get
            {
                object obj = this.ViewState["RenderForm"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["RenderForm"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if(!this.IsInForm && this.RenderForm)
            {
                writer.Write("<form id=\"history-form\" class=\"x-hidden\">");
            }
            else
            {
                writer.Write("<div class=\"x-hidden\">");
            }

            writer.Write("<input type=\"hidden\" id=\"x-history-field\" /><iframe id=\"x-history-frame\"></iframe>");

            if (!this.IsInForm && this.RenderForm)
            {
                writer.Write("</form>");
            }
            else
            {
                writer.Write("</div>");
            }
        }

        private HistoryListeners listeners;

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
        public HistoryListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new HistoryListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private HistoryAjaxEvents ajaxEvents;

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
        public HistoryAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new HistoryAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        /// <summary>
        /// Add a new token to the history stack. This can be any arbitrary value, although it would commonly be the concatenation of a component id and another id marking the specifc history state of that component.
        /// </summary>
        [Description("Add a new token to the history stack. This can be any arbitrary value, although it would commonly be the concatenation of a component id and another id marking the specifc history state of that component.")]
        public virtual void Add(string token, bool preventDuplicate)
        {
            this.AddScript("Ext.History.add({0}, {1});", JSON.Serialize(token), JSON.Serialize(preventDuplicate));
        }

        /// <summary>
        /// Add a new token to the history stack. This can be any arbitrary value, although it would commonly be the concatenation of a component id and another id marking the specifc history state of that component.
        /// </summary>
        [Description("Add a new token to the history stack. This can be any arbitrary value, although it would commonly be the concatenation of a component id and another id marking the specifc history state of that component.")]
        public virtual void Add(string token)
        {
            this.AddScript("Ext.History.add({0});", JSON.Serialize(token));
        }

        /// <summary>
        /// Programmatically steps back one step in browser history (equivalent to the user pressing the Back button).
        /// </summary>
        [Description("Programmatically steps back one step in browser history (equivalent to the user pressing the Back button).")]
        public virtual void Back()
        {
            this.AddScript("Ext.History.back();");
        }

        /// <summary>
        /// Programmatically steps forward one step in browser history (equivalent to the user pressing the Forward button).
        /// </summary>
        [Description("Programmatically steps forward one step in browser history (equivalent to the user pressing the Forward button).")]
        public virtual void Forward()
        {
            this.AddScript("Ext.History.forward();");
        }
    }
}