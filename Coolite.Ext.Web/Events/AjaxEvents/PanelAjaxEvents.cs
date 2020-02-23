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
    public class PanelAjaxEvents : ContainerAjaxEvents
    {
        private ComponentAjaxEvent activate;

        /// <summary>
        /// Fires after the Panel has been visually activated. Note that Panels do not directly support being activated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been activated.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been visually activated. Note that Panels do not directly support being activated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.")]
        public virtual ComponentAjaxEvent Activate
        {
            get
            {
                if (this.activate == null)
                {
                    this.activate = new ComponentAjaxEvent();
                }
                return this.activate;
            }
        }

        private ComponentAjaxEvent beforeClose;

        /// <summary>
        /// Fires before the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window). This event only applies to such subclasses. A handler can return false to cancel the close.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel being closed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclose", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window). This event only applies to such subclasses. A handler can return false to cancel the close.")]
        public virtual ComponentAjaxEvent BeforeClose
        {
            get
            {
                if (this.beforeClose == null)
                {
                    this.beforeClose = new ComponentAjaxEvent();
                }
                return this.beforeClose;
            }
        }

        private ComponentAjaxEvent beforeCollapse;

        /// <summary>
        /// Fires before the Panel is collapsed. A handler can return false to cancel the collapse.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "the Panel being collapsed.")]
        [ListenerArgument(1, "animate", typeof(bool), "True if the collapse is animated, else false.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is collapsed. A handler can return false to cancel the collapse.")]
        public virtual ComponentAjaxEvent BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentAjaxEvent();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentAjaxEvent beforeExpand;

        /// <summary>
        /// Fires before the Panel is expanded. A handler can return false to cancel the expand.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been activated.")]
        [ListenerArgument(1, "animate", typeof(bool), "True if the expand is animated, else false.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is expanded. A handler can return false to cancel the expand.")]
        public virtual ComponentAjaxEvent BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentAjaxEvent();
                }
                return this.beforeExpand;
            }
        }

        private ComponentAjaxEvent bodyResize;

        /// <summary>
        /// Fires after the Panel has been resized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel which has been resized.")]
        [ListenerArgument(1, "width", typeof(int), "The Panel's new width.")]
        [ListenerArgument(2, "height", typeof(int), "The Panel's new height.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("bodyresize", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been resized.")]
        public virtual ComponentAjaxEvent BodyResize
        {
            get
            {
                if (this.bodyResize == null)
                {
                    this.bodyResize = new ComponentAjaxEvent();
                }
                return this.bodyResize;
            }
        }

        private ComponentAjaxEvent close;

        /// <summary>
        /// Fires after the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window).
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been closed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("close", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window).")]
        public virtual ComponentAjaxEvent Close
        {
            get
            {
                if (this.close == null)
                {
                    this.close = new ComponentAjaxEvent();
                }
                return this.close;
            }
        }

        private ComponentAjaxEvent collapse;

        /// <summary>
        /// Fires after the Panel has been collapsed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been collapsed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been collapsed.")]
        public virtual ComponentAjaxEvent Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentAjaxEvent();
                }
                return this.collapse;
            }
        }

        private ComponentAjaxEvent deactivate;

        /// <summary>
        /// Fires after the Panel has been visually deactivated. Note that Panels do not directly support being deactivated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been deactivated.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("deactivate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been visually deactivated. Note that Panels do not directly support being deactivated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.")]
        public virtual ComponentAjaxEvent Deactivate
        {
            get
            {
                if (this.deactivate == null)
                {
                    this.deactivate = new ComponentAjaxEvent();
                }
                return this.deactivate;
            }
        }

        private ComponentAjaxEvent expand;

        /// <summary>
        /// Fires after the Panel has been expanded.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been expanded.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been expanded.")]
        public virtual ComponentAjaxEvent Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentAjaxEvent();
                }
                return this.expand;
            }
        }

        private ComponentAjaxEvent titleChange;

        /// <summary>
        /// Fires after the Panel title has been set or changed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel which has had its title changed.")]
        [ListenerArgument(1, "title", typeof(string), "new title.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("titlechange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel title has been set or changed.")]
        public virtual ComponentAjaxEvent TitleChange
        {
            get
            {
                if (this.titleChange == null)
                {
                    this.titleChange = new ComponentAjaxEvent();
                }
                return this.titleChange;
            }
        }

        private ComponentAjaxEvent beforeUpdate;

        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "url", typeof(string), "url")]
        [ListenerArgument(2, "iframe", typeof(object), "iframe")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeupdate", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before iframe loading.")]
        public virtual ComponentAjaxEvent BeforeUpdate
        {
            get
            {
                if (this.beforeUpdate == null)
                {
                    this.beforeUpdate = new ComponentAjaxEvent();
                }
                return this.beforeUpdate;
            }
        }

        private ComponentAjaxEvent update;

        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "iframe", typeof(object), "iframe")]
        [ListenerArgument(2, "url", typeof(string), "url")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("update", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired after successful update is made.")]
        public virtual ComponentAjaxEvent Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new ComponentAjaxEvent();
                }
                return this.update;
            }
        }

        private ComponentAjaxEvent failure;

        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "iframe", typeof(object), "iframe")]
        [ListenerArgument(2, "url", typeof(string), "url")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("failure", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired on update failure.")]
        public virtual ComponentAjaxEvent Failure
        {
            get
            {
                if (this.failure == null)
                {
                    this.failure = new ComponentAjaxEvent();
                }
                return this.failure;
            }
        }
    }
}