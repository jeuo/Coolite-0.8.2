/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public class SaveMask : LoadMask
    {
        /// <summary>
        /// The text to display in a centered saving message box (defaults to 'Saving...').
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("Saving...")]
        [Description("The text to display in a centered saving message box (defaults to 'Saving...').")]
        public override string Msg
        {
            get
            {
                return (string)this.ViewState["Msg"] ?? "Saving...";
            }
            set
            {
                this.ViewState["Msg"] = value;
            }
        }
    }
}