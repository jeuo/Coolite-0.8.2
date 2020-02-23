using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Coolite.Utilities;
using System.Collections.Generic;

namespace Coolite.EmbeddedResourceBuilder
{
    class EmbeddedResourceBuilder
    {
        const string assemblyJSTemplate = "[assembly: WebResource(\"{1}.Coolite.{0}\", \"text/javascript\")]";
        const string assemblyCSSTemplate = "[assembly: WebResource(\"{1}.Coolite.{0}\", \"text/css\", PerformSubstitution = true)]";
        const string assemblyGIFTemplate = "[assembly: WebResource(\"{1}.Coolite.{0}\", \"image/gif\")]";
        const string assemblyPNGTemplate = "[assembly: WebResource(\"{1}.Coolite.{0}\", \"image/png\")]";
        const string webresourceTemplate = "url(\"<%=WebResource(\"{1}.Coolite.{0}\")%>\")";

        ArrayList list = new ArrayList();

        static void Main(string[] args)
        {
            EmbeddedResourceBuilder erb = new EmbeddedResourceBuilder();

            erb.root = args[0];
            erb.assemblyRoot = args[1];
            erb.copyrightRoot = args[2].Trim();
            erb.copyrightMode = args[3];

            erb.InitializeSysApplication();
            erb.Start();
            erb.UpdateAssemblyInfo();
            erb.UpdateIconEnum();

            if (erb.HasCopyrightMode)
            {
                erb.InitializeCopyrightStatement();
                erb.AddCopyright(erb.CopyrightRoot);
            }
        }

        string root;
        string Root
        {
            get
            {
                return this.root;
            }
        }

        string assemblyRoot;
        string AssemblyRoot
        {
            get
            {
                return this.assemblyRoot;
            }
        }

        string[] goodFile;
        string[] GoodFile
        {
            get
            {
                return this.goodFile;
            }
        }

        string[] badFolder;
        string[] BadFolder
        {
            get
            {
                return this.badFolder;
            }
        }

        private string sysapp = string.Empty;
        private void InitializeSysApplication()
        {
            this.sysapp = FileUtils.ReadFile(this.Root + @"\Templates\SysApplication_Template.txt");
        }

        private void Start()
        {
            this.goodFile = new string[]{ "js", "css", "gif", "png" };
            this.badFolder = new string[]{ ".svn", "source", "raw-images", "test", "src" };

            this.FindFiles(this.Root + @"\Coolite\");
        }

        public void FindFiles(string src)
        {
            string[] files = Directory.GetFileSystemEntries(src);

            foreach (string el in files)
            {
                if (Directory.Exists(el))
                {
                    DirectoryInfo di = new DirectoryInfo(el);
                    if (!this.IsMatch(di.Name, this.BadFolder))
                    {
                        this.FindFiles(el);
                    }
                }
                else
                {
                    FileInfo fi = new FileInfo(el);
                    if (this.IsMatch(StringUtils.RightOfRightmostOf(fi.Name.ToLower(), '.'), this.GoodFile))
                    {
                        this.Embeddify(fi);
                    }
                }
            }
        }

        private void AddSysApplication(string path)
        {
            string temp = FileUtils.ReadFile(path);
            if (!temp.EndsWith(this.sysapp))
            {
                FileUtils.WriteToEnd(path, this.sysapp);
            }
        }

        private void Embeddify(FileInfo file)
        {
            string value = StringUtils.RightOfRightmostOf(file.FullName, @"\Coolite\").Replace(@"\", ".");

            switch (file.Extension)
            {
                case ".js":
                    this.AddSysApplication(file.FullName);
                    value = string.Format(assemblyJSTemplate, value, this.AssemblyRoot);
                    break;
                case ".css":
                    this.StripImagesFromCSS(file);
                    value = string.Format(assemblyCSSTemplate, value, this.AssemblyRoot);
                    break;
                case ".gif":
                    value = string.Format(assemblyGIFTemplate, value, this.AssemblyRoot);
                    break;
                case ".png":
                    value = string.Format(assemblyPNGTemplate, value, this.AssemblyRoot);
                    break;
            }

            this.AddToList(value);
        }

        private void StripImagesFromCSS(FileInfo file)
        {
            if (!file.Name.Contains("-embedded"))
            {
                Dictionary<string, string> list = new Dictionary<string, string>();

                string css = FileUtils.ReadFile(file.FullName);

                //Regex regex = new Regex(@"url\([^<]\s*'*""*.*\.(gif|png)""*'*\s*\)");
                Regex regex = new Regex(@"url\(\s*'*""*.*\.(gif|png)""*'*\s*\)");
                MatchCollection matches = regex.Matches(css);

                foreach (Match match in matches)
                {
                    if (!match.Value.Contains("foo.gif"))
                    {
                        string value = match.Value.ToLower().Replace(" ", "").Replace("url(", "").Replace("..", "").Replace(")", "").Replace("/", ".").Replace("'", "").Replace("\"", "");

                        if (value.StartsWith("."))
                        {
                            value = value.TrimStart('.');
                        }

                        string path = "";

                        if (value.StartsWith("images."))
                        {
                            string dir = file.Directory.ToString();

                            if (dir.EndsWith(@"\css"))
                            {
                                path = StringUtils.RightOf(dir, @"\Build\Resources\Coolite\").Replace(@"\css","").Replace(@"\", ".");
                            }
                        }

                        value = string.Concat(path, ".", value);

                        if (!list.ContainsKey(match.Value))
                        {
                            list.Add(match.Value, value);
                        }

                        if (StringUtils.RightOfRightmostOf(value, '.').Equals("gif"))
                        {
                            value = string.Format(assemblyGIFTemplate, value, this.AssemblyRoot);
                        }
                        else
                        {
                            value = string.Format(assemblyPNGTemplate, value, this.AssemblyRoot);
                        }


                        this.AddToList(value);
                    }
                }
                this.MakeEmbeddedCSS(file.FullName, css, list);
            }
        }

        private void MakeEmbeddedCSS(string path, string css, Dictionary<string, string> toMatch)
        {
            foreach (KeyValuePair<string, string> item in toMatch)
            {
                css = css.Replace(item.Key, string.Format(webresourceTemplate, item.Value, this.AssemblyRoot));
                //css = css.Replace(item.Value, string.Format(webresourceTemplate, item.Key, this.AssemblyRoot));
            }

            path = StringUtils.LeftOfRightmostOf(path, '.') + "-embedded." + StringUtils.RightOfRightmostOf(path, '.');
            FileUtils.WriteFile(path, css);
        }

        public void UpdateIconEnum()
        {
            string marker = "/****/";

            string path = @"..\..\Enums\Icon.cs";

            string source = FileUtils.ReadFile(path);

            string start = StringUtils.LeftOf(source, marker);
            string end = StringUtils.RightOfRightmostOf(source, marker);

            string[] files = Directory.GetFileSystemEntries(this.Root + @"\Coolite\icons\");

            StringBuilder sb = new StringBuilder();

            sb.Append(start);
            sb.Append(marker);
            sb.Append(Environment.NewLine);

            foreach (string el in files)
            {
                FileInfo fi = new FileInfo(el);
                if (this.IsMatch(StringUtils.RightOfRightmostOf(fi.Name.ToLower(), '.'), this.GoodFile))
                {
                    sb.Append("\t\t" + StringUtils.ToCamelCase(StringUtils.LeftOfRightmostOf(fi.Name, '.').Split('_')) + "," + Environment.NewLine);
                }
            }
            // Chop the last ','
            sb.Replace(",", "", sb.Length - 4, 4);
            sb.Append("\t\t" + marker);
            sb.Append(end);

            FileUtils.WriteFile(path, sb.ToString().Trim());
        }

        private void UpdateAssemblyInfo()
        {
            string marker = "/*COOLITE TOOLKIT EMBEDDED RESOURCES*/";
            string path = @"..\..\Properties\AssemblyInfo.cs";
            string info = FileUtils.ReadFile(path);

            info = info.Substring(0, info.IndexOf(marker)).Trim();
            info += Environment.NewLine + Environment.NewLine + marker + Environment.NewLine + Environment.NewLine;

            this.list.Sort();

            StringBuilder results = new StringBuilder(1024);

            foreach (string s in this.list)
            {
                results.Append(s + Environment.NewLine);
            }

            info += results.ToString().Trim();

            FileUtils.WriteFile(path, info);
        }

        private void AddToList(string value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }

        private bool IsMatch(string value, string[] toMatch)
        {
            foreach (string s in toMatch)
            {
                if (value.Equals(s))
                    return true;
            }
            return false;
        }

        string copyrightStatement;
        string CopyrightStatement
        {
            get
            {
                return this.copyrightStatement;
            }
        }

        string copyrightRoot;
        string CopyrightRoot
        {
            get
            {
                return this.copyrightRoot;
            }
        }

        string copyrightMode = "";
        string CopyrightMode
        {
            get
            {
                return this.copyrightMode.Trim();
            }
        }

        bool HasCopyrightMode
        {
            get
            {
                return (!string.IsNullOrEmpty(this.CopyrightMode) && this.CopyrightMode.Length > 0);
            }
        }

        private void InitializeCopyrightStatement()
        {
            //object[] args = new object[3];
            //Version v = Assembly.GetExecutingAssembly().GetName().Version;
            //args[0] = string.Format("{0}.{1}.{2}", v.Major, v.MajorRevision, v.Minor);
            //args[1] = DateTime.Today.ToString("yyyy-MM-dd");
            //args[2] = DateTime.Today.ToString("yyyy");

            this.copyrightStatement = FileUtils.ReadFile(this.Root + string.Format(@"\Templates\Copyright-{0}.txt", this.CopyrightMode));
        }

        public void AddCopyright(string src)
        {
            string[] files = Directory.GetFileSystemEntries(src);

            foreach (string el in files)
            {
                if (Directory.Exists(el))
                {
                    this.AddCopyright(el);
                }
                else
                {
                    FileInfo fi = new FileInfo(el);
                    if (StringUtils.RightOfRightmostOf(fi.Name.ToLower(), '.').Equals("cs"))
                    {
                        this.StampCopyright(fi);
                    }
                }
            }
        }

        public void StampCopyright(FileInfo file)
        {
            string content = FileUtils.ReadFile(file.FullName);

            if (content.StartsWith("/********"))
            {
                MatchCollection matches = new Regex(@"\/\*{8}.*\*{8}\/", RegexOptions.Singleline).Matches(content);

                if (matches.Count == 1)
                {

                    FileUtils.WriteFile(file.FullName, content.Replace(matches[0].Value, this.CopyrightStatement));
                    return;
                }
            }
            else
            {
                FileUtils.WriteToStart(file.FullName, this.CopyrightStatement);
            }
        }
    }
}