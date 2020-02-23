/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:TreePanel runat=\"server\" Title=\"Title\" Height=\"300\"></{0}:TreePanel>")]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.TreePanel), "Build.Resources.ToolboxIcons.TreePanel.bmp")]
    [Designer(typeof(EmptyDesigner))]
    [Description("The TreePanel provides tree-structured UI representation of tree-structured data.")]
    [Xtype("coolitetreepanel")]
    [InstanceOf(ClassName = "Coolite.Ext.TreePanel")]
    public class TreePanel : TreePanelBase, IAjaxPostBackEventHandler
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);
            if(this.Loader.Primary != null && this.Loader.Primary.BaseParams.Count > 0)
            {
                TreeLoader loader = this.Loader.Primary as TreeLoader;
                if(loader != null)
                {
                    if(loader.Listeners.BeforeLoad.IsDefault)
                    {
                        loader.Listeners.BeforeLoad.Fn = BuildParams(this.Loader.Primary.BaseParams, null, true);     
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(loader.Listeners.BeforeLoad.Fn))
                        {
                            loader.Listeners.BeforeLoad.Fn = BuildParams(this.Loader.Primary.BaseParams, loader.Listeners.BeforeLoad.Fn, true);     
                        }
                        else
                        {
                            loader.Listeners.BeforeLoad.Fn = BuildParams(this.Loader.Primary.BaseParams, loader.Listeners.BeforeLoad.Handler, false);     
                        }
                    }
                }
            }
        }

        private string BuildParams(ParameterCollection parameters, string userHandler, bool isFn)
        {
            StringBuilder sb = new StringBuilder("function(loader,node){if(!loader.baseParams){loader.baseParams = {};};");

            sb.AppendFormat("Ext.apply(loader.baseParams,{0});", parameters.ToJson(0));
            if(!string.IsNullOrEmpty(userHandler))
            {
                sb.Append(userHandler);
                if(isFn)
                {
                    sb.Append("(loader,node);");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
        
        private TreePanelListeners listeners;

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
        public TreePanelListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new TreePanelListeners();
                    this.listeners.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }


        private TreePanelAjaxEvents ajaxEvents;

        /// <summary>
        /// Server-side AjaxEvent Handlers
        /// </summary>
        [Category("Events")]
        [Themeable(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Server-side AjaxEventHandlers")]
        [ViewStateMember]
        public TreePanelAjaxEvents AjaxEvents
        {
            get
            {
                if (this.ajaxEvents == null)
                {
                    this.ajaxEvents = new TreePanelAjaxEvents();
                    this.ajaxEvents.InitOwners(this);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.ajaxEvents).TrackViewState();
                    }
                }
                return this.ajaxEvents;
            }
        }

        #region Implementation of IAjaxPostBackEventHandler

        public void RaiseAjaxPostBackEvent(string eventArgument, ParameterCollection extraParams)
        {
            bool success = true;
            string msg = null;
            TreeNodeCollection nodes = null;
            try
            {
                if (string.IsNullOrEmpty(eventArgument))
                {
                    throw new ArgumentNullException("eventArgument");
                }

                switch(eventArgument)
                {
                    case "nodeload":
                        NodeLoadEventArgs e = new NodeLoadEventArgs(extraParams);
                        PageTreeLoader loader = (PageTreeLoader) this.Loader.Primary;
                        loader.OnNodeLoad(e);
                        nodes = e.Nodes;
                        success = e.Success;
                        msg = e.ErrorMessage;
                        break;
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = this.IsDebugging ? ex.ToString() : ex.Message;
                if (this.ScriptManager.RethrowAjaxExceptions)
                {
                    throw;
                }
            }

            AfterAjaxEventArgs eAjaxPostBackResult = new AfterAjaxEventArgs(new Response(success, msg));
            eAjaxPostBackResult.Response.Data = nodes != null ? nodes.ToJson() : null;

            ScriptManager.ServiceResponse = eAjaxPostBackResult.Response;
        }

        #endregion
    }
}