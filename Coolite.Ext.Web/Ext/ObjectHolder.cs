/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    [ToolboxItem(false)]
    [Designer(typeof(EmptyDesigner))]
    public class ObjectHolder : Observable, ICustomConfigSerialization
    {
        private JsonObject items;

        public virtual JsonObject Items
        {
            get
            {
                if(this.items == null)
                {
                    this.items = new JsonObject();
                }

                return this.items;
            }
        }

        public virtual string Serialize(Control owner)
        {
            if(this.Items.Count == 0)
            {
                return "";
            }
            return string.Concat("this.", this.ClientID, "=", JSON.Serialize(this.Items),";");
        }

        public void Update()
        {
            Ext.EnsureAjaxEvent();
            this.AddScript("{0}={1};", this.ClientID, JSON.Serialize(this.Items));
        }
    }
}