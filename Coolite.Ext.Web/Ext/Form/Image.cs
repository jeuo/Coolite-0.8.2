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
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [Xtype("image")]
    [InstanceOf(ClassName = "Coolite.Ext.Image")]
    [ToolboxData("<{0}:Image runat=\"server\" />")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Image), "Build.Resources.ToolboxIcons.Image.bmp")]
    [ContainerStyle("display:inline;")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Basic image field.")]
    public class Image : BoxComponent
    {
        [ClientConfig(JsonMode.Ignore)]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetImageUrl")]
        public virtual string ImageUrl
        {
            get
            {
                return (string)this.ViewState["ImageUrl"] ?? "";
            }
            set
            {
                this.ViewState["ImageUrl"] = value;
            }
        }

        [ClientConfig("imageUrl")]
        [DefaultValue("")]
        internal virtual string ImageUrlProxy
        {
            get
            {
                return this.ResolveUrlLink(this.ImageUrl);
            }
        }

        [ClientConfig("altText")]
        [Category("Config Options")]
        [DefaultValue("")]
        [AjaxEventUpdate(MethodName = "SetAltText")]
        public virtual string AlternateText
        {
            get
            {
                return (string)this.ViewState["AlternateText"] ?? "";
            }
            set
            {
                this.ViewState["AlternateText"] = value;
            }
        }

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(ImageAlign.NotSet)]
        [AjaxEventUpdate(MethodName = "SetAlign")]
        public ImageAlign Align
        {
            get
            {
                object obj = this.ViewState["Align"];
                return (obj == null) ? ImageAlign.NotSet : (ImageAlign)obj;
            }
            set
            {
                this.ViewState["Align"] = value;
            }
        }

        private BoxComponentListeners listeners;

        /// <summary>
        /// Client-side JavaScript EventHandlers
        /// </summary>
        [ClientConfig("listeners", JsonMode.Object)]
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Client-side JavaScript EventHandlers")]
        public BoxComponentListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new BoxComponentListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private BoxComponentAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        public BoxComponentAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new BoxComponentAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        protected virtual void SetImageUrl(string url)
        {
            this.AddScript("{0}.setImageUrl({1});", this.ClientID, JSON.Serialize(this.ResolveUrlLink(url)));
        }

        protected virtual void SetAltText(string altText)
        {
            this.AddScript("{0}.setAltText({1});", this.ClientID, JSON.Serialize(altText));
        }

        protected virtual void SetAlign(ImageAlign align)
        {
            this.AddScript("{0}.setAlign({1});", this.ClientID, JSON.Serialize(align.ToString().ToLower()));
        }
    }
}