/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class PortalAjaxEvents : PanelAjaxEvents
    {
        private ComponentAjaxEvent validateDrop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("validatedrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent ValidateDrop
        {
            get
            {
                if (this.validateDrop == null)
                {
                    this.validateDrop = new ComponentAjaxEvent();
                }
                return this.validateDrop;
            }
        }

        private ComponentAjaxEvent beforeDragOver;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedragover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeDragOver
        {
            get
            {
                if (this.beforeDragOver == null)
                {
                    this.beforeDragOver = new ComponentAjaxEvent();
                }
                return this.beforeDragOver;
            }
        }

        private ComponentAjaxEvent dragOver;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragover", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent DragOver
        {
            get
            {
                if (this.dragOver == null)
                {
                    this.dragOver = new ComponentAjaxEvent();
                }
                return this.dragOver;
            }
        }

        private ComponentAjaxEvent beforeDrop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedrop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeDrop
        {
            get
            {
                if (this.beforeDrop == null)
                {
                    this.beforeDrop = new ComponentAjaxEvent();
                }
                return this.beforeDrop;
            }
        }

        private ComponentAjaxEvent drop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("drop", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent Drop
        {
            get
            {
                if (this.drop == null)
                {
                    this.drop = new ComponentAjaxEvent();
                }
                return this.drop;
            }
        }
    }
}