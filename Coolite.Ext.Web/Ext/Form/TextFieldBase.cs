/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Base Class for all Text Field Web Controls.
    /// </summary>
    [Xtype("textfield")]
    [InstanceOf(ClassName = "Ext.form.TextField")]
    [Description("Base Class for all Text Field Web Controls.")]
    public abstract class TextFieldBase : Field
    {
        [ClientConfig(JsonMode.Ignore)]
        [DefaultValue("")]
        public override string AutoCreate
        {
            get
            {
                return base.AutoCreate;
            }
            set
            {
                base.AutoCreate = value;
            }
        }

        [ClientConfig("autoCreate", JsonMode.Raw)]
        [DefaultValue("")]
        protected virtual string AutoCreateProxy
        {
            get
            {
                string obj = "";

                if (this.MaxLength > -1 && this.Truncate && string.IsNullOrEmpty(this.AutoCreate))
                {
                    JsonObject temp = new JsonObject();

                    temp["tag"] = "input";
                    temp["type"] = "text";
                    temp["maxlength"] = this.MaxLength;
                    temp["autocomplete"] = "off";

                    obj = temp.ToJson();
                }

                return obj;
            }
        }

        /// <summary>
        /// False to validate that the value length > 0 (defaults to true).
        /// </summary>
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

        /// <summary>
        /// Error text to display if the allow blank validation fails (defaults to 'This field is required').
        /// </summary>
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

        /// <summary>
        /// True to disable input keystroke filtering (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to disable input keystroke filtering (defaults to false).")]
        public virtual bool DisableKeyFilter
        {
            get
            {
                object obj = this.ViewState["DisableKeyFilter"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DisableKeyFilter"] = value;
            }
        }

        /// <summary>
        /// The CSS class to apply to an empty field to style the emptyText (defaults to 'x-form-empty-field'). This class is automatically added and removed as needed depending on the current field value.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The CSS class to apply to an empty field to style the emptyText (defaults to 'x-form-empty-field'). This class is automatically added and removed as needed depending on the current field value.")]
        public virtual string EmptyClass
        {
            get
            {
                return (string)this.ViewState["EmptyClass"] ?? "";
            }
            set
            {
                this.ViewState["EmptyClass"] = value;
            }
        }

        /// <summary>
        /// The default text to display in an empty field (defaults to null).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The default text to display in an empty field (defaults to null).")]
        public virtual string EmptyText
        {
            get
            {
                return (string)this.ViewState["EmptyText"] ?? "";
            }
            set
            {
                this.ViewState["EmptyText"] = value;
            }
        }

        /// <summary>
        /// True to enable the proxying of key events for the HTML input field (defaults to false)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable the proxying of key events for the HTML input field (defaults to false)")]
        public virtual bool EnableKeyEvents
        {
            get
            {
                object obj = this.ViewState["EnableKeyEvents"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["EnableKeyEvents"] = value;
            }
        }

        /// <summary>
        /// True if this field should automatically grow and shrink to its content (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if this field should automatically grow and shrink to its content (defaults to false).")]
        public virtual bool Grow
        {
            get
            {
                object obj = this.ViewState["Grow"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Grow"] = value;
            }
        }

        /// <summary>
        /// The maximum width to allow when grow = true (defaults to 800).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "800")]
        [Description("The maximum width to allow when grow = true (defaults to 800).")]
        public virtual Unit GrowMax
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["GrowMax"], Unit.Pixel(800), "GrowMax");
            }
            set
            {
                this.ViewState["GrowMax"] = value;
            }
        }

        /// <summary>
        /// The minimum width to allow when grow = true (defaults to 30).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "30")]
        [Description("The minimum width to allow when grow = true (defaults to 30).")]
        public virtual Unit GrowMin
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["GrowMin"], Unit.Pixel(30), "GrowMin");
            }
            set
            {
                this.ViewState["GrowMin"] = value;
            }
        }

        /// <summary>
        /// The type attribute for input fields.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(InputType.Text)]
        [Description("The type attribute for input fields.")]
        public virtual InputType InputType
        {
            get
            {
                object obj = this.ViewState["InputType"];
                return (obj == null) ? InputType.Text : (InputType)obj;
            }
            set
            {
                this.ViewState["InputType"] = value;
            }
        }

        /// <summary>
        /// An input mask regular expression that will be used to filter keystrokes that don't match (defaults to null).
        /// </summary>
        [ClientConfig(typeof(RegexJsonConverter))]
        [Category("Config Options")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.WebControls.RegexTypeEditor", typeof(UITypeEditor))]
        [Description("An input mask regular expression that will be used to filter keystrokes that don't match (defaults to null).")]
        public virtual string MaskRe
        {
            get
            {
                return (string)this.ViewState["MaskRe"] ?? "";
            }
            set
            {
                this.ViewState["MaskRe"] = value;
            }
        }

        /// <summary>
        /// Maximum input field length allowed (defaults to Number.MAX_VALUE).
        /// </summary>
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

        /// <summary>
        /// Error text to display if the maximum length validation fails (defaults to 'The maximum length for this field is {maxLength}').
        /// </summary>
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

        /// <summary>
        /// Minimum input field length required (defaults to 0).
        /// </summary>
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

        /// <summary>
        /// Error text to display if the minimum length validation fails (defaults to 'The minimum length for this field is {minLength}').
        /// </summary>
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

        /// <summary>
        /// A JavaScript RegExp object to be tested against the field value during validation (defaults to null). If available, this regex will be evaluated only after the basic validators all return true, and will be passed the current field value. If the test fails, the field will be marked invalid using RegexText.
        /// </summary>
        [ClientConfig(typeof(RegexJsonConverter))]
        [Category("Config Options")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.WebControls.RegexTypeEditor", typeof(UITypeEditor))]
        [Description("A JavaScript RegExp object to be tested against the field value during validation (defaults to null). If available, this regex will be evaluated only after the basic validators all return true, and will be passed the current field value. If the test fails, the field will be marked invalid using RegexText.")]
        public virtual string Regex
        {
            get
            {
                return (string)this.ViewState["Regex"] ?? "";
            }
            set
            {
                this.ViewState["Regex"] = value;
            }
        }

        /// <summary>
        /// The error text to display if regex is used and the test fails during validation (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to display if regex is used and the test fails during validation (defaults to '').")]
        public virtual string RegexText
        {
            get
            {
                return (string)this.ViewState["RegexText"] ?? "";
            }
            set
            {
                this.ViewState["RegexText"] = value;
            }
        }

        /// <summary>
        /// True to automatically select any existing field text when the field receives input focus (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to automatically select any existing field text when the field receives input focus (defaults to false).")]
        public virtual bool SelectOnFocus
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

        /// <summary>
        /// If MaxLength property has been set, more characters than the MaxLength can be entered if Truncate='false'. If 'false', then a validation error is rendered if more characters entered (or pasted) than the MaxLength property. Default value is 'true'.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("If MaxLength property has been set, more characters than the MaxLength can be entered if Truncate='false'. If 'false', then a validation error is rendered if more characters entered (or pasted) than the MaxLength property. Default value is 'true'.")]
        public virtual bool Truncate
        {
            get
            {
                object obj = this.ViewState["Truncate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Truncate"] = value;
            }
        }

        /// <summary>
        /// A custom validation function to be called during field validation (defaults to null). If available, this function will be called only after the basic validators all return true, and will be passed the current field value and expected to return boolean true if the value is valid or a string error message if invalid.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.WebControls.RegexTypeEditor", typeof(UITypeEditor))]
        [Description("A custom validation function to be called during field validation (defaults to null). If available, this function will be called only after the basic validators all return true, and will be passed the current field value and expected to return boolean true if the value is valid or a string error message if invalid.")]
        public virtual string Validator
        {
            get
            {
                return (string)this.ViewState["Validator"] ?? "";
            }
            set
            {
                this.ViewState["Validator"] = value;
            }
        }

        /// <summary>
        /// A validation type name as defined in Ext.form.VTypes (defaults to null).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A validation type name as defined in Ext.form.VTypes (defaults to null).")]
        public virtual string Vtype
        {
            get
            {
                return (string)this.ViewState["Vtype"] ?? "";
            }
            set
            {
                this.ViewState["Vtype"] = value;
            }
        }

        /// <summary>
        /// A custom error message to display in place of the default message provided for the vtype currently set for this field (defaults to ''). Only applies if vtype is set, else ignored.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("A custom error message to display in place of the default message provided for the vtype currently set for this field (defaults to ''). Only applies if vtype is set, else ignored.")]
        public virtual string VtypeText
        {
            get
            {
                return (string)this.ViewState["VtypeText"] ?? "";
            }
            set
            {
                this.ViewState["VtypeText"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Resets the current field value to the originally-loaded value and clears any validation messages. Also adds emptyText and emptyClass if the original value was blank.
        /// </summary>
        [Description("Resets the current field value to the originally-loaded value and clears any validation messages. Also adds emptyText and emptyClass if the original value was blank.")]
        public override void Reset()
        {
            string template = "{0}.reset();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Automatically grows the field to accomodate the width of the text up to the maximum field width allowed. This only takes effect if grow = true, and fires the autosize event.
        /// </summary>
        [Description("Automatically grows the field to accomodate the width of the text up to the maximum field width allowed. This only takes effect if grow = true, and fires the autosize event.")]
        public virtual void AutoSize()
        {
            string template = "{0}.autoSize();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Selects text in this field
        /// </summary>
        [Description("Selects text in this field")]
        public virtual void SelectText()
        {
            string template = "{0}.selectText();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Selects text in this field
        /// </summary>
        [Description("Selects text in this field")]
        public virtual void SelectText(int start)
        {
            string template = "{0}.selectText({1});";
            this.AddScript(template, this.ClientID, start);
        }

        /// <summary>
        /// Selects text in this field
        /// </summary>
        [Description("Selects text in this field")]
        public virtual void SelectText(int start, int end)
        {
            string template = "{0}.selectText({1},{2});";
            this.AddScript(template, this.ClientID, start, end);
        }
    }
}