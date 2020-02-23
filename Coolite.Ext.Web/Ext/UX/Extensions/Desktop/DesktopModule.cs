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
    public class DesktopModule : StateManagedItem
    {
        [ClientConfig("id")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string ModuleID
        {
            get
            {
                return (string)this.ViewState["ModuleID"] ?? "";
            }
            set
            {
                this.ViewState["ModuleID"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public virtual string WindowID
        {
            get
            {
                return (string)this.ViewState["WindowID"] ?? "";
            }
            set
            {
                this.ViewState["WindowID"] = value;
            }
        }

        [ClientConfig("windowID")]
        [DefaultValue("")]
        internal string WindowProxy
        {
            get
            {
                if(string.IsNullOrEmpty(this.WindowID))
                {
                    return "";
                }
                Control control = ControlUtils.FindControl(this.Owner, this.WindowID, true);
                if (control != null)
                {
                    return control.ClientID;
                }
                
                throw new InvalidOperationException(string.Format("The DesktopWindow with the ID of '{0}' was not found", this.WindowID));
            }
        }
        
        private MenuItem launcher;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public MenuItem Launcher
        {
            get
            {
                if(this.launcher == null)
                {
                    this.launcher = new MenuItem();
                }

                return this.launcher;
            }
        }

        [DefaultValue("")]
        [ClientConfig("launcher",JsonMode.Raw)]
        internal string LauncherProxy
        {
            get
            {
                if (string.IsNullOrEmpty(this.launcher.Text))
                {
                    return "";
                }
                return string.Concat("{", this.Launcher.ClientID, "_ClientInit}");
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        public virtual bool AutoRun
        {
            get
            {
                object obj = this.ViewState["AutoRun"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoRun"] = value;
            }
        }

    }

    public class DesktopModulesCollection : StateManagedCollection<DesktopModule> { }
}