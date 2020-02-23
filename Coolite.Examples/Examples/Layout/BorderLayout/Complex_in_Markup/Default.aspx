<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Complex BorderLayout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <h1>Complex BorderLayout in Markup</h1>
    
    <ext:Button 
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
        Title="Complex Layout"
        Icon="Coolite"
        Width="640" 
        Height="480" 
        Plain="true"
        Border="false"
        Collapsible="true"
        BodyBorder="false"
        AnimateTarget="Button1">
        <Body>
            <ext:BorderLayout runat="server">
                <West MinWidth="175" MaxWidth="400" Split="true" Collapsible="true">
                    <ext:Panel 
                        ID="WestPanel" 
                        runat="server" 
                        Title="West"
                        Width="175">
                        <Body>
                            <ext:Accordion runat="server" Animate="true">
                                <ext:Panel 
                                    ID="Navigation" 
                                    runat="server" 
                                    Title="Navigation" 
                                    Icon="FolderGo"
                                    Border="false" 
                                    BodyStyle="padding:6px;">
                                    <Body>West</Body>
                                </ext:Panel>
                                <ext:Panel 
                                    ID="Settings" 
                                    runat="server" 
                                    Title="Settings" 
                                    Icon="FolderWrench"
                                    Border="false" 
                                    BodyStyle="padding:6px;" 
                                    Collapsed="True">
                                    <Body>Some settings in here.</Body>
                                </ext:Panel>
                            </ext:Accordion>
                        </Body>
                    </ext:Panel>
                </West>
                <Center>
                    <ext:TabPanel 
                        ID="CenterPanel" 
                        runat="server" 
                        ActiveTabIndex="1">
                        <Tabs>
                            <ext:Tab 
                                ID="Tab1" 
                                runat="server" 
                                Title="Close Me" 
                                Closable="true" 
                                Border="false" 
                                BodyStyle="padding:6px;">
                                <Body>Hello...</Body>
                            </ext:Tab>
                            <ext:Tab 
                                ID="Tab2" 
                                runat="server" 
                                Title="Center" 
                                Border="false" 
                                BodyStyle="padding:6px;">
                                <Body>...World</Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </Center>
                <East Collapsible="true" Split="true" MinWidth="225">
                    <ext:Panel ID="EastPanel" runat="server" Width="225" Title="East">
                        <Body>
                            <ext:FitLayout runat="server">
                                <ext:TabPanel 
                                    runat="server" 
                                    ActiveTabIndex="0" 
                                    TabPosition="Bottom" 
                                    Border="false" ID="ctl71" Title="ctl71">
                                    <Tabs>
                                        <ext:Tab 
                                            runat="server" 
                                            Title="Tab 1" 
                                            Border="false" 
                                            BodyStyle="padding:6px;" ID="ctl72">
                                            <Body>East Tab 1</Body>
                                        </ext:Tab>
                                        <ext:Tab 
                                            runat="server" 
                                            Title="Tab 2" 
                                            Closable="true" 
                                            Border="false" 
                                            BodyStyle="padding:6px;" ID="ctl73">
                                            <Body>East Tab 2</Body>
                                        </ext:Tab>
                                    </Tabs>
                                </ext:TabPanel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </East>
                <South Split="true" Collapsible="true">
                    <ext:Panel 
                        ID="SouthPanel" 
                        runat="server"
                        Title="South" 
                        Height="150"
                        BodyStyle="padding:6px;" 
                        />
                </South>
            </ext:BorderLayout>
        </Body>
    </ext:Window>
</body>
</html>