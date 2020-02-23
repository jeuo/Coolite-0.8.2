/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ControlValueProperty("FileBytes")]
    [ValidationProperty("FileName")]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
    [AspNetHostingPermissionAttribute(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]

    [ToolboxData("<{0}:FileUploadField runat=\"server\" />")]
    [DefaultEvent("FileSelected")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [SupportsEventValidation]
    [Designer(typeof(EmptyDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.FileUploadField), "Build.Resources.ToolboxIcons.FileUploadField_New.bmp")]
    [Xtype("fileuploadfield")]
    [InstanceOf(ClassName = "Ext.form.FileUploadField")]
    [ClientStyle(Type = typeof(FileUploadField), FilePath = "/ux/extensions/fileuploadfield/css/file-upload.css", WebResource = "Coolite.Ext.Web.Build.Resources.Coolite.ux.extensions.fileuploadfield.css.file-upload.css")]
    [Description("File upload field")]
    public class FileUploadField : TextFieldBase, IIcon
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            if (this.AutoPostBack)
            {
                this.On("fileselected", new JFunction(this.PostBackFunction));
            }

            this.SetValue(this.Text);
        }

        

        /// <summary>
        /// The Text value to initialize this field with.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetText")]
        [Category("Appearance")]
        [Localizable(true)]
        [Themeable(false)]
        [Bindable(true, BindingDirection.TwoWay)]
        [Description("The Text value to initialize this field with.")]
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

        [Browsable(false)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = (string)value;
            }
        }

        /// <summary>
        /// The button text to display on the upload button (defaults to 'Browse...'). Note that if you supply a value for ButtonCfg, the ButtonCfg.Text value will be used instead if available.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetButtonText")]
        [ClientConfig]
        [DefaultValue("Browse...")]
        [Category("Config Options")]
        [Localizable(true)]
        [Themeable(false)]
        [Description("The button text to display on the upload button (defaults to 'Browse...'). Note that if you supply a value for ButtonCfg, the ButtonCfg.Text value will be used instead if available.")]
        public virtual string ButtonText
        {
            get
            {
                return (string)this.ViewState["ButtonText"] ?? "Browse...";
            }
            set
            {
                this.ViewState["ButtonText"] = value;
            }
        }

        /// <summary>
        /// True to display the file upload field as a button with no visible text field (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display the file upload field as a button with no visible text field (defaults to false).")]
        public virtual bool ButtonOnly
        {
            get
            {
                object obj = this.ViewState["ButtonOnly"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ButtonOnly"] = value;
            }
        }

        /// <summary>
        /// The number of pixels of space reserved between the button and the text field (defaults to 3).  Note that this only applies if ButtonOnly=false.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(3)]
        [Description("The number of pixels of space reserved between the button and the text field (defaults to 3).  Note that this only applies if ButtonOnly=false.")]
        public virtual int ButtonOffset
        {
            get
            {
                object obj = this.ViewState["ButtonOffset"];
                return (obj == null) ? 3 : (int)obj;
            }
            set
            {
                this.ViewState["ButtonOffset"] = value;
            }
        }

        /// <summary>
        /// True to mark the field as readOnly in HTML (defaults to false) -- Note: this only sets the element's readOnly DOM attribute.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetReadOnly")]
        [ClientConfig]
        [Category("Config Options")]
        [Bindable(true)]
        [DefaultValue(true)]
        [Description("True to mark the field as readOnly in HTML (defaults to false) -- Note: this only sets the element's readOnly DOM attribute.")]
        public override bool ReadOnly
        {
            get
            {
                object obj = this.ViewState["ReadOnly"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ReadOnly"] = value;
            }
        }

        /// <summary>
        /// The icon to use in the Button. See also, IconCls to set an icon with a custom Css class.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(Icon.None)]
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Description("The icon to use in the Button. See also, IconCls to set an icon with a custom Css class.")]
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
        /// A css class which sets a background image to be used as the icon for this button.
        /// </summary>
        [AjaxEventUpdate(MethodName = "SetIconClass")]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("A css class which sets a background image to be used as the icon for this button.")]
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

        byte[] cachedBytes;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true, BindingDirection.OneWay)]
        [Browsable(false)]
        public byte[] FileBytes
        {
            get
            {
                if (this.cachedBytes == null)
                {
                    this.cachedBytes = new byte[this.FileContent.Length];
                    this.FileContent.Read(this.cachedBytes, 0, this.cachedBytes.Length);
                }
                return (byte[])(this.cachedBytes.Clone());
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Stream FileContent
        {
            get
            {
                if (this.PostedFile == null)
                {
                    return Stream.Null;
                }
                
                return this.PostedFile.InputStream;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FileName
        {
            get
            {
                if (this.PostedFile == null)
                {
                    return string.Empty;
                }
                 
                return this.PostedFile.FileName;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasFile
        {
            get
            {
                return !string.IsNullOrEmpty(this.FileName);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HttpPostedFile PostedFile
        {
            get
            {
                if (this.Page == null || !this.Page.IsPostBack)
                {
                    return null;
                }
                    
                if (this.Context == null || this.Context.Request == null)
                {
                    return null;
                }

                return this.Context.Request.Files[this.ClientID + "-file"];
            }
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            if(this.IsInForm)
            {
                this.Page.Form.Enctype = "multipart/form-data";
            }

            base.OnPreRender(e);
        }

        private FileUploadFieldListeners listeners;

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
        [ViewStateMember]
        public FileUploadFieldListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new FileUploadFieldListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private FileUploadFieldAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side Ajax EventHandlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side Ajax EventHandlers")]
        [ViewStateMember]
        public FileUploadFieldAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new FileUploadFieldAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string val = postCollection[this.UniqueName];
            if (val != null && this.Text != val)
            {
                try
                {
                    this.ViewState.Suspend();
                    this.Text = val.Equals(this.EmptyText) ? string.Empty : val;
                }
                finally
                {
                    this.ViewState.Resume();
                }

                return true;
            }
            return false;
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

        /// <summary>
        /// Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.
        /// </summary>
        [Description("Sets the CSS class that provides a background image to use as the button's icon. This method also changes the value of the iconCls config internally.")]
        protected virtual void SetIconClass(string cls)
        {
            this.AddScript("{0}.button.setIconClass({1});", this.ClientID, JSON.Serialize(cls));
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

        /// <summary>
        /// Sets this button's text
        /// </summary>
        [Description("Sets this button's text")]
        protected virtual void SetButtonText(string text)
        {
            this.AddScript("{0}.button.setText({1});", this.ClientID, JSON.Serialize(text));
        }

        /// <summary>
        /// Sets this text
        /// </summary>
        [Description("Sets this text")]
        protected virtual void SetText(string text)
        {
            this.AddScript("{0}.setText({1});", this.ClientID, JSON.Serialize(text));
        }
    }
}
