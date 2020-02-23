/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class StartMenuConfig : StateManagedItem, IIcon
    {
        private Desktop desktop;

        public StartMenuConfig(Desktop desktop)
        {
            this.desktop = desktop;
        }

        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [NotifyParentProperty(true)]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                if (this.Icon != Icon.None)
                {
                    return string.Format("icon-{0}", this.Icon.ToString().ToLower());
                }
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(300)]
        [NotifyParentProperty(true)]
        public virtual int Width
        {
            get
            {
                object obj = this.ViewState["Width"];
                return obj == null ? 300 : (int)obj;
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(100)]
        [NotifyParentProperty(true)]
        public virtual int ToolsWidth
        {
            get
            {
                object obj = this.ViewState["ToolsWidth"];
                return obj == null ? 100 : (int)obj;
            }
            set
            {
                this.ViewState["ToolsWidth"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(300)]
        [NotifyParentProperty(true)]
        public virtual int Height
        {
            get
            {
                object obj = this.ViewState["Height"];
                return obj == null ? 300 : (int)obj;
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Title
        {
            get
            {
                return (string)this.ViewState["Title"] ?? "";
            }
            set
            {
                this.ViewState["Title"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        public virtual bool Shadow
        {
            get
            {
                object obj = this.ViewState["Shadow"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Shadow"] = value;
            }
        }

        ItemsCollection<BaseMenuItem> toolItems;

        [ClientConfig("toolItems", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Tool items collection")]
        public virtual ItemsCollection<BaseMenuItem> ToolItems
        {
            get
            {
                if (this.toolItems == null)
                {
                    this.toolItems = new ItemsCollection<BaseMenuItem>();
                    this.toolItems.AfterItemAdd += AfterItemAdd;
                    this.toolItems.SingleItemMode = false;
                }
                return this.toolItems;
            }
        }

        ItemsCollection<BaseMenuItem> items;

        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Items collection")]
        public virtual ItemsCollection<BaseMenuItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<BaseMenuItem>();
                    this.items.AfterItemAdd += AfterItemAdd;
                    this.items.SingleItemMode = false;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(BaseMenuItem item)
        {
            if (!this.desktop.Controls.Contains(item))
            {
                this.desktop.Controls.Add(item);
            }

            if (!this.desktop.LazyItems.Contains(item))
            {
                this.desktop.LazyItems.Add(item);
            }
        }

        List<Icon> IIcon.Icons
        {
            get
            {
                List<Icon> icons = new List<Icon>(1);
                icons.Add(this.Icon);
                return icons;
            }
        }
    }
}