<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TreePanel with Async TreeLoader using HttpHandler - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>TreePanel with Async TreeLoader using HttpHandler</h1> 
        
        <ext:TreePanel 
            ID="TreePanel1" 
            runat="server" 
            Title="Tree" 
            AutoHeight="true" 
            Border="false">
            <Loader>
                <ext:TreeLoader DataUrl="TreeLoader.ashx" />
            </Loader>
            <Root>
                <ext:AsyncTreeNode NodeID="0" Text="Root" />
            </Root>
        </ext:TreePanel>
    </form>
</body>
</html>