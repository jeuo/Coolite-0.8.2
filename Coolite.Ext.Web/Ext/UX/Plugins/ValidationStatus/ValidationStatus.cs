/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Coolite.Ext.Web;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [InstanceOf(ClassName = "Ext.ux.ValidationStatus")]
    [ClientScript(Type = typeof(ValidationStatus), FilePath = "/ux/plugins/validationstatus/validationstatus.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.validationstatus.validationstatus.js")]
    [ClientStyle(Type = typeof(ValidationStatus), FilePath = "/ux/plugins/validationstatus/css/validationstatus.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.plugins.validationstatus.css.validationstatus-embedded.css")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ValidationStatus), "Build.Resources.ToolboxIcons.Plugin_New.bmp")]
    [ToolboxData("<{0}:ValidationStatus runat=\"server\" />")]
    public class ValidationStatus : Plugin, IIcon
    {
        /// <summary>
        /// The FormPanel to use.
        /// </summary>
        [ClientConfig("{raw}form", JsonMode.ToClientID)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The FormPanel to use.")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string FormPanelID
        {
            get
            {
                return (string)ViewState["FormPanelID"] ?? "";
            }
            set
            {
                this.ViewState["FormPanelID"] = value;
            }
        }

        /// <summary>
        /// The error icon
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The error icon")]
        public virtual Icon ErrorIcon
        {
            get
            {
                object obj = this.ViewState["ErrorIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["ErrorIcon"] = value;
            }
        }

        /// <summary>
        /// The error icon css class
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("x-status-error")]
        [Description("The error icon css class")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string ErrorIconCls
        {
            get
            {
                return (string)ViewState["ErrorIconCls"] ?? "x-status-error";
            }
            set
            {
                this.ViewState["ErrorIconCls"] = value;
            }
        }

        [ClientConfig("errorIconCls")]
        [DefaultValue("")]
        internal virtual string ErrorIconClsProxy
        {
            get
            {
                if (this.ErrorIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.ErrorIcon);
                }
                return this.ErrorIconCls;
            }
        }

        /// <summary>
        /// The error list css class
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-status-error-list")]
        [Description("The error list css class")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string ErrorListCls
        {
            get
            {
                return (string)ViewState["ErrorListCls"] ?? "x-status-error-list";
            }
            set
            {
                this.ViewState["ErrorListCls"] = value;
            }
        }

        /// <summary>
        /// The valid icon
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The valid icon")]
        public virtual Icon ValidIcon
        {
            get
            {
                object obj = this.ViewState["ValidIcon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["ValidIcon"] = value;
            }
        }

        /// <summary>
        /// The valid icon css class
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("x-status-valid")]
        [Description("The valid icon css class")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string ValidIconCls
        {
            get
            {
                return (string)ViewState["ValidIconCls"] ?? "x-status-valid";
            }
            set
            {
                this.ViewState["ValidIconCls"] = value;
            }
        }

        [ClientConfig("validIconCls")]
        [DefaultValue("")]
        internal virtual string ValidIconClsProxy
        {
            get
            {
                if (this.ValidIcon != Icon.None)
                {
                    return ScriptManager.GetIconClassName(this.ValidIcon);
                }
                return this.ValidIconCls;
            }
        }

        /// <summary>
        /// The text which shown when errors exist
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("The form has errors (click for details...)")]
        [Description("The text which shown when errors exist")]
        [IDReferenceProperty(typeof(Store))]
        [NotifyParentProperty(true)]
        public virtual string ShowText
        {
            get
            {
                return (string)ViewState["ShowText"] ?? "The form has errors (click for details...)";
            }
            set
            {
                this.ViewState["ShowText"] = value;
            }
        }

        /// <summary>
        /// The text which hide error list when click on it
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Click again to hide the error list")]
        [Description("The text which hide error list when click on it")]
        [NotifyParentProperty(true)]
        public virtual string HideText
        {
            get
            {
                return (string)ViewState["HideText"] ?? "Click again to hide the error list";
            }
            set
            {
                this.ViewState["HideText"] = value;
            }
        }

        List<Icon> IIcon.Icons
        {
            get
            {
                List<Icon> icons = new List<Icon>(2);
                icons.Add(this.ErrorIcon);
                icons.Add(this.ValidIcon);
                return icons;
            }
        }
    }
}
