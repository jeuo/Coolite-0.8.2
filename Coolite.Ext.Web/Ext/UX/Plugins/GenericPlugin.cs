/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [ToolboxData("<{0}:GenericPlugin runat=\"server\" />")]
    [Description("A generic Plugin.")]
    public class GenericPlugin : Plugin
    {
        public GenericPlugin() { }

        public GenericPlugin(string instanceOf, string path)
        {
            this.InstanceOf = instanceOf;
            this.Path = path;
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The JavaScript class name. Used to create a 'new' instance of the object.")]
        [NotifyParentProperty(true)]
        new public virtual string InstanceOf
        {
            get
            {
                return (string)this.ViewState["InstanceOf"] ?? "";
            }
            set
            {
                this.ViewState["InstanceOf"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The file path to the required JavaScript file.")]
        [NotifyParentProperty(true)]
        public virtual string Path
        {
            get
            {
                return (string)this.ViewState["Path"] ?? "";
            }
            set
            {
                this.ViewState["Path"] = value;
            }
        }
    }
}