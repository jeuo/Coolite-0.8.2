/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Text;

namespace Coolite.Ext.Web
{
    public class ParameterCollection : StateManagedCollection<Parameter>
    {
        private readonly bool camelNames;
        public ParameterCollection()
        {
        }

        public ParameterCollection(bool camelNames)
        {
            this.camelNames = camelNames;
        }

        public Parameter GetParameter(string name)
        {
            foreach (Parameter parameter in this)
            {
                if (parameter.Name == name)
                {
                    return parameter;
                }
            }

            return null;
        }

        public string this[string name]
        {
            get
            {
                foreach (Parameter parameter in this)
                {
                    if(parameter.Name == name)
                    {
                        return parameter.Value;
                    }
                }

                return null;
            }
            set
            {
                foreach (Parameter parameter in this)
                {
                    if (parameter.Name == name)
                    {
                        parameter.Value = value;
                        return;
                    }
                }

                this.Add(new Parameter(name, value));
            }
        }

        public string ToJson()
        {
            return this.ToJson(0);
        }

        //typeParams=0 - all
        //typeParams=1 - service(start, limit, sort, dir)
        //typeParams=2 - all except service
        internal string ToJson(int typeParams)
        {
            if (this.Count == 0)
            {
                return "{}";
            }

            StringBuilder sb = new StringBuilder(256);
            sb.Append("{");
            bool removeComma = false;
            foreach (Parameter parameter in this)
            {
                if (typeParams == 0 || (typeParams == 1 && this.IsService(parameter.Name)) || (typeParams == 2 && !this.IsService(parameter.Name)))
                {
                    sb.AppendFormat("{0},", TokenUtils.ParseTokens(parameter.ToString(camelNames), parameter.Owner));
                    removeComma = true;
                }
            }
            if (removeComma)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}");

            return sb.ToString();
        }

        private bool IsService(string name)
        {
            switch(name)
            {
                case "start":
                case "limit":
                case "sort":
                case "dir":
                    return true;
                default:
                    return false;
            }
        }
    }
}