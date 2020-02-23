<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Show and Hide Tabs - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Show and Hide Tabs</h1>
        
        <ext:TabPanel ID="TabPanel1" runat="server" Width="800" Height="300">
            <Tabs>
                <ext:Tab ID="Tab1" Title="Tab1" runat="server" Closable="true" CloseAction="Hide" Html="Tab1">
                    <Listeners>
                        <Close Handler="#{ShowTab1}.setDisabled(false);#{CloseTab1}.setDisabled(true);" />
                    </Listeners>
                </ext:Tab>
                <ext:Tab ID="Tab2" Title="Tab2" runat="server" Closable="true" CloseAction="Hide" Html="Tab2">
                    <Listeners>
                        <Close Handler="#{ShowTab2}.setDisabled(false);" />
                    </Listeners>
                </ext:Tab>
                <ext:Tab ID="Tab3" Title="Tab3" runat="server" Closable="true" CloseAction="Hide" Html="Tab3">
                    <Listeners>
                        <Close Handler="#{ShowTab3}.setDisabled(false);" />
                    </Listeners>
                </ext:Tab>
                
                <ext:Tab ID="Tab4" Title="Tab4" runat="server" Closable="true" CloseAction="Hide" Html="Tab4">
                    <Listeners>
                        <Close Handler="#{ShowTab4}.setDisabled(false);" />
                    </Listeners>
                </ext:Tab>
                
                <ext:Tab ID="Tab5" Title="Tab5 - Close Event" runat="server" Closable="true" CloseAction="Close">
                    <Body>
                        Tab5 with CloseAction='Close' (after closing the tab can't be reshow)
                    </Body>
                    <Listeners>
                        <Close Handler="alert('Tab5 has been closed');" />
                    </Listeners>
                </ext:Tab>
                
                <ext:Tab ID="Tab6" Title="Tab6" runat="server" Closable="true" CloseAction="Hide" Hidden="true" Html="PreHidden Tab6">
                    <Listeners>
                        <Close Handler="#{ShowTab6}.setDisabled(false);" />
                    </Listeners>
                </ext:Tab>
            </Tabs>
            
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarButton ID="CloseTab1" runat="server" Text="Close Tab1">
                            <Listeners>
                                <Click Handler="#{TabPanel1}.closeTab(#{Tab1});this.setDisabled(true);#{ShowTab1}.setDisabled(false);" />
                            </Listeners>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarFill />
                        <ext:ToolbarButton ID="ShowTab1" runat="server" Text="Show Tab1" Disabled="true">
                            <Listeners>
                                <Click Handler="#{TabPanel1}.addTab(#{Tab1});this.setDisabled(true);#{CloseTab1}.setDisabled(false);" />
                            </Listeners>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ShowTab2" runat="server" Text="Show Tab2" Disabled="true">
                            <Listeners>
                                <Click Handler="#{TabPanel1}.addTab(#{Tab2});this.setDisabled(true);" />
                            </Listeners>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ShowTab3" runat="server" Text="Show Tab3 at begin" Disabled="true">
                            <Listeners>
                                <Click Handler="#{TabPanel1}.addTab(#{Tab3}, 0);this.setDisabled(true);" />
                            </Listeners>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ShowTab4" runat="server" Text="Show Tab4 without activate" Disabled="true">
                            <Listeners>
                                <Click Handler="#{TabPanel1}.addTab(#{Tab4}, false);this.setDisabled(true);" />
                            </Listeners>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarButton ID="ShowTab6" runat="server" Text="Show Tab6" >
                            <Listeners>
                                <Click Handler="#{TabPanel1}.addTab(#{Tab6});this.setDisabled(true);" />
                            </Listeners>
                        </ext:ToolbarButton>
                    </Items>
                </ext:Toolbar>
            </BottomBar>
        </ext:TabPanel>
    </form>
</body>
</html>
