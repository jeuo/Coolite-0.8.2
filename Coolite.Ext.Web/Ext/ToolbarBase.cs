/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class ToolbarBase : BoxComponent
    {
        protected override void OnPreRender(EventArgs e)
        {
            if (this.Flat && !Ext.IsAjaxRequest)
            {
                this.Cls = string.Concat(this.Cls, string.IsNullOrEmpty(this.Cls) ? "" : " ", "x-inline-toolbar");
            }
            if (this.Items.Count == 1)
            {
                //workaround for creating array always in ItemCollectionJsonConverter
                this.Items.Add(new ToolbarSpacer());
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// True to use flat style.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to use flat style.")]
        [NotifyParentProperty(true)]
        public virtual bool Flat
        {
            get
            {
                object obj = this.ViewState["Flat"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Flat"] = value;
            }
        }


        ItemsCollection<Observable> items;

        [ClientConfig("items", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        //[Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Items collection")]
        public virtual ItemsCollection<Observable> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Observable>();
                    this.items.AfterItemAdd += AfterItemAdd;
                    this.items.SingleItemMode = this.SingleItemMode;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Observable item)
        {
            if (!this.Controls.Contains(item))
            {
                this.Controls.Add(item);
            }

            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }

            ToolbarItem ti = item as ToolbarItem;
            if(ti != null)
            {
                ti.Toolbar = this;    
            }
        }
    }
}