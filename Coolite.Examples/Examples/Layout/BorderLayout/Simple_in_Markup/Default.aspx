<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple BorderLayout in Markup - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />

    <h1>Simple BorderLayout in Markup</h1>
    
    <ext:Button 
        ID="Button1" 
        runat="server" 
        Text="Show Window" 
        Icon="Application">
        <Listeners>
            <Click Handler="#{Window1}.show();" />
        </Listeners>    
    </ext:Button>
        
    <ext:Window 
        ID="Window1" 
        runat="server" 
        Title="Simple Layout" 
        Icon="Coolite"
        Width="600" 
        Height="350"
        Border="false" 
        Collapsible="true"
        Plain="true">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <West Collapsible="true" MinWidth="175" Split="true">
                    <ext:Panel runat="server" Width="175" Title="Navigation" />
                </West>
                <Center>
                    <ext:TabPanel runat="server" ActiveTabIndex="0">
                        <Tabs>
                            <ext:Tab 
                                ID="Tab1" 
                                runat="server" 
                                Title="First Tab" 
                                BodyStyle="padding: 6px;">
                                <Body>First Tab</Body>
                            </ext:Tab>
                            <ext:Tab 
                                ID="Tab2" 
                                runat="server" 
                                Title="Another Tab" 
                                BodyStyle="padding: 6px;">
                                <Body>Another Tab</Body>
                            </ext:Tab>
                            <ext:Tab 
                                ID="Tab3" 
                                runat="server" 
                                Title="Closeable Tab" 
                                Closable="true" 
                                BodyStyle="padding: 6px;">
                                <Body>Closable Tab</Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </Center>
            </ext:BorderLayout>
        </Body>
    </ext:Window>
</body>
</html>