<%@ Page Language="C#" %>
<%@ Import Namespace="Coolite.Utilities" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.ScriptManager1.RegisterIcon(Icon.Accept);
        }
    }

    protected void UpdateStatusBar(object sender, AjaxEventArgs e)
    {
        // Delay the Thread for .5 seconds
        System.Threading.Thread.Sleep(500);
        
        string index = e.ExtraParams["index"];
        
        Coolite.Ext.Web.Button button = ControlUtils.FindControl(this.ScriptManager1, "Button" + index) as Coolite.Ext.Web.Button;
        
        StatusBar statusBar = ControlUtils.FindControl(this.ScriptManager1, "StatusBar" + index) as StatusBar;

        statusBar.SetStatus(new StatusBarStatusConfig("Updated: " + DateTime.Now.ToLongTimeString() + " (server time)", " "));
        button.Enabled = true;
    }
    
    protected void TextArea1_Save(object sender, AjaxEventArgs e)
    {
        // Delay the Thread for .5 seconds to demonstrate the status indicator
        System.Threading.Thread.Sleep(500);
        
        StatusBarStatusConfig config = new StatusBarStatusConfig();
        config.IconCls = "x-status-saved";
        config.Text = string.Concat("Draft auto-saved at ", DateTime.Now.ToLongTimeString(), " (server time)");

        this.StatusBar4.SetStatus(config);
    }
</script>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>StatusBar - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        #StatusBar4 .x-status-text {
            color: #777777;
        }
        
        #StatusBar4 .x-status-text-panel .spacer {
            font-size: 0;
            line-height: 0;
            width: 60px;
        }
        
        #StatusBar4 .x-status-busy {
            background: transparent url(/icons/disk-png/coolite.axd) no-repeat scroll 3px 3px;
            padding-left: 25px;
        }

        #StatusBar4 .x-status-saved {
            background: transparent url(/icons/accept-png/coolite.axd) no-repeat scroll 3px 3px;
            padding-left: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />

        <h1>StatusBar Examples</h1>
        
        <p>Here are several examples of using and customizing the Ext.StatusBar component.</p>
        
        <h2>1. Basic StatusBar</h2>
        
        <p>This is a simple StatusBar with a few standard Toolbar items included.</p>
        
        <ext:Panel 
            ID="Panel1" 
            runat="server" 
            Title="Basic StatusBar" 
            Width="350" 
            Height="100" 
            BodyStyle="padding:10px;">
            <Body>
                <ext:ContainerLayout runat="server">
                    <ext:Button 
                        ID="Button1" 
                        runat="server" 
                        Text="Do Loading">
                        <AjaxEvents>
                            <Click OnEvent="UpdateStatusBar" Before="el.disable();#{StatusBar1}.showBusy();">
                                <EventMask MinDelay="2000" />
                                <ExtraParams>
                                    <ext:Parameter Name="index" Value="1" />
                                </ExtraParams>
                            </Click>
                        </AjaxEvents>   
                    </ext:Button>
                </ext:ContainerLayout>
            </Body>
            <BottomBar>
                <ext:StatusBar 
                    ID="StatusBar1" 
                    runat="server"
                    DefaultText="Default status">
                    <Items>
                        <ext:ToolbarButton runat="server" Text="A Button" />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarTextItem runat="server" Text="Plain Text" />
                    </Items>
                </ext:StatusBar>
            </BottomBar>
        </ext:Panel>
        
        <h2>2. Right-Aligned StatusBar</h2>
        
        <p>This is a simple StatusBar that has the status element right-aligned. Any StatusBar items will be added in exactly the same order on the left side of the bar.</p>
        
         <ext:Panel 
            ID="Panel2" 
            runat="server" 
            Title="Right-aligned StatusBar" 
            Width="350" 
            Height="100" 
            BodyStyle="padding:10px;">
            <Body>
                <ext:ContainerLayout runat="server">
                    <ext:Button 
                        ID="Button2" 
                        runat="server" 
                        Text="Do Loading">
                        <AjaxEvents>
                            <Click OnEvent="UpdateStatusBar" Before="el.disable();#{StatusBar2}.showBusy();">
                                <EventMask MinDelay="2000" />
                                <ExtraParams>
                                    <ext:Parameter Name="index" Value="2" />
                                </ExtraParams>
                            </Click>
                        </AjaxEvents>    
                    </ext:Button>
                </ext:ContainerLayout>
            </Body>
            <BottomBar>
                <ext:StatusBar 
                    ID="StatusBar2" 
                    runat="server"
                    DefaultText="Default status"
                    StatusAlign="Right">
                    <Items>
                        <ext:ToolbarButton runat="server" Text="A Button" />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarTextItem runat="server" Text="Plain Text" />
                    </Items>
                </ext:StatusBar>
            </BottomBar>
        </ext:Panel>
        
        <h2>3. Status Window</h2>
        
        <p>You can add a StatusBar to a window in exactly the same way.</p>
        
        <ext:Button ID="Button4" runat="server" Text="Show Window">
            <Listeners>
                <Click Handler="#{Window1}.show();" />
            </Listeners>
        </ext:Button>
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Collapsible="false" 
            Icon="Application" 
            Title="StatusBar Window"
            Width="400"
            MinWidth="350"
            Modal="true"
            Height="150"
            BodyStyle="padding:10px;"
            ShowOnLoad="false">
            <Body>
                <ext:ContainerLayout runat="server">
                    <ext:Button 
                        ID="Button3" 
                        runat="server" 
                        Text="Do Loading">
                        <AjaxEvents>
                            <Click OnEvent="UpdateStatusBar" Before="el.disable();#{StatusBar3}.showBusy();">
                                <EventMask MinDelay="2000" />
                                <ExtraParams>
                                    <ext:Parameter Name="index" Value="3" />
                                </ExtraParams>
                            </Click>
                        </AjaxEvents>   
                    </ext:Button>
                </ext:ContainerLayout>
            </Body>
             <BottomBar>
                <ext:StatusBar 
                    ID="StatusBar3" 
                    runat="server"
                    DefaultText="Ready">
                    <Items>
                        <ext:ToolbarButton runat="server" Text="A Button" />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarTextItem runat="server" AutoDataBind="true" Text='<%# DateTime.Today.ToShortDateString() %>' />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarSplitButton runat="server" Text="Status Menu" MenuAlign="br-tr?">
                            <Menu>
                                <ext:Menu ID="Menu1" runat="server">
                                    <Items>
                                        <ext:MenuItem runat="server" Text="Item1" />
                                        <ext:MenuItem runat="server" Text="Item2" />
                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:ToolbarSplitButton>
                    </Items>
                </ext:StatusBar>
            </BottomBar>
        </ext:Window>
        
        <h2>4. Customizing the Look and Feel</h2>
        
        <p>This is a standard StatusBar with some custom CSS styles applied and some event handling in place to handle the TextArea's keypress events. Notice that after you've stopped typing for a few seconds a simulated auto-save process will begin.</p>
        
        <ext:Panel 
            runat="server" 
            Width="500"
            AutoHeight="true"
            Title="Word Processor"
            BodyStyle="padding:5px;">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:TextArea 
                        ID="TextArea1" 
                        runat="server"
                        EnableKeyEvents="true"
                        Grow="true"
                        GrowMin="100"
                        GrowMax="200">
                        <Listeners>
                            <KeyPress 
                                Handler="var v = el.getValue(),
                                    wc = 0, cc = v.length ? v.length : 0;
                                    
                                    if (cc &gt; 0) {
                                        wc = v.match(/\b/g);
                                        wc = wc ? wc.length / 2 : 0;
                                    }
                                    
                                    #{wordCount}.setText('Words: ' + wc);
                                    #{charCount}.setText('Chars: ' + cc);"
                                 Buffer="1" 
                                 />
                        </Listeners>
                        <AjaxEvents>
                            <KeyPress 
                                OnEvent="TextArea1_Save" 
                                Buffer="1500" 
                                Before="#{StatusBar4}.showBusy('Saving draft...');" 
                                />
                        </AjaxEvents>
                    </ext:TextArea>
                </ext:FitLayout>
            </Body>
            <BottomBar>
                <ext:StatusBar ID="StatusBar4" runat="server" DefaultText="Ready">
                    <Items>
                        <ext:ToolbarFill runat="server" />
                        <ext:ToolbarTextItem ID="wordCount" runat="server" Text="Words: 0" />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarTextItem ID="charCount" runat="server" Text="Chars: 0" />
                        <ext:ToolbarSeparator runat="server" />
                        <ext:ToolbarTextItem ID="clock" runat="server" Text=" " />
                    </Items>
                </ext:StatusBar>
            </BottomBar>
        </ext:Panel>
        
        <ext:TaskManager runat="server">
            <Tasks>
                <ext:Task AutoRun="true" Interval="100">
                    <Listeners>
                        <Update Handler="#{clock}.setText(new Date().format('g:i:s A'));" />
                    </Listeners>
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </form>
</body>
</html>