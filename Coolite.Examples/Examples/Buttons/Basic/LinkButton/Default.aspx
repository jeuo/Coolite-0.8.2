<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
    protected void Button_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("Clicked", "LinkButton").Show();
    }
</script>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LinkButton Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
   
    <h1>LinkButton Control Variations</h1>
    
    <h2>1. Simple LinkButton</h2>
    
    <ext:LinkButton runat="server" Text="Click to test">
        <AjaxEvents>
            <Click OnEvent="Button_Click" />
        </AjaxEvents>
    </ext:LinkButton>
    
    <h2>2. LinkButton with icon and ajax event</h2>
    
    <ext:LinkButton ID="HyperLink1" runat="server" Icon="Accept" Text="Click to test" />
    
    <h2>3. LinkButton with right-align icon</h2>
    
    <ext:LinkButton runat="server" Icon="Accept" IconAlign="Right" Text="Click to test" />
    
    <h2>4. LinkButton with menu</h2>
    
    <ext:LinkButton runat="server" Icon="Accept" Text="Click to test">
        <Menu>
            <ext:Menu runat="server">
                <Items>
                    <ext:MenuItem runat="server" Text="Add" Icon="Add" />
                    <ext:MenuItem runat="server" Text="Remove" Icon="Delete" />
                </Items>
            </ext:Menu>
        </Menu>
    </ext:LinkButton>
    
    <h2>5. LinkButton in group</h2>
      
    <ext:LinkButton runat="server" Icon="GroupAdd" Text="Add group" ToggleGroup="Group1" />&nbsp;&nbsp;
    <ext:LinkButton runat="server" Icon="GroupDelete" Text="Delete group" ToggleGroup="Group1" />&nbsp;&nbsp;
    <ext:LinkButton runat="server" Icon="GroupEdit" Text="Edit group" ToggleGroup="Group1" />
</body>
</html>
