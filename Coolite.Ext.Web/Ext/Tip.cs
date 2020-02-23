/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Utilities;
using System.Text.RegularExpressions;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Ext.Tip")]
    public abstract class Tip : PanelBase
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string RenderTo
        {
            get
            {
                return "";
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string ApplyTo
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// True to render a close tool button into the tooltip header (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("True to render a close tool button into the tooltip header (defaults to false).")]
        public virtual bool Closable
        {
            get
            {
                object obj = this.ViewState["Closable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Closable"] = value;
            }
        }

        /// <summary>
        /// Experimental. The default Ext.Element.alignTo anchor position value for this tip relative to its element of origin (defaults to 'tl-bl?').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Experimental. The default Ext.Element.alignTo anchor position value for this tip relative to its element of origin (defaults to 'tl-bl?').")]
        public virtual string DefaultAlign
        {
            get
            {
                return (string)this.ViewState["DefaultAlign"] ?? "";
            }
            set
            {
                this.ViewState["DefaultAlign"] = value;
            }
        }

        /// <summary>
        /// The maximum width of the tip in pixels (defaults to 300). The maximum supported value is 500.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(300)]
        [NotifyParentProperty(true)]
        [Description("The maximum width of the tip in pixels (defaults to 300). The maximum supported value is 500.")]
        public virtual int MaxWidth
        {
            get
            {
                object obj = this.ViewState["MaxWidth"];
                return (obj == null) ? 300 : (int)obj;
            }
            set
            {
                this.ViewState["MaxWidth"] = value;
            }
        }

        /// <summary>
        /// The minimum width of the tip in pixels (defaults to 40).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(40)]
        [NotifyParentProperty(true)]
        [Description("The minimum width of the tip in pixels (defaults to 40).")]
        public virtual int MinWidth
        {
            get
            {
                object obj = this.ViewState["MinWidth"];
                return (obj == null) ? 40 : (int)obj;
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Shows this tip at the specified XY position.
        /// </summary>
        [Description("Shows this tip at the specified XY position.")]
        public virtual void ShowAt(Unit x, Unit y)
        {
            string template = "{0}.showAt([{1},{2}]);";
            this.AddScript(template, this.ClientID, x.Value.ToString(), y.Value.ToString());
        }

        /// <summary>
        /// Experimental. Shows this tip at a position relative to another element using a standard Ext.Element.alignTo anchor position value.
        /// </summary>
        [Description("Experimental. Shows this tip at a position relative to another element using a standard Ext.Element.alignTo anchor position value.")]
        public virtual void ShowBy(string id)
        {
            string template = "{0}.showBy(\"{1}\");";
            this.AddScript(template, this.ClientID, id);
        }

        /// <summary>
        /// Experimental. Shows this tip at a position relative to another element using a standard Ext.Element.alignTo anchor position value.
        /// </summary>
        [Description("Experimental. Shows this tip at a position relative to another element using a standard Ext.Element.alignTo anchor position value.")]
        public virtual void ShowBy(string id, string position)
        {
            string template = "{0}.showBy(\"{1}\",\"{2}\");";
            this.AddScript(template, this.ClientID, id, position);
        }
    }
}