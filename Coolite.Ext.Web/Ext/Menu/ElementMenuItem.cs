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
    [ToolboxItem(false)]
    [Description("A menu item that wraps the Ext.ColorPalette component.")]
    [InstanceOf(ClassName = "Coolite.Ext.ElementMenuItem")]
    public class ElementMenuItem : BaseMenuItem, ICustomConfigSerialization
    {
        [ClientConfig(JsonMode.Ignore)]
        public override string Xtype
        {
            get { return base.Xtype; }
        }
        
        [ClientConfig("target", JsonMode.Raw)]
        [DefaultValue("")]
        internal string TargetProxy
        {
            get
            {
                return TokenUtils.ParseAndNormalize(this.Target, this);
            }
        }

        [Category("Config Options")]
        [DefaultValue(TargetElement.Element)]
        [ClientConfig(JsonMode.ToLower)]
        [Description("The element of target which will be used during menu item rendering")]
        public virtual TargetElement TargetElement
        {
            get
            {
                object obj = this.ViewState["TargetElement"];
                return obj == null ? TargetElement.Element : (TargetElement)obj;
            }
            set
            {
                this.ViewState["TargetElement"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue(true)]
        [ClientConfig]
        [Description("If true then element will be shiffted on horizontal, the icon place will be visible")]
        public virtual bool Shift
        {
            get
            {
                object obj = this.ViewState["Shift"];
                return obj == null ? true : (bool) obj;
            }
            set
            {
                this.ViewState["Shift"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The target element which will be placed to toolbar.")]
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

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the containing menu after this item is clicked (defaults to false).")]
        [NotifyParentProperty(true)]
        public override bool HideOnClick
        {
            get
            {
                object obj = this.ViewState["HideOnClick"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HideOnClick"] = value;
            }
        }

        public string Serialize(Control owner)
        {
            return string.Concat("new Coolite.Ext.ElementMenuItem(", new ClientConfig(true).Serialize(this, true), ")");
        }
    }

    public enum TargetElement
    {
        Element,
        Wrap
    }
}