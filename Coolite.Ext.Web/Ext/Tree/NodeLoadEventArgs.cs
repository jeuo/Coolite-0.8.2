/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;

namespace Coolite.Ext.Web
{
    public class NodeLoadEventArgs : EventArgs
    {
        private readonly ParameterCollection extraParams;

        public NodeLoadEventArgs(ParameterCollection extraParams)
        {
            this.extraParams = extraParams;
        }

        public ParameterCollection ExtraParams
        {
            get
            {
                return this.extraParams;
            }
        }

        public string NodeID
        {
            get
            {
                return this.ExtraParams["node"];
            }
        }

        private TreeNodeCollection nodes;

        public TreeNodeCollection Nodes
        {
            get
            {
                if(this.nodes == null)
                {
                    this.nodes = new TreeNodeCollection(false);
                }
                return this.nodes;
            }
            set
            {
                this.nodes = value;
            }
        }

        public bool Success
        {
            get { return ScriptManager.AjaxSuccess; }
            set { ScriptManager.AjaxSuccess = value; }
        }

        public string ErrorMessage
        {
            get { return ScriptManager.AjaxErrorMessage; }
            set { ScriptManager.AjaxErrorMessage = value; }
        }
    }
}