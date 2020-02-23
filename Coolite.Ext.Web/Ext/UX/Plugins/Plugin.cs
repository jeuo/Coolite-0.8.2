/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [Description("Base Class for all Component Plugins. Add plugin to a Component using the .Plugins property or <Plugins> node.")]
    public abstract class Plugin : Observable 
    {
        [ClientConfig(JsonMode.Ignore)]
        protected override string ClientIDProxy
        {
            get
            {
                return base.ClientIDProxy;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IDMode IDMode
        {
            get { return base.IDMode; }
            set { base.IDMode = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Unit Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool EnableTheming
        {
            get { return base.EnableTheming; }
            set { base.EnableTheming = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool EnableViewState
        {
            get { return base.EnableViewState; }
            set { base.EnableViewState = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string SkinID
        {
            get { return base.SkinID; }
            set { base.SkinID = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override short TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // render nothing
            this.SimpleRender(writer);
        }
    }
}