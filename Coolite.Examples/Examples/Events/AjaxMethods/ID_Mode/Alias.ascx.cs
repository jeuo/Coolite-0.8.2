using Coolite.Ext.Web;

[AjaxMethodProxyID(IDMode = AjaxMethodProxyIDMode.Alias, Alias = "UC")]
public partial class AliasID : System.Web.UI.UserControl
{
    [AjaxMethod]
    public void HelloUserControl()
    {
        Ext.Msg.Alert("Message", "Hello from UserControl").Show();
    }
}