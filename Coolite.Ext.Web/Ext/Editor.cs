/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A base editor field that handles displaying/hiding on demand and has some built-in sizing and event handling logic.
    /// </summary>
    [Xtype("editor")]
    [InstanceOf(ClassName = "Ext.Editor")]
    [ToolboxData("<{0}:Editor runat=\"server\" />")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Editor), "Build.Resources.ToolboxIcons.Editor.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("A base editor field that handles displaying/hiding on demand and has some built-in sizing and event handling logic.")]
    public class Editor : Component
    {
        /// <summary>
        /// Event name for activate the editor
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue("click")]
        [NotifyParentProperty(true)]
        [Description("Event name for activate the editor")]
        public virtual string ActivateEvent
        {
            get
            {
                return (string)this.ViewState["ActivateEvent"] ?? "click";
            }
            set
            {
                this.ViewState["ActivateEvent"] = value;
            }
        }

        
        private EditorAlignmentConfig alignment;

        /// <summary>
        /// True to have the tooltip follow the mouse as it moves over the target element (defaults to false).
        /// </summary>
        [ClientConfig(JsonMode.ToString)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("True to have the tooltip follow the mouse as it moves over the target element (defaults to false).")]
        public virtual EditorAlignmentConfig Alignment
        {
            get
            {
                if(this.alignment == null)
                {
                    this.alignment = new EditorAlignmentConfig();
                }

                return this.alignment;
            }
        }

        /// <summary>
        /// Size for the editor to automatically adopt the size of the underlying field, "Width" to adopt the width only, or "Height" to adopt the height only (defaults to Disable)
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(EditorAutoSize.Disable)]
        [NotifyParentProperty(true)]
        [Description("Size for the editor to automatically adopt the size of the underlying field, Width to adopt the width only, or Height to adopt the height only (defaults to Disable)")]
        public virtual EditorAutoSize AutoSize
        {
            get
            {
                object obj = this.ViewState["AutoSize"];
                return (obj == null) ? EditorAutoSize.Disable : (EditorAutoSize)obj;
            }
            set
            {
                this.ViewState["AutoSize"] = value;
            }
        }

        [DefaultValue("false")]
        [ClientConfig("autoSize", JsonMode.Raw)]
        internal string AutoSizeProxy
        {
            get
            {
                switch(this.AutoSize)
                {
                    case EditorAutoSize.Disable:
                        return "false";
                    case EditorAutoSize.Fit:
                        return "true";
                    case EditorAutoSize.Width:
                        return JSON.Serialize("width");
                    case EditorAutoSize.Height:
                        return JSON.Serialize("height");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// True to cancel the edit when the escape key is pressed (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to cancel the edit when the escape key is pressed (defaults to false)")]
        public virtual bool CancelOnEsc
        {
            get
            {
                object obj = this.ViewState["CancelOnEsc"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CancelOnEsc"] = value;
            }
        }

        /// <summary>
        /// True to complete the edit when the enter key is pressed (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to complete the edit when the enter key is pressed (defaults to false)")]
        public virtual bool CompleteOnEnter
        {
            get
            {
                object obj = this.ViewState["CompleteOnEnter"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CompleteOnEnter"] = value;
            }
        }

        /// <summary>
        /// False to keep the bound element visible while the editor is displayed (defaults to true)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("False to keep the bound element visible while the editor is displayed (defaults to true)")]
        public virtual bool HideEl
        {
            get
            {
                object obj = this.ViewState["HideEl"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HideEl"] = value;
            }
        }

        /// <summary>
        /// True to skip the edit completion process (no save, no events fired) if the user completes an edit and the value has not changed (defaults to false). Applies only to string values - edits for other data types will never be ignored.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to skip the edit completion process (no save, no events fired) if the user completes an edit and the value has not changed (defaults to false). Applies only to string values - edits for other data types will never be ignored.")]
        public virtual bool IgnoreNoChange
        {
            get
            {
                object obj = this.ViewState["IgnoreNoChange"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["IgnoreNoChange"] = value;
            }
        }

        /// <summary>
        /// True to automatically revert the field value and cancel the edit when the user completes an edit and the field validation fails (defaults to true)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to automatically revert the field value and cancel the edit when the user completes an edit and the field validation fails (defaults to true)")]
        public virtual bool RevertInvalid
        {
            get
            {
                object obj = this.ViewState["RevertInvalid"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["RevertInvalid"] = value;
            }
        }

        /// <summary>
        /// "sides" for sides/bottom only, "frame" for 4-way shadow, and "drop" for bottom-right shadow (defaults to "frame")
        /// </summary>
        [ClientConfig(typeof(ShadowJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ShadowMode.Frame)]
        [Description("\"sides\" for sides/bottom only, \"frame\" for 4-way shadow, and \"drop\" for bottom-right shadow (defaults to \"frame\")")]
        [NotifyParentProperty(true)]
        public virtual ShadowMode Shadow
        {
            get
            {
                object obj = this.ViewState["Shadow"];
                return (obj == null) ? ShadowMode.Frame : (ShadowMode)obj;
            }
            set
            {
                this.ViewState["Shadow"] = value;
            }
        }

        /// <summary>
        /// Handle the keydown/keypress events so they don't propagate (defaults to true)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Handle the keydown/keypress events so they don't propagate (defaults to true)")]
        public virtual bool SwallowKeys
        {
            get
            {
                object obj = this.ViewState["SwallowKeys"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["SwallowKeys"] = value;
            }
        }

        /// <summary>
        /// Handle the keydown/keypress events so they don't propagate (defaults to true)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to update the innerHTML of the bound element when the update completes (defaults to false)")]
        public virtual bool UpdateEl
        {
            get
            {
                object obj = this.ViewState["UpdateEl"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["UpdateEl"] = value;
            }
        }

        /// <summary>
        /// The data value of the underlying field (defaults to "")
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The data value of the underlying field (defaults to \"\")")]
        public virtual string Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        private ItemsCollection<Field> field;

        /// <summary>
        /// The Field object (or descendant)
        /// </summary>
        [ClientConfig("field", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("The Field object (or descendant)")]
        public virtual ItemsCollection<Field> Field
        {
            get
            {
                if (this.field == null)
                {
                    this.field = new ItemsCollection<Field>();
                    this.field.SingleItemMode = true;
                    this.field.AfterItemAdd += this.AfterItemAdd;
                }

                return this.field;
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
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string RenderTo
        {
            get
            {
                return "";
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string ApplyTo
        {
            get
            {
                return "";
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        internal virtual bool IsSeparate
        {
            get
            {
                return true;
            }
        }

        private Control targetControl;

        public Control TargetControl
        {
            get
            {
                return this.targetControl;
            }
            set
            {
                this.targetControl = value;
            }
        }

        /// <summary>
        /// The target id to associate with this tooltip.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The target id to associate with this tooltip.")]
        public virtual string Target
        {
            get
            {
                return (string)this.ViewState["Target"] ?? "";
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        [ClientConfig("target", JsonMode.Raw)]
        [DefaultValue("")]
        internal string TargetProxy
        {
            get
            {
                string target = this.Target;
                if (!string.IsNullOrEmpty(target))
                {
                    return this.ParseTarget(target);
                }

                if (this.TargetControl != null)
                {
                    return JSON.Serialize(this.TargetControl.ClientID);
                }

                return "";
            }
        }

        internal override string GetClientConstructor(bool instanceOnly, string body)
        {
            string field = this.Field.Count == 0 ? "{xtype: \"textfield\"}" : "{}";

            string template = (instanceOnly) ? "new {1}({3},{2})" : "new {1}({3},{2});";
            return string.Format(template, this.ClientID, this.InstanceOf, body ?? this.InitialConfig, field);
        }

        private InlineEditorListeners listeners;

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
        public InlineEditorListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new InlineEditorListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private InlineEditorAjaxEvents ajaxEvents;

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
        public InlineEditorAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new InlineEditorAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        /// <summary>
        /// Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.
        /// </summary>
        /// <param name="remainVisible">Override the default behavior and keep the editor visible after cancel</param>
        [Description("Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.")]
        public virtual void CancelEdit(bool remainVisible)
        {
            string template = "{0}.cancelEdit({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(remainVisible));
        }

        /// <summary>
        /// Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.
        /// </summary>
        [Description("Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.")]
        public virtual void CancelEdit()
        {
            string template = "{0}.cancelEdit();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Ends the editing process, persists the changed value to the underlying field, and hides the editor.
        /// </summary>
        /// <param name="remainVisible">Override the default behavior and keep the editor visible after edit (defaults to false)</param>
        [Description("Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.")]
        public virtual void CompleteEdit(bool remainVisible)
        {
            string template = "{0}.completeEdit({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(remainVisible));
        }

        /// <summary>
        /// Ends the editing process, persists the changed value to the underlying field, and hides the editor.
        /// </summary>
        [Description("Cancels the editing process and hides the editor without persisting any changes. The field value will be reverted to the original starting value.")]
        public virtual void CompleteEdit()
        {
            string template = "{0}.completeEdit();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Realigns the editor to the bound field based on the current alignment config value.
        /// </summary>
        [Description("Realigns the editor to the bound field based on the current alignment config value.")]
        public virtual void Realign()
        {
            string template = "{0}.alignment={1};{0}.realign();";
            this.AddScript(template, this.ClientID, JSON.Serialize(this.Alignment.ToString()));
        }

        /// <summary>
        /// Sets the height and width of this editor.
        /// </summary>
        /// <param name="width">The new width</param>
        /// <param name="height">The new height</param>
        [Description("Sets the height and width of this editor.")]
        public virtual void SetSize(int width, int height)
        {
            string template = "{0}.setSize({1},{2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(width), JSON.Serialize(height));
        }

        /// <summary>
        /// Sets the data value of the editor
        /// </summary>
        /// <param name="value">Any valid value supported by the underlying field</param>
        [Description("Sets the data value of the editor")]
        internal virtual void SetValue(string value)
        {
            string template = "{0}.setValue({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }

        /// <summary>
        /// Starts the editing process and shows the editor.
        /// </summary>
        /// <param name="el">The element to edit</param>
        /// <param name="value">A value to initialize the editor with. If a value is not provided, it defaults to the innerHTML of el.</param>
        [Description("Starts the editing process and shows the editor.")]
        public virtual void StartEdit(string el, string value)
        {
            string template = "{0}.startEdit(Coolite.Ext.getEl({1}), {2});";
            this.AddScript(template, this.ClientID, this.ParseTarget(el), JSON.Serialize(value));
        }

        /// <summary>
        /// Starts the editing process and shows the editor.
        /// </summary>
        /// <param name="el">The element to edit</param>
        [Description("Starts the editing process and shows the editor.")]
        public virtual void StartEdit(string el)
        {
            string template = "{0}.startEdit(Coolite.Ext.getEl({1}));";
            this.AddScript(template, this.ClientID, this.ParseTarget(el));
        }
    }

    public class EditorAlignmentConfig : StateManagedItem
    {
        /// <summary>
        /// Element anchor point
        /// </summary>
        [DefaultValue(AnchorPoint.Center)]
        [Description("Element anchor point")]
        [NotifyParentProperty(true)]
        public AnchorPoint ElementAnchor
        {
            get
            {
                object obj = this.ViewState["ElementAnchor"];
                return obj == null ? AnchorPoint.Center : (AnchorPoint)obj;
            }
            set
            {
                this.ViewState["ElementAnchor"] = value;
            }
        }

        /// <summary>
        /// Target anchor point
        /// </summary>
        [DefaultValue(AnchorPoint.Center)]
        [Description("Target anchor point")]
        [NotifyParentProperty(true)]
        public AnchorPoint TargetAnchor
        {
            get
            {
                object obj = this.ViewState["TargetAnchor"];
                return obj == null ? AnchorPoint.Center : (AnchorPoint)obj;
            }
            set
            {
                this.ViewState["TargetAnchor"] = value;
            }
        }

        /// <summary>
        ///  The editor will attempt to align as specified, but the position will be adjusted to constrain to the viewport if necessary
        /// </summary>
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("The editor will attempt to align as specified, but the position will be adjusted to constrain to the viewport if necessary")]
        public virtual bool ConstrainViewport
        {
            get
            {
                object obj = this.ViewState["ConstrainViewport"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ConstrainViewport"] = value;
            }
        }

        public override string ToString()
        {
            return string.Concat(Fx.AnchorConvert(this.ElementAnchor), "-",
                Fx.AnchorConvert(this.TargetAnchor), this.ConstrainViewport ? "?" : "");
        }
    }
}