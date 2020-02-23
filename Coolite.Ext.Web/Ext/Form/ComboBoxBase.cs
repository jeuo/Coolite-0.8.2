/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A combobox control with support for autocomplete, remote-loading, paging and many other features.
    /// </summary>
    [Xtype("coolitetriggercombo")]
    [Description("A combobox control with support for autocomplete, remote-loading, paging and many other features.")]
    [ClientStyle(Type = typeof(TriggerField), DefaultOnlyStyle = true, FilePath = "/ux/extensions/triggerfield/css/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.triggerfield-embedded.css")]
    [ClientStyle(Type = typeof(TriggerField), Theme = Theme.Gray, FilePath = "/ux/extensions/triggerfield/css/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.triggerfield-embedded.css")]
    [ClientStyle(Type = typeof(TriggerField), Theme = Theme.Slate, FilePath = "/ux/extensions/triggerfield/css/slate/triggerfield.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.triggerfield.css.slate.triggerfield-embedded.css")]
    public abstract class ComboBoxBase<T> : TriggerFieldBase where T : StateManagedItem 
    {
        /// <summary>
        /// The text query to send to the server to return all records for the list with no filtering (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text query to send to the server to return all records for the list with no filtering (defaults to '').")]
        public virtual string AllQuery
        {
            get
            {
                return (string)this.ViewState["AllQuery"] ?? "";
            }
            set
            {
                this.ViewState["AllQuery"] = value;
            }
        }

        /// <summary>
        /// The underlying data field name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'text' if transforming a select).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The underlying data field name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'text' if transforming a select).")]
        public virtual string DisplayField 
        {
            get
            {
                return (string)this.ViewState["DisplayField"] ?? "text";
            }
            set
            {
                this.ViewState["DisplayField"] = value;
            }
        }

        /// <summary>
        /// False to prevent the user from typing text directly into the field, just like a traditional select (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("False to prevent the user from typing text directly into the field, just like a traditional select (defaults to true).")]
        public virtual bool Editable 
        {
            get
            {
                object obj = this.ViewState["Editable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Editable"] = value;
            }
        }

        /// <summary>
        /// True to restrict the selected value to one of the values in the list, false to allow the user to set arbitrary text into the field (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("True to restrict the selected value to one of the values in the list, false to allow the user to set arbitrary text into the field (defaults to true).")]
        public virtual bool ForceSelection
        {
            get
            {
                object obj = this.ViewState["ForceSelection"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ForceSelection"] = value;
            }
        }

        /// <summary>
        /// The height in pixels of the dropdown list resize handle if resizable = true (defaults to 8).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "8")]
        [Description("The height in pixels of the dropdown list resize handle if resizable = true (defaults to 8).")]
        public virtual Unit HandleHeight
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["HandleHeight"], Unit.Pixel(8), "HandleHeight");
            }
            set
            {
                this.ViewState["HandleHeight"] = value;
            }
        }

        /// <summary>
        /// If specified, a hidden form field with this name is dynamically generated to store the field's data value (defaults to the underlying DOM element's name). Required for the combo's value to automatically post during a form submission.
        /// </summary>
        [ClientConfig]
        [DefaultValue("")]
        [Description("If specified, a hidden form field with this name is dynamically generated to store the field's data value (defaults to the underlying DOM element's name). Required for the combo's value to automatically post during a form submission.")]
        internal virtual string HiddenName
        {
            get
            {
                return this.AllowCustomValue ? "" : string.Concat(this.UniqueName, "_Value");
            }
        }

        /// <summary>
        /// This setting is required if a custom XTemplate has been specified in tpl which assigns a class other than 'x-combo-list-item' to dropdown list items. A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to determine what nodes the DataView which handles the dropdown display will be working with.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("This setting is required if a custom XTemplate has been specified in tpl which assigns a class other than 'x-combo-list-item' to dropdown list items. A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to determine what nodes the DataView which handles the dropdown display will be working with.")]
        public virtual string ItemSelector
        {
            get
            {
                return (string)this.ViewState["ItemSelector"] ?? "";
            }
            set
            {
                this.ViewState["ItemSelector"] = value;
            }
        }

        /// <summary>
        /// True to not initialize the list for this combo until the field is focused. (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("True to not initialize the list for this combo until the field is focused. (defaults to true).")]
        public virtual bool LazyInit
        {
            get
            {
                object obj = this.ViewState["LazyInit"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["LazyInit"] = value;
            }
        }

        /// <summary>
        /// True to prevent the ComboBox from rendering until requested (should always be used when rendering into an Ext.Editor, defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("True to prevent the ComboBox from rendering until requested (should always be used when rendering into an Ext.Editor, defaults to false).")]
        public virtual bool LazyRender
        {
            get
            {
                object obj = this.ViewState["LazyRender"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["LazyRender"] = value;
            }
        }

        /// <summary>
        /// True to fire select event after setValue on page load
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("True to fire select event after setValue on page load")]
        public virtual bool FireSelectOnLoad
        {
            get
            {
                object obj = this.ViewState["FireSelectOnLoad"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["FireSelectOnLoad"] = value;
            }
        }

        /// <summary>
        /// A valid anchor position value. See Ext.Element.alignTo for details on supported anchor positions (defaults to 'tl-bl').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A valid anchor position value. See Ext.Element.alignTo for details on supported anchor positions (defaults to 'tl-bl').")]
        public virtual string ListAlign
        {
            get
            {
                return (string)this.ViewState["ListAlign"] ?? "";
            }
            set
            {
                this.ViewState["ListAlign"] = value;
            }
        }

        /// <summary>
        /// CSS class to apply to the dropdown list element (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("CSS class to apply to the dropdown list element (defaults to '').")]
        public virtual string ListClass
        {
            get
            {
                return (string)this.ViewState["ListClass"] ?? "";
            }
            set
            {
                this.ViewState["ListClass"] = value;
            }
        }

        /// <summary>
        /// The width in pixels of the dropdown list (defaults to the width of the ComboBox field).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The width in pixels of the dropdown list (defaults to the width of the ComboBox field).")]
        public virtual Unit ListWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["ListWidth"], Unit.Empty, "ListWidth");
            }
            set
            {
                this.ViewState["ListWidth"] = value;
            }
        }

        /// <summary>
        /// The text to display in the dropdown list while data is loading. Only applies when mode = 'remote' (defaults to 'Loading...').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Loading...")]
        [Localizable(true)]
        [Description("The text to display in the dropdown list while data is loading. Only applies when mode = 'remote' (defaults to 'Loading...').")]
        public virtual string LoadingText
        {
            get
            {
                return (string)this.ViewState["LoadingText"] ?? "Loading...";
            }
            set
            {
                this.ViewState["LoadingText"] = value;
            }
        }

        /// <summary>
        /// The maximum height in pixels of the dropdown list before scrollbars are shown (defaults to 300).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "300")]
        [Description("The maximum height in pixels of the dropdown list before scrollbars are shown (defaults to 300).")]
        public virtual Unit MaxHeight
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MaxHeight"], Unit.Pixel(300), "MaxHeight");
            }
            set
            {
                this.ViewState["MaxHeight"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "90")]
        [Description("The minimum height in pixels of the dropdown list when the list is constrained by its distance to the viewport edges (defaults to 90).")]
        public virtual Unit MinHeight
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinHeight"], Unit.Pixel(90), "MinHeight");
            }
            set
            {
                this.ViewState["MinHeight"] = value;
            }
        }

        /// <summary>
        /// The minimum number of characters the user must type before autocomplete and typeahead activate (defaults to 4 if remote or 0 if local, does not apply if editable = false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(4)]
        [Description("The minimum number of characters the user must type before autocomplete and typeahead activate (defaults to 4 if remote or 0 if local, does not apply if editable = false).")]
        public virtual int MinChars
        {
            get
            {
                object obj = this.ViewState["MinChars"];
                return (obj == null) ? 4 : (int)obj;
            }
            set
            {
                this.ViewState["MinChars"] = value;
            }
        }

        /// <summary>
        /// The minimum width of the dropdown list in pixels (defaults to 70, will be ignored if listWidth has a higher value).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "70")]
        [Description("The minimum width of the dropdown list in pixels (defaults to 70, will be ignored if listWidth has a higher value).")]
        public virtual Unit MinListWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinListWidth"], Unit.Pixel(70), "MinListWidth");
            }
            set
            {
                this.ViewState["MinListWidth"] = value;
            }
        }

        /// <summary>
        /// Set to 'local' if the ComboBox loads local data (defaults to 'remote' which loads from the server).
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(DataLoadMode.Remote)]
        [Description("Set to 'local' if the ComboBox loads local data (defaults to 'remote' which loads from the server).")]
        public virtual DataLoadMode Mode
        {
            get
            {
                object obj = this.ViewState["Mode"];
                return (obj == null) ? DataLoadMode.Remote : (DataLoadMode)obj;
            }
            set
            {
                this.ViewState["Mode"] = value;
            }
        }

        /// <summary>
        /// If greater than 0, a paging toolbar is displayed in the footer of the dropdown list and the filter queries will execute with page addToStart and limit parameters. Only applies when mode = 'remote' (defaults to 0).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("If greater than 0, a paging toolbar is displayed in the footer of the dropdown list and the filter queries will execute with page addToStart and limit parameters. Only applies when mode = 'remote' (defaults to 0).")]
        public virtual int PageSize
        {
            get
            {
                object obj = this.ViewState["PageSize"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        /// <summary>
        /// The length of time in milliseconds to delay between the addToStart of typing and sending the query to filter the dropdown list (defaults to 500 if mode = 'remote' or 10 if mode = 'local').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(500)]
        [Description("The length of time in milliseconds to delay between the addToStart of typing and sending the query to filter the dropdown list (defaults to 500 if mode = 'remote' or 10 if mode = 'local').")]
        public virtual int QueryDelay
        {
            get
            {
                object obj = this.ViewState["QueryDelay"];
                return (obj == null) ? (this.Mode == DataLoadMode.Local) ? 10 : 500 : (int)obj;
            }
            set
            {
                this.ViewState["QueryDelay"] = value;
            }
        }

        /// <summary>
        /// Name of the query as it will be passed on the querystring (defaults to 'query').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("query")]
        [Description("Name of the query as it will be passed on the querystring (defaults to 'query').")]
        public virtual string QueryParam
        {
            get
            {
                return (string)this.ViewState["QueryParam"] ?? "query";
            }
            set
            {
                this.ViewState["QueryParam"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to add a resize handle to the bottom of the dropdown list (defaults to false)")]
        public virtual bool Resizable
        {
            get
            {
                object obj = this.ViewState["Resizable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Resizable"] = value;
            }
        }

        /// <summary>
        /// CSS class to apply to the selected items in the dropdown list (defaults to 'x-combo-selected').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("CSS class to apply to the selected items in the dropdown list (defaults to 'x-combo-selected').")]
        public virtual string SelectedClass
        {
            get
            {
                return (string)this.ViewState["SelectedClass"] ?? "";
            }
            set
            {
                this.ViewState["SelectedClass"] = value;
            }
        }

        /// <summary>
        /// 'Sides' for the default effect, 'Frame' for 4-way shadow, and 'Drop' for bottom-right.
        /// </summary>
        [ClientConfig(typeof(ShadowJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ShadowMode.Sides)]
        [Description("'Sides' for the default effect, 'Frame' for 4-way shadow, and 'Drop' for bottom-right.")]
        public virtual ShadowMode Shadow
        {
            get
            {
                object obj = this.ViewState["Shadow"];
                return (obj == null) ? ShadowMode.Sides : (ShadowMode)obj;
            }
            set
            {
                this.ViewState["Shadow"] = value;
            }
        }

        [ClientConfig("shadow")]
        [Category("Config Options")]
        [DefaultValue(true)]
        public virtual bool EnableShadow
        {
            get
            {
                object obj = this.ViewState["EnableShadow"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnableShadow"] = value;
            }
        }

        /// <summary>
        /// True to automatically select any existing field text when the field receives input focus (defaults to false).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to automatically select any existing field text when the field receives input focus (defaults to false).")]
        public override bool SelectOnFocus
        {
            get
            {
                object obj = this.ViewState["SelectOnFocus"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SelectOnFocus"] = value;
            }
        }

        [ClientConfig("selectOnFocus")]
        [DefaultValue(false)]
        internal bool SelectOnFocusProxy
        {
            get
            {
                return (this.Editable) ? this.SelectOnFocus : false;
            }
        }

        private XTemplate template;
        /// <summary>
        /// The template string to use to display each item in the dropdown list.
        /// </summary>
        [Category("Config Options")]
        [Description("The template string to use to display each item in the dropdown list.")]
        [ClientConfig("tpl", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
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

        /// <summary>
        /// The ID of an existing select to convert to a ComboBox.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The ID of an existing select to convert to a ComboBox.")]
        public virtual string Transform
        {
            get
            {
                return (string)this.ViewState["Transform"] ?? "";
            }
            set
            {
                this.ViewState["Transform"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("If supplied, a header element is created containing this text and added into the top of the dropdown list.")]
        public virtual string Title
        {
            get
            {
                return (string)this.ViewState["Title"] ?? "";
            }
            set
            {
                this.ViewState["Title"] = value;
            }
        }

        /// <summary>
        /// The action to execute when the trigger field is activated. Use 'All' to run the query specified by the allQuery config option (defaults to 'Query').
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(TriggerAction.Query)]
        [Description("The action to execute when the trigger field is activated. Use 'All' to run the query specified by the allQuery config option (defaults to 'Query').")]
        public virtual TriggerAction TriggerAction
        {
            get
            {
                object obj = this.ViewState["TriggerAction"];
                return (obj == null) ? TriggerAction.All : (TriggerAction)obj;
            }
            set
            {
                this.ViewState["TriggerAction"] = value;
            }
        }

        /// <summary>
        /// True to populate and autoselect the remainder of the text being typed after a configurable delay (typeAheadDelay) if it matches a known value (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("True to populate and autoselect the remainder of the text being typed after a configurable delay (typeAheadDelay) if it matches a known value (defaults to false).")]
        public virtual bool TypeAhead
        {
            get
            {
                object obj = this.ViewState["TypeAhead"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TypeAhead"] = value;
            }
        }

        /// <summary>
        /// The length of time in milliseconds to wait until the typeahead text is displayed if TypeAhead = true (defaults to 250).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(250)]
        [Description("The length of time in milliseconds to wait until the typeahead text is displayed if TypeAhead = true (defaults to 250).")]
        public virtual int TypeAheadDelay
        {
            get
            {
                object obj = this.ViewState["TypeAheadDelay"];
                return (obj == null) ? 250 : (int)obj;
            }
            set
            {
                this.ViewState["TypeAheadDelay"] = value;
            }
        }



        /// <summary>
        /// The underlying data value name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'value' if transforming a select) Note: use of a valueField requires the user to make a selection in order for a value to be mapped.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The underlying data value name to bind to this ComboBox (defaults to undefined if mode = 'remote' or 'value' if transforming a select) Note: use of a valueField requires the user to make a selection in order for a value to be mapped.")]
        public virtual string ValueField
        {
            get
            {
                return (string)this.ViewState["ValueField"] ?? (this.AllowCustomValue ? "" : "value");
            }
            set
            {
                this.ViewState["ValueField"] = value;
            }
        }

        private bool allowCustomValue;
        internal virtual bool AllowCustomValue
        {
            get
            {
                return false;
            }
            set
            {
                this.allowCustomValue = value;
            }
        }

        /// <summary>
        /// When using a name/value combo, if the value passed to setValue is not found in the store, valueNotFoundText will be displayed as the field text if defined (defaults to undefined).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("When using a name/value combo, if the value passed to setValue is not found in the store, valueNotFoundText will be displayed as the field text if defined (defaults to undefined).")]
        public virtual string ValueNotFoundText
        {
            get
            {
                return (string)this.ViewState["ValueNotFoundText"] ?? "";
            }
            set
            {
                this.ViewState["ValueNotFoundText"] = value;
            }
        }

        //[Category("Config Options")]
        //[DefaultValue("")]
        //[Description("The DataView used to display the ComboBox's options.")]
        //public virtual DataView View
        //{
        //    get
        //    {
        //        object obj = this.ViewState["View"];
        //        return (obj == null) ? null : (DataView)obj;
        //    }
        //    set
        //    {
        //        this.ViewState["View"] = value;
        //    }
        //}

        /// <summary>
        /// The data store to use.
        /// </summary>
        [ClientConfig("store", JsonMode.ToClientID)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The data store to use.")]
        [IDReferenceProperty(typeof(Store))]
        public virtual string StoreID
        {
            get
            {
                return (string)this.ViewState["StoreID"] ?? "";
            }
            set
            {
                this.ViewState["StoreID"] = value;
            }
        }

        //[Browsable(false)]
        //[AjaxEventUpdate(MethodName = "SetValue")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override object Value
        //{
        //    get { return base.Value; }
        //    set { base.Value = value; }
        //}

        private ListItem selectedItem;

        [Category("Appearance")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        [DefaultValue(null)]
        public virtual ListItem SelectedItem
        {
            get
            {
                if (this.selectedItem == null)
                {
                    this.selectedItem = new ListItem(this);
                }
                return this.selectedItem;
            }
        }

        [Category("8. ComboBox")]
        [ClientConfig("initSelectedIndex")]
        [AjaxEventUpdate(MethodName = "SetSelectedIndex")]
        [DefaultValue(-1)]
        public virtual int SelectedIndex
        {
            get
            {
                object obj = this.ViewState["SelectedIndex"];
                return (obj == null) ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["SelectedIndex"] = value;
            }
        }

        private ListItemCollection<T> items;
        
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public ListItemCollection<T> Items
        {
            get
            {
                if(items == null)
                {
                    items = new ListItemCollection<T>();
                }

                return items;
            }
        }

        protected virtual string ItemsToStore
        {
            get
            {
                StringWriter sw = new StringWriter();
                JsonTextWriter jw = new JsonTextWriter(sw);
                ListItemCollectionJsonConverter converter = new ListItemCollectionJsonConverter();
                converter.WriteJson(jw, this.Items);

                return sw.GetStringBuilder().ToString();
            }
        }

        [ClientConfig]
        [Category("Behavior")]
        [DefaultValue(true)]
        public virtual bool AlwaysMergeItems
        {
            get
            {
                object obj = this.ViewState["AlwaysMergeItems"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AlwaysMergeItems"] = value;
            }
        }

        [ClientConfig("store", JsonMode.Raw)]
        [DefaultValue("")]
        internal virtual string ItemsProxy
        {
            get
            {
                if(!string.IsNullOrEmpty(this.StoreID) || !string.IsNullOrEmpty(this.Transform))
                {
                    return "";
                }

                return this.ItemsToStore;
            }
        }

        [ClientConfig("mergeItems", JsonMode.Raw)]
        [DefaultValue("")]
        internal string MergeItems
        {
            get
            {
                if (string.IsNullOrEmpty(this.StoreID) || this.Items.Count == 0)
                {
                    return "";
                }

                return this.ItemsToStore;
            }
        }

        private FieldTrigerCollection triggers;

        [ClientConfig("triggersConfig", JsonMode.AlwaysArray)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual FieldTrigerCollection Triggers
        {
            get
            {
                if (this.triggers == null)
                {
                    this.triggers = new FieldTrigerCollection();
                }
                return this.triggers;
            }
        }

        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [Description("Trigger AutoPostBack")]
        public virtual bool TriggerAutoPostBack
        {
            get
            {
                object obj = this.ViewState["TriggerAutoPostBack"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TriggerAutoPostBack"] = value;
            }
        }

        private static readonly object EventTriggerClicked = new object();
        public delegate void TriggerClickedHandler(object sender, TriggerEventArgs e);

        [Category("Action")]
        [Description("Fires when a trigger has been clicked")]
        public event TriggerClickedHandler TriggerClicked
        {
            add
            {
                Events.AddHandler(EventTriggerClicked, value);
            }
            remove
            {
                Events.RemoveHandler(EventTriggerClicked, value);
            }
        }

        protected virtual void OnTriggerClicked(TriggerEventArgs e)
        {
            TriggerClickedHandler handler = (TriggerClickedHandler)Events[EventTriggerClicked];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        [Description("Show a trigger")]
        public virtual void ShowTrigger(int index)
        {
            Ext.EnsureAjaxEvent();
            this.Triggers[index].HideTrigger = false;
            string template = "{0}.triggers[{1}].show();";
            this.AddScript(template, this.ClientID, index);
        }

        [Description("Hide a trigger")]
        public virtual void ConcealTrigger(int index)
        {
            Ext.EnsureAjaxEvent();
            this.Triggers[index].HideTrigger = true;
            string template = "{0}.triggers[{1}].hide();";
            this.AddScript(template, this.ClientID, index);
        }

        [Description("Insert record")]
        public virtual void InsertRecord(int index, IDictionary<string,object> values)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.insertRecord({1}, {2});";
            this.AddScript(template, this.ClientID, index, JSON.Serialize(values, null, true));
        }

        [Description("Add record")]
        public virtual void AddRecord(IDictionary<string, object> values)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.addRecord({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(values, null, true));
        }

        [Description("Insert item")]
        public virtual void InsertItem(int index, string text, object value)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.insertItem({1}, {2}, {3});";
            this.AddScript(template, this.ClientID, index, JSON.Serialize(text), JSON.Serialize(value, null, true));
        }

        [Description("Add item")]
        public virtual void AddItem(string text, object value)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.addItem({1}, {2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(text), JSON.Serialize(value, null, true));
        }

        [Description("Remove by field")]
        public virtual void RemoveByField(string field, object value)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.removeByField({1}, {2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(field), JSON.Serialize(value, null, true));
        }

        [Description("Remove by index")]
        public virtual void RemoveByIndex(int index)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.removeByIndex({1});";
            this.AddScript(template, this.ClientID, index);
        }

        [Description("Remove by value")]
        public virtual void RemoveByValue(object value)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.removeByValue({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value, null, true));
        }

        [Description("Remove by text")]
        public virtual void RemoveByText(string text)
        {
            Ext.EnsureAjaxEvent();
            string template = "{0}.removeByText({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(text));
        }

        /// <summary>
        /// Sets a data value into the field and validates it. To set the value directly without validation see setRawValue.
        /// </summary>
        [Description("Sets a data value into the field and validates it. To set the value directly without validation see setRawValue.")]
        public override void SetValue(object value)
        {
            string template = "{0}.setValue({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value, null, true));
        }

        /// <summary>
        /// Clears any text/value currently set in the field
        /// </summary>
        /// <param name="value"></param>
        [Description("Clears any text/value currently set in the field")]
        public virtual void ClearValue()
        {
            string template = "{0}.clearValue();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Hides the dropdown list if it is currently expanded. Fires the collapse event on completion.
        /// </summary>
        [Description("Hides the dropdown list if it is currently expanded. Fires the collapse event on completion.")]
        public virtual void Collapse()
        {
            string template = "{0}.collapse();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Expands the dropdown list if it is currently hidden. Fires the expand event on completion.
        /// </summary>
        /// <param name="query">The SQL query to execute</param>
        /// <param name="forceAll">true to force the query to execute even if there are currently fewer characters in the field than the minimum specified by the minChars config option. It also clears any filter previously saved in the current store (defaults to false)</param>
        [Description("Expands the dropdown list if it is currently hidden. Fires the expand event on completion.")]
        public virtual void DoQuery(string query, bool forceAll)
        {
            string template = "{0}.doQuery({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(query), forceAll.ToString().ToLower());
        }

        /// <summary>
        /// Expands the dropdown list if it is currently hidden. Fires the expand event on completion.
        /// </summary>
        [Description("Expands the dropdown list if it is currently hidden. Fires the expand event on completion.")]
        public virtual void Expand()
        {
            string template = "{0}.expand();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Select an item in the dropdown list by its numeric index in the list. This function does NOT cause the select event to fire. The store must be loaded and the list expanded for this function to work, otherwise use setValue.
        /// </summary>
        /// <param name="index">The zero-based index of the list item to select</param>
        /// <param name="scrollIntoView">False to prevent the dropdown list from autoscrolling to display the selected item if it is not currently in view (defaults to true)</param>
        [Description("Select an item in the dropdown list by its numeric index in the list. This function does NOT cause the select event to fire. The store must be loaded and the list expanded for this function to work, otherwise use setValue.")]
        public virtual void Select(int index, bool scrollIntoView)
        {
            string template = "{0}.select({1},{2});";
            this.AddScript(template, this.ClientID, index, scrollIntoView.ToString().ToLower());
        }

        /// <summary>
        /// Select an item in the dropdown list by its data value. This function does NOT cause the select event to fire. The store must be loaded and the list expanded for this function to work, otherwise use setValue.
        /// </summary>
        /// <param name="value">The data value of the item to select</param>
        /// <param name="scrollIntoView">False to prevent the dropdown list from autoscrolling to display the selected item if it is not currently in view (defaults to true)</param>
        [Description("Select an item in the dropdown list by its data value. This function does NOT cause the select event to fire. The store must be loaded and the list expanded for this function to work, otherwise use setValue.")]
        public virtual void Select(string value, bool scrollIntoView)
        {
            string template = "{0}.select({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value), scrollIntoView.ToString().ToLower());
        }

        protected virtual void SetSelectedIndex(int index)
        {
            string template = "{0}.selectByIndex({1});";
            this.AddScript(template, this.ClientID, index);
        }
    }
}