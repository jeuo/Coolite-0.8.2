/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web;

namespace Coolite.Ext.Web
{
    public partial class Ext
    {
        internal static void EnsureAjaxEvent()
        {
            if(!Ext.IsAjaxRequest)
            {
                throw new InvalidOperationException("This operation requires and AjaxRequest");
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool IsAjaxRequest
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    return Ext.HasXCooliteHeader(HttpContext.Current.Request) || Ext.HasInputFieldMarker(HttpContext.Current.Request);
                }
                return false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool IsMicrosoftAjaxRequest
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null)
                {
                    return Ext.HasXMicrosoftAjaxHeader(HttpContext.Current.Request);
                }
                return false;
            }
        }

        internal static bool HasInputFieldMarker(HttpRequest request)
        {
            if(request == null)
            {
                return false;    
            }

            string marker = request.Form["__CooliteAjaxEventMarker"];
            if (!string.IsNullOrEmpty(marker))
            {
                if (marker.ToLower().Contains("delta=true"))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool HasXCooliteHeader(HttpRequest request)
        {
            string[] values = request.Headers.GetValues("X-Coolite");
            if (values != null)
            {
                foreach (string value in values)
                {
                    if (value.ToLower().Contains("delta=true"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool HasXMicrosoftAjaxHeader(HttpRequest request)
        {
            string[] values = HttpContext.Current.Request.Headers.GetValues("X-MicrosoftAjax");
            if (values != null)
            {
                foreach (string value in values)
                {
                    if (value.ToLower().Contains("delta=true"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Redirect(string url)
        {
            Ext.Redirect(url, null, null);
        }

        public static void Redirect(string url, string msg)
        {
            Ext.Redirect(url, msg, null);
        }

        public static void Redirect(string url, string msg, string msgCls)
        {
            ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);

            if(sm == null)
            {
                throw new InvalidOperationException("The ScriptManager can not be found during the Ext.Redirect.");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url", "The redirection url is empty");
            }

            if(!string.IsNullOrEmpty(msg))
            {
                Mask.Show(new Mask.Config { 
                    Msg = msg,
                    MsgCls = msgCls
                });
            }

            sm.AddScript(string.Concat("window.location=\"", (Coolite.Utilities.UrlUtils.IsUrl(url) || sm.Page == null || TokenUtils.IsRawToken(url)) ? url : sm.Page.ResolveUrl(url), "\";"));
        }


        /*  User Agent Detection (browser)
            -----------------------------------------------------------------------------------------------*/

        public static bool IsOpera
        {
            get
            {
                return (HttpContext.Current != null && (HttpContext.Current.Request.UserAgent??"").ToLower().Contains("opera"));
            }
        }

        public static bool IsChrome
        {
            get
            {
                return (HttpContext.Current != null && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("chrome"));
            }
        }

        public static bool IsWebKit
        {
            get
            {
                return (HttpContext.Current != null && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("webkit"));
            }
        }

        public static bool IsSafari
        {
            get
            {
                return (HttpContext.Current != null && !Ext.IsChrome && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("safari"));
            }
        }

        public static bool IsSafari3
        {
            get
            {
                return (HttpContext.Current != null && Ext.IsSafari && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("version/3"));
            }
        }

        public static bool IsSafari4
        {
            get
            {
                return (HttpContext.Current != null && Ext.IsSafari && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("version/4"));
            }
        }

        public static bool IsIE
        {
            get
            {
                return (HttpContext.Current != null && !Ext.IsOpera && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("msie"));
            }
        }

        public static bool IsIE6
        {
            get
            {

                return (HttpContext.Current != null && Ext.IsIE && HttpContext.Current.Request.Browser.MajorVersion <= 6);
            }
        }

        public static bool IsIE7
        {
            get
            {
                return (HttpContext.Current != null && Ext.IsIE && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("msie 7"));
            }
        }

        public static bool IsIE8
        {
            get
            {
                return (HttpContext.Current != null && Ext.IsIE && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("msie 8"));
            }
        }

        public static bool IsGecko
        {
            get
            {
                return (HttpContext.Current != null && !Ext.IsWebKit && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("gecko"));
            }
        }

        public static bool IsGecko3
        {
            get
            {
                return (HttpContext.Current != null && Ext.IsGecko && (HttpContext.Current.Request.UserAgent ?? "").ToLower().Contains("rv:1.9"));
            }
        }


        /*  User Agent Detection (operating system)
            -----------------------------------------------------------------------------------------------*/

        public static bool IsWindows
        {
            get
            {
                string ua = (HttpContext.Current.Request.UserAgent ?? "").ToLower();
                return (HttpContext.Current != null && (ua.Contains("windows") || ua.Contains("win32")));
            }
        }

        public static bool IsMac
        {
            get
            {
                string ua = (HttpContext.Current.Request.UserAgent ?? "").ToLower();
                return (HttpContext.Current != null && (ua.Contains("macintosh") || ua.Contains("mac os x")));
            }
        }

        public static bool IsLinux
        {
            get
            {
                string ua = (HttpContext.Current.Request.UserAgent ?? "").ToLower();
                return (HttpContext.Current != null && ua.Contains("linux"));
            }
        }


        /*  Singletons 
            -----------------------------------------------------------------------------------------------*/

        public static Mask Mask
        {
            get
            {
                return Mask.Instance;
            }
        }

        public static MessageBox MessageBox
        {
            get
            {
                return MessageBox.Instance;
            }
        }

        public static MessageBox Msg
        {
            get
            {
                return Ext.MessageBox;
            }
        }

        public static QuickTips QuickTips
        {
            get
            {
                return QuickTips.Instance;
            }
        }

        public static WindowMgr WindowMgr
        {
            get
            {
                return WindowMgr.Instance;
            }
        }

        public static Notification Notification
        {
            get
            {
                return Notification.Instance;
            }
        }
    }
}