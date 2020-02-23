<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <ext:Window ID="Window1" runat="server" Width="400" Height="300" Title="Move/Resize Window" BodyStyle="padding:6px;">
        <Body>
            <ul>
                <li>MOVE: Alt+LEFT/RIGHT/UP/DOWN</li>
                <li>RESIZE: Ctrl+LEFT/RIGHT/UP/DOWN</li>
            </ul>
        </Body>
        
        <KeyMap>
            <%--SIZE CHANGING--%>
            <ext:KeyBinding Ctrl="true">
                <Keys>
                    <ext:Key Code="RIGHT" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setWidth(#{Window1}.getSize().width+10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Ctrl="true">
                <Keys>
                    <ext:Key Code="LEFT" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setWidth(#{Window1}.getSize().width-10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Ctrl="true">
                <Keys>
                    <ext:Key Code="UP" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setHeight(#{Window1}.getSize().height-10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Ctrl="true">
                <Keys>
                    <ext:Key Code="DOWN" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setHeight(#{Window1}.getSize().height+10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <%--POSITION CHANGING--%>
            <ext:KeyBinding Alt="true">
                <Keys>
                    <ext:Key Code="RIGHT" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setPosition(#{Window1}.getPosition(false)[0]+10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Alt="true">
                <Keys>
                    <ext:Key Code="LEFT" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setPosition(#{Window1}.getPosition(false)[0]-10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Alt="true">
                <Keys>
                    <ext:Key Code="UP" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setPosition(undefined, #{Window1}.getPosition(false)[1]-10);" />
                </Listeners>
            </ext:KeyBinding>
            
            <ext:KeyBinding Alt="true">
                <Keys>
                    <ext:Key Code="DOWN" />
                </Keys>
                <Listeners>
                    <Event Handler="#{Window1}.setPosition(undefined, #{Window1}.getPosition()[1]+10);" />
                </Listeners>
            </ext:KeyBinding>
        </KeyMap>
    </ext:Window>
</body>
</html>
