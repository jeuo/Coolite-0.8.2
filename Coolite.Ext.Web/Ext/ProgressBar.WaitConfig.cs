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
    public partial class ProgressBar : BoxComponent
    {
        [ToolboxItem(false)]
        [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
        public class WaitConfig
        {
            public WaitConfig() { }

            public virtual string ToJsonString()
            {
                return new ClientConfig().Serialize(this);
            }

            int duration = -1;

            /// <summary>
            /// The length of time in milliseconds that the progress bar should run before resetting itself (defaults to undefined, in which case it will run indefinitely until reset is called)
            /// </summary>
            [ClientConfig]
            [DefaultValue(-1)]
            [NotifyParentProperty(true)]
            [Description("The length of time in milliseconds that the progress bar should run before resetting itself (defaults to undefined, in which case it will run indefinitely until reset is called)")]
            public virtual int Duration
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

            int interval = 1000;

            /// <summary>
            /// The length of time in milliseconds between each progress update (defaults to 1000 ms)
            /// </summary>
            [ClientConfig]
            [DefaultValue(1000)]
            [NotifyParentProperty(true)]
            [Description("The length of time in milliseconds between each progress update (defaults to 1000 ms)")]
            public virtual int Interval
            {
                get
                {
                    return this.interval;
                }
                set
                {
                    this.interval = value;
                }
            }

            int increment = 10;

            /// <summary>
            /// The number of progress update segments to display within the progress bar (defaults to 10).  If the bar reaches the end and is still updating, it will automatically wrap back to the beginning.
            /// </summary>
            [ClientConfig]
            [DefaultValue(10)]
            [NotifyParentProperty(true)]
            [Description("The number of progress update segments to display within the progress bar (defaults to 10).  If the bar reaches the end and is still updating, it will automatically wrap back to the beginning.")]
            public virtual int Increment
            {
                get
                {
                    return this.increment;
                }
                set
                {
                    this.increment = value;
                }
            }

            string text = "";

            /// <summary>
            /// Optional text to display in the progress bar element (defaults to '').
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("Optional text to display in the progress bar element (defaults to '').")]
            public virtual string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                }
            }

            string fn = "";

            /// <summary>
            /// A callback function to execute after the progress bar finishes auto-updating. The function will be called with no arguments. This function will be ignored if duration is not specified since in that case the progress bar can only be stopped programmatically, so any required function should be called by the same code after it resets the progress bar.
            /// </summary>
            [ClientConfig(JsonMode.Raw)]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("A callback function to execute after the progress bar finishes auto-updating. The function will be called with no arguments. This function will be ignored if duration is not specified since in that case the progress bar can only be stopped programmatically, so any required function should be called by the same code after it resets the progress bar.")]
            public virtual string Fn
            {
                get
                {
                    return this.fn;
                }
                set
                {
                    this.fn = value;
                }
            }

            string scope = "";

            /// <summary>
            /// The scope that is passed to the callback function (only applies when duration and fn are both passed).
            /// </summary>
            [ClientConfig(JsonMode.Raw)]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("The scope that is passed to the callback function (only applies when duration and fn are both passed).")]
            public virtual string Scope
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
        }        
    }
}