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

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.grid.RowExpander")]
    public class RowExpander : OuterPlugin
    {
        [ClientConfig("proxyId")]
        protected override string ClientIDProxy
        {
            get { return base.ClientIDProxy; }
        }

        private XTemplate template;

        /// <summary>
        /// The template string to use to display each item in the dropdown list.
        /// </summary>
        [Category("Config Options")]
        [ClientConfig("tpl", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("The template string to use to display each item in the dropdown list.")]
        public virtual XTemplate Template
        {
            get
            {
                if (this.template == null)
                {
                    this.template = new XTemplate();
                }

                return this.template;
            }
        }

        [DefaultValue(0)]
        public int ColumnPosition
        {
            get
            {
                object obj = this.ViewState["ColumnPosition"];
                return obj != null ? (int) obj : 0;
            }
            set
            {
                this.ViewState["ColumnPosition"] = value;
            }
        }

        [DefaultValue(true)]
        [ClientConfig]
        [NotifyParentProperty(true)]
        public bool Collapsed
        {
            get
            {
                object obj = this.ViewState["Collapsed"];
                return obj != null ? (bool)obj : true;
            }
            set
            {
                this.ViewState["Collapsed"] = value;
            }
        }

        private JFunction prepare;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction Prepare
        {
            get
            {
                if (this.prepare == null)
                {
                    this.prepare = new JFunction();
                    this.prepare.Args = new string[] { "record"};
                }
                return this.prepare;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Page.LoadComplete += Page_LoadComplete;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                GridPanel parent = this.ParentComponent as GridPanel;

                if (parent == null)
                {
                    throw new InvalidOperationException("The RowExpander plugin can only be added to the GridPanel Control or a Control which inherits from GridPanel.");
                }

                parent.Controls.Add(this.Template);
                parent.ColumnModel.BeforeClientInit += GridPanel_BeforeClientInit;
            }
        }

        private void GridPanel_BeforeClientInit(Observable sender)
        {
            GridPanel parent = this.ParentComponent as GridPanel;
            parent.ColumnModel.Columns.Insert(this.ColumnPosition, new ReferenceColumn(this.ClientID));
            this.Template.EnsureScriptRegistering(true);
            this.ScriptManager.RegisterBeforeClientInitScript(this.GetClientConstructor());
        }

        private RowExpanderListeners listeners;

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
        public RowExpanderListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new RowExpanderListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private RowExpanderAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side AjaxEvent Handlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side AjaxEventHandlers")]
        public RowExpanderAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new RowExpanderAjaxEvents();
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