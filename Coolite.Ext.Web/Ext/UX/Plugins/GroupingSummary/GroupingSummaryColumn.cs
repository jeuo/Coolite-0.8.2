/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    public class GroupingSummaryColumn : Column
    {

        //summaryType:'totalCost',
        //summaryRenderer: Ext.util.Format.usMoney

        //groupable: false,

        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(SummaryType.None)]
        [NotifyParentProperty(true)]
        public virtual SummaryType SummaryType
        {
            get
            {
                object obj = this.ViewState["SummaryType"];
                return (obj == null) ? SummaryType.None : (SummaryType)obj;
            }
            set
            {
                this.ViewState["SummaryType"] = value;
            }
        }

        [ClientConfig("summaryType")]
        [Category("Config Options")]
        [DefaultValue(true)]
        public virtual string CustomSummaryType
        {
            get
            {
                return (string)this.ViewState["CustomSummaryType"] ?? "";
            }
            set
            {
                this.ViewState["CustomSummaryType"] = value;
            }
        }

        private Renderer summaryRenderer;

        [ClientConfig(typeof(RendererJsonConverter))]
        [Category("Config Options")]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ViewStateMember]
        public virtual Renderer SummaryRenderer
        {
            get
            {
                if (this.summaryRenderer == null)
                {
                    this.summaryRenderer = new Renderer();
                }

                return this.summaryRenderer;
            }
            set
            {
                this.summaryRenderer = value;
            }
        }

    }

    public enum SummaryType
    {
        None,
        Average,
        Count,
        Max,
        Min,
        Sum,
        TotalCost
    }
}
