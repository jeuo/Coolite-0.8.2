/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/
using System.ComponentModel;
using System.Web.UI;
using System.Xml.Serialization;

namespace Coolite.Ext.Web
{
    public class AsyncTreeNode : TreeNodeBase
    {
        public AsyncTreeNode() { }

        public AsyncTreeNode(string text)
        {
            this.Text = text;
        }

        public AsyncTreeNode(string id, string text)
        {
            this.NodeID = id;
            this.Text = text;
        }

        //[ClientConfig]
        //internal string NodeType
        //{
        //    get
        //    {
        //        return "async";
        //    }
        //}

        [DefaultValue(true)]
        [NotifyParentProperty(true)]
        public override bool EnforceNodeType
        {
            get
            {
                object obj = this.ViewState["EnforceNodeType"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["EnforceNodeType"] = value;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        internal string NodeType
        {
            get
            {
                return this.EnforceNodeType ? "async" : "";
            }
        }

        private AsyncTreeNodeListeners listeners;

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
        public AsyncTreeNodeListeners Listeners
        {
            get
            {
                if (this.listeners == null)
                {
                    this.listeners = new AsyncTreeNodeListeners();
                    this.listeners.InitOwners(this.Owner);

                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager)this.listeners).TrackViewState();
                    }
                }
                return this.listeners;
            }
        }
    }
}