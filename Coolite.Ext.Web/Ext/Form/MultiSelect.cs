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
using System.Drawing;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:MultiSelect runat=\"server\"></{0}:MultiSelect>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.MultiSelect), "Build.Resources.ToolboxIcons.MultiSelect.bmp")]
    [InstanceOf(ClassName = "Ext.ux.Multiselect")]
    [Description("A control that allows selection and form submission of multiple list items.")]
    [Xtype("multiselect")]
    [ClientScript(Type = typeof(MultiSelect), FilePath = "/ux/extensions/ddview/ddview.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.ddview.ddview.js")]
    [ClientScript(Type = typeof(MultiSelect), FilePath = "/ux/extensions/multiselect/multiselect.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.multiselect.multiselect.js")]
    [ClientStyle(Type = typeof(MultiSelect), FilePath = "/ux/extensions/multiselect/resources/css/multiselect.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.multiselect.resources.css.multiselect.css")]
    public class MultiSelect : MultiSelectBase<ListItem>, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                EventHandler handler = (EventHandler)Events[EventSelectionChanged];
                if (handler != null)
                {
                    this.On("change", new JFunction(this.PostBackFunction));
                }
            }

            if (!string.IsNullOrEmpty(this.StoreID))
            {
                Store store = ControlUtils.FindControl<Store>(this, this.StoreID, true);
                if (store == null)
                {
                    throw new InvalidOperationException(string.Format("The Control '{0}' could not find the StoreID of '{1}'.", this.ID, this.StoreID));
                }

                if (this.SelectedItems.Count > 0)
                {
                    HandlerConfig options = new HandlerConfig();
                    options.Single = true;
                    string template = "{0}.store.on(\"{1}\",{2},{3},{4});";

                    string values = this.SelectedItems.ValuesToJsonArray();
                    string indexes = this.SelectedItems.IndexesToJsonArray(true);


                    string suppressEvent = this.FireSelectOnLoad ? "false" : "true";
                    values = values != "[]" ? string.Concat(".setValue(", values, ", true, ", suppressEvent, ");") : "";
                    indexes = indexes != "[]" ? string.Concat(".setValueByIndex(", indexes, ", true, ", suppressEvent, ");") : "";

                    this.AddScript(template,
                            this.ClientID, "load",
                            string.Concat("function(){", this.ClientID, values, indexes, this.ClientID, ".clearInvalid();}"),
                            "this",
                            options.ToJsonString()
                            );
                }
            }
            else
            {
                if (this.SelectedItems.Count > 0)
                {
                    string values = this.SelectedItems.ValuesToJsonArray();
                    string indexes = this.SelectedItems.IndexesToJsonArray(true);
                    string suppressEvent = this.FireSelectOnLoad ? "false" : "true";

                    if(values != "[]")
                    {
                        this.AddScript(string.Concat(this.ClientID, ".setValue(", values, ", true, ", suppressEvent, ");")); 
                    }

                    if (indexes != "[]")
                    {
                        this.AddScript(string.Concat(this.ClientID, ".setValue(", indexes, ", true, ", suppressEvent, ");"));
                    }
                }
            }
        }

        private static readonly object EventSelectionChanged = new object();

        [Category("Action")]
        public event EventHandler SelectionChanged
        {
            add
            {
                this.Events.AddHandler(EventSelectionChanged, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventSelectionChanged, value);
            }
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)this.Events[EventSelectionChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string text = postCollection[string.Concat(this.UniqueName, "_text")];
            string values = postCollection[this.UniqueName];
            string indexes = postCollection[string.Concat(this.UniqueName, "_indexes")];

            if (values == null)
            {
                return false;
            }

            bool fireEvent = false;

            if (string.IsNullOrEmpty(values))
            {
                fireEvent = this.SelectedItems.Count > 0;
                this.SelectedItems.Clear();
                return fireEvent;
            }

            string[] arrValues = values.Split(new[] {this.Delimiter}, StringSplitOptions.RemoveEmptyEntries);
            string[] arrIndexes = indexes.Split(new[] { this.Delimiter }, StringSplitOptions.RemoveEmptyEntries);
            string[] arrText = new string[0];

            if(!string.IsNullOrEmpty(text))
            {
                arrText = text.Split(new[] { this.Delimiter }, StringSplitOptions.RemoveEmptyEntries);
            }

            SelectedListItemCollection temp = new SelectedListItemCollection();
            for (int i = 0; i < arrValues.Length; i++)
            {
                string value = arrValues[i];
                string index = arrIndexes[i];
                string _text = arrText.Length > 0 ? arrText[i] : "";

                SelectedListItem item = new SelectedListItem(_text, value, int.Parse(index));

                temp.Add(item);

                if(!this.SelectedItems.Contains(item))
                {
                    fireEvent = true;
                }
            }

            this.SelectedItems.Clear();
            this.SelectedItems.AddRange(temp);

            return fireEvent;
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnSelectionChanged(EventArgs.Empty);
        }

        
        private MultiSelectListeners listeners;

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
        public MultiSelectListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new MultiSelectListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private MultiSelectAjaxEvents ajaxEvents;

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
        public MultiSelectAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new MultiSelectAjaxEvents();
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
