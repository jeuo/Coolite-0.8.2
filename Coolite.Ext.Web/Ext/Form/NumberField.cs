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
using System.Drawing;
using System.Globalization;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Numeric text field that provides automatic keystroke filtering and numeric validation.
    /// </summary>
    [Xtype("numberfield")]
    [InstanceOf(ClassName = "Ext.form.NumberField")]
    [ToolboxData("<{0}:NumberField runat=\"server\" />")]
    [DefaultProperty("Text")]
    [DefaultEvent("TextChanged")]
    [ValidationProperty("Text")]
    [ControlValueProperty("Text")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(NumberFieldDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.NumberField), "Build.Resources.ToolboxIcons.NumberField.bmp")]
    [Description("Numeric text field that provides automatic keystroke filtering and numeric validation.")]
    public class NumberField : TextFieldBase, IEditableTextControl, ITextControl, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                this.On("change", new JFunction(this.PostBackFunction));
            }

            this.SetValue(this.Text);
        }

        [ClientConfig(JsonMode.ToLower)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override InputType InputType
        {
            get
            {
                return InputType.Text;
            }
            set
            {
                base.InputType = value;
            }
        }

        /// <summary>
        /// The Text value to initialize this field with.
        /// </summary>
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue("")]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("The Text value to initialize this field with.")]
        public virtual string Text
        {
            get
            {
                if (this.IsNull)
                {
                    return "";
                }
                return this.Number.ToString(NumberFormatInfo.InvariantInfo);
            }
            set
            {
                this.Number = this.CheckRange(value);
            }
        }

        public virtual void SetValue(double value)
        {
            if (!value.Equals(double.MinValue))
            {
                base.SetValue(value);
            }
            else
            {
                base.SetValue(null);
            }
        }

        /// <summary>
        /// The Number (double) to initialize this field with.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue(Double.MinValue)]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("The Number (double) to initialize this field with.")]
        public virtual double Number
        {
            get
            {
                object obj = this.ViewState["Number"];
                return (obj == null) ? Double.MinValue : this.CheckRange((double)obj);
            }
            set
            {
                this.ViewState["Number"] = this.CheckRange(value);
            }
        }

        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Number = (double)value;
            }
        }


        /// <summary>
        /// Gets a value indicating whether the Number is equal to double.MinValue.
        /// </summary>
        [Description("Gets a value indicating whether the Number is equal to double.MinValue.")]
        public virtual bool IsNull
        {
            get
            {
                return (this.Number == double.MinValue);
            }
        }

        public virtual void Clear()
        {
            this.Number = this.MinValue;
        }

        protected virtual double CheckRange(string value)
        {
            if (string.IsNullOrEmpty(value) || value == this.EmptyText || value == this.BlankText)
            {
                return double.MinValue;
            }
            Double number;
            try
            {
                if (!string.IsNullOrEmpty(this.DecimalSeparator))
                {
                    NumberFormatInfo nf = new NumberFormatInfo();
                    nf.NumberDecimalSeparator = this.DecimalSeparator;
                    number = Double.Parse(value, nf);
                }
                else
                {
                    number = Double.Parse(value, NumberFormatInfo.InvariantInfo);
                }
                
            }
            catch (Exception exception)
            {
                throw new InvalidCastException("The Text value supplied is not a type of Double. " + exception.Message);
            }
            return this.CheckRange(number);
        }

        protected virtual double CheckRange(double number)
        {
            number = (this.MinValue > number) ? this.MinValue : number;
            return (this.MaxValue < number) ? this.MaxValue : number;
        }

        private TextFieldListeners listeners;

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
        public TextFieldListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TextFieldListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private TextFieldAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public TextFieldAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TextFieldAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.MinValue > this.MaxValue)
            {
                throw new ArgumentOutOfRangeException("MinValue", "The MinValue must be less then the MaxValue.");
            }

            base.Render(writer);
        }

        private static readonly object EventNumberChanged = new object();

        /// <summary>
        /// Fires when the Text property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Text property has been changed")]
        public event EventHandler TextChanged
        {
            add
            {
                Events.AddHandler(EventNumberChanged, value);
            }
            remove
            {
                Events.RemoveHandler(EventNumberChanged, value);
            }
        }

        protected virtual void OnNumberChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EventNumberChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[this.UniqueName];
            
            if (val != null && !this.ReadOnly && !this.Text.Equals(val))
            {
                try
                {
                    this.ViewState.Suspend();
                    this.Text = val.Equals(this.EmptyText) ? "" : val;
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return true;
            }
            return false;
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnNumberChanged(EventArgs.Empty);
        }


        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// False to disallow decimal values (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disallow decimal values (defaults to true).")]
        public virtual bool AllowDecimals
        {
            get
            {
                object obj = this.ViewState["AllowDecimals"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowDecimals"] = value;
            }
        }

        /// <summary>
        /// False to prevent entering a negative sign (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to prevent entering a negative sign (defaults to true).")]
        public virtual bool AllowNegative
        {
            get
            {
                object obj = this.ViewState["AllowNegative"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowNegative"] = value;
            }
        }

        /// <summary>
        /// The base set of characters to evaluate as valid numbers (defaults to '0123456789').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("0123456789")]
        [Description("The base set of characters to evaluate as valid numbers (defaults to '0123456789').")]
        public virtual string BaseChars
        {
            get
            {
                object obj = this.ViewState["BaseChars"];
                return (obj == null) ? "0123456789" : (string)obj;
            }
            set
            {
                this.ViewState["BaseChars"] = value;
            }
        }

        /// <summary>
        /// The maximum precision to display after the decimal separator (defaults to 2).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(2)]
        [Description("The maximum precision to display after the decimal separator (defaults to 2).")]
        public virtual int DecimalPrecision
        {
            get
            {
                object obj = this.ViewState["DecimalPrecision"];
                return (obj == null) ? 2 : (int)obj;
            }
            set
            {
                this.ViewState["DecimalPrecision"] = value;
            }
        }

        /// <summary>
        /// False to disallow trim trailed zeros.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disallow trim trailed zeros.")]
        public virtual bool TrimTrailedZeros
        {
            get
            {
                object obj = this.ViewState["TrimTrailedZeros"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["TrimTrailedZeros"] = value;
            }
        }

        /// <summary>
        /// Character(s) to allow as the decimal separator (defaults to '.').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(".")]
        [Description("Character(s) to allow as the decimal separator (defaults to '.').")]
        public virtual string DecimalSeparator
        {
            get
            {
                return (string)this.ViewState["DecimalSeparator"] ?? ".";
            }
            set
            {
                this.ViewState["DecimalSeparator"] = value;
            }
        }

        /// <summary>
        /// Error text to display if the maximum value validation fails (defaults to 'The maximum value for this field is {maxValue}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("The maximum value for this field is {maxValue}")]
        [Localizable(true)]
        [Description("Error text to display if the maximum value validation fails (defaults to 'The maximum value for this field is {maxValue}').")]
        public virtual string MaxText
        {
            get
            {
                return (string)this.ViewState["MaxText"] ?? "The maximum value for this field is {maxValue}";
            }
            set
            {
                this.ViewState["MaxText"] = value;
            }
        }

        /// <summary>
        /// The maximum allowed value (defaults to Double.MaxValue)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(Double.MaxValue)]
        [Description("The maximum allowed value (defaults to Double.MaxValue)")]
        public virtual Double MaxValue
        {
            get
            {
                object obj = this.ViewState["MaxValue"];
                return (obj == null) ? Double.MaxValue : (Double)obj;
            }
            set
            {
                this.ViewState["MaxValue"] = value;
            }
        }

        /// <summary>
        /// Error text to display if the minimum value validation fails (defaults to 'The minimum value for this field is {minValue}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("The minimum value for this field is {minValue}")]
        [Localizable(true)]
        [Description("Error text to display if the minimum value validation fails (defaults to 'The minimum value for this field is {minValue}').")]
        public virtual string MinText
        {
            get
            {
                return (string)this.ViewState["MinText"] ?? "The minimum value for this field is {minValue}";
            }
            set
            {
                this.ViewState["MinText"] = value;
            }
        }

        /// <summary>
        /// The minimum allowed value (defaults to Double.MinValue)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(Double.MinValue)]
        [Description("The minimum allowed value (defaults to Double.MinValue)")]
        public virtual Double MinValue
        {
            get
            {
                object obj = this.ViewState["MinValue"];
                return (obj == null) ? Double.MinValue : (Double)obj;
            }
            set
            {
                this.ViewState["MinValue"] = value;
            }
        }

        /// <summary>
        /// Error text to display if the value is not a valid number. For example, this can happen if a valid character like '.' or '-' is left in the field with no number (defaults to '{value} is not a valid number').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("{value} is not a valid number")]
        [Localizable(true)]
        [Description("Error text to display if the value is not a valid number. For example, this can happen if a valid character like '.' or '-' is left in the field with no number (defaults to '{value} is not a valid number').")]
        public virtual string NanText
        {
            get
            {
                return (string)this.ViewState["NanText"] ?? "{value} is not a valid number";
            }
            set
            {
                this.ViewState["NanText"] = value;
            }
        }
    }
}