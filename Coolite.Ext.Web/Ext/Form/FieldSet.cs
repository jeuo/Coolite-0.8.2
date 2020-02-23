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
    /// <summary>
    /// Standard contentContainer used for grouping form fields.
    /// </summary>
    [Xtype("fieldset")]
    [InstanceOf(ClassName = "Ext.form.FieldSet")]
    [ToolboxData("<{0}:FieldSet runat=\"server\"><Body></Body></{0}:FieldSet>")]
    [DefaultEvent("Width")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.FieldSet), "Build.Resources.ToolboxIcons.FieldSet.bmp")]
    [Designer(typeof(FieldSetDesigner))]
    [Description("Standard contentContainer used for grouping form fields.")]
    public class FieldSet : Panel
    {
        protected override bool IsCollapsible
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.")]
        public override bool AutoHeight
        {
            get
            {
                object obj = this.ViewState["AutoHeight"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AutoHeight"] = value;
            }
        }

        /// <summary>
        /// True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.")]
        public virtual string CheckboxName
        {
            get
            {
                return (string)this.ViewState["CheckboxName"] ?? "";
            }
            set
            {
                this.ViewState["CheckboxName"] = value;
            }
        }


        /// <summary>
        /// True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render a checkbox into the fieldset frame just in front of the legend (defaults to false). The fieldset will be expanded or collapsed when the checkbox is toggled.")]
        public virtual bool CheckboxToggle
        {
            get
            {
                object obj = this.ViewState["CheckboxToggle"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["CheckboxToggle"] = value;
            }
        }

        /// <summary>
        /// A css class to apply to the x-form-items of fields. This property cascades to child containers.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class to apply to the x-form-items of fields. This property cascades to child containers.")]
        public override string ItemCls
        {
            get
            {
                return (string)this.ViewState["ItemCls"] ?? "";
            }
            set
            {
                this.ViewState["ItemCls"] = value;
            }
        }

        /// <summary>
        /// The width of labels. This property cascades to child containers.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "75")]
        [Description("The width of labels. This property cascades to child containers.")]
        public virtual Unit LabelWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["LabelWidth"], Unit.Pixel(75), "LabelWidth");
            }
            set
            {
                this.ViewState["LabelWidth"] = value;
            }
        }
    }
}