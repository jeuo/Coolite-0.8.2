<%@ Page Language="C#" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HyperLink Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
   
    <h1>HyperLink Control Variations</h1>
    
    <h2>1. Simple HyperLink</h2>    
    <ext:HyperLink runat="server" NavigateUrl="http://www.coolite.com" Text="http://www.coolite.com" />
    
    <h2>2. HyperLink with icon</h2>    
    <ext:HyperLink ID="HyperLink1" runat="server" Icon="Coolite" NavigateUrl="http://www.coolite.com" Text="http://www.coolite.com" />
    
    <h2>3. HyperLink with right-align icon</h2>    
    <ext:HyperLink ID="HyperLink2" runat="server" Icon="Coolite" IconAlign="Right" NavigateUrl="http://www.coolite.com" Text="http://www.coolite.com" />
    
    <h2>4. Image HyperLink</h2>    
    <ext:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://www.coolite.com" ImageUrl="http://coolite.com/images/logo.gif" />
    
</body>
</html>
