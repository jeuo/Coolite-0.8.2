/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.IO;
using System.Net;
using System.Web;

namespace Coolite.Ext.Web
{
    public class AjaxRequestModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.PostAcquireRequestState += OnPostAcquireRequestState;
            app.PreSendRequestHeaders += RedirectPreSendRequestHeaders;
            app.ReleaseRequestState += AjaxRequestFilter;
        }

        void Application_Error(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpContext context = app.Context;

            if (Ext.HasXCooliteHeader(context.Request))
            {
                AjaxResponse responseObject = new AjaxResponse(true);
                string error = null;
                if (HttpContext.Current != null)
                {
                    error = HttpContext.Current.Error != null ? HttpContext.Current.Error.ToString() : null;    
                }
                
                if (!ScriptManager.AjaxSuccess || !string.IsNullOrEmpty(error))
                {
                    responseObject.Success = false;
                    if (!string.IsNullOrEmpty(error))
                    {
                        responseObject.ErrorMessage = error;
                    }
                    else
                    {
                        responseObject.ErrorMessage = ScriptManager.AjaxErrorMessage;
                    }
                }

                app.Context.Response.Clear();
                app.Context.Response.ClearContent();
                app.Context.Response.ClearHeaders();
                app.Context.Response.StatusCode = (int)HttpStatusCode.OK;
                app.Context.Response.Write(responseObject.ToString());
                app.Context.Response.End();
                app.CompleteRequest();
            }
        }

        private void OnPostAcquireRequestState(object sender, EventArgs eventArgs)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpRequest request = app.Context.Request;

            if (Ext.IsAjaxRequest)
            {
                if (AjaxMethod.IsStaticMethodRequest(request) || Coolite.Utilities.ReflectionUtils.IsTypeOf(app.Context.Handler, "System.Web.Script.Services.ScriptHandlerFactory+HandlerWrapper"))
                {
                    this.ProcessRequest(app, request);
                }
            }
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

        private void ProcessRequest(HttpApplication app, HttpRequest request)
        {
            AjaxResponse responseObject = new AjaxResponse(true);
            try
            {
                HttpContext context = HttpContext.Current;
                HandlerMethods handler = HandlerMethods.GetHandlerMethods(context, request.FilePath);
                string methodName = HandlerMethods.GetMethodName(context);

                if (handler == null)
                {
                    throw new Exception(string.Format("The Method '{0}' has not been defined.", request.FilePath));
                }

                if (string.IsNullOrEmpty(methodName))
                {
                    return;
                    throw new Exception("No methodName has been set in the configuration.");
                }

                AjaxMethod ajaxMethod = handler.GetStaticMethod(methodName);

                if (ajaxMethod == null)
                {
                    throw new Exception(string.Format("The static AjaxMethod '{0}' has not been defined.", ajaxMethod));
                }

                object result = ajaxMethod.Invoke();
                
                if(!ScriptManager.AjaxSuccess)
                {
                    responseObject.Success = false;
                    responseObject.ErrorMessage = ScriptManager.AjaxErrorMessage;
                }
                else
                {
                    responseObject.Result = result;
                }
            }
            catch (Exception e)
            {
                responseObject.Success = false;
                responseObject.ErrorMessage = IsDebugging ? e.ToString() : e.Message;
            }

            app.Context.Response.Clear();
            app.Context.Response.ClearContent();
            app.Context.Response.ClearHeaders();
            app.Context.Response.StatusCode = 200;
            app.Context.Response.ContentType = "application/json";
            app.Context.Response.Charset = "utf-8";
            app.Context.Response.Cache.SetNoServerCaching();
            app.Context.Response.Cache.SetMaxAge(TimeSpan.Zero);
            app.Context.Response.Write(responseObject.ToString());
            app.CompleteRequest();
        }

        private void AjaxRequestFilter(object sender, EventArgs e)
        {
            if (HttpContext.Current == null || HttpContext.Current.Response == null)
            {
                return;
            }

            HttpResponse response = HttpContext.Current.Response;

            if (Ext.IsAjaxRequest)
            {
                //try
                //{
                //    string attach = response.Headers["Content-Disposition"];
                    
                //    if (attach != null && attach.StartsWith("attachment;", StringComparison.InvariantCultureIgnoreCase))
                //    {
                //        return;
                //    }    
                //}
                //catch (PlatformNotSupportedException)
                //{
                //}
                
                if (response.ContentType.Equals("text/html", StringComparison.InvariantCultureIgnoreCase))
                {
                    response.Filter = new AjaxRequestFilter(response.Filter);
                }
            }
            else
            {
                object marker = HttpContext.Current.Items[ScriptManager.FilterMarker];

                if (marker != null && (bool)marker)
                {
                    if (!string.IsNullOrEmpty(response.ContentType) && response.ContentType.Equals("text/html", StringComparison.InvariantCultureIgnoreCase))
                    {
                        response.Filter = new InitScriptFilter(response.Filter);
                    }
                }
            }
        }

        private void RedirectPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpContext context = app.Context;

            if (context.Response.StatusCode == 302)
            {
                if (Ext.HasXCooliteHeader(context.Request) || Ext.HasInputFieldMarker(context.Request))
                {
                    string url = context.Response.RedirectLocation;
                    context.Response.StatusCode = 200;
                    context.Response.SuppressContent = false;
                    //context.Response.ContentType = "application/json";
                    context.Response.ContentType = "text/html";
                    context.Response.Charset = "utf-8";
                    context.Response.ClearContent();

                    AjaxResponse responseObject = new AjaxResponse(true);

                    responseObject.Script = string.Concat("window.location=\"", url, "\";");

                    TextWriter writer = context.Response.Output;
                    writer.Write(responseObject.ToString());
                }
            }
        }

        public void Dispose() { }
    }
}