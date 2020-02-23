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
    /// <summary>
    /// Abstract base class for reading structured data from a data source and
    /// converting it into an object containing RecordField objects and metadata
    /// for use by an Store. This class is intended to be extended and should
    /// not be created directly. For existing implementations, see ArrayReader,
    /// JsonReader and XmlReader.
    /// </summary>
    public abstract class DataReader : StateManagedItem
    {
        /// <summary>
        /// Name of the property within a row object that contains a record identifier value.
        /// </summary>
        [ClientConfig("id")]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Name of the property within a row object that contains a record identifier value.")]
        //If set name of this property as Id then VS think that this is ID of control and compiler error occurs if same Id is exists
        public virtual string ReaderID
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

        private RecordFieldCollection fields;

        /// <summary>
        /// An array of field definition objects
        /// </summary>
        [ClientConfig(JsonMode.AlwaysArray)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Either a Collection of RecordField definition objects")]
        [NotifyParentProperty(true)]
        public RecordFieldCollection Fields
        {
            get
            {
                if (fields == null)
                {
                    fields = new RecordFieldCollection();
                }
                return fields;
            }
        }

        [DefaultValue("")]
        [ClientConfig("fields", JsonMode.Raw)]
        protected string EmptyFields
        {
            get
            {
                if(this.Fields.Count == 0)
                {
                    return "[]";
                }

                return "";
            }
        }
    }
}
