/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Standard form container.
    /// </summary>
    [Xtype("form")]
    [InstanceOf(ClassName = "Ext.form.FormPanel")]
    [ToolboxData("<{0}:FormPanel ID=\"FormPanel1\" runat=\"server\" Title=\"Title\" BodyStyle=\"padding:5px;\" Height=\"185\" Width=\"300\" Frame=\"true\" ButtonAlign=\"Right\"><Body><{0}:FormLayout ID=\"FormLayout1\" runat=\"server\"><{0}:Anchor Horizontal=\"100%\"><{0}:TextField ID=\"TextField1\" runat=\"server\" FieldLabel=\"Label\"></{0}:TextField></{0}:Anchor></{0}:FormLayout></Body><Buttons><{0}:Button runat=\"server\" Text=\"Submit\" Icon=\"Disk\" /></Buttons></ext:FormPanel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.FormPanel), "Build.Resources.ToolboxIcons.FormPanel_New.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("Standard form container.")]
    public class FormPanel : FormPanelBase
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);
            if (this.BaseParams.Count > 0)
            {
                if (this.Listeners.BeforeAction.IsDefault)
                {
                    this.Listeners.BeforeAction.Fn = this.BuildParams(this.BaseParams, null, true);
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Listeners.BeforeAction.Fn))
                    {
                        this.Listeners.BeforeAction.Fn = this.BuildParams(this.BaseParams, this.Listeners.BeforeAction.Fn, true);
                    }
                    else
                    {
                        this.Listeners.BeforeAction.Fn = this.BuildParams(this.BaseParams, this.Listeners.BeforeAction.Handler, false);
                    }
                }
            }
        }

        private string BuildParams(ParameterCollection parameters, string userHandler, bool isFn)
        {
            StringBuilder sb = new StringBuilder("function(form,action){if(!form.baseParams){form.baseParams = {};};");

            sb.AppendFormat("Ext.apply(form.baseParams,{0});", parameters.ToJson(0));
            if (!string.IsNullOrEmpty(userHandler))
            {
                sb.Append(userHandler);
                if (isFn)
                {
                    sb.Append("(form,action);");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }

        private FormPanelListeners listeners;

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
        public FormPanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new FormPanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }

        private FormPanelAjaxEvents ajaxEvents;

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
        public FormPanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new FormPanelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }
    }
}