/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public abstract class Adapter : BaseMenuItem
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("True if this item can be visually activated (defaults to false).")]
        public override bool CanActivate
        {
            get
            {
                object obj = this.ViewState["CanActivate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["CanActivate"] = value;
            }
        }
    }
}