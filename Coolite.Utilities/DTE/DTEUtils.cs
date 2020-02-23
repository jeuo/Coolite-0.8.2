/*
 * http://www.codeproject.com/KB/cs/Help_to_use_EnvDTE.aspx
 * At the moment (9-May-2008), code published under no explicit license. Assuming Public Domain.
 * Original publisher has been contacted.
 */

// Less clean, but the following will work to get the current DTE instance. 

//object _dte2 = null;
//object _dte3 = null;

//_dte3 = this.GetService(typeof(EnvDTE80.DTE2));

//if(_dte3 != null)
//{
//    this.height = ((EnvDTE80.DTE2)_dte3).ActiveWindow.Height - offset;
//}
//else
//{
//    _dte2 = this.GetService(typeof(EnvDTE._DTE));
//    if (_dte2 != null)
//    {
//        this.height = ((EnvDTE._DTE)_dte2).ActiveWindow.Height - offset;
//    }
//}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace Coolite.Utilities
{
    public static class DTEUtilities
    {
        public static _DTE _application = null;

        /// <summary>
        /// IDE Connection to VisualStudio
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            try
            {
                for (int i = 0; i < 5 && _application == null; ++i)
                    _application = GetCurrentDTE();
            }
            catch (Exception) { /* insert your message here */ }
            return (_application != null);
        }

        /// <summary>
        /// Important disconnect
        /// </summary>
        /// <returns></returns>
        public static bool Disconnect()
        {
            try
            {
                for (int i = 0; i < 5 && _application != null; ++i)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_application);
            }
            catch (Exception) { /* insert your message here */ }
            return (_application == null);
        }

        /// <summary>
        /// Return the current project
        /// </summary>
        /// <returns></returns>
        public static Project GetCurrentProject()
        {
            Project p = null;
            try
            {
                p = GetCurrentProject(_application);
            }
            catch (Exception)
            {
                p = null;
                /* insert your message here */
            }
            return p;
        }

        /// <summary>
        /// Get all Projects from Solution
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetAllProjectOfSolution()
        {
            List<Project> prj = new List<Project>();
            try
            {
                if (_application == null || _application.Solution == null ||
                _application.Solution.Projects == null)
                    return prj;
                foreach (Project p in _application.Solution.Projects)
                    prj.Add(p);
            }
            catch (Exception) { /* insert your message here */ }
            return prj;
        }

        /// <summary>
        /// Return all class from this Project
        /// </summary>
        /// <returns></returns>
        public static List<CodeClass> GetAllClassOfCurrentProject()
        {
            List<CodeClass> cc = null;
            try
            {
                cc = GetAllClassFromProject(GetCurrentProject());
            }
            catch (Exception)
            {
                cc = new List<CodeClass>();
                /* insert your message here */
            }
            return cc;
        }

        /// <summary>
        /// Return all class matching the interface list
        /// </summary>
        /// <param name="interfaceName">l'interface</param>
        /// <returns></returns>
        public static List<CodeClass> GetAllClassOfCurrentProject(List<string>
        interfaceNames, List<string> attributeNames)
        {
            List<CodeClass> cc = null;
            try
            {
                cc = GetAllClassFromProject(GetCurrentProject(), interfaceNames,
                attributeNames);
            }
            catch (Exception)
            {
                cc = new List<CodeClass>();
                /* insert your message here */
            }
            return cc;
        }

        /// <summary>
        /// Return all class of this solution
        /// </summary>
        /// <returns></returns>
        public static List<CodeClass> GetAllClassOfCurrentSolution()
        {
            List<CodeClass> someclasses = new List<CodeClass>();
            List<string> interfaceNames = new List<string>();
            List<string> attributeNames = new List<string>();
            try
            {
                foreach (Project p in GetAllProjectOfSolution())
                    GetAllClassFromProject(p, someclasses, interfaceNames, attributeNames);
            }
            catch (Exception) { /* insert your message here */ }
            return someclasses;
        }

        /// <summary>
        /// Return all class matching interface list
        /// </summary>
        /// <param name="interfaceName">l'interface</param>
        /// <returns></returns>
        public static List<CodeClass> GetAllClassOfCurrentSolution(List<string>
        interfaceNames, List<string> attributeNames)
        {
            List<CodeClass> someclasses = new List<CodeClass>();
            try
            {
                foreach (Project p in GetAllProjectOfSolution())
                    GetAllClassFromProject(p, someclasses, interfaceNames, attributeNames);
            }
            catch (Exception) { /* insert your message here */ }
            return someclasses;
        }

        static _DTE GetCurrentDTE()
        {
            EnvDTE.DTE dte = null;
            // i take ROT
            UCOMIRunningObjectTable rot;
            uint uret = GetRunningObjectTable(0, out rot);
            if (uret == S_OK)
            {
                // I Take emunerator
                UCOMIEnumMoniker EnumMon;
                rot.EnumRunning(out EnumMon);
                if (EnumMon != null)
                {
                    // object limit
                    const int NUMMONS = 5000;
                    UCOMIMoniker[] aMons = new UCOMIMoniker[NUMMONS];
                    int Fetched = 0;
                    EnumMon.Next(NUMMONS, aMons, out Fetched);
                    // i'm creating binding to monikers.
                    string Name;
                    UCOMIBindCtx ctx;
                    uret = CreateBindCtx(0, out ctx);
                    // ROT name of _DTE using id process
                    System.Diagnostics.Process currentProcess =
                    System.Diagnostics.Process.GetCurrentProcess();
                    string dteName = "VisualStudio.DTE";
                    for (int i = 0; i < Fetched; ++i)
                    {
                        // i take the name
                        aMons[i].GetDisplayName(ctx, null, out Name);
                        // found?
                        if (Name.IndexOf(dteName) != -1)
                        {
                            object temp;
                            rot.GetObject(aMons[i], out temp);
                            dte = (EnvDTE.DTE)temp;
                            break;
                        }
                    }
                }
            }
            return dte;
        }

        static Project GetCurrentProject(_DTE application)
        {
            Project prj = null;
            // active solution?
            if ((application != null) && (application.Solution.Count > 0))
            {
                // can i find project in solutions?
                int progetti = application.Solution.Projects.Count;
                if (progetti > 0)
                {
                    Document activeDoc = application.ActiveDocument;
                    Project tmpPrj = null;
                    ProjectItem prjItm;
                    for (int i = 1; i <= progetti; ++i)
                    {
                        tmpPrj = application.Solution.Projects.Item(i);
                        int elementiprogetti = tmpPrj.ProjectItems.Count;
                        for (int j = 1; j <= elementiprogetti; ++j)
                        {
                            prjItm = tmpPrj.ProjectItems.Item(j);
                            string itemFullName = prjItm.get_FileNames(1);
                            if (itemFullName.Equals(activeDoc.FullName))
                            {
                                prj = tmpPrj;
                                break;
                            }
                        }
                    }
                }
            }
            return prj;
        }

        /// <summary>
        /// return CodeClass of classname indicated
        /// </summary>
        /// <param name="prj"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static CodeClass GetClassFromProject(Project prj, string className)
        {
            CodeClass someclass = null;
            try
            {
                if (prj != null)
                {
                    ProjectItem prjItem;
                    for (int i = 1; i <= prj.ProjectItems.Count; ++i)
                    {
                        prjItem = prj.ProjectItems.Item(i);
                        FileCodeModel codeModel = prjItem.FileCodeModel;
                        if (codeModel == null)
                            continue;
                        CodeElements elements = codeModel.CodeElements;
                        for (int j = 1; j <= elements.Count; ++j)
                        {
                            CodeElement element = elements.Item(j);
                            if (element.Kind == vsCMElement.vsCMElementNamespace)
                            {
                                CodeNamespace cns = (CodeNamespace)element;
                                CodeElements melements = cns.Members;
                                for (int k = 1; k <= melements.Count; ++k)
                                {
                                    CodeElement melemt = melements.Item(k);
                                    if ((melemt.Kind == vsCMElement.vsCMElementClass) &&
                                    (melemt.Name.Equals(className)))
                                    {
                                        someclass = (CodeClass)melemt;
                                        break;
                                    }
                                }
                            }
                            if (someclass != null)
                                break;
                        }
                        if (someclass != null)
                            break;
                    }
                }
            }
            catch (Exception) { /* insert your message here */ }
            return someclass;
        }
        
        static void GetAllClassFromProject(Project prj, List<CodeClass> someclass,
        List<string> interfaceNames, List<string> attributeNames)
        {
            if (someclass == null || prj == null)
                return;
            GetAllClassFromProjectRaw(prj.ProjectItems, someclass, interfaceNames,
            attributeNames);
        }

        static void GetAllClassFromProjectRaw(ProjectItems prjItems, List<CodeClass>
        someclass, List<string> interfaceNames, List<string> attributeNames)
        {
            ProjectItem prjItem = null;
            for (int i = 1; i <= prjItems.Count; ++i)
            {
                prjItem = prjItems.Item(i);
                FileCodeModel codeModel = prjItem.FileCodeModel;
                if (codeModel == null)
                {
                    if (prjItem.ProjectItems == null || prjItem.ProjectItems.Count < 1)
                        continue;
                    GetAllClassFromProjectRaw(prjItem.ProjectItems, someclass, interfaceNames,
                    attributeNames);
                }
                else
                {
                    CodeElements elements = codeModel.CodeElements;
                    for (int j = 1; j <= elements.Count; ++j)
                    {
                        CodeElement element = elements.Item(j);
                        if (element.Kind == vsCMElement.vsCMElementNamespace)
                        {
                            CodeNamespace cns = (CodeNamespace)element;
                            CodeElements melements = cns.Members;
                            for (int k = 1; k <= melements.Count; ++k)
                            {
                                CodeElement melemt = melements.Item(k);
                                if (melemt.Kind == vsCMElement.vsCMElementClass)
                                {
                                    if (!melemt.Name.EndsWith("Resources") && !
                                    melemt.Name.EndsWith("Settings"))
                                    {
                                        if (interfaceNames.Count == 0 && attributeNames.Count == 0)
                                            someclass.Add((CodeClass)melemt);
                                        else
                                        {
                                            CodeClass c = (CodeClass)melemt;
                                            bool bToAdd = false;
                                            if (interfaceNames.Count > 0)
                                            {
                                                CodeElements ce = c.ImplementedInterfaces;
                                                foreach (CodeElement cel in ce)
                                                    if (interfaceNames.Contains(cel.Name))
                                                    {
                                                        if (!someclass.Contains(c))
                                                            bToAdd = true;
                                                        break;
                                                    }
                                            }
                                            // remove comment if you want i'll be able to controll
                                            // attribute presence
                                            //if (bToAdd && attributeNames.Count > 0)
                                            //{
                                            // CodeElements cea = c.Attributes;
                                            // foreach (CodeElement cel in cea)
                                            // if (attributeNames.Contains((cel.Name)))
                                            // {
                                            // if (!someclass.Contains(c))
                                            // bToAdd = true;
                                            // break;
                                            // }
                                            //}
                                            if (bToAdd)
                                                someclass.Add(c);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        static List<CodeClass> GetAllClassFromProject(Project prj)
        {
            List<CodeClass> someclass = new List<CodeClass>();
            if (prj == null)
                return someclass;
            List<string> interfaceNames = new List<string>();
            List<string> attributeNames = new List<string>();
            GetAllClassFromProject(prj, someclass, interfaceNames, attributeNames);
            return someclass;
        }
        static List<CodeClass> GetAllClassFromProject(Project prj, List<string>
        interfaceNames, List<string> attributeNames)
        {
            List<CodeClass> someclass = new List<CodeClass>();
            if (prj == null)
                return someclass;
            GetAllClassFromProject(prj, someclass, interfaceNames, attributeNames);
            return someclass;
        }
        [DllImport("ole32.dll", EntryPoint = "GetRunningObjectTable")]
        static extern uint GetRunningObjectTable(uint res, out UCOMIRunningObjectTable
        ROT);
        [DllImport("ole32.dll", EntryPoint = "CreateBindCtx")]
        static extern uint CreateBindCtx(uint res, out UCOMIBindCtx ctx);
        static readonly uint S_OK = 0;
    }
}