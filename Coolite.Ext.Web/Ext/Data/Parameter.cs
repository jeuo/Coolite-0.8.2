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
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [DefaultProperty("Params")]
    [ParseChildren(true, "Params")]
    public abstract class BaseParameter: StateManagedItem
    {
        protected BaseParameter() { }

        protected BaseParameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        protected BaseParameter(string name, string value, ParameterMode mode) : this(name, value)
        {
            this.Mode = mode;
        }

        protected BaseParameter(string name, string value, bool encode) : this(name, value)
        {
            this.Encode = encode;
        }

        protected BaseParameter(string name, string value, ParameterMode mode, bool encode)
            : this(name, value)
        {
            this.Mode = mode;
            this.Encode = encode;
        }

        [ClientConfig]
        public string Name
        {
            get
            {
                return (string)this.ViewState["Name"] ?? "";
            }
            set
            {
                this.ViewState["Name"] = value;
            }
        }

        [ClientConfig]
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

        protected virtual ParameterMode DefaultMode
        {
             get
             {
                 return ParameterMode.Value;
             }  
        }

        /// <summary>
        /// Wrap in quote or not
        /// </summary>
        public ParameterMode Mode
        {
            get
            {
                object o = this.ViewState["Mode"];
                return o == null ? this.DefaultMode : (ParameterMode)o;
            }
            set
            {
                this.ViewState["Mode"] = value;
            }
        }

        /// <summary>
        /// Encode value, it might be useful when value is js object
        /// </summary>
        public bool Encode
        {
            get
            {
                object o = this.ViewState["Encode"];
                return o == null ? false : (bool)o;
            }
            set
            {
                this.ViewState["Encode"] = value;
            }
        }

        public string ToString(bool camelNames)
        {
            ParameterMode mode = this.Mode;
            string name = camelNames ? StringUtils.ToLowerCamelCase(this.Name) : this.Name;

            if(this.Params.Count > 0)
            {
                return this.ToStringInnerParams(name);
            }
            else
            {
                string script = TokenUtils.ParseTokens(this.Value, this.Owner);
                if (TokenUtils.IsRawToken(script))
                {
                    mode = ParameterMode.Raw;
                    script = TokenUtils.ReplaceRawToken(script);
                }
                return string.Concat(JSON.Serialize(name), ":", this.Encode ? "Ext.encode(" : "", mode == ParameterMode.Raw ? script : JSON.Serialize(script), this.Encode ? ")" : "");
            }
        }

        private string ToStringInnerParams(string name)
        {
            if(!string.IsNullOrEmpty(this.Value))
            {
                throw new Exception("The Value can't be used with Params in a Parameter object.");
            }

            StringBuilder sb = new StringBuilder();
            if(!string.IsNullOrEmpty(name))
            {
                sb.Append(name).Append(":");
            }
            sb.Append("{");

            foreach (Parameter parameter in this.Params)
            {
                sb.Append(parameter.ToString());
                sb.Append(",");
            }

            if (sb[sb.Length-1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.ToString(this.CamelName);
        }

        private bool camelName;

        internal bool CamelName
        {
            get
            {
                return this.camelName;
            }
            set
            {
                this.camelName = value;
            }
        }

        public string ValueToString()
        {
            ParameterMode mode = this.Mode;

            if (this.Params.Count > 0)
            {
                return this.ToStringInnerParams(null);
            }
            else
            {

                string script = TokenUtils.ParseTokens(this.Value, this.Owner);

                if (TokenUtils.IsRawToken(script))
                {
                    mode = ParameterMode.Raw;
                    script = TokenUtils.ReplaceRawToken(script);
                }

                return string.Concat(this.Encode ? "Ext.encode(" : "",
                                     mode == ParameterMode.Raw ? script : JSON.Serialize(script), this.Encode ? ")" : "");
            }
        }

        private ParameterCollection userParams;

        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public virtual ParameterCollection Params
        {
            get
            {
                if (this.userParams == null)
                {
                    this.userParams = new ParameterCollection();
                    this.userParams.Owner = this.Owner;
                }
                return this.userParams;
            }
        }
    }

    public class Parameter : BaseParameter
    {
        public Parameter()
        {
        }

        public Parameter(string name, string value) : base(name, value)
        {
        }

        public Parameter(string name, string value, ParameterMode mode) : base(name, value, mode)
        {
        }

        public Parameter(string name, string value, bool encode) : base(name, value, encode)
        {
        }

        public Parameter(string name, string value, ParameterMode mode, bool encode) : base(name, value, mode, encode)
        {
        }
    }

    public enum ParameterMode
    {
        Raw,
        Value
    }
}
