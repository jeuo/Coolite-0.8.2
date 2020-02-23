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
using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Very simple plugin for adding a close context menu to tabs
    /// </summary>
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.ux.plugins.TabCloseMenu")]
    [ClientScript(Type = typeof(TabCloseMenu), FilePath = "/ux/plugins/tabclosemenu/tabclosemenu.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.tabclosemenu.tabclosemenu.js")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TabCloseMenu), "Build.Resources.ToolboxIcons.Plugin.bmp")]
    [ToolboxData("<{0}:TabCloseMenu runat=\"server\" />")]
    [Description("Very simple plugin for adding a close context menu to tabs")]
    public class TabCloseMenu : Plugin, IIcon
    {
        /// <summary>
        /// Text to display in ContextMenu for menu option to close current Tab.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Close Tab")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Description("Text to display in ContextMenu for menu option to close current Tab.")]
        public string CloseTabText
        {
            get
            {
                return (string)this.ViewState["CloseTabText"] ?? "Close Tab";
            }
            set
            {
                this.ViewState["CloseTabText"] = value;
            }
        }

        /// <summary>
        /// Text to display in ContextMenu for menu option to close other Tabs.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Close Other Tabs")]
        [Localizable(true)]
        [NotifyParentProperty(true)]
        [Description("Text to display in ContextMenu for menu option to close other Tabs.")]
        public string CloseOtherTabsText
        {
            get
            {
                return (string)this.ViewState["CloseOtherTabsText"] ?? "Close Other Tabs";
            }
            set
            {
                this.ViewState["CloseOtherTabsText"] = value;
            }
        }

        /// <summary>
        /// The icon to use for the CloseTab menu item. See also, CloseTabIconCls to set an icon with a custom Css class.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The icon to use for the CloseTab menu item. See also, CloseTabIconCls to set an icon with a custom Css class.")]
        public virtual Icon CloseTabIcon
        {
            get
            {
                object obj = this.ViewState["CloseTabIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["CloseTabIcon"] = value;
            }
        }

        [ClientConfig("closeTabIconCls")]
        [DefaultValue("")]
        internal virtual string CloseTabIconClsProxy
        {
            get
            {
                if (this.CloseTabIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.CloseTabIcon);
                }
                return this.CloseTabIconCls;
            }
        }
 
        /// <summary>
        /// A CSS class that will provide a background image to be used as the icon to use for the CloseTab menu item (defaults to '').
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class that will provide a background image to be used as the icon to use for the CloseTab menu item (defaults to '').")]
        [NotifyParentProperty(true)]
        public virtual string CloseTabIconCls
        {
            get
            {
                return (string)this.ViewState["CloseTabIconCls"] ?? "";
            }
            set
            {
                this.ViewState["CloseTabIconCls"] = value;
            }
        }

        /// <summary>
        /// The icon to use for the CloseOtherTabs menu item. See also, CloseOtherTabsIconCls to set an icon with a custom Css class.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The icon to use for the CloseOtherTabs menu item. See also, CloseOtherTabsIconCls to set an icon with a custom Css class.")]
        public virtual Icon CloseOtherTabsIcon
        {
            get
            {
                object obj = this.ViewState["CloseOtherTabsIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["CloseOtherTabsIcon"] = value;
            }
        }

        [ClientConfig("closeOtherTabsIconCls")]
        [DefaultValue("")]
        internal virtual string CloseOtherTabsIconClsProxy
        {
            get
            {
                if (this.CloseOtherTabsIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.CloseOtherTabsIcon);
                }
                return this.CloseOtherTabsIconCls;
            }
        }

        /// <summary>
        /// A CSS class that will provide a background image to be used as the icon to use for the CloseOtherTabs menu item (defaults to '').
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A CSS class that will provide a background image to be used as the icon to use for the CloseOtherTabs menu item (defaults to '').")]
        [NotifyParentProperty(true)]
        public virtual string CloseOtherTabsIconCls
        {
            get
            {
                return (string)this.ViewState["CloseOtherTabsIconCls"] ?? "";
            }
            set
            {
                this.ViewState["CloseOtherTabsIconCls"] = value;
            }
        }

        List<Icon> IIcon.Icons
        {
            get
            {
                List<Icon> icons = new List<Icon>(2);
                icons.Add(this.CloseTabIcon);
                icons.Add(this.CloseOtherTabsIcon);
                return icons;
            }
        }
    }
}