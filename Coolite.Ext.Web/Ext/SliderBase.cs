/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    [Xtype("slider")]
    [InstanceOf(ClassName = "Ext.Slider")]
    public abstract class SliderBase : BoxComponent
    {
        /// <summary>
        /// Turn on or off animation. Defaults to true
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Turn on or off animation. Defaults to true")]
        public virtual bool Animate
        {
            get
            {
                object obj = this.ViewState["Animate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }

        /// <summary>
        /// Determines whether or not clicking on the Slider axis will change the slider. Defaults to true
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Determines whether or not clicking on the Slider axis will change the slider. Defaults to true")]
        public virtual bool ClickToChange
        {
            get
            {
                object obj = this.ViewState["ClickToChange"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ClickToChange"] = value;
            }
        }

        /// <summary>
        /// How many units to change the slider when adjusting by drag and drop. Use this option to enable 'snapping'.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("How many units to change the slider when adjusting by drag and drop. Use this option to enable 'snapping'.")]
        [NotifyParentProperty(true)]
        public virtual int Increment
        {
            get
            {
                object obj = this.ViewState["Increment"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Increment"] = value;
            }
        }

        /// <summary>
        /// How many units to change the Slider when adjusting with keyboard navigation. Defaults to 1. If the increment config is larger, it will be used instead.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(1)]
        [Description("How many units to change the Slider when adjusting with keyboard navigation. Defaults to 1. If the increment config is larger, it will be used instead.")]
        [NotifyParentProperty(true)]
        public virtual int KeyIncrement
        {
            get
            {
                object obj = this.ViewState["KeyIncrement"];
                return (obj == null) ? 1 : (int)obj;
            }
            set
            {
                this.ViewState["KeyIncrement"] = value;
            }
        }

        /// <summary>
        /// The maximum value for the Slider. Defaults to 100.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(100)]
        [Description("The maximum value for the Slider. Defaults to 100.")]
        [NotifyParentProperty(true)]
        public virtual int MaxValue
        {
            get
            {
                object obj = this.ViewState["MaxValue"];
                return (obj == null) ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["MaxValue"] = value;
            }
        }

        /// <summary>
        /// The minimum value for the Slider. Defaults to 0.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The minimum value for the Slider. Defaults to 0.")]
        [NotifyParentProperty(true)]
        public virtual int MinValue
        {
            get
            {
                object obj = this.ViewState["MinValue"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["MinValue"] = value;
            }
        }

        /// <summary>
        /// The value to initialize the slider with. Defaults to minValue.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The value to initialize the slider with. Defaults to minValue.")]
        [NotifyParentProperty(true)]
        public virtual int Value
        {
            get
            {
                object obj = this.ViewState["Value"];
                return (obj == null) ? 0 : (int)obj;
            }
            set 
            {
                this.ViewState["Value"] = value;
            }
        }

        /// <summary>
        /// Orient the Slider vertically rather than horizontally, defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Orient the Slider vertically rather than horizontally, defaults to false.")]
        [NotifyParentProperty(true)]
        public virtual bool Vertical
        {
            get
            {
                object obj = this.ViewState["Vertical"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Vertical"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Programmatically sets the value of the Slider. Ensures that the value is constrained within the minValue and maxValue.
        /// </summary>
        /// <param name="number">The value to set the slider to. (This will be constrained within minValue and maxValue)</param>
        [Description("Programmatically sets the value of the Slider. Ensures that the value is constrained within the minValue and maxValue.")]
        public virtual void SetValue(int number)
        {
            this.SetValue(number, true);
        }

        /// <summary>
        /// Programmatically sets the value of the Slider. Ensures that the value is constrained within the minValue and maxValue.
        /// </summary>
        /// <param name="number">The value to set the slider to. (This will be constrained within minValue and maxValue)</param>
        /// <param name="animate">Turn on or off animation, defaults to true</param>
        [Description("Programmatically sets the value of the Slider. Ensures that the value is constrained within the minValue and maxValue.")]
        public virtual void SetValue(int number, bool animate)
        {
            this.AddScript("{0}.setValue({1},{2});", this.ClientID, number, animate.ToString().ToLower());
        }

        /// <summary>
        /// Synchronizes the thumb position to the proper proportion of the total component width based on the current slider value. This will be called automatically when the Slider is resized by a layout, but if it is rendered auto width, this method can be called from another resize handler to sync the Slider if necessary.
        /// </summary>
        [Description("Synchronizes the thumb position to the proper proportion of the total component width based on the current slider value. This will be called automatically when the Slider is resized by a layout, but if it is rendered auto width, this method can be called from another resize handler to sync the Slider if necessary.")]
        public virtual void SyncThumb()
        {
            this.AddScript("{0}.syncThumb();", this.ClientID);
        }
    }
}