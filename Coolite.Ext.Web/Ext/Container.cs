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
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Base Class for any visual Component that uses a box contentContainer.
    /// </summary>
    [Xtype("contentContainer")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Description("Base Class for any visual Component that uses a box contentContainer.")]
    public abstract class Container : BoxComponent, ILayout
    {
        public override bool HasLayout()
        {
            return this.Layout != null;
        }

        /// <summary>
        /// A string component id or the numeric index of the component that should be initially activated within the contentContainer's layout on render.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetActiveItem")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A string component id of the component that should be initially activated within the contentContainer's layout on render.")]
        [NotifyParentProperty(true)]    
        public virtual string ActiveItem
        {
            get
            {
                return (string)this.ViewState["ActiveItem"] ?? "";
            }
            set
            {
                this.ViewState["ActiveItem"] = value;
            }
        }

        /// <summary>
        /// A string component id or the numeric index of the component that should be initially activated within the contentContainer's layout on render.
        /// </summary>
        [ClientConfig("activeItem")]
        [AjaxEventUpdate(MethodName = "SetActiveIndex")]
        [Category("Config Options")]
        [DefaultValue(-1)]
        [Description("A numeric index of the component that should be initially activated within the contentContainer's layout on render.")]
        [NotifyParentProperty(true)]
        public virtual int ActiveIndex
        {
            get
            {
                object obj = this.ViewState["ActiveIndex"];
                return (obj == null) ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["ActiveIndex"] = value;
            }
        }

        protected virtual void SetActiveIndex(int index)
        {
            this.AddScript("if({0}.getLayout().setActiveItem){{{0}.getLayout().setActiveItem({1});}}", this.ClientID, index);
        }

        protected virtual void SetActiveItem(string item)
        {
            this.AddScript("if({0}.getLayout().setActiveItem){{{0}.getLayout().setActiveItem(\"{1}\");}}", this.ClientID, item);
        }

        /// <summary>
        /// If true the contentContainer will automatically destroy any contained component that is removed from it, else destruction must be handled manually (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("If true the contentContainer will automatically destroy any contained component that is removed from it, else destruction must be handled manually (defaults to true).")]
        [NotifyParentProperty(true)]    
        public virtual bool AutoDestroy
        {
            get
            {
                object obj = this.ViewState["AutoDestroy"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoDestroy"] = value;
            }
        }

        /// <summary>
        /// When set to true (100 milliseconds) or a number of milliseconds, the layout assigned for this contentContainer will buffer the frequency it calculates and does a re-layout of components. This is useful for heavy containers or containers with a large amount of sub components that frequent calls to layout are expensive.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("When set to true (100 milliseconds) or a number of milliseconds, the layout assigned for this contentContainer will buffer the frequency it calculates and does a re-layout of components. This is useful for heavy containers or containers with a large amount of sub components that frequent calls to layout are expensive.")]
        [NotifyParentProperty(true)]    
        public virtual bool BufferResize
        {
            get
            {
                object obj = this.ViewState["BufferResize"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["BufferResize"] = value;
            }
        }

        /// <summary>
        /// The default type of contentContainer represented by this object as registered in Ext.ComponentMgr (defaults to 'panel').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("panel")]
        [Description("The default type of contentContainer represented by this object as registered in Ext.ComponentMgr (defaults to 'panel').")]
        [NotifyParentProperty(true)]    
        public virtual string DefaultType
        {
            get
            {
                return (string)this.ViewState["DefaultType"] ?? "panel";
            }
            set
            {
                this.ViewState["DefaultType"] = value;
            }
        }

        private ParameterCollection defaults;

        /// <summary>
        /// A config object that will be applied to all components added to this contentContainer either via the items config or via the add or insert methods. The defaults config can contain any number of name/value property pairs to be added to each items, and should be valid for the types of items being added to the contentContainer. For example, to automatically apply padding to the body of each of a set of contained Ext.Panel items, you could pass: defaults: {bodyStyle:'padding:15px'}.
        /// </summary>
        [ClientConfig(JsonMode.ArrayToObject)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("A config object that will be applied to all components added to this contentContainer either via the items config or via the add or insert methods. The defaults config can contain any number of name/value property pairs to be added to each items, and should be valid for the types of items being added to the contentContainer. For example, to automatically apply padding to the body of each of a set of contained Ext.Panel items, you could pass: defaults: {bodyStyle:'padding:15px'}.")]
        public virtual ParameterCollection Defaults
        {
            get
            {
                if (this.defaults == null)
                {
                    this.defaults = new ParameterCollection(true);
                    this.defaults.Owner = this;
                    this.defaults.AfterItemAdd += Defaults_AfterItemAdd;
                }
                return this.defaults;
            }
        }

        void Defaults_AfterItemAdd(Parameter item)
        {
            item.CamelName = true;
        }

        /// <summary>
        /// True to hide the borders of each contained component, false to defer to the component's existing border settings (defaults to false).
        /// </summary>

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the borders of each contained component, false to defer to the component's existing border settings (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool HideBorders
        {
            get
            {
                object obj = this.ViewState["HideBorders"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideBorders"] = value;
            }
        }

        /// <summary>
        /// True to automatically monitor window resize events to handle anything that is sensitive to the current size of the viewport. This value is typically managed by the chosen layout and should not need to be set manually.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to automatically monitor window resize events to handle anything that is sensitive to the current size of the viewport. This value is typically managed by the chosen layout and should not need to be set manually.")]
        [NotifyParentProperty(true)]    
        public virtual bool MonitorResize
        {
            get
            {
                object obj = this.ViewState["MonitorResize"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["MonitorResize"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Adds a component to this container. Fires the beforeadd event before adding, then fires the add event after the component has been added. If the container is already rendered when add is called, you may need to call doLayout to refresh the view. This is required so that you can add multiple child components if needed while only refreshing the layout once.
        /// </summary>
        [Description("Adds a component to this container. Fires the beforeadd event before adding, then fires the add event after the component has been added. If the container is already rendered when add is called, you may need to call doLayout to refresh the view. This is required so that you can add multiple child components if needed while only refreshing the layout once.")]
        public virtual void Add(Component component)
        {
            if (this is TabPanelBase && component is Tab)
            {
                ((TabPanelBase)this).Tabs.Add((Tab)component);
            }
            else
            {
                this.Items.Add(component);
            }
        }

        /// <summary>
        /// Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.
        /// </summary>
        [Description("Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.")]
        public virtual void Bubble(string function)
        {
            this.AddScript("{0}.bubble({1});", this.ClientID, function);
        }

        /// <summary>
        /// Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.
        /// </summary>
        [Description("Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.")]
        public virtual void Bubble(string function, string scope)
        {
            this.AddScript("{0}.bubble({1},{2});", this.ClientID, function, scope);
        }

        /// <summary>
        /// Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.
        /// </summary>
        [Description("Bubbles up the component/container heirarchy, calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the bubble is stopped.")]
        public virtual void Bubble(string function, string scope, Dictionary<string, object> args)
        {
            this.AddScript("{0}.bubble({1},{2},{3});", this.ClientID, function, scope, JSON.Serialize(args));
        }

        /// <summary>
        /// Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.
        /// </summary>
        [Description("Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.")]
        public virtual void Cascade(string function)
        {
            this.AddScript("{0}.cascade({1});", this.ClientID, function);
        }

        /// <summary>
        /// Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.
        /// </summary>
        [Description("Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.")]
        public virtual void Cascade(string function, string scope)
        {
            this.AddScript("{0}.cascade({1},{2});", this.ClientID, function, scope);
        }

        /// <summary>
        /// Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.
        /// </summary>
        [Description("Cascades down the component/container heirarchy from this component (called first), calling the specified function with each component. The scope (this) of function call will be the scope provided or the current component. The arguments to the function will be the args provided or the current component. If the function returns false at any point, the cascade is stopped on that branch.")]
        public virtual void Cascade(string function, string scope, Dictionary<string, object> args)
        {
            this.AddScript("{0}.cascade({1},{2},{3});", this.ClientID, function, scope, JSON.Serialize(args));
        }

        /// <summary>
        /// Force this container's layout to be recalculated. A call to this function is required after adding a new component to an already rendered container, or possibly after changing sizing/position properties of child components.
        /// </summary>
        [Description("Force this container's layout to be recalculated. A call to this function is required after adding a new component to an already rendered container, or possibly after changing sizing/position properties of child components.")]
        public virtual void DoLayout()
        {
            this.AddScript("{0}.doLayout();", this.ClientID);
        }

        /// <summary>
        /// Force this container's layout to be recalculated. A call to this function is required after adding a new component to an already rendered container, or possibly after changing sizing/position properties of child components.
        /// </summary>
        [Description("Force this container's layout to be recalculated. A call to this function is required after adding a new component to an already rendered container, or possibly after changing sizing/position properties of child components.")]
        public virtual void DoLayout(bool shallow)
        {
            this.AddScript("{0}.doLayout({1});", this.ClientID, shallow.ToString().ToLower());
        }

        /// <summary>
        /// Inserts a Component into this Container at a specified index. Fires the beforeadd event before inserting, then fires the add event after the Component has been inserted.
        /// </summary>
        /// <param name="index">The index at which the Component will be inserted into the Container's items collection</param>
        /// <param name="component">The child Component to insert.</param>
        [Description("Inserts a Component into this Container at a specified index. Fires the beforeadd event before inserting, then fires the add event after the Component has been inserted.")]
        public virtual void Insert(int index, Component component)
        {
            this.AddScript("{0}.insert({1},{2});", this.ClientID, index, component.ClientID);
        }

        /// <summary>
        /// Inserts a Component into this Container at a specified index. Fires the beforeadd event before inserting, then fires the add event after the Component has been inserted.
        /// </summary>
        /// <param name="index">The index at which the Component will be inserted into the Container's items collection</param>
        /// <param name="id">The id of the child Component to insert.</param>
        [Description("Inserts a Component into this Container at a specified index. Fires the beforeadd event before inserting, then fires the add event after the Component has been inserted.")]
        public virtual void Insert(int index, string id)
        {
            this.AddScript("{0}.insert({1},Ext.getCmp(\"{2}\"));", this.ClientID, index, id);
        }

        /// <summary>
        /// Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.
        /// </summary>
        [Description("Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.")]
        public virtual void Remove(Component component)
        {
            this.AddScript("{0}.remove({1});", this.ClientID, component.ClientID);
        }

        /// <summary>
        /// Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.
        /// </summary>
        [Description("Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.")]
        public virtual void Remove(Component component, bool destroy)
        {
            this.AddScript("{0}.remove({1},{2});", this.ClientID, component.ClientID, destroy.ToString().ToLower());
        }

        /// <summary>
        /// Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.
        /// </summary>
        [Description("Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.")]
        public virtual void Remove(string id)
        {
            this.AddScript("{0}.remove(Ext.getCmp(\"{1}\"),{2});", this.ClientID, id);
        }

        /// <summary>
        /// Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.
        /// </summary>
        [Description("Removes a component from this container. Fires the beforeremove event before removing, then fires the remove event after the component has been removed.")]
        public virtual void Remove(string id, bool destroy)
        {
            this.AddScript("{0}.remove(Ext.getCmp(\"{1}\"),{2});", this.ClientID, id, destroy.ToString().ToLower());
        }


        /*  Items
            -----------------------------------------------------------------------------------------------*/

        ItemsCollection<Component> items;

        /// <summary>
        /// Items Collection
        /// </summary>
        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [DeferredRender]
        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Items Collection")]
        public virtual ItemsCollection<Component> Items
        {
            get
            {
                if (this.Layout != null)
                {
                    return this.Layout.Items;
                }

                if (this.items == null)
                {
                    this.items = new ItemsCollection<Component>();
                    this.items.AfterItemAdd += this.AfterItemAdd;
                    this.items.AfterItemRemove += this.AfterItemRemove;
                    this.items.SingleItemMode = this.SingleItemMode;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Component item)
        {
            if (this is IContent && item is Layout)
            {
                ((IContent)this).BodyControls.Add(item);
            }
            else
            {
                this.Controls.Add(item);
            }

            if (item.Parent is Layout || item.Parent is TabPanelBase)
            {
                if (!this.LazyItems.Contains(item))
                {
                    this.LazyItems.Add(item);
                }
            }
            else
            {
                this.Items.Remove(item);
            }
        }

        protected virtual void AfterItemRemove(Component item)
        {
            if (this.LazyItems.Contains(item))
            {
                this.LazyItems.Remove(item);
            }
        }


        /*  ILayout
            -----------------------------------------------------------------------------------------------*/

        [ClientConfig(JsonMode.UnrollObject)]
        [DeferredRender]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Browsable(false)]
        public virtual Layout Layout
        {
            get
            {
                if (this is IContent)
                {
                    foreach (Control control in ((IContent)this).BodyControls)
                    {
                        if (control is Layout)
                        {
                            return (Layout)control;
                        }
                        else if (control is ContentPlaceHolder || control is UserControl)
                        {
                            Layout l = this.FindLayout(control);
                            if(l != null)
                            {
                                return l;
                            }
                        }
                    }
                }
                return null;
            }
        }

        protected virtual Layout FindLayout(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Layout)
                {
                    return (Layout)c;
                }
            }

            foreach (Control c in control.Controls)
            {
                if (control is ContentPlaceHolder || control is UserControl)
                {
                    Layout l = this.FindLayout(c);
                    if (l != null)
                    {
                        return l;
                    }
                }
            }

            return null;
        }

        internal virtual Store Store
        {
            get
            {
                if (this is IContent)
                {
                    foreach (Control control in ((IContent)this).BodyControls)
                    {
                        if (control is Store)
                        {
                            return (Store)control;
                        }
                        else if (control is ContentPlaceHolder || control is UserControl)
                        {
                            foreach (Control c in control.Controls)
                            {
                                if (c is Store)
                                {
                                    return (Store)c;
                                }
                            }
                        }
                    }
                }
                return null;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode && this.Page != null && !Ext.IsAjaxRequest && this.Layout != null)
            {
                Store store = this.Store;

                if (store != null)
                {
                    store.ForcePreRender();
                }
            }
            base.Render(writer);
        }
    }
}