/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    public class Paging<T>
    {
        private List<T> data;
        private int totalRecords;

        public Paging() { }

        public Paging(IEnumerable<T> data, int totalRecords)
        {
            this.data = new List<T>(data);
            this.totalRecords = totalRecords;
        }

        public List<T> Data
        {
            get { return data; }
            set { data = value; }
        }

        public int TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }
    }
}