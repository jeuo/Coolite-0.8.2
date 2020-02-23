/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Coolite.Utilities
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Accepts a Unix/PHP date string format and returns a valid .NET date format
        /// </summary>
        /// <param name="format">The PHP format string</param>
        /// <returns>The format string converted to .NET DateTime format specifiers</returns>
        public static string ConvertPHPToNet(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return "";
            }

            StringBuilder final = new StringBuilder(128);
            string temp = "";
            Match m = Regex.Match(format, @"(%|\\)?.|%%", RegexOptions.IgnoreCase);

            while(m.Success)
            {
                temp = m.Value;

                if(temp.StartsWith(@"\") || temp.StartsWith("%%"))
                {
                    final.Append(temp.Replace(@"\", "").Replace("%%", "%"));
                }

                switch(temp)
                {
                    case "d":
                        final.Append("dd");
                        break;
                    case "D":
                        final.Append("ddd");
                        break;
                    case "j":
                        final.Append("d");
                        break;
                    case "l":
                        final.Append("dddd");
                        break;
                    case "F":
                        final.Append("MMMM");
                        break;
                    case "m":
                        final.Append("MM");
                        break;
                    case "M":
                        final.Append("MMM");
                        break;
                    case "n":
                        final.Append("M");
                        break;
                    case "Y":
                        final.Append("yyyy");
                        break;
                    case "y":
                        final.Append("yy");
                        break;
                    case "a":
                    case "A":
                        final.Append("tt");
                        break;
                    case "g":
                        final.Append("h");
                        break;
                    case "G":
                        final.Append("H");
                        break;
                    case "h":
                        final.Append("hh");
                        break;
                    case "H":
                        final.Append("HH");
                        break;
                    case "i":
                        final.Append("mm");
                        break;
                    case "s":
                        final.Append("ss");
                        break;
                    default:
                        final.Append(temp);
                        break;
                }
                m = m.NextMatch();
            }

            return final.ToString();
        }

        /// <summary>
        /// Accepts a Unix/PHP date string format and returns a valid .NET date format
        /// </summary>
        /// <param name="format">The .NET format string</param>
        /// <returns>The format string converted to PHP format specifiers</returns>
        public static string ConvertNetToPHP(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return "";
            }

            StringBuilder final = new StringBuilder(128);
            string temp = "";

            switch (format.Trim())
            {
                case "d":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    break;
                case "D":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;
                    break;
                case "t":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
                    break;
                case "T":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                    break;
            }

            Match m = Regex.Match(format, @"(\\)?(dd?d?d?|MM?M?M?|yy?y?y?|hh?|HH?|mm?|ss?|tt?|S)|.", RegexOptions.IgnoreCase);

            while (m.Success)
            {
                temp = m.Value;

                switch (temp)
                {
                    case "dd":
                        final.Append("d");
                        break;
                    case "ddd":
                        final.Append("D");
                        break;
                    case "d":
                        final.Append("j");
                        break;
                    case "dddd":
                        final.Append("l");
                        break;
                    case "MMMM":
                        final.Append("F");
                        break;
                    case "MM":
                        final.Append("m");
                        break;
                    case "MMM":
                        final.Append("M");
                        break;
                    case "M":
                        final.Append("n");
                        break;
                    case "yyyy":
                        final.Append("Y");
                        break;
                    case "yy":
                        final.Append("y");
                        break;
                    case "tt":
                        final.Append("a");
                        break;
                    case "h":
                        final.Append("g");
                        break;
                    case "H":
                        final.Append("G");
                        break;
                    case "hh":
                        final.Append("h");
                        break;
                    case "HH":
                        final.Append("H");
                        break;
                    case "mm":
                        final.Append("i");
                        break;
                    case "ss":
                        final.Append("s");
                        break;
                    default:
                        final.Append(temp);
                        break;
                }
                m = m.NextMatch();
            }

            return final.ToString();
        }

        /// <summary>
        /// Convert .NET DateTime to JavaScript object
        /// </summary>
        /// <param name="date">.NET DateTime</param>
        /// <returns>JavaScript Date as string</returns>
        public static string DateNetToJs(DateTime date)
        {
            DateTimeOffset value = new DateTimeOffset(date);
            DateTimeOffset utcDateTime = value.ToUniversalTime();
            long javaScriptTicks = (utcDateTime.Ticks - (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks) / (long)10000;

            string offset;
            TimeSpan utcOffset = value.Offset;
            offset = utcOffset.Hours.ToString("+00;-00", CultureInfo.InvariantCulture) + utcOffset.Minutes.ToString("00;00", CultureInfo.InvariantCulture);
            return string.Concat("new Date(", javaScriptTicks.ToString(CultureInfo.InvariantCulture), offset, ")");
        }

        public static DateTimeFormatInfo ClientDateTimeFormatInfo()
        {
            if(HttpContext.Current == null)
            {
                return null;
            }
            string lang = HttpContext.Current.Request.UserLanguages[0];

            if (lang != null)
            {
                if (lang.Length < 3)
                {
                    lang = lang + "-" + lang.ToUpper();
                }

                return new CultureInfo(lang).DateTimeFormat;
            }

            return null;
        }
    }
}