<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MenuPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function MenuItemClick(menuItem) {
            CenterPanel.body.update(String.format("Clicked: {0}", menuItem.text)).highlight();            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />

        <h1>MenuPanel Example</h1>        
        
        <ext:Window runat="server" 
            Title="MenuPanel Window" 
            Icon="Accept"
            Closable="false" 
            CenterOnLoad="true" 
            Width="600" 
            Height="370" >
            
            <Body>
                <ext:BorderLayout runat="server">
                    <West Split="true">
                        <ext:Panel runat="server" Border="false" Width="350">
                            <Body>
                                <ext:Accordion runat="server">
                                    <ext:MenuPanel runat="server" Title="Menu panel with selection saving" Icon="ArrowSwitch">
                                        <Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="Point 1" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 2" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 3" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 4" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 5" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 6" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 7" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 8" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 9" Icon="ArrowRight" />
                                            </Items>
                                            <Listeners>
                                                <ItemClick Fn="MenuItemClick" />
                                            </Listeners>                                            
                                        </Menu>                                        
                                    </ext:MenuPanel>
                                    <ext:MenuPanel runat="server" Title="Menu panel without selection saving" SaveSelection="false" Icon="ArrowSwitch">
                                        <Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="Point 1" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 2" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 3" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 4" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 5" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 6" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 7" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 8" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 9" Icon="ArrowRight" />
                                            </Items>
                                            <Listeners>
                                                <ItemClick Fn="MenuItemClick" />
                                            </Listeners>
                                        </Menu>
                                    </ext:MenuPanel>
                                    <ext:MenuPanel runat="server" Title="Menu with predefined selection" SelectedIndex="1" Icon="ArrowSwitch">
                                        <Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="Point 1" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 2" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 3" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 4" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 5" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 6" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 7" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 8" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 9" Icon="ArrowRight" />
                                            </Items>
                                            <Listeners>
                                                <ItemClick Fn="MenuItemClick" />
                                            </Listeners>
                                        </Menu>
                                    </ext:MenuPanel>
                                    <ext:MenuPanel runat="server" Title="Menu 4" Icon="ArrowSwitch">
                                        <Menu runat="server">
                                            <Items>
                                                <ext:MenuItem runat="server" Text="Point 1" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 2" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 3" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 4" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 5" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 6" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 7" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 8" Icon="ArrowRight" />
                                                <ext:MenuItem runat="server" Text="Point 9" Icon="ArrowRight" />
                                            </Items>
                                            <Listeners>
                                                <ItemClick Fn="MenuItemClick" />
                                            </Listeners>
                                        </Menu>
                                    </ext:MenuPanel>
                                    <ext:MenuPanel runat="server" Title="Menu 5" Icon="ArrowSwitch">
                                        <Menu runat="server">
                                            <Items>
                                                <ext:MenuItem ID="MenuItem1" runat="server" Text="Point 1" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem2" runat="server" Text="Point 2" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem3" runat="server" Text="Point 3" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem4" runat="server" Text="Point 4" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem5" runat="server" Text="Point 5" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem6" runat="server" Text="Point 6" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem7" runat="server" Text="Point 7" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem8" runat="server" Text="Point 8" Icon="ArrowRight" />
                                                <ext:MenuItem ID="MenuItem9" runat="server" Text="Point 9" Icon="ArrowRight" />
                                            </Items>
                                            <Listeners>
                                                <ItemClick Fn="MenuItemClick" />
                                            </Listeners>
                                        </Menu>
                                    </ext:MenuPanel>
                                </ext:Accordion>
                            </Body>
                        </ext:Panel>
                    </West>
                    <Center>
                        <ext:Panel ID="CenterPanel" runat="server" Title="Center" BodyStyle="padding:20px;" />
                    </Center>
                </ext:BorderLayout>
            </Body>
        
        </ext:Window>
                
    </form>
</body>
</html>