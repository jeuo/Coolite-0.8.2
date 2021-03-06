﻿<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Flat Toolbar - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />

    <h2>Flat Toolbar</h2>    
    
    <ext:Toolbar runat="server" Flat="true">
        <Items>                        
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
            
            <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group2" Icon="GroupAdd" Pressed="true" />
            <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group2" Icon="GroupDelete" />
            <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group2" Icon="GroupEdit" />
        </Items>
    </ext:Toolbar>
    
    <h2>Panel with Flat Toolbar</h2>
    
    <ext:Panel ID="Panel1" runat="server" Title="Panel with toolbar" Width="300" Height="185" Frame="true">
        <TopBar>
            <ext:Toolbar runat="server" Flat="true">
                <Items>                        
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
                    
                    <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group1" Icon="GroupAdd" Pressed="true" />
                    <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group1" Icon="GroupDelete" />
                    <ext:ToolbarButton runat="server" EnableToggle="true" ToggleGroup="Group1" Icon="GroupEdit" />
                </Items>
            </ext:Toolbar>
        </TopBar>
    </ext:Panel>
</body>
</html>