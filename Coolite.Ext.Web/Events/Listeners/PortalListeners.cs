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
    public class PortalListeners : PanelListeners
    {
        private ComponentListener validateDrop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("validatedrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener ValidateDrop
        {
            get
            {
                if (this.validateDrop == null)
                {
                    this.validateDrop = new ComponentListener();
                }
                return this.validateDrop;
            }
        }

        private ComponentListener beforeDragOver;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedragover", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeDragOver
        {
            get
            {
                if (this.beforeDragOver == null)
                {
                    this.beforeDragOver = new ComponentListener();
                }
                return this.beforeDragOver;
            }
        }

        private ComponentListener dragOver;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("dragover", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener DragOver
        {
            get
            {
                if (this.dragOver == null)
                {
                    this.dragOver = new ComponentListener();
                }
                return this.dragOver;
            }
        }

        private ComponentListener beforeDrop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedrop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeDrop
        {
            get
            {
                if (this.beforeDrop == null)
                {
                    this.beforeDrop = new ComponentListener();
                }
                return this.beforeDrop;
            }
        }

        private ComponentListener drop;

        [ListenerArgument(0, "e", typeof(object))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("drop", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener Drop
        {
            get
            {
                if (this.drop == null)
                {
                    this.drop = new ComponentListener();
                }
                return this.drop;
            }
        }
    }
}