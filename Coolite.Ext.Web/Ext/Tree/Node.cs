/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;

namespace Coolite.Ext.Web
{
    public abstract class Node : StateManagedItem
    {
        private static readonly object idCounter = new object();
            
        protected Node()
        {
            //if (HttpContext.Current != null)
            //{
            //    this.autoID = this.GetID().ToString();
            //}
        }
        
        public int GetID()
        {
            lock (idCounter)
            {
                object obj = HttpContext.Current.Items["_uniqueTreeNodeID"];
                int id = 0;

                if (obj != null)
                {
                    id = (int)obj;
                    id++;
                }

                HttpContext.Current.Items["_uniqueTreeNodeID"] = id;

                return id;
            }
        }


        private readonly string autoID="";

        [ClientConfig("id")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The id for this node. If one is not specified, one is generated.")]
        public virtual string NodeID
        {
            get
            {
                return (string)this.ViewState["NodeID"] ?? this.autoID;
            }
            set { this.ViewState["NodeID"] = value; }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True if this node is a leaf and does not have children")]
        [NotifyParentProperty(true)]
        public virtual bool Leaf
        {
            get
            {
                object obj = this.ViewState["Leaf"];
                return (obj == null) ? false : (bool) obj;
            }
            set { this.ViewState["Leaf"] = value; }
        }
    }
}