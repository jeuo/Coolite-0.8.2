/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public abstract class Fx
    {
        private Config options;
        /// <summary>
        /// Fx config object
        /// </summary>
        public virtual Config Options
        {
            get
            {
                return this.options;
            }
            set
            {
                this.options = value;
            }
        }

        [ClientConfig]
        public abstract string FxName
        { 
            get;
        }

        public virtual string Serialize()
        {
            return string.Concat(this.FxName, "(", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}", ")");
        }

        public virtual string Arguments
        {
            get
            {
                if(this.Options == null)
                {
                    return "{}";
                }
                return new ClientConfig().Serialize(this.Options);
            }
        }

        [ClientConfig("args", JsonMode.Raw)]
        internal string ArgumentsArray
        {
            get
            {
                return string.Concat("[", this.Arguments, "]");
            }
        }

        public static string AnchorConvert(AnchorPoint anchor)
        {
            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    return "tl";
                case AnchorPoint.Top:
                    return "t";
                case AnchorPoint.TopRight:
                    return "tr";
                case AnchorPoint.Left:
                    return "l";
                case AnchorPoint.Center:
                    return "c";
                case AnchorPoint.Right:
                    return "r";
                case AnchorPoint.BottomLeft:
                    return "bl";
                case AnchorPoint.Bottom:
                    return "b";
                case AnchorPoint.BottomRight:
                    return "br";
                default:
                    throw new ArgumentOutOfRangeException("anchor");
            }
        }

        public class Config
        {
            private string afterCls="";
            /// <summary>
            /// A css class to apply after the effect
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [Description("A css class to apply after the effect")]
            public string AfterCls
            {
                get
                {
                    return this.afterCls;
                }
                set
                {
                    this.afterCls = value;
                }
            }

            private string afterStyle = "";
            /// <summary>
            /// A style specification string, e.g. "width:100px", that will be applied to the Element after the effect finishes
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [Description("A style specification string, e.g. 'width:100px', that will be applied to the Element after the effect finishes")]
            public string AfterStyle
            {
                get
                {
                    return this.afterStyle;
                }
                set
                {
                    this.afterStyle = value;
                }
            }

            private bool block = false;
            /// <summary>
            /// Whether the effect should block other effects from queueing while it runs
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("Whether the effect should block other effects from queueing while it runs")]
            public bool Block
            {
                get
                {
                    return this.block;
                }
                set
                {
                    this.block = value;
                }
            }

            private JFunction callback;
            /// <summary>
            /// A function called when the effect is finished. Note that effects are queued internally by the Fx class, so do not need to use the callback parameter to specify another effect -- effects can simply be chained together and called in sequence (e.g., el.slideIn().highlight();). The callback is intended for any additional code that should run once a particular effect has completed. The Element being operated upon is passed as the first parameter.
            /// </summary>
            [ClientConfig(JsonMode.Raw)]
            [DefaultValue(null)]
            [Description("A function called when the effect is finished. Note that effects are queued internally by the Fx class, so do not need to use the callback parameter to specify another effect -- effects can simply be chained together and called in sequence (e.g., el.slideIn().highlight();). The callback is intended for any additional code that should run once a particular effect has completed. The Element being operated upon is passed as the first parameter.")]
            public JFunction Callback
            {
                get
                {
                    return this.callback;
                }
                set
                {
                    this.callback = value;
                }
            }

            private bool concurrent = false;
            /// <summary>
            /// Whether to allow subsequently-queued effects to run at the same time as the current effect, or to ensure that they run in sequence
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("Whether to allow subsequently-queued effects to run at the same time as the current effect, or to ensure that they run in sequence")]
            public bool Concurrent
            {
                get
                {
                    return this.concurrent;
                }
                set
                {
                    this.concurrent = value;
                }
            }

            private float duration = 1;
            /// <summary>
            /// The length of time (in seconds) that the effect should last
            /// </summary>
            [ClientConfig]
            [DefaultValue(1)]
            [Description("The length of time (in seconds) that the effect should last")]
            public float Duration
            {
                get
                {
                    return this.duration;
                }
                set
                {
                    this.duration = value;
                }
            }

            private Easing easing = Easing.EaseOut;
            /// <summary>
            /// A valid Easing value for the effect
            /// </summary>
            [ClientConfig(JsonMode.ToCamelLower)]
            [DefaultValue(Easing.EaseOut)]
            [Description("A valid Easing value for the effect")]
            public Easing Easing
            {
                get
                {
                    return this.easing;
                }
                set
                {
                    this.easing = value;
                }
            }

            private bool remove = false;
            /// <summary>
            /// Whether the Element should be removed from the DOM and destroyed after the effect finishes
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            public bool Remove
            {
                get
                {
                    return this.remove;
                }
                set
                {
                    this.remove = value;
                }
            }

            private string scope = "this";
            /// <summary>
            /// The scope of the effect function
            /// </summary>
            [ClientConfig(JsonMode.Raw)]
            [DefaultValue("this")]
            [Description("The scope of the effect function")]
            public string Scope
            {
                get
                {
                    return this.scope;
                }
                set
                {
                    this.scope = value;
                }
            }

            private bool stopFx = false;
            /// <summary>
            /// Whether subsequent effects should be stopped and removed after the current effect finishes
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("Whether subsequent effects should be stopped and removed after the current effect finishes")]
            public bool StopFx
            {
                get
                {
                    return this.stopFx;
                }
                set
                {
                    this.stopFx = value;
                }
            }

            private bool useDisplay = false;
            /// <summary>
            /// Whether to use the display CSS property instead of visibility when hiding Elements (only applies to effects that end with the element being visually hidden, ignored otherwise)
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("Whether to use the display CSS property instead of visibility when hiding Elements (only applies to effects that end with the element being visually hidden, ignored otherwise)")]
            public bool UseDisplay
            {
                get
                {
                    return this.useDisplay;
                }
                set
                {
                    this.useDisplay = value;
                }
            }

        }
    }

    /// <summary>
    /// Fade an element in (from transparent to opaque). The ending opacity can be specified using the "endOpacity" config option. 
    /// </summary>
    public class FadeIn : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get 
            {
                return "fadeIn";
            }
        }

         public class FadeInConfig : Fx.Config
         {
             private float endOpacity = 1;
             /// <summary>
             /// The ending opacity
             /// </summary>
             [ClientConfig]
             [DefaultValue(1)]
             [Description("The ending opacity")]
             public float EndOpacity
             {
                 get
                 {
                     return this.endOpacity;
                 }
                 set
                 {
                     if(value<0 || value>1)
                     {
                         throw new ArgumentOutOfRangeException("value", "EndOpacity must be between 0 and 1");
                     }
                     this.endOpacity = value;
                 }
             }

         }
    }

    /// <summary>
    /// Fade an element out (from opaque to transparent). The ending opacity can be specified using the "endOpacity" config option. Note that IE may require useDisplay:true in order to redisplay correctly. 
    /// </summary>
    public class FadeOut : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "fadeOut";
            }
        }

        public class FadeOutConfig : Fx.Config
        {
            private float endOpacity = 0;
            /// <summary>
            /// The ending opacity
            /// </summary>
            [ClientConfig]
            [DefaultValue(0)]
            [Description("The ending opacity")]
            public float EndOpacity
            {
                get
                {
                    return this.endOpacity;
                }
                set
                {
                    if (value < 0 || value > 1)
                    {
                        throw new ArgumentOutOfRangeException("value", "EndOpacity must be between 0 and 1");
                    }
                    this.endOpacity = value;
                }
            }
        }
    }

    /// <summary>
    /// Shows a ripple of exploding, attenuating borders to draw attention to an Element
    /// </summary>
    public class Frame : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "frame";
            }
        }

        private string color = "C3DAF9";
        /// <summary>
        /// The color of the border. Should be a 6 char hex color without the leading # (defaults to light blue: 'C3DAF9').
        /// </summary>
        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        private int count = 1;
        /// <summary>
        /// The number of ripples to display (defaults to 1)
        /// </summary>
        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        public override string Serialize()
        {
            return string.Concat(this.FxName, "(",JSON.Serialize(this.Color),",",this.Count,",", this.Options != null ? new ClientConfig().Serialize(this.Options):"{}", ")");
        }

        public override string Arguments
        {
            get
            {
                return string.Concat(JSON.Serialize(this.Color), ",", this.Count, ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}");
            }
        }
    }

    /// <summary>
    /// Slides the element while fading it out of view. An anchor point can be optionally passed to set the ending point of the effect. 
    /// </summary>
    public class Ghost : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "ghost";
            }
        }

        private AnchorPoint anchor = AnchorPoint.Bottom;
        /// <summary>
        /// One of the valid Fx anchor positions (defaults to AnchorPoint.CenterBottom)
        /// </summary>
        public AnchorPoint Anchor
        {
            get
            {
                return this.anchor;
            }
            set
            {
                this.anchor = value;
            }
        }

        public override string Serialize()
        {
            return string.Concat(this.FxName, "(", JSON.Serialize(Fx.AnchorConvert(this.Anchor)), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}", ")");
        }

        public override string Arguments
        {
            get
            {
                return string.Concat(JSON.Serialize(Fx.AnchorConvert(this.Anchor)), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}");
            }
        }
    }

    /// <summary>
    /// Highlights the Element by setting a color (applies to the background-color by default, but can be changed using the "attr" config option) and then fading back to the original color. If no original color is available, you should provide the "endColor" config option which will be cleared after the animation. 
    /// </summary>
    public class Highlight : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "highlight";
            }
        }

        private string color = "ffff9c";
        /// <summary>
        /// The highlight color. Should be a 6 char hex color without the leading # (defaults to yellow: 'ffff9c')
        /// </summary>
        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public class HighlightConfig : Fx.Config
        {
            private string attr = "background-color";
            /// <summary>
            /// Can be any valid CSS property (attribute) that supports a color value
            /// </summary>
            [ClientConfig]
            [DefaultValue("background-color")]
            [Description("Can be any valid CSS property (attribute) that supports a color value")]
            public string Attr
            {
                get
                {
                    return this.attr;
                }
                set
                {
                    this.attr = value;
                }
            }

            private string endColor = "";
            /// <summary>
            /// End fading color
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [Description("End fading color")]
            public string EndColor
            {
                get
                {
                    return this.endColor;
                }
                set
                {
                    this.endColor = value;
                }
            }
        }

        public override string Serialize()
        {
            return string.Concat(this.FxName, "(", JSON.Serialize(this.Color), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}", ")");
        }

        public override string Arguments
        {
            get
            {
                return string.Concat(JSON.Serialize(this.Color), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}");
            }
        }
    }

    /// <summary>
    /// Fades the element out while slowly expanding it in all directions. When the effect is completed, the element will be hidden (visibility = 'hidden') but block elements will still take up space in the document. The element must be removed from the DOM using the 'remove' config option if desired. 
    /// </summary>
    public class Puff : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "puff";
            }
        }
    }

    public class Scale : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "scale";
            }
        }

        private int? width;
        /// <summary>
        /// The new width (pass undefined to keep the original width)
        /// </summary>
        public int? Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        private int? height;
        /// <summary>
        /// The new height (pass undefined to keep the original height)
        /// </summary>
        public int? Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public override string Serialize()
        {
            return string.Concat(this.FxName, "(", JSON.Serialize(this.Width), ",", JSON.Serialize(this.Height), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}", ")");
        }

        public override string Arguments
        {
            get
            {
                return string.Concat(JSON.Serialize(this.Width), ",", JSON.Serialize(this.Height), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}");
            }
        }
    }

    /// <summary>
    /// Animates the transition of any combination of an element's dimensions, xy position and/or opacity. Any of these properties not specified in the config object will not be changed. This effect requires that at least one new dimension, position or opacity setting must be passed in on the config object in order for the function to have any effect. 
    /// </summary>
    public class Shift : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "shift";
            }
        }

        public class ShiftConfig : Fx.Config
        {
            private int? width;
            /// <summary>
            /// Element's width
            /// </summary>
            [ClientConfig]
            [DefaultValue(null)]
            [Description("Element's width")]
            public int? Width
            {
                get
                {
                    return this.width;
                }
                set
                {
                    this.width = value;
                }
            }

            private int? height;
            /// <summary>
            /// Element's height
            /// </summary>
            [ClientConfig]
            [DefaultValue(null)]
            [Description("Element's height")]
            public int? Height
            {
                get
                {
                    return this.height;
                }
                set
                {
                    this.height = value;
                }
            }

            private int? x;
            /// <summary>
            /// Element's x position
            /// </summary>
            [ClientConfig]
            [DefaultValue(null)]
            [Description("Element's x position")]
            public int? X
            {
                get
                {
                    return this.x;
                }
                set
                {
                    this.x = value;
                }
            }

            private int? y;
            /// <summary>
            /// Element's y position
            /// </summary>
            [ClientConfig]
            [DefaultValue(null)]
            [Description("Element's y position")]
            public int? Y
            {
                get
                {
                    return this.y;
                }
                set
                {
                    this.y = value;
                }
            }

            private float? opacity;
            /// <summary>
            /// Element's opacity
            /// </summary>
            [ClientConfig]
            [DefaultValue(null)]
            [Description("Element's opacity")]
            public float? Opacity
            {
                get
                {
                    return this.opacity;
                }
                set
                {
                    this.opacity = value;
                }
            }
        }
    }

    /// <summary>
    /// Slides the element into view. An anchor point can be optionally passed to set the point of origin for the slide effect. This function automatically handles wrapping the element with a fixed-size container if needed. 
    /// </summary>
    public class SlideIn : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "slideIn";
            }
        }

        private AnchorPoint anchor = AnchorPoint.Top;
        /// <summary>
        /// One of the valid Fx anchor positions (defaults to AnchorPoint.CenterTop)
        /// </summary>
        public AnchorPoint Anchor
        {
            get
            {
                return this.anchor;
            }
            set
            {
                this.anchor = value;
            }
        }

        public override string Serialize()
        {
            return string.Concat(this.FxName, "(", JSON.Serialize(Fx.AnchorConvert(this.Anchor)), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}", ")");
        }

        public override string Arguments
        {
            get
            {
                return string.Concat(JSON.Serialize(Fx.AnchorConvert(this.Anchor)), ",", this.Options != null ? new ClientConfig().Serialize(this.Options) : "{}");
            }
        }
    }

    /// <summary>
    /// Slides the element out of view. An anchor point can be optionally passed to set the end point for the slide effect. When the effect is completed, the element will be hidden (visibility = 'hidden') but block elements will still take up space in the document. The element must be removed from the DOM using the 'remove' config option if desired. This function automatically handles wrapping the element with a fixed-size container if needed.
    /// </summary>
    public class SlideOut : SlideIn
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "slideOut";
            }
        }
    }

    /// <summary>
    /// Blinks the element as if it was clicked and then collapses on its center (similar to switching off a television). When the effect is completed, the element will be hidden (visibility = 'hidden') but block elements will still take up space in the document. The element must be removed from the DOM using the 'remove' config option if desired.
    /// </summary>
    public class SwitchOff : Fx
    {
        [ClientConfig]
        public override string FxName
        {
            get
            {
                return "switchOff";
            }
        }
    }
}