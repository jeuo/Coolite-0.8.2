/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Configuration;
using System.Web.UI.Design;

namespace Coolite.Ext.Web
{
    public class WebConfigUtils
    {
        public static IDMode GetIDModeFromWebConfig(ISite site)
        {
            IDMode idMode = IDMode.Legacy;
            GlobalConfig coolite = GetCooliteSection(site);
            if (coolite != null)
            {
                idMode = coolite.IDMode;
            }
            return idMode;
        }

        public static InitScriptMode GetInitScriptModeFromWebConfig(ISite site)
        {
            InitScriptMode mode = InitScriptMode.Inline;
            GlobalConfig coolite = GetCooliteSection(site);
            if (coolite != null)
            {
                mode = coolite.InitScriptMode;
            }
            return mode;
        }

        public static Theme GetThemeFromWebConfig(ISite site)
        {
            Theme theme = Theme.Default;
            GlobalConfig config = GetCooliteSection(site);
            if (config != null)
            {
                theme = config.Theme;
            }
            return theme;
        }

        public static ScriptMode GetScriptModeFromWebConfig(ISite site)
        {
            ScriptMode mode = ScriptMode.Release;
            GlobalConfig config = GetCooliteSection(site);
            if (config != null)
            {
                mode = config.ScriptMode;
            }
            return mode;
        }

        public static ViewStateMode GetAjaxViewStateFromWebConfig(ISite site)
        {
            ViewStateMode mode = ViewStateMode.Default;
            GlobalConfig config = GetCooliteSection(site);
            if (config != null)
            {
                mode = config.AjaxViewStateMode;
            }
            return mode;
        }

        public static string GetLocaleFromWebConfig(ISite site)
        {
            string locale = "Invariant";
            GlobalConfig config = GetCooliteSection(site);
            if (config != null)
            {
                locale = config.Locale;
            }
            return locale;
        }

        public static ClientProxy GetAjaxMethodProxyFromWebConfig(ISite site)
        {
            ClientProxy mode = ClientProxy.Default;
            GlobalConfig config = GetCooliteSection(site);
            if (config != null)
            {
                mode = config.AjaxMethodProxy;
            }
            return mode;
        }

        public static ScriptAdapter GetScriptAdapterFromWebConfig(ISite site)
        {
            ScriptAdapter scriptAdapter = ScriptAdapter.Ext;
            GlobalConfig coolite = GetCooliteSection(site);
            if (coolite != null)
            {
                scriptAdapter = coolite.ScriptAdapter;
            }
            return scriptAdapter;
        }

        public static StateProvider GetStateProviderFromWebConfig(ISite site)
        {
            StateProvider stateProvider = StateProvider.PostBack;
            GlobalConfig coolite = GetCooliteSection(site);
            if (coolite != null)
            {
                stateProvider = coolite.StateProvider;
            }
            return stateProvider;
        }

        public static GlobalConfig GetCooliteSection(ISite site)
        {
            if (site != null)
            {
                IWebApplication app = (IWebApplication)site.GetService(typeof(IWebApplication));
                if (app != null)
                {
                    Configuration config = app.OpenWebConfiguration(false);

                    if (config != null)
                    {
                        ConfigurationSection section = config.GetSection("coolite");
                        if (section != null)
                        {
                            return section as GlobalConfig;
                        }
                    }
                }
            }
            return null;
        }
    }
}