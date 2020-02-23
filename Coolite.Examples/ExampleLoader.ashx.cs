using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Coolite.Examples;
using Coolite.Ext.Web;
using Coolite.Utilities;

namespace Coolite.Examples
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ExampleLoader : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            string url = context.Request["url"];

            if(string.IsNullOrEmpty(url))
            {
                return;
            }

            //url = url.ToLower();

            if(!url.EndsWith("/"))
            {
                url = url + "/";
            }
            
            string examplesFolder = new Uri(HttpContext.Current.Request.Url, "Examples/").AbsolutePath.ToLower();
            if(!url.StartsWith(examplesFolder,true, CultureInfo.InvariantCulture))
            {
                url = examplesFolder.TrimEnd(new []{'/'}) + url;

                id = "e" + url.ToLower().GetHashCode().ToString();
            }

            string wId = context.Request["wId"];

            HttpRequest r = HttpContext.Current.Request;
            Uri uri = new Uri(r.Url.Scheme + "://" + r.Url.Authority + url, UriKind.Absolute);

            string path = context.Server.MapPath(uri.AbsolutePath);
            DirectoryInfo dir = new DirectoryInfo(path);

            ExampleConfig cfg = null;
            if (File.Exists(dir.FullName + "config.xml"))
            {
                cfg = new ExampleConfig(dir.FullName + "config.xml", false);
            }

            string tabs = BuildSourceTabs(id, cfg, dir);

            string script = string.Format("var w = Ext.getCmp({0});w.add({1});w.doLayout();", JSON.Serialize(wId), tabs);
            context.Response.Write(script);
        }

        readonly string[] excludeFolders = new[] { ".svn", "_svn" };
        readonly string[] excludeList = new[] { "config.xml" };
        readonly string[] excludeExtensions = new[] { ".png", ".jpg", ".gif", ".bmp" };

        private string BuildSourceTabs(string id, ExampleConfig cfg, DirectoryInfo dir)
        {
            List<string> files = cfg != null ? cfg.OuterFiles : new List<string>();

            FileInfo[] filesInfo = dir.GetFiles();
            List<FileInfo> fileList = new List<FileInfo>(filesInfo);

            int dIndex = 0;
            for (int ind = 0; ind < fileList.Count; ind++)
            {
                if(fileList[ind].Name.ToLower() == "default.aspx")
                {
                    dIndex = ind;
                }
            }

            if(dIndex>0)
            {
                FileInfo fi = fileList[dIndex];
                fileList.RemoveAt(dIndex);
                fileList.Insert(0, fi);
            }

            foreach (string file in files)
            {
                fileList.Add(new FileInfo(file));
            }

            DirectoryInfo[] resources = dir.GetDirectories("resources",SearchOption.TopDirectoryOnly);

            if(resources.Length > 0)
            {
                GetSubFiles(fileList, resources[0]); 
            }
            
            
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            sb.Append(string.Concat("id: \"tpw", id,"\","));
            sb.Append("xtype: \"tabpanel\",");
            sb.Append("border: false,");
            sb.Append("activeTab: 0,");
            sb.Append("items:[");

            int i = 0;
            foreach (FileInfo fileInfo in fileList)
            {
                if (excludeList.Contains(fileInfo.Name) || excludeExtensions.Contains(fileInfo.Extension.ToLower()))
                {
                    continue;
                }

                Panel panel = new Panel();
                panel.ID = "tptw" + id + i++;
                panel.Title = fileInfo.Name;
                panel.CustomConfig.Add(new ConfigItem("url", UIHelpers.PhysicalToVirtual(fileInfo.FullName),ParameterMode.Value));
                switch (fileInfo.Extension)
                {
                    case ".aspx":
                    case ".ascx":
                        panel.Icon = Icon.PageWhiteCode;
                        break;
                    case ".cs":
                        panel.Icon = Icon.PageWhiteCsharp;
                        break;
                    case ".xml":
                    case ".xsl":
                        panel.Icon = Icon.ScriptCodeRed;
                        break;
                    case ".js":
                        panel.Icon = Icon.Script;
                        break;
                    case ".css":
                        panel.Icon = Icon.Css;
                        break;
                }
                panel.AutoLoad.Url = UIHelpers.ApplicationRoot + "/GenerateSource.ashx";
                panel.AutoLoad.Mode = LoadMode.IFrame;
                panel.AutoLoad.Params.Add(new Parameter("f", UIHelpers.PhysicalToVirtual(fileInfo.FullName),ParameterMode.Value));
                panel.AutoLoad.ShowMask = true;

                sb.Append(new ClientConfig(true).Serialize(panel));
                sb.Append(",");
            }
            
            if(sb[sb.Length - 1] == ',')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("]");

            sb.Append("}");
            
            return sb.ToString();
        }

        private void GetSubFiles(List<FileInfo> fileList, DirectoryInfo dir)
        {
            FileInfo[] filesInfo = dir.GetFiles();

            foreach (FileInfo file in filesInfo)
            {
                if (excludeList.Contains(file.Name) || excludeExtensions.Contains(file.Extension.ToLower()))
                {
                    continue;
                }
                fileList.Add(file);
            }

            DirectoryInfo[] folders = dir.GetDirectories();
            foreach (DirectoryInfo folder in folders)
            {
                if(excludeFolders.Contains(folder.Name.ToLower()) || folder.Name.StartsWith("_"))
                {
                    continue;
                }

                GetSubFiles(fileList, folder);
            }
        }

        public bool IsReusable
        {

            get
            {
                return false;
            }
        }
    }
}
