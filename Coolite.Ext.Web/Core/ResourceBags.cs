/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Coolite.Ext.Web
{
    public partial class ScriptManager
    {
        /*  setValues
            -----------------------------------------------------------------------------------------------*/

        private List<object> setValuesBag = new List<object>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<object> SetValuesBag
        {
            get
            {
                return this.setValuesBag;
            }
        }

        public void SetValue(string name, string value)
        {
            this.setValuesBag.Add(string.Concat("[", JSON.Serialize(name), ",", value, "]"));
        }
        
        
        /*  PreClientInit
            -----------------------------------------------------------------------------------------------*/

        private List<string> scriptBeforeClientInitBag = new List<string>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> ScriptBeforeClientInitBag
        {
            get
            {
                return this.scriptBeforeClientInitBag;
            }
        }

        public void RegisterBeforeClientInitScript(string script)
        {
            this.scriptBeforeClientInitBag.Add(script);
        }


        /*  ScriptClientSpecialInitBag
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> scriptClientSpecialInitBag = new Dictionary<string, string>();

        public bool IsClientSpecialInitScriptRegistered(string key)
        {
            return this.scriptClientSpecialInitBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ScriptClientSpecialInitBag
        {
            get
            {
                return this.scriptClientSpecialInitBag;
            }
        }

        public void RegisterClientSpecialInitScript(string key, string script)
        {
            if (!this.IsClientSpecialInitScriptRegistered(key))
            {
                this.scriptClientSpecialInitBag.Add(key, script);
            }
        }


        /*  ClientInitScript
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> scriptClientInitBag = new Dictionary<string, string>();

        public bool IsClientInitScriptRegistered(string key)
        {
            return this.scriptClientInitBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ScriptClientInitBag
        {
            get
            {
                return this.scriptClientInitBag;
            }
        }

        public void RegisterClientInitScript(string key, string script)
        {
            if (!this.IsClientInitScriptRegistered(key))
            {
                this.scriptClientInitBag.Add(key, script);
            }
        }


        /*  PostClientInit
            -----------------------------------------------------------------------------------------------*/

        private List<string> scriptAfterClientInitBag = new List<string>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> ScriptAfterClientInitBag
        {
            get
            {
                return this.scriptAfterClientInitBag;
            }
        }

        public void RegisterAfterClientInitScript(string script)
        {
            this.scriptAfterClientInitBag.Add(script);
        }


        /*  onReady
            -----------------------------------------------------------------------------------------------*/
        private static long proxyScriptNumber;
        internal static long ScriptOrderNumber
        {
            get
            {
                return System.Threading.Interlocked.Increment(ref proxyScriptNumber);
            }
        }

        private readonly SortedList<long, string> scriptOnReadyBag = new SortedList<long, string>();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SortedList<long, string> ScriptOnReadyBag
        {
            get
            {
                return this.scriptOnReadyBag;
            }
        }

        public void RegisterOnReadyScript(string script)
        {
            this.RegisterOnReadyScript(ScriptManager.ScriptOrderNumber, script);
        }


        internal void RegisterOnReadyScript(long orderNumber, string script)
        {
            if (!string.IsNullOrEmpty(script))
            {
                this.scriptOnReadyBag.Add(orderNumber, script);
            }
        }


        /*  onWindowResize
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> scriptOnWindowResizeBag = new Dictionary<string, string>();

        public bool IsOnWindowResizeScriptRegistered(string key)
        {
            return this.scriptOnWindowResizeBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ScriptOnWindowResizeBag
        {
            get
            {
                return this.scriptOnWindowResizeBag;
            }
        }

        public void RegisterOnWindowResizeScript(string key, string script)
        {
            if (!this.IsOnWindowResizeScriptRegistered(key))
            {
                this.scriptOnWindowResizeBag.Add(key, script);
            }
        }


        /*  onTextResize
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> scriptOnTextResizeBag = new Dictionary<string, string>();

        public bool IsOnTextResizeScriptRegistered(string key)
        {
            return this.scriptOnTextResizeBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ScriptOnTextResizeBag
        {
            get
            {
                return this.scriptOnTextResizeBag;
            }
        }

        public void RegisterOnTextResizeScript(string key, string script)
        {
            if (!this.IsOnTextResizeScriptRegistered(key))
            {
                this.scriptOnTextResizeBag.Add(key, script);
            }
        }


        /*  JavaScript - Blocks
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientScriptBlock = new Dictionary<string, string>();

        public bool IsClientScriptBlockRegistered(string key)
        {
            return this.clientScriptBlock.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ClientScriptBlockBag
        {
            get
            {
                return this.clientScriptBlock;
            }
        }

        public void RegisterClientScriptBlock(string resourceName)
        {
            this.RegisterClientScriptBlock(resourceName, this.GetWebResourceAsString(resourceName));
        }

        public void RegisterClientScriptBlock(string key, string script)
        {
            if (!this.IsClientScriptBlockRegistered(key))
            {
                this.clientScriptBlock.Add(key, script);
            }
        }


        /*  JavaScript - Includes
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientScriptIncludeBag = new Dictionary<string, string>();

        public bool IsClientScriptIncludeRegistered(string key)
        {
            return (this.clientScriptIncludeBag.ContainsKey(key) || this.clientScriptIncludeInternalBag.ContainsKey(key));
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ClientScriptIncludeBag
        {
            get
            {
                return this.clientScriptIncludeBag;
            }
        }

        public void RegisterClientScriptInclude(string resourceName)
        {
            this.RegisterClientScriptInclude(this.GetType(), resourceName);
        }

        public void RegisterClientScriptInclude(Type type, string resourceName)
        {
            this.RegisterClientScriptInclude(resourceName, this.GetWebResourceUrl(type, resourceName));
        }

        public void RegisterClientScriptInclude(string key, string url)
        {
            if (!this.IsClientScriptIncludeRegistered(key))
            {
                this.clientScriptIncludeBag.Add(key, this.ResolveUrl(url));
            }
        }


        /*  JavaScript - Includes Internal
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientScriptIncludeInternalBag = new Dictionary<string, string>();

        internal bool IsClientScriptIncludeInternalRegistered(string key)
        {
            return (this.clientScriptIncludeInternalBag.ContainsKey(key) || this.clientScriptIncludeBag.ContainsKey(key));
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary<string, string> ClientScriptIncludeInternalBag
        {
            get
            {
                return this.clientScriptIncludeInternalBag;
            }
        }

        internal void RegisterClientScriptIncludeInternal(string resourceName)
        {
            this.RegisterClientScriptIncludeInternal(this.GetType(), resourceName);
        }

        internal void RegisterClientScriptIncludeInternal(Type type, string resourceName)
        {
            this.RegisterClientScriptIncludeInternal(resourceName, this.GetWebResourceUrl(type, resourceName));
        }

        internal void RegisterClientScriptIncludeInternal(string key, string url)
        {
            if (!this.IsClientScriptIncludeInternalRegistered(key))
            {
                this.clientScriptIncludeInternalBag.Add(key, this.ResolveUrl(url));
            }
        }


        /*  StyleSheet - Blocks
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientStyleBlockBag = new Dictionary<string, string>();

        internal bool IsClientStyleBlockRegistered(string key)
        {
            return this.clientStyleBlockBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ClientStyleBlockBag
        {
            get
            {
                return this.clientStyleBlockBag;
            }
        }

        public void RegisterClientStyleBlock(string resourceName)
        {
            this.RegisterClientStyleBlock(this.GetType(), resourceName);
        }

        public void RegisterClientStyleBlock(Type type, string resourceName)
        {
            this.RegisterClientStyleBlock(resourceName, this.GetWebResourceAsString(type, resourceName));
        }

        public void RegisterClientStyleBlock(string key, string styles)
        {
            if (!this.IsClientStyleBlockRegistered(key))
            {
                this.clientStyleBlockBag.Add(key, styles);
            }
        }


        /*  StyleSheet - Includes
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientStyleIncludeBag = new Dictionary<string, string>();

        internal bool IsClientStyleIncludeRegistered(string key)
        {
            return this.clientStyleIncludeBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, string> ClientStyleIncludeBag
        {
            get
            {
                return this.clientStyleIncludeBag;
            }
        }

        public void RegisterClientStyleInclude(string resourceName)
        {
            this.RegisterClientStyleInclude(this.GetType(), resourceName);
        }

        public void RegisterClientStyleInclude(Type type, string resourceName)
        {
            this.RegisterClientStyleInclude(resourceName, this.GetWebResourceUrl(type, resourceName));
        }

        public void RegisterClientStyleInclude(string key, string url)
        {
            if (!this.IsClientStyleIncludeRegistered(key))
            {
                this.clientStyleIncludeBag.Add(key, this.ResolveUrl(url));
            }
        }


        /*  StyleSheet - Includes Internal
            -----------------------------------------------------------------------------------------------*/

        private Dictionary<string, string> clientStyleIncludeInternalBag = new Dictionary<string, string>();
        private Dictionary<string, string> themeIncludeInternalBag = new Dictionary<string, string>();

        internal bool IsClientStyleIncludeInternalRegistered(string key)
        {
            return this.clientStyleIncludeInternalBag.ContainsKey(key);
        }

        internal bool IsThemeIncludeInternalRegistered(string key)
        {
            return this.themeIncludeInternalBag.ContainsKey(key);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary<string, string> ClientStyleIncludeInternalBag
        {
            get
            {
                return this.clientStyleIncludeInternalBag;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal Dictionary<string, string> ThemeIncludeInternalBag
        {
            get
            {
                return this.themeIncludeInternalBag;
            }
        }

        internal void RegisterClientStyleIncludeInternal(string resourceName)
        {
            this.RegisterClientStyleIncludeInternal(this.GetType(), resourceName);
        }

        internal void RegisterClientStyleIncludeInternal(Type type, string resourceName)
        {
            this.RegisterClientStyleIncludeInternal(resourceName, this.GetWebResourceUrl(type, resourceName));
        }

        internal void RegisterClientStyleIncludeInternal(string key, string url)
        {
            if (!this.IsClientStyleIncludeInternalRegistered(key))
            {
                this.clientStyleIncludeInternalBag.Add(key, this.ResolveUrl(url));
            }
        }

        internal void RegisterThemeIncludeInternal(Type type, string resourceName)
        {
            this.RegisterThemeIncludeInternal(resourceName, this.GetWebResourceUrl(type, resourceName));
        }

        internal void RegisterThemeIncludeInternal(string key, string url)
        {
            if (!this.IsThemeIncludeInternalRegistered(key))
            {
                this.themeIncludeInternalBag.Add(key, this.ResolveUrl(url));
            }
        }

        private static List<string> locales;

        internal static List<string> Locales
        {
            get
            {
                if (ScriptManager.locales == null)
                {
                    ScriptManager.locales = new List<string>("af bg ca cs da de el-GR en en-GB es fa fi fr fr-CA he hr hu id it ja ko lt lv mk nl no nn-NO pl pt pt-BR pt-PT ro ru sk sl sr sr-Cyrl-CS sv-SE th tr uk vi zh-CN zh-TW".Split(' '));
                }
                return ScriptManager.locales;
            }
        }

        private static List<CultureInfo> supportedCultures;

        public static List<CultureInfo> SupportedCultures
        {
            get
            {
                if (ScriptManager.supportedCultures == null)
                {
                    List<CultureInfo> cultures = new List<CultureInfo>(ScriptManager.Locales.Count);

                    foreach (string c in ScriptManager.Locales)
                    {
                        cultures.Add(new CultureInfo(c));
                    }

                    ScriptManager.supportedCultures = cultures;
                }

                return ScriptManager.supportedCultures;
            }
        }

        public static bool IsSupportedCulture(CultureInfo culture)
        {
            bool parent;
            return ScriptManager.IsSupportedCulture(culture, out parent);
        }

        public static bool IsSupportedCulture(CultureInfo culture, out bool isParentSupported)
        {
            string child = culture.ToString();
            string parent = (culture.IsNeutralCulture) ? culture.ToString() : culture.Parent.ToString();

            bool childSupport = ScriptManager.Locales.Contains(child);
            bool parentSupport = ScriptManager.Locales.Contains(parent);
            isParentSupported = !childSupport && parentSupport;
            return (childSupport || parentSupport);
        }

        public static bool IsSupportedCulture(string code)
        {
            bool parent;
            return ScriptManager.IsSupportedCulture(code, out parent);
        }

        public static bool IsSupportedCulture(string code, out bool isParentSupported)
        {
            isParentSupported = false;
            if (code == "Invariant")
            {
                return false;
            }
            try
            {
                return ScriptManager.IsSupportedCulture(new CultureInfo(code), out isParentSupported);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}