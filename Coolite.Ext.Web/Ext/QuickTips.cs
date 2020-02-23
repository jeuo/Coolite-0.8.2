/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Text;
using System.Web;
using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public class QuickTips : ScriptClass
    {
        private const string INSTANCE = "Ext.QuickTips";

        private QuickTips() { }

        public static QuickTips Instance
        {
            get
            {
                return (HttpContext.Current.Items[QuickTips.INSTANCE] ?? (HttpContext.Current.Items[QuickTips.INSTANCE] = new QuickTips())) as QuickTips;
            }
        }

        public override string Serialize()
        {
            return "";
        }

        /// <summary>
        /// Disable quick tips globally.
        /// </summary>
        [Description("Disable quick tips globally.")]
        public virtual void Disable()
        {
            this.AddScript(this.Build(string.Concat(QuickTips.INSTANCE, ".disable();")));
        }

        /// <summary>
        /// Enable quick tips globally.
        /// </summary>
        [Description("Enable quick tips globally.")]
        public virtual void Enable()
        {
            this.AddScript(this.Build(string.Concat(QuickTips.INSTANCE, ".enable();")));
        }

        /// <summary>
        /// Initialize the global QuickTips instance and prepare any quick tips.
        /// </summary>
        /// <param name="autoRender">True to render the QuickTips container immediately to preload images. (Defaults to true)</param>
        /// <returns>QuickTips</returns>
        [Description("Initialize the global QuickTips instance and prepare any quick tips.")]
        public virtual void Init()
        {
            this.AddScript(this.Build(string.Concat(QuickTips.INSTANCE, ".init();")));
        }

        /// <summary>
        /// Initialize the global QuickTips instance and prepare any quick tips.
        /// </summary>
        /// <param name="autoRender">True to render the QuickTips container immediately to preload images. (Defaults to true)</param>
        /// <returns>QuickTips</returns>
        [Description("Initialize the global QuickTips instance and prepare any quick tips.")]
        public virtual void Init(bool autoRender)
        {
            this.AddScript(this.Build(string.Concat(QuickTips.INSTANCE, ".init(", autoRender.ToString().ToLower(), ");")));
        }
    }
}