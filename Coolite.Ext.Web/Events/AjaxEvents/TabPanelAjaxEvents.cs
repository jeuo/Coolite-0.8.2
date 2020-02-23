/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class TabPanelAjaxEvents : ContainerAjaxEvents
    {
        private ComponentAjaxEvent beforeTabChange;

        /// <summary>
        /// Fires before the active tab changes. Handlers can return false to cancel the tab change.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "newTab", typeof(Panel), "The tab being activated")]
        [ListenerArgument(2, "currentTab", typeof(Panel), "The current active tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the active tab changes. Handlers can return false to cancel the tab change.")]
        public virtual ComponentAjaxEvent BeforeTabChange 
        {
            get
            {
                if (this.beforeTabChange == null)
                {
                    this.beforeTabChange = new ComponentAjaxEvent();
                }
                return this.beforeTabChange;
            }
        }

        private ComponentAjaxEvent contextMenu;

        /// <summary>
        /// Fires when the original browser contextmenu event originated from a tab element.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "tab", typeof(Panel), "The target tab")]
        [ListenerArgument(2, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the original browser contextmenu event originated from a tab element.")]
        public virtual ComponentAjaxEvent ContextMenu 
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentAjaxEvent();
                }
                return this.contextMenu;
            }
        }

        private ComponentAjaxEvent tabChange;

        /// <summary>
        /// Fires after the active tab has changed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "tab", typeof(Panel), "The new active tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("tabchange", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the active tab has changed.")]
        public virtual ComponentAjaxEvent TabChange 
        {
            get
            {
                if (this.tabChange == null)
                {
                    this.tabChange = new ComponentAjaxEvent();
                }
                return this.tabChange;
            }
        }

        private ComponentAjaxEvent beforeTabClose;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabclose", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeTabClose
        {
            get
            {
                if (this.beforeTabClose == null)
                {
                    this.beforeTabClose = new ComponentAjaxEvent();
                }
                return this.beforeTabClose;
            }
        }

        private ComponentAjaxEvent beforeTabHide;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabhide", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent BeforeTabHide
        {
            get
            {
                if (this.beforeTabHide == null)
                {
                    this.beforeTabHide = new ComponentAjaxEvent();
                }
                return this.beforeTabHide;
            }
        }

        private ComponentAjaxEvent tabClose;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("tabclose", typeof(AjaxEventJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentAjaxEvent TabClose
        {
            get
            {
                if (this.tabClose == null)
                {
                    this.tabClose = new ComponentAjaxEvent();
                }
                return this.tabClose;
            }
        }
    }
}