<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AbsoluteLayout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />

    <ext:ViewPort runat="server">
        <Body>
            <ext:AbsoluteLayout runat="server">
                <Anchors>
                    <ext:Anchor>
                        <ext:Panel 
                            runat="server" 
                            BodyStyle="padding:15px;" 
                            Width="200" 
                            Height="100" 
                            Frame="true" 
                            Title="Panel 1" 
                            X="50" 
                            Y="50" 
                            Html="Positioned at x:50, y:50"
                            />
                    </ext:Anchor>
                    <ext:Anchor>
                        <ext:Panel 
                            runat="server" 
                            BodyStyle="padding:15px;" 
                            Width="200" 
                            Height="100" 
                            Frame="true" 
                            Title="Panel 2" 
                            X="125" 
                            Y="125" 
                            Html="Positioned at x:125, y:125"
                            />
                    </ext:Anchor>
                </Anchors>
            </ext:AbsoluteLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>
