/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Xml;

namespace Coolite.Ext.Web
{
    public class StoreRefreshDataEventArgs : EventArgs
    {
        private readonly XmlNode parameters;

        public StoreRefreshDataEventArgs()
        {
        }

        internal StoreRefreshDataEventArgs(XmlNode parameters)
        {
            this.parameters = parameters;
        }

        private int totalCount = -1;
        public int TotalCount
        {
           get
           {
               return totalCount;
           }
            set
            {
                totalCount = value;
            }
        }

        private ParameterCollection p;
        public ParameterCollection Parameters
        {
            get
            {
                if (p != null)
                {
                    return p;
                }

                if (this.parameters == null)
                {
                    return new ParameterCollection();
                }

                p = ScriptManager.XmlToParams(this.parameters);

                return p;
            }
        }

        public int Start
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Parameters["start"]))
                {
                    return int.Parse(this.Parameters["start"]);
                }

                return -1;
            }
        }

        public int Limit
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Parameters["limit"]))
                {
                    return int.Parse(this.Parameters["limit"]);
                }

                return -1;
            }
        }

        public string Sort
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Parameters["sort"]))
                {
                    return this.Parameters["sort"];
                }

                return "";
            }
        }

        public SortDirection Dir
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Parameters["dir"]))
                {
                    return (SortDirection)Enum.Parse(typeof(SortDirection), this.Parameters["dir"], true);
                }

                return SortDirection.Default;
            }
        }
    }
}
