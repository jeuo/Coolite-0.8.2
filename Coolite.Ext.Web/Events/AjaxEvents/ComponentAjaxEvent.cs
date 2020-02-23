/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [TypeConverter(typeof (AjaxEventConverter))]
    [ToolboxItem(false)]
    public class ComponentAjaxEvent : BaseAjaxEvent
    {
        public delegate void AjaxEventHandler(object sender, AjaxEventArgs e);

        private event AjaxEventHandler handler;

        public event AjaxEventHandler Event
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.handler = (AjaxEventHandler) System.Delegate.Combine(this.handler, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.handler = (AjaxEventHandler)System.Delegate.Remove(this.handler, value);
            }
        }

        internal virtual void OnEvent(AjaxEventArgs e)
        {
            if (this.handler != null)
            {
                this.handler(this.Owner, e);
            }
        }

        internal string HandlerName
        {
            get
            {
                return this.handler.Method.Name;
            }
        }

        public override bool IsDefault
        {
            get
            {
                return this.handler == null && string.IsNullOrEmpty(this.Url) && this.Type == AjaxEventType.Submit;
            }
        }

        /// <summary>
        /// Before handler with params: el, type, action, extraParams
        /// </summary>
        [ClientConfig(typeof(AjaxEventHandlerJsonConverter))]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Before handler with params: el, type, action, extraParams")]
        public string Before
        {
            get
            {
                return (string)this.ViewState["Before"] ?? "";
            }
            set
            {
                this.ViewState["Before"] = value;
            }
        }

        /// <summary>
        /// Success handler with params: response, result, control, type, action, extraParams
        /// </summary>
        [DefaultValue("")]
        [ClientConfig("userSuccess", typeof(AjaxEventHandlerJsonConverter))]
        [NotifyParentProperty(true)]
        [Description("Success handler with params: response, result, el, type, action, extraParams")]
        public string Success
        {
            get
            {
                return (string)this.ViewState["Success"] ?? "";
            }
            set
            {
                this.ViewState["Success"] = value;
            }
        }

        /// <summary>
        /// Failure handler with params: response, result, control, type, action, extraParams
        /// </summary>
        [DefaultValue("")]
        [ClientConfig("userFailure", typeof(AjaxEventHandlerJsonConverter))]
        [NotifyParentProperty(true)]
        [Description("Failure handler with params: response, result, control, type, action, extraParams")]
        public string Failure
        {
            get
            {
                return (string)this.ViewState["Failure"] ?? "";
            }
            set
            {
                this.ViewState["Failure"] = value;
            }
        }

        /// <summary>
        /// Show warning if request fail. If Failure handler exists then this handler will be called instead showing warning
        /// </summary>
        [ClientConfig]
        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        [Description("Show a Window with error message is AjaxEvent request fails. This message Window will only show if a Failure Handler does not exist.")]
        public bool ShowWarningOnFailure
        {
            get
            {
                object obj = this.ViewState["ShowWarningOnFailure"];
                return obj != null ? (bool)obj : true;
            }
            set
            {
                this.ViewState["ShowWarningOnFailure"] = value;
            }
        }

        public virtual void Clear()
        {
            this.Before = this.Success = this.Failure = "";
            this.ShowWarningOnFailure = true;
        }

        private AjaxEventConfirmation confirmation;

        [ClientConfig(JsonMode.Object)]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ViewStateMember]
        public AjaxEventConfirmation Confirmation
        {
            get
            {
                if (this.confirmation == null)
                {
                    this.confirmation = new AjaxEventConfirmation();
                }

                return this.confirmation;
            }
        }
    }

    public class AjaxEventConfirmation : StateManagedItem
    {
        [DefaultValue(false)]
        [ClientConfig]
        [NotifyParentProperty(true)]
        [Description("If true show confirmation dialog")]
        public bool ConfirmRequest
        {
            get
            {
                object obj = this.ViewState["ConfirmRequest"];
                return obj != null ? (bool)obj : false;
            }
            set
            {
                this.ViewState["ConfirmRequest"] = value;
            }
        }

        [DefaultValue("Confirmation")]
        [ClientConfig]
        [NotifyParentProperty(true)]
        [Description("Confirmation dialog title")]
        public string Title
        {
            get
            {
                return (string)this.ViewState["Title"] ?? "Confirmation";
            }
            set
            {
                this.ViewState["Title"] = value;
            }
        }

        [DefaultValue("Are you sure?")]
        [ClientConfig]
        [NotifyParentProperty(true)]
        [Description("Confirmation dialog message")]
        public string Message
        {
            get
            {
                return (string)this.ViewState["Message"] ?? "Are you sure?";
            }
            set
            {
                this.ViewState["Message"] = value;
            }
        }

        /// <summary>
        /// Before confirm handler. Return false to cancel confirm
        /// </summary>
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Before confirm handler. Return false to cancel confirm")]
        public string BeforeConfirm
        {
            get
            {
                return (string)this.ViewState["BeforeConfirm"] ?? "";
            }
            set
            {
                this.ViewState["BeforeConfirm"] = value;
            }
        }

        [ClientConfig("beforeConfirm", JsonMode.Raw)]
        [DefaultValue("")]
        internal string BeforeConfirmProxy
        {
            get
            {
                if(string.IsNullOrEmpty(this.BeforeConfirm))
                {
                    return "";
                }

                return string.Concat("function(config){", this.BeforeConfirm, "}");
            }
        }

    }

    public class AjaxEventArgs : EventArgs
    {
        private readonly ParameterCollection extraParams;

        public AjaxEventArgs(ParameterCollection extraParams)
        {
            this.extraParams = extraParams;
        }

        public ParameterCollection ExtraParams
        {
            get
            {
                return this.extraParams;
            }
        }

        public ParameterCollection ExtraParamsResponse
        {
            get
            {
                return ScriptManager.ExtraParamsResponse;
            }
        }

        public bool Success
        {
            get { return ScriptManager.AjaxSuccess; }
            set { ScriptManager.AjaxSuccess = value; }
        }

        public string ErrorMessage
        {
            get { return ScriptManager.AjaxErrorMessage; }
            set { ScriptManager.AjaxErrorMessage = value; }
        }
    }
}
