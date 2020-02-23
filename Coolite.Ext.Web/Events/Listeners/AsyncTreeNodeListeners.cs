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
    public class AsyncTreeNodeListeners : TreeNodeListeners
    {
        private ComponentListener beforeLoad;

        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeload", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before this node is loaded, return false to cancel")]
        public virtual ComponentListener BeforeLoad
        {
            get
            {
                if (this.beforeLoad == null)
                {
                    this.beforeLoad = new ComponentListener();
                }
                return this.beforeLoad;
            }
        }

        private ComponentListener load;

        [ListenerArgument(0, "node")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("load", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when this node is loaded")]
        public virtual ComponentListener Load
        {
            get
            {
                if (this.load == null)
                {
                    this.load = new ComponentListener();
                }
                return this.load;
            }
        }
    }
}