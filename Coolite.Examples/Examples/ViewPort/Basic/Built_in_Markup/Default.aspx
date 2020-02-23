<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViewPort with BorderLayout - Coolite Toolkit Examples</title>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North Split="true" Collapsible="true">
                    <ext:Panel 
                        ID="Panel1" 
                        runat="server"
                        Title="North" 
                        Height="150"
                        BodyStyle="padding:6px;"
                        Html="North"
                        />
                </North>
                <West MinWidth="225" MaxWidth="400" Split="true" Collapsible="true">
                    <ext:Panel 
                        ID="WestPanel" 
                        runat="server" 
                        Title="West"
                        Width="225">
                        <Body>
                            <ext:Accordion 
                                ID="Accordion1" 
                                runat="server" 
                                Animate="true">
                                <ext:Panel 
                                    ID="Navigation" 
                                    runat="server" 
                                    Title="Navigation" 
                                    Border="false" 
                                    BodyStyle="padding:6px;"
                                    Icon="FolderGo"
                                    Html="West"
                                    />
                                <ext:Panel 
                                    ID="Settings" 
                                    runat="server" 
                                    Title="Settings" 
                                    Border="false" 
                                    BodyStyle="padding:6px;"
                                    Icon="FolderWrench"
                                    Html="Some settings in here"
                                    />
                            </ext:Accordion>
                        </Body>
                    </ext:Panel>
                </West>
                <Center>
                    <ext:TabPanel 
                        ID="CenterPanel" 
                        runat="server">
                        <Tabs>
                            <ext:Tab 
                                ID="CenterTab1" 
                                runat="server" 
                                Title="Center" 
                                Border="false" 
                                BodyStyle="padding:6px;"
                                Html="<h1>ViewPort with BorderLayout</h1>"
                                />
                            <ext:Tab 
                                ID="CenterTab2" 
                                runat="server" 
                                Title="Close Me" 
                                Closable="true" 
                                Border="false" 
                                BodyStyle="padding:6px;"
                                Html="Closeable Tab"
                                />
                        </Tabs>
                    </ext:TabPanel>
                </Center>
                <East Collapsible="true" Split="true" MinWidth="225">
                    <ext:Panel ID="EastPanel" runat="server" Width="225" Title="East">
                        <Body>
                            <ext:FitLayout ID="FitLayout1" runat="server">
                                <ext:TabPanel ID="TabPanel1" 
                                    runat="server"
                                    ActiveTabIndex="1" 
                                    TabPosition="Bottom" 
                                    Border="false">
                                    <Tabs>
                                        <ext:Tab 
                                            ID="Tab1" 
                                            runat="server" 
                                            Title="Tab 1" 
                                            Border="false" 
                                            BodyStyle="padding:6px;"
                                            Html="East Tab 1"
                                            />
                                        <ext:Tab 
                                            ID="Tab2" 
                                            runat="server" 
                                            Title="Tab 2" 
                                            Closable="true" 
                                            Border="false" 
                                            BodyStyle="padding:6px;"
                                            Html="East Tab 2"
                                            />
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
                        Html="South"
                        />
                </South>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>