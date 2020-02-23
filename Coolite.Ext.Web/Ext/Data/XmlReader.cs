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
    /// Data reader class to create an Array of Ext.data.Record objects from an XML document
    /// based on mappings in a provided Ext.data.Record constructor.
    ///
    /// Note that in order for the browser to parse a returned XML document, the Content-Type
    /// header in the HTTP response must be set to "text/xml".
    /// </summary>
    [InstanceOf(ClassName = "Ext.data.XmlReader")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class XmlReader : DataReader
    {
        /// <summary>
        /// The DomQuery path to the repeated element which contains record information.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The DomQuery path to the repeated element which contains record information.")]
        public virtual string Record
        {
            get
            {
                return (string)this.ViewState["Record"] ?? "";
            }
            set
            {
                this.ViewState["Record"] = value;
            }
        }

        /// <summary>
        /// The DomQuery path to the success attribute used by forms.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The DomQuery path to the success attribute used by forms.")]
        public virtual string Success
        {
            get
            {
                return (string)this.ViewState["Success"] ?? "";
            }
            set
            {
                this.ViewState["Success"] = value;
            }
        }

        /// <summary>
        /// The DomQuery path from which to retrieve the total number of records in the dataset.
        /// This is only needed if the whole dataset is not passed in one go, but is being paged
        /// from the remote server.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("The DomQuery path from which to retrieve the total number of records in the dataset. This is only needed if the whole dataset is not passed in one go, but is being paged from the remote server.")]
        public virtual string TotalRecords
        {
            get
            {
                return (string)this.ViewState["TotalRecords"] ?? "";
            }
            set
            {
                this.ViewState["TotalRecords"] = value;
            }
        }
    }
}
