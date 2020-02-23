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
    [Xtype("hyperlink")]
    [InstanceOf(ClassName = "Coolite.Ext.HyperLink")]
    [ToolboxData("<{0}:HyperLink runat=\"server\" Text=\"HyperLink\" />")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.HyperLink), "Build.Resources.ToolboxIcons.HyperLink.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Basic hyperlink field.")]
    public class HyperLink : Label
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
        [AjaxEventUpdate(MethodName = "SetNavigateUrl")]
        public virtual string NavigateUrl
        {
            get
            {
                return (string)this.ViewState["NavigateUrl"] ?? "";
            }
            set
            {
                this.ViewState["NavigateUrl"] = value;
            }
        }

        [ClientConfig("url")]
        [DefaultValue("")]
        internal virtual string NavigateUrlProxy
        {
            get {
                return this.ResolveUrlLink(this.NavigateUrl);
            }
        }
        
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetTarget")]
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

        protected virtual void SetNavigateUrl(string url)
        {
            this.AddScript("{0}.setUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetImageUrl(string url)
        {
            this.AddScript("{0}.setImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetTarget(string target)
        {
            this.AddScript("{0}.setTarget({1});", this.ClientID, JSON.Serialize(target));
        }
    }
}