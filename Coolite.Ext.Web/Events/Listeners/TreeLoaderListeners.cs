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
    public class TreeLoaderListeners : ComponentListeners
    {
        private ComponentListener beforeLoad;

        [ListenerArgument(0, "loader", typeof(TreeLoader), "this")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "callback")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("beforeload", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires before a network request is made to retrieve the Json text which specifies a node's children.")]
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

        [ListenerArgument(0, "loader", typeof(TreeLoader), "this")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "response")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("load", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the node has been successfuly loaded.")]
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

        private ComponentListener loadException;

        [ListenerArgument(0, "loader", typeof(TreeLoader), "this")]
        [ListenerArgument(1, "node", typeof(Node))]
        [ListenerArgument(2, "response")]
        [ListenerArgument(3, "errorMessage")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("loadexception", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires if the network request failed.")]
        public virtual ComponentListener LoadException
        {
            get
            {
                if (this.loadException == null)
                {
                    this.loadException = new ComponentListener();
                }
                return this.loadException;
            }
        }
    }
}