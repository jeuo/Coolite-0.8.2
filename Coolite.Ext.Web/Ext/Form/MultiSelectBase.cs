/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public abstract class MultiSelectBase<T> : Field where T : StateManagedItem 
    {
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

        private ListItemCollection<T> items;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public ListItemCollection<T> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ListItemCollection<T>();
                }

                return this.items;
            }
        }

        [ClientConfig("store", JsonMode.Raw)]
        [DefaultValue("")]
        internal string ItemsProxy
        {
            get
            {
                if (!string.IsNullOrEmpty(this.StoreID))
                {
                    return "";
                }

                return this.ItemsToStore;
            }
        }

        private string ItemsToStore
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

        private SelectedListItemCollection selectedItems;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [ViewStateMember]
        public SelectedListItemCollection SelectedItems
        {
            get
            {
                if (this.selectedItems == null)
                {
                    this.selectedItems = new SelectedListItemCollection();
                }

                return this.selectedItems;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The underlying data field name to bind to this MultiSelect.")]
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

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The underlying data value name to bind to this MultiSelect.")]
        public virtual string ValueField
        {
            get
            {
                return (string)this.ViewState["ValueField"] ?? "value";
            }
            set
            {
                this.ViewState["ValueField"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to validate that the value length > 0 (defaults to true).")]
        public virtual bool AllowBlank
        {
            get
            {
                object obj = this.ViewState["AllowBlank"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowBlank"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(-1)]
        [Description("Maximum input field length allowed (defaults to Number.MAX_VALUE).")]
        public virtual int MaxLength
        {
            get
            {
                object obj = this.ViewState["MaxLength"];
                return (obj == null) ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["MaxLength"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("Minimum input field length required (defaults to 0).")]
        public virtual int MinLength
        {
            get
            {
                object obj = this.ViewState["MinLength"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["MinLength"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Error text to display if the maximum length validation fails (defaults to 'The maximum length for this field is {maxLength}').")]
        public virtual string MaxLengthText
        {
            get
            {
                return (string)this.ViewState["MaxLengthText"] ?? "";
            }
            set
            {
                this.ViewState["MaxLengthText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Error text to display if the minimum length validation fails (defaults to 'The minimum length for this field is {minLength}').")]
        public virtual string MinLengthText
        {
            get
            {
                return (string)this.ViewState["MinLengthText"] ?? "";
            }
            set
            {
                this.ViewState["MinLengthText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Error text to display if the allow blank validation fails (defaults to 'This field is required').")]
        public virtual string BlankText
        {
            get
            {
                return (string)this.ViewState["BlankText"] ?? "";
            }
            set
            {
                this.ViewState["BlankText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Causes drag operations to copy nodes rather than move (defaults to false).")]
        public virtual bool Copy
        {
            get
            {
                object obj = this.ViewState["Copy"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Copy"] = value;
            }
        }

        [ClientConfig("allowDup")]
        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool AllowDuplicates
        {
            get
            {
                object obj = this.ViewState["AllowDuplicates"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDuplicates"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool AllowTrash
        {
            get
            {
                object obj = this.ViewState["AllowTrash"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AllowTrash"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The title text to display in the panel header (defaults to '')")]
        public virtual string Legend
        {
            get
            {
                return (string)this.ViewState["Legend"] ?? "";
            }
            set
            {
                this.ViewState["Legend"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(",")]
        [Description("The string used to delimit between items when set or returned as a string of values")]
        public virtual string Delimiter
        {
            get
            {
                return (string)this.ViewState["Delimiter"] ?? ",";
            }
            set
            {
                this.ViewState["Delimiter"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The ddgroup name(s) for the View's DragZone (defaults to undefined).")]
        public virtual string DragGroup
        {
            get
            {
                return (string)this.ViewState["DragGroup"] ?? "";
            }
            set
            {
                this.ViewState["DragGroup"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The ddgroup name(s) for the View's DropZone (defaults to undefined).")]
        public virtual string DropGroup
        {
            get
            {
                return (string)this.ViewState["DropGroup"] ?? "";
            }
            set
            {
                this.ViewState["DropGroup"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool AppendOnly
        {
            get
            {
                object obj = this.ViewState["AppendOnly"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AppendOnly"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        public virtual string SortField
        {
            get
            {
                return (string)this.ViewState["SortField"] ?? "";
            }
            set
            {
                this.ViewState["SortField"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(SortDirection.ASC)]
        [NotifyParentProperty(true)]
        public SortDirection Direction
        {
            get
            {
                object obj = this.ViewState["Direction"];
                return (obj == null) ? SortDirection.ASC : (SortDirection)obj;
            }
            set
            {
                this.ViewState["Direction"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to submit text of selected items")]
        public virtual bool SubmitText
        {
            get
            {
                object obj = this.ViewState["SubmitText"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SubmitText"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Set init selecetion without event fires")]
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

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to allow multi selection (defaults to true).")]
        public virtual bool MultiSelect
        {
            get
            {
                object obj = this.ViewState["MultiSelect"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["MultiSelect"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(KeepSelectionMode.Always)]
        [Description("True to allow multi selection (defaults to true).")]
        public virtual KeepSelectionMode KeepSelectionOnClick
        {
            get
            {
                object obj = this.ViewState["KeepSelectionOnClick"];
                return (obj == null) ? KeepSelectionMode.Always : (KeepSelectionMode)obj;
            }
            set
            {
                this.ViewState["KeepSelectionOnClick"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Custom CSS styles to be applied to the body element in the format expected by Ext.Element.applyStyles (defaults to null).")]
        [NotifyParentProperty(true)]
        public virtual string BodyStyle
        {
            get
            {
                string style = (string)this.ViewState["BodyStyle"] ?? "";
                if (!string.IsNullOrEmpty(style))
                {
                    if (!style.EndsWith(";"))
                    {
                        style += ";";
                    }
                }
                return style;
            }
            set
            {
                this.ViewState["BodyStyle"] = value;
            }
        }

        //[Category("Config Options")]
        //[DefaultValue(SelectionMemoryMode.Auto)]
        //[Description("True to enable saving selection during paging. Default is true.")]
        //public virtual SelectionMemoryMode SelectionMemory
        //{
        //    get
        //    {
        //        object obj = this.ViewState["SelectionMemory"];
        //        return (obj == null) ? SelectionMemoryMode.Auto : (SelectionMemoryMode)obj;
        //    }
        //    set
        //    {
        //        this.ViewState["SelectionMemory"] = value;
        //    }
        //}

        //[ClientConfig("selectionMemory")]
        //[DefaultValue(true)]
        //internal bool SelectionMemoryProxy
        //{
        //    get
        //    {
        //        switch (this.SelectionMemory)
        //        {
        //            case SelectionMemoryMode.Auto:
        //                return !string.IsNullOrEmpty(this.PbarID);
        //            case SelectionMemoryMode.Disabled:
        //                return false;
        //            case SelectionMemoryMode.Enabled:
        //                return true;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //    }
        //}

        //[ClientConfig("pbarID")]
        //[Category("Config Options")]
        //[DefaultValue("")]
        //internal virtual string PbarID
        //{
        //    get
        //    {
        //        PagingToolBar pBar = null;
        //        if (this.BottomBar.Count > 0 && this.BottomBar[0] is PagingToolBar)
        //        {
        //            pBar = this.BottomBar[0] as PagingToolBar;
        //        }
        //        else if (this.TopBar.Count > 0 && this.TopBar[0] is PagingToolBar)
        //        {
        //            pBar = this.TopBar[0] as PagingToolBar;
        //        }

        //        return (pBar != null && pBar.Visible) ? pBar.ClientID : "";
        //    }
        //}

        private ToolbarCollection bottomBar;

        /// <summary>
        /// The bottom toolbar of the panel.
        /// </summary>
        [ClientConfig("bbar", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The bottom toolbar of the panel.")]
        public virtual ToolbarCollection BottomBar
        {
            get
            {
                if (this.bottomBar == null)
                {
                    this.bottomBar = new ToolbarCollection();
                    this.bottomBar.AfterItemAdd += this.AfterItemAdd;
                }

                return this.bottomBar;
            }
        }

        private ToolbarCollection topBar;

        [ClientConfig("tbar", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The top toolbar of the panel.")]
        public virtual ToolbarCollection TopBar
        {
            get
            {
                if (this.topBar == null)
                {
                    this.topBar = new ToolbarCollection();
                    this.topBar.AfterItemAdd += this.AfterItemAdd;
                }

                return this.topBar;
            }
        }

        protected virtual void AfterItemAdd(Component item)
        {
            this.Controls.Add(item);
            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

        public void UpdateSelection()
        {
            if (this.SelectedItems.Count == 0)
            {
                this.AddScript(string.Concat(this.ClientID, ".reset();"));
            }
            else
            {
                this.AddScript(string.Format("{0}.setValue({1});", this.ClientID, this.SelectedItems.ValuesToJsonArray()));
            }
        }
    }
}