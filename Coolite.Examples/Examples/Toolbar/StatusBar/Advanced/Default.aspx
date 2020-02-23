<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>StatusBar Advanced - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />   
    
    <script runat="server">
        protected void FormSave(object sender, AjaxEventArgs e)
        {
            FormStatusBar.SetStatus(new StatusBarStatusConfig { Text = "Form saved!", IconCls = " ", Clear2 = true });                  
        }
    </script>
    <style type="text/css">
        .list {
            list-style-image:none;
            list-style-position:outside;
            list-style-type:square;
            padding-left:16px;
        }
        .list li {
            font-size:11px;
            padding:3px;
        }
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />

        <h1>Advanced StatusBar Example</h1>
        <p>This is an advanced example of customizing an Ext.StatusBar via a plugin.</p>
        
        <h2>Customizing the StatusBar</h2>
        <p>The ValidationStatus plugin hooks into the StatusBar and automatically 
        monitors the validation status of any fields in the associated FormPanel.  Items of interest:</p>
        <ul class="list">
            <li>The StatusBar syncs in real-time with the valid state of the form as you type</li>
            <li>When the form is invalid, the error status message can be clicked to hide/show a custom error list</li>
            <li>The error list items can be clicked to focus the associated fields</li>            
        </ul>
        
        <br/>
        
        <ext:Panel runat="server" 
            Title="StatusBar with Integrated Form Validation"
            Width="350"
            Height="150">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:FormPanel ID="StatusForm" runat="server"
                        LabelWidth="75"
                        Width="350"
                        ButtonAlign="Right"
                        Border="false"
                        BodyStyle="padding:10px 10px 0;"                         
                    >
                        <Defaults>
                            <ext:Parameter Name="Anchor" Value="95%" />
                            <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                            <ext:Parameter Name="SelectOnFocus" Value="true" Mode="Raw" />
                            <ext:Parameter Name="MsgTarget" Value="side" />
                        </Defaults>
                        
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField runat="server" FieldLabel="Name" BlankText="Name is required" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:DateField runat="server" FieldLabel="Birthdate" BlankText="Birthdate is required" />
                                </ext:Anchor>
                            </ext:FormLayout>
                        </Body>
                        
                        <Buttons>
                            <ext:Button runat="server" Text="Save" Icon="Disk">
                                <AjaxEvents>
                                    <Click OnEvent="FormSave" Before="var valid= #{StatusForm}.getForm().isValid(); if(valid){#{FormStatusBar}.showBusy('Saving form...');} return valid;">
                                        <EventMask ShowMask="true" MinDelay="1000" Target="CustomTarget" CustomTarget="={#{StatusForm}.getEl()}" />
                                    </Click>
                                </AjaxEvents>
                            </ext:Button>
                        </Buttons>
                        
                    </ext:FormPanel>
                </ext:FitLayout>
            </Body>
            
            <BottomBar>
                <ext:StatusBar ID="FormStatusBar" runat="server" DefaultText="Ready">
                    <Plugins>
                        <ext:ValidationStatus runat="server" FormPanelID="StatusForm" ValidIcon="Accept" ErrorIcon="Exclamation" />
                    </Plugins>
                </ext:StatusBar>
            </BottomBar>
        </ext:Panel>
    </form>
</body>
</html>