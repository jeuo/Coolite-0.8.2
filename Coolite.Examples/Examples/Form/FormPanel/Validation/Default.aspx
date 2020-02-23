<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FormPanel Validation - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .icon-exclamation {
            padding-left: 25px !important;
            background: url(/icons/exclamation-png/coolite.axd) no-repeat 3px 3px !important;
        }
        .icon-accept {
            padding-left: 25px !important;
            background: url(/icons/accept-png/coolite.axd) no-repeat 3px 3px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:FormPanel 
            ID="FormPanel1" 
            runat="server" 
            Title="FormPanel Validation (all fields required)"
            MonitorPoll="500" 
            MonitorValid="true" 
            Width="600" 
            BodyStyle="padding:5px;" 
            ButtonAlign="Right">
            <Body>
                <ext:ColumnLayout runat="server">
                    <ext:LayoutColumn ColumnWidth=".5">
                        <ext:Panel runat="server" Border="false" Header="false">
                            <Defaults>
                                <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                                <ext:Parameter Name="MsgTarget" Value="side" />
                            </Defaults>
                            <Body>
                                <ext:FormLayout runat="server" LabelAlign="Top">
                                    <ext:Anchor Horizontal="92%">
                                        <ext:TextField ID="TextField1" runat="server" FieldLabel="First Name" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="92%">
                                        <ext:TextField runat="server" FieldLabel="Company" />
                                    </ext:Anchor>
                                </ext:FormLayout>
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                    <ext:LayoutColumn ColumnWidth=".5">
                        <ext:Panel runat="server" Border="false">
                            <Defaults>
                                <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                                <ext:Parameter Name="MsgTarget" Value="side" />
                            </Defaults>
                            <Body>
                                <ext:FormLayout ID="FormLayout2" runat="server" LabelAlign="Top">
                                    <ext:Anchor Horizontal="92%">
                                        <ext:TextField runat="server" FieldLabel="Last Name" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="92%">
                                        <ext:TextField runat="server" FieldLabel="Email" Vtype="email" />
                                    </ext:Anchor>
                                </ext:FormLayout>
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                </ext:ColumnLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save">
                    <Listeners>
                        <Click Handler="if(#{FormPanel1}.getForm().isValid()){Ext.Msg.alert('Submit', 'Saved!');}else{Ext.Msg.show({icon: Ext.MessageBox.ERROR, msg: 'FormPanel is incorrect', buttons:Ext.Msg.OK});}" />
                    </Listeners>
                </ext:Button>
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
            <BottomBar>
                <ext:StatusBar ID="FormStatus" runat="server" />
            </BottomBar>
            <Listeners>
                <ClientValidation Handler="#{FormStatus}.setStatus({text: valid ? 'Form is valid' : 'Form is invalid', iconCls: valid ? 'icon-accept' : 'icon-exclamation'});" />
            </Listeners>
        </ext:FormPanel>
    </form>
</body>
</html>
