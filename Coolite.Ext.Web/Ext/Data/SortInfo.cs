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
    /// <summary>
    /// A config object in the format: {field: 'fieldName', direction: 'ASC|DESC'}. The direction property is case-sensitive.
    /// </summary>
    [ToolboxItem(false)]
    [DefaultProperty("Field")]
    [Description("A config object in the format: {field: 'fieldName', direction: 'ASC|DESC'}. The direction property is case-sensitive.")]
    public class SortInfo : StateManagedItem
    {
        public SortInfo() { }

        public SortInfo(string field, SortDirection direction)
        {
            this.Field = field;
            this.Direction = direction;
        }

        public override bool IsDefault
        {
            get
            {
                return string.IsNullOrEmpty(this.Field);
            }
        }

        private string field = "";

        /// <summary>
        /// Internal UI Event. Fired before the view is refreshed.
        /// </summary>
        [ClientConfig]
        [DefaultValue("")]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before the view is refreshed.")]
        public string Field
        {
            get
            {
                return this.field;
            }
            set
            {
                this.field = value;
            }
        }

        private SortDirection direction = SortDirection.ASC;

        /// <summary>
        /// Internal UI Event. Fired before the view is refreshed.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [DefaultValue(SortDirection.Default)]
        [NotifyParentProperty(true)]
        [Description("Internal UI Event. Fired before the view is refreshed.")]
        public SortDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public virtual string ToExtConfigString()
        {
            return string.Empty;
        }
    }
}
