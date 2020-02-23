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
    [TypeConverter(typeof(ListenersConverter))]
    public abstract class ComponentBaseListeners : ComponentListeners
    {
        private ComponentListener beforeDestroy;

        /// <summary>
        /// Fires before the component is destroyed. Return false to stop the destroy.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforedestroy", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is destroyed. Return false to stop the destroy.")]
        public virtual ComponentListener BeforeDestroy
        {
            get
            {
                if (this.beforeDestroy == null)
                {
                    this.beforeDestroy = new ComponentListener();
                }
                return this.beforeDestroy;
            }
        }

        private ComponentListener beforeHide;

        /// <summary>
        /// Fires before the component is hidden. Return false to stop the hide.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforehide", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is hidden. Return false to stop the hide.")]
        public virtual ComponentListener BeforeHide
        {
            get
            {
                if (this.beforeHide == null)
                {
                    this.beforeHide = new ComponentListener();
                }
                return this.beforeHide;
            }
        }

        private ComponentListener beforeRender;

        /// <summary>
        /// Fires before the component is rendered. Return false to stop the render.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforerender", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is rendered. Return false to stop the render.")]
        public virtual ComponentListener BeforeRender
        {
            get
            {
                if (this.beforeRender == null)
                {
                    this.beforeRender = new ComponentListener();
                }
                return this.beforeRender;
            }
        }

        private ComponentListener beforeShow;

        /// <summary>
        /// Fires before the component is shown. Return false to stop the show.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeshow", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the component is shown. Return false to stop the show.")]
        public virtual ComponentListener BeforeShow
        {
            get
            {
                if (this.beforeShow == null)
                {
                    this.beforeShow = new ComponentListener();
                }
                return this.beforeShow;
            }
        }

        private ComponentListener beforeStateRestore;

        /// <summary>
        /// Fires before the state of the component is restored. Return false to stop the restore.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestaterestore", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the state of the component is restored. Return false to stop the restore.")]
        public virtual ComponentListener BeforeStateRestore
        {
            get
            {
                if (this.beforeStateRestore == null)
                {
                    this.beforeStateRestore = new ComponentListener();
                }
                return this.beforeStateRestore;
            }
        }

        private ComponentListener beforeStateSave;

        /// <summary>
        /// Fires before the state of the component is saved to the configured state provider. Return false to stop the save.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforestatesave", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the state of the component is saved to the configured state provider. Return false to stop the save.")]
        public virtual ComponentListener BeforeStateSave
        {
            get
            {
                if (this.beforeStateSave == null)
                {
                    this.beforeStateSave = new ComponentListener();
                }
                return this.beforeStateSave;
            }
        }

        private ComponentListener destroy;

        /// <summary>
        /// Fires after the component is destroyed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("destroy", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is destroyed.")]
        public virtual ComponentListener Destroy
        {
            get
            {
                if (this.destroy == null)
                {
                    this.destroy = new ComponentListener();
                }
                return this.destroy;
            }
        }

        private ComponentListener disable;

        /// <summary>
        /// Fires after the component is disabled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("disable", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is disabled.")]
        public virtual ComponentListener Disable
        {
            get
            {
                if (this.disable == null)
                {
                    this.disable = new ComponentListener();
                }
                return this.disable;
            }
        }

        private ComponentListener enable;

        /// <summary>
        /// Fires after the component is enabled.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("enable", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is enabled.")]
        public virtual ComponentListener Enable
        {
            get
            {
                if (this.enable == null)
                {
                    this.enable = new ComponentListener();
                }
                return this.enable;
            }
        }

        private ComponentListener hide;

        /// <summary>
        /// Fires after the component is hidden.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("hide", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is hidden.")]
        public virtual ComponentListener Hide
        {
            get
            {
                if (this.hide == null)
                {
                    this.hide = new ComponentListener();
                }
                return this.hide;
            }
        }

        private ComponentListener render;

        /// <summary>
        /// Fires after the component is rendered.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("render", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is rendered.")]
        public virtual ComponentListener Render
        {
            get
            {
                if (this.render == null)
                {
                    this.render = new ComponentListener();
                }
                return this.render;
            }
        }

        private ComponentListener show;

        /// <summary>
        /// Fires after the component is shown.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("show", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the component is shown.")]
        public virtual ComponentListener Show
        {
            get
            {
                if (this.show == null)
                {
                    this.show = new ComponentListener();
                }
                return this.show;
            }
        }

        private ComponentListener stateRestore;

        /// <summary>
        /// Fires after the state of the component is restored.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("staterestore", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the state of the component is restored.")]
        public virtual ComponentListener StateRestore
        {
            get
            {
                if (this.stateRestore == null)
                {
                    this.stateRestore = new ComponentListener();
                }
                return this.stateRestore;
            }
        }

        private ComponentListener stateSave;

        /// <summary>
        /// Fires after the state of the component is saved to the configured state provider.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Component), "this")]
        [ListenerArgument(1, "state", typeof(object), "The hash of state values")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("statesave", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the state of the component is saved to the configured state provider.")]
        public virtual ComponentListener StateSave
        {
            get
            {
                if (this.stateSave == null)
                {
                    this.stateSave = new ComponentListener();
                }
                return this.stateSave;
            }
        }
    }
}