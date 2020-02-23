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
    [Description("Adds a separator bar to a menu, used to divide logical groups of menu items. Generally you will add one of these by using \" - \" in you call to add() or in your items config rather than creating one directly.")]
    [InstanceOf(ClassName = "Ext.menu.Separator")]
    public class MenuSeparator : BaseMenuItem, ICustomConfigSerialization
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide the containing menu after this item is clicked (defaults to true).")]
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

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The default CSS class to use for text items (defaults to \"x-menu-text\")")]
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

        public string Serialize(Control owner)
        {
            return string.Concat("new Ext.menu.Separator(", new ClientConfig(true).Serialize(this, true), ")");
        }
    }
}