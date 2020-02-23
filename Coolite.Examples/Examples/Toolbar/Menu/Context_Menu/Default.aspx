<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Example</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var setColor = function (menu, color) {
            var cmp;
            if (menu.lastTargetIn(Panel1)) {
                cmp = Panel1;
            } else if (menu.lastTargetIn(Panel2)) {
                cmp = Panel2;
            }
            
            cmp.body.applyStyles(String.format('background-color:#{0}', color));
        }
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager2" runat="server" />
        
        <ext:ColorMenu ID="ColorMenu1" runat="server">
            <Palette runat="server">
                <Listeners>
                    <Select Handler="setColor(#{ColorMenu1}, color);" />
                </Listeners>
            </Palette>
        </ext:ColorMenu>
        
        <ext:Panel 
            ID="Panel1" 
            runat="server" 
            Height="200"
            Title="Right-Click on this Panel"
            ContextMenuID="ColorMenu1"
            />
        
        <ext:Panel 
            ID="Panel2" 
            runat="server" 
            Height="200"
            Title="Right-Click on this Panel too!"
            ContextMenuID="ColorMenu1"
            />
        
    </form>
</body>
</html>