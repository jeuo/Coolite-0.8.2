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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.Resizable")]
    [ToolboxData("<{0}:Resizable runat=\"server\" />")]
    [Description("Applies drag handles to an element to make it resizable.")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Resizable), "Build.Resources.ToolboxIcons.Resizable.bmp")]
    [Designer(typeof(EmptyDesigner))]
    public class Resizable : Observable
    {
        internal override string GetClientConstructor(bool instanceOnly, string body)
        {
            string template = (instanceOnly) ? "new {1}({2},{3})" : "this.{0}=new {1}({2},{3});";

            return string.Format(template, this.ClientID, "Ext.Resizable", this.ElementProxy, body ?? this.InitialConfig);
        }

        private string ElementProxy
        {
            get
            {
                string parsedElement = TokenUtils.ParseTokens(this.Element, this);

                if (TokenUtils.IsRawToken(parsedElement))
                {
                    return TokenUtils.ReplaceRawToken(parsedElement);
                }

                return string.Concat("\"", parsedElement, "\"");
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id or element to resize")]
        public virtual string Element
        {
            get
            {
                return (string)this.ViewState["Element"] ?? "";
            }
            set
            {
                this.ViewState["Element"] = value;
            }
        }


        [Category("Config Options")]
        [DefaultValue(typeof(Size), "")]
        [Description("The array [width, height] with values to be added to the resize operation's new size (defaults to [0, 0])")]
        [NotifyParentProperty(true)]
        public Size Adjustments
        {
            get
            {
                object obj = this.ViewState["Adjustments"];
                return obj != null ? (Size)obj : Size.Empty;
            }
            set
            {
                this.ViewState["Adjustments"] = value;
            }
        }

        [ClientConfig("adjustments", JsonMode.Raw)]
        [DefaultValue("")]
        internal string AdjustmentsProxy
        {
            get
            {
                if(this.Adjustments.IsEmpty)
                {
                    return "";
                }

                return string.Concat("[", this.Adjustments.Width, ",", this.Adjustments.Height, "]");
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to animate the resize (not compatible with dynamic sizing, defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool Animate
        {
            get
            {
                object obj = this.ViewState["Animate"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to disable mouse tracking. This is only applied at config time. (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool DisableTrackOver
        {
            get
            {
                object obj = this.ViewState["DisableTrackOver"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DisableTrackOver"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Convenience to initialize drag drop (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Draggable
        {
            get
            {
                object obj = this.ViewState["Draggable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Draggable"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0.35)]
        [Description("Animation duration if animate = true (defaults to .35)")]
        public virtual double Duration
        {
            get
            {
                object obj = this.ViewState["Duration"];
                return (obj == null) ? 0.35 : (double)obj;
            }
            set
            {
                this.ViewState["Duration"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to resize the element while dragging instead of using a proxy (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Dynamic
        {
            get
            {
                object obj = this.ViewState["Dynamic"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Dynamic"] = value;
            }
        }

        [ClientConfig(JsonMode.ToCamelLower)]
        [Category("Config Options")]
        [DefaultValue(Easing.EaseOutStrong)]
        [Description("Animation easing if animate = true (defaults to 'easeOutStrong')")]
        public virtual Easing Easing
        {
            get
            {
                object obj = this.ViewState["Easing"];
                return (obj == null) ? Easing.EaseOutStrong : (Easing)obj;
            }
            set
            {
                this.ViewState["Easing"] = value;
            }
        }

        [ClientConfig("enabled")]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to disable resizing (defaults to true)")]
        [NotifyParentProperty(true)]
        public virtual bool EnabledResizing
        {
            get
            {
                object obj = this.ViewState["EnabledResizing"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnabledResizing"] = value;
            }
        }

        
        [Category("Config Options")]
        [DefaultValue(ResizeHandle.None)]
        [Description("String consisting of the resize handles to display (defaults to undefined)")]
        public virtual ResizeHandle Handles
        {
            get
            {
                object obj = this.ViewState["Handles"];
                return (obj == null) ? ResizeHandle.None : (ResizeHandle)obj;
            }
            set
            {
                this.ViewState["Handles"] = value;
            }
        }

        [ClientConfig("handles")]
        [DefaultValue("")]
        internal string HandlesProxy
        {
            get
            {
                switch(this.Handles)
                {
                    case ResizeHandle.None:
                        return "";
                    case ResizeHandle.North:
                        return "n";
                    case ResizeHandle.South:
                        return "s";
                    case ResizeHandle.East:
                        return "e";
                    case ResizeHandle.West:
                        return "w";
                    case ResizeHandle.NorthWest:
                        return "nw";
                    case ResizeHandle.SouthWest:
                        return "sw";
                    case ResizeHandle.SouthEast:
                        return "se";
                    case ResizeHandle.NorthEast:
                        return "ne";
                    case ResizeHandle.All:
                        return "all";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The width of this component in pixels (defaults to auto).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The width of the element in pixels (defaults to null)")]
        [NotifyParentProperty(true)]
        new public virtual Unit Width
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Width"], Unit.Empty, "Width");
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        /// <summary>
        /// The height of this component in pixels (defaults to auto).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The height of the element in pixels (defaults to null)")]
        [NotifyParentProperty(true)]
        new public virtual Unit Height
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Height"], Unit.Empty, "Height");
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The increment to snap the height resize in pixels (dynamic must be true, defaults to 0).")]
        public virtual int HeightIncrement
        {
            get
            {
                object obj = this.ViewState["HeightIncrement"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["HeightIncrement"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(10000)]
        [Description("The maximum height for the element (defaults to 10000)")]
        public virtual int MaxHeight
        {
            get
            {
                object obj = this.ViewState["MaxHeight"];
                return (obj == null) ? 10000 : (int)obj;
            }
            set
            {
                this.ViewState["MaxHeight"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(10000)]
        [Description("The maximum width for the element (defaults to 10000)")]
        public virtual int MaxWidth
        {
            get
            {
                object obj = this.ViewState["MaxWidth"];
                return (obj == null) ? 10000 : (int)obj;
            }
            set
            {
                this.ViewState["MaxWidth"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(5)]
        [Description("The minimum height for the element (defaults to 5)")]
        public virtual int MinHeight
        {
            get
            {
                object obj = this.ViewState["MinHeight"];
                return (obj == null) ? 5 : (int)obj;
            }
            set
            {
                this.ViewState["MinHeight"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(5)]
        [Description("The minimum width for the element (defaults to 5)")]
        public virtual int MinWidth
        {
            get
            {
                object obj = this.ViewState["MinWidth"];
                return (obj == null) ? 5 : (int)obj;
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The minimum allowed page X for the element (only used for west resizing, defaults to 0)")]
        public virtual int MinX
        {
            get
            {
                object obj = this.ViewState["MinX"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["MinX"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The minimum allowed page Y for the element (only used for north resizing, defaults to 0)")]
        public virtual int MinY
        {
            get
            {
                object obj = this.ViewState["MinY"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["MinY"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to ensure that the resize handles are always visible, false to display them only when the user mouses over the resizable borders. This is only applied at config time. (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Pinned
        {
            get
            {
                object obj = this.ViewState["Pinned"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Pinned"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to preserve the original ratio between height and width during resize (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool PreserveRatio
        {
            get
            {
                object obj = this.ViewState["PreserveRatio"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["PreserveRatio"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("id of element to resize")]
        public virtual string ResizeChild
        {
            get
            {
                return (string)this.ViewState["ResizeChild"] ?? "";
            }
            set
            {
                this.ViewState["ResizeChild"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True for transparent handles. This is only applied at config time. (defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Transparent
        {
            get
            {
                object obj = this.ViewState["Transparent"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Transparent"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The increment to snap the width resize in pixels (dynamic must be true, defaults to 0)")]
        public virtual int WidthIncrement
        {
            get
            {
                object obj = this.ViewState["WidthIncrement"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["WidthIncrement"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to wrap an element with a div if needed (required for textareas and images, defaults to false)")]
        [NotifyParentProperty(true)]
        public virtual bool Wrap
        {
            get
            {
                object obj = this.ViewState["Wrap"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Wrap"] = value;
            }
        }

        private ResizableListeners listeners;

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
        public ResizableListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new ResizableListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private ResizableAjaxEvents ajaxEvents;

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
        public ResizableAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new ResizableAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
    }
}