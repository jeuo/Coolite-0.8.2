/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;
using Coolite.Utilities;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class Mask : ScriptClass
    {
        private Mask() { }

        public static Mask Instance
        {
            get
            {
                return (HttpContext.Current.Items["Coolite.Ext.Mask"] ?? (HttpContext.Current.Items["Coolite.Ext.Mask"] = new Mask())) as Mask;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        public override string Serialize()
        {
            return this.Build(string.Concat("Coolite.Ext.Mask.show(", this.currentConfig != null ? this.currentConfig.Serialize() : "", ");"));
        }


        /*  Configure
            -----------------------------------------------------------------------------------------------*/

        private Config currentConfig = null;

        public virtual Mask Configure(Config config)
        {
            this.currentConfig = config;
            return this;
        }


        /*  Show
            -----------------------------------------------------------------------------------------------*/

        public virtual Mask Show()
        {
            this.Render();
            return this;
        }

        public virtual Mask Show(Config config)
        {
            return this.Configure(config).Show();
        }


        /*  Hide
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Hides the message box if it is displayed
        /// </summary>
        [Description("Hides the mask")]
        public virtual Mask Hide()
        {
            this.AddScript("Coolite.Ext.Mask.hide();");
            return this;
        }


        /*  Config
            -----------------------------------------------------------------------------------------------*/

        [ToolboxItem(false)]
        [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
        public class Config
        {
            public virtual string Serialize()
            {
                return new ClientConfig().Serialize(this);
            }

            string msg = "";

            /// <summary>
            /// The title text
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("The title text")]
            public virtual string Msg
            {
                get
                {
                    return this.msg;
                }
                set
                {
                    this.msg = value;
                }
            }

            string msgCls = "";

            /// <summary>
            /// An id or Element from which the message box should animate as it opens and closes (defaults to undefined)
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("An id or Element from which the message box should animate as it opens and closes (defaults to undefined)")]
            public virtual string MsgCls
            {
                get
                {
                    return this.msgCls;
                }
                set
                {
                    this.msgCls = value;
                }
            }

            string el = "";

            /// <summary>
            /// An id or Element from which the message box should animate as it opens and closes (defaults to undefined)
            /// </summary>
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("An element to mask")]
            public virtual string El
            {
                get
                {
                    return this.el;
                }
                set
                {
                    this.el = value;
                }
            }

            private Control control = null;

            /// <summary>
            /// A Control to mask
            /// </summary>
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("An element to mask")]
            public virtual Control Control
            {
                get
                {
                    return this.control;
                }
                set
                {
                    this.control = value;
                }
            }

            [ClientConfig("el", JsonMode.Raw)]
            [DefaultValue("")]
            protected virtual string ElProxy
            {
                get
                {
                    if (this.Control != null)
                    {
                        if (this.Control is Component)
                        {
                            return this.Control.ClientID;
                        }

                        return string.Concat("Ext.get(\"", this.Control.ClientID, "\")");
                    }

                    return this.El;
                }
            }
        }
    }
}