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
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public abstract class InnerObservable : Observable
    {
        [ClientConfig("proxyId")]
        protected override string ClientIDProxy
        {
            get
            {
                return base.ClientIDProxy;
            }
        }

        private bool scriptAdded;

        protected virtual bool NeedRender
        {
            get
            {
                return false;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Visible)
            {
                if (this.DesignMode || Ext.IsAjaxRequest)
                {
                    return;
                }

                if (this is IPostBackDataHandler && !this.IsMVC)
                {
                    this.Page.RegisterRequiresPostBack(this);
                }
            }
        }

        protected override void PreRenderAction()
        {
            if (this.Visible)
            {
                this.RegisterScript(false);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.NeedRender)
            {
                base.Render(writer);
            }
            else
            {
                this.SimpleRender(writer);
            }
        }

        internal void RegisterScript(bool inTop)
        {
            if (!this.scriptAdded && this.Visible)
            {
                if (Ext.IsMicrosoftAjaxRequest && !this.IsInUpdatePanelRefresh)
                {
                    return;
                }

                this.OnClientInit();

                if (this.ParentComponent == null || inTop)
                {
                    if (inTop)
                    {
                        this.ScriptManager.RegisterBeforeClientInitScript(this.GetClientConstructor());
                    }
                    else
                    {
                        this.ScriptManager.RegisterClientSpecialInitScript(this.ClientInitID, this.GetClientConstructor());
                    }
                }
                else
                {
                    Observable c = this;
                    Observable temp = null;

                    while (c.ParentComponent != null)
                    {
                        temp = c.ParentComponent;
                        if (temp != null && Ext.IsMicrosoftAjaxRequest && !temp.IsInUpdatePanelRefresh)
                        {
                            break;
                        }
                        c = c.ParentComponent;
                    }

                    if (c.IsLazy)
                    {
                        this.ScriptManager.RegisterClientSpecialInitScript(this.ClientInitID, this.GetClientConstructor());
                    }
                    else
                    {
                        c.AddBeforeClientInitScript(this.GetClientConstructor());
                    }
                }

                this.RegisterCustomScripts();
                this.scriptAdded = true;
            }
        }

        public void EnsureScriptRegistering(bool inTop)
        {
            this.RegisterScript(inTop);
        }

        protected override void OnClientInit()
        {
            this.ScriptManager.RegisterInitID(this);
            this.OnBeforeClientInitHandler();
            this.EnsureChildControls();
            this.OnAfterClientInitHandler();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.ParentComponent == null || (Ext.IsMicrosoftAjaxRequest && !this.IsInUpdatePanelRefresh))
            {
                return;
            }

            Observable c = this;
            Observable temp = null;

            while (c.ParentComponent != null)
            {
                temp = c.ParentComponent;
                if (temp != null && Ext.IsMicrosoftAjaxRequest && !temp.IsInUpdatePanelRefresh)
                {
                    break;
                }
                c = c.ParentComponent;
            }

            c.BeforeClientInit += ParentComponentOrObservable_BeforeClientInit;
        }

        protected void ParentComponentOrObservable_BeforeClientInit(Observable sender)
        {
            this.RegisterScript(false);
        }
    }
}