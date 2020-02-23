/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public class ListItem : StateManagedItem
    {
        private WebControl parent;

        internal ListItem(WebControl parent)
        {
            this.parent = parent;
        }

        public ListItem() { }

        public ListItem(string text, string value)
        {
            this.Value = value;
            this.Text = text;
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                string oldValue = this.Text;
                this.ViewState["Text"] = value;
                if (string.IsNullOrEmpty(this.Value) || oldValue == this.Value)
                {
                    this.Value = value;
                }
            }
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string Value
        {
            get
            {
                return (string)this.ViewState["Value"] ?? "";
            }
            set
            {
                this.ViewState["Value"] = value;
                this.SetValue(value);
            }
        }

        internal void SetValue(string value)
        {
            if(parent != null && parent.AllowCallbackScriptMonitoring && Ext.IsAjaxRequest)
            {
                parent.AddScript("{0}.setValue({1});", parent.ClientID, JSON.Serialize(value));
            }
        }
    }

    public class ListItemCollection<T> : StateManagedCollection<T> where T : StateManagedItem { }
}
