/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Abstract base class for grid SelectionModels. It provides the interface that should
    /// be implemented by descendant classes. This class should not be directly instantiated.
    /// </summary>
    [Description("Abstract base class for grid SelectionModels. It provides the interface that should be implemented by descendant classes. This class should not be directly instantiated.")]
    public abstract class AbstractSelectionModel : InnerObservable
    {
        public abstract void UpdateSelection();


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Locks the selections.
        /// </summary>
        [Description("Locks the selections.")]
        public virtual void Lock()
        {
            this.AddScript("{0}.lock();", this.ClientID);
        }

        /// <summary>
        /// Unlocks the selections.
        /// </summary>
        [Description("Unlocks the selections.")]
        public virtual void Unlock()
        {
            this.AddScript("{0}.unlock();", this.ClientID);
        }
    }
}