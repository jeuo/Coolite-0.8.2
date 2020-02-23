/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;

namespace Coolite.Ext.Web
{
    public class AfterAjaxEventArgs : EventArgs
    {
        private Response ajaxResponse;

        public AfterAjaxEventArgs() { }

        public AfterAjaxEventArgs(Response ajaxResponse)
        {
            this.ajaxResponse = ajaxResponse;
        }

        public Response Response
        {
            get
            {
                if(ajaxResponse == null)
                {
                    ajaxResponse = new Response(true);
                }

                return ajaxResponse;
            }
        }
    }
}