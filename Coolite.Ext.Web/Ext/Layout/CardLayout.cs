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
    /// <summary>
    /// This layout contains multiple panels, each fit to the contentContainer, where only a single panel can be visible at any given time. This layout style is most commonly used for wizards, tab implementations, etc. This class is intended to be extended or created via the layout:'card' Ext.Container.layout config, and should generally not need to be created directly via the new keyword.
    /// </summary>
    [Layout("card")]
    [ToolboxData("<{0}:CardLayout runat=\"server\"></{0}:CardLayout>")]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.CardLayout), "Build.Resources.ToolboxIcons.CardLayout.bmp")]
    [Description("This layout contains multiple panels, each fit to the contentContainer, where only a single panel can be visible at any given time. This layout style is most commonly used for wizards, tab implementations, etc. This class is intended to be extended or created via the layout:'card' Ext.Container.layout config, and should generally not need to be created directly via the new keyword.")]
    public class CardLayout : ContainerLayout
    {
        /// <summary>
        /// True to render each contained items at the time it becomes active, false to render all contained items as soon as the layout is rendered (defaults to false). If there is a significant amount of content or a lot of heavy controls being rendered into panels that are not displayed by default, setting this to true might improve performance.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render each contained items at the time it becomes active, false to render all contained items as soon as the layout is rendered (defaults to false). If there is a significant amount of content or a lot of heavy controls being rendered into panels that are not displayed by default, setting this to true might improve performance.")]
        public virtual bool DeferredRender
        {
            get
            {
                object obj = this.ViewState["DeferredRender"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["DeferredRender"] = value;
            }
        }

        [ClientConfig(JsonMode.Object)]
        [Browsable(false)]
        internal override LayoutConfig LayoutConfig
        {
            get
            {
                return new CardLayoutConfigProxy(
                    this.DeferredRender,
                    this.RenderHidden,
                    this.ExtraCls);
            }
        }
    }

    [ToolboxItem(false)]
    public class CardLayoutConfigProxy : LayoutConfig
    {
        private readonly bool deferredRender;

        public CardLayoutConfigProxy(bool deferredRender, bool renderHidden, string extraCls) : base(renderHidden, extraCls)
        {
            this.deferredRender = deferredRender;
        }

        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool DeferredRender
        {
            get
            {
                return this.deferredRender;
            }
        }
    }
}