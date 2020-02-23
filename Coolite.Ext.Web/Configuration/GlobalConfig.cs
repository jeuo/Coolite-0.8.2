/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Configuration;

namespace Coolite.Ext.Web
{
    public class GlobalConfig : ConfigurationSection
    {
        private static GlobalConfig settings = ConfigurationManager.GetSection("coolite") as GlobalConfig;

        public static GlobalConfig Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new GlobalConfig();
                }
                return settings;
            }
        }

        [ConfigurationProperty("ajaxViewStateMode", DefaultValue = ViewStateMode.Default, IsRequired = false)]
        public ViewStateMode AjaxViewStateMode
        {
            get
            {
                return (ViewStateMode)this["ajaxViewStateMode"];
            }
        }

        [ConfigurationProperty("ajaxMethodProxy", DefaultValue = ClientProxy.Default, IsRequired = false)]
        public ClientProxy AjaxMethodProxy
        {
            get
            {
                return (ClientProxy)this["ajaxMethodProxy"];
            }
        }

        [ConfigurationProperty("idMode", DefaultValue = IDMode.Inherit, IsRequired = false)]
        public IDMode IDMode
        {
            get
            {
                return (IDMode)this["idMode"];
            }
        }

        [ConfigurationProperty("initScriptMode", DefaultValue = InitScriptMode.Inline, IsRequired = false)]
        public InitScriptMode InitScriptMode
        {
            get
            {
                return (InitScriptMode)this["initScriptMode"];
            }
        }

        [ConfigurationProperty("gzip", DefaultValue = true, IsRequired = false)]
        public bool GZip
        {
            get
            {
                return (bool)this["gzip"];
            }
        }

        [ConfigurationProperty("cleanResourceUrl", DefaultValue = true, IsRequired = false)]
        public bool CleanResourceUrl
        {
            get
            {
                return (bool)this["cleanResourceUrl"];
            }
        }

        [ConfigurationProperty("clientInitAjaxMethods", DefaultValue = false, IsRequired = false)]
        public bool ClientInitAjaxMethods
        {
            get
            {
                return (bool)this["clientInitAjaxMethods"];
            }
        }

        [ConfigurationProperty("scriptAdapter", DefaultValue = ScriptAdapter.Ext, IsRequired = false)]
        public ScriptAdapter ScriptAdapter
        {
            get
            {
                return (ScriptAdapter)this["scriptAdapter"];
            }
        }

        [ConfigurationProperty("renderScripts", DefaultValue = ResourceLocationType.Embedded, IsRequired = false)]
        public ResourceLocationType RenderScripts
        {
            get
            {
                return (ResourceLocationType)this["renderScripts"];
            }
        }

        [ConfigurationProperty("renderStyles", DefaultValue = ResourceLocationType.Embedded, IsRequired = false)]
        public ResourceLocationType RenderStyles
        {
            get
            {
                return (ResourceLocationType)this["renderStyles"];
            }
        }

        [ConfigurationProperty("resourcePath", DefaultValue = "~/Coolite/", IsRequired = false)]
        public string ResourcePath
        {
            get
            {
                return (string)this["resourcePath"];
            }
        }

        [ConfigurationProperty("scriptMode", DefaultValue = ScriptMode.Release, IsRequired = false)]
        public ScriptMode ScriptMode
        {
            get
            {
                return (ScriptMode)this["scriptMode"];
            }
        }

        [ConfigurationProperty("sourceFormatting", DefaultValue = false, IsRequired = false)]
        public bool SourceFormatting
        {
            get
            {
                return (bool)this["sourceFormatting"];
            }
        }

        [ConfigurationProperty("stateProvider", DefaultValue = StateProvider.PostBack, IsRequired = false)]
        public StateProvider StateProvider
        {
            get
            {
                return (StateProvider)this["stateProvider"];
            }
        }

        [ConfigurationProperty("theme", DefaultValue = Theme.Default, IsRequired = false)]
        public Theme Theme
        {
            get
            {
                Theme theme = (Theme)this["theme"];
                return theme;
            }
        }

        [ConfigurationProperty("quickTips", DefaultValue = true, IsRequired = false)]
        public bool QuickTips
        {
            get
            {
                return (bool)this["quickTips"];
            }
        }

        [ConfigurationProperty("ajaxEventUrl", DefaultValue = "", IsRequired = false)]
        public string AjaxEventUrl
        {
            get
            {
                return (string)this["ajaxEventUrl"];
            }
        }

        [ConfigurationProperty("locale", DefaultValue = "", IsRequired = false)]
        public string Locale
        {
            get
            {
                return (string)this["locale"];
            }
        }

        [ConfigurationProperty("ajaxMethodNamespace", DefaultValue = "Coolite.AjaxMethods", IsRequired = false)]
        public string AjaxMethodNamespace
        {
            get
            {
                return (string)this["ajaxMethodNamespace"];
            }
        }

        [ConfigurationProperty("removeViewState", DefaultValue = false, IsRequired = false)]
        public bool RemoveViewState
        {
            get
            {
                return (bool)this["removeViewState"];
            }
        }

        [ConfigurationProperty("rethrowAjaxExceptions", DefaultValue = false, IsRequired = false)]
        public bool RethrowAjaxExceptions
        {
            get
            {
                return (bool)this["rethrowAjaxExceptions"];
            }
        }

        //[ConfigurationProperty("settings")]
        //public CooliteConfigurationElementCollection Settings
        //{
        //    get
        //    {
        //        return (CooliteConfigurationElementCollection)this["settings"];
        //    }
        //}
    }

    //public class CooliteConfigurationElement : ConfigurationElement
    //{
    //    [ConfigurationProperty("key", IsRequired = true)]
    //    public string Key
    //    {
    //        get
    //        {
    //            return this["key"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("value", IsRequired = false)]
    //    public string Value
    //    {
    //        get
    //        {
    //            return this["value"] as string;
    //        }
    //    }
    //}

    //public class CooliteConfigurationElementCollection : ConfigurationElementCollection
    //{
    //    public CooliteConfigurationElement this[int index]
    //    {
    //        get
    //        {
    //            return (CooliteConfigurationElement)base.BaseGet(index);
    //        }
    //        set
    //        {
    //            if (base.BaseGet(index) != null)
    //            {
    //                base.BaseRemoveAt(index);
    //            }
    //            this.BaseAdd(index, value);
    //        }
    //    }

    //    public CooliteConfigurationElement this[string name]
    //    {
    //        get
    //        {
    //            object value = base.BaseGet(name);
    //            return (value == null) ? null : (CooliteConfigurationElement)value;

    //            //return (CooliteConfigurationElement)base.BaseGet(name);
    //        }
    //        set
    //        {
    //            if (base.BaseGet(name) != null)
    //            {
    //                base.BaseRemove(name);
    //            }
    //            this.BaseAdd(value);
    //        }
    //    }

    //    protected override ConfigurationElement CreateNewElement()
    //    {
    //        return new CooliteConfigurationElement();
    //    }

    //    protected override object GetElementKey(ConfigurationElement element)
    //    {
    //        return ((CooliteConfigurationElement)element).Key;
    //    }
    //}
}