<%@ Page Language="C#" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Label Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
   
    <h1>Label Control Variations</h1>
    
    <h2>1. Simple label</h2>    
    <ext:Label runat="server" Text="Label" />
    
    <h2>2. Html label</h2>    
    <ext:Label runat="server" Html="<b>Label</b>" />
    
    <h2>3. Label with icon</h2>    
    <ext:Label runat="server" Text="Label" Icon="Note" />
    
    <h2>4. Label with right-align icon</h2>    
    <ext:Label ID="Label1" runat="server" Text="Label" Icon="Note" IconAlign="Right" />    
    
</body>
</html>
