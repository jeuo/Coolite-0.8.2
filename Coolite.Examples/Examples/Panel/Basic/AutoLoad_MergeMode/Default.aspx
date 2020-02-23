<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void btnParent_Click(object sender, AjaxEventArgs e)
    {
        this.Label1.Text = "Parent [AjaxEvent]: " + DateTime.Now.ToLongTimeString();
        Ext.Msg.Alert("AjaxEvent", "Parent Button Clicked").Show();
    }

    [AjaxMethod]
    public void ButtonClickParent()
    {
        this.Label1.Text = "Parent [AjaxMethod]: " + DateTime.Now.ToLongTimeString();
        Ext.Msg.Alert("AjaxMethod", "Parent Button Clicked").Show();
    }
</script>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example</title>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
 
    <form id="Form1" runat="server">
        <ext:Panel 
            ID="Panel1" 
            runat="server" 
            Title="Parent" 
            Height="300" 
            Width="500"
            BodyStyle="padding:6px;">
            <AutoLoad Url="Child.aspx" Mode="Merge" />
            <Buttons>
                <ext:Button ID="btnParent" runat="server" Text="Submit [AjaxEvent]">
                    <AjaxEvents>
                        <Click OnEvent="btnParent_Click" />
                    </AjaxEvents>
                </ext:Button>
                <ext:Button ID="Button1" IDMode="Ignore" runat="server" Text="Submit [AjaxMethod]">
                    <Listeners>
                        <Click Handler="Coolite.AjaxMethods.ButtonClickParent();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Panel>
    
        <ext:Label ID="Label1" runat="server" />
    </form>
</body>
</html>
