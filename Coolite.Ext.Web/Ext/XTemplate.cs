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
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:XTemplate runat=\"server\"></{0}:XTemplate>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.XTemplate), "Build.Resources.ToolboxIcons.XTemplate.bmp")]
    [ToolboxItem(false)]
    [Designer(typeof(EmptyDesigner))]
    [DefaultProperty("Text")]
    [ParseChildren(true, "Text")]
    [InstanceOf(ClassName = "Ext.XTemplate")]
    public class XTemplate : InnerObservable, ICustomConfigSerialization
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CreateTextArea();
        }

        private HtmlTextArea textArea;

        internal HtmlTextArea TextArea
        {
            get
            {
                if (this.textArea == null)
                {
                    this.CreateTextArea();
                }
                return this.textArea;
            }
        }

        private void CreateTextArea()
        {
            if (this.textArea == null)
            {
                this.textArea = new HtmlTextArea();
                this.textArea.EnableViewState = false;
                this.textArea.ID = string.Concat(this.ID, "_template");
                this.textArea.Attributes["class"] = "x-hidden";
                this.textArea.Visible = false;
                this.Controls.Add(this.textArea);
            }
        }

        private bool renderingComplete;

        protected override void OnPreRender(EventArgs e)
        {
            this.SetTextArea();
            base.OnPreRender(e);
        }

        private void SetTextArea()
        {
            if(this.AutoDataBind)
            {
                this.DataBind();
            }

            if (this.NeedRender && !this.renderingComplete)
            {
                this.TextArea.Value = this.Text;
                this.TextArea.Visible = true;
                this.renderingComplete = true;
            }
        }

        protected override bool NeedRender
        {
            get
            {
                return !string.IsNullOrEmpty(this.Text);
            }
        }

        public string Serialize(Control owner)
        {
            if (!this.IsDefault && !this.IsParentDeferredRender)
            {
                this.SetTextArea();
                string id = this.TextArea.ClientID;
                return string.Concat("this.", this.ClientID, "=Ext.XTemplate.from(\"", id, "\");Ext.get(\"", id, "\").remove();");
            }

            return "";
        }

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Template text")]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.Text);
            }
        }

        /// <summary>
        /// Applies the supplied values to the template and appends the new node(s) to el.
        /// </summary>
        /// <param name="target">The context element</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and appends the new node(s) to el.")]
        public void Append(string target, object data)
        {
            this.ScriptHelper("append", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and appends the new node(s) to el.
        /// </summary>
        /// <param name="target">A ContentPanel whose body will be updated.</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and appends the new node(s) to el.")]
        public void Append(ContentPanel target, object data)
        {
            this.ScriptHelper("append", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) after el.
        /// </summary>
        /// <param name="target">The context element</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) after el.")]
        public void InsertAfter(string target, object data)
        {
            this.ScriptHelper("insertAfter", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) after el.
        /// </summary>
        /// <param name="target">A ContentPanel whose body will be updated.</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) after el.")]
        public void InsertAfter(ContentPanel target, object data)
        {
            this.ScriptHelper("insertAfter", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) before el.
        /// </summary>
        /// <param name="target">The context element</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) before el.")]
        public void InsertBefore(string target, object data)
        {
            this.ScriptHelper("insertBefore", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) before el.
        /// </summary>
        /// <param name="target">A ContentPanel whose body will be updated.</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) before el.")]
        public void InsertBefore(ContentPanel target, object data)
        {
            this.ScriptHelper("insertBefore", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) as the first child of el.
        /// </summary>
        /// <param name="target">The context element</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) as the first child of el.")]
        public void InsertFirst(string target, object data)
        {
            this.ScriptHelper("insertFirst", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and inserts the new node(s) as the first child of el.
        /// </summary>
        /// <param name="target">A ContentPanel whose body will be updated.</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and inserts the new node(s) as the first child of el.")]
        public void InsertFirst(ContentPanel target, object data)
        {
            this.ScriptHelper("insertFirst", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and overwrites the content of el with the new node(s).
        /// </summary>
        /// <param name="target">The context element</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and overwrites the content of el with the new node(s).")]
        public void Overwrite(string target, object data)
        {
            this.ScriptHelper("overwrite", target, data);
        }

        /// <summary>
        /// Applies the supplied values to the template and overwrites the content of el with the new node(s).
        /// </summary>
        /// <param name="target">A ContentPanel whose body will be updated.</param>
        /// <param name="data">The template values. Can be an array if your params are numeric (i.e. {0}) or an object (i.e. {foo: 'bar'})</param>
        [Description("Applies the supplied values to the template and overwrites the content of el with the new node(s).")]
        public void Overwrite(ContentPanel target, object data)
        {
            this.ScriptHelper("overwrite", target, data);
        }

        protected void ScriptHelper(string name, ContentPanel target, object data)
        {
            this.ScriptHelper(name, string.Concat("={", target.ClientID, ".body}"), data);
        }

        protected void ScriptHelper(string name, string target, object data)
        {
            this.AddScript(string.Concat(this.ClientID, ".", name, "(Coolite.Ext.getEl(", this.ParseTarget(target), "),", JSON.Serialize(data), ");"));
        }
    }
}