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
using System.IO.Compression;
using System.Text;
using System.Web;

namespace Coolite.Utilities
{
    public class CompressionUtils
    {
        public static void GZipAndSend(object src)
        {
            CompressionUtils.GZipAndSend((src != null) ? src.ToString() : "");
        }

        public static void GZipAndSend(string src)
        {
            CompressionUtils.GZipAndSend(src, "application/json");
        }

        public static void GZipAndSend(string src, string responseType)
        {
            CompressionUtils.GZipAndSend(Encoding.UTF8.GetBytes(src), responseType);
        }

        public static void GZipAndSend(byte[] src, string responseType)
        {
            CompressionUtils.Send(CompressionUtils.GZip(src), responseType);
        }

        public static void Send(byte[] src, string responseType)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.AppendHeader("Content-Encoding", "gzip");
            response.Charset = "utf-8";
            response.ContentType = responseType;
            response.BinaryWrite(src);
        }

        public static byte[] GZip(string src)
        {
            return GZip(Encoding.UTF8.GetBytes(src));
        }

        public static byte[] GZip(byte[] src)
        {
            MemoryStream stream = new MemoryStream();
            GZipStream zipstream = new GZipStream(stream, CompressionMode.Compress);
            zipstream.Write(src, 0, src.Length);
            zipstream.Close();
            return stream.ToArray();
        }

        public static bool IsGZipSupported
        {
            get
            {
                string encoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
                return (!string.IsNullOrEmpty(encoding) && encoding.ToLower().Contains("gzip"));
            }
        }
    }
}
