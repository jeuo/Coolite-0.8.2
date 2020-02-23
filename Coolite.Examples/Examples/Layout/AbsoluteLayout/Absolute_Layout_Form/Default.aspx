<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AbsoluteLayout Form - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        /* Custom rule to make the toolbar fit within a framed panel with no margin: */
        .email-form .x-panel-mc .x-panel-tbar .x-toolbar {
            border-top: 1px solid #C2D6EF;
            border-left: 1px solid #C2D6EF;
            border-bottom: 1px solid #99BBE8;
            margin: -5px -4px 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
    
        <ext:ViewPort runat="server">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:Panel runat="server" Header="false" Border="false" BodyStyle="padding:15px;">
                        <Body>
                            <ext:FitLayout runat="server">
                                <ext:Panel runat="server" Title="New Email" Cls="email-form" Frame="true" BodyStyle="10px 5px 5px;">
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:ToolbarButton runat="server" Text="Send" Icon="EmailGo" />
                                                <ext:ToolbarButton runat="server" Text="Save" Icon="Disk" />
                                                <ext:ToolbarButton runat="server" Text="Check Spelling" Icon="Spellcheck" />
                                                <ext:ToolbarButton runat="server" Text="Print" Icon="Printer" />
                                                <ext:ToolbarFill runat="server" />
                                                <ext:ToolbarButton runat="server" Text="Attach a File" Icon="PageAttach" />
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    
                                    <Body>
                                        <ext:FitLayout runat="server">
                                            <ext:Panel runat="server" BaseCls="x-plain" Border="true">
                                                <Body>
                                                    <ext:AbsoluteLayout runat="server">
                                                        <Anchors>
                                                            <ext:Anchor>
                                                                <ext:Label runat="server" X="0" Y="15" Text="From:" />
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:TextField runat="server" X="55" Y="10">
                                                                    <CustomConfig>
                                                                        <ext:ConfigItem Name="anchor" Value="100%" Mode="Value" />
                                                                    </CustomConfig>
                                                                </ext:TextField>    
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:Label runat="server" X="0" Y="42" Text="To:" />
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <%-- The button is not a Field subclass, so it must be 
    	                                                        wrapped in a panel for proper positioning to work--%>
                                                                <ext:Panel runat="server" X="55" Y="37">
                                                                    <Body>
                                                                        <ext:ContainerLayout runat="server">
                                                                            <ext:Button runat="server" Text="Contacts..." />
                                                                        </ext:ContainerLayout>
                                                                    </Body>
                                                                </ext:Panel>
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:TextField runat="server" X="135" Y="37">
                                                                    <CustomConfig>
                                                                        <ext:ConfigItem Name="anchor" Value="100%" Mode="Value" />
                                                                    </CustomConfig>
                                                                </ext:TextField>    
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:Label runat="server" X="0" Y="69" Text="Subject:" />
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:TextField runat="server" X="55" Y="64">
                                                                    <CustomConfig>
                                                                        <ext:ConfigItem Name="anchor" Value="100%" Mode="Value" />
                                                                    </CustomConfig>
                                                                </ext:TextField>    
                                                            </ext:Anchor>
                                                        
                                                            <ext:Anchor>
                                                                <ext:TextArea runat="server" X="0" Y="91">
                                                                    <CustomConfig>
                                                                        <ext:ConfigItem Name="anchor" Value="100% 100%" Mode="Value" />
                                                                    </CustomConfig>
                                                                </ext:TextArea>
                                                            </ext:Anchor>
                                                        </Anchors>
                                                    </ext:AbsoluteLayout>
                                                </Body>
                                            </ext:Panel>
                                        </ext:FitLayout>
                                    </Body>
                                </ext:Panel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </ext:FitLayout>
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>
