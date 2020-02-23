
<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void btnChild_Click(object sender, AjaxEventArgs e)
    {
        this.Label2.Text = "Child [AjaxEvent]: " + DateTime.Now.ToLongTimeString();
        Ext.Msg.Alert("AjaxEvent", "Child Button Clicked").Show();
    }

    [AjaxMethod]
    public void ButtonClickChild()
    {
        this.Label2.Text = "Child [AjaxMethod]: " + DateTime.Now.ToLongTimeString();
        Ext.Msg.Alert("AjaxMethod", "Child Button Clicked").Show();
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Child</title>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" RenderScripts="None" />
    
    <ext:Panel 
        ID="Panel2" 
        runat="server" 
        Title="Child" 
        Width="300" 
        Height="185"
        Frame="true">
        <Body>
            <ext:ContainerLayout ID="ContainerLayout1" runat="server">
                <ext:Label ID="Label2" runat="server" />
            </ext:ContainerLayout>
        </Body>
        <Buttons>
            <ext:Button ID="btnChild" runat="server" Text="Submit [AjaxEvent]">
                <AjaxEvents>
                    <Click OnEvent="btnChild_Click" Url="Child.aspx" />
                </AjaxEvents>
            </ext:Button>
            <ext:Button ID="Button1" IDMode="Ignore" runat="server" Text="Submit [AjaxMethod]">
                <Listeners>
                    <Click Handler="Coolite.AjaxMethods.ButtonClickChild({ url: 'Child.aspx' });" />
                </Listeners>
            </ext:Button>
        </Buttons>
    </ext:Panel>
</body>
</html>
