/**
 * @version: 1.0.0
 * @author: Coolite Inc. http://www.coolite.com/
 * @date: 2008-05-26
 * @copyright: Copyright (c) 2006-2008, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license: See license.txt and http://www.coolite.com/license/. 
 * @website: http://www.coolite.com/
 */

using System;
using System.IO;
using System.Collections;

namespace Coolite.Utilities
{
    public class FileUtils
    {
        public static string ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                sr.Close();
                return text;
            }
        }

        public static void WriteFile(string path, string value)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Write(value);
            sw.Close();
        }

        public static void WriteToStart(string path, string value)
        {
            string temp = ReadFile(path);
            WriteFile(path, value.Trim() + temp.Trim());
        }

        public static void WriteToEnd(string path, string value)
        {
            string temp = ReadFile(path);
            WriteFile(path, temp.Trim() + value.Trim());
        }
    }
}
