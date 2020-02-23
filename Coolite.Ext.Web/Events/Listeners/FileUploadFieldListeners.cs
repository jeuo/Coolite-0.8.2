/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System.ComponentModel;
using System.Web.UI;

namespace Coolite.Ext.Web
{
    public class FileUploadFieldListeners : TextFieldListeners
    {
        private ComponentListener fileSelected;

        /// <summary>
        /// Fires when the underlying file input field's value has changed from the user selecting a new file from the system file selection dialog.
        /// </summary>
        [ListenerArgument(0, "el", typeof(Field), "This file upload field")]
        [ListenerArgument(1, "value", typeof(int), "The file value returned by the underlying file input field")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ClientConfig("fileselected", typeof(ListenerJsonConverter))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        [Description("Fires when the underlying file input field's value has changed from the user selecting a new file from the system file selection dialog.")]
        public virtual ComponentListener FileSelected
        {
            get
            {
                if (this.fileSelected == null)
                {
                    this.fileSelected = new ComponentListener();
                }
                return this.fileSelected;
            }
        }
    }
}