<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KeyMap Toggling BorderLayout Regions - Coolite Toolkit Example</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <ext:ScriptManager runat="server" />
        
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout runat="server">
                <North Collapsible="true">
                    <ext:Panel ID="North" runat="server" Title="North" Frame="true" Height="200" />
                </North>
                
                <West Collapsible="true">
                    <ext:Panel ID="West" runat="server" Title="West" Frame="true" Width="200" />
                </West>
                
                <Center>
                    <ext:Panel ID="Center" runat="server" Border="true" BodyStyle="padding:6px">
                        <Body>
                            <ul>
                                <li>If keys are not working then click on center area</li>
                                <li>NORTH toggle: N</li>
                                <li>WEST toggle: W</li>
                                <li>EAST toggle: E</li>
                                <li>SOUTH toggle: S</li>
                            </ul>
                        </Body>
                    </ext:Panel>
                </Center>
                
                <East Collapsible="true">
                    <ext:Panel ID="East" runat="server" Title="East" Frame="true" Width="200" />
                </East>
                
                <South Collapsible="true">
                    <ext:Panel ID="South" runat="server" Title="South" Frame="true" Height="200" />
                </South>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
    
    <ext:KeyMap runat="server" Target="={Ext.isGecko ? Ext.getDoc() : Ext.getBody()}">
        <ext:KeyBinding>
            <Keys>
                <ext:Key Code="N" />
            </Keys>
            <Listeners>
                <Event Handler="#{North}.toggleCollapse();" />
            </Listeners>
        </ext:KeyBinding>    
        
        <ext:KeyBinding>
            <Keys>
                <ext:Key Code="W" />
            </Keys>
            <Listeners>
                <Event Handler="#{West}.toggleCollapse();" />
            </Listeners>
        </ext:KeyBinding>
        
        <ext:KeyBinding>
            <Keys>
                <ext:Key Code="E" />
            </Keys>
            <Listeners>
                <Event Handler="#{East}.toggleCollapse();" />
            </Listeners>
        </ext:KeyBinding>
        
        <ext:KeyBinding>
            <Keys>
                <ext:Key Code="S" />
            </Keys>
            <Listeners>
                <Event Handler="#{South}.toggleCollapse();" />
            </Listeners>
        </ext:KeyBinding>
    </ext:KeyMap>
</body>
</html>
