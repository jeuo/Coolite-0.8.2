/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class Notification : ScriptClass
    {
        private const string INSTANCE = "Coolite.Ext.Notification";

        private Notification() { }

        public static Notification Instance
        {
            get
            {
                return (HttpContext.Current.Items[Notification.INSTANCE] ?? (HttpContext.Current.Items[Notification.INSTANCE] = new Notification())) as Notification;
            }
        }

        public override string Serialize()
        {
            return "";
        }

        public virtual Notification Show(Config config)
        {
            this.AddScript(this.Build(string.Concat(Notification.INSTANCE, ".show(", new ClientConfig().Serialize(config), ");")));

            return this;
        }


        public class Config
        {
            private string id = "";
            /// <summary>
            /// ID of instance
            /// </summary>
            [ClientConfig("id")]
            [Category("Config Options")]
            [DefaultValue(true)]
            [Description("ID of instance")]
            [NotifyParentProperty(true)]
            public virtual string ID
            {
                get
                {
                    return this.id;
                }
                set
                {
                    this.id = value;
                }
            }

            private string title="";
            /// <summary>
            /// The title text to display in the window header
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("The title text to display in the window header")]
            [NotifyParentProperty(true)]
            public virtual string Title
            {
                get
                {
                    return this.title;
                }
                set
                {
                    this.title = value;
                }
            }

            private string html = "";

            /// <summary>
            /// The title text to display in the window header
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("An HTML fragment to use as the panel's body content.")]
            [NotifyParentProperty(true)]
            public virtual string Html
            {
                get
                {
                    return this.html;
                }
                set
                {
                    this.html = value;
                }
            }

            private string contentEl = "";
            /// <summary>
            /// The id of an existing HTML node to use as the panel's body content
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("The id of an existing HTML node to use as the panel's body content")]
            [NotifyParentProperty(true)]
            public virtual string ContentEl
            {
                get
                {
                    return this.contentEl;
                }
                set
                {
                    this.contentEl = value;
                }
            }
            
            private Unit width = Unit.Pixel(200);

            /// <summary>
            /// The width of this notification in pixels (defaults to 200).
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(typeof(Unit), "200")]
            [Description("The width of this notification in pixels (defaults to 200).")]
            [NotifyParentProperty(true)]
            public virtual Unit Width
            {
                get
                {
                    return this.width;
                }
                set
                {
                    this.width = value;
                }
            }

            private Unit height = Unit.Pixel(100);

            /// <summary>
            /// The height of this notification in pixels (defaults to 100).
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(typeof(Unit), "100")]
            [Description("The height of this notification in pixels (defaults to 100).")]
            [NotifyParentProperty(true)]
            public virtual Unit Height
            {
                get
                {
                    return this.height;
                }
                set
                {
                    this.height = value;
                }
            }

            private bool autoHide = true;

            /// <summary>
            /// False to stay visible after showing
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(true)]
            [Description("False to stay visible after showing")]
            [NotifyParentProperty(true)]
            public virtual bool AutoHide
            {
                get
                {
                    return this.autoHide;
                }
                set
                {
                    this.autoHide = value;
                }
            }

            private bool closable = true;

            /// <summary>
            /// False to hide the button and disallow closing the window
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(true)]
            [Description("False to hide the button and disallow closing the window")]
            [NotifyParentProperty(true)]
            public virtual bool Closable
            {
                get
                {
                    return this.closable;
                }
                set
                {
                    this.closable = value;
                }
            }

            private bool shadow = false;

            /// <summary>
            /// True to show a shadow
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(false)]
            [Description("True to show a shadow")]
            [NotifyParentProperty(true)]
            public virtual bool Shadow
            {
                get
                {
                    return this.shadow;
                }
                set
                {
                    this.shadow = value;
                }
            }


            private bool plain = false;

            /// <summary>
            /// False to add a lighter background color to visually highlight the body element and separate it more distinctly from the surrounding frame
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(false)]
            [Description("False to add a lighter background color to visually highlight the body element and separate it more distinctly from the surrounding frame")]
            [NotifyParentProperty(true)]
            public virtual bool Plain
            {
                get
                {
                    return this.plain;
                }
                set
                {
                    this.plain = value;
                }
            }

            private bool resizable = false;

            /// <summary>
            /// True to allow user resizing at each edge and corner of the window, false to disable resizing 
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(false)]
            [Description("True to allow user resizing at each edge and corner of the window, false to disable resizing ")]
            [NotifyParentProperty(true)]
            public virtual bool Resizable
            {
                get
                {
                    return this.resizable;
                }
                set
                {
                    this.resizable = value;
                }
            }

            private bool draggable = false;

            /// <summary>
            /// True to allow the window to be dragged by the header bar 
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(false)]
            [Description("True to allow the window to be dragged by the header bar")]
            [NotifyParentProperty(true)]
            public virtual bool Draggable
            {
                get
                {
                    return this.draggable;
                }
                set
                {
                    this.draggable = value;
                }
            }

            private string bodyStyle = "";

            /// <summary>
            /// Custom CSS styles to be applied to the body element  
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("Custom CSS styles to be applied to the body element")]
            [NotifyParentProperty(true)]
            public virtual string BodyStyle
            {
                get
                {
                    return this.bodyStyle;
                }
                set
                {
                    this.bodyStyle = value;
                }
            }

            private AlignConfig alignConfig;
            /// <summary>
            /// Align config object 
            /// </summary>
            [ClientConfig("alignToCfg", JsonMode.Object)]
            [Category("Config Options")]
            [DefaultValue(null)]
            [Description("Align config object")]
            [NotifyParentProperty(true)]
            public virtual AlignConfig AlignCfg
            {
                get
                {
                    return this.alignConfig;
                }
                set
                {
                    this.alignConfig = value;
                }
            }

            private ShowMode showMode = ShowMode.Grid;

            /// <summary>
            /// Determines how the Notification Windows will be shown in relation to each other if more than one rendered to the viewport at a single time. 
            /// Options include "Grid" which will show each individual separately in a matrix and new Notification Windows will be shown in the best available 
            /// empty hole within the grid. Best available is considered bottom-right.
            /// If ShowMode.Stack, the Notification Windows will be stacked on top of each other hiding the Window below.
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(ShowMode.Grid)]
            [Description("False to show a notification upon all other visible notofications")]
            [NotifyParentProperty(true)]
            public virtual ShowMode ShowMode
            {
                get
                {
                    return this.showMode;
                }
                set
                {
                    this.showMode = value;
                }
            }

            private bool closeVisible = false;

            /// <summary>
            /// True to close all other visible notifications
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(false)]
            [Description("True to close all other visible notifications")]
            [NotifyParentProperty(true)]
            public virtual bool CloseVisible
            {
                get
                {
                    return this.closeVisible;
                }
                set
                {
                    this.closeVisible = value;
                }
            }

            private bool modal = false;

            /// <summary>
            /// True to make the window modal and mask everything behind it when displayed, false to display it without restricting access to other UI elements (defaults to false).
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("True to make the window modal and mask everything behind it when displayed, false to display it without restricting access to other UI elements (defaults to false).")]
            public virtual bool Modal
            {
                get
                {
                    return this.modal;
                }
                set
                {
                    this.modal = value;
                }
            }

            private string pinEvent = "click";

            /// <summary>
            /// Stop hidding event, 'none' if hidding can't be stoped
            /// </summary>
            [Category("Config Options")]
            [DefaultValue("click")]
            [Description("Stop hidding event, 'none' if hidding can't be stoped")]
            [NotifyParentProperty(true)]
            public virtual string PinEvent
            {
                get
                {
                    return this.pinEvent;
                }
                set
                {
                    this.pinEvent = value;
                }
            }

            [ClientConfig("pinEvent")]
            protected virtual string PinEventProxy
            {
                get
                {
                    return this.PinEvent.ToLower();
                }
            }

            private int hideDelay = 2500;

            /// <summary>
            /// Hide delay in ms
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue(2500)]
            [Description("Hide delay in ms")]
            [NotifyParentProperty(true)]
            public virtual int HideDelay
            {
                get
                {
                    return this.hideDelay;
                }
                set
                {
                    this.hideDelay = value;
                }
            }

            private ConfigItemCollection customConfig;
            

            [ClientConfig("-", typeof(CustomConfigJsonConverter))]
            [Description("Collection of custom js config")]
            public virtual ConfigItemCollection CustomConfig
            {
                get
                {
                    if (this.customConfig == null)
                    {
                        this.customConfig = new ConfigItemCollection();
                    }

                    return this.customConfig;
                }
                set
                {
                    this.customConfig = value;
                }
            }

            private Fx showFx;

            [ClientConfig(JsonMode.Object)]
            [Category("Config Options")]
            [DefaultValue(null)]
            [NotifyParentProperty(true)]
            public virtual Fx ShowFx
            {
                get
                {
                    return this.showFx;
                }
                set
                {
                    this.showFx = value;
                }
            }

            private Fx hideFx;

            [ClientConfig(JsonMode.Object)]
            [Category("Config Options")]
            [DefaultValue(null)]
            [NotifyParentProperty(true)]
            public virtual Fx HideFx
            {
                get
                {
                    return this.hideFx;
                }
                set
                {
                    this.hideFx = value;
                }
            }

            private Icon icon = Icon.None;
            /// <summary>
            /// The icon to use in the header. See also, IconCls to set an icon with a custom Css class.
            /// </summary>
            [Category("Config Options")]
            [DefaultValue(Icon.None)]
            [Description("The icon to use in the header. See also, IconCls to set an icon with a custom Css class.")]
            public virtual Icon Icon
            {
                get
                {
                    return this.icon;
                }
                set
                {
                    this.icon = value;
                }
            }

            [ClientConfig("iconCls")]
            [DefaultValue("")]
            internal virtual string IconClsProxy
            {
                get
                {
                    if (this.Icon != Icon.None)
                    {
                        return ScriptManager.GetIconClassName(this.Icon);
                    }
                    return this.IconCls;
                }
            }

            private string iconCls = "";
            /// <summary>
            /// A css class which sets a background image to be used as the icon in the header.
            /// </summary>
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("A css class which sets a background image to be used as the icon in the header.")]
            [NotifyParentProperty(true)]
            public virtual string IconCls
            {
                get
                {
                    return this.iconCls;
                }
                set
                {
                    this.iconCls = value;
                }
            }

            private LoadConfig autoLoad;

            /// <summary>
            /// A valid url spec according to the UpdateOptions Ext.UpdateOptions.update method. If autoLoad is not null, the panel will attempt to load its contents immediately upon render. The URL will become the default URL for this panel's body element, so it may be refreshed at any time.
            /// </summary>
            [Category("Config Options")]
            [DefaultValue(null)]
            [Description("A valid url spec according to the UpdateOptions Ext.UpdateOptions.update method. If autoLoad is not null, the panel will attempt to load its contents immediately upon render. The URL will become the default URL for this panel's body element, so it may be refreshed at any time.")]
            [NotifyParentProperty(true)]
            [PersistenceMode(PersistenceMode.InnerProperty)]
            public virtual LoadConfig AutoLoad
            {
                get
                {
                    if (this.autoLoad == null)
                    {
                        this.autoLoad = new LoadConfig();
                    }
                    return this.autoLoad;
                }
                set
                {
                    this.autoLoad = value;
                }
            }

            [ClientConfig("autoLoad", JsonMode.Raw)]
            [DefaultValue("")]
            internal string AutoLoadProxy
            {
                get
                {
                    if (this.AutoLoad != null && !this.AutoLoad.IsDefault)
                    {
                        return new ClientConfig().Serialize(this.AutoLoad);
                    }
                    return "";
                }
            }

            private WindowListeners listeners;

            /// <summary>
            /// Client-side JavaScript EventHandlers
            /// </summary>
            [ClientConfig("listeners", JsonMode.Object)]
            [Description("Client-side JavaScript EventHandlers")]
            public WindowListeners Listeners
            {
                get
                {
                    if (this.listeners == null)
                    {
                        this.listeners = new WindowListeners();
                    }
                    return this.listeners;
                }
                set
                {
                    this.listeners = value;
                }
            }

            private ToolsCollection tools;

            /// <summary>
            /// An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element referenced by a public property called tools.<tool-type>.
            /// </summary>
            [ClientConfig(JsonMode.AlwaysArray)]
            [Category("Config Options")]
            [NotifyParentProperty(true)]
            [PersistenceMode(PersistenceMode.InnerProperty)]
            [Description("An array of tool button configs to be added to the header tool area. When rendered, each tool is stored as an Element referenced by a public property called tools.<tool-type>.")]
            public virtual ToolsCollection Tools
            {
                get
                {
                    if (this.tools == null)
                    {
                        this.tools = new ToolsCollection();
                    }

                    return this.tools;
                }
                set
                {
                    this.tools = value;
                }
            }

            private bool showPin = false;

            /// <summary>
            /// True to show pin tool button.
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("True to show pin tool button.")]
            public virtual bool ShowPin
            {
                get
                {
                    return this.showPin;
                }
                set
                {
                    this.showPin = value;
                }
            }

            private bool pinned = false;

            /// <summary>
            /// True to show pin tool button.
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("True to to show window as pinned.")]
            public virtual bool Pinned
            {
                get
                {
                    return this.pinned;
                }
                set
                {
                    this.pinned = value;
                }
            }

            private bool bringToFront = false;

            /// <summary>
            /// True to show pin tool button.
            /// </summary>
            [ClientConfig]
            [DefaultValue(false)]
            [Description("True to show window as pinned.")]
            public virtual bool BringToFront
            {
                get
                {
                    return this.bringToFront;
                }
                set
                {
                    this.bringToFront = value;
                }
            }
        }

        public enum ShowMode
        {
            Grid,
            Stack
        }

        public class AlignConfig
        {
            private string el="";

            [ClientConfig("el", JsonMode.Raw)]
            [DefaultValue("")]
            internal string ElProxy
            {
                get
                {
                    if (string.IsNullOrEmpty(this.El))
                    {
                        return "";
                    }
                    return string.Concat("Coolite.Ext.getEl(", TokenUtils.ParseAndNormalize(this.El), ")");
                }
            }

            [DefaultValue("")]
            [Description("Align element (default is document)")]
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

            private AnchorPoint elementAnchor = AnchorPoint.BottomRight;

            [DefaultValue(AnchorPoint.BottomRight)]
            [Description("Element anchor point")]
            public AnchorPoint ElementAnchor
            {
                get
                {
                    return this.elementAnchor;
                }
                set
                {
                    this.elementAnchor = value;
                }
            }

            private AnchorPoint targetAnchor = AnchorPoint.BottomRight;

            [DefaultValue(AnchorPoint.BottomRight)]
            [Description("Target anchor point")]
            public AnchorPoint TargetAnchor
            {
                get
                {
                    return this.targetAnchor;
                }
                set
                {
                    this.targetAnchor = value;
                }
            }

            [ClientConfig("position")]
            [DefaultValue("")]
            internal string AnchorsProxy
            {
                get
                {
                    return string.Concat(Fx.AnchorConvert(this.ElementAnchor), "-",
                                         Fx.AnchorConvert(this.TargetAnchor));
                }
            }

            private int offsetX=-20;
            public int OffsetX
            {
                get
                {
                    return this.offsetX;
                }
                set
                {
                    this.offsetX = value;
                }
            }

            private int offsetY = -20;
            public int OffsetY
            {
                get
                {
                    return this.offsetY;
                }
                set
                {
                    this.offsetY = value;
                }
            }

            [ClientConfig("offset", JsonMode.Raw)]
            [DefaultValue("")]
            internal string OffsetProxy
            {
                get
                {
                    if(this.OffsetX == -20 && this.OffsetY == -20)
                    {
                        return "";
                    }

                    return string.Concat("[", this.OffsetX, ",", this.OffsetY,"]");
                }
            }
        }
    }
}