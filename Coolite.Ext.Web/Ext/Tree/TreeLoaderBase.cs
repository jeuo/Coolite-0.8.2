/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
   public abstract class TreeLoaderBase : StateManagedItem
    {
        private ParameterCollection baseAttrs;

        [ClientConfig("baseAttrs", JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("(optional) An object containing attributes to be added to all nodes created by this loader. If the attributes sent by the server have an attribute in this object, they take priority.")]
        [ViewStateMember]
        public virtual ParameterCollection BaseAttributes
        {
            get
            {
                if (this.baseAttrs == null)
                {
                    this.baseAttrs = new ParameterCollection();
                    this.baseAttrs.Owner = this.Owner;
                }

                return this.baseAttrs;
            }
        }

        private ParameterCollection baseParams;

        //[ClientConfig(JsonMode.ArrayToObject)]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("(optional) An object containing properties which specify HTTP parameters to be passed to each request for child nodes.")]
        [ViewStateMember]
        public virtual ParameterCollection BaseParams
        {
            get
            {
                if (this.baseParams == null)
                {
                    this.baseParams = new ParameterCollection();
                    this.baseParams.Owner = this.Owner;
                }

                return this.baseParams;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("(optional) Default to true. Remove previously existing child nodes before loading.")]
        public virtual bool ClearOnLoad
        {
            get
            {
                object obj = this.ViewState["ClearOnLoad"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ClearOnLoad"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The URL from which to request a Json string which specifies an array of node definition objects representing the child nodes to be loaded.")]
        public virtual string DataUrl
        {
            get
            {
                return (string)this.ViewState["DataUrl"] ?? "";
            }
            set
            {
                this.ViewState["DataUrl"] = value;
            }
        }

        [ClientConfig("url")]
        [DefaultValue("")]
        internal string UrlProxy
        {
            get
            {
                return string.IsNullOrEmpty(this.DataUrl) ? "" : this.Owner.ResolveUrl(this.DataUrl);
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("If set to true, the loader recursively loads 'children' attributes when doing the first load on nodes.")]
        public virtual bool PreloadChildren
        {
            get
            {
                object obj = this.ViewState["PreloadChildren"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["PreloadChildren"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(HttpMethod.Default)]
        [NotifyParentProperty(true)]
        [Description("The HTTP request method for loading data.")]
        public virtual HttpMethod RequestMethod
        {
            get
            {
                object obj = this.ViewState["RequestMethod"];
                return obj == null ? HttpMethod.Default : (HttpMethod)obj;
            }
            set
            {
                this.ViewState["RequestMethod"] = value;
            }
        }

        [ClientConfig]
        [NotifyParentProperty(true)]
        [DefaultValue(30000)]
        [Description("The timeout in milliseconds to be used for requests. (defaults to 30000)")]
        public int Timeout
        {
            get
            {
                object obj = this.ViewState["Timeout"];
                return obj == null ? 30000 : (int)this.ViewState["Timeout"];
            }
            set
            {
                this.ViewState["Timeout"] = value;
            }
        }

        //TODO: UIProviders
    }

   public class TreeLoaderCollection : StateManagedCollection<TreeLoaderBase>
   {
       [ClientConfig(JsonMode.Object)]
       public TreeLoaderBase Primary
       {
           get
           {
               if (this.Count > 0)
               {
                   return this[0];
               }

               return null;
           }
       }

       public TreeLoaderCollection()
           : this(true)
       {
       }

       public TreeLoaderCollection(bool single)
       {
           this.SingleItemMode = single;
       }
   }
}