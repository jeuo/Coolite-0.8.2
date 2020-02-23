/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    public abstract class ListenerAction : StateManagedItem
    {
        [IDReferenceProperty(typeof(WebControl))]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string ControlID
        {
            get
            {
                object o = this.ViewState["ControlID"];
                return o == null ? "" : (string) o;
            }
            set
            {
                this.ViewState["ControlID"] = value;
            }
        }

        protected virtual Type[] AllowedTypes
        {
            get
            {
                return new Type[0];
            }
        }

        protected abstract string Name
        { 
            get;
        }

        protected virtual bool ControlIsRequired
        {
            get
            {
                return true;
            }
        }

        protected string Selector
        {
            get
            {
                return this.ControlIsRequired ? this.GetControl().ClientID : this.ControlID;
            }
        }

        public abstract string GetScript();

        protected Control GetControl()
        {
            if (this.IsDefault)
            {
                return null;
            }

            Control control = ControlUtils.FindControl(this.Owner, this.ControlID);

            if (control == null)
            {
                throw new InvalidOperationException(string.Concat("The control with ID='", this.ControlID, "' can't be find"));
            }

            return control;
        }

        protected void CheckControlTypeIsAcceptable()
        {
            Type[] allowedTypes = this.AllowedTypes;
            if (allowedTypes.Length == 0 || this.IsDefault)
            {
                return;
            }

            Control control = this.GetControl();

            foreach (Type allowedType in allowedTypes)
            {
                if(control.GetType().IsAssignableFrom(allowedType))
                {
                    return;
                }
            }

            throw new InvalidCastException(string.Concat("The control with type='", control.GetType().ToString(),"' doesn't acceptable."));
        }

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.ControlID);
            }
        }
    }

    public class ListenerActionCollection : StateManagedCollection<ListenerAction>
    {
    }
}
