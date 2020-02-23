/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [Xtype("coolitemultifield")]
    [InstanceOf(ClassName = "Coolite.Ext.MultiField")]
    [Description("Multi field class")]
    [ToolboxData("<{0}:MultiField runat=\"server\" />")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.MultiField), "Build.Resources.ToolboxIcons.MultiField.bmp")]
    public class MultiField : Field
    {
        private ItemsCollection<Component> fields;

        /// <summary>
        /// A collection of fields
        /// </summary>
        [ClientConfig("fields", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("A collection of fields.")]
        public virtual ItemsCollection<Component> Fields
        {
            get
            {
                if (this.fields == null)
                {
                    this.fields = new ItemsCollection<Component>();
                    this.fields.AfterItemAdd += this.AfterItemAdd;
                }

                return this.fields;
            }
        }

        protected virtual void AfterItemAdd(Component item)
        {
            if(item is MultiField)
            {
                throw new Exception("MultiField can't contains another MultiField");
            }
            this.Controls.Add(item);
            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

    }
}