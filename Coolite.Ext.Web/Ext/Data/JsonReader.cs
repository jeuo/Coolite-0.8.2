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
    /// <summary>
    /// Data reader class to create an Array of Ext.data.Record objects from a JSON response based on mappings in a provided Ext.data.Record constructor.
    /// </summary>
    [InstanceOf(ClassName = "Ext.data.JsonReader")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class JsonReader : DataReader
    {
        /// <summary>
        /// Name of the property which contains the Array of row objects.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Name of the property which contains the Array of row objects.")]
        public virtual string Root
        {
            get
            {
                object obj = this.ViewState["Root"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Root"] = value;
            }
        }

        /// <summary>
        /// Name of the property from which to retrieve the success attribute used by forms.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Name of the property from which to retrieve the success attribute used by forms.")]
        public virtual string SuccessProperty
        {
            get
            {
                object obj = this.ViewState["SuccessProperty"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                this.ViewState["SuccessProperty"] = value;
            }
        }

        /// <summary>
        /// Name of the property from which to retrieve the total number of records in the dataset.
        /// This is only needed if the whole dataset is not passed in one go, but is being paged
        /// from the remote server.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The DomQuery path from which to retrieve the total number of records in the dataset. This is only needed if the whole dataset is not passed in one go, but is being paged from the remote server.")]
        public virtual string TotalProperty
        {
            get
            {
                object obj = this.ViewState["TotalProperty"];
                return obj == null ? "" : (string)obj;
            }
            set
            {
                this.ViewState["TotalProperty"] = value;
            }
        }
    }
}
