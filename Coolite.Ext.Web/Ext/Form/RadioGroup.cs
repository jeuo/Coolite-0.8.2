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
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:RadioGroup runat=\"server\" />")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.RadioGroup), "Build.Resources.ToolboxIcons.RadioGroup.bmp")]
    [Description("A grouping container for Ext.form.Radio controls.")]
    [InstanceOf(ClassName = "Ext.form.RadioGroup")]
    [Xtype("radiogroup")]
    public class RadioGroup : CheckboxGroupBase
    {
        private RadioGroupListeners listeners;

        /// <summary>
        /// Client-side JavaScript Event Handlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("2. Observable")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript Event Handlers")]
        [ViewStateMember]
        public RadioGroupListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new RadioGroupListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private RadioGroupAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax Event Handlers
        /// </summary>
        [Category("2. Observable")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax Event Handlers")]
        [ViewStateMember]
        public RadioGroupAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new RadioGroupAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //SetGroup();

            if (!this.DesignMode && this.AutomaticGrouping && !Ext.IsAjaxRequest)
            {
                this.Page.LoadComplete += delegate
                {
                    this.SetGroup();
                };
            }
        }

        private void SetGroup()
        {
            string groupName = string.IsNullOrEmpty(this.GroupName)
                                   ? this.ClientID + "_Group"
                                   : this.GroupName;

            foreach (Radio item in this.Items)
            {
                RadioColumn rCol = item as RadioColumn;

                if (rCol != null)
                {
                    foreach (Component comp in rCol.Items)
                    {
                        Radio radio = comp as Radio;

                        if (radio != null && string.IsNullOrEmpty(radio.GroupName))
                        {
                            radio.GroupName = groupName;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(item.GroupName))
                    {
                        item.GroupName = groupName;
                    }
                }
            }
        }

        ItemsCollection<Radio> items;

        /// <summary>
        /// Items collection
        /// </summary>
        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Items collection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual ItemsCollection<Radio> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Radio>();
                    this.items.AfterItemAdd += AfterItemAdd;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Radio item)
        {
            if (!this.Controls.Contains(item))
            {
                this.Controls.Add(item);
            }

            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

        [Category("Config Options")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [DefaultValue(true)]
        [Description("Automatic grouping (defaults to true).")]
        public virtual bool AutomaticGrouping
        {
            get
            {
                object obj = this.ViewState["AutomaticGrouping"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutomaticGrouping"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The field's HTML name attribute.")]
        public virtual string GroupName
        {
            get
            {
                return (string)this.ViewState["GroupName"] ?? "";
            }
            set
            {
                this.ViewState["GroupName"] = value;
            }
        }

        [Description("A List of Radio Controls in this RadioGroup that are Checked.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Radio> CheckedItems
        {
            get
            {
                return Coolite.Utilities.ControlUtils.FindControls<Radio>(this).FindAll(cb => cb.Checked);
            }
        }
    }
}