/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [Layout("cooliteanchor")]
    [ToolboxData("<{0}:AnchorLayout runat=\"server\"></{0}:AnchorLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.AnchorLayout), "Build.Resources.ToolboxIcons.AnchorLayout.bmp")]
    [DefaultProperty("Anchors")]
    [ParseChildren(true, "Anchors")]
    public class AnchorLayout : ContainerLayout
    {
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The height of this Anchor in pixels (defaults to auto).")]
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Unit Height
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Height"], Unit.Empty, "Height");
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The width of this Anchor in pixels (defaults to auto).")]
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Unit Width
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

        [ClientConfig("anchorSize", JsonMode.Object)]
        [DefaultValue(null)]
        [Browsable(false)]
        public AnchorSizeProxy AnchorSize
        {
            get
            {
                return new AnchorSizeProxy(this.Width, this.Height);
            }
        }

        private AnchorCollection anchors;

        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Anchors collection")]
        [ViewStateMember]
        public AnchorCollection Anchors
        {
            get
            {
                if (this.anchors == null)
                {
                    this.anchors = new AnchorCollection();
                    this.anchors.AfterItemAdd += Anchors_AfterItemAdd;
                }
                return this.anchors;
            }
        }

        private void Anchors_AfterItemAdd(Anchor item)
        {
            if (item.Control != null)
            {
                this.Items.Add((Component)item.Control);
                item.Items[0].AdditionalConfig = item;
            }
            item.Items.AfterItemAdd += delegate(Component cItem)
                                           {
                                               this.Items.Add(cItem);
                                               cItem.AdditionalConfig = item;
                                           };
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                foreach (Anchor anchor in this.Anchors)
                {
                    if (anchor.Items.Count == 0)
                    {
                        throw new InvalidOperationException("Anchor must contain at least one Component.");
                    }
                }
            }

            base.OnPreRender(e);
        }
    }

    public class AnchorCollection : StateManagedCollection<Anchor> 
    {
        public virtual void Add(Component component)
        {
            this.Add(new Anchor(component));
        }
    }
   
    public class Anchor : LayoutItem
    {
        public Anchor() { }

        public Anchor(Component component) 
        {
            this.Items.Add(component);
        }

        public Anchor(Component component, string horizontal, string vertical)
        {
            this.Items.Add(component);
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if component should be rendered as a Form Field with a Field Label and Label separator (defaults to false).")]
        [NotifyParentProperty(true)]
        public virtual bool IsFormField
        {
            get
            {
                object obj = this.ViewState["IsFormField"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["IsFormField"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        public virtual string Horizontal
        {
            get
            {
                string temp = (string)this.ViewState["Horizontal"] ?? "";
                if (!string.IsNullOrEmpty(this.Vertical) && string.IsNullOrEmpty(temp))
                {
                    return "100%";
                }
                return temp;
            }
            set
            {
                this.ViewState["Horizontal"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Vertical
        {
            get
            {
                return (string)this.ViewState["Vertical"] ?? "";
            }
            set
            {
                this.ViewState["Vertical"] = value;
            }
        }

        [ClientConfig("anchor", JsonMode.Value)]
        [DefaultValue("")]
        [Browsable(false)]
        protected virtual string AnchorProxy
        {
            get
            {
                if (string.IsNullOrEmpty(this.Vertical) && !string.IsNullOrEmpty(this.Horizontal))
                {
                    return this.Horizontal;
                }
                else if (!string.IsNullOrEmpty(this.Horizontal) && !string.IsNullOrEmpty(this.Vertical))
                {
                    return string.Concat(this.Horizontal, " ", this.Vertical);
                }
                return string.Empty;
            }
        }
    }


    public class AnchorSizeProxy
    {
        private readonly bool isFormField;
        private readonly Unit width;
        private readonly Unit height;

        public AnchorSizeProxy(Unit width, Unit height)
        {
            this.width = width;
            this.height = height;
        }

        public AnchorSizeProxy(Unit width, Unit height, bool isFormField)
        {
            this.width = width;
            this.height = height;
            this.isFormField = isFormField;
        }

        [ClientConfig]
        [DefaultValue(false)]
        public bool IsFormField
        {
            get { return this.isFormField; }
        }

        [ClientConfig]
        [DefaultValue(typeof(Unit), "")]
        public Unit Width
        {
            get { return this.width; }
        }

        [ClientConfig]
        [DefaultValue(typeof(Unit), "")]
        public Unit Height
        {
            get { return this.height; }
        }
    }

}