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
    [InstanceOf(ClassName = "Coolite.Ext.HttpWriteProxy")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HttpWriteProxy : HttpProxy
    {
        /// <summary>
        /// If save handler is web service then response will be xml. This option specifies how to handle response.
        /// If false then the response is handled as json
        /// If true then the response is handled as xml
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [NotifyParentProperty(true)]
        [Description("If save handler is web service then response will be xml. This option specifies how to handle response.")]
        public bool HandleSaveResponseAsXml
        {
            get
            {
                object obj = this.ViewState["HandleSaveResponseAsXml"];
                return obj == null ? false : (bool)obj;
            }
            set
            {
                this.ViewState["HandleSaveResponseAsXml"] = value;
            }
        }
    }
}
