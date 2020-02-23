/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Data reader class to create an Array of Ext.data.Record objects from an Array.
    /// Each element of that Array represents a row of data fields. The fields are pulled
    /// into a Record object using as a subscript, the mapping property of the field
    /// definition if it exists, or the field's ordinal position in the definition.
    /// </summary>
    [InstanceOf(ClassName = "Ext.data.ArrayReader")]
    public class ArrayReader : JsonReader
    {
        /// <summary>
        /// Name of the property within a row object that contains a record identifier value.
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Name of the property within a row object that contains a record identifier value.")]
        public override string ReaderID
        {
            get
            {
                object obj = this.ViewState["ReaderID"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ReaderID"] = value;
            }
        }

        [ClientConfig("id")]
        [DefaultValue("")]
        internal string ReaderIDProxy
        {
            get
            {
                if(string.IsNullOrEmpty(this.ReaderID))
                {
                    return "";
                }

                int index;
                if(int.TryParse(this.ReaderID,out index))
                {
                    if(index < this.Fields.Count)
                    {
                        return index.ToString();
                    }
                    
                    throw new InvalidOperationException("Invalid index in the ReaderID of ArrayReader");
                }

                for (int i = 0; i < this.Fields.Count; i++)
                {
                    if(this.Fields[i].Name == this.ReaderID)
                    {
                        return i.ToString();
                    }
                }

                return "";
            }
        }
    }
}
