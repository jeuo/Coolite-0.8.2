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
using System.Text;
using System.Web.UI;
using Coolite.Utilities;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Base Class for Form Fields that provides default event handling, sizing, value handling and other functionality.
    /// </summary>
    [Xtype("field")]
    [ContainerStyle("display:inline;")]
    [Description("Base Class for Form Fields that provides default event handling, sizing, value handling and other functionality.")]
    public abstract class Field : BoxComponent, IAutoPostBack, IPostBackDataHandler, IToolbarItem
    {
        protected virtual string UniqueName
        {
            get
            {
                return this.ClientID;
            }
        }

        /// <summary>
        /// TextBox_AutoPostBack
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [Description("TextBox_AutoPostBack")]
        public virtual bool AutoPostBack
        {
            get
            {
                object obj = this.ViewState["AutoPostBack"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Themeable(false)]
        [Description("Gets or sets a value indicating whether validation is performed when the control is set to validate when a postback occurs.")]
        public virtual bool CausesValidation
        {
            get
            {
                object obj = this.ViewState["CausesValidation"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) The name of the field in the grid's Ext.data.Store's Ext.data.Record definition from which to draw the column's value.")]
        public virtual string DataIndex
        {
            get
            {
                object obj = this.ViewState["DataIndex"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["DataIndex"] = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Controls ValidationGroup
        /// </summary>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue("")]
        [Description("Gets or Sets the Controls ValidationGroup")]
        public virtual string ValidationGroup
        {
            get
            {
                return (string)this.ViewState["ValidationGroup"] ?? "";
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }


        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// A DomHelper element spec (defaults to {tag: 'input', type: 'text', size: '20', autocomplete: 'off'}).
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A DomHelper element spec (defaults to {tag: 'input', type: 'text', size: '20', autocomplete: 'off'}).")]
        public virtual string AutoCreate
        {
            get
            {
                return (string)this.ViewState["AutoCreate"] ?? "";
            }
            set
            {
                this.ViewState["AutoCreate"] = value;
            }
        }

        /// <summary>
        /// The default CSS class for the field (defaults to 'x-form-field').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The default CSS class for the field (defaults to 'x-form-field').")]
        public virtual string FieldClass
        {
            get
            {
                return (string)this.ViewState["FieldClass"] ?? "";
            }
            set
            {
                this.ViewState["FieldClass"] = value;
            }
        }

        /// <summary>
        /// The CSS class to use when the field receives focus (defaults to 'x-form-focus').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The CSS class to use when the field receives focus (defaults to 'x-form-focus').")]
        public virtual string FocusClass
        {
            get
            {
                return (string)this.ViewState["FocusClass"] ?? "";
            }
            set
            {
                this.ViewState["FocusClass"] = value;
            }
        }

        /// <summary>
        /// True to hide the label when the field hide
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to hide the label when the field hide")]
        public virtual bool HideWithLabel
        {
            get
            {
                object obj = this.ViewState["HideWithLabel"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["HideWithLabel"] = value;
            }
        }

        /// <summary>
        /// The CSS class to use when marking a field invalid (defaults to 'x-form-invalid').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The CSS class to use when marking a field invalid (defaults to 'x-form-invalid').")]
        public virtual string InvalidClass
        {
            get
            {
                return (string)this.ViewState["InvalidClass"] ?? "";
            }
            set
            {
                this.ViewState["InvalidClass"] = value;
            }
        }

        /// <summary>
        /// The error text to use when marking a field invalid and no message is provided (defaults to 'The value in this field is invalid').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to use when marking a field invalid and no message is provided (defaults to 'The value in this field is invalid').")]
        public virtual string InvalidText
        {
            get
            {
                return (string)this.ViewState["InvalidText"] ?? "";
            }
            set
            {
                this.ViewState["InvalidText"] = value;
            }
        }

        /// <summary>
        /// EXPERIMENTAL The effect used when displaying a validation message under the field (defaults to 'normal').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("normal")]
        [Description("EXPERIMENTAL The effect used when displaying a validation message under the field (defaults to 'normal').")]
        public virtual string MsgFx
        {
            get
            {
                return (string)this.ViewState["MsgFx"] ?? "normal";
            }
            set
            {
                this.ViewState["MsgFx"] = value;
            }
        }

        /// <summary>
        /// The location where error text should display. (defaults to 'Qtip').
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [TypeConverter(typeof(MessageTarget))]
        [DefaultValue(MessageTarget.Qtip)]
        [Description("The location where error text should display. (defaults to 'Qtip').")]
        public virtual MessageTarget MsgTarget
        {
            get
            {
                object obj = this.ViewState["MsgTarget"];
                return (obj == null) ? MessageTarget.Qtip : (MessageTarget)obj;
            }
            set
            {
                this.ViewState["MsgTarget"] = value;
            }
        }

        /// <summary>
        /// True to mark the field as readOnly in HTML (defaults to false) -- Note: this only sets the element's readOnly DOM attribute.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetReadOnly")]
        [ClientConfig]
        [Category("Config Options")]
        [Bindable(true)]
        [DefaultValue(false)]
        [Description("True to mark the field as readOnly in HTML (defaults to false) -- Note: this only sets the element's readOnly DOM attribute.")]
        public virtual bool ReadOnly 
        {
            get
            {
                object obj = this.ViewState["ReadOnly"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ReadOnly"] = value;
            }
        }

        /// <summary>
        /// The tabIndex for this field. Note this only applies to fields that are rendered, not those which are built via applyTo (defaults to undefined).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue((short)0)]
        [Description("The tabIndex for this field. Note this only applies to fields that are rendered, not those which are built via applyTo (defaults to undefined).")]
        public override short TabIndex
        {
            get
            {
                object obj = this.ViewState["TabIndex"];
                return (obj == null) ? (short)0 : (short)obj;
            }
            set
            {
                this.ViewState["TabIndex"] = value;
            }
        }

        /// <summary>
        /// Whether the field should validate when it loses focus (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("Whether the field should validate when it loses focus (defaults to true).")]
        public virtual bool ValidateOnBlur
        {
            get
            {
                object obj = this.ViewState["ValidateOnBlur"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ValidateOnBlur"] = value;
            }
        }

        /// <summary>
        /// The length of time in milliseconds after user input begins until validation is initiated (defaults to 250).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(250)]
        [Description("The length of time in milliseconds after user input begins until validation is initiated (defaults to 250).")]
        public virtual int ValidateDelay
        {
            get
            {
                object obj = this.ViewState["ValidateDelay"];
                return (obj == null) ? 250 : (int)obj;
            }
            set
            {
                this.ViewState["ValidateDelay"] = value;
            }
        }


        /// <summary>
        /// The event that should initiate field validation. Set to false to disable automatic validation (defaults to 'keyup').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The event that should initiate field validation. Set to false to disable automatic validation (defaults to 'keyup').")]
        public virtual string ValidationEvent
        {
            get
            {
                return (string)this.ViewState["ValidationEvent"] ?? "";
            }
            set
            {
                this.ViewState["ValidationEvent"] = value;
            }
        }

        /// <summary>
        /// A value to initialize this field with.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Misc")]
        [DefaultValue(null)]
        [Description("A value to initialize this field with.")]
        public virtual object Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? null : obj;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        /// <summary>
        /// The note.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The note.")]
        [AjaxEventUpdate(MethodName = "SetNote")]
        public virtual string Note
        {
            get
            {
                return (string)this.ViewState["Note"] ?? "";
            }
            set
            {
                this.ViewState["Note"] = value;
            }
        }

        /// <summary>
        /// The note css class.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The note css class.")]
        [AjaxEventUpdate(MethodName = "SetNoteCls")]
        public virtual string NoteCls
        {
            get
            {
                return (string)this.ViewState["NoteCls"] ?? "";
            }
            set
            {
                this.ViewState["NoteCls"] = value;
            }
        }

        /// <summary>
        /// Note align
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(NoteAlign.Down)]
        [Description("Note align")]
        public virtual NoteAlign NoteAlign
        {
            get
            {
                object obj = this.ViewState["NoteAlign"];
                return (obj == null) ? NoteAlign.Down : (NoteAlign)obj;
            }
            set
            {
                this.ViewState["NoteAlign"] = value;
            }
        }

        /// <summary>
        /// True to encode note text
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to encode note text")]
        public virtual bool NoteEncode
        {
            get
            {
                object obj = this.ViewState["NoteEncode"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["NoteEncode"] = value;
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Clear any invalid styles/messages for this field
        /// </summary>
        [Description("Clear any invalid styles/messages for this field")]
        public virtual void ClearInvalid()
        {
            string template = "{0}.clearInvalid();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Mark this field as invalid, using msgTarget to determine how to display the error and applying invalidClass to the field's element.
        /// </summary>
        [Description("Mark this field as invalid, using msgTarget to determine how to display the error and applying invalidClass to the field's element.")]
        public virtual void MarkInvalid()
        {
            string template = "{0}.markInvalid();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Mark this field as invalid, using msgTarget to determine how to display the error and applying invalidClass to the field's element.
        /// </summary>
        [Description("Mark this field as invalid, using msgTarget to determine how to display the error and applying invalidClass to the field's element.")]
        public virtual void MarkInvalid(string msg)
        {
            string template = "{0}.markInvalid(\"{1}\");";
            this.AddScript(template, this.ClientID, msg);
        }

        /// <summary>
        /// Resets the current field value to the originally loaded value and clears any validation messages
        /// </summary>
        [Description("Resets the current field value to the originally loaded value and clears any validation messages")]
        public virtual void Reset()
        {
            string template = "{0}.reset();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Sets the underlying DOM field's value directly, bypassing validation. To set the value with validation see setValue.
        /// </summary>
        [Description("Sets the underlying DOM field's value directly, bypassing validation. To set the value with validation see setValue.")]
        public virtual void SetRawValue(object value)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setRawValue({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value, converters));
        }

        /// <summary>
        /// Sets a data value into the field and validates it. To set the value directly without validation see setRawValue.
        /// </summary>
        [Description("Sets a data value into the field and validates it. To set the value directly without validation see setRawValue.")]
        public virtual void SetValue(object value)
        {
            if (!this.IsParentDeferredRender && this.Visible)
            {
                List<JsonConverter> converters = new List<JsonConverter>();
                converters.Add(new CtorDateTimeJsonConverter());

                this.ScriptManager.SetValue(this.ClientID, JSON.Serialize(value, converters));
            }
        }

        protected virtual void SetReadOnly(bool value)
        {
            string template = "{0}.setReadOnly({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }

        protected virtual void SetNoteCls(string cls)
        {
            string template = "{0}.setNoteCls({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(cls));
        }

        protected virtual void SetNote(string note)
        {
            this.SetNote(note, this.NoteEncode);
        }

        public virtual void SetNote(string note, bool encode)
        {
            string template = "{0}.setNote({1}, {2});";
            this.AddScript(template, this.ClientID, JSON.Serialize(note), JSON.Serialize(encode));
        }

        public virtual void ShowNote()
        {
            string template = "{0}.showNote();";
            this.AddScript(template, this.ClientID);
        }

        public virtual void HideNote()
        {
            string template = "{0}.hideNote();";
            this.AddScript(template, this.ClientID);
        }

        /*  IPostBackDataHandler + IPostBackEventHandler
            -----------------------------------------------------------------------------------------------*/

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return this.LoadPostData(postDataKey, postCollection);
        }

        protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.RaisePostDataChangedEvent();
        }

        protected virtual void RaisePostDataChangedEvent() { }
        
    }
}