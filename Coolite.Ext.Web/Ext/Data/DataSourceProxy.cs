/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    [InstanceOf(ClassName = "Coolite.Ext.DataSourceProxy")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DataSourceProxy : DataProxy
    {
        private int totalCount;

        [DefaultValue(0)]
        public int TotalCount
        {
            get
            {
                return this.totalCount;   
            }
            set
            {
                this.totalCount = value;
            }
        }
    }
}
