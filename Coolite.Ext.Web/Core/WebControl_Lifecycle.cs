/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public partial class WebControl
    {
        /*  ViewState
            -----------------------------------------------------------------------------------------------*/

        private ViewStateProxy viewState;

        protected new ViewStateProxy ViewState
        {
            get
            {
                if (this.viewState == null)
                {
                    this.viewState = new ViewStateProxy(this, base.ViewState);
                }
                return this.viewState;
            }
        }

        protected override void LoadViewState(object state)
        {
            object[] states = state as object[];
            if (states != null)
            {
                foreach (Pair pair in states)
                {
                    switch ((string)pair.First)
                    {
                        case "base":
                            base.LoadViewState(pair.Second);
                            break;
                        case "vsMembers":
                            ViewStateProcessor.LoadViewState(this, pair.Second);
                            break;
                    }
                }
            }
            else
            {
                base.LoadViewState(state);
            }
        }

        protected override object SaveViewState()
        {
            List<Pair> state = new List<Pair>();
            object baseState = base.SaveViewState();
            if (baseState != null)
            {
                state.Add(new Pair("base", baseState));
            }

            object vsMembers = ViewStateProcessor.SaveViewState(this);
            if (vsMembers != null)
            {
                state.Add(new Pair("vsMembers", vsMembers));
            }

            return state.Count == 0 ? null : state.ToArray();
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        private bool isLast = false;

        internal virtual bool IsLast
        {
            get
            {
                return this.isLast;
            }
            set
            {
                this.isLast = value;
            }
        }

        internal protected virtual bool IsIDRequired
        {
            get
            {
                return !this.IsGeneratedID || this.ForceIDRendering;
            }
        }

        private bool isGeneratedID = true;

        internal virtual bool IsGeneratedID
        {
            get 
            { 
                return this.isGeneratedID; 
            }
            private set 
            { 
                this.isGeneratedID = value; 
            }
        }

        private bool forceIDRendering;
        
        internal virtual protected bool ForceIDRendering
        {
            get 
            { 
                return this.forceIDRendering; 
            }
            set 
            { 
                this.forceIDRendering = value; 
            }
        }

        private bool allowCallbackScriptMonitoring;

        internal virtual bool AllowCallbackScriptMonitoring
        {
            get
            {
                return this.allowCallbackScriptMonitoring;
            }
            set
            {
                this.allowCallbackScriptMonitoring = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.EnsureChildControls();

            if (this.DesignMode)
            {
                this.RegisterBeforeAfterScript();
            }
            else
            {
                if (this.Page != null)
                {
                    this.Page.LoadComplete += PageLoadComplete;
                }
            }

            //if (this is IContent)
            //{
            //    if (((IContent)this).Body != null)
            //    {
            //        ((IContent)this).Body.InstantiateIn(((IContent)this).BodyContainer);
            //    }
            //}

            this.AllowCallbackScriptMonitoring = true;
        }

        protected virtual void PageLoadComplete(object sender, EventArgs e) { }

        private void RegisterBeforeAfterScript()
        {
            if (this.IsLazy)
            {
                Component parent = this.ParentComponentNotLazy;

                if (parent != null)
                {
                    parent.AddBeforeClientInitScript(this.before);
                    parent.AddAfterClientInitScript(this.after);
                }
                else
                {
                    this.ScriptManager.RegisterBeforeClientInitScript(this.before);
                    this.ScriptManager.RegisterAfterClientInitScript(this.after);
                }
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            string containerStyle = this.GetContainerStyleAttribute();

            if (!string.IsNullOrEmpty(containerStyle))
            {
                string[] styles = containerStyle.Split(';');

                foreach (string style in styles)
                {
                    if (!string.IsNullOrEmpty(style))
                    {
                        string[] parts = style.Split(':');
                        writer.AddStyleAttribute(parts[0], parts[1]);
                    }
                }
            }
        }

        internal virtual void ForcePreRender()
        {
            this.PreRenderAction();
        }

        private bool rendered;

        protected virtual void PreRenderAction()
        {
            if (this.Visible && !this.rendered)
            {
                this.SetResources();

                this.rendered = true;
            }
        }

        protected virtual void SimpleRender(HtmlTextWriter writer)
        {
            this.PreRenderAction();

            if (this.IsLast)
            {
                if (!Ext.IsAjaxRequest)
                {
                    this.ScriptManager.BaseRenderAction();
                }
                this.ScriptManager.RenderAction(writer);
            }
        }

        void ICompositeControlDesignerAccessor.RecreateChildControls()
        {
            this.RecreateChildControls();
        }

        protected virtual void RecreateChildControls()
        {
            this.CreateChildControls();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.EnsureID();

            if (this is IContent)
            {
                this.Controls.Add(((IContent)this).BodyContainer);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            this.AddAttributesToRender(writer);
            writer.RenderBeginTag(this.DesignMode ? "div" : this is Layout ? "div:layout" : "div:container");
        }

        private bool preRendered;

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Visible && !this.preRendered)
            {
                if (this is IPostBackDataHandler && !this.IsMVC)
                {
                    this.Page.RegisterRequiresPostBack(this);
                }

                this.preRendered = true;
            }

            if (Ext.IsMicrosoftAjaxRequest)
            {
                if (this.AutoDataBind)
                {
                    this.DataBind();
                }

                this.PreRenderAction();
            }

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Visible)
            {
                bool isAsync = Ext.IsMicrosoftAjaxRequest;

                if (!isAsync)
                {
                    if (this.AutoDataBind)
                    {
                        this.DataBind();
                    }

                    this.PreRenderAction();
                }

                if (!(this is IVirtual))
                {
                    if (isAsync
                        && this.IsInUpdatePanelRefresh
                        && !(this is Layout)
                        && this is Observable
                        && !this.IsLazy)
                    {
                        this.Attributes.Add("class", "AsyncRender");
                    }

                    StringBuilder sb = new StringBuilder(256);
                    base.Render(new HtmlTextWriter(new StringWriter(sb)));

                    string html = sb.ToString().Trim();

                    html = Regex.Replace(html, "<div:layout id=\"[^\"]+\"[^<]*>|</div:layout>", "", RegexOptions.IgnoreCase);

                    if (!this.HasContent())
                    {
                        html = Regex.Replace(html, "<div:body id=\"[^\"]+_Body\"[^<]*>|</div:body>", "", RegexOptions.IgnoreCase);
                    }

                    if (this.IsLazy || (this.RemoveContainer && !Ext.IsMicrosoftAjaxRequest))
                    {
                        html = Regex.Replace(html, "<div:container id=\"[^\"]+\"[^<]*>|</div:container>", "", RegexOptions.IgnoreCase); ;
                    }

                    writer.Write(html
                                 .Replace("div:body", "div")
                                 .Replace("div:container", "div")
                                 .Replace(string.Format("id=\"{0}\"", this.ClientID), string.Format("id=\"{0}\"", this.ContainerID)));
                }
            }

            if (this.IsLast)
            {
                if (!Ext.IsAjaxRequest)
                {
                    this.ScriptManager.BaseRenderAction();
                }

                this.ScriptManager.RenderAction(writer);
            }
        }
    }
}