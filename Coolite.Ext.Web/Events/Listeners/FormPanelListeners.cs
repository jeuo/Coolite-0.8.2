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
    public class FormPanelListeners : PanelListeners
    {
        private ComponentListener clientValidation;

        /// <summary>
        /// If the monitorValid config option is true, this event fires repetitively to notify of valid state
        /// </summary>
        [ListenerArgument(0, "el", typeof(FormPanel))]
        [ListenerArgument(1, "valid")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("clientvalidation", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("If the monitorValid config option is true, this event fires repetitively to notify of valid state")]
        public virtual ComponentListener ClientValidation
        {
            get
            {
                if (this.clientValidation == null)
                {
                    this.clientValidation = new ComponentListener();
                }
                return this.clientValidation;
            }
        }

        private ComponentListener actionComplete;

        /// <summary>
        /// Fires when an action is completed.
        /// </summary>
        [ListenerArgument(0, "el", typeof(FormPanel))]
        [ListenerArgument(1, "action")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("actioncomplete", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when an action is completed.")]
        public virtual ComponentListener ActionComplete
        {
            get
            {
                if (this.actionComplete == null)
                {
                    this.actionComplete = new ComponentListener();
                }
                return this.actionComplete;
            }
        }

        private ComponentListener actionFailed;

        /// <summary>
        /// Fires when an action fails.
        /// </summary>
        [ListenerArgument(0, "el", typeof(FormPanel))]
        [ListenerArgument(1, "action")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("actionfailed", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when an action fails.")]
        public virtual ComponentListener ActionFailed
        {
            get
            {
                if (this.actionFailed == null)
                {
                    this.actionFailed = new ComponentListener();
                }
                return this.actionFailed;
            }
        }

        private ComponentListener beforeAction;

        /// <summary>
        /// Fires before any action is performed. Return false to cancel the action.
        /// </summary>
        [ListenerArgument(0, "el", typeof(FormPanel))]
        [ListenerArgument(1, "action")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeaction", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before any action is performed. Return false to cancel the action.")]
        public virtual ComponentListener BeforeAction
        {
            get
            {
                if (this.beforeAction == null)
                {
                    this.beforeAction = new ComponentListener();
                }
                return this.beforeAction;
            }
        }
    }
}