/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class PropertyGridParameter : BaseParameter
    {
        public PropertyGridParameter() { }

        public PropertyGridParameter(string name, string value) : base(name, value) { }

        private EditorCollection editor;

        /// <summary>
        /// (optional) The Ext.form.Field to use when editing values in this column if editing is supported by the grid.
        /// </summary>
        //[ClientConfig("editor>Editor")]
        [Category("Config Options")]
        [NotifyParentProperty(true)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("(optional) The Ext.form.Field to use when editing values in this column if editing is supported by the grid.")]
        public virtual EditorCollection Editor
        {
            get
            {
                if (this.editor == null)
                {
                    editor = new EditorCollection();
                }

                return editor;
            }
        }

        private bool isChanged;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue(false)]
        public bool IsChanged
        {
            get { return this.isChanged; }
            internal set { this.isChanged = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", JSON.Serialize(Name), Mode == ParameterMode.Value ? JSON.Serialize(this.Value) : Value);
        }
    }

    public class PropertyGridParameterCollection : StateManagedCollection<PropertyGridParameter>
    {
        public PropertyGridParameter this[string name]
        {
            get 
            {
                return GetParameterByName(name);
            }
        }

        private PropertyGridParameter GetParameterByName(string name)
        {
            foreach (PropertyGridParameter parameter in this)
            {
                if(parameter.Name == name)
                {
                    return parameter;
                }
            }

            return null;
        }

        public string ToJsonObject()
        {
            if (this.Count == 0)
            {
                return "{}";
            }

            StringBuilder sb = new StringBuilder(256);
            sb.Append("{");
            foreach (PropertyGridParameter parameter in this)
            {
                sb.Append(string.Concat(parameter.ToString(), ","));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");

            return sb.ToString();
        }
    }
}