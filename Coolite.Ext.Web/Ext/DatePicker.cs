/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Simple DatePicker class.
    /// </summary>
    [Xtype("datepicker")]
    [InstanceOf(ClassName = "Ext.DatePicker")]
    [ToolboxData("<{0}:DatePicker runat=\"server\" />")]
    [DefaultProperty("SelectedDate")]
    [DefaultEvent("SelectionChanged")]
    [ValidationProperty("SelectedDate")]
    [ControlValueProperty("SelectedDate")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(DatePickerDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.DatePicker), "Build.Resources.ToolboxIcons.DatePicker.bmp")]
    [Description("Simple DatePicker class.")]
    public class DatePicker : Component, IDate, IAutoPostBack, IPostBackDataHandler, IPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                HandlerConfig cfg = new HandlerConfig();
                cfg.Delay = 10;
                this.On("select", new JFunction(this.PostBackFunction), "this", cfg);
            }
        }

        /// <summary>
        /// AutoPostBack
        /// </summary>
        /// <value><c>true</c> if [auto post back]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        [Description("AutoPostBack")]
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
                return (obj != null && (bool)obj);
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
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
                this.ViewState["ValidationGroup"] = value;
            }
        }

        DateTime selectedDate = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the current selected date of the DatePicker. Accepts and returns a DateTime object.
        /// </summary>
        [AjaxEventUpdate(MethodName="SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("Gets or sets the current selected date of the DatePicker. Accepts and returns a DateTime object.")]
        public virtual DateTime SelectedDate
        {
            get
            {
                object obj = this.ViewState["SelectedDate"];
                return (obj == null) ? DateTime.MinValue : (DateTime)obj;
            }
            set
            {
                this.ViewState["SelectedDate"] = value.Date;
            }
        }

        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("Gets or sets the current selected date of the DatePicker.")]
        public object SelectedValue
        {
            get
            {
                if (this.IsNull)
                {
                    return null;
                }
                else
                {
                    return this.SelectedDate;
                }
            }
            set
            {
                object init = value;

                if (init is DateTime)
                {
                    this.SelectedDate = (DateTime)init;
                }
                else if (init == null || init is System.DBNull)
                {
                    this.SelectedDate = DateTime.MinValue;
                }
                else
                {
                    try
                    {
                        this.SelectedDate = DateTime.Parse(init.ToString(), System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
                    }
                    catch (FormatException)
                    {
                        this.SelectedDate = DateTime.MinValue;
                    }
                }
            }
        }

        [ClientConfig(typeof(CtorDateTimeJsonConverter))]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual object Value
        {
            get
            {
                return this.SelectedDate;
            }
            set
            {
                DateTime temp = DateTime.MinValue;

                if (value is string)
                {
                    try
                    {
                        temp = DateTime.ParseExact((string)value, this.Format, System.Threading.Thread.CurrentThread.CurrentCulture);
                    }
                    catch
                    {
                        try
                        {
                            temp = DateTime.Parse((string)value, System.Threading.Thread.CurrentThread.CurrentCulture);
                        }
                        catch { }
                    }
                }
                else if (value is DateTime)
                {
                    temp = (DateTime)value;
                }
                
                this.SelectedDate = temp;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the SelectedDate is [Null]. There is no DateTime Null value in .NET, so DateTime.MinValue is returned to represent [Null].
        /// </summary>
        [Browsable(false)]
        [Description("Gets a value indicating whether the SelectedDate is [Null]. There is no DateTime Null value in .NET, so DateTime.MinValue is returned to represent [Null].")]
        public virtual bool IsNull
        {
            get
            {
                return (this.SelectedDate == DateTime.MinValue);
            }
        }

        /// <summary>
        /// The text to display on the cancel button.
        /// </summary>
        [ClientConfig]
        [UrlProperty]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The text to display on the cancel button.")]
        public virtual string CancelText
        {
            get
            {
                return (string)this.ViewState["CancelText"] ?? "";
            }
            set
            {
                this.ViewState["CancelText"] = value;
            }
        }

        private DisabledDateCollection disabledDates;
        /// <summary>
        /// An array of \"dates\" to disable, as strings. These strings will be used to build a dynamic regular expression so they are very powerful.
        /// </summary>

        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An array of \"dates\" to disable, as strings. These strings will be used to build a dynamic regular expression so they are very powerful.")]
        public virtual DisabledDateCollection DisabledDates
        {
            get
            {
                if (this.disabledDates == null)
                {
                    this.disabledDates = new DisabledDateCollection();
                }
                return this.disabledDates;
            }
        }

        [ClientConfig("disabledDates", JsonMode.Raw)]
        [DefaultValue("")]
        internal string DisabledDatesProxy
        {
            get
            {
                this.DisabledDates.Format = string.IsNullOrEmpty(this.Format) ? "yyyy-MM-dd\\Thh:mm:ss" : this.Format;
                return this.DisabledDates.ToString();
            }
        }

        /// <summary>
        /// An array of textual day names which can be overriden for localization support (defaults to Date.dayNames).
        /// </summary>
        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("An array of textual day names which can be overriden for localization support (defaults to Date.dayNames).")]
        public virtual string[] DayNames
        {
            get
            {
                object obj = this.ViewState["DayNames"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["DayNames"] = value;
            }
        }

        /// <summary>
        /// JavaScript regular expression used to disable a pattern of dates (defaults to null).
        /// </summary>
        [ClientConfig(typeof(RegexJsonConverter))]
        [Category("Config Options")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.WebControls.RegexTypeEditor", typeof(UITypeEditor))]
        [Description("JavaScript regular expression used to disable a pattern of dates (defaults to null).")]
        public virtual string DisabledDatesRE
        {
            get
            {
                return (string)this.ViewState["DisabledDatesRE"] ?? "";
            }
            set
            {
                this.ViewState["DisabledDatesRE"] = value;
            }
        }

        /// <summary>
        /// An array of days to disable, 0-based. For example, [0, 6] disables Sunday and Saturday (defaults to null).
        /// </summary>
        [ClientConfig(typeof(IntArrayJsonConverter))]
        [TypeConverter(typeof(IntArrayConverter))]
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("An array of days to disable, 0-based. For example, [0, 6] disables Sunday and Saturday (defaults to null).")]
        public virtual int[] DisabledDays
        {
            get
            {
                object obj = this.ViewState["DisabledDays"];
                return (obj == null) ? null : (int[])obj;
            }
            set
            {
                this.ViewState["DisabledDays"] = value;
            }
        }

        /// <summary>
        /// The tooltip to display when the date falls on a disabled day (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The tooltip to display when the date falls on a disabled day (defaults to '').")]
        public virtual string DisabledDaysText
        {
            get
            {
                return (string)this.ViewState["DisabledDaysText"] ?? "";
            }
            set
            {
                this.ViewState["DisabledDaysText"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("d")]
        [Description("")]
        public virtual string Format
        {
            get
            {
                return (string)this.ViewState["Format"] ?? "d";
            }
            set
            {
                this.ViewState["Format"] = value;
            }
        }

        [ClientConfig("format")]
        [DefaultValue("")]
        protected virtual string FormatProxy
        {
            get
            {
                return DateTimeUtils.ConvertNetToPHP(this.Format);
            }
        }

        /// <summary>
        /// The maximum allowed date.
        /// </summary>
        [ClientConfig(typeof(CtorDateTimeJsonConverter))]
        [AjaxEventUpdate(MethodName = "SetMaxDate")]
        [Category("Config Options")]
        [Bindable(true)]
        [Description("The maximum allowed date.")]
        public virtual DateTime MaxDate
        {
            get
            {
                object obj = this.ViewState["MaxDate"];
                if (obj == null && this.DesignMode)
                {
                    return DateTime.MinValue;
                }
                return (obj == null) ? DateTime.MaxValue : (DateTime)obj;

            }
            set
            {
                this.ViewState["MaxDate"] = value.Date;
            }
        }

        /// <summary>
        /// The error text to display when the date in the cell is after MaxValue (defaults to 'The date in this field must be before {MaxValue}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to display when the date in the cell is after MaxValue (defaults to 'The date in this field must be before {MaxValue}').")]
        public virtual string MaxText
        {
            get
            {
                return (string)this.ViewState["MaxText"] ?? "";
            }
            set
            {
                this.ViewState["MaxText"] = value;
            }
        }

        /// <summary>
        /// The minimum allowed date.
        /// </summary>
        [ClientConfig(typeof(CtorDateTimeJsonConverter))]
        [AjaxEventUpdate(MethodName = "SetMinDate")]
        [Category("Config Options")]
        [Bindable(true)]
        [Description("The minimum allowed date.")]
        public virtual DateTime MinDate
        {
            get
            {
                object obj = this.ViewState["MinDate"];
                return (obj == null) ? DateTime.MinValue : (DateTime)obj;

            }
            set
            {
                this.ViewState["MinDate"] = value.Date;
            }
        }

        /// <summary>
        /// The error text to display when the date in the cell is before MinValue (defaults to 'The date in this field must be after {MinValue}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to display when the date in the cell is before MinValue (defaults to 'The date in this field must be after {MinValue}').")]
        public virtual string MinText
        {
            get
            {
                return (string)this.ViewState["MinText"] ?? "";
            }
            set
            {
                this.ViewState["MinText"] = value;
            }
        }

        /// <summary>
        /// An array of textual month names which can be overriden for localization support (defaults to Date.monthNames).
        /// </summary>
        [ClientConfig(typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("An array of textual month names which can be overriden for localization support (defaults to Date.monthNames).")]
        public virtual string[] MonthNames
        {
            get
            {
                object obj = this.ViewState["MonthNames"];
                return (obj == null) ? null : (string[])obj;
            }
            set
            {
                this.ViewState["MonthNames"] = value;
            }
        }

        /// <summary>
        /// The header month selector tooltip (defaults to 'Choose a month (Control+Up/Down to move years)').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Choose a month (Control+Up/Down to move years)")]
        [Localizable(true)]
        [Description("The header month selector tooltip (defaults to 'Choose a month (Control+Up/Down to move years)').")]
        public virtual string MonthYearText
        {
            get
            {
                return (string)this.ViewState["MonthYearText"] ?? "Choose a month (Control+Up/Down to move years)";
            }
            set
            {
                this.ViewState["MonthYearText"] = value;
            }
        }

        /// <summary>
        /// The next month navigation button tooltip (defaults to 'Next Month (Control+Right)').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Next Month (Control+Right)")]
        [Localizable(true)]
        [Description("The next month navigation button tooltip (defaults to 'Next Month (Control+Right)').")]
        public virtual string NextText
        {
            get
            {
                return (string)this.ViewState["NextText"] ?? "Next Month (Control+Right)";
            }
            set
            {
                this.ViewState["NextText"] = value;
            }
        }

        /// <summary>
        /// The text to display on the ok button.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The text to display on the ok button.")]
        public virtual string OkText
        {
            get
            {
                return (string)this.ViewState["OkText"] ?? "";
            }
            set
            {
                this.ViewState["OkText"] = value;
            }
        }

        /// <summary>
        /// The previous month navigation button tooltip (defaults to 'Previous Month (Control+Left)').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Previous Month (Control+Left)")]
        [Localizable(true)]
        [Description("The previous month navigation button tooltip (defaults to 'Previous Month (Control+Left)').")]
        public virtual string PrevText
        {
            get
            {
                return (string)this.ViewState["PrevText"] ?? "Previous Month (Control+Left)";
            }
            set
            {
                this.ViewState["PrevText"] = value;
            }
        }

        /// <summary>
        /// False to hide the footer area containing the Today button and disable the keyboard handler for spacebar that selects the current date (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to hide the footer area containing the Today button and disable the keyboard handler for spacebar that selects the current date (defaults to true).")]
        public virtual bool ShowToday
        {
            get
            {
                object obj = this.ViewState["ShowToday"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ShowToday"] = value;
            }
        }

        /// <summary>
        /// Day index at which the week should begin, 0-based (defaults to 0, which is Sunday).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("Day index at which the week should begin, 0-based (defaults to 0, which is Sunday).")]
        public virtual int StartDay
        {
            get
            {
                object obj = this.ViewState["StartDay"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["StartDay"] = value;
            }
        }

        /// <summary>
        /// The text to display on the button that selects the current date (defaults to 'Today').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Today")]
        [Localizable(true)]
        [Description("The text to display on the button that selects the current date (defaults to 'Today').")]
        public virtual string TodayText
        {
            get
            {
                return (string)this.ViewState["TodayText"] ?? "Today";
            }
            set
            {
                this.ViewState["TodayText"] = value;
            }
        }

        /// <summary>
        /// The tooltip to display for the button that selects the current date (defaults to '{current date} (Spacebar)').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("{current date} (Spacebar)")]
        [Localizable(true)]
        [Description("The tooltip to display for the button that selects the current date (defaults to '{current date} (Spacebar)').")]
        public virtual string TodayTip
        {
            get
            {
                return (string)this.ViewState["TodayTip"] ?? "{current date} (Spacebar)";
            }
            set
            {
                this.ViewState["TodayTip"] = value;
            }
        }

        private DatePickerListeners listeners;

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
        public DatePickerListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new DatePickerListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private DatePickerAjaxEvents ajaxEvents;

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
        public DatePickerAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new DatePickerAjaxEvents();
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

        private static readonly object EventSelectionChanged = new object();

        /// <summary>
        /// Fires when the Item property has been changed
        /// </summary>
        [Category("Action")]
        [Description("Fires when the Item property has been changed")]
        public event EventHandler SelectionChanged
        {
            add
            {
                Events.AddHandler(DatePicker.EventSelectionChanged, value);
            }
            remove
            {
                Events.RemoveHandler(DatePicker.EventSelectionChanged, value);
            }
        }

        protected virtual void OnSelectionChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[DatePicker.EventSelectionChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return this.LoadPostData(postDataKey, postCollection);
        }

        protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[string.Concat(this.ClientID, "_Input")];
            if (!string.IsNullOrEmpty(val))
            {
                try
                {
                    this.ViewState.Suspend();
                    this.SelectedDate = DateTime.ParseExact(val, "yyyy\\-MM\\-dd\\THH\\:mm\\:ss", System.Threading.Thread.CurrentThread.CurrentCulture);
                }
                catch
                {
                    this.SelectedDate = DateTime.MinValue;
                }
                finally
                {
                    this.ViewState.Resume();
                }
                return true;
            }
            return false;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.RaisePostDataChangedEvent();
        }

        protected virtual void RaisePostDataChangedEvent() { }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.OnSelectionChanged(EventArgs.Empty);
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Replaces any existing date with the new value and refreshes the DatePicker.
        /// </summary>
        [Description("Replaces any existing date with the new value and refreshes the DatePicker.")]
        protected virtual void SetValue(DateTime date)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setValue({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(date.Date, converters));
        }

        /// <summary>
        /// Replaces any existing minDate with the new value and refreshes the DatePicker.
        /// </summary>
        [Description("Replaces any existing minDate with the new value and refreshes the DatePicker.")]
        protected virtual void SetMinDate(DateTime minDate)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setMinDate({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(minDate.Date, converters));
        }

        /// <summary>
        /// Replaces any existing maxDate with the new value and refreshes the DatePicker.
        /// </summary>
        [Description("Replaces any existing maxDate with the new value and refreshes the DatePicker.")]
        protected virtual void SetMaxDate(DateTime maxDate)
        {
            List<JsonConverter> converters = new List<JsonConverter>();
            converters.Add(new CtorDateTimeJsonConverter());

            string template = "{0}.setMaxDate({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(maxDate.Date, converters));
        }

        // <summary>
        /// Replaces any existing disabled dates with new values and refreshes the DatePicker.
        /// </summary>
        public void UpdateDisabledDates()
        {
            this.AddScript("{0}.setDisabledDates({1});", this.ClientID, this.DisabledDates.ToString());
        }

        /// <summary>
        /// Replaces any existing disabled days (by index, 0-6) with new values and refreshes the DatePicker.
        /// </summary>
        public void UpdateDisabledDays()
        {
            this.AddScript("{0}.setDisabledDays({1});", JSON.Serialize(this.DisabledDays));
        }
    }
}
