using Coolite.Ext.Web;

[AjaxMethodProxyID(IDMode = AjaxMethodProxyIDMode.None)]
public partial class NoneID : System.Web.UI.MasterPage
{
    [AjaxMethod]
    public void HelloMasterPage()
    {
        Ext.Msg.Alert("Message", "Hello from MasterPage").Show();
    }
}
