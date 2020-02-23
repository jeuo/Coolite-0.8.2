/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.KeyMap")]
    [DefaultProperty("Keys")]
    [ParseChildren(true, "Keys")]
    [ToolboxData("<{0}:KeyMap runat=\"server\"></{0}:KeyMap>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.KeyMap), "Build.Resources.ToolboxIcons.KeyMap.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Handles mapping keys to actions for an element.")]
    public class KeyMap : Observable
    {
        internal override string GetClientConstructor(bool instanceOnly, string body)
        {
            if(string.IsNullOrEmpty(this.Target))
            {
                throw new ArgumentNullException("Target", "The Target must defined for KeyMap control");
            }

            if (this.Keys.Count == 0)
            {
                return "";
            }

            string template = (instanceOnly) ? "new {1}(Coolite.Ext.getEl({2}),{3}{4})" : "this.{0}=new {1}(Coolite.Ext.getEl({2}),{3}{4});";

            return string.Format(template, this.ClientID,
                                           "Ext.KeyMap", 
                                           this.TargetProxy,
                                           this.KeysProxy, 
                                           string.IsNullOrEmpty(this.EventName) ? "" : "," + this.EventName);
        }

        private KeyBindingCollection keys;

        /// <summary>
        /// A KeyMap config object (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to this panel (defaults to null).
        /// </summary>
        [ClientConfig("keys", JsonMode.Array)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("A KeyMap config object (in the format expected by Ext.KeyMap.addBinding used to assign custom key handling to this panel (defaults to null).")]
        [NotifyParentProperty(true)]
        [ViewStateMember]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public virtual KeyBindingCollection Keys
        {
            get
            {
                if (this.keys == null)
                {
                    this.keys = new KeyBindingCollection();
                    this.keys.AfterItemAdd += this.AfterKeyBindingAdd;
                }

                return this.keys;
            }
        }

        private string KeysProxy
        {
            get
            {
                if (this.Keys.Count == 1)
                {
                    return new ClientConfig().SerializeInternal(this.Keys[0], this);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    bool comma = false;
                    sb.Append("[");
                    foreach (KeyBinding keyBinding in this.Keys)
                    {
                        if (comma)
                        {
                            sb.Append(",");
                        }

                        sb.Append(new ClientConfig().SerializeInternal(keyBinding, this));

                        comma = true;
                    }
                    sb.Append("]");

                    return sb.ToString();
                }
            }
        }

        protected virtual void AfterKeyBindingAdd(KeyBinding keyBinding)
        {
            keyBinding.Owner = this;
            keyBinding.Listeners.Event.Owner = this;
        }

        private string TargetProxy
        {
            get
            {
                string parsedTarget = TokenUtils.ParseTokens(this.Target, this);

                if (TokenUtils.IsRawToken(parsedTarget))
                {
                    return TokenUtils.ReplaceRawToken(parsedTarget);
                }

                return string.Concat("\"", parsedTarget, "\"");
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The element to bind to")]
        public virtual string Target
        {
            get
            {
                return (string)this.ViewState["Target"] ?? "";
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(optional) The event to bind to (defaults to 'keydown')")]
        public virtual string EventName
        {
            get
            {
                return (string)this.ViewState["EventName"] ?? "";
            }
            set
            {
                this.ViewState["EventName"] = value;
            }
        }

        public void Enable()
        {
            this.AddScript("{0}.enable();", this.ClientID);
        }

        public void Disable()
        {
            this.AddScript("{0}.disable();", this.ClientID);
        }

        public void AddKeyBinding(KeyBinding keyBinding)
        {
            Ext.EnsureAjaxEvent();
            this.AddScript("{0}.addBinding({1});", this.ClientID, JSON.Serialize(keyBinding));
        }
    }
}