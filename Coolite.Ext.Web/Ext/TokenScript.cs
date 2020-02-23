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

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [DefaultProperty("ScriptBlock")]
    [ParseChildren(true, "ScriptBlock")]
    [Designer(typeof(EmptyDesigner))]
    public class TokenScript : WebControl
    {
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        [Description("Script text")]
        [DefaultValue("")]
        public string ScriptBlock
        {
            get
            {
                return (string)this.ViewState["ScriptBlock"] ?? "";
            }
            set
            {
                this.ViewState["ScriptBlock"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if(!this.DesignMode)
            {
                this.Controls.Add(new LiteralControl(TokenUtils.ReplaceRawToken(TokenUtils.ParseTokens(this.ScriptBlock, this))));
            }

            base.OnPreRender(e);
        }
    }
}