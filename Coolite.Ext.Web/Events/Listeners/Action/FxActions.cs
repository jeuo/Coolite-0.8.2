/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
    public abstract class FxAction : ListenerMethod
    {
        protected override bool ControlIsRequired
        {
            get
            {
                return false;
            }
        }
    }

    public class _Highlight : FxAction
    {
        protected override string Name
        {
            get
            {
                return "highlight";
            }
        }

        //TODO: need to add Args
    }
}
