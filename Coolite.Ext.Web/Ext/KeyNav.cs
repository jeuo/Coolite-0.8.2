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
using System.Drawing;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.KeyNav")]
    [ToolboxData("<{0}:KeyNav runat=\"server\"></{0}:KeyNav>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.KeyNav), "Build.Resources.ToolboxIcons.KeyMap.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Provides a convenient wrapper for normalized keyboard navigation. KeyNav allows you to bind navigation keys to function calls that will get called when the keys are pressed, providing an easy way to implement custom navigation schemes for any UI component.")]
    public class KeyNav : Observable
    {
        internal override string GetClientConstructor(bool instanceOnly, string body)
        {
            if (string.IsNullOrEmpty(this.Target))
            {
                throw new ArgumentNullException("Target", "The Target must defined for KeyNav control");
            }

            if(this.IsDefault)
            {
                return "";
            }

            this.InitOwer();

            string template = (instanceOnly) ? "new {1}(Coolite.Ext.getEl({2}),{3})" : "this.{0}=new {1}(Coolite.Ext.getEl({2}),{3});";

            return string.Format(template, this.ClientID,
                                           "Ext.KeyNav",
                                           this.TargetProxy,
                                           this.InitialConfig);
        }

        public override bool IsDefault
        {
            get
            {
                return this.Left.IsDefault
                       && this.Right.IsDefault
                       && this.Up.IsDefault
                       && this.Down.IsDefault
                       && this.PageDown.IsDefault
                       && this.PageUp.IsDefault
                       && this.Home.IsDefault
                       && this.End.IsDefault
                       && this.Tab.IsDefault
                       && this.Del.IsDefault
                       && this.Esc.IsDefault
                       && this.Enter.IsDefault;
            }
        }

        private void InitOwer()
        {
           this.Left.Owner = this;
           this.Right.Owner = this;
           this.Up.Owner = this;
           this.Down.Owner = this;
           this.PageDown.Owner = this;
           this.PageUp.Owner = this;
           this.Home.Owner = this;
           this.End.Owner = this;
           this.Tab.Owner = this;
           this.Del.Owner = this;
           this.Esc.Owner = this;
           this.Enter.Owner = this;
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

        private JFunction left;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Left
        {
            get
            {
                if(this.left == null)
                {
                    this.left = new JFunction(null, "e");
                }

                return this.left;
            }
        }


        private JFunction right;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Right
        {
            get
            {
                if (this.right == null)
                {
                    this.right = new JFunction(null, "e");
                }

                return this.right;
            }
        }


        private JFunction up;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Up
        {
            get
            {
                if (this.up == null)
                {
                    this.up = new JFunction(null, "e");
                }

                return this.up;
            }
        }


        private JFunction down;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Down
        {
            get
            {
                if (this.down == null)
                {
                    this.down = new JFunction(null, "e");
                }

                return this.down;
            }
        }


        private JFunction pageUp;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction PageUp
        {
            get
            {
                if (this.pageUp == null)
                {
                    this.pageUp = new JFunction(null, "e");
                }

                return this.pageUp;
            }
        }


        private JFunction pageDown;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction PageDown
        {
            get
            {
                if (this.pageDown == null)
                {
                    this.pageDown = new JFunction(null, "e");
                }

                return this.pageDown;
            }
        }


        private JFunction del;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Del
        {
            get
            {
                if (this.del == null)
                {
                    this.del = new JFunction(null, "e");
                }

                return this.del;
            }
        }


        private JFunction home;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Home
        {
            get
            {
                if (this.home == null)
                {
                    this.home = new JFunction(null, "e");
                }

                return this.home;
            }
        }


        private JFunction end;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction End
        {
            get
            {
                if (this.end == null)
                {
                    this.end = new JFunction(null, "e");
                }

                return this.end;
            }
        }

        private JFunction enter;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Enter
        {
            get
            {
                if (this.enter == null)
                {
                    this.enter = new JFunction(null, "e");
                }

                return this.enter;
            }
        }


        private JFunction esc;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Esc
        {
            get
            {
                if (this.esc == null)
                {
                    this.esc = new JFunction(null, "e");
                }

                return this.esc;
            }
        }


        private JFunction tab;

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public JFunction Tab
        {
            get
            {
                if (this.tab == null)
                {
                    this.tab = new JFunction(null, "e");
                }

                return this.tab;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(KeyEventAction.StopEvent)]
        [Description("The method to call on the Ext.EventObject after this KeyNav intercepts a key. Valid values are Ext.EventObject.stopEvent, Ext.EventObject.preventDefault and Ext.EventObject.stopPropagation (defaults to 'stopEvent')")]
        [NotifyParentProperty(true)]
        public virtual KeyEventAction DefaultEventAction
        {
            get
            {
                object obj = this.ViewState["DefaultEventAction"];
                return (obj == null) ? KeyEventAction.StopEvent : (KeyEventAction)obj;
            }
            set
            {
                this.ViewState["DefaultEventAction"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to disable this KeyNav instance (defaults to false)")]
        [NotifyParentProperty(true)]
        [AjaxEventUpdate(MethodName="SetDisabled")]
        public virtual bool Disabled
        {
            get
            {
                object obj = this.ViewState["Disabled"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Disabled"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("Handle the keydown event instead of keypress (defaults to false). KeyNav automatically does this for IE since IE does not propagate special keys on keypress, but setting this to true will force other browsers to also handle keydown instead of keypress.")]
        [NotifyParentProperty(true)]
        public virtual bool ForceKeyDown
        {
            get
            {
                object obj = this.ViewState["ForceKeyDown"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ForceKeyDown"] = value;
            }
        }

        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The scope of the callback function")]
        [NotifyParentProperty(true)]
        public virtual string Scope
        {
            get
            {
                object obj = this.ViewState["Scope"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Scope"] = value;
            }
        }

        private void SetDisabled(bool disabled)
        {
            this.AddScript("{0}.{1}();", this.ClientID, disabled ? "disable" : "enable");
        }
    }
}