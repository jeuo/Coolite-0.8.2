/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Provides a convenient wrapper for TextFields that adds a clickable trigger button (looks like a combobox by default). The trigger has no default action, so you must assign a function to implement the trigger click handler by overriding onTriggerClick. You can create a TriggerField directly, as it renders exactly like a combobox for which you can provide a custom implementation.
    /// </summary>
    [Xtype("trigger")]
    [Description("Provides a convenient wrapper for TextFields that adds a clickable trigger button (looks like a combobox by default). The trigger has no default action, so you must assign a function to implement the trigger click handler by overriding onTriggerClick. You can create a TriggerField directly, as it renders exactly like a combobox for which you can provide a custom implementation.")]
    public abstract class TriggerFieldBase : TextFieldBase
    {
        /// <summary>
        /// True to hide the trigger element and display only the base text field (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the trigger element and display only the base text field (defaults to false).")]
        public virtual bool HideTrigger
        {
            get
            {
                object obj = this.ViewState["HideTrigger"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideTrigger"] = value;
            }
        }

        /// <summary>
        /// A CSS class to apply to the trigger.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class to apply to the trigger.")]
        public virtual string TriggerClass
        {
            get
            {
                return (string)this.ViewState["TriggerClass"] ?? "";
            }
            set
            {
                this.ViewState["TriggerClass"] = value;
            }
        }
    }
}