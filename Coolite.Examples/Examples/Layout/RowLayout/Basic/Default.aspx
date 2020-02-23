<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Row Layout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <ext:ViewPort runat="server">
        <Body>
            <ext:RowLayout runat="server" Split="true">
                <ext:LayoutRow RowHeight="0.25">
                    <ext:Panel ID="Panel1" runat="server" Title="Initial Height = 25%" />
                </ext:LayoutRow>
                
                <ext:LayoutRow>
                    <ext:Panel ID="Panel2" runat="server" Title="Initial Height = 100px" Height="100" />
                </ext:LayoutRow>
                
                <ext:LayoutRow RowHeight="0.75">
                    <ext:Panel ID="Panel3" runat="server" Title="Initial Height = 75%" />
                </ext:LayoutRow>
            </ext:RowLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>
