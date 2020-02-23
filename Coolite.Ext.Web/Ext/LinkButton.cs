/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [Xtype("linkbutton")]
    [InstanceOf(ClassName = "Coolite.Ext.LinkButton")]
    [ToolboxData("<{0}:LinkButton runat=\"server\" Text=\"LinkButton\" />")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.LinkButton), "Build.Resources.ToolboxIcons.LinkButton.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("Simple LinkButton class")]
    public class LinkButton : Button
    {
        // <summary>
        /// (optional) Set the CSS text-align property of the icon. The center is not supported. Defaults left.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(Alignment.Left)]
        [Description("(optional) Set the CSS text-align property of the icon. The center is not supported. Defaults to \"Left\"")]
        public virtual Alignment IconAlign
        {
            get
            {
                object obj = this.ViewState["IconAlign"];
                return (obj == null) ? Alignment.Left : (Alignment)obj;
            }
            set
            {
                this.ViewState["IconAlign"] = value;
            }
        }
    }
}
