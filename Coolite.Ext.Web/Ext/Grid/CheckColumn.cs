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
    public class CheckColumn : ColumnBase, ICustomConfigSerialization
    {
        [Category("Config Options")]
        [DefaultValue(false)]
        public virtual bool Editable
        {
            get
            {
                object obj = this.ViewState["Editable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Editable"] = value;
            }
        }

        public string Serialize(Control owner)
        {
            return string.Concat("new Ext.grid.CheckColumn(",new ClientConfig().Serialize(this,true),")");
        }
    }
}
