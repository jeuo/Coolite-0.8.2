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
    public class TabPanelListeners : ContainerListeners
    {
        private ComponentListener beforeTabChange;

        /// <summary>
        /// Fires before the active tab changes. Handlers can return false to cancel the tab change.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "newTab", typeof(Panel), "The tab being activated")]
        [ListenerArgument(2, "currentTab", typeof(Panel), "The current active tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before the active tab changes. Handlers can return false to cancel the tab change.")]
        public virtual ComponentListener BeforeTabChange 
        {
            get
            {
                if (this.beforeTabChange == null)
                {
                    this.beforeTabChange = new ComponentListener();
                }
                return this.beforeTabChange;
            }
        }

        private ComponentListener contextMenu;

        /// <summary>
        /// Fires when the original browser contextmenu event originated from a tab element.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "tab", typeof(Panel), "The target tab")]
        [ListenerArgument(2, "e", typeof(object), "EventObject")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("contextmenu", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the original browser contextmenu event originated from a tab element.")]
        public virtual ComponentListener ContextMenu 
        {
            get
            {
                if (this.contextMenu == null)
                {
                    this.contextMenu = new ComponentListener();
                }
                return this.contextMenu;
            }
        }

        private ComponentListener tabChange;

        /// <summary>
        /// Fires after the active tab has changed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(TabPanel), "this")]
        [ListenerArgument(1, "tab", typeof(Panel), "The new active tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("tabchange", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires after the active tab has changed.")]
        public virtual ComponentListener TabChange 
        {
            get
            {
                if (this.tabChange == null)
                {
                    this.tabChange = new ComponentListener();
                }
                return this.tabChange;
            }
        }

        private ComponentListener beforeTabClose;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabclose", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeTabClose
        {
            get
            {
                if (this.beforeTabClose == null)
                {
                    this.beforeTabClose = new ComponentListener();
                }
                return this.beforeTabClose;
            }
        }

        private ComponentListener beforeTabHide;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforetabhide", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener BeforeTabHide
        {
            get
            {
                if (this.beforeTabHide == null)
                {
                    this.beforeTabHide = new ComponentListener();
                }
                return this.beforeTabHide;
            }
        }

        private ComponentListener tabClose;

        [ListenerArgument(0, "tab", typeof(Tab), "tab")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("tabclose", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public virtual ComponentListener TabClose
        {
            get
            {
                if (this.tabClose == null)
                {
                    this.tabClose = new ComponentListener();
                }
                return this.tabClose;
            }
        }

    }
}