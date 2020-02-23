using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coolite.Ext.Web;


namespace Coolite.Examples.HighLighter
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    public partial class HighLighterButton : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string Url
        {
            get; set;
        }

        public Syntax Syntax
        {
            get;set;
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Coolite.Ext.Web.Button Button
        {
            get
            {
                return this.btnSource;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Coolite.Ext.Web.Window Window
        {
            get
            {
                return this.winSource;
            }
        }

        protected void btnSource_Click(object sender, AjaxEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Url))
            {
                Uri url = new Uri(HttpContext.Current.Request.Url, this.Url);
                switch (this.Syntax)
                {
                    case Syntax.Aspx:
                        this.Window.Html = HighLighterUtils.AspxToHtml(url);
                        break;
                    case Syntax.CSharp:
                        this.Window.Html = HighLighterUtils.CSharpToHtml(url);
                        break;
                    case Syntax.Xml:
                        this.Window.Html = HighLighterUtils.XmlToHtml(url);
                        break;
                    case Syntax.JavaScript:
                        this.Window.Html = HighLighterUtils.JsToHtml(url);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                this.Window.Show();
            }
        }
    }

    public enum Syntax
    {
        Aspx,
        CSharp,
        Xml,
        JavaScript
    }
}