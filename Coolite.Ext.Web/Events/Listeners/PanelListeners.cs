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
    public class PanelListeners : ContainerListeners
    {
        private ComponentListener activate;

        /// <summary>
        /// Fires after the Panel has been visually activated. Note that Panels do not directly support being activated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been activated.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("activate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been visually activated. Note that Panels do not directly support being activated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.")]
        public virtual ComponentListener Activate
        {
            get
            {
                if (this.activate == null)
                {
                    this.activate = new ComponentListener();
                }
                return this.activate;
            }
        }

        private ComponentListener beforeClose;

        /// <summary>
        /// Fires before the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window). This event only applies to such subclasses. A handler can return false to cancel the close.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel being closed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeclose", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window). This event only applies to such subclasses. A handler can return false to cancel the close.")]
        public virtual ComponentListener BeforeClose
        {
            get
            {
                if (this.beforeClose == null)
                {
                    this.beforeClose = new ComponentListener();
                }
                return this.beforeClose;
            }
        }

        private ComponentListener beforeCollapse;

        /// <summary>
        /// Fires before the Panel is collapsed. A handler can return false to cancel the collapse.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "the Panel being collapsed.")]
        [ListenerArgument(1, "animate", typeof(bool), "True if the collapse is animated, else false.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforecollapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is collapsed. A handler can return false to cancel the collapse.")]
        public virtual ComponentListener BeforeCollapse
        {
            get
            {
                if (this.beforeCollapse == null)
                {
                    this.beforeCollapse = new ComponentListener();
                }
                return this.beforeCollapse;
            }
        }

        private ComponentListener beforeExpand;

        /// <summary>
        /// Fires before the Panel is expanded. A handler can return false to cancel the expand.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been activated.")]
        [ListenerArgument(1, "animate", typeof(bool), "True if the expand is animated, else false.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeexpand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the Panel is expanded. A handler can return false to cancel the expand.")]
        public virtual ComponentListener BeforeExpand
        {
            get
            {
                if (this.beforeExpand == null)
                {
                    this.beforeExpand = new ComponentListener();
                }
                return this.beforeExpand;
            }
        }

        private ComponentListener bodyResize;

        /// <summary>
        /// Fires after the Panel has been resized.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel which has been resized.")]
        [ListenerArgument(1, "width", typeof(int), "The Panel's new width.")]
        [ListenerArgument(2, "height", typeof(int), "The Panel's new height.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("bodyresize", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been resized.")]
        public virtual ComponentListener BodyResize
        {
            get
            {
                if (this.bodyResize == null)
                {
                    this.bodyResize = new ComponentListener();
                }
                return this.bodyResize;
            }
        }

        private ComponentListener close;

        /// <summary>
        /// Fires after the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window).
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been closed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("close", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel is closed. Note that Panels do not directly support being closed, but some Panel subclasses do (like Ext.Window).")]
        public virtual ComponentListener Close
        {
            get
            {
                if (this.close == null)
                {
                    this.close = new ComponentListener();
                }
                return this.close;
            }
        }

        private ComponentListener collapse;

        /// <summary>
        /// Fires after the Panel has been collapsed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been collapsed.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("collapse", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been collapsed.")]
        public virtual ComponentListener Collapse
        {
            get
            {
                if (this.collapse == null)
                {
                    this.collapse = new ComponentListener();
                }
                return this.collapse;
            }
        }

        private ComponentListener deactivate;

        /// <summary>
        /// Fires after the Panel has been visually deactivated. Note that Panels do not directly support being deactivated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been deactivated.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("deactivate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been visually deactivated. Note that Panels do not directly support being deactivated, but some Panel subclasses do (like Ext.Window). Panels which are child Components of a TabPanel fire the activate and deactivate events under the control of the TabPanel.")]
        public virtual ComponentListener Deactivate
        {
            get
            {
                if (this.deactivate == null)
                {
                    this.deactivate = new ComponentListener();
                }
                return this.deactivate;
            }
        }

        private ComponentListener expand;

        /// <summary>
        /// Fires after the Panel has been expanded.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel that has been expanded.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("expand", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel has been expanded.")]
        public virtual ComponentListener Expand
        {
            get
            {
                if (this.expand == null)
                {
                    this.expand = new ComponentListener();
                }
                return this.expand;
            }
        }

        private ComponentListener titleChange;

        /// <summary>
        /// Fires after the Panel title has been set or changed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Panel), "The Panel which has had its title changed.")]
        [ListenerArgument(1, "title", typeof(string), "new title.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("titlechange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the Panel title has been set or changed.")]
        public virtual ComponentListener TitleChange
        {
            get
            {
                if (this.titleChange == null)
                {
                    this.titleChange = new ComponentListener();
                }
                return this.titleChange;
            }
        }

        private ComponentListener beforeUpdate;

        //e: {url, iframe, params}
        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "e", typeof(string), "event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeupdate", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before iframe loading.")]
        public virtual ComponentListener BeforeUpdate
        {
            get
            {
                if (this.beforeUpdate == null)
                {
                    this.beforeUpdate = new ComponentListener();
                }
                return this.beforeUpdate;
            }
        }

        private ComponentListener update;

        //e: {url, iframe, params, response}
        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "e", typeof(string), "event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("update", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired after successful update is made.")]
        public virtual ComponentListener Update
        {
            get
            {
                if (this.update == null)
                {
                    this.update = new ComponentListener();
                }
                return this.update;
            }
        }

        private ComponentListener failure;

        //e: {url, iframe, params, response}
        [ListenerArgument(0, "el", typeof(Panel), "this")]
        [ListenerArgument(1, "e", typeof(string), "event object")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("failure", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fired on update failure.")]
        public virtual ComponentListener Failure
        {
            get
            {
                if (this.failure == null)
                {
                    this.failure = new ComponentListener();
                }
                return this.failure;
            }
        }
    }
}