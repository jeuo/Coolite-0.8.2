/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;

namespace Coolite.Ext.Web
{
    public class ChangeRecords<T>
    {
        private List<T> deleted;
        private List<T> updated;
        private List<T> created;

        public List<T> Deleted
        {
            get
            {
                if(deleted == null)
                {
                    deleted = new List<T>();
                }
                return deleted;
            }
            set { deleted = value; }
        }

        public List<T> Updated
        {
            get
            {
                if (updated == null)
                {
                    updated = new List<T>();
                }
                return updated;
            }
            set { updated = value; }
        }

        public List<T> Created
        {
            get
            {
                if (created == null)
                {
                    created = new List<T>();
                }
                return created;
            }
            set { created = value; }
        }
    }
}