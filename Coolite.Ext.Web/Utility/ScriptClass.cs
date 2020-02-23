/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System.Web;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public abstract class ScriptClass
    {
        public abstract string Serialize();

        public virtual void Render()
        {
            this.AddScript(this.Serialize());
        }

        private Page page;
        private ScriptManager sm;

        internal virtual void AddScript(string script)
        {
            if (this.page != null && this.sm != null)
            {
                this.sm.AddScript(script);
                return;
            }

            ScriptManager.AddInstanceScript(script);
        }

        internal virtual string Build(string script)
        {
            return this.Build("{0}", string.Concat("={", script, "}"));
        }

        internal virtual string Build(string template, params object[] args)
        {
            if (this.sm == null)
            {
                this.sm = ScriptManager.GetInstance(HttpContext.Current);
            }

            if (this.page == null && this.sm != null)
            {
                this.page = this.sm.Page;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is string)
                {
                    args[i] = TokenUtils.ParseAndNormalize(args[i].ToString(), this.sm);
                }
            }

            return string.Format(template, args);
        }
    }
}