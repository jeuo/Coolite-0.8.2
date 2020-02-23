<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Menu3_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("Click", e.ExtraParams["Param"]);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Field Note - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server"/>

        <ext:Panel ID="Panel1" runat="server" Title="Panel with toolbars" Width="600" Height="300">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarButton runat="server" Icon="Add" StandOut="true">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Add');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="StandOut button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Accept" StandOut="true">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Accept');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="StandOut button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Delete" StandOut="true">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Delete');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="StandOut button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarSeparator/>
                        
                        <ext:ToolbarButton runat="server" Icon="Add">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Add');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Simple button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Accept">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Accept');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Simple button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Delete">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Delete');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Simple button" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarSeparator/>
                        
                        <ext:ToolbarButton runat="server" Icon="Add" Text="Add">
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Add');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Button with text" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Accept" Text="Accept" Disabled="true" >
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Accept');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Disabled" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                         <ext:ToolbarButton runat="server" Icon="Delete" Text="Delete" >
                            <Listeners>
                                <Click Handler="Ext.Msg.alert('Click','Click on Delete');" />                                
                            </Listeners>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Button with text" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarSeparator/>
                        
                        <ext:ToolbarSpacer runat="server" />
                        
                        <ext:ToolbarButton runat="server" Icon="Application" Text="With menu">
                            <Menu>
                              <ext:Menu runat="server">
                                <Items>
                                    <ext:MenuItem runat="server" Icon="Accept" Text="Menu 1" Handler="function(){alert('Menu 1');}"></ext:MenuItem>
                                    <ext:MenuItem runat="server" Icon="Add" Text="Menu 2">
                                        <Listeners>
                                            <Click Handler="alert('Menu 2');" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem runat="server" Text="Menu 3">
                                        <AjaxEvents>
                                            <Click OnEvent="Menu3_Click">
                                                <EventMask ShowMask="true" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="Param" Value="Menu 3" Mode="Value" />
                                                </ExtraParams>
                                            </Click>
                                        </AjaxEvents>
                                    </ext:MenuItem>
                                    <ext:MenuSeparator runat="server"></ext:MenuSeparator>                                   
                                    <ext:MenuItem runat="server" Text="Menu 5"></ext:MenuItem>
                                </Items>
                              </ext:Menu>
                            </Menu>
                            <ToolTips>
                                <ext:ToolTip runat="server" Html="Application" />
                            </ToolTips>
                        </ext:ToolbarButton>
                        
                        <ext:ToolbarFill />
                        
                        <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="G1" Icon="GroupAdd" Pressed="true" />
                        <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="G1" Icon="GroupDelete" />
                        <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="G1" Icon="GroupEdit" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ComboBox runat="server">
                            <Items>
                                <ext:ListItem Text="Option1" />
                                <ext:ListItem Text="Option2" />
                                <ext:ListItem Text="Option3" />
                                <ext:ListItem Text="Option4" />
                                <ext:ListItem Text="Option5" />
                            </Items>
                        </ext:ComboBox>
                        
                        <ext:ToolbarSpacer/>
                        
                        <ext:TextField runat="server" EmptyText="[Enter some text]" />
                        <ext:ToolbarSpacer />
                        <ext:ToolbarHtmlElement Target="#{divOnToolbar}" />
                        <ext:ToolbarFill />
                        
                        <ext:DateField runat="server" />
                    </Items>
                </ext:Toolbar>
            </BottomBar>
        </ext:Panel>
        
        <div class="x-hide-display">
            <div id="divOnToolbar"  style="color:White; background-color:Black; padding:2px 5px;">I am div on toolbar</div>
        </div>        
    </form>
</body>
</html>