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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [Xtype("imagebutton")]
    [ToolboxData("<{0}:ImageButton runat=\"server\" />")]
    [DefaultEvent("Click")]
    [DefaultProperty("ImageUrl")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ImageButton), "Build.Resources.ToolboxIcons.ImageButton.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [InstanceOf(ClassName = "Coolite.Ext.ImageButton")]
    [Description("Simple ImageButton class")]
    public class ImageButton : Button
    {
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetImageUrl")]
        public virtual string ImageUrl
        {
            get
            {
                return (string)this.ViewState["ImageUrl"] ?? "";
            }
            set
            {
                this.ViewState["ImageUrl"] = value;
            }
        }

        [ClientConfig("imageUrl")]
        [DefaultValue("")]
        internal virtual string ImageUrlProxy
        {
            get
            {
                return this.ResolveUrlLink(this.ImageUrl);
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetOverImageUrl")]
        public virtual string OverImageUrl
        {
            get
            {
                return (string)this.ViewState["OverImageUrl"] ?? "";
            }
            set
            {
                this.ViewState["OverImageUrl"] = value;
            }
        }

        [ClientConfig("overImageUrl")]
        [DefaultValue("")]
        internal virtual string OverImageUrlProxy
        {
            get
            {
                return this.ResolveUrlLink(this.OverImageUrl);
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetDisabledImageUrl")]
        public virtual string DisabledImageUrl
        {
            get
            {
                return (string)this.ViewState["DisabledImageUrl"] ?? "";
            }
            set
            {
                this.ViewState["DisabledImageUrl"] = value;
            }
        }

        [ClientConfig("disabledImageUrl")]
        [DefaultValue("")]
        internal virtual string DisabledImageUrlProxy
        {
            get
            {
                return this.ResolveUrlLink(this.DisabledImageUrl);
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetPressedImageUrl")]
        public virtual string PressedImageUrl
        {
            get
            {
                return (string)this.ViewState["PressedImageUrl"] ?? "";
            }
            set
            {
                this.ViewState["PressedImageUrl"] = value;
            }
        }

        [ClientConfig("pressedImageUrl")]
        [DefaultValue("")]
        internal virtual string PressedImageUrlProxy
        {
            get
            {
                return this.ResolveUrlLink(this.PressedImageUrl);
            }
        }

        [ClientConfig("altText")]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetAltText")]
        public virtual string AlternateText
        {
            get
            {
                return (string)this.ViewState["AlternateText"] ?? "";
            }
            set
            {
                this.ViewState["AlternateText"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(ImageAlign.NotSet)]
        [AjaxEventUpdate(MethodName = "SetAlign")]
        public ImageAlign Align
        {
            get
            {
                object obj = this.ViewState["Align"];
                return (obj == null) ? ImageAlign.NotSet : (ImageAlign)obj;
            }
            set
            {
                this.ViewState["Align"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override Icon Icon
        {
            get { return base.Icon; }
            set { base.Icon = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override string IconCls
        {
            get { return base.IconCls; }
            set { base.IconCls = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override ButtonType Type
        {
            get { return base.Type; }
            set { base.Type = value; }
        }

        /* AjaxEvent update functions*/
        protected virtual void SetImageUrl(string url)
        {
            this.AddScript("{0}.setImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetDisabledImageUrl(string url)
        {
            this.AddScript("{0}.setDisabledImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetOverImageUrl(string url)
        {
            this.AddScript("{0}.setOverImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetPressedImageUrl(string url)
        {
            this.AddScript("{0}.setPressedImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetAltText(string altText)
        {
            this.AddScript("{0}.setAltText({1});", this.ClientID, JSON.Serialize(altText));
        }

        protected virtual void SetAlign(ImageAlign align)
        {
            this.AddScript("{0}.setAlign({1});", this.ClientID, JSON.Serialize(align.ToString().ToLower()));
        }
    }
}