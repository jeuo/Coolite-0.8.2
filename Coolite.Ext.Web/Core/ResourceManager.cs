/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.Design;
using System.Xml;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class InitializationScriptNotFoundException : NullReferenceException
    {
        public InitializationScriptNotFoundException(string errorMessage)
            : base(errorMessage) { }

        public InitializationScriptNotFoundException(string errorMessage, Exception inner)
            : base(errorMessage, inner) { }
    }

    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    internal class ResourceManager : Page, IHttpHandler, IRequiresSessionState
    {
        Stream stream;
        ScriptManager sm;
        StringBuilder sb;
        HttpContext context;
        string webResource;
        byte[] image;
        byte[] output;
        int length;

        [FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
        private static long GetAssemblyTime(Assembly assembly)
        {
            AssemblyName assemblyName = assembly.GetName();
            return File.GetLastWriteTime(new Uri(assemblyName.CodeBase).LocalPath).Ticks;
        } 

        public override void ProcessRequest(HttpContext context)
        {
            this.context = context;
            
            string file = this.context.Request.RawUrl;

            if (file.Contains("coolite/coolite-init-js/coolite.axd?"))
            {
                string key = StringUtils.RightOfRightmostOf(file, '?');

                if (!string.IsNullOrEmpty(key))
                {
                    try
                    {
                        string script = this.context.Session[key].ToString();
                        this.context.Session.Remove(key);
                        CompressionUtils.GZipAndSend(script);
                    }
                    catch (NullReferenceException)
                    {
                        throw new InitializationScriptNotFoundException("The Coolite initialization script was not found.");
                    }
                }
            }
            else
            {
                this.SetResponseCache(context);

                this.sm = new ScriptManager();

                this.SetWebResourceName(file);

                if (CompressionUtils.IsGZipSupported && this.sm.GZip)
                {
                    try
                    {
                        this.stream = this.GetType().Assembly.GetManifestResourceStream(this.webResource);

                        switch (StringUtils.RightOfRightmostOf(this.webResource, '.'))
                        {
                            case "js":
                                this.WriteFile("text/javascript");
                                break;
                            case "css":
                                this.WriteFile("text/css");
                                break;
                            case "gif":
                                this.WriteImage("image/gif");
                                break;
                            case "png":
                                this.WriteImage("image/png");
                                break;
                            case "jpg":
                            case "jpeg":
                                this.WriteImage("image/jpg");
                                break;
                        }
                    }
                    catch
                    {
                        this.context.Response.Redirect(Page.ClientScript.GetWebResourceUrl(this.sm.GetType(), this.webResource));
                    }
                    finally
                    {
                        if(this.stream != null)
                        {
                            this.stream.Close();
                        }
                    }
                }
                else
                {
                    this.context.Response.Redirect(Page.ClientScript.GetWebResourceUrl(this.sm.GetType(), this.webResource));
                }
            }
        }

        private void SetResponseCache(HttpContext context)
        {
            HttpCachePolicy cache = context.Response.Cache;

            cache.SetLastModified(new DateTime(ResourceManager.GetAssemblyTime(typeof(ResourceManager).Assembly)));
            cache.SetOmitVaryStar(true);
            cache.SetVaryByCustom("v");
            cache.SetExpires(DateTime.UtcNow.AddYears(1));
            cache.SetMaxAge(TimeSpan.FromDays(365));
            cache.SetValidUntilExpires(true);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetCacheability(HttpCacheability.Public);
        }

        private void WriteFile(string responseType)
        {
            this.output = this.GetCache();

            if (this.output != null)
            {
                CompressionUtils.Send(this.output, responseType);
                return;
            }

            this.sb = new StringBuilder(4096);

            using (this.stream)
            {
                StreamReader reader = new StreamReader(this.stream);
                this.sb.Append(reader.ReadToEnd());
                reader.Close();
            }

            byte[] gzip;

            if (responseType == "text/css")
            {
                gzip = CompressionUtils.GZip(this.sm.ParseCssWebResourceUrls(this.sb.ToString()));
            }
            else
            {
                gzip = CompressionUtils.GZip(this.sb.ToString());
            }

            this.SetCache(gzip);

            CompressionUtils.Send(gzip, responseType);
        }

        private void WriteImage(string responseType)
        {
            this.output = this.GetCache();

            if (this.output != null)
            {
                CompressionUtils.Send(this.output, responseType);
                return;
            }

            this.length = Convert.ToInt32(this.stream.Length);
            this.image = new Byte[this.length];
            this.stream.Read(this.image, 0, this.length);

            byte[] gzip = CompressionUtils.GZip(this.image);

            this.SetCache(gzip);

            CompressionUtils.Send(gzip, responseType);
        }

        private byte[] GetCache()
        {
            return this.context.Cache[this.webResource] as byte[];
        }

        private void SetCache(byte[] output)
        {
            this.context.Cache.Insert(this.webResource, output, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromDays(30));
        }     
        
        private void SetWebResourceName(string filePath)
        {
            this.webResource = StringUtils.ReplaceLastInstanceOf(StringUtils.RightOf(StringUtils.LeftOfRightmostOf(filePath, "/coolite.axd"), this.sm.ApplicationName).Insert(0, ScriptManager.ASSEMBLYSLUG).Replace('/', '.'), "-", ".");
        }

        new public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public static bool HasModule()
        {
            if (HttpContext.Current.Application["HasModule"] != null)
            {
                return (bool)HttpContext.Current.Application["HasModule"];
            }

            bool result = false;
            string path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config");
            XmlTextReader reader = new XmlTextReader(new StreamReader(path));

            try
            {
                if (reader.ReadToFollowing("httpModules"))
                {
                    if (reader.ReadInnerXml().Contains("type=\"Coolite.Ext.Web.AjaxRequestModule"))
                    {
                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                reader.Close();
            }

            HttpContext.Current.Application["HasModule"] = result;
            return result;
        }

        public static bool HasHandler()
        {
            if (HttpContext.Current.Application["HasHandler"] != null)
            {
                return (bool)HttpContext.Current.Application["HasHandler"];
            }

            bool result = false;
            string path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config");
            XmlTextReader reader = new XmlTextReader(new StreamReader(path));

            try
            {
                if (reader.ReadToFollowing("httpHandlers"))
                {
                    if (reader.ReadInnerXml().Contains("type=\"Coolite.Ext.Web.ResourceManager\""))
                    {
                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                reader.Close();
            }

            HttpContext.Current.Application["HasHandler"] = result;
            return result;
        }

        internal static void CheckConfiguration(ISite site)
        {
            if (site == null)
            {
                return;
            }

            IWebApplication app = (IWebApplication)site.GetService(typeof(IWebApplication));

            if (app == null)
            {
                return;
            }

            Configuration config = app.OpenWebConfiguration(false);

            HttpHandlersSection handlers = (HttpHandlersSection)config.GetSection("system.web/httpHandlers");

            // Does the httpHandlers Secton already exist?
            if (handlers == null)
            {
                // If not, add it...
                handlers = new HttpHandlersSection();

                ConfigurationSectionGroup group = config.GetSectionGroup("system.web");

                // Does the system.web Section already exist?
                if (group == null)
                {
                    // If not, add it...
                    config.SectionGroups.Add("system.web", new ConfigurationSectionGroup());
                    group = config.GetSectionGroup("system.web");
                }

                if (group != null)
                {
                    group.Sections.Add("httpHandlers", handlers);
                }
            }

            HttpHandlerAction action = new HttpHandlerAction("*/coolite.axd", "Coolite.Ext.Web.ResourceManager", "*", false);

            // Does the ResourceManager already exist?
            if (handlers.Handlers.IndexOf(action) < 0)
            {
                // If not, add it...
                handlers.Handlers.Add(action);
                config.Save();
            }



            HttpModulesSection modules = (HttpModulesSection)config.GetSection("system.web/httpModules");

            // Does the httpModules Secton already exist?
            if (modules == null)
            {
                // If not, add it...
                modules = new HttpModulesSection();

                ConfigurationSectionGroup group = config.GetSectionGroup("system.web");

                // Does the system.web Section already exist?
                if (group == null)
                {
                    // If not, add it...
                    config.SectionGroups.Add("system.web", new ConfigurationSectionGroup());
                    group = config.GetSectionGroup("system.web");
                }

                if (group != null)
                {
                    group.Sections.Add("httpModules", modules);
                }
            }


            //<add name="AjaxRequestModule" type="Coolite.Ext.Web.AjaxRequestModule, Coolite.Ext.Web" />

            HttpModuleAction action2 = new HttpModuleAction("AjaxRequestModule", "Coolite.Ext.Web.AjaxRequestModule, Coolite.Ext.Web");

            // Does the ResourceManager already exist?
            if (modules.Modules.IndexOf(action2) < 0)
            {
                // If not, add it...
                modules.Modules.Add(action2);
                config.Save();
            }
        }
    }
}