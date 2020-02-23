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
    /// <summary>
    /// A simple utility class for generically masking elements while loading data. If the element being masked has an underlying Ext.data.Store, the masking will be automatically synchronized with the store's loading process and the mask element will be cached for reuse. For all other elements, this mask will replace the element's UpdateOptions load indicator and will be destroyed after the initial load.
    /// </summary>
    [Description("A simple utility class for generically masking elements while loading data. If the element being masked has an underlying Ext.data.Store, the masking will be automatically synchronized with the store's loading process and the mask element will be cached for reuse. For all other elements, this mask will replace the element's UpdateOptions load indicator and will be destroyed after the initial load.")]
    public class LoadMask : StateManagedItem
    {
        /// <summary>
        /// True to create a single-use mask that is automatically destroyed after loading (useful for page loads), False to persist the mask element reference for multiple uses (e.g., for paged data widgets). Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to create a single-use mask that is automatically destroyed after loading (useful for page loads), False to persist the mask element reference for multiple uses (e.g., for paged data widgets). Defaults to false.")]
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

        /// <summary>
        /// The text to display in a centered loading message box (defaults to 'Loading...').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Loading...")]
        [Description("The text to display in a centered loading message box (defaults to 'Loading...').")]
        public virtual string Msg
        {
            get
            {
                return (string)this.ViewState["Msg"] ?? "Loading...";
            }
            set
            {
                this.ViewState["Msg"] = value;
            }
        }

        /// <summary>
        /// The CSS class to apply to the loading message element (defaults to 'x-mask-loading').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-mask-loading")]
        [Description("The CSS class to apply to the loading message element (defaults to 'x-mask-loading').")]
        public virtual string MsgCls
        {
            get
            {
                return (string)this.ViewState["MsgCls"] ?? "x-mask-loading";
            }
            set
            {
                this.ViewState["MsgCls"] = value;
            }
        }

        /// <summary>
        /// True to create a single-use mask that is automatically destroyed after loading (useful for page loads), False to persist the mask element reference for multiple uses (e.g., for paged data widgets). Defaults to false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to create a single-use mask that is automatically destroyed after loading (useful for page loads), False to persist the mask element reference for multiple uses (e.g., for paged data widgets). Defaults to false.")]
        public virtual bool RemoveMask
        {
            get
            {
                object obj = this.ViewState["RemoveMask"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["RemoveMask"] = value;
            }
        }

        public override bool IsDefault
        {
            get
            {
                if(this is EventMask)
                {
                    return base.IsDefault;
                }
                return (this.ShowMask == false);
            }
        }
    }
}