/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "")]
    public class DatePickerOptions : DatePicker
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoPostBack
        {
            get { return base.AutoPostBack; }
            set { base.AutoPostBack = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool CausesValidation
        {
            get { return base.CausesValidation; }
            set { base.CausesValidation = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string ValidationGroup
        {
            get { return base.ValidationGroup; }
            set { base.ValidationGroup = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DateTime SelectedDate
        {
            get { return base.SelectedDate; }
            set { base.SelectedDate = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        public override string ApplyTo
        {
            get { return base.ApplyTo; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        protected override string ClientIDProxy
        {
            get { return base.ClientIDProxy; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        public override string ID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ClientConfig(JsonMode.Ignore)]
        public override string RenderTo
        {
            get { return base.RenderTo; }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        protected override void Render(HtmlTextWriter writer) { }

        protected override void OnPreRender(EventArgs e) { }
    }
}
