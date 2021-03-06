/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
	public enum IDMode
	{
        /// <summary>
        /// [Default] Inherits the IDMode for the Parent. This is the default functionality of all Toolkit Controls.
        /// </summary>
        Inherit,
        /// <summary>
        /// Legacy functionality for generating CliendID's. No change from default ASP.NET functionality.
        /// </summary>
        Legacy,
        /// <summary>
        /// Render the "id" property in the client as exactly the value set. Developer must manually ensure client-side ID uniqueness.
        /// </summary>
        Static,
        /// <summary>
        /// Do not render the "id" property in the client. 
        /// </summary>
        Ignore,
        /// <summary>
        /// Only render the "id" property if the .ID is explicitly set, otherwise renders as the ClientID if autogenerated by the ASP.NET runtime.
        /// </summary>
        Explicit
	}
}