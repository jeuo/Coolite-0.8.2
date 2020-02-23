/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:DesktopWindow runat=\"server\" Title=\"Title\" Collapsible=\"true\" Icon=\"Application\"><Body></Body></{0}:DesktopWindow>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.DesktopWindow), "Build.Resources.ToolboxIcons.DesktopWindow.bmp")]
    [Designer(typeof(WindowDesigner))]
    [Description("A specialized panel intended for use as an application window. Windows are floated and draggable by default, and also provide specific behavior like the ability to maximize and restore (with an event for minimizing, since the minimize behavior is application-specific). Windows can also be linked to a Ext.WindowGroup or managed by the Ext.WindowManager to provide grouping, activation, to front/back and other application-specific behavior.")]
    [Xtype("desktopwindow")]
    [InstanceOf(ClassName = "Coolite.Ext.DesktopWindow")]
    public class DesktopWindow : Window
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.Maximizable)
            {
                this.CustomConfig.Add(new ConfigItem("maximizable", "true", ParameterMode.Raw));
            }

            if (this.Minimizable)
            {
                this.CustomConfig.Add(new ConfigItem("minimizable", "true", ParameterMode.Raw));
            }

            this.ShowOnLoad = false;
            base.OnBeforeClientInit(sender);
        }

        protected override void SetMaximizable()
        {
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the 'maximize' tool button and allow the user to maximize the window, false to hide the button and disallow maximizing the window (defaults to false). Note that when a window is maximized, the tool button will automatically change to a 'restore' button with the appropriate behavior already built-in that will restore the window to its previous size.")]
        public override bool Maximizable
        {
            get
            {
                object obj = this.ViewState["Maximizable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Maximizable"] = value;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the 'minimize' tool button and allow the user to minimize the window, false to hide the button and disallow minimizing the window (defaults to false). Note that this button provides no implementation -- the behavior of minimizing a window is implementation-specific, so the minimize event must be handled and a custom minimize behavior implemented for this option to be useful.")]
        public override bool Minimizable
        {
            get
            {
                object obj = this.ViewState["Minimizable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Minimizable"] = value;
            }
        }

        [ClientConfig(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        public Desktop Desktop
        {
            get
            {
                if(this.DesignMode)
                {
                    return null;
                }
                return Desktop.GetCurrent(this.Page);
            }
        }

        [DefaultValue("")]
        [ClientConfig(JsonMode.Raw)]
        internal string Manager
        {
            get
            {
                if (this.DesignMode)
                {
                    return "";
                }

                return string.Concat(this.Desktop.ClientID, ".desktop.getManager()");
            }
        }

        [DefaultValue("")]
        [ClientConfig("desktop", JsonMode.Raw)]
        internal string DesktopReference
        {
            get
            {
                if (this.DesignMode)
                {
                    return "";
                }

                return string.Concat(this.Desktop.ClientID, ".desktop");
            }
        }

        public override void Show()
        {
            this.AddScript("{0}.getDesktop().showWindow({1});", this.Desktop.ClientID, JSON.Serialize(this.ClientID));
        }
    }
}