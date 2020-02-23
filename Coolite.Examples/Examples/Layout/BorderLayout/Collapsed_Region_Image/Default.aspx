<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image on Collapsed BorderLayout Region - Coolite Toolkit Examples</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .west-panel .x-layout-collapsed-west{
            background: url(collapsed-west.png) no-repeat center;
        }
        
        .south-panel .x-layout-collapsed-south{
            background: url(collapsed-south.png) no-repeat center;
        }
    </style>
</head>
<body>
    <ext:ScriptManager runat="server" />

    <h1>Image on Collapsed BorderLayout Region</h1>
    
    <ext:Window 
        runat="server" 
        Title="Collapsed Region Image" 
        Icon="Coolite"
        Width="600" 
        Height="350"
        Border="false"
        Closable="false"
        X="100"
        Y="100"
        Plain="true">
        <Body>
            <ext:BorderLayout runat="server">
                <West Collapsible="true" MinWidth="175" Split="true">
                    <ext:Panel 
                        runat="server"                             
                        Width="175" 
                        CtCls="west-panel"
                        Title="Navigation" 
                        Collapsed="true"
                        BodyStyle="padding:5px;"
                        Html="Collapse Panel to see image."
                        />
                </West>
                <Center>
                    <ext:Panel runat="server" Title="Center region" />
                </Center>
                <South Collapsible="true" MinHeight="100" Split="true">
                    <ext:Panel 
                        runat="server"                             
                        Height="100" 
                        CtCls="south-panel"
                        Title="Footer"
                        Collapsed="true"
                        />
                </South>                    
            </ext:BorderLayout>
        </Body>
    </ext:Window>
</body>
</html>