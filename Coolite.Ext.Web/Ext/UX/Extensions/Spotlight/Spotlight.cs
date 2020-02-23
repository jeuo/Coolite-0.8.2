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
    [ToolboxData("<{0}:Spotlight runat=\"server\"></{0}:Spotlight>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Spotlight), "Build.Resources.ToolboxIcons.Spotlight.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("This control allows you to restrict input to a particular element by masking all other page content.")]
    [ClientScript(Type = typeof(Spotlight), FilePath = "/ux/extensions/spotlight/spotlight.js", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.spotlight.spotlight.js")]
    [InstanceOf(ClassName = "Ext.Spotlight")]
    public class Spotlight : Observable, IVirtual
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to animate the spot (defaults to true).")]
        [NotifyParentProperty(true)]
        public virtual bool Animate
        {
            get
            {
                object obj = this.ViewState["Animate"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Animate"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(0.25)]
        [Description("Animation duration if animate = true (defaults to .25)")]
        public virtual double Duration
        {
            get
            {
                object obj = this.ViewState["Duration"];
                return (obj == null) ? 0.25 : (double)obj;
            }
            set
            {
                this.ViewState["Duration"] = value;
            }
        }

        [ClientConfig(JsonMode.ToCamelLower)]
        [Category("Config Options")]
        [DefaultValue(Easing.EaseNone)]
        [Description("Animation easing if animate = true (defaults to 'easeNone')")]
        public virtual Easing Easing
        {
            get
            {
                object obj = this.ViewState["Easing"];
                return (obj == null) ? Easing.EaseNone : (Easing)obj;
            }
            set
            {
                this.ViewState["Easing"] = value;
            }
        }

        public virtual void Show(string id)
        {
            string template = "{0}.show({1});";
            this.AddScript(template, this.ClientID, JSON.Serialize(id));
        }

        public virtual void Show(WebControl control)
        {
            string template = "{0}.show({1});";
            this.AddScript(template, this.ClientID, control.ClientID);
        }

        public virtual void Hide()
        {
            string template = "{0}.hide();";
            this.AddScript(template, this.ClientID);
        }
    }
}