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
    public abstract class GridFilter : StateManagedItem
    {
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The Store data index of the field this filter represents. The dataIndex does not actually have to exist in the store.")]
        public virtual string DataIndex
        {
            get
            {
                object obj = this.ViewState["DataIndex"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["DataIndex"] = value;
            }
        }

        public abstract FilterType Type
        {
            get;
        }

        private GridPanel parentGrid;

        public GridPanel ParentGrid
        {
            get
            {
                return this.parentGrid;
            }
            set
            {
                this.parentGrid = value;
            }
        }

        public void SetActive(bool active)
        {
            Ext.EnsureAjaxEvent();
            if(this.ParentGrid != null)
            {
                this.ParentGrid.AddScript("{0}.getFilterPlugin().getFilter({1}).setActive({2});", this.ParentGrid.ClientID, JSON.Serialize(this.DataIndex), JSON.Serialize(active));
            }
        }
    }

    public class GridFilterCollection : StateManagedCollection<GridFilter>
    {
    }

    public enum FilterType
    {
        Boolean,
        Date,
        List,
        Numeric,
        String
    }
}
