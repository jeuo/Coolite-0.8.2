/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class ToolbarItem : Observable, IToolbarItem, ICustomConfigSerialization
    {
        private ToolbarBase toolbar;
        public ToolbarBase Toolbar
        {
            get
            {
                return this.toolbar;
            }
            internal set
            {
                this.toolbar = value;
            }
        }

        public virtual string Serialize(Control owner)
        {
            if (this.IsIDRequired)
            {
                return string.Concat("this.", this.ClientID, " = new ",this.InstanceOf,"(", new ClientConfig().Serialize(this, true), ")", this.IsLazy ? "" : ";");
            }
            
            return new ClientConfig().Serialize(this, true);
        }

        /// <summary>
        /// Destroys this component by purging any event listeners, removing the component's element from the DOM, removing the component from its Ext.Container (if applicable) and unregistering it from Ext.ComponentMgr. Destruction is generally handled automatically by the framework and this method should usually not need to be called directly.
        /// </summary>
        [Description("Destroys this component by purging any event listeners, removing the component's element from the DOM, removing the component from its Ext.Container (if applicable) and unregistering it from Ext.ComponentMgr. Destruction is generally handled automatically by the framework and this method should usually not need to be called directly.")]
        public virtual void Destroy()
        {
            this.AddScript("{0}.destroy();", this.ClientID);
        }

        /// <summary>
        /// Disable this component.
        /// </summary>
        [Description("Disable this component.")]
        public virtual void Disable()
        {
            this.AddScript("{0}.disable();", this.ClientID);
        }

        /// <summary>
        /// Enable this component.
        /// </summary>
        [Description("Enable this component.")]
        public virtual void Enable()
        {
            this.AddScript("{0}.enable();", this.ClientID);
        }

        /// <summary>
        /// Try to focus this component.
        /// </summary>
        [Description("Try to focus this component.")]
        new public virtual void Focus()
        {
            this.AddScript("{0}.focus();", this.ClientID);
        }

        /// <summary>
        /// Hide this component.
        /// </summary>
        [Description("Hide this component.")]
        public virtual void Hide()
        {
            this.AddScript("{0}.hide();", this.ClientID);
        }

        /// <summary>
        /// Convenience function to hide or show this component by boolean.
        /// </summary>
        [Description("Convenience function to hide or show this component by boolean.")]
        public virtual void SetVisible(bool disabled)
        {
            this.AddScript("{0}.setVisible({1});", this.ClientID, disabled.ToString().ToLower());
        }

        /// <summary>
        /// Show this component.
        /// </summary>
        [Description("Show this component.")]
        public virtual void Show()
        {
            this.AddScript("{0}.show();", this.ClientID);
        }
    }

    [ToolboxItem(false)]
    [Xtype("coolitetbspacer")]
    [Description("A simple element that adds extra horizontal space between items in a toolbar.")]
    [InstanceOf(ClassName = "Coolite.Ext.ToolbarSpacer")]
    public class ToolbarSpacer : ToolbarItem
    {
        public ToolbarSpacer() { }

        public ToolbarSpacer(Unit width)
        {
            this.Width = width;
        }

        public ToolbarSpacer(int width)
        {
            this.Width = Unit.Pixel(width);
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [NotifyParentProperty(true)]
        new public virtual Unit Width
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Width"], Unit.Empty, "Width");
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }
    }

    [ToolboxItem(false)]
    [Xtype("tbfill")]
    [Description("A simple element that adds a greedy (100% width) horizontal space between items in a toolbar.")]
    [InstanceOf(ClassName = "Ext.Toolbar.Fill")]
    public class ToolbarFill : ToolbarItem { }

    [ToolboxItem(false)]
    [Xtype("tbseparator")]
    [Description("A simple class that adds a vertical separator bar between toolbar items.")]
    [InstanceOf(ClassName = "Ext.Toolbar.Separator")]
    public class ToolbarSeparator : ToolbarItem { }

    [ToolboxItem(false)]
    [Xtype("tbtext")]
    [Description("A simple class that renders text directly into a toolbar.")]
    [InstanceOf(ClassName = "Ext.Toolbar.TextItem")]
    public class ToolbarTextItem : ToolbarItem
    {
        [ClientConfig]
        [AjaxEventUpdate(MethodName="SetText")]
        [Category("Config Options")]
        [DefaultValue("")]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Clears the status text and iconCls. Also supports clearing via an optional fade out animation.
        /// </summary>
        [Description("Clears the status text and iconCls. Also supports clearing via an optional fade out animation.")]
        protected virtual void SetText(string text)
        {
            if(this.Toolbar == null)
            {
                throw new ArgumentNullException("Toolbar", string.Format("The Toolbar that this ToolbarTextItem ({0}) belongs to is empty.", this.ID));
            }

            int index = this.Toolbar.Items.IndexOf(this);
            if(index<0)
            {
                throw new ArgumentException("The ToolbarItem can not be found in the Toolbar Items Collection");
            }
            this.AddScript("{0}.items.get({1}).setText({2});", this.Toolbar.ClientID, index, JSON.Serialize(text));
        }
    }

    [ToolboxItem(false)]
    [Xtype("coolitetbhtml")]
    [Description("Any standard HTML element.")]
    [InstanceOf(ClassName = "Ext.Toolbar.HtmlElement")]
    public class ToolbarHtmlElement : ToolbarItem
    {
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
    }
}