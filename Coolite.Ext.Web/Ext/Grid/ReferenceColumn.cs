/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Web.UI;

namespace Coolite.Ext.Web
{
    class ReferenceColumn : ColumnBase, ICustomConfigSerialization
    {
        public ReferenceColumn() { }

        public ReferenceColumn(string reference)
        {
            this.Reference = reference;
        }

        public string Reference
        {
            get
            {
                object obj = this.ViewState["Reference"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Reference"] = value;
            }
        }

        public string Serialize(Control owner)
        {
            return this.Reference;
        }
    }
}
