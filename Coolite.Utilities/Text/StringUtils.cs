/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Coolite.Utilities
{
    public static class StringUtils
    {
        public static string Concat(IEnumerable items)
        {
            return Concat(items, ",", "{0}");
        }

        public static string Concat(IEnumerable items, string separator)
        {
            return Concat(items, separator, "{0}");
        }

        public static string Concat(IEnumerable items, string separator, string template)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in items)
            {
                sb.Append(separator);
                sb.Append(string.Format(template, item.ToString()));
            }
            return RightOf(sb.ToString(), separator);
        }

        /// <summary>
        /// Chops one character from each end of string.
        /// </summary>
        public static string Chop(string value)
        {
            return Chop(value, 1);
        }

        /// <summary>
        /// Chops the specified number of characters from each end of string. 
        /// </summary>
        public static string Chop(string value, int characters)
        {
            return value.Substring(characters, value.Length - characters - 1);
        }

        /// <summary>
        /// MD5Hash's a string. 
        /// </summary>
        public static string ToMD5Hash(string value)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value.Trim());
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        /// <param name="text">The text to convert to sentence case</param>
        public static string ToTitleCase(string text)
        {
            return StringUtils.ToTitleCase(text.Split(' '));
        }

        /// <summary>
        /// Converts the first character of each word to Uppercase. Example: "the lazy dog" returns "The Lazy Dog"
        /// </summary>
        public static string ToTitleCase(string[] words)
        {
            for(int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        public static string ToLowerCamelCase(string value)
        {
            return value.Substring(0, 1).ToLower(CultureInfo.InvariantCulture) + value.Substring(1);
        }

        public static string ToLowerCamelCase(string[] values)
        {
            return ToLowerCamelCase(ToCamelCase(values));
        }

        public static string ToCamelCase(string value)
        {
            return value.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + value.Substring(1);
        }

        public static string ToCamelCase(string[] values, string separator)
        {
            string temp = string.Empty;
            foreach (string s in values)
            {
                temp += separator;
                temp += ToCamelCase(s);
            }
            return temp;
        }

        public static string ToCamelCase(string[] values)
        {
            return ToCamelCase(values, "");
        }

        /// <summary>
        /// Pad the left side of a string with characters to make the total length.
        /// </summary>
        public static string PadLeft(string src, char c, Int32 totalLength)
        {
            if (totalLength < src.Length)
                return src;
            return new String(c, totalLength - src.Length) + src;
        }

        /// <summary>
        /// Pad the right side of a string with a '0' if a single character.
        /// </summary>
        public static string PadRight(string src)
        {
            return PadRight(src, '0', 2);
        }

        /// <summary>
        /// Pad the right side of a string with characters to make the total length.
        /// </summary>
        public static string PadRight(string src, char c, Int32 totalLength)
        {
            if (totalLength < src.Length)
                return src;
            return src + new String(c, totalLength - src.Length);
        }

        /// <summary>
        /// Left of the first occurance of c
        /// </summary>
        public static string LeftOf(string src, char c)
        {
            int i = src.IndexOf(c);
            if (i == -1)
            {
                return src;
            }

            return src.Substring(0, i);
        }

        /// <summary>
        /// Left of the first occurance of text
        /// </summary>
        public static string LeftOf(string src, string text)
        {
            int i = src.IndexOf(text);
            if (i == -1)
            {
                return src;
            }

            return src.Substring(0, i);
        }

        /// <summary>
        /// Left of the n'th occurance of c
        /// </summary>
        public static string LeftOf(string src, char c, int n)
        {
            int i = -1;
            while (n != 0)
            {
                i = src.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return src;
                }
                --n;
            }
            return src.Substring(0, i);
        }

        /// <summary>
        /// Right of the first occurance of c
        /// </summary>
        public static string RightOf(string src, char c)
        {
            int i = src.IndexOf(c);
            if (i == -1)
            {
                return "";
            }
            return src.Substring(i + 1);
        }

        /// <summary>
        /// Right of the first occurance of text
        /// </summary>
        public static string RightOf(string src, string text)
        {
            int i = src.IndexOf(text);
            if (i == -1)
            {
                return "";
            }
            return src.Substring(i + text.Length);
        }

        /// <summary>
        /// Right of the n'th occurance of c
        /// </summary>
        public static string RightOf(string src, char c, int n)
        {
            int i = -1;
            while (n != 0)
            {
                i = src.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return "";
                }
                --n;
            }
            return src.Substring(i + 1);
        }

        /// <summary>
        /// Right of the n'th occurance of c
        /// </summary>
        public static string RightOf(string src, string c, int n)
        {
            int i = -1;
            while (n != 0)
            {
                i = src.IndexOf(c, i + 1);
                if (i == -1)
                {
                    return "";
                }
                --n;
            }
            return src.Substring(i + 1);
        }

        public static string LeftOfRightmostOf(string src, char c)
        {
            int i = src.LastIndexOf(c);
            if (i == -1)
            {
                return src;
            }
            return src.Substring(0, i);
        }

        public static string LeftOfRightmostOf(string src, string value)
        {
            int i = src.LastIndexOf(value);
            if (i == -1)
            {
                return src;
            }
            return src.Substring(0, i);
        }

        public static string RightOfRightmostOf(string src, char c)
        {
            int i = src.LastIndexOf(c);
            if (i == -1)
            {
                return src;
            }
            return src.Substring(i + 1);
        }

        public static string RightOfRightmostOf(string src, string value)
        {
            int i = src.LastIndexOf(value);
            if (i == -1)
            {
                return src;
            }
            return src.Substring(i + value.Length);
        }

        public static string ReplaceLastInstanceOf(string src, string oldValue, string newValue)
        {
            return string.Format("{0}{1}{2}", StringUtils.LeftOfRightmostOf(src, oldValue), newValue, StringUtils.RightOfRightmostOf(src, oldValue));
        }

        /// <summary>
        /// Accepts a string like "ArrowRotateClockwise" and returns "arrow_rotate_clockwise.png".
        /// </summary>
        public static string ToCharacterSeparatedFileName(string name, char separator, string extension)
        {
            MatchCollection match = Regex.Matches(name, @"([A-Z]+)[a-z]*|\d{1,}[a-z]{0,}");

            string temp = "";
            
            for (int i = 0; i < match.Count; i++)
            {
                if (i != 0)
                {
                    temp += separator;
                }
                temp += match[i].ToString().ToLower();
            }
            
            string format = (string.IsNullOrEmpty(extension)) ? "{0}{1}" : "{0}.{1}";
            
            return string.Format(format, temp, extension);
        }

        public static string Enquote(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            string t;

            //sb.Append('"');
            for (i = 0; i < len; i += 1)
            {
                char c = s[i];
                if ((c == '\\') || (c == '"') || (c == '>'))
                {
                    sb.Append('\\');
                    sb.Append(c);
                }
                else if (c == '\b')
                    sb.Append("\\b");
                else if (c == '\t')
                    sb.Append("\\t");
                else if (c == '\n')
                    sb.Append("\\n");
                else if (c == '\f')
                    sb.Append("\\f");
                else if (c == '\r')
                    sb.Append("\\r");
                else
                {
                    if (c < ' ')
                    {
                        //t = "000" + Integer.toHexString(c); 
                        string tmp = new string(c, 1);
                        t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
            //sb.Append('"');
            return sb.ToString();
        }

        public static string EnsureSemiColon(string text)
        {
            return (string.IsNullOrEmpty(text) || text.EndsWith(";")) ? text : string.Concat(text, ";");
        }
    }
}