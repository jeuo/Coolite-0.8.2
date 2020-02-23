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

namespace Coolite.Ext.Web
{
    [DefaultProperty("Items")]
    [ParseChildren(true, "Items")]
    public abstract class LayoutItem : StateManagedItem
    {
        ItemsCollection<Component> items;

        /// <summary>
        /// Gets the Items Collection
        /// </summary>
        [Category("Config Options")]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Items")]
        public virtual ItemsCollection<Component> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Component>();
                    this.items.SingleItemMode = true;
                }
                return this.items;
            }
        }

        public Control Control
        {
            get
            {
                return this.Items.Count > 0 ? this.Items[0] : null;
            }
        }
    }
}