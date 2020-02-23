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
    [Description("A split button that renders into a toolbar.")]
    [ToolboxItem(false)]
    [Xtype("tbsplit")]
    [InstanceOf(ClassName = "Ext.Toolbar.SplitButton")]
    public class ToolbarSplitButton : SplitButton, IToolbarItem
    {
        protected override void OnBeforeClientInit(Observable sender)
        {
            base.OnBeforeClientInit(sender);

            if (this.StandOut)
            {
                if (this.Icon != Icon.None || !string.IsNullOrEmpty(this.IconCls))
                {
                    this.Cls = (string.IsNullOrEmpty(this.Text) ? "x-btn-icon" : "x-btn-text-icon") + " x-btn-over";
                }
                else
                {
                    this.Cls = "x-btn-over";
                }

                this.On("mouseout", "function(){this.getEl().addClass('x-btn-over');}");
            }
        }

        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to enable stand out by default (defaults to false).")]
        public virtual bool StandOut
        {
            get
            {
                object obj = this.ViewState["StandOut"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["StandOut"] = value;
            }
        }
    }
}