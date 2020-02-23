/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A standard tooltip implementation for providing additional information when hovering over a target element.
    /// </summary>
    [InstanceOf(ClassName = "Ext.ToolTip")]
    //[ToolboxItem(false)]
    [ToolboxData("<{0}:ToolTip runat=\"server\" Title=\"Message\"></{0}:ToolTip>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ToolTip), "Build.Resources.ToolboxIcons.ToolTip.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("A standard tooltip implementation for providing additional information when hovering over a target element.")]
    public class ToolTip : Tip
    {
        /// <summary>
        /// True to automatically hide the tooltip after the mouse exits the target element or after the dismissDelay has expired if set (defaults to true). If closable = true a close tool button will be rendered into the tooltip header.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True to automatically hide the tooltip after the mouse exits the target element or after the dismissDelay has expired if set (defaults to true). If closable = true a close tool button will be rendered into the tooltip header.")]
        public virtual bool AutoHide
        {
            get
            {
                object obj = this.ViewState["AutoHide"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoHide"] = value;
            }
        }

        /// <summary>
        /// Delay in milliseconds before the tooltip automatically hides (defaults to 5000). To disable automatic hiding, set dismissDelay = 0.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(5000)]
        [NotifyParentProperty(true)]
        [Description("Delay in milliseconds before the tooltip automatically hides (defaults to 5000). To disable automatic hiding, set dismissDelay = 0.")]
        public virtual int DismissDelay
        {
            get
            {
                object obj = this.ViewState["DismissDelay"];
                return (obj == null) ? 5000 : (int)obj;
            }
            set
            {
                this.ViewState["DismissDelay"] = value;
            }
        }

        /// <summary>
        /// Delay in milliseconds after the mouse exits the target element but before the tooltip actually hides (defaults to 200). Set to 0 for the tooltip to hide immediately.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(200)]
        [NotifyParentProperty(true)]
        [Description("Delay in milliseconds after the mouse exits the target element but before the tooltip actually hides (defaults to 200). Set to 0 for the tooltip to hide immediately.")]
        public virtual int HideDelay
        {
            get
            {
                object obj = this.ViewState["HideDelay"];
                return (obj == null) ? 200 : (int)obj;
            }
            set
            {
                this.ViewState["HideDelay"] = value;
            }
        }

        /// <summary>
        /// An XY offset from the mouse position where the tooltip should be shown (defaults to [15,18]).
        /// </summary>
        [ClientConfig(typeof(IntArrayJsonConverter))]
        [TypeConverter(typeof(IntArrayConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("An XY offset from the mouse position where the tooltip should be shown (defaults to [15,18]).")]
        public virtual int[] MouseOffset
        {
            get
            {
                object obj = this.ViewState["MouseOffset"];
                return (obj == null) ? null : (int[])obj;
            }
            set
            {
                this.ViewState["MouseOffset"] = value;
            }
        }

        /// <summary>
        /// Delay in milliseconds before the tooltip displays after the mouse enters the target element (defaults to 500).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(500)]
        [NotifyParentProperty(true)]
        [Description("Delay in milliseconds before the tooltip displays after the mouse enters the target element (defaults to 500).")]
        public virtual int ShowDelay
        {
            get
            {
                object obj = this.ViewState["ShowDelay"];
                return (obj == null) ? 500 : (int)obj;
            }
            set
            {
                this.ViewState["ShowDelay"] = value;
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

        /// <summary>
        /// True to have the tooltip follow the mouse as it moves over the target element (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to have the tooltip follow the mouse as it moves over the target element (defaults to false).")]
        public virtual bool TrackMouse
        {
            get
            {
                object obj = this.ViewState["TrackMouse"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["TrackMouse"] = value;
            }
        }

        private PanelListeners listeners;

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
        public PanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new PanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private PanelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public PanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new PanelAjaxEvents();
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
        /// Update the html of the Body, optionally searching for and processing scripts.
        /// </summary>
        [Description("Update the html of the Body, optionally searching for and processing scripts.")]
        public override void Update(string html)
        {
            string template = "{0}.html={1};if({0}.body){{{0}.body.update({1});}}";
            this.AddScript(template, this.ClientID, JSON.Serialize(html));
        }
    }
}