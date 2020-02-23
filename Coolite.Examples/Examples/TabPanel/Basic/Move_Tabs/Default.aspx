<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Moving Tab between TabPanels - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        var moveTab = function(source, destination) {
            var tab = source.getActiveTab();
            source.closeTab(tab, 'hide');
            destination.addTab(tab);
        }
    </script>
    
    <style type="text/css">
        .x-tab-strip-top {
        	height: 23px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Moving Tab between TabPanels</h1>
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Width="600" 
            Height="250" 
            Icon="Tab" 
            Title="Moving tabs"
            X="100"
            Y="100">
            <Body>
                <ext:BorderLayout runat="server">
                    <West MarginsSummary="5 0 5 5">
                        <ext:TabPanel ID="TabPanel1" runat="server" Width="200" EnableTabScroll="true">   
                            <Tabs>
                                <ext:Tab ID="Tab1" runat="server" Title="Tab1" Html="Tab1" BodyStyle="padding:5px" />
                                <ext:Tab ID="Tab2" runat="server" Title="Tab2" Html="Tab2" BodyStyle="padding:5px" />
                                <ext:Tab ID="Tab3" runat="server" Title="Tab3" Html="Tab3" BodyStyle="padding:5px" />
                            </Tabs>                            
                        </ext:TabPanel>
                    </West>
                    
                    <Center MarginsSummary="5 5 5 5">
                        <ext:Panel runat="server">
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:ToolbarButton runat="server" Icon="ArrowRight">
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Move Tab" Html="West To East" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Handler="moveTab(#{TabPanel1}, #{TabPanel2});" />
                                            </Listeners>
                                        </ext:ToolbarButton>
                                        
                                        <ext:ToolbarFill />
                                        
                                        <ext:ToolbarButton runat="server" Icon="ArrowLeft">
                                            <ToolTips>
                                                <ext:ToolTip runat="server" Title="Move Tab" Html="East to West" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Handler="moveTab(#{TabPanel2}, #{TabPanel1});" />
                                            </Listeners>
                                        </ext:ToolbarButton>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:Panel>
                    </Center>
                    
                    <East MarginsSummary="5 5 5 0">
                        <ext:TabPanel ID="TabPanel2" runat="server" Width="200" EnableTabScroll="true">                                    
                            <Tabs>
                                <ext:Tab ID="Tab4" runat="server" Title="Tab4" Html="Tab4" BodyStyle="padding:5px" />
                                <ext:Tab ID="Tab5" runat="server" Title="Tab5" Html="Tab5" BodyStyle="padding:5px" />
                                <ext:Tab ID="Tab6" runat="server" Title="Tab6" Html="Tab6" BodyStyle="padding:5px" />
                            </Tabs>
                        </ext:TabPanel>
                    </East>
                </ext:BorderLayout>
            </Body>
        </ext:Window>
        
        
    </form>
</body>
</html>
