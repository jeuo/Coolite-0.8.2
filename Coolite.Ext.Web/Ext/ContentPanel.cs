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
using System.Web.UI.HtmlControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// A Panel with a &lt;Body> region. 
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultProperty("Body")]
    public abstract class ContentPanel : PanelBase, IContent
    {
        /*  IContent
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// The id of an existing HTML node to use as the panel's body content (defaults to '').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DeferredRender]
        [DefaultValue("")]
        [Description("The id of an existing HTML node to use as the panel's body content (defaults to '').")]
        public virtual string ContentEl
        {
            get
            {
                if (!this.DesignMode)
                {
                    if (!this.BodyContainer.Visible)
                    {
                        return "";
                    }

                    if (this.Body == null && this.BodyControls.Count < 1)
                    {
                        this.Controls.Remove(this.BodyContainer);
                        return "";
                    }
                }

                return (this.AutoLoad.IsDefault
                    && string.IsNullOrEmpty(this.Html)
                    && this.Layout == null)
                    ? this.BodyContainer.ClientID : "";
            }
        }

        private ITemplate body;

        [DefaultValue(null)]
        [Browsable(false)]
        [TemplateInstance(TemplateInstance.Single)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]    
        public virtual ITemplate Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
                if (value != null)
                {
                    value.InstantiateIn(this.BodyContainer);
                }
            }
        }

        private HtmlGenericControl bodyContainer;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual HtmlGenericControl BodyContainer
        {
            get
            {
                if (this.bodyContainer == null)
                {
                    this.bodyContainer = this.CreateContainer();
                }
                return this.bodyContainer;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlCollection BodyControls
        {
            get
            {
                return this.BodyContainer.Controls;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Body != null && !string.IsNullOrEmpty(this.Html) && !Ext.IsAjaxRequest)
            {
                throw new ArgumentException(string.Format("The Html property and Body template have both been set on {0} (ID: {1}). Only one of the properties can be set at a time.", this.GetType().Name, this.ID));
            }

            if (this.BodyContainer != null && !this.DesignMode)
            {
                this.BodyContainer.ID = string.Concat(this.ID, "_Body");
                this.BodyContainer.Attributes.Add("class", "x-hidden");
            }

            base.OnPreRender(e);
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Updates the content of the Panel body with the supplied string ('text') value. Same as the .SetHtml(html) Method.
        /// </summary>
        [Description("Updates the body of the Panel body with the supplied string ('text') value. Same as the .SetHtml(html) Method.")]
        public virtual void UpdateBody(string text)
        {
            this.SetHtml(text);
        }
    }
}