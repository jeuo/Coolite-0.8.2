/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/using System.ComponentModel;

namespace Coolite.Ext.Web
{
    public class LayoutConfig
    {
        private readonly bool renderHidden;
        private readonly string extraCls;

        public LayoutConfig()
        {
        }

        public LayoutConfig(bool renderHidden, string extraCls)
        {
            this.renderHidden = renderHidden;
            this.extraCls = extraCls;
        }


        [ClientConfig]
        [DefaultValue(false)]
        public virtual bool RenderHidden
        {
            get
            {
                return this.renderHidden;
            }
        }

        [ClientConfig]
        [DefaultValue("")]
        public virtual string ExtraCls
        {
            get
            {
                return this.extraCls;
            }
        }
    }
}