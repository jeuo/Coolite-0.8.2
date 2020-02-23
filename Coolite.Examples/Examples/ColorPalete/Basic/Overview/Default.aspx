<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ColorPalate Overview - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Color_Changed(object sender, EventArgs e)
        {
            this.Label1.Html = string.Format("Choosen color: #<span style='color:#{0};font-weight:bold;'>{0}</span>", this.ColorPalette1.Value);
        }

        protected void AjaxColor_Changed(object sender, AjaxEventArgs e)
        {
            this.Label2.Html = string.Format("Choosen color: #<span style='color:#{0};font-weight:bold;'>{0}</span>", this.ColorPalette2.Value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <h2>1. PostBack model</h2>
        
        <ext:ColorPalette 
            ID="ColorPalette1" 
            runat="server" 
            AutoPostBack="true" 
            OnColorChanged="Color_Changed"
            />
        
        <ext:Label ID="Label1" runat="server" />
        
        <h2>2. AjaxEvent model</h2>
        
        <ext:ColorPalette ID="ColorPalette2" runat="server">
            <AjaxEvents>
                <Select OnEvent="AjaxColor_Changed" />
            </AjaxEvents>
        </ext:ColorPalette>
        
        <ext:Label ID="Label2" runat="server" />
    </form>
</body>
</html>
