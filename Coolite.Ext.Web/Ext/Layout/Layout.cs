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
    /// <summary>
    /// Every layout is composed of one or more Ext.Container elements internally, and Layout provides the basic foundation for all other layout classes in Ext. It is a non-visual class that simply provides the base logic required for a Container to function as a layout.
    /// </summary>
    [Description("Every layout is composed of one or more Ext.Container elements internally, and Layout provides the basic foundation for all other layout classes in Ext. It is a non-visual class that simply provides the base logic required for a Container to function as a layout.")]
    public abstract class Layout : Component
    {
        /// <summary>
        /// The layout type to be used in this Body Container. If not specified, a default is Container. Specific config values for the chosen layout type can be specified using layoutConfig.
        /// </summary>
        [ClientConfig("layout")]
        [Category("Config Options")]
        [DefaultValue("container")]
        [Description("The layout type to be used in this Body Container. If not specified, a default is Container. Specific config values for the chosen layout type can be specified using layoutConfig.")]
        [Browsable(false)]
        public virtual string LayoutType
        {
            get
            {
                object[] attrs = this.GetType().GetCustomAttributes(typeof(LayoutAttribute), true);

                if (attrs.Length == 1)
                {
                    return ((LayoutAttribute)attrs[0]).Name;
                }

                return "container";
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Height
        {
            get 
            { 
                return base.Height; 
            }
            set 
            { 
                base.Height = value; 
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Width
        {
            get 
            { 
                return base.Width; 
            }
            set 
            { 
                base.Width = value; 
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string ClientIDProxy
        {
            get
            {
                return base.ClientIDProxy;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// An optional extra CSS class that will be added to the contentContainer (defaults to ''). This can be useful for adding customized styles to the contentContainer or any of its children using standard CSS rules.
        /// </summary>
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An optional extra CSS class that will be added to the contentContainer (defaults to ''). This can be useful for adding customized styles to the contentContainer or any of its children using standard CSS rules.")]
        [NotifyParentProperty(true)]                
        public virtual string ExtraCls
        {
            get
            {
                return (string)this.ViewState["ExtraCls"] ?? "";
            }
            set
            {
                this.ViewState["ExtraCls"] = value;
            }
        }

        /// <summary>
        /// True to hide each contained items on render (defaults to false).
        /// </summary>
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to hide each contained items on render (defaults to false).")]
        [NotifyParentProperty(true)]                
        public virtual bool RenderHidden
        {
            get
            {
                object obj = this.ViewState["RenderHidden"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["RenderHidden"] = value;
            }
        }

        ItemsCollection<Component> items;

        /// <summary>
        /// Items collection
        /// </summary>
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Editor(typeof(ItemCollectionEditor), typeof(UITypeEditor))]
        [Description("Items collection")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual ItemsCollection<Component> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new ItemsCollection<Component>();
                    this.items.AfterItemAdd += AfterItemAdd;
                    this.items.SingleItemMode = this.SingleItemMode;
                }
                return this.items;
            }
        }

        protected virtual void AfterItemAdd(Component item)
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

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                Control parent = this.Parent;

                while (parent is ContentPlaceHolder || parent is UserControl)
                {
                    parent = parent.Parent;
                }

                parent = parent.Parent;

                if (parent != null && !(parent is IContent) && !(parent is CheckboxGroupBase))
                {
                    throw new InvalidOperationException(string.Format("Parent for Layout Control ({0}) must be a Coolite Container Control, such as Panel, TabPanel, Window, ViewPort, etc.", this.ID));
                }
            }

            base.OnPreRender(e);
        }
    }
}