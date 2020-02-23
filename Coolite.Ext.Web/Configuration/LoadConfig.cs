/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [Description("The only required property is url. The optional properties nocache, text and scripts are shorthand for disableCaching, indicatorText and loadScripts and are used to set their associated property on this panel Updater instance.")]
    public class LoadConfig : StateManagedItem
    {
        public LoadConfig() { }

        public LoadConfig(string url)
        {
            this.Url = url;
        }

        public LoadConfig(string url, LoadMode mode)
        {
            this.Url = url;
            this.Mode = mode;
        }

        public LoadConfig(string url, LoadMode mode, bool noCache)
        {
            this.Url = url;
            this.Mode = mode;
            this.NoCache = noCache;
        }

        public virtual string ToJsonString()
        {
            return new ClientConfig().Serialize(this);
        }

        public override bool IsDefault
        {
            get
            {
                return (string.IsNullOrEmpty(this.Url)
                    && this.Mode == LoadMode.Merge
                    && string.IsNullOrEmpty(this.Callback)
                    && string.IsNullOrEmpty(this.Scope)
                    && !this.ShowMask);
            }
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Url
        {
            get
            {
                return (string)this.ViewState["Url"] ?? "";
            }
            set
            {
                this.ViewState["Url"] = value;
            }
        }

        [ClientConfig("url")]
        [DefaultValue("")]
        internal virtual string UrlProxy
        {
            get
            {
                string url = this.Url;

                if (string.IsNullOrEmpty(url))
                {
                    return "";
                }

                return (Coolite.Utilities.UrlUtils.IsUrl(url) || this.Owner == null || this.Owner.Page == null || TokenUtils.IsRawToken(url)) ? url : this.Owner.Page.ResolveUrl(url);
            }
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Callback
        {
            get
            {
                return (string)this.ViewState["Callback"] ?? "";
            }
            set
            {
                this.ViewState["Callback"] = value;
            }
        }

        [ClientConfig("callback", JsonMode.Raw)]
        [DefaultValue("")]
        internal string CallbackProxy
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Callback))
                {
                    return new JFunction(TokenUtils.ParseTokens(this.Callback), "el", "success", "response", "options").ToString();
                }
                return "";
            }
        }

        //el : Ext.Element
        //The Element being updated.
        //success : Boolean
        //True for success, false for failure.
        //response : XMLHttpRequest
        //The XMLHttpRequest which processed the update.
        //options : Object
        //The config object passed to the update call.

        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(LoadMode.Merge)]
        [NotifyParentProperty(true)]
        public virtual LoadMode Mode
        {
            get
            {
                object obj = this.ViewState["Mode"];
                return (obj == null) ? LoadMode.Merge : (LoadMode)obj;
            }
            set
            {
                this.ViewState["Mode"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Scope
        {
            get
            {
                return (string)this.ViewState["Scope"] ?? "";
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool DiscardUrl
        {
            get
            {
                object obj = this.ViewState["DiscardUrl"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DiscardUrl"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool PassParentSize
        {
            get
            {
                object obj = this.ViewState["PassParentSize"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["PassParentSize"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Event which triggers loading process. Default value is render")]
        public virtual string TriggerEvent
        {
            get
            {
                return ((string)this.ViewState["TriggerEvent"] ?? "").ToLowerInvariant();
            }
            set
            {
                this.ViewState["TriggerEvent"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("IFrame css style class")]
        public virtual string Cls
        {
            get
            {
                return (string)this.ViewState["Cls"] ?? "";
            }
            set
            {
                this.ViewState["Cls"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("If true then loading performs after reload function calling.")]
        public virtual bool ManuallyTriggered
        {
            get
            {
                object obj = this.ViewState["ManuallyTriggered"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ManuallyTriggered"] = value;
            }
        }


        [ClientConfig]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("Reload content on each show event.")]
        public virtual bool ReloadOnEvent
        {
            get
            {
                object obj = this.ViewState["ReloadOnEvent"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ReloadOnEvent"] = value;
            }
        }

        [ClientConfig("nocache")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool NoCache
        {
            get
            {
                object obj = this.ViewState["NoCache"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["NoCache"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        public virtual bool Scripts
        {
            get
            {
                object obj = this.ViewState["Scripts"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Scripts"] = value;
            }
        }

        [ClientConfig("scripts")]
        [DefaultValue(false)]
        internal bool ScriptsProxy
        {
            get
            {
                return this.Scripts;
            }
        }

        [ClientConfig]
        [DefaultValue(0)]
        [NotifyParentProperty(true)]
        public virtual int Timeout
        {
            get
            {
                object obj = this.ViewState["Timeout"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Timeout"] = value;
            }
        }

        private ParameterCollection extraParams;

        [ClientConfig(JsonMode.ArrayToObject)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ParameterCollection Params
        {
            get
            {
                if (this.extraParams == null)
                {
                    this.extraParams = new ParameterCollection();
                    this.extraParams.Owner = this.Owner;
                }
                return this.extraParams;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to show the mask while iframe loading.")]
        public virtual bool ShowMask
        {
            get
            {
                object obj = this.ViewState["ShowMask"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ShowMask"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("The IFrame LoadMask message.")]
        public virtual string MaskMsg
        {
            get
            {
                return (string)this.ViewState["MaskMsg"] ?? "";
            }
            set
            {
                this.ViewState["MaskMsg"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Localizable(true)]
        [Description("The IFrame LoadMask css class.")]
        public virtual string MaskCls
        {
            get
            {
                return (string)this.ViewState["MaskCls"] ?? "";
            }
            set
            {
                this.ViewState["MaskCls"] = value;
            }
        }
    }

    public enum LoadMode
    {
        IFrame,
        Merge
    }
}