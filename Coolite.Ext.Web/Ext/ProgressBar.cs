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
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// An updateable progress bar component. The progress bar supports two different modes: manual and automatic.
    /// </summary>
    [Xtype("progress")]
    [InstanceOf(ClassName = "Ext.ProgressBar")]
    [ToolboxData("<{0}:ProgressBar runat=\"server\" Width=\"300\"></{0}:ProgressBar>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ProgressBar), "Build.Resources.ToolboxIcons.ProgressBar.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("An updateable progress bar component. The progress bar supports two different modes: manual and automatic.")]
    public partial class ProgressBar : BoxComponent
    {
        /// <summary>
        /// The base CSS class to apply to the progress bar's wrapper element (defaults to 'x-progress')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-progress")]
        [Description("The base CSS class to apply to the progress bar's wrapper element (defaults to 'x-progress')")]
        [NotifyParentProperty(true)]
        public virtual string BaseCls
        {
            get
            {
                return (string)this.ViewState["BaseCls"] ?? "x-progress";
            }
            set
            {
                this.ViewState["BaseCls"] = value;
            }
        }
        
        /// <summary>
        /// The progress bar text (defaults to '')
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The progress bar text (defaults to '')")]
        [NotifyParentProperty(true)]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// The element to render the progress text to (defaults to the progress bar's internal text element)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The element to render the progress text to (defaults to the progress bar's internal text element)")]
        [NotifyParentProperty(true)]
        public virtual string TextEl
        {
            get
            {
                return (string)this.ViewState["TextEl"] ?? "";
            }
            set
            {
                this.ViewState["TextEl"] = value;
            }
        }

        /// <summary>
        /// A floating point value between 0 and 1 (e.g., .5, defaults to 0)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("A floating point value between 0 and 1 (e.g., .5, defaults to 0)")]
        [NotifyParentProperty(true)]
        public virtual float Value
        {
            get
            {
                var obj = this.ViewState["Value"];
                return (obj == null) ? 0 : (float)this.ViewState["Value"];
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }


        /*  Listeners and AjaxEvents
            -----------------------------------------------------------------------------------------------*/

        private ProgressBarListeners listeners;

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
        public ProgressBarListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ProgressBarListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private ProgressBarAjaxEvents ajaxEvents;

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
        public ProgressBarAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ProgressBarAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Resets the progress bar value to 0 and text to empty string. If hide = true, the progress bar will also be hidden (using the hideMode property internally).
        /// </summary>
        [Description("Resets the progress bar value to 0 and text to empty string. If hide = true, the progress bar will also be hidden (using the hideMode property internally).")]
        public virtual void Reset()
        {
            string template = "{0}.reset();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Resets the progress bar value to 0 and text to empty string. If hide = true, the progress bar will also be hidden (using the hideMode property internally).
        /// </summary>
        /// <param name="hide">True to hide the progress bar</param>
        [Description("Resets the progress bar value to 0 and text to empty string. If hide = true, the progress bar will also be hidden (using the hideMode property internally).")]
        public virtual void Reset(bool hide)
        {
            string template = "{0}.reset({1});";
            this.AddScript(template, this.ClientID, hide.ToString().ToLower());
        }

        /// <summary>
        /// Synchronizes the inner bar width to the proper proportion of the total componet width based on the current progress value. This will be called automatically when the ProgressBar is resized by a layout, but if it is rendered auto width, this method can be called from another resize handler to sync the ProgressBar if necessary.
        /// </summary>
        [Description("Synchronizes the inner bar width to the proper proportion of the total componet width based on the current progress value. This will be called automatically when the ProgressBar is resized by a layout, but if it is rendered auto width, this method can be called from another resize handler to sync the ProgressBar if necessary.")]
        public virtual void SyncProgressBar()
        {
            string template = "{0}.syncProgressBar();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Updates the progress bar value, and optionally its text. If the text argument is not specified, any existing text value will be unchanged. To blank out existing text, pass ''. Note that even if the progress bar value exceeds 1, it will never automatically reset -- you are responsible for determining when the progress is complete and calling reset to clear and/or hide the control.
        /// </summary>
        [Description("Updates the progress bar value, and optionally its text. If the text argument is not specified, any existing text value will be unchanged. To blank out existing text, pass ''. Note that even if the progress bar value exceeds 1, it will never automatically reset -- you are responsible for determining when the progress is complete and calling reset to clear and/or hide the control.")]
        public virtual void UpdateProgress(float value)
        {
            string template = "{0}.updateProgress({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }

        /// <summary>
        /// Updates the progress bar value, and optionally its text. If the text argument is not specified, any existing text value will be unchanged. To blank out existing text, pass ''. Note that even if the progress bar value exceeds 1, it will never automatically reset -- you are responsible for determining when the progress is complete and calling reset to clear and/or hide the control.
        /// </summary>
        [Description("Updates the progress bar value, and optionally its text. If the text argument is not specified, any existing text value will be unchanged. To blank out existing text, pass ''. Note that even if the progress bar value exceeds 1, it will never automatically reset -- you are responsible for determining when the progress is complete and calling reset to clear and/or hide the control.")]
        public virtual void UpdateProgress(float value, string text)
        {
            string template = "{0}.updateProgress({1},\"{2}\");";
            this.AddScript(template, this.ClientID, JSON.Serialize(value), text);
        }

        /// <summary>
        /// Updates the progress bar text. If specified, textEl will be updated, otherwise the progress bar itself will display the updated text.
        /// </summary>
        /// <param name="text">The string to display in the progress text element</param>
        [Description("Updates the progress bar text. If specified, textEl will be updated, otherwise the progress bar itself will display the updated text.")]
        public virtual void UpdateText()
        {
            string template = "{0}.updateText();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Updates the progress bar text. If specified, textEl will be updated, otherwise the progress bar itself will display the updated text.
        /// </summary>
        /// <param name="text">The string to display in the progress text element</param>
        [Description("Updates the progress bar text. If specified, textEl will be updated, otherwise the progress bar itself will display the updated text.")]
        public virtual void UpdateText(string text)
        {
            string template = "{0}.updateText(\"{1}\");";
            this.AddScript(template, this.ClientID, text);
        }

        /// <summary>
        /// Initiates an auto-updating progress bar. A duration can be specified, in which case the progress bar will automatically reset after a fixed amount of time and optionally call a callback function if specified. If no duration is passed in, then the progress bar will run indefinitely and must be manually cleared by calling reset.
        /// </summary>
        [Description("Initiates an auto-updating progress bar. A duration can be specified, in which case the progress bar will automatically reset after a fixed amount of time and optionally call a callback function if specified. If no duration is passed in, then the progress bar will run indefinitely and must be manually cleared by calling reset.")]
        public virtual void Wait()
        {
            string template = "{0}.wait();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Initiates an auto-updating progress bar. A duration can be specified, in which case the progress bar will automatically reset after a fixed amount of time and optionally call a callback function if specified. If no duration is passed in, then the progress bar will run indefinitely and must be manually cleared by calling reset.
        /// </summary>
        /// <param name="config">Configuration options</param>
        [Description("Updates the progress bar text. If specified, textEl will be updated, otherwise the progress bar itself will display the updated text.")]
        public virtual void Wait(WaitConfig config)
        {
            string template = "{0}.wait({1});";
            this.AddScript(template, this.ClientID, config.ToJsonString());
        }
    }
}