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
    public class ContainerListeners : BoxComponentListeners
    {
        private ComponentListener add;

        /// <summary>
        /// Fires after any Component is added or inserted into the contentContainer.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was added")]
        [ListenerArgument(2, "index", typeof(int), "The index at which the component was added to the container's items collection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("add", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after any Component is added or inserted into the contentContainer.")]
        public virtual ComponentListener Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new ComponentListener();
                }
                return this.add;
            }
        }

        private ComponentListener afterLayout;

        /// <summary>
        /// Fires when the components in this contentContainer are arranged by the associated layout manager.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "layout", typeof(ContainerLayout), "The ContainerLayout implementation for this container")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afterlayout", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the components in this contentContainer are arranged by the associated layout manager.")]
        public virtual ComponentListener AfterLayout
        {
            get
            {
                if (this.afterLayout == null)
                {
                    this.afterLayout = new ComponentListener();
                }
                return this.afterLayout;
            }
        }

        private ComponentListener beforeAdd;

        /// <summary>
        /// Fires before any Component is added or inserted into the contentContainer. A handler can return false to cancel the add.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was added")]
        [ListenerArgument(2, "index", typeof(int), "The index at which the component was added to the container's items collection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeadd", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before any Component is added or inserted into the contentContainer. A handler can return false to cancel the add.")]
        public virtual ComponentListener BeforeAdd
        {
            get
            {
                if (this.beforeAdd == null)
                {
                    this.beforeAdd = new ComponentListener();
                }
                return this.beforeAdd;
            }
        }

        private ComponentListener beforeRemove;

        /// <summary>
        /// Fires before any Component is removed from the contentContainer. A handler can return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component being removed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before any Component is removed from the contentContainer. A handler can return false to cancel the remove.")]
        public virtual ComponentListener BeforeRemove
        {
            get
            {
                if (this.beforeRemove == null)
                {
                    this.beforeRemove = new ComponentListener();
                }
                return this.beforeRemove;
            }
        }

        private ComponentListener remove;

        /// <summary>
        /// Fires after any Component is removed from the contentContainer.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was removed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after any Component is removed from the contentContainer.")]
        public virtual ComponentListener Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new ComponentListener();
                }
                return this.remove;
            }
        }
    }
}