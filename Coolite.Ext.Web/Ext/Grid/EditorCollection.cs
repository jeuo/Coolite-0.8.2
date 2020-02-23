/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

namespace Coolite.Ext.Web
{
    public class EditorCollection : SingleItemCollection<Field>
    {
        [ClientConfig(JsonMode.Object)]
        public Field Editor
        {
            get
            {
                if (this.Count > 0)
                {
                    this[0].ApplyTo = "";
                    return this[0];
                }

                return null;
            }
        }
    }
}
