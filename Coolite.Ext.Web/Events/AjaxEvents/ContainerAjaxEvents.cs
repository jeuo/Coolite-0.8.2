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
    public class ContainerAjaxEvents : BoxComponentAjaxEvents
    {
        private ComponentAjaxEvent add;

        /// <summary>
        /// Fires after any Component is added or inserted into the contentContainer.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was added")]
        [ListenerArgument(2, "index", typeof(int), "The index at which the component was added to the container's items collection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("add", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after any Component is added or inserted into the contentContainer.")]
        public virtual ComponentAjaxEvent Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new ComponentAjaxEvent();
                }
                return this.add;
            }
        }

        private ComponentAjaxEvent afterLayout;

        /// <summary>
        /// Fires when the components in this contentContainer are arranged by the associated layout manager.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "layout", typeof(ContainerLayout), "The ContainerLayout implementation for this container")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("afterlayout", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the components in this contentContainer are arranged by the associated layout manager.")]
        public virtual ComponentAjaxEvent AfterLayout
        {
            get
            {
                if (this.afterLayout == null)
                {
                    this.afterLayout = new ComponentAjaxEvent();
                }
                return this.afterLayout;
            }
        }

        private ComponentAjaxEvent beforeAdd;

        /// <summary>
        /// Fires before any Component is added or inserted into the contentContainer. A handler can return false to cancel the add.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was added")]
        [ListenerArgument(2, "index", typeof(int), "The index at which the component was added to the container's items collection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeadd", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before any Component is added or inserted into the contentContainer. A handler can return false to cancel the add.")]
        public virtual ComponentAjaxEvent BeforeAdd
        {
            get
            {
                if (this.beforeAdd == null)
                {
                    this.beforeAdd = new ComponentAjaxEvent();
                }
                return this.beforeAdd;
            }
        }

        private ComponentAjaxEvent beforeRemove;

        /// <summary>
        /// Fires before any Component is removed from the contentContainer. A handler can return false to cancel the remove.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component being removed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeremove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before any Component is removed from the contentContainer. A handler can return false to cancel the remove.")]
        public virtual ComponentAjaxEvent BeforeRemove
        {
            get
            {
                if (this.beforeRemove == null)
                {
                    this.beforeRemove = new ComponentAjaxEvent();
                }
                return this.beforeRemove;
            }
        }

        private ComponentAjaxEvent remove;

        /// <summary>
        /// Fires after any Component is removed from the contentContainer.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Container), "this")]
        [ListenerArgument(1, "component", typeof(Component), "The component that was removed")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("remove", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after any Component is removed from the contentContainer.")]
        public virtual ComponentAjaxEvent Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new ComponentAjaxEvent();
                }
                return this.remove;
            }
        }
    }
}