/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.ComponentModel.Design;

namespace Coolite.Ext.Web
{
    public class EmptyDesigner : WebControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            return base.CreatePlaceHolderDesignTimeHtml(this.Message);
        }

        private string Message
        {
            get
            {
                return @"<table style=""margin: 8px;"">
                    <tr>
                        <td style=""white-space: nowrap;padding-right:8px;"">Please configure in Source View.</td>
                    </tr>
                </table>";
            }
        }

        public override bool AllowResize
        {
            get
            {
                return false;
            }
        }
    }
}