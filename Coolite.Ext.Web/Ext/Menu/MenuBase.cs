/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class MenuBase : InnerObservable
    {
        protected override bool RemoveContainer
        {
            get
            {
                return true;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return this.Items.Count == 0;
            }
        }

        protected override bool NeedRender
        {
            get
            {
                return true;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to allow multiple menus to be displayed at the same time (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool AllowOtherMenus
        {
            get
            {
                object obj = this.ViewState["AllowOtherMenus"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AllowOtherMenus"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The default Ext.Element.alignTo anchor position value for this menu relative to its element of origin (defaults to \"tl-bl?\")")]
        [NotifyParentProperty(true)]
        public virtual string DefaultAlign
        {
            get
            {
                return (string)this.ViewState["DefaultAlign"] ?? "";
            }
            set
            {
                this.ViewState["DefaultAlign"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to ignore clicks on any item in this menu that is a parent item (displays a submenu) so that the submenu is not dismissed when clicking the parent item (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool IgnoreParentClicks
        {
            get
            {
                object obj = this.ViewState["IgnoreParentClicks"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["IgnoreParentClicks"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(120)]
        [Description("The minimum width of the menu in pixels (defaults to 120).")]
        [NotifyParentProperty(true)]
        public virtual int MinWidth
        {
            get
            {
                object obj = this.ViewState["MinWidth"];
                return (obj == null) ? 120 : (int)obj;
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        /// <summary>
        /// The width of this component in pixels (defaults to auto).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetWidth")]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The width of this component in pixels (defaults to auto).")]
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

        [ClientConfig(typeof(ShadowJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(ShadowMode.Sides)]
        [Description("True or \"sides\" for the default effect, \"frame\" for 4-way shadow, and \"drop\" for bottom-right shadow (defaults to \"sides\")")]
        [NotifyParentProperty(true)]
        public virtual ShadowMode Shadow
        {
            get
            {
                object obj = this.ViewState["Shadow"];
                return (obj == null) ? ShadowMode.Sides : (ShadowMode)obj;
            }
            set
            {
                this.ViewState["Shadow"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The Ext.Element.alignTo anchor position value to use for submenus of this menu (defaults to \"tl-tr?\")")]
        [NotifyParentProperty(true)]
        public virtual string SubMenuAlign
        {
            get
            {
                return (string)this.ViewState["SubMenuAlign"] ?? "";
            }
            set
            {
                this.ViewState["SubMenuAlign"] = value;
            }
        }

        ItemsCollection<BaseMenuItem> items;

        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Items collection")]
        public virtual ItemsCollection<BaseMenuItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<BaseMenuItem>();
                    this.items.AfterItemAdd += AfterItemAdd;
                    this.items.SingleItemMode = this.SingleItemMode;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(BaseMenuItem item)
        {
            if (!this.Controls.Contains(item))
            {
                this.Controls.Add(item);
            }

            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool DisableMenuNavigation
        {
            get
            {
                object obj = this.ViewState["DisableMenuNavigation"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DisableMenuNavigation"] = value;
            }
        }

        [ClientConfig("keyNav", JsonMode.Raw)]
        [DefaultValue("")]
        internal virtual string DisableMenuNavigationProxy
        {
            get
            {
                return this.DisableMenuNavigation ? "{disable:Ext.emptyFn}" : "";
            }
        }

        /// <summary>
        /// Hides this menu and optionally all parent menus
        /// </summary>
        /// <param name="deep">True to hide all parent menus recursively, if any</param>
        [Description("Hides this menu and optionally all parent menus")]
        public virtual void Hide(bool deep)
        {
            this.AddScript("{0}.hide({1});", this.ClientID, JSON.Serialize(deep));
        }

        /// <summary>
        /// Hides this menu and optionally all parent menus
        /// </summary>
        [Description("Hides this menu and optionally all parent menus")]
        public virtual void Hide()
        {
            this.AddScript("{0}.hide();", this.ClientID);
        }

        /// <summary>
        /// Removes and destroys all items in the menu
        /// </summary>
        [Description("Removes and destroys all items in the menu")]
        public virtual void RemoveAll()
        {
            this.AddScript("{0}.removeAll();", this.ClientID);
        }

        /// <summary>
        /// Displays this menu relative to another element
        /// </summary>
        /// <param name="element">The element to align to</param>
        /// <param name="position">The Ext.Element.alignTo anchor position to use in aligning to the element</param>
        [Description("Displays this menu relative to another element")]
        public virtual void Show(string element, string position)
        {
            this.AddScript("{0}.show({1}, {2});", this.ClientID, element, JSON.Serialize(position));
        }

        /// <summary>
        /// Displays this menu relative to another element
        /// </summary>
        /// <param name="element">The element to align to</param>
        [Description("Displays this menu relative to another element")]
        public virtual void Show(string element)
        {
            this.AddScript("{0}.show({1});", this.ClientID, element);
        }

        /// <summary>
        /// Displays this menu at a specific xy position
        /// </summary>
        /// <param name="x">Contains [x] value for the position at which to show the menu (coordinates are page-based)</param>
        /// <param name="y">Contains [y] value for the position at which to show the menu (coordinates are page-based)</param>
        [Description("Displays this menu at a specific xy position")]
        public virtual void ShowAt(int x, int y)
        {
            this.AddScript("{0}.showAt([{1},{2}]);", this.ClientID, x, y);
        }

        /// <summary>
        /// Sets the width of the menu.
        /// </summary>
        [Description("Sets the width of the menu.")]
        protected virtual void SetWidth(int width)
        {
            this.AddScript("{0}.el.setWidth({1});", this.ClientID, width);
        }
    }
}