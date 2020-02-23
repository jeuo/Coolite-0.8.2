/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Text;

namespace Coolite.Ext.Web
{
    public class SelectedListItem : StateManagedItem
    {
        public SelectedListItem() { }

        public SelectedListItem(string text, string value, int index)
        {
            this.Value = value;
            this.Text = text;
            this.Index = index;
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string Text
        {
            get
            {
                return (string)this.ViewState["Text"] ?? "";
            }
            internal set
            {
                string oldValue = this.Text;
                this.ViewState["Text"] = value;
                if (string.IsNullOrEmpty(this.Value) || oldValue == this.Value)
                {
                    this.Value = value;
                }
            }
        }

        [DefaultValue("")]
        [NotifyParentProperty(true)]
        public string Value
        {
            get
            {
                return (string)this.ViewState["Value"] ?? "";
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        [DefaultValue("-1")]
        [NotifyParentProperty(true)]
        public int Index
        {
            get
            {
                object obj = this.ViewState["Index"];
                return obj == null  ? -1 : (int)obj;
            }
            set
            {
                this.ViewState["Index"] = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            SelectedListItem tmp = (SelectedListItem)obj;
            
            return this.Value == tmp.Value || this.Index == tmp.Index;
            
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode()*17 + this.Index;
        } 
    }



    public class SelectedListItemCollection : StateManagedCollection<SelectedListItem>
    {
        public string ValuesToJsonArray()
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append("[");
            bool needComma = false;
            foreach (SelectedListItem item in this)
            {
                if(string.IsNullOrEmpty(item.Value))
                {
                    continue;
                }
                
                if (needComma)
                {
                    sb.Append(",");
                }
                
                sb.Append(JSON.Serialize(item.Value));

                needComma = true;
            }

            sb.Append("]");

            return sb.ToString();
        }

        public string IndexesToJsonArray()
        {
            return this.IndexesToJsonArray(false);
        }
        
        public string IndexesToJsonArray(bool skipWithValue)
        {
            StringBuilder sb = new StringBuilder(128);
            sb.Append("[");
            bool needComma = false;
            foreach (SelectedListItem item in this)
            {
                if (item.Index < 0 || (skipWithValue && !string.IsNullOrEmpty(item.Value)))
                {
                    continue;
                }

                if (needComma)
                {
                    sb.Append(",");
                }

                sb.Append(item.Index);

                needComma = true;
            }

            sb.Append("]");

            return sb.ToString();
        }
    }
}
