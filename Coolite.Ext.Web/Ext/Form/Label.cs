/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Basic Label field.
    /// </summary>
    [Xtype("label")]
    [InstanceOf(ClassName = "Ext.form.Label")]
    [ToolboxData("<{0}:Label runat=\"server\" />")]
    [ContainerStyle("display:inline;")]
    [DefaultProperty("Html")]
    [ParseChildren(true, "Html")]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(LabelDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.Label), "Build.Resources.ToolboxIcons.Label.bmp")]
    [Description("Basic Label field.")]
    public class Label : BoxComponent, ITextControl, IIcon
    {
        public Label() { }

        public Label(string text) 
        {
            this.Text = text;
        }

        public Label(string format, string text)
        {
            this.Format = format;
            this.Text = text;
        }

        /// <summary>
        /// The format of the string to render using the .Text property. Example 'Hello {0}'.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The format of the string to render using the .Text property. Example 'Hello {0}'.")]
        public virtual string Format
        {
            get
            {
                return (string)this.ViewState["Format"] ?? "";
            }
            set
            {
                this.ViewState["Format"] = value;
            }
        }

        /// <summary>
        /// The default text to display if the Text property is empty (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("The default text to display if the Text property is empty (defaults to '').")]
        public virtual string EmptyText
        {
            get
            {
                return (string)this.ViewState["EmptyText"] ?? "";
            }
            set
            {
                this.ViewState["EmptyText"] = value;
            }
        }
        
        /// <summary>
        /// The id of the input element to which this label will be bound via the standard 'htmlFor' attribute. If not specified, the attribute will not be added to the label.
        /// </summary>
        [ClientConfig("forId")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id of the input element to which this label will be bound via the standard 'htmlFor' attribute. If not specified, the attribute will not be added to the label.")]
        public virtual string ForID
        {
            get
            {
                return (string)this.ViewState["ForID"] ?? "";
            }
            set
            {
                this.ViewState["ForID"] = value;
            }
        }

        /// <summary>
        /// An HTML fragment that will be used as the label's innerHTML (defaults to ''). Note that if text is specified it will take precedence and this value will be ignored.
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetHtml")]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("An HTML fragment that will be used as the label's innerHTML (defaults to ''). Note that if text is specified it will take precedence and this value will be ignored.")]
        public virtual string Html
        {
            get
            {
                return (string)this.ViewState["Html"] ?? "";
            }
            set
            {
                this.ViewState["Html"] = value;
            }
        }

        /// <summary>
        /// The plain text to display within the label (defaults to ''). If you need to include HTML tags within the label's innerHTML, use the html config instead.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetText")]
        [Localizable(true)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The plain text to display within the label (defaults to ''). If you need to include HTML tags within the label's innerHTML, use the html config instead.")]
        public virtual string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [ClientConfig("text")]
        [DefaultValue("")]
        protected virtual string TextProxy
        {
            get
            {
                if (!string.IsNullOrEmpty(this.EmptyText) && string.IsNullOrEmpty(this.Text))
                {
                    return this.EmptyText;
                }

                if (!string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.Format))
                {
                    return string.Format(this.Format, this.Text);
                }

                return this.Text;
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

        /// <summary>
        /// The icon to use in the label. See also, IconCls to set an icon with a custom Css class.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [Description("The icon to use in the label. See also, IconCls to set an icon with a custom Css class.")]
        public virtual Icon Icon
        {
            get
            {
                object obj = this.ViewState["Icon"];
                return (obj == null) ? Icon.None : (Icon)obj;
            }
            set
            {
                this.ViewState["Icon"] = value;
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

        /// <summary>
        /// A css class which sets a background image to be used as the icon for this label.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class which sets a background image to be used as the icon for this label.")]
        [NotifyParentProperty(true)]
        public virtual string IconCls
        {
            get
            {
                return (string)this.ViewState["IconCls"] ?? "";
            }
            set
            {
                this.ViewState["IconCls"] = value;
            }
        }

        // <summary>
        /// (optional) Set the CSS text-align property of the icon. The center is not supported. Defaults left.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(Alignment.Left)]
        [Description("(optional) Set the CSS text-align property of the icon. The center is not supported. Defaults to \"Left\"")]
        public virtual Alignment IconAlign
        {
            get
            {
                object obj = this.ViewState["IconAlign"];
                return (obj == null) ? Alignment.Left : (Alignment)obj;
            }
            set
            {
                this.ViewState["IconAlign"] = value;
            }
        }

        private ItemsCollection<Editor> editor;

        /// <summary>
        /// Inline editor
        /// </summary>
        [ClientConfig("editor", typeof(ItemCollectionJsonConverter))]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Inline editor")]
        public virtual ItemsCollection<Editor> Editor
        {
            get
            {
                if (this.editor == null)
                {
                    this.editor = new ItemsCollection<Editor>();
                    this.editor.SingleItemMode = true;
                    this.editor.AfterItemAdd += this.AfterItemAdd;
                }

                return this.editor;
            }
        }

        protected virtual void AfterItemAdd(Editor item)
        {
            this.Controls.Add(item);
            if (!this.LazyItems.Contains(item))
            {
                this.LazyItems.Add(item);
            }
        }

        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Updates the label's innerHTML with the specified string.
        /// </summary>
        [Description("Updates the label's innerHTML with the specified string.")]
        protected virtual void SetHtml(string html)
        {
            this.SetText(html, false);
        }

        /// <summary>
        /// Updates the label's innerHTML with the specified string.
        /// </summary>
        [Description("Updates the label's innerHTML with the specified string.")]
        protected virtual void SetText(string text)
        {
            this.SetText(text, true);
        }

        /// <summary>
        /// Updates the label's innerHTML with the specified string.
        /// </summary>
        [Description("Updates the label's innerHTML with the specified string.")]
        protected virtual void SetText(string text, bool encode)
        {
            this.AddScript("{0}.setText({1},{2});", this.ClientID, JSON.Serialize(text), encode.ToString().ToLower());
        }

        /// <summary>
        /// Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.
        /// </summary>
        [Description("Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.")]
        protected virtual void SetIconClass(string cls)
        {
            this.AddScript("{0}.setIconClass({1});", this.ClientID, JSON.Serialize(cls));
        }

        /// <summary>
        /// Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.
        /// </summary>
        protected virtual void SetIconClass(Icon icon)
        {
            if (this.Icon != Icon.None)
            {
                this.SetIconClass(ScriptManager.GetIconClassName(icon)); 
            }
            else
            {
                this.SetIconClass(""); 
            }
        }

        List<Icon> IIcon.Icons
        {
            get
            {
                List<Icon> icons = new List<Icon>(1);
                icons.Add(this.Icon);
                return icons;
            }
        }
    }
}