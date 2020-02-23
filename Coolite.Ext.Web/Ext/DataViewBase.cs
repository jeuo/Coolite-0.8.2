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
    public abstract class DataViewBase : BoxComponent
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if(string.IsNullOrEmpty(this.ItemSelector))
            {
                throw new ArgumentNullException("ItemSelector", "The ItemSelector can't be empty");
            }

            if(string.IsNullOrEmpty(this.Template.Text))
            {
                throw new ArgumentNullException("Template", "The Template can't be empty");
            }
        }

        protected override void OnBeforeClientInitHandler()
        {
            base.OnBeforeClientInitHandler();
            this.PrepareData.Args = new string[] {"data"};
        }

        /// <summary>
        /// True to defer emptyText being applied until the store's first load
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to defer emptyText being applied until the store's first load")]
        [NotifyParentProperty(true)]
        public virtual bool DeferEmptyText
        {
            get
            {
                object obj = this.ViewState["DeferEmptyText"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DeferEmptyText"] = value;
            }
        }

        /// <summary>
        /// The text to display in the view when there is no data to display (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text to display in the view when there is no data to display (defaults to '').")]
        public virtual string EmptyText
        {
            get
            {
                object obj = this.ViewState["EmptyText"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["EmptyText"] = value;
            }
        }

        /// <summary>
        /// This is a required setting. A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to determine what nodes this DataView will be working with.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("This is a required setting. A simple CSS selector (e.g. div.some-class or span:first-child) that will be used to determine what nodes this DataView will be working with.")]
        public virtual string ItemSelector
        {
            get
            {
                object obj = this.ViewState["ItemSelector"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ItemSelector"] = value;
            }
        }

        /// <summary>
        /// A string to display during data load operations (defaults to undefined). If specified, this text will be displayed in a loading div and the view's contents will be cleared while loading, otherwise the view's contents will continue to display normally until the new data is loaded and the contents are replaced.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A string to display during data load operations (defaults to undefined). If specified, this text will be displayed in a loading div and the view's contents will be cleared while loading, otherwise the view's contents will continue to display normally until the new data is loaded and the contents are replaced.")]
        public virtual string LoadingText
        {
            get
            {
                object obj = this.ViewState["LoadingText"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["LoadingText"] = value;
            }
        }

        /// <summary>
        /// True to allow selection of more than one item at a time, false to allow selection of only a single item at a time or no selection at all, depending on the value of singleSelect (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow selection of more than one item at a time, false to allow selection of only a single item at a time or no selection at all, depending on the value of singleSelect (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool MultiSelect
        {
            get
            {
                object obj = this.ViewState["MultiSelect"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["MultiSelect"] = value;
            }
        }

        /// <summary>
        /// A CSS class to apply to each item in the view on mouseover (defaults to undefined).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class to apply to each item in the view on mouseover (defaults to undefined).")]
        public virtual string OverClass
        {
            get
            {
                object obj = this.ViewState["OverClass"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["OverClass"] = value;
            }
        }

        /// <summary>
        /// A CSS class to apply to each selected item in the view (defaults to 'x-view-selected').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-view-selected")]
        [Description("A CSS class to apply to each selected item in the view (defaults to 'x-view-selected').")]
        public virtual string SelectedClass
        {
            get
            {
                object obj = this.ViewState["SelectedClass"];
                return (obj == null) ? "x-view-selected" : (string)obj;
            }
            set
            {
                this.ViewState["SelectedClass"] = value;
            }
        }

        /// <summary>
        /// True to enable multiselection by clicking on multiple items without requiring the user to hold Shift or Ctrl, false to force the user to hold Ctrl or Shift to select more than on item (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable multiselection by clicking on multiple items without requiring the user to hold Shift or Ctrl, false to force the user to hold Ctrl or Shift to select more than on item (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool SimpleSelect
        {
            get
            {
                object obj = this.ViewState["SimpleSelect"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SimpleSelect"] = value;
            }
        }

        /// <summary>
        /// True to allow selection of exactly one item at a time, false to allow no selection at all (defaults to false). Note that if multiSelect = true, this value will be ignored.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow selection of exactly one item at a time, false to allow no selection at all (defaults to false). Note that if multiSelect = true, this value will be ignored.")]
        [NotifyParentProperty(true)]
        public virtual bool SingleSelect
        {
            get
            {
                object obj = this.ViewState["SingleSelect"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["SingleSelect"] = value;
            }
        }

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
                object obj = this.ViewState["StoreID"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["StoreID"] = value;
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
        /// True to enable mouseenter and mouseleave events
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable mouseenter and mouseleave events")]
        public virtual bool TrackOver
        {
            get
            {
                object obj = this.ViewState["TrackOver"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TrackOver"] = value;
            }
        }

        private JFunction prepareData;
        
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual JFunction PrepareData
        {
            get
            {
                if (this.prepareData == null)
                {
                    this.prepareData = new JFunction();
                }
                return this.prepareData;
            }
            set
            {
                this.prepareData = value;
            }
        }
    }
}