<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Basic Window - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <h1>Simple Coolite Window Sample</h1>
            <p>The following example demonstrates how to create a Window and have the Window show when the Page first loads.</p>
        </div>
        
        <ext:Button ID="Button1" runat="server" Text="Show Window" Icon="Application">
            <Listeners>
                <Click Handler="#{Window1}.show();" />
            </Listeners>
        </ext:Button>
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="Hello World!" 
            Height="175px" 
            Width="300px"
            BodyStyle="padding: 6px; background-color: #fff;" 
            Collapsible="True" 
            Modal="True" 
            Icon="Application">
            <Body>
                This is my first <a href="http://www.extjs.com/">Ext</a> Window 
                using the <a href="http://www.coolite.com/"> Coolite Toolkit</a>.
            </Body>
        </ext:Window>
    </form>
</body>
</html>