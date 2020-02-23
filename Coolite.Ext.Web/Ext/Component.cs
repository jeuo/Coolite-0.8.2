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
    /// Base Class for all Coolite ASP.NET Web Controls.
    /// </summary>
    [Xtype("component")]
    [Description("Base Class for all Coolite ASP.NET Web Controls.")]
    public abstract class Component : Observable
    {
        protected override void OnAfterClientInit(Observable sender)
        {
            base.OnAfterClientInit(sender);

            if(!string.IsNullOrEmpty(this.ContextMenuID))
            {
                Control menu = ControlUtils.FindControl(this, this.ContextMenuID, true);
                if (menu != null)
                {
                    this.AddScript(string.Format("this.{0}.getEl().on('contextmenu', function(e, t){{this.{1}.trg=t;e.stopEvent();e.preventDefault();this.{1}.showAt(e.getPoint());}}, this);", this.ClientID, menu.ClientID));
                }
                else
                {
                    throw new InvalidOperationException(string.Format("The Menu with the ID of '{0}' was not found.", this.ContextMenuID));
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                if (this.Plugins.Count > 0)
                {
                    foreach (Plugin plugin in this.Plugins)
                    {
                        if (plugin is GenericPlugin)
                        {
                            GenericPlugin gp = (GenericPlugin)plugin;

                            if (!string.IsNullOrEmpty(gp.Path))
                            {
                                this.ScriptManager.RegisterClientScriptInclude(plugin.ClientID, this.ResolveUrl(gp.Path));
                            }
                        }
                    }
                }
            }

            base.OnPreRender(e);
        }

        private object additionalConfig;

        [ClientConfig(JsonMode.UnrollObject)]
        [DefaultValue(null)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal protected object AdditionalConfig
        {
            get
            {
                return additionalConfig;
            }
            set
            {
                additionalConfig = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [IDReferenceProperty(typeof(MenuBase))]
        public virtual string ContextMenuID
        {
            get
            {
                return (string)this.ViewState["ContextMenuID"] ?? "";
            }
            set
            {
                this.ViewState["ContextMenuID"] = value;
            }
        }

        /// <summary>
        /// Whether the component can move the Dom node when rendering (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Whether the component can move the Dom node when rendering (defaults to true).")]
        public virtual bool AllowDomMove
        {
            get
            {
                object obj = this.ViewState["AllowDomMove"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDomMove"] = value;
            }
        }

        /// <summary>
        /// The id of the node, a DOM node or an existing Element corresponding to a DIV that is already present in the document that specifies some structural markup for this component.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id of the node, a DOM node or an existing Element corresponding to a DIV that is already present in the document that specifies some structural markup for this component.")]
        public virtual string ApplyTo
        {
            get
            {
                if (this.IsLazy)
                {
                    return "";
                }
                return (string)this.ViewState["ApplyTo"] ?? "";
            }
            internal set
            {
                this.ViewState["ApplyTo"] = value;
            }
        }

        /// <summary>
        /// A tag name or DomHelper spec to create an element with. This is intended to create shorthand utility components inline via JSON. It should not be used for higher level components which already create their own elements.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A tag name or DomHelper spec to create an element with. This is intended to create shorthand utility components inline via JSON. It should not be used for higher level components which already create their own elements.")]
        public virtual string AutoEl
        {
            get
            {
                return (string)this.ViewState["AutoEl"] ?? "";
            }
            set
            {
                this.ViewState["AutoEl"] = value;
            }
        }

        /// <summary>
        /// True if the component should check for hidden classes (e.g. 'x-hidden' or 'x-hide-display') and remove them on render (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True if the component should check for hidden classes (e.g. 'x-hidden' or 'x-hide-display') and remove them on render (defaults to false).")]
        public virtual bool AutoShow
        {
            get
            {
                object obj = this.ViewState["AutoShow"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoShow"] = value;
            }
        }

        /// <summary>
        /// The CSS class used to provide field clearing (defaults to 'x-form-clear-left').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The CSS class used to provide field clearing (defaults to 'x-form-clear-left').")]
        public virtual string ClearCls
        {
            get
            {
                return (string)this.ViewState["ClearCls"] ?? "";
            }
            set
            {
                this.ViewState["ClearCls"] = value;
            }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to this component's Element (defaults to ''). This can be useful for adding customized styles to the component or any of its children using standard CSS rules.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("An optional extra CSS class that will be added to this component's Element (defaults to ''). This can be useful for adding customized styles to the component or any of its children using standard CSS rules.")]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to this component's contentContainer (defaults to ''). This can be useful for adding customized styles to the contentContainer or any of its children using standard CSS rules.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("An optional extra CSS class that will be added to this component's contentContainer (defaults to ''). This can be useful for adding customized styles to the contentContainer or any of its children using standard CSS rules.")]
        public virtual string CtCls
        {
            get
            {
                return (string)this.ViewState["CtCls"] ?? "";
            }
            set
            {
                this.ViewState["CtCls"] = value;
            }
        }

        /// <summary>
        /// Render this component disabled (default is false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetDisabled")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Render this component disabled (default is false).")]
        public virtual bool Disabled
        {
            get
            {
                object obj = this.ViewState["Disabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Disabled"] = value;
            }
        }

        /// <summary>
        /// CSS class added to the component when it is disabled (defaults to 'x-items-disabled').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("CSS class added to the component when it is disabled (defaults to 'x-items-disabled').")]
        public virtual string DisabledClass
        {
            get
            {
                return (string)this.ViewState["DisabledClass"] ?? "";
            }
            set
            {
                this.ViewState["DisabledClass"] = value;
            }
        }

        /// <summary>
        /// The label text to display next to this field (defaults to '').
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetFieldLabel")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The label text to display next to this field (defaults to '').")]
        public virtual string FieldLabel
        {
            get
            {
                return (string)this.ViewState["FieldLabel"] ?? "";
            }
            set
            {
                this.ViewState["FieldLabel"] = value;
            }
        }

        /// <summary>
        /// Render this component hidden (default is false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(Script = "{0}.setVisible(!{1});")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Render this component hidden (default is false).")]
        public virtual bool Hidden
        {
            get
            {
                object obj = this.ViewState["Hidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Hidden"] = value;
            }
        }

        /// <summary>
        /// True to completely hide the label element (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to completely hide the label element (defaults to false).")]
        public virtual bool HideLabel
        {
            get
            {
                object obj = this.ViewState["HideLabel"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideLabel"] = value;
            }
        }

        /// <summary>
        /// How this component should be hidden. Supported values are 'visibility' (css visibility), 'offsets' (negative offset position) and 'display' (css display) - defaults to 'display'.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(HideMode.Display)]
        [NotifyParentProperty(true)]
        [Description("How this component should be hidden. Supported values are 'visibility' (css visibility), 'offsets' (negative offset position) and 'display' (css display) - defaults to 'display'.")]
        public virtual HideMode HideMode
        {
            get
            {
                object obj = this.ViewState["HideMode"];
                return (obj == null) ? HideMode.Display : (HideMode)obj;
            }
            set
            {
                this.ViewState["HideMode"] = value;
            }
        }

        /// <summary>
        /// True to hide and show the component's contentContainer when hide/show is called on the component, false to hide and show the component itself (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to hide and show the component's contentContainer when hide/show is called on the component, false to hide and show the component itself (defaults to false)")]
        public virtual bool HideParent
        {
            get
            {
                object obj = this.ViewState["HideParent"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideParent"] = value;
            }
        }

        /// <summary>
        /// An additional CSS class to apply to this field (defaults to the contentContainer's itemCls value if set, or '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An additional CSS class to apply to this field (defaults to the contentContainer's itemCls value if set, or '').")]
        public virtual string ItemCls
        {
            get
            {
                return (string)this.ViewState["ItemCls"] ?? "";
            }
            set
            {
                this.ViewState["ItemCls"] = value;
            }
        }

        /// <summary>
        /// An additional CSS class to apply to this field form label.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An additional CSS class to apply to this field form label.")]
        public virtual string LabelCls
        {
            get
            {
                return (string)this.ViewState["LabelCls"] ?? "";
            }
            set
            {
                this.ViewState["LabelCls"] = value;
            }
        }

        /// <summary>
        /// The standard separator to display after the text of each form label (defaults is a colon ':'). To display no separator for this field's label specify empty string ''.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(":")]
        [Description("The standard separator to display after the text of each form label (defaults is a colon ':'). To display no separator for this field's label specify empty string ''.")]
        public virtual string LabelSeparator
        {
            get
            {
                return (string)this.ViewState["LabelSeparator"] ?? ":";
            }
            set
            {
                this.ViewState["LabelSeparator"] = value;
            }
        }

        /// <summary>
        /// A CSS style specification to apply directly to this field's label (defaults to the contentContainer's labelStyle value if set, or ''). For example, labelStyle: 'font-weight:bold;'.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS style specification to apply directly to this field's label (defaults to the contentContainer's labelStyle value if set, or ''). For example, labelStyle: 'font-weight:bold;'.")]
        public virtual string LabelStyle
        {
            get
            {
                return (string)this.ViewState["LabelStyle"] ?? "";
            }
            set
            {
                this.ViewState["LabelStyle"] = value;
            }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to this component's Element when the mouse moves over the Element, and removed when the mouse moves out. (defaults to ''). This can be useful for adding customized 'active' or 'hover' styles to the component or any of its children using standard CSS rules.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("An optional extra CSS class that will be added to this component's Element when the mouse moves over the Element, and removed when the mouse moves out. (defaults to ''). This can be useful for adding customized 'active' or 'hover' styles to the component or any of its children using standard CSS rules.")]
        public virtual string OverCls
        {
            get
            {
                return (string)this.ViewState["OverCls"] ?? "";
            }
            set
            {
                this.ViewState["OverCls"] = value;
            }
        }


        /*  Items
            -----------------------------------------------------------------------------------------------*/

        PluginsCollection<Plugin> plugins;

        /// <summary>
        /// An object or array of controls that inherit from IPlugin that will provide custom functionality for this component. The only requirement for a valid plugin is that it contain an init method that accepts a reference of type Ext.Component. When a component is created, if any plugins are available, the component will call the init method on each plugin, passing a reference to itself. Each plugin can then call methods or respond to events on the component as needed to provide its functionality.
        /// </summary>
        [ClientConfig("plugins", typeof(PluginCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("An object or array of controls that inherit from IPlugin that will provide custom functionality for this component. The only requirement for a valid plugin is that it contain an init method that accepts a reference of type Ext.Component. When a component is created, if any plugins are available, the component will call the init method on each plugin, passing a reference to itself. Each plugin can then call methods or respond to events on the component as needed to provide its functionality.")]
        public virtual PluginsCollection<Plugin> Plugins
        {
            get
            {
                if (this.plugins == null)
                {
                    this.plugins = new PluginsCollection<Plugin>();
                    this.plugins.AfterPluginAdd += this.AfterPluginAdd;
                    this.plugins.AfterPluginRemove += this.AfterPluginRemove;
                }
                return this.plugins;
            }
        }

        protected virtual void AfterPluginAdd(Plugin plugin)
        {
            this.Controls.Add(plugin);
        }

        protected virtual void AfterPluginRemove(Plugin plugin)
        {
            this.Controls.Remove(plugin);
        }

        string renderTo = "";

        /// <summary>
        /// The id of the node, a DOM node or an existing Element that will be the contentContainer to render this component into.
        /// </summary>
        [DefaultValue("")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The id of the node, a DOM node or an existing Element that will be the contentContainer to render this component into.")]
        public virtual string RenderTo
        {
            get
            {
                if (this.IsLazy)
                {
                    return "";
                }
                return string.IsNullOrEmpty(this.renderTo) ? this.ContainerID : this.renderTo;
            }
            internal set
            {
                this.renderTo = value;
            }
        }

        [ClientConfig("renderTo")]
        [DefaultValue("")]
        protected virtual string RenderToProxy
        {
            get
            {
                if (this.AutoRender)
                {
                    if (!string.IsNullOrEmpty(this.CustomRenderTo))
                    {
                        return this.CustomRenderTo;
                    }

                    if (!string.IsNullOrEmpty(this.RenderTo))
                    {
                        return this.RenderTo;
                    }
                }

                return "";
            }
        }

        /// <summary>
        /// Automatically render control on client during page load. Default is true.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Automatically render control on client during page load. Default is true.")]
        public virtual bool AutoRender
        {
            get
            {
                object obj = this.ViewState["AutoRender"];
                return obj != null ? (bool) obj : true;
            }
            set
            {
                this.ViewState["AutoRender"] = value;
            }
        }

        /// <summary>
        /// A custom value for the renderTo config property. Wrap value with ={value_here} token if you require the value to be rendered as a raw string, otherwise the value will be wrapped in \"\".
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A custom value for the renderTo config property. Wrap value with ={value_here} token if you require the value to be rendered as a raw string, otherwise the value will be wrapped in \"\".")]
        public virtual string CustomRenderTo
        {
            get
            {
                return (string)this.ViewState["CustomRenderTo"] ?? "";
            }
            set
            {
                this.ViewState["CustomRenderTo"] = value;
            }
        }

        /// <summary>
        /// An array of events that, when fired, should trigger this component to save its state (defaults to none). These can be any types of events supported by this component, including browser or custom events (e.g., ['click', 'customerchange']).
        /// </summary>
        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An array of events that, when fired, should trigger this component to save its state (defaults to none). These can be any types of events supported by this component, including browser or custom events (e.g., ['click', 'customerchange']).")]
        public virtual string[] StateEvents
        {
            get
            {
                object obj = this.ViewState["StateEvents"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["StateEvents"] = value;
            }
        }

        /// <summary>
        /// The unique id for this component to use for state management purposes (defaults to the component id).
        /// </summary>
        [ClientConfig("stateId")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The unique id for this component to use for state management purposes (defaults to the component id).")]
        public virtual string StateID
        {
            get
            {
                return (string)this.ViewState["StateID"] ?? "";
            }
            set
            {
                this.ViewState["StateID"] = value;
            }
        }

        /// <summary>
        /// A flag which causes the Component to attempt to restore the state of internal properties from a saved state on startup.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("A flag which causes the Component to attempt to restore the state of internal properties from a saved state on startup.")]
        public virtual bool Stateful
        {
            get
            {
                object obj = this.ViewState["Stateful"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Stateful"] = value;
            }
        }

        /// <summary>
        /// A custom style specification to be applied to this component's Element.
        /// </summary>
        [ClientConfig("style")]
        [AjaxEventUpdate(MethodName = "ApplyStyles")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("A custom style specification to be applied to this component's Element.")]
        public virtual string StyleSpec
        {
            get
            {
                return (string)this.ViewState["StyleSpec"] ?? "";
            }
            set
            {
                this.ViewState["StyleSpec"] = value;
            }
        }

        private ItemsCollection<ToolTip> toolTips;

        /// <summary>
        /// A collection of ToolTip configs used to add ToolTips to the Button
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("A collection of ToolTip configs used to add ToolTips to the Button")]
        public virtual ItemsCollection<ToolTip> ToolTips
        {
            get
            {
                if (this.toolTips == null)
                {
                    this.toolTips = new ItemsCollection<ToolTip>();
                    this.toolTips.AfterItemAdd += new ItemsCollection<ToolTip>.AfterItemAddHandler(ToolTips_AfterItemAdd);
                }
                return this.toolTips;
            }
        }

        protected virtual void ToolTips_AfterItemAdd(ToolTip item)
        {
            if (!this.Controls.Contains(item))
            {
                item.TargetControl = this;
                this.Controls.Add(item);
            }
        }

        // TODO: "Ambiguous match found" Exception thrown in VS Designer and IIS if Parent class defines Listeners property.
        // For now, the Listeners property can only be implemented in the leaf (bottom child).
        // NOTE: Figure out some way to get this to work. Arg...
        // REFERENCE: http://en.csharp-online.net/CSharp_Coding_Solutions%E2%80%94Understanding_the_Overloaded_Return_Type_and_Property

        //private ComponentListeners listeners;

        //[Category("Events")]
        //[Themeable(false)]
        //[NotifyParentProperty(true)]
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[Description("Client-side JavaScript EventHandlers")]
        //public virtual ComponentListeners Listeners
        //{
        //    get
        //    {
        //        if (this.listeners == null)
        //        {
        //            this.listeners = new ComponentListeners();
        //        }
        //        return this.listeners;
        //    }
        //}


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Adds a CSS class to the component's underlying element.
        /// </summary>
        [Description("Adds a CSS class to the component's underlying element.")]
        public virtual void AddClass(string cls)
        {
            this.AddScript("{0}.addClass({1});", this.ClientID, JSON.Serialize(cls));
        }

        /// <summary>
        /// More flexible version of setStyle for setting style properties.
        /// </summary>
        [Description("More flexible version of setStyle for setting style properties.")]
        public virtual void ApplyStyles(string styles)
        {
            this.AddScript("{0}.el.applyStyles({1});", this.ClientID, JSON.Serialize(styles));
        }

        /// <summary>
        /// Destroys this component by purging any event listeners, removing the component's element from the DOM, removing the component from its Ext.Container (if applicable) and unregistering it from Ext.ComponentMgr. Destruction is generally handled automatically by the framework and this method should usually not need to be called directly.
        /// </summary>
        [Description("Destroys this component by purging any event listeners, removing the component's element from the DOM, removing the component from its Ext.Container (if applicable) and unregistering it from Ext.ComponentMgr. Destruction is generally handled automatically by the framework and this method should usually not need to be called directly.")]
        public virtual void Destroy()
        {
            this.AddScript("{0}.destroy();", this.ClientID);
        }

        /// <summary>
        /// Try to focus this component.
        /// </summary>
        [Description("Try to focus this component.")]
        new public virtual void Focus()
        {
            this.AddScript("{0}.focus();", this.ClientID);
        }

        /// <summary>
        /// Try to focus this component.
        /// </summary>
        [Description("Try to focus this component.")]
        public virtual void Focus(bool selectText)
        {
            this.AddScript("{0}.focus({1});", this.ClientID, JSON.Serialize(selectText));
        }

        /// <summary>
        /// Try to focus this component.
        /// </summary>
        [Description("Try to focus this component.")]
        public virtual void Focus(bool selectText, int delay)
        {
            this.AddScript("{0}.focus({1},{2});", this.ClientID, JSON.Serialize(selectText), delay);
        }

        /// <summary>
        /// Hide this component.
        /// </summary>
        [Description("Hide this component.")]
        public virtual void Hide()
        {
            this.AddScript("{0}.hide();", this.ClientID);
        }

        /// <summary>
        /// Removes a CSS class from the component's underlying element.
        /// </summary>
        [Description("Removes a CSS class from the component's underlying element.")]
        public virtual void RemoveClass(string cls)
        {
            this.AddScript("{0}.removeClass({1});", this.ClientID, JSON.Serialize(cls));
        }

        /// <summary>
        /// Show this component.
        /// </summary>
        [Description("Show this component.")]
        public virtual void Show()
        {
            this.AddScript("{0}.show();", this.ClientID);
        }


        /*  Protected Client Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Convenience function for setting disabled/enabled by boolean.
        /// </summary>
        [Description("Convenience function for setting disabled/enabled by boolean.")]
        protected internal virtual void SetDisabled(bool disabled)
        {
            this.AddScript("{0}.setDisabled({1});", this.ClientID, disabled.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Convenience function to hide or show this component by boolean.
        /// </summary>
        [Description("Convenience function to hide or show this component by boolean.")]
        protected internal virtual void SetVisible(bool visible)
        {
            this.AddScript("{0}.setVisible({1});", this.ClientID, visible.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Convenience function for setting the FieldLabel of a Component during an AjaxEvent or AjaxMethod request.
        /// </summary>
        protected internal virtual void SetFieldLabel(string text)
        {
            this.AddScript("{0}.setFieldLabel(\"{1}\");", this.ClientID, text);
        }
    }
}