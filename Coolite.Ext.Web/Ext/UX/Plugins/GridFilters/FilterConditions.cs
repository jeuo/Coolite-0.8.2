/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace Coolite.Ext.Web
{
    public class FilterConditions
    {
        const string fieldPattern = @"f_([\d]+)_field";
        const string dataTypePattern = @"f_([\d]+)_data_type";
        const string dataComparisonPattern = @"f_([\d]+)_data_comparison";
        const string dataValuePattern = @"f_([\d]+)_data_value";
        
        private string filtersXmlStr;

        public FilterConditions(string filtersXmlStr)
        {
            this.filtersXmlStr = filtersXmlStr;
            this.ParseConditions();
        }

        private void ParseConditions()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(string.Concat("<filters>", this.filtersXmlStr, "</filters>"));
            this.conditions = new FilterConditionCollection();
            foreach (XmlNode node in xml.SelectSingleNode("filters").ChildNodes)
            {
                string name = node.Name;

                int value;
                if(this.CheckPattern(fieldPattern, name, out value))
                {
                    FilterCondition condition = this.GetCondition(value);
                    condition.Name = node.InnerText;
                    continue;
                }

                if (this.CheckPattern(dataTypePattern, name, out value))
                {
                    FilterCondition condition = this.GetCondition(value);
                    condition.FilterType = (FilterType)Enum.Parse(typeof(FilterType), node.InnerText, true);
                    continue;
                }

                if (this.CheckPattern(dataComparisonPattern, name, out value))
                {
                    FilterCondition condition = this.GetCondition(value);
                    condition.Comparison = (Comparison)Enum.Parse(typeof(Comparison), node.InnerText, true);
                    continue;
                }

                if (this.CheckPattern(dataValuePattern, name, out value))
                {
                    FilterCondition condition = this.GetCondition(value);
                    condition.Value = node.InnerText; 

                    continue;
                }
            }

            this.conditions.Sort(delegate(FilterCondition x, FilterCondition y)
                                     {
                                         if (x == null)
                                         {
                                             return y == null ? 0 : -1;
                                         }
                                         else
                                         {
                                             return y == null ? 1 : x.Id.CompareTo(y.Id);
                                         }
                                     });
        }

        private FilterCondition GetCondition(int id)
        {
            foreach (FilterCondition condition in this.Conditions)
            {
                if(condition.Id == id)
                {
                    return condition;
                }
            }

            FilterCondition newCondition = new FilterCondition(id);
            this.conditions.Add(newCondition);
            return newCondition;
        }

        private bool CheckPattern(string pattern, string str, out int id)
        {
            id = -1;

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(str);
            foreach (Match match in matches)
            {
                if(match.Success)
                {
                    id = int.Parse(match.Groups[1].Value);
                    return true;
                }
            }

            return false;
        }

        private FilterConditionCollection conditions;

        public ReadOnlyCollection<FilterCondition> Conditions
        {
            get
            {
                if (conditions == null)
                {
                    conditions = new FilterConditionCollection();
                }
                return conditions.AsReadOnly();
            }
        }
    }

    public class FilterConditionCollection : List<FilterCondition>
    {
    }

    public class FilterCondition
    {
        private int id;

        internal FilterCondition(int id)
        {
            this.id = id;
        }

        internal int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string fieldName;
        private FilterType filterType;
        private Comparison comparison = Comparison.Eq;
        private string value;
        private List<string> valuesList;

        public string Name
        {
            get { return fieldName; }
            internal set { fieldName = value; }
        }

        public FilterType FilterType
        {
            get { return filterType; }
            internal set { filterType = value; }
        }

        public Comparison Comparison
        {
            get { return comparison; }
            internal set { comparison = value; }
        }

        public string Value
        {
            get { return value; }
            internal set
            {
                if(this.FilterType == FilterType.List || this.value != null)
                {
                    if(this.valuesList == null)
                    {
                        this.valuesList = new List<string>();    
                    }

                    if(this.value != null)
                    {
                        this.valuesList.Add(this.value);
                        this.value = null;
                    }

                    this.valuesList.Add(value);
                    return;
                }
                this.value = value;
            }
        }

        public DateTime ValueAsDate
        {
            get
            {
                //IFormatProvider culture = new CultureInfo("en-US", true);
                //return DateTime.Parse(this.Value, culture);
                return DateTime.ParseExact(this.Value, "d", System.Threading.Thread.CurrentThread.CurrentCulture);
            }
        }

        public DateTime ValueAsDateF(string format)
        {
            return DateTime.ParseExact(this.Value, format, System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        public bool ValueAsBoolean
        {
            get
            {
                if(this.Value == "1" || this.Value == "true" || this.Value == "True" || this.Value == "Yes" || this.Value == "yes")
                {
                    return true;
                }

                if (this.Value == "0" || this.Value == "false" || this.Value == "False" || this.Value == "No" || this.Value == "no")
                {
                    return false;
                }

                throw new ArgumentException(string.Concat("The value '",this.Value,"' can't be parsed to bool"));
            }
        }

        public int ValueAsInt
        {
            get
            {
                return int.Parse(this.Value);
            }
        }

        public double ValueAsDouble
        {
            get
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return Double.Parse(this.Value, culture);
            }
        }

        
        //If int then return int
        //else if double then return double
        public object ValueAsNumeric
        {
            get
            {
                int vi;
                if (int.TryParse(this.Value, out vi))
                {
                    return vi;
                }

                return this.ValueAsDouble;
            }
        }

        public ReadOnlyCollection<string> ValuesList
        {
            get
            {
                if(this.FilterType != FilterType.List)
                {
                    throw new InvalidOperationException("The filter type is not List");
                }

                return this.valuesList.AsReadOnly();
            }
        }
    }

    public enum Comparison
    {
        Eq,
        Gt,
        Lt
    }
    
}
