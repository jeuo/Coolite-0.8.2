/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:CheckboxGroup runat=\"server\" />")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.CheckboxGroup), "Build.Resources.ToolboxIcons.CheckboxGroup.bmp")]
    [Description("A grouping container for Ext.form.Checkbox controls.")]
    [InstanceOf( ClassName = "Ext.form.CheckboxGroup")]
    [Xtype("checkboxgroup")]
    public class CheckboxGroup : CheckboxGroupBase
    {
        private CheckboxGroupListeners listeners;

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
        public CheckboxGroupListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new CheckboxGroupListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private CheckboxGroupAjaxEvents ajaxEvents;

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
        public CheckboxGroupAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new CheckboxGroupAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
        
        ItemsCollection<Checkbox> items;

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
        public virtual ItemsCollection<Checkbox> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Checkbox>();
                    this.items.AfterItemAdd += AfterItemAdd;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Checkbox item)
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

        [Description("A List of Checkbox Controls in this CheckboxGroup that are Checked.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual List<Checkbox> CheckedItems
        {
            get
            {
                return Coolite.Utilities.ControlUtils.FindControls<Checkbox>(this).FindAll(cb => cb.Checked);

                // TODO: OK... just in case rollback to .NET 2.0 pureness is required, see also RadioGroup.CheckedItems.
                //return Coolite.Utilities.ControlUtils.FindControls<Checkbox>(this).FindAll(delegate(Checkbox checkbox)
                //{
                //    return checkbox.Checked;
                //});
            }
        }
    }
}