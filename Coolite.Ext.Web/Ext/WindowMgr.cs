/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Text;
using System.Web;

namespace Coolite.Ext.Web
{
    public class WindowMgr : ScriptClass
    {
        private const string INSTANCE = "Ext.WindowMgr";

        private WindowMgr() { }

        public static WindowMgr Instance
        {
            get
            {
                return (HttpContext.Current.Items[WindowMgr.INSTANCE] ?? (HttpContext.Current.Items[WindowMgr.INSTANCE] = new WindowMgr())) as WindowMgr;
            }
        }

        public override string Serialize()
        {
            return "";
        }

        //private readonly StringBuilder scriptBuilder = new StringBuilder(64);

        //public override string Serialize()
        //{
        //    string scriptText = this.scriptBuilder.ToString();
        //    if (!string.IsNullOrEmpty(scriptText))
        //    {
        //        this.scriptBuilder.Length = 0;
        //        return scriptText;
        //    }
        //    return "";
        //}

        //public virtual void Perform()
        //{
        //    this.Render();
        //}

        /// <summary>
        /// Brings the specified window to the front of any other active windows.
        /// </summary>
        /// <param name="windowID">The id of the window</param>
        /// <returns>WindowMgr</returns>
        public virtual WindowMgr BringToFront(string windowID)
        {
            this.AddScript(this.Build(string.Concat(WindowMgr.INSTANCE, ".bringToFront(", JSON.Serialize(windowID), ");")));

            //scriptBuilder.AppendFormat("{0}.bringToFront({1});", WindowMgr.INSTANCE, JSON.Serialize(windowID));
            return this;
        }

        /// <summary>
        /// Brings the specified window to the front of any other active windows.
        /// </summary>
        /// <param name="window">Window</param>
        /// <returns>WindowMgr</returns>
        public virtual WindowMgr BringToFront(Window window)
        {
            return this.BringToFront(window.ClientID);
        }

        /// <summary>
        /// Hides all windows in the group.
        /// </summary>
        /// <returns>WindowMgr</returns>
        public virtual WindowMgr HideAll()
        {
            this.AddScript(this.Build(string.Concat(WindowMgr.INSTANCE, ".hideAll();")));

            //scriptBuilder.AppendFormat("{0}.hideAll();", WindowMgr.INSTANCE);
            return this;
        }

        /// <summary>
        /// Sends the specified window to the back of other active windows.
        /// </summary>
        /// <param name="windowID">The id of the window</param>
        /// <returns>WindowMgr</returns>
        public virtual WindowMgr SendToBack(string windowID)
        {
            this.AddScript(this.Build(string.Concat(WindowMgr.INSTANCE, ".sendToBack(", JSON.Serialize(windowID), ");")));

            //scriptBuilder.AppendFormat("{0}.sendToBack({1});", WindowMgr.INSTANCE, JSON.Serialize(windowID));
            return this;
        }

        /// <summary>
        /// Sends the specified window to the back of other active windows.
        /// </summary>
        /// <param name="window">Window</param>
        /// <returns>WindowMgr</returns>
        public virtual WindowMgr SendToBack(Window window)
        {
            return this.SendToBack(window.ClientID);
        }
    }
}
