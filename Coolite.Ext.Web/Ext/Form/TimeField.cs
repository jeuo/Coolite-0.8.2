/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Provides a time input field with a time dropdown and automatic time validation.
    /// </summary>
    [Xtype("timefield")]
    [Description("Provides a time input field with a time dropdown and automatic time validation.")]
    [ToolboxData("<{0}:TimeField runat=\"server\"></{0}:TimeField>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TimeField), "Build.Resources.ToolboxIcons.TimeField.bmp")]
    [InstanceOf(ClassName = "Ext.form.TimeField")]
    public class TimeField : ComboBox
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                this.ViewState.Suspend();
                this.SelectedTime = this.initTime;
            }
            finally
            {
                this.ViewState.Resume();
            }
        }

        [ClientConfig("store", JsonMode.Raw)]
        [DefaultValue("")]
        internal override string ItemsProxy
        {
            get
            {
                if (!string.IsNullOrEmpty(this.StoreID) || this.Items.Count == 0)
                {
                    return "";
                }

                return this.ItemsToStore;
            }
        }

        private TimeSpan initTime;
        
        [TypeConverter(typeof(TimeSpanConverter))]
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        public TimeSpan SelectedTime
        {
            get
            {
                if(string.IsNullOrEmpty(this.SelectedItem.Value))
                {
                    return TimeSpan.MinValue;
                }

                DateTime dt = DateTime.ParseExact(this.SelectedItem.Value, this.Format, System.Threading.Thread.CurrentThread.CurrentCulture);
                return dt.TimeOfDay;
            }
            set
            {
                this.initTime = value;

                if (value == TimeSpan.MinValue)
                {
                    this.SelectedItem.Value = "";
                }
                else
                {
                    this.SelectedItem.Value = new DateTime(value.Ticks).ToString(this.Format, System.Threading.Thread.CurrentThread.CurrentCulture);
                }
            }
        }

        [ClientConfig("value")]
        [DefaultValue("")]
        protected virtual string SelectedTimeProxy
        {
            get
            {

                return this.SelectedItem.Value;
            }
        }

        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Appearance")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
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
                    return this.SelectedTime;
                }
            }
            set
            {
                object init = value;

                try
                {
                    if (init is TimeSpan)
                    {
                        this.SelectedTime = (TimeSpan)init;
                    }
                    else if (init is DateTime)
                    {
                        this.SelectedTime = ((DateTime)init).TimeOfDay;
                    }
                    else if (init == null || init is System.DBNull)
                    {
                        this.SelectedTime = TimeSpan.MinValue;
                    }
                    else
                    {
                        try
                        {
                            DateTime postedValue = DateTime.ParseExact(init.ToString(), this.Format, System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat);
                            this.SelectedTime = postedValue.TimeOfDay;
                        }
                        catch (FormatException)
                        {
                            this.SelectedTime = TimeSpan.Parse(init.ToString());
                        }
                    }
                }
                catch
                {
                    this.SelectedTime = TimeSpan.MinValue;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the SelectedTime is [Null]. There is no TimeSpan Null value in .NET, so TimeSpan.MinValue is returned to represent [Null].
        /// </summary>
        [Description("Gets a value indicating whether the SelectedTime is [Null]. There is no TimeSpan Null value in .NET, so TimeSpan.MinValue is returned to represent [Null].")]
        public virtual bool IsNull
        {
            get
            {
                return (this.SelectedTime == TimeSpan.MinValue);
            }
        }

        public virtual void Clear()
        {
            this.SelectedTime = TimeSpan.MinValue;
        }

        [Browsable(false)]
        [AjaxEventUpdate(MethodName = "SetTimeValue")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        [DefaultValue(null)]
        public override object Value
        {
            get
            {
                if (this.IsNull)
                {
                    return null;
                }
                else
                {
                    return this.SelectedTime;
                }
            }
            set
            {
                try
                {
                    if (value is TimeSpan)
                    {
                        this.SelectedTime = (TimeSpan)value;
                    }
                    else if (value is DateTime)
                    {
                        this.SelectedTime = ((DateTime)value).TimeOfDay;
                    }
                    else if (value == null || value is System.DBNull)
                    {
                        this.SelectedTime = TimeSpan.MinValue;
                    }
                    else
                    {
                        try
                        {
                            DateTime postedValue = DateTime.ParseExact(value.ToString(), this.Format, System.Threading.Thread.CurrentThread.CurrentCulture);
                            this.SelectedTime = postedValue.TimeOfDay;
                        }
                        catch
                        {
                            this.SelectedTime = TimeSpan.Parse(value.ToString());
                        }
                    }
                }
                catch
                {
                    this.SelectedTime = TimeSpan.MinValue;
                }
            }
        }

        /// <summary>
        /// Multiple date formats separated by \" | \" to try when parsing a user input value and it doesn't match the defined format (defaults to 'm/d/Y|m-d-y|m-d-Y|m/d|m-d|d').
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("Multiple date formats separated by \" | \" to try when parsing a user input value and it doesn't match the defined format (defaults to 'm/d/Y|m-d-y|m-d-Y|m/d|m-d|d').")]
        public virtual string AltFormats
        {
            get
            {
                return (string)this.ViewState["AltFormats"] ?? "";
            }
            set
            {
                this.ViewState["AltFormats"] = value;
            }
        }

        [ClientConfig("altFormats")]
        [DefaultValue("")]
        protected virtual string AltFormatsProxy
        {
            get
            {
                return !string.IsNullOrEmpty(this.AltFormats) ? DateTimeUtils.ConvertNetToPHP(this.AltFormats) : "";
            }
        }

        /// <summary>
        /// The default date format string which can be overriden for localization support. The format must be valid according to Date.parseDate (defaults to 'm/d/y').
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("t")]
        [Description("The default date format string which can be overriden for localization support. The format must be valid according to Date.parseDate (defaults to 'h:mm tt', e.g., '3:15 PM'). For 24-hour time format try 'H:mm' instead.")]
        public virtual string Format
        {
            get
            {
                return (string)this.ViewState["Format"] ?? "t";
            }
            set
            {
                this.ViewState["Format"] = value;
            }
        }

        [ClientConfig("format", typeof(NetToPHPDateFormatStringJsonConverter))]
        [DefaultValue("")]
        protected virtual string FormatProxy
        {
            get
            {
                return this.Format;
            }
        }

        /// <summary>
        /// The number of minutes between each time value in the list (defaults to 15).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(15)]
        [Description("The number of minutes between each time value in the list (defaults to 15).")]
        public virtual int Increment
        {
            get
            {
                object obj = this.ViewState["Increment"];
                return (obj == null) ? 15 : (int)obj;
            }
            set
            {
                this.ViewState["Increment"] = value;
            }
        }

        /// <summary>
        /// The error text to display when the time is after maxValue (defaults to 'The time in this field must be equal to or before {0}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to display when the time is after maxValue (defaults to 'The time in this field must be equal to or before {0}').")]
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
        /// The maximum allowed time. Can be either a Javascript date object or a string date in a valid format (defaults to TimeSpan.MaxValue).
        /// </summary>
        [Category("Config Options")]
        [Description("The maximum allowed time. Can be either a Javascript date object or a string date in a valid format (defaults to null).")]
        [TypeConverter(typeof(TimeSpanConverter))]
        public virtual TimeSpan MaxTime
        {
            get
            {
                object obj = this.ViewState["MaxTime"];

                if (obj == null && this.DesignMode)
                {
                    return new TimeSpan(23, 59, 59);
                }
                
                return (obj == null) ? TimeSpan.MaxValue : (TimeSpan)obj;
            }
            set
            {
                this.ViewState["MaxTime"] = value;
            }
        }

        [ClientConfig("maxValue")]
        [DefaultValue("")]
        protected virtual string MaxTimeProxy
        {
            get
            {
                return (this.MaxTime != TimeSpan.MaxValue) ? new DateTime(this.MaxTime.Ticks).ToString(this.Format, System.Threading.Thread.CurrentThread.CurrentCulture).ToLower(System.Threading.Thread.CurrentThread.CurrentCulture) : "";
            }
        }

        /// <summary>
        /// The minimum allowed time. Can be either a Javascript date object or a string date in a valid format (defaults to TimeSpan.MinValue).
        /// </summary>
        [Category("Config Options")]
        [TypeConverter(typeof(TimeSpanConverter))]
        [Description("The minimum allowed time. Can be either a Javascript date object or a string date in a valid format (defaults to null).")]
        public virtual TimeSpan MinTime
        {
            get
            {
                object obj = this.ViewState["MinTime"];

                if (obj == null && this.DesignMode)
                {
                    return TimeSpan.Zero;
                }
                
                return (obj == null) ? TimeSpan.MinValue : (TimeSpan)obj;
            }
            set
            {
                this.ViewState["MinTime"] = value;
            }
        }

        [ClientConfig("minValue")]
        [DefaultValue("")]
        protected virtual string MinTimeProxy
        {
            get
            {
                return (this.MinTime != TimeSpan.MinValue) ? new DateTime(this.MinTime.Ticks).ToString(this.Format, System.Threading.Thread.CurrentThread.CurrentCulture).ToLower(System.Threading.Thread.CurrentThread.CurrentCulture) : "";
            }
        }

        /// <summary>
        /// The error text to display when the date in the cell is before minValue (defaults to 'The time in this field must be equal to or after {0}').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The error text to display when the date in the cell is before minValue (defaults to 'The time in this field must be equal to or after {0}').")]
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

        internal void SetSelectedTime(TimeSpan time)
        {
            this.AddScript("this.{0}.setValue({1});", this.ClientID, new DateTime(time.Ticks).ToString(this.Format, System.Threading.Thread.CurrentThread.CurrentCulture));
        }

        /*  Hidden
            -----------------------------------------------------------------------------------------------*/

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string AllQuery
        {
            get { return base.AllQuery; }
            set { base.AllQuery = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override int QueryDelay
        {
            get { return base.QueryDelay; }
            set { base.QueryDelay = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string QueryParam
        {
            get { return base.QueryParam; }
            set { base.QueryParam = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string StoreID
        {
            get { return base.StoreID; }
            set { base.StoreID = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override ListItem SelectedItem
        {
            get { return base.SelectedItem; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig]
        [DefaultValue("")]
        public override string ValueField
        {
            get { return "text"; }
            set { base.ValueField = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override string DisplayField
        {
            get { return ""; }
            set { base.DisplayField = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override TriggerAction TriggerAction
        {
            get { return TriggerAction.Query; }
            set { base.TriggerAction = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ClientConfig(JsonMode.Ignore)]
        public override DataLoadMode Mode
        {
            get { return DataLoadMode.Remote; }
            set { base.Mode = value; }
        }
    }
}