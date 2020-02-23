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
    public abstract class SplitButtonBase : ButtonBase
    {
        /// <summary>
        /// A function called when the arrow button is clicked (can be used instead of click event).
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A function called when the arrow button is clicked (can be used instead of click event).")]
        public virtual string ArrowHandler
        {
            get
            {
                return (string)this.ViewState["ArrowHandler"] ?? "";
            }
            set
            {
                this.ViewState["ArrowHandler"] = value;
            }
        }

        /// <summary>
        /// The title attribute of the arrow.
        /// </summary>
        [ClientConfig]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The title attribute of the arrow.")]
        public virtual string ArrowTooltip
        {
            get
            {
                return (string)this.ViewState["ArrowTooltip"] ?? "";
            }
            set
            {
                this.ViewState["ArrowTooltip"] = value;
            }
        }
    }
}