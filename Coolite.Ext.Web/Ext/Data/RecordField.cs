/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// The field definition object which specify field names, and optionally,
    /// data types, and a mapping for an Reader to extract the field's value
    /// from a data object.
    /// </summary>
    public class RecordField : StateManagedItem
    {
        private JFunction convert;
        public RecordField() { }

        public RecordField(string name)
        {
            this.Name = name;
        }

        public RecordField(string name, RecordFieldType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public RecordField(string name, RecordFieldType type, string dateFormat)
        {
            this.Name = name;
            this.Type = type;
            this.DateFormat = dateFormat;
        }

        /// <summary>
        /// The name by which the field is referenced within the Record.
        /// This is referenced by, for example the DataIndex property in
        /// column definition objects passed to ColumnModel
        /// </summary>
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The name by which the field is referenced within the Record. This is referenced by, for example the DataIndex property in column definition objects passed to ColumnModel")]
        public virtual string Name
        {
            get
            {
                object obj = this.ViewState["Name"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Name"] = value;
            }
        }

        [ClientConfig("name")]
        internal string NameProxy
        {
            get
            {
                return (string.IsNullOrEmpty(this.Name)) ? this.Mapping : this.Name;
            }
        }

        /// <summary>
        /// (Optional) A path specification for use by the Reader implementation
        /// that is creating the Record to access the data value from the data object.
        /// 
        /// If an JsonReader is being used, then this is a string containing the javascript
        /// expression to reference the data relative to the Record item's root.
        /// 
        /// If an XmlReader is being used, this is an Ext.DomQuery path to the data item
        /// relative to the Record element.
        /// 
        /// If the mapping expression is the same as the field name, this may be omitted.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(Optional) A path specification for use by the Reader implementation that is creating the Record to access the data value from the data object. If an JsonReader is being used, then this is a string containing the javascript expression to reference the data relative to the Record item's root. If an XmlReader is being used, this is an Ext.DomQuery path to the data item relative to the Record element. If the mapping expression is the same as the field name, this may be omitted.")]
        public virtual string Mapping
        {
            get
            {
                object obj = this.ViewState["Mapping"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["Mapping"] = value;
            }
        }

        [Category("Config Options")]
        [DefaultValue("")]
        public virtual string ServerMapping
        {
            get
            {
                object obj = this.ViewState["ServerMapping"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["ServerMapping"] = value;
            }
        }

        /// <summary>
        /// The data type for conversion to displayable value
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(RecordFieldType.Auto)]
        [Description("The data type for conversion to displayable value")]
        public virtual RecordFieldType Type
        {
            get
            {
                object obj = this.ViewState["Type"];
                return (obj == null) ? RecordFieldType.Auto : (RecordFieldType)obj;
            }
            set
            {
                this.ViewState["Type"] = value;
            }
        }

        /// <summary>
        /// Sort method
        /// </summary>
        [ClientConfig(typeof(ToLowerCamelCase))]
        [Category("Config Options")]
        [DefaultValue(SortTypeMethod.None)]
        [Description("Sort method")]
        public virtual SortTypeMethod SortType
        {
            get
            {
                object obj = this.ViewState["SortType"];
                return (obj == null) ? SortTypeMethod.None : (SortTypeMethod)obj;
            }
            set
            {
                this.ViewState["SortType"] = value;
            }
        }

        /// <summary>
        /// (Optional) Initial direction to sort
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(SortDirection.Default)]
        [Description("(Optional) Initial direction to sort")]
        public virtual SortDirection SortDir
        {
            get
            {
                object obj = this.ViewState["SortDir"];
                return (obj == null) ? SortDirection.Default : (SortDirection)obj;
            }
            set
            {
                this.ViewState["SortDir"] = value;
            }
        }

        /// <summary>
        /// (Optional) A function which converts the value provided by the Reader
        /// into an object that will be stored in the Record. 
        /// 
        /// It is passed the following parameters:
        ///    v : Mixed
        ///        The data value as read by the Reader.
        /// 
        ///    rec : Mixed
        ///          The data object containting the row as read by the Reader.
        ///          Depending on Reader type, this could be an Array, an object,
        ///          or an XML element.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("(Optional) A function which converts the value provided by the Reader into an object that will be stored in the Record.")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual JFunction Convert
        {
            get
            {
                if (this.convert == null)
                {
                    this.convert = new JFunction();
                    this.convert.Args = new string[] { "value", "record" };
                }
                return this.convert;
            }
        }

        /// <summary>
        /// (Optional) A format String for the Date.parseDate function
        /// </summary>
        [ClientConfig(typeof(NetToPHPDateFormatStringJsonConverter))]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(Optional) A format String for the Date.parseDate function")]
        public virtual string DateFormat
        {
            get
            {
                string temp = (string)this.ViewState["DateFormat"] ?? "";

                if (this.Type == RecordFieldType.Date && string.IsNullOrEmpty(temp))
                {
                    temp = "yyyy-MM-ddThh:mm:ss";
                }
                return temp;
            }
            set
            {
                this.ViewState["DateFormat"] = value;
            }
        }

        /// <summary>
        /// (Optional) The default value passed to the Reader when the field does not exist in the data object
        /// 
        /// Please pay attention that if you use string const then need wrap like this
        ///     DefaultValue="'String const'"
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("(Optional) The default value passed to the Reader when the field does not exist in the data object")]
        public virtual string DefaultValue
        {
            get
            {
                object obj = this.ViewState["DefaultValue"];
                return (obj == null) ? "" : (string)obj;
            }
            set
            {
                this.ViewState["DefaultValue"] = value;
            }
        }

        /// <summary>
        /// True to render this property as complex object
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render this property as complex object")]
        public virtual bool IsComplex
        {
            get
            {
                object obj = this.ViewState["IsComplex"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["IsComplex"] = value;
            }
        }

    }
}