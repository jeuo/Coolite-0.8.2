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
    /// This is a layout specifically designed for creating forms.
    /// </summary>
    [Layout("form")]
    [ToolboxData("<{0}:FormLayout runat=\"server\"><Anchors><{0}:Anchor><{0}:TextField runat=\"server\" FieldLabel=\"Field1\" /></{0}:Anchor></Anchors></{0}:FormLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.FormLayout), "Build.Resources.ToolboxIcons.AnchorLayout.bmp")]
    [DefaultProperty("Anchors")]
    [ParseChildren(true, "Anchors")]
    public class FormLayout : AnchorLayout
    {
        /// <summary>
        /// A CSS style specification string to add to each field element in this layout (defaults to '').
        /// </summary>
        [NotifyParentProperty(true)]
        [Description("A CSS style specification string to add to each field element in this layout (defaults to '').")]
        public virtual string ElementStyle
        {
            get
            {
                return (string)this.ViewState["ElementStyle"] ?? "";
            }
            set
            {
                this.ViewState["ElementStyle"] = value;
            }
        }

        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        [DefaultValue(null)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new FormLayoutProxy(
                    this.ElementStyle, 
                    this.LabelSeparator, 
                    this.LabelStyle, 
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide field labels by default (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool HideLabels
        {
            get
            {
                object obj = this.ViewState["HideLabels"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideLabels"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("A CSS class to add to the div wrapper that contains each field label and field element (the default class is 'x-form-item' and itemCls will be added to that)")]
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

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(LabelAlign.Left)]
        [NotifyParentProperty(true)]
        [Description("The default label alignment. The default value is empty string '' for left alignment, but specifying 'top' will align the labels above the fields.")]
        public virtual LabelAlign LabelAlign
        {
            get
            {
                object obj = this.ViewState["LabelAlign"];
                return (obj == null) ? LabelAlign.Left : (LabelAlign)obj;
            }
            set
            {
                this.ViewState["LabelAlign"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(5)]
        [NotifyParentProperty(true)]
        [Description("The default padding in pixels for field labels (defaults to 5). labelPad only applies if labelWidth is also specified, otherwise it will be ignored.")]
        public virtual int LabelPad
        {
            get
            {
                object obj = this.ViewState["LabelPad"];
                return (obj == null) ? 5 : (int)obj;
            }
            set
            {
                this.ViewState["LabelPad"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(100)]
        [NotifyParentProperty(true)]
        [Description("The default width in pixels of field labels (defaults to 100).")]
        public virtual int LabelWidth
        {
            get
            {
                object obj = this.ViewState["LabelWidth"];
                return (obj == null) ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["LabelWidth"] = value;
            }
        }

    }

    internal class FormLayoutProxy : LayoutConfig
    {
        private readonly string elementStyle;
        private readonly string labelSeparator;
        private readonly string labelStyle;

        public FormLayoutProxy(string elementStyle, string labelSeparator, string labelStyle, bool renderHidden, string extraCls) : base(renderHidden, extraCls)
        {
            this.elementStyle = elementStyle;
            this.labelSeparator = labelSeparator;
            this.labelStyle = labelStyle;
        }

        [ClientConfig]
        [DefaultValue("")]
        public string ElementStyle
        {
            get
            {
                return this.elementStyle;
            }
        }

        [ClientConfig]
        [DefaultValue(":")]
        public string LabelSeparator
        {
            get
            {
                return this.labelSeparator;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        public string LabelStyle
        {
            get
            {
                return this.labelStyle;
            }
        }
    }
}