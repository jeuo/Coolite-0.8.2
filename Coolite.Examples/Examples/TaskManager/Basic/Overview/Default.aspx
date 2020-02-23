<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <script runat="server">
        protected void RefreshTime(object sender, AjaxEventArgs e)
        {
            this.ServerTimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    </script>     
</head>
<body>    
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="var msg = function (text) { 
                    #{LogArea}.setValue(
                        String.format('{0}\n{1} : {2}', 
                        #{LogArea}.getValue(), 
                        text, 
                        new Date().dateFormat('H:i:s'))); 
                    }" />
            </Listeners>
        </ext:ScriptManager>        
        
        <ext:ViewPort runat="server">
            <Body>
                <ext:BorderLayout runat="server">
                    <Center>
                        <ext:Panel runat="server" Title="TaskManager example" Icon="Time" Border="false">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:ToolbarButton 
                                            ID="btnStartAll" 
                                            runat="server" 
                                            Text="Start All Tasks" 
                                            Icon="ControlPlayBlue" 
                                            Enabled="false">
                                            <Listeners>
                                                <Click Handler="el.disable();#{TaskManager1}.startAll();#{btnStopAll}.enable()" />
                                            </Listeners>
                                        </ext:ToolbarButton>
                                        <ext:ToolbarButton 
                                            ID="btnStopAll" 
                                            runat="server" 
                                            Text="Stop All Tasks" 
                                            Icon="ControlStopBlue">
                                            <Listeners>
                                                <Click Handler="el.disable();#{TaskManager1}.stopAll();#{btnStartAll}.enable();" />
                                            </Listeners>
                                        </ext:ToolbarButton>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Body>
                                <ext:ColumnLayout runat="server" FitHeight="true">
                                    <ext:LayoutColumn ColumnWidth="0.5">
                                        <ext:Panel 
                                            runat="server" 
                                            Title="Local time" 
                                            BodyStyle="padding:20px;text-align:center;">
                                            <Body>
                                                <ext:ContainerLayout runat="server">
                                                    <ext:Label ID="LocalTimeLabel" runat="server" StyleSpec="font-weight:bold;font-size:500%;" />
                                                </ext:ContainerLayout>
                                            </Body>
                                            <BottomBar>
                                                <ext:Toolbar runat="server">
                                                    <Items>
                                                         <ext:ToolbarButton ID="StartLocalTime" runat="server" Text="Start Task">
                                                            <Listeners>
                                                                <Click Handler="#{TaskManager1}.startTask(0);" />
                                                            </Listeners>
                                                        </ext:ToolbarButton>
                                                        <ext:ToolbarButton ID="StopLocalTime" runat="server" Text="Stop Task">
                                                            <Listeners>
                                                                <Click Handler="#{TaskManager1}.stopTask(0);" />
                                                            </Listeners>
                                                        </ext:ToolbarButton>
                                                    </Items>
                                                </ext:Toolbar>
                                            </BottomBar>
                                        </ext:Panel>
                                    </ext:LayoutColumn>
                                    
                                    <ext:LayoutColumn ColumnWidth="0.5">
                                        <ext:Panel 
                                            ID="ServerTimeContainer" 
                                            runat="server" 
                                            Title="Server Time (update every 5 seconds)" 
                                            BodyStyle="padding:20px;text-align:center;">
                                            <Body>
                                                <ext:ContainerLayout runat="server">
                                                    <ext:Label ID="ServerTimeLabel" runat="server" StyleSpec="font-weight:bold;font-size:500%;" />
                                                </ext:ContainerLayout>
                                            </Body>
                                            <BottomBar>
                                                <ext:Toolbar runat="server">
                                                    <Items>
                                                         <ext:ToolbarButton ID="StartServerTime" runat="server" Text="Start Task">
                                                            <Listeners>
                                                                <Click Handler="#{TaskManager1}.startTask('servertime');" />
                                                            </Listeners>
                                                        </ext:ToolbarButton>
                                                        <ext:ToolbarButton ID="StopServerTime" runat="server" Text="Stop Task">
                                                            <Listeners>
                                                                <Click Handler="#{TaskManager1}.stopTask('servertime');" />
                                                            </Listeners>
                                                        </ext:ToolbarButton>
                                                    </Items>
                                                </ext:Toolbar>
                                            </BottomBar>
                                        </ext:Panel>
                                    </ext:LayoutColumn>
                                </ext:ColumnLayout>
                            </Body>
                        </ext:Panel>
                    </Center>
                    
                    <South>
                        <ext:Panel runat="server" Height="200" Border="false">
                            <Body>
                                <ext:FitLayout runat="server">
                                    <ext:TextArea ID="LogArea" runat="server" />
                                </ext:FitLayout>
                            </Body>
                        </ext:Panel>
                    </South>
                </ext:BorderLayout>
            </Body>
        </ext:ViewPort>
        
        <ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task                    
                    OnStart="
                        #{StartLocalTime}.setDisabled(true);
                        #{StopLocalTime}.setDisabled(false);
                        msg('Start Client');"
                    OnStop="
                        #{StartLocalTime}.setDisabled(false);
                        #{StopLocalTime}.setDisabled(true);
                        msg('Stop Client');">
                    <Listeners>
                        <Update Handler="#{LocalTimeLabel}.setText(new Date().dateFormat('H:i:s'));" />
                    </Listeners>    
                </ext:Task>
                
                <ext:Task 
                    TaskID="servertime"
                    Interval="5000"
                    OnStart="
                        #{StartServerTime}.setDisabled(true);
                        #{StopServerTime}.setDisabled(false); 
                        msg('Start Server')"
                    OnStop="
                        #{StartServerTime}.setDisabled(false);
                        #{StopServerTime}.setDisabled(true);
                        msg('Stop Server')">
                    <AjaxEvents>
                        <Update OnEvent="RefreshTime">
                            <EventMask 
                                ShowMask="true" 
                                Target="CustomTarget" 
                                CustomTarget="={Ext.getCmp('#{ServerTimeContainer}').getBody()}" 
                                MinDelay="350"
                                />
                        </Update>
                    </AjaxEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </form>
</body>
</html>
