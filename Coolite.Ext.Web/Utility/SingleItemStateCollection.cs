/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class SingleItemStateCollection<T> : SingleItemCollection<T>, IStateManager where T : IStateManager
    {
        private bool isTrackingViewState;

        public void LoadViewState(object state)
        {
            if (state != null && this.Count > 0)
            {
                this[0].LoadViewState(state);
            }
        }

        public object SaveViewState()
        {
            return this.Count > 0 ? this[0].SaveViewState() : null;
        }

        public void TrackViewState()
        {
            this.isTrackingViewState = true;
            if (this.Count > 0)
            {
                this[0].TrackViewState();
            }
        }

        public bool IsTrackingViewState
        {
            get { return isTrackingViewState; }
        }
    }
}