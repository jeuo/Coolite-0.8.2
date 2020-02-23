/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class ScriptContainerDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            if (this.Control.Parent != this.Control.Page.Header)
            {
                throw new Exception("Please place the &lt;cool:ScriptContainer&gt; into the &lt;head&gt; of this Page.");
            }
            return string.Empty;
        }
    }
}