/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class WebControlDesigner : System.Web.UI.Design.ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component); 
            this.control = (WebControl)component;
        }

        public override string GetDesignTimeHtml()
        {
            return base.GetDesignTimeHtml();
        }

        WebControl control;
        internal WebControl Control
        {
            get
            {
                return this.control;
            }
        }


        /*  Properties
            -----------------------------------------------------------------------------------------------*/

        public override bool AllowResize
        {
            get
            {
                return true;
            }
        }

        private WebControlDesigner currentDesigner;
        public WebControlDesigner CurrentDesigner
        {
            get
            {
                return currentDesigner??this;
            }
            set 
            {
                currentDesigner = value; 
            }
        }

        public string GetWebResourceUrl(string webResourceName)
        {
            IServiceProvider serviceProvider = this.Component.Site;
            string result = string.Empty;
            if (serviceProvider != null)
            {
                IResourceUrlGenerator urlGenerator =
                    (IResourceUrlGenerator)serviceProvider.GetService(typeof(IResourceUrlGenerator));
                if (urlGenerator != null)
                {
                    result = urlGenerator.GetResourceUrl(this.Component.GetType(), webResourceName);
                }
            }
            return result;
        }

        List<Icon> icons = new List<Icon>();

        public void AddIcon(Icon icon)
        {
            if (icon != Icon.None && !this.CurrentDesigner.icons.Contains(icon))
            {
                this.CurrentDesigner.icons.Add(icon);
            }
        }

        public string GetIconStyleBlock()
        {
            try
            {
                if (this.icons.Count > 0)
                {
                    StringBuilder iconlist = new StringBuilder(128);

                    foreach (Icon icon in this.icons)
                    {
                        iconlist.Append(this.Control.ScriptManager.GetIconClass(icon));
                    }

                    return string.Format(ScriptManager.StyleBlockTemplate, iconlist.ToString());
                }
            }
            catch
            {
                // swallow Exception
            }
            return string.Empty;
        }
        
        /*  HTML
            -----------------------------------------------------------------------------------------------*/

        public virtual string HtmlBegin { get { return string.Empty; } }
        public virtual string HtmlEnd { get { return string.Empty; } }

        /*  ActionList
            -----------------------------------------------------------------------------------------------*/

        private DesignerActionListCollection actionLists;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new WebControlActionList(this.Component));
                }
                return actionLists;
            }
        }

        public string WriteLog(string message)
        {
            //string path = @"c:\Coolite.Designer.Log.txt";
            //FileUtils.WriteFile(path, message);
            return message;
        }

        public void Refresh()
        {
            try
            {
                IMenuCommandService menuService =
                    (IMenuCommandService)GetService(typeof(IMenuCommandService));
                if (menuService != null)
                {
                    CommandID r = new CommandID(new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}"), 189);
                    menuService.GlobalInvoke(r);
                }
            }
            catch { }
        }



        /*  Error Handeling
            -----------------------------------------------------------------------------------------------*/

        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            return base.GetDesignTimeHtml() + string.Format(ErrorMessageTemplate, e.Message, e.StackTrace);
        }

        private static string ErrorMessageTemplate
        {
            get
            {
return @"<br /><div class=""x-tip x-form-invalid-tip"" style=""position: autstarttop: auto; left:auto; visibility: visible; z-index: auto; border:0 none;display: block;"">
  <div class=""x-tip-tl"">
    <div class=""x-tip-tr"">
      <div class=""x-tip-tc"">
        <div class=""x-tip-header x-unselectable""><span class=""x-tip-header-text""></span></div>
      </div>
    </div>
  </div>
    <div class=""x-tip-bwrap"">
        <div class=""x-tip-ml"">
            <div class=""x-tip-mr"">
                <div class=""x-tip-mc"">
                    <div style=""height: auto; width: auto;"" class=""x-tip-body"">Oops! A <b>Coolite Toolkit</b> Desgin-Time Error has occured.<br />Error Message: {0}<br /><br />Stack Trace: <br /><pre>{1}</pre><br /></div>
                </div>
            </div>
        </div>
    </div>
    <div class=""x-tip-bl x-panel-nofooter"">
      <div class=""x-tip-br"">
        <div class=""x-tip-bc""></div>
      </div>
    </div>
  </div>
</div>";
            }
        }


        /*  Simple DesignTime Log
            -----------------------------------------------------------------------------------------------*/

        //StringBuilder log = null;

        //private string ReadLog()
        //{
        //    if (this.control.Trace)
        //    {
        //        return this.log.ToString();
        //    }
        //    return string.Empty;
        //}

        //private void WriteLog(string name, string value)
        //{
        //    if (this.control.Trace)
        //    {
        //        string template = "<div style=\"margin: 12px;font:normal 11px tahoma, arial, helvetica, sans-serif;color:#444;\"><h4>{0}</h4><code>{1}</code></div>";
        //        this.log.Append(string.Format(template, name, HttpUtility.HtmlAttributeEncode(value)));
        //    }
        //}
    }
}