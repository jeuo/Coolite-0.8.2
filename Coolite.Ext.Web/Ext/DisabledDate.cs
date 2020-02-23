/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.IO;
using Newtonsoft.Json;

namespace Coolite.Ext.Web
{
    public class DisabledDate : StateManagedItem
    {
        public DisabledDate() { }

        public DisabledDate(DateTime date)
        {
            this.Date = date;
        }

        public DisabledDate(string regex)
        {
            this.regex = regex;
        }

        public DisabledDate(int year, int month, int day)
        {
            this.Date = new DateTime(year,month,day);
        }

        private DateTime date;

        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value.Date; }
        }

        private string regex;

        public string RegEx
        {
            get { return this.regex; }
            set { this.regex = value; }
        }

        public string ToString(string format)
        {
            if(!string.IsNullOrEmpty(this.regex))
            {
                return this.regex;
            }

            if(this.Date == DateTime.MinValue)
            {
                throw new ArgumentException("The Date or RegEx must be specified for DisabledDate object.");
            }

            //clear time
            this.Date = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, 0,0,0,0);

            return this.Date.ToString(format);
        }
    }

    public class DisabledDateCollection : StateManagedCollection<DisabledDate>
    {
        private string format;

        internal string Format
        {
            get { return this.format?? "d"; }
            set { this.format = value; }
        }

        public override string ToString()
        {
            if(this.Count == 0)
            {
                return "";
            }

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);
            
            writer.WriteStartArray();

            foreach (DisabledDate disabledDate in this)
            {
                writer.WriteValue(disabledDate.ToString(this.Format));
            }

            writer.WriteEndArray();
            writer.Flush();

            return sw.GetStringBuilder().ToString();
        }
    }
}
