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
    [ToolboxItem(false)]
    [TypeConverter(typeof(AjaxEventsConverter))]
    public abstract class ComponentBaseAjaxEvents : ComponentAjaxEvents
    {
        private ComponentAjaxEvent beforeDestroy;

        /// <summary>
        /// Fires before the component is destroyed. Return false to stop the destroy.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedestroy", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is destroyed. Return false to stop the destroy.")]
        public virtual ComponentAjaxEvent BeforeDestroy
        {
            get
            {
                if (this.beforeDestroy == null)
                {
                    this.beforeDestroy = new ComponentAjaxEvent();
                }
                return this.beforeDestroy;
            }
        }

        private ComponentAjaxEvent beforeHide;

        /// <summary>
        /// Fires before the component is hidden. Return false to stop the hide.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforehide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is hidden. Return false to stop the hide.")]
        public virtual ComponentAjaxEvent BeforeHide
        {
            get
            {
                if (this.beforeHide == null)
                {
                    this.beforeHide = new ComponentAjaxEvent();
                }
                return this.beforeHide;
            }
        }

        private ComponentAjaxEvent beforeRender;

        /// <summary>
        /// Fires before the component is rendered. Return false to stop the render.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerender", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is rendered. Return false to stop the render.")]
        public virtual ComponentAjaxEvent BeforeRender
        {
            get
            {
                if (this.beforeRender == null)
                {
                    this.beforeRender = new ComponentAjaxEvent();
                }
                return this.beforeRender;
            }
        }

        private ComponentAjaxEvent beforeShow;

        /// <summary>
        /// Fires before the component is shown. Return false to stop the show.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeshow", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is shown. Return false to stop the show.")]
        public virtual ComponentAjaxEvent BeforeShow
        {
            get
            {
                if (this.beforeShow == null)
                {
                    this.beforeShow = new ComponentAjaxEvent();
                }
                return this.beforeShow;
            }
        }

        private ComponentAjaxEvent beforeStateRestore;

        /// <summary>
        /// Fires before the state of the component is restored. Return false to stop the restore.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestaterestore", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the state of the component is restored. Return false to stop the restore.")]
        public virtual ComponentAjaxEvent BeforeStateRestore
        {
            get
            {
                if (this.beforeStateRestore == null)
                {
                    this.beforeStateRestore = new ComponentAjaxEvent();
                }
                return this.beforeStateRestore;
            }
        }

        private ComponentAjaxEvent beforeStateSave;

        /// <summary>
        /// Fires before the state of the component is saved to the configured state provider. Return false to stop the save.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestatesave", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the state of the component is saved to the configured state provider. Return false to stop the save.")]
        public virtual ComponentAjaxEvent BeforeStateSave
        {
            get
            {
                if (this.beforeStateSave == null)
                {
                    this.beforeStateSave = new ComponentAjaxEvent();
                }
                return this.beforeStateSave;
            }
        }

        private ComponentAjaxEvent destroy;

        /// <summary>
        /// Fires after the component is destroyed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("destroy", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is destroyed.")]
        public virtual ComponentAjaxEvent Destroy
        {
            get
            {
                if (this.destroy == null)
                {
                    this.destroy = new ComponentAjaxEvent();
                }
                return this.destroy;
            }
        }

        private ComponentAjaxEvent disable;

        /// <summary>
        /// Fires after the component is disabled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disable", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is disabled.")]
        public virtual ComponentAjaxEvent Disable
        {
            get
            {
                if (this.disable == null)
                {
                    this.disable = new ComponentAjaxEvent();
                }
                return this.disable;
            }
        }

        private ComponentAjaxEvent enable;

        /// <summary>
        /// Fires after the component is enabled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("enable", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is enabled.")]
        public virtual ComponentAjaxEvent Enable
        {
            get
            {
                if (this.enable == null)
                {
                    this.enable = new ComponentAjaxEvent();
                }
                return this.enable;
            }
        }

        private ComponentAjaxEvent hide;

        /// <summary>
        /// Fires after the component is hidden.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is hidden.")]
        public virtual ComponentAjaxEvent Hide
        {
            get
            {
                if (this.hide == null)
                {
                    this.hide = new ComponentAjaxEvent();
                }
                return this.hide;
            }
        }

        private ComponentAjaxEvent render;

        /// <summary>
        /// Fires after the component is rendered.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("render", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is rendered.")]
        public virtual ComponentAjaxEvent Render
        {
            get
            {
                if (this.render == null)
                {
                    this.render = new ComponentAjaxEvent();
                }
                return this.render;
            }
        }

        private ComponentAjaxEvent show;

        /// <summary>
        /// Fires after the component is shown.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("show", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is shown.")]
        public virtual ComponentAjaxEvent Show
        {
            get
            {
                if (this.show == null)
                {
                    this.show = new ComponentAjaxEvent();
                }
                return this.show;
            }
        }

        private ComponentAjaxEvent stateRestore;

        /// <summary>
        /// Fires after the state of the component is restored.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("staterestore", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the state of the component is restored.")]
        public virtual ComponentAjaxEvent StateRestore
        {
            get
            {
                if (this.stateRestore == null)
                {
                    this.stateRestore = new ComponentAjaxEvent();
                }
                return this.stateRestore;
            }
        }

        private ComponentAjaxEvent stateSave;

        /// <summary>
        /// Fires after the state of the component is saved to the configured state provider.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("statesave", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the state of the component is saved to the configured state provider.")]
        public virtual ComponentAjaxEvent StateSave
        {
            get
            {
                if (this.stateSave == null)
                {
                    this.stateSave = new ComponentAjaxEvent();
                }
                return this.stateSave;
            }
        }
    }
}