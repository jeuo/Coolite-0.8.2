/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;
using System.Reflection;
using Coolite.Utilities;
using System.Drawing;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A specialized toolbar that is bound to a Ext.data.Store and provides automatic paging controls.
    /// </summary>
    [Xtype("paging")]
    [InstanceOf(ClassName = "Ext.PagingToolbar")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.PagingToolbar), "Build.Resources.ToolboxIcons.PagingToolbar.bmp")]
    [ToolboxData("<{0}:PagingToolbar runat=\"server\"></{0}:PagingToolbar>")]
    [Description("A specialized toolbar that is bound to a Ext.data.Store and provides automatic paging controls.")]
    public class PagingToolbar : ToolbarBase, IPostBackDataHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            string fn = "this.getActivePageField().setValue(data.activePage);";
            this.On("change", new JFunction(fn, "el", "data"));

            string storeId = string.IsNullOrEmpty(this.StoreID) ? this.ProxyStoreID : this.StoreID;

            if (this.PageIndex > 1 && !string.IsNullOrEmpty(storeId))
            {
                Store store = ControlUtils.FindControl<Store>(this, storeId, true);
                if (store == null)
                {
                    throw new InvalidOperationException(string.Format("The Control '{0}' could not find the StoreID of '{1}'.", this.ID, storeId));
                }

                HandlerConfig options = new HandlerConfig();
                options.Single = true;
                string template = "{0}.store.on(\"load\",{1},{2},{3});";
                this.AddScript(template,
                                this.ClientID,
                                string.Concat("function(){", this.ClientID, ".changePage(", this.PageIndex, ");}"),
                                "this",
                                options.ToJsonString()
                                );
            }
        }

        //private System.Web.UI.HtmlControls.HtmlInputHidden input;

        //protected override void CreateChildControls()
        //{
        //    base.CreateChildControls();

        //    this.input = new System.Web.UI.HtmlControls.HtmlInputHidden();
        //    this.Controls.Add(this.input);
        //    this.input.EnableViewState = false;
        //    this.input.ID = string.Concat(this.ID, "_ActivePage");
        //}

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    this.input.Value = this.PageIndex.ToString();

        //    base.Render(writer);
        //}

        /// <summary>
        /// The index of current page.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(1)]
        [Description("The index of current page.")]
        [NotifyParentProperty(true)]
        [AjaxEventUpdate(MethodName = "SetPageIndex")]
        public virtual int PageIndex
        {
            get
            {
                object obj = this.ViewState["PageIndex"];
                return (obj == null) ? 1 : (int)obj;
            }
            set
            {
                this.ViewState["PageIndex"] = value;
            }
        }

        /// <summary>
        /// True to display the displayMsg (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display the displayMsg (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool DisplayInfo
        {
            get
            {
                object obj = this.ViewState["DisplayInfo"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["DisplayInfo"] = value;
            }
        }

        /// <summary>
        /// The paging status message to display (defaults to 'Displaying {0} - {1} of {2}'). Note that this string is formatted using the braced numbers 0-2 as tokens that are replaced by the values for start, end and total respectively. These tokens should be preserved when overriding this string if showing those values is desired.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Displaying {0} - {1} of {2}")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("The paging status message to display (defaults to 'Displaying {0} - {1} of {2}'). Note that this string is formatted using the braced numbers 0-2 as tokens that are replaced by the values for start, end and total respectively. These tokens should be preserved when overriding this string if showing those values is desired.")]
        public virtual string DisplayMsg
        {
            get
            {
                return (string)this.ViewState["DisplayMsg"] ?? "Displaying {0} - {1} of {2}";
            }
            set
            {
                this.ViewState["DisplayMsg"] = value;
            }
        }

        /// <summary>
        /// The message to display when no records are found (defaults to 'No data to display').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("No data to display")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("The message to display when no records are found (defaults to 'No data to display').")]
        public virtual string EmptyMsg
        {
            get
            {
                return (string)this.ViewState["EmptyMsg"] ?? "No data to display";
            }
            set
            {
                this.ViewState["EmptyMsg"] = value;
            }
        }

        /// <summary>
        /// The number of records to display per page (defaults to 20).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(20)]
        [Description("The number of records to display per page (defaults to 20).")]
        [NotifyParentProperty(true)]
        [AjaxEventUpdate(MethodName = "SetPageSize")]
        public virtual int PageSize
        {
            get
            {
                object obj = this.ViewState["PageSize"];
                return (obj == null) ? 20 : (int)obj;
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        /// <summary>
        /// The data store to use.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The data store to use.")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string StoreID
        {
            get
            {
                return (string)ViewState["StoreID"] ?? "";
            }
            set
            {
                this.ViewState["StoreID"] = value;
            }
        }

        [ClientConfig("store", JsonMode.ToClientID)]
        [DefaultValue("")]
        internal virtual string ProxyStoreID
        {
            get
            {
                string id = this.StoreID;

                if (string.IsNullOrEmpty(id))
                {
                    Component cmp = this.ParentComponent;

                    if (cmp != null)
                    {
                        PropertyInfo property = cmp.GetType().GetProperty("StoreID");

                        if (property != null)
                        {
                            string value = property.GetValue(cmp, null) as string;

                            if (!string.IsNullOrEmpty(value))
                            {
                                id = value;
                            }
                        }
                    }
                }

                return id;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'of {0}'). Note that this string is formatted using {0} as a token that is replaced by the number of total pages. This token should be preserved when overriding this string if showing the total page count is desired.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("of {0}")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'of {0}'). Note that this string is formatted using {0} as a token that is replaced by the number of total pages. This token should be preserved when overriding this string if showing the total page count is desired.")]
        public virtual string AfterPageText
        {
            get
            {
                return (string)ViewState["AfterPageText"] ?? "of {0}";
            }
            set
            {
                this.ViewState["AfterPageText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'Page')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("µÚ")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'µÚ')")]
        public virtual string BeforePageText
        {
            get
            {
                return (string)this.ViewState["BeforePageText"] ?? "µÚ";
            }
            set
            {
                this.ViewState["BeforePageText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'First Page')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("First Page")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'First Page')")]
        public virtual string FirstText
        {
            get
            {
                return (string)this.ViewState["FirstText"] ?? "First Page";
            }
            set
            {
                this.ViewState["FirstText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'Last Page')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Last Page")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'Last Page')")]
        public virtual string LastText
        {
            get
            {
                return (string)this.ViewState["LastText"] ?? "Last Page";
            }
            set
            {
                this.ViewState["LastText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'Next Page')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Next Page")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'Next Page')")]
        public virtual string NextText
        {
            get
            {
                return (string)this.ViewState["NextText"] ?? "Next Page";
            }
            set
            {
                this.ViewState["NextText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'Previous Page')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Previous Page")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'Previous Page')")]
        public virtual string PrevText
        {
            get
            {
                return (string)this.ViewState["PrevText"] ?? "Previous Page";
            }
            set
            {
                this.ViewState["PrevText"] = value;
            }
        }

        /// <summary>
        /// Customizable piece of the default paging text (defaults to 'Refresh')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Refresh")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("Customizable piece of the default paging text (defaults to 'Refresh')")]
        public virtual string RefreshText
        {
            get
            {
                return (string)this.ViewState["RefreshText"] ?? "Refresh";
            }
            set
            {
                this.ViewState["RefreshText"] = value;
            }
        }

        private ParameterCollection paramNames;

        /// <summary>
        /// Object mapping of parameter names for load calls (defaults to {start: 'start', limit: 'limit'})
        /// </summary>
        [ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Object mapping of parameter names for load calls (defaults to {start: 'start', limit: 'limit'})")]
        public virtual ParameterCollection ParamNames
        {
            get
            {
                if (this.paramNames == null)
                {
                    this.paramNames = new ParameterCollection();
                }

                return this.paramNames;
            }
        }

        private PagingToolbarListeners listeners;

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
        public PagingToolbarListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PagingToolbarListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private PagingToolbarAjaxEvents ajaxEvents;

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
        public PagingToolbarAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PagingToolbarAjaxEvents();
                    this.ajaxEvents.InitOwners(this);
                    
                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[string.Concat(this.ClientID, "_ActivePage")];
            if (!string.IsNullOrEmpty(val))
            {
                int activePageNum;
                if (int.TryParse(val, out activePageNum))
                {
                    if (activePageNum > -1 && this.PageIndex != activePageNum)
                    {
                        try
                        {
                            this.ViewState.Suspend();
                            this.PageIndex = activePageNum;
                        }
                        finally
                        {
                            this.ViewState.Resume();
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent() { }

        public virtual void SetPageIndex(int index)
        {
            string template = "{0}.changePage({1});";
            this.AddScript(template, this.ClientID, index);
        }

        public virtual void SetPageSize(int size)
        {
            string template = "{0}.pageSize = {1}; {0}.doLoad(0);";
            this.AddScript(template, this.ClientID, size);
        }
    }
}
