/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class CheckboxBase : Field
    {
        /// <summary>
        /// The text that appears beside the checkbox (defaults to '').
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetBoxLabel")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The text that appears beside the checkbox (defaults to '').")]
        public virtual string BoxLabel
        {
            get
            {
                return (string)this.ViewState["BoxLabel"] ?? "";
            }
            set
            {
                this.ViewState["BoxLabel"] = value;
            }
        }

        /// <summary>
        /// True if the the checkbox should render already checked (defaults to false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetValue")]
        [Category("Config Options")]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [DefaultValue(false)]
        [Description("True if the the checkbox should render already checked (defaults to false).")]
        public virtual bool Checked
        {
            get
            {
                object obj = this.ViewState["Checked"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Checked"] = value;
            }
        }

        /// <summary>
        /// The CSS class to use when the control is checked (defaults to 'x-form-check-checked'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-form-check-checked")]
        [Description("The CSS class to use when the control is checked (defaults to 'x-form-check-checked'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.")]
        public virtual string CheckedCls
        {
            get
            {
                return (string)this.ViewState["CheckedCls"] ?? "x-form-check-checked";
            }
            set
            {
                this.ViewState["CheckedCls"] = value;
            }
        }

        /// <summary>
        /// The CSS class to use when the control receives input focus (defaults to 'x-form-check-focus'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-form-check-focus")]
        [Description("The CSS class to use when the control receives input focus (defaults to 'x-form-check-focus'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.")]
        public virtual string FocusCls
        {
            get
            {
                return (string)this.ViewState["FocusCls"] ?? "x-form-check-focus";
            }
            set
            {
                this.ViewState["FocusCls"] = value;
            }
        }

        /// <summary>
        /// The value that should go into the generated input element's value attribute (defaults to undefined, with no value attribute)
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [Description("The value that should go into the generated input element's value attribute (defaults to undefined, with no value attribute)")]
        internal virtual string InputValue
        {
            get
            {
                return this.ClientID;
            }
        }

        /// <summary>
        /// The CSS class to use when the control is being actively clicked (defaults to 'x-form-check-down'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-form-check-down")]
        [Description("The CSS class to use when the control is being actively clicked (defaults to 'x-form-check-down'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.")]
        public virtual string MouseDownCls
        {
            get
            {
                return (string)this.ViewState["MouseDownCls"] ?? "x-form-check-down";
            }
            set
            {
                this.ViewState["MouseDownCls"] = value;
            }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to this component's Element when the mouse moves over the Element, and removed when the mouse moves out. (defaults to ''). This can be useful for adding customized 'active' or 'hover' styles to the component or any of its children using standard CSS rules.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-form-check-over")]
        [Description("The CSS class to use when the control is hovered over (defaults to 'x-form-check-over'). Note that this class applies to both checkboxes and radio buttons and is added to the control's wrapper element.")]
        public override string OverCls
        {
            get
            {
                return (string)this.ViewState["OverCls"] ?? "x-form-check-over";
            }
            set
            {
                this.ViewState["OverCls"] = value;
            }
        }

        protected virtual void SetBoxLabel(string value)
        {
            string template = "{0}.setBoxLabel({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(value));
        }
    }
}