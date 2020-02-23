/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Web;

namespace Coolite.Ext.Web
{
    public class AjaxMethodHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        protected static bool IsDebugging
        {
            get
            {
                bool result = false;
                if (HttpContext.Current != null)
                {
                    result = HttpContext.Current.IsDebuggingEnabled;
                }

                return result;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            AjaxResponse responseObject = new AjaxResponse(true);

            try
            {
                HandlerMethods handler = HandlerMethods.GetHandlerMethods(context, context.Request.FilePath);
                string methodName = HandlerMethods.GetMethodName(context);

                if (handler == null)
                {
                    throw new Exception(string.Format("The Method '{0}' has not been defined.", context.Request.FilePath));
                }

                if (string.IsNullOrEmpty(methodName))
                {
                    throw new Exception("No methodName has been set in the configuration.");
                }

                AjaxMethod ajaxMethod = handler.GetStaticMethod(methodName);

                if (ajaxMethod == null)
                {
                    throw new Exception(string.Format("The static AjaxMethod '{0}' has not been defined.", methodName));
                }

                responseObject.Result = ajaxMethod.Invoke();
            }
            catch (Exception e)
            {
                responseObject.Success = false;
                responseObject.ErrorMessage = IsDebugging ? e.ToString() : e.Message; 
            }

            context.Response.Cache.SetNoServerCaching();
            context.Response.Cache.SetMaxAge(TimeSpan.Zero);
            context.Response.Write(responseObject.ToString());
            context.Response.End();
        }
    }
}