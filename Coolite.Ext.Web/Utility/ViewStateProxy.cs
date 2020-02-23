/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Reflection;
using System.Threading;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public class ViewStateProxy : StateManagedItem
    {
        private readonly WebControl control;
        public ViewStateProxy(WebControl control, StateBag viewState)
        {
            this.control = control;
            this.ViewState = viewState;
        }

        public virtual bool Suspend()
        {
            bool oldValue = control.AllowCallbackScriptMonitoring;
            this.control.AllowCallbackScriptMonitoring = false;
            Monitor.Enter(this.control);
            return oldValue;
        }

        public virtual void Resume(bool oldValue)
        {
            this.control.AllowCallbackScriptMonitoring = oldValue;
            Monitor.Exit(this.control);
        }

        public virtual void Resume()
        {
            this.Resume(true);
        }

        public object this[string key]
        {
            get
            {
                return this.ViewState[key];
            }
            set
            {
                this.ViewState[key] = value;
                if (Ext.IsAjaxRequest && control.AllowCallbackScriptMonitoring)
                {
                    PropertyInfo pi = control.GetType().GetProperty(key);

                    if(pi == null)
                    {
                        return;
                    }

                    object[] attrs = pi.GetCustomAttributes(typeof(AjaxEventUpdateAttribute), true);

                    if (attrs.Length > 0)
                    {
                        this.control.CallbackValues[key] = value;
                        ((AjaxEventUpdateAttribute)attrs[0]).RegisterScript(this.control, pi); 
                    }
                    else
                    {
                        ClientConfigAttribute attr = ClientConfig.GetClientConfigAttribute(pi);
                        if (attr != null)
                        {
                            this.control.CallbackValues[key] = value;
                            this.control.AddScript(string.Format(AjaxEventUpdateAttribute.AutoGenerateFormat, this.control.ClientID, JSON.Serialize(value), StringUtils.ToLowerCamelCase(pi.Name)));
                        }
                    }
                }
            }
        }

    }
}