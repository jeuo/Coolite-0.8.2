/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public abstract class CheckboxGroupBase : Field
    {
        [ClientConfig(JsonMode.Ignore)]
        [DefaultValue("")]
        public override string ItemCls
        {
            get { return base.ItemCls; }
            set { base.ItemCls = value; }
        }

        [ClientConfig("itemCls")]
        [DefaultValue("")]
        internal virtual string ItemClsProxy
        {
            get
            {
                return this.ItemCls + " x-form-cb-label-nowrap";
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("False to validate that at least one item in the group is checked (defaults to true). If no items are selected at validation time, BlankText will be used as the error text.")]
        public virtual bool AllowBlank
        {
            get
            {
                object obj = this.ViewState["AllowBlank"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["AllowBlank"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("Error text to display if the AllowBlank validation fails (defaults to 'You must select at least one item in this group')")]
        public virtual string BlankText
        {
            get
            {
                return (string)this.ViewState["BlankText"] ?? "";
            }
            set
            {
                this.ViewState["BlankText"] = value;
            }
        }

        [ClientConfig("columns")]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("Specifies the number of columns to use when displaying grouped checkbox/radio controls using automatic layout.")]
        public virtual int ColumnsNumber
        {
            get
            {
                object obj = this.ViewState["ColumnsNumber"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["ColumnsNumber"] = value;
            }
        }

        [ClientConfig("columns", typeof(StringArrayJsonConverter))]
        [TypeConverter(typeof(StringArrayConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [Description("You can also specify an array of column widths, mixing integer (fixed width) and float (percentage width) values as needed (e.g., [100, .25, .75]). Any integer values will be rendered first, then any float values will be calculated as a percentage of the remaining space. Float values do not have to add up to 1 (100%) although if you want the controls to take up the entire field container you should do so.")]
        public virtual string[] ColumnsWidths
        {
            get
            {
                object obj = this.ViewState["ColumnsWidths"];
                string[] widths =  (obj == null) ? null : (string[])obj;

                if(this.ColumnsNumber > 0 && widths != null && widths.Length > 0)
                {
                    throw new ArgumentException("Do not use both ColumnsNumber and ColumnsWidths");
                }

                return widths;
            }
            set
            {
                this.ViewState["ColumnsWidths"] = value;
            }
        }

        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to distribute contained controls across columns, completely filling each column top to bottom before starting on the next column. The number of controls in each column will be automatically calculated to keep columns as even as possible. The default value is false, so that controls will be added to columns one at a time, completely filling each row left to right before starting on the next row.")]
        public virtual bool Vertical
        {
            get
            {
                object obj = this.ViewState["Vertical"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Vertical"] = value;
            }
        }
    }
}