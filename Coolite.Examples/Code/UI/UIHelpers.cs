using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Coolite.Ext.Web;

namespace Coolite.Examples
{
    public class UIHelpers
    {
        public static TreeNodeCollection BuildTreeNodes(bool refreshSiteMap)
        {
            XmlDocument map = null;
            XmlElement root = null;
            XmlElement examplesNode = null;
            if (refreshSiteMap)
            {
                map = new XmlDocument();
                XmlDeclaration dec = map.CreateXmlDeclaration("1.0", "utf-8", null);
                map.AppendChild(dec);

                root = map.CreateElement("siteMap");
                root.SetAttribute("xmlns", "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0");
                map.AppendChild(root);

                examplesNode = map.CreateElement("siteMapNode");
                examplesNode.SetAttribute("title", "Examples");
                root.AppendChild(examplesNode);
            }

            string path = HttpContext.Current.Server.MapPath("~/Examples/");
            TreeNodeCollection result = BuildTreeLevel(new DirectoryInfo(path), 1, 3, examplesNode);
            if(root != null && root.ChildNodes.Count > 0)
            {
                map.Save(HttpContext.Current.Server.MapPath("Web.sitemap"));
            }
            return result;
        }

        public static string ApplicationRoot
        {
            get
            {
                string root = HttpContext.Current.Request.ApplicationPath;
                return root == "/" ? "" : root;
            }
        }

        private static readonly string[] excludeList = { ".svn", "_svn", "Shared" };
        private static TreeNodeCollection BuildTreeLevel(DirectoryInfo root, int level, int maxLevel, XmlElement siteMap)
        {
            DirectoryInfo[] folders = root.GetDirectories();

            folders = SortFolders(root, folders);

            TreeNodeCollection nodes = new TreeNodeCollection(false);
            foreach (DirectoryInfo folder in folders)
            {
                if ((folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ||
                    excludeList.Contains(folder.Name) || folder.Name.StartsWith("_"))
                {
                    continue;
                }

                ExampleConfig cfg = new ExampleConfig(folder.FullName + "\\config.xml", false);

                string iconCls = string.IsNullOrEmpty(cfg.IconCls) ? "" : cfg.IconCls;
                TreeNode node = new TreeNode();
                XmlElement siteNode = null;
                if (level < maxLevel)
                {
                    node.Text = MarkNew(folder.FullName, folder.Name.Replace("_", " "));
                    node.IconCls = iconCls;
                    node.SingleClickExpand = true;

                    if(siteMap != null)
                    {
                        siteNode = siteMap.OwnerDocument.CreateElement("siteMapNode");
                        siteNode.SetAttribute("title", node.Text);
                        siteMap.AppendChild(siteNode);
                    }

                    node.Nodes.AddRange(BuildTreeLevel(folder, level + 1, maxLevel, siteNode));
                }
                else
                {
                    string img = UIHelpers.ApplicationRoot + "/resources/images/noimage.gif";
                    string title = folder.Name.Replace("_", " ");
                    string desc = string.IsNullOrEmpty(cfg.Description) ? "No description" : cfg.Description;

                    if (File.Exists(folder.FullName + "\\thumbnail.png"))
                    {
                        img = PhysicalToVirtual(folder.FullName + "\\thumbnail.png");
                    }
                    else if (File.Exists(folder.FullName + "\\thumbnail.gif"))
                    {
                        img = PhysicalToVirtual(folder.FullName + "\\thumbnail.gif");
                    }

                    node.Text = MarkNew(folder.FullName, folder.Name.Replace("_", " "));
                    node.IconCls = iconCls;
                    node.SingleClickExpand = true;
                    string qtip = string.Format("<div class='thumb-wrap' style='margin:0px;float:none;'><img src='{0}' title='{1}'/><div><h4>{1}</h4><p>{2}</p></div></div>",
                                                img, title, desc);
                    node.Qtip = qtip;
                    string url = PhysicalToVirtual(folder.FullName + "/");
                    node.NodeID = "e" + url.ToLower().GetHashCode();
                    node.Href = url;
                    node.Leaf = true;

                    if (siteMap != null)
                    {
                        siteNode = siteMap.OwnerDocument.CreateElement("siteMapNode");
                        siteNode.SetAttribute("title", node.Text);
                        siteNode.SetAttribute("description", desc);
                        siteNode.SetAttribute("url", "~" + PhysicalToVirtual(folder.FullName + "/"));
                        siteMap.AppendChild(siteNode);
                    }
                }

                nodes.Add(node);
            }

            return nodes;
        }

        private const string NEW_MARKER = "<span>&nbsp;</span>";
        private static ExampleConfig rootCfg;
        private static string MarkNew(string folder, string name)
        {
            if(rootCfg == null)
            {
                rootCfg = new ExampleConfig(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Examples/")) + "\\config.xml", false);
            }

            foreach (string newFolder in rootCfg.NewFolders)
            {
                if(string.Concat(HttpContext.Current.Server.MapPath("~/Examples/"),newFolder).StartsWith(folder, StringComparison.CurrentCultureIgnoreCase))
                {
                    return name + NEW_MARKER;
                }
            }

            return name;
        }
        
        private static DirectoryInfo[] SortFolders(DirectoryInfo root, DirectoryInfo[] folders)
        {
            string cfgPath = root.FullName + "\\config.xml";
            if(File.Exists(root.FullName + "\\config.xml"))
            {
                ExampleConfig rootCfg = new ExampleConfig(cfgPath, false);
                if(rootCfg.OrderFolders.Count > 0)
                {
                    List<DirectoryInfo> list = new List<DirectoryInfo>(folders);
                    int pasteIndex = 0;
                    foreach (string orderFolder in rootCfg.OrderFolders)
                    {
                        int dIndex = 0;
                        for (int ind = 0; ind < list.Count; ind++)
                        {
                            if (list[ind].Name.ToLower() == orderFolder)
                            {
                                dIndex = ind;
                            }
                        }

                        if (dIndex > 0)
                        {
                            DirectoryInfo di = list[dIndex];
                            list.RemoveAt(dIndex);
                            list.Insert(pasteIndex++, di);
                        }
                    }

                    folders = list.ToArray();
                }
            }
            return folders;
        }

        public static void FindExamples(DirectoryInfo root, int level, int maxLevel, List<ExampleGroup> examples)
        {
            DirectoryInfo[] folders = root.GetDirectories();
            folders = SortFolders(root, folders);

            foreach (DirectoryInfo folder in folders)
            {
                if ((folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ||
                    excludeList.Contains(folder.Name) || folder.Name.StartsWith("_"))
                {
                    continue;
                }

                if (level < maxLevel)
                {
                    if(level == 1)
                    {
                        examples.Add(new ExampleGroup { id = folder.Name, title = folder.Name });
                    }
                    FindExamples(folder, level + 1, maxLevel, examples);
                }
                else
                {
                    string imgUrl = UIHelpers.ApplicationRoot+"/resources/images/noimage.gif";
                    string descr = "No description";
                    string name = MarkNew(folder.FullName, folder.Name.Replace("_", " "));

                    if (File.Exists(folder.FullName + "\\config.xml"))
                    {
                        ExampleConfig cfg = new ExampleConfig(folder.FullName + "\\config.xml", false);
                        descr = cfg.Description;
                    }

                    if (File.Exists(folder.FullName + "\\thumbnail.png"))
                    {
                        imgUrl = PhysicalToVirtual(folder.FullName + "\\thumbnail.png");
                    }
                    else if (File.Exists(folder.FullName + "\\thumbnail.gif"))
                    {
                        imgUrl = PhysicalToVirtual(folder.FullName + "\\thumbnail.gif");
                    }

                    string url = PhysicalToVirtual(folder.FullName + "/");

                    ExampleGroup group = examples[examples.Count - 1];
                    group.samples.Add(new { id = "e" + url.ToLower().GetHashCode(), name, url, imgUrl, descr, sub = folder.Parent.Name.Replace("_", " ") });
                }
            }
        }

        public static string PhysicalToVirtual(string physicalPath)
        {
            string pathOfWebRoot = HttpContext.Current.Server.MapPath("~/").ToLower();

            int index = physicalPath.IndexOf(pathOfWebRoot,StringComparison.InvariantCultureIgnoreCase);
            if (index == -1)
                throw new Exception("Physical path can't be mapped to the current application.");

            string relUrl = Path.DirectorySeparatorChar.ToString();

            index += pathOfWebRoot.Length;
            relUrl += physicalPath.Substring(index);

            relUrl = relUrl.Replace("\\", "/");

            return UIHelpers.ApplicationRoot + relUrl;
        }

        public static string PhysicalToAbsolute(string physicalPath)
        {
            HttpRequest r = HttpContext.Current.Request;
            return r.Url.Scheme + "://" + r.Url.Authority + PhysicalToVirtual(physicalPath);
        }
    }

    public class ExampleConfig
    {
        public ExampleConfig(string path, bool includeDescriptors)
        {
            this.Description = "No description";
            XmlDocument xml = new XmlDocument();

            if (File.Exists(path))
            {
                try
                {
                    xml.Load(path);
                }
                catch (FileNotFoundException)
                {
                    return;
                } 
            }

            XmlNode root = xml.SelectSingleNode("example");

            if(root == null)
            {
                return;
            }

            XmlAttribute iconCls = root.Attributes["iconCls"];

            if (iconCls != null)
            {
                this.IconCls = iconCls.Value;
            }

            XmlNode desc = root.SelectSingleNode("description");
            if(desc != null)
            {
                this.Description = desc.InnerText;
            }

            XmlNodeList files = root.SelectNodes("include/file");
            if(files != null)
            {
                string url = UIHelpers.PhysicalToAbsolute(path);
                foreach (XmlNode file in files)
                {
                    XmlAttribute urlAttr = file.Attributes["url"];
                    XmlAttribute zipAttr = file.Attributes["zip"];

                    if (includeDescriptors && zipAttr != null && zipAttr.InnerText.ToLower() == "false")
                    {
                        continue;
                    }

                    if(urlAttr != null && !string.IsNullOrEmpty(urlAttr.InnerText))
                    {
                        string fileUrl = urlAttr.InnerText;
                        Uri uri = new Uri(new Uri(url, UriKind.Absolute), fileUrl);
                        this.OuterFiles.Add(HttpContext.Current.Server.MapPath(uri.AbsolutePath));

                        if (includeDescriptors && (fileUrl.EndsWith("ashx.cs") || fileUrl.EndsWith("asmx.cs")))
                        {
                            uri = new Uri(new Uri(url, UriKind.Absolute), fileUrl.Remove(fileUrl.Length - 3, 3));
                            this.OuterFiles.Add(HttpContext.Current.Server.MapPath(uri.AbsolutePath));
                        }
                    }
                }
            }

            XmlNodeList folders = root.SelectNodes("zip-folders/folder");
            if(folders != null)
            {
                string url = UIHelpers.PhysicalToAbsolute(path);
                foreach (XmlNode folder in folders)
                {
                    XmlAttribute urlAttr = folder.Attributes["url"];

                    if (urlAttr != null && !string.IsNullOrEmpty(urlAttr.InnerText))
                    {
                        string folderUrl = urlAttr.InnerText;
                        Uri uri = new Uri(new Uri(url, UriKind.Absolute), folderUrl);
                        this.ZipFolders.Add(HttpContext.Current.Server.MapPath(uri.AbsolutePath));
                    }
                }
            }

            folders = root.SelectNodes("order/folder");
            if (folders != null)
            {
                foreach (XmlNode folder in folders)
                {
                    XmlAttribute urlAttr = folder.Attributes["name"];

                    if (urlAttr != null && !string.IsNullOrEmpty(urlAttr.InnerText))
                    {
                        string folderName = urlAttr.InnerText;
                        this.OrderFolders.Add(folderName.ToLower());
                    }
                }
            }

            folders = root.SelectNodes("new/folder");
            if (folders != null)
            {
                foreach (XmlNode folder in folders)
                {
                    XmlAttribute urlAttr = folder.Attributes["name"];

                    if (urlAttr != null && !string.IsNullOrEmpty(urlAttr.InnerText))
                    {
                        string folderName = urlAttr.InnerText;
                        this.NewFolders.Add(folderName.ToLower());
                    }
                }
            }
        }

        public string IconCls { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        private List<string> outerFiles;

        public List<string> OuterFiles
        {
            get
            {
                if(this.outerFiles == null)
                {
                    this.outerFiles = new List<string>();
                }
                return outerFiles;
            }
        }

        private List<string> zipFolders;

        public List<string> ZipFolders
        {
            get
            {
                if (this.zipFolders == null)
                {
                    this.zipFolders = new List<string>();
                }
                return zipFolders;
            }
        }

        private List<string> orderFolders;
        public List<string> OrderFolders
        {
            get
            {
                if (this.orderFolders == null)
                {
                    this.orderFolders = new List<string>();
                }
                return this.orderFolders;
            }
        }

        private List<string> newFolders;
        public List<string> NewFolders
        {
            get
            {
                if (this.newFolders == null)
                {
                    this.newFolders = new List<string>();
                }
                return this.newFolders;
            }
        }
    }

    public class ExampleGroup
    {
        private List<object> examples;

        public string id { get; set; }

        public string title { get; set; }

        public List<object> samples
        {
            get
            {
                if(this.examples == null)
                {
                    this.examples = new List<object>();
                }
                return examples;
            }
        }
    }

}
