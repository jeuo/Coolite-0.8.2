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
    public abstract class ListenerProperty : ListenerAction
    {
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string Value
        {
            get
            {
                return (string)this.ViewState["Value"] ?? "";
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        public override string GetScript()
        {
            if (this.IsDefault)
            {
                return "";
            }
            
            this.CheckControlTypeIsAcceptable();

            return string.Concat(this.Selector, ".", this.Name, "=", this.Value, ";");
        }
    }
}
