﻿<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Deferred Loading - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                this.Label1.Text = DateTime.Now.ToLongTimeString();
            }
        }
    </script>
    
    <style type="text/css">
         .msg {
        	border: 1px solid #999;
        	padding: 6px;
        	width: 250px;
        	font-weight: bold;
            text-align: center;
            margin-bottom: 30px;
        }
        
        .msg em {
        	font-style: italic;
        	font-weight: bold;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
         <ext:ScriptManager ID="ScriptManager1" runat="server" />        
        
         <div class="msg x-box-mc">
              Main page creation time: <em><ext:Label ID="Label1" runat="server" /></em>
         </div>
         <hr />
         
         <br />
         <br />
         
         <h3>1. TabPanel</h3>
         <ext:TabPanel runat="server" Width="500" Height="200" EnableTabScroll="true" DeferredRender="false">
            <Tabs>
                <ext:Tab runat="server" Title="Loading on render">
                    <AutoLoad Url="Child.aspx" Mode="IFrame" ShowMask="true" />
                </ext:Tab>
                
                <ext:Tab runat="server" Title="Loading on show">
                    <AutoLoad Url="Child.aspx" Mode="IFrame" TriggerEvent="show" ShowMask="true" />
                </ext:Tab>
                
                <ext:Tab ID="ManuallyLoadTab" runat="server" Title="Manual Load">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:ToolbarButton runat="server" Text="Load" Icon="Connect">
                                    <Listeners>
                                        <Click Handler="#{ManuallyLoadTab}.reload();" />
                                    </Listeners>
                                </ext:ToolbarButton>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <AutoLoad Url="Child.aspx" Mode="IFrame" ManuallyTriggered="true" ShowMask="true" />
                </ext:Tab>
                
                <ext:Tab runat="server" Title="Loading on Show with Reloading">
                    <AutoLoad 
                        Url="Child.aspx" 
                        Mode="IFrame" 
                        TriggerEvent="show" 
                        ReloadOnEvent="true" 
                        ShowMask="true"
                        NoCache="true"
                        />
                </ext:Tab>
                
                <ext:Tab runat="server" Title="Deferred Loading of Internal Panels">
                    <Body>
                        <ext:Panel ID="InnerPanel1" runat="server" Title="Panel" Height="150">
                             <AutoLoad Url="Child.aspx" Mode="IFrame" ManuallyTriggered="true" ShowMask="true" />
                        </ext:Panel>
                    </Body>
                    <Listeners>
                        <Activate Handler="#{InnerPanel1}.reload();" Single="true" />
                    </Listeners>
                </ext:Tab>
            </Tabs>
         </ext:TabPanel>
         
         <br />
         <br />
         
         <h3>2. Accordion</h3>
         <ext:Panel runat="server" Title="Accordion with Deferred Loaded Panels" Width="500" Height="300">
            <Body>
                <ext:Accordion runat="server">
                    <ext:Panel runat="server" Title="Panel 1">
                         <AutoLoad Url="Child.aspx" Mode="IFrame" ShowMask="true" />
                    </ext:Panel>
                    
                    <ext:Panel runat="server" Title="Panel 2">
                         <AutoLoad Url="Child.aspx" Mode="IFrame" TriggerEvent="expand" ShowMask="true" />
                    </ext:Panel>
                    
                     <ext:Panel runat="server" Title="Panel 3">
                         <AutoLoad Url="Child.aspx" Mode="IFrame" TriggerEvent="expand" ShowMask="true" />
                    </ext:Panel>
                </ext:Accordion>
            </Body>
         </ext:Panel>
         
         <br />
         <br />
         
         <h3>3. Panel - loading on render and on each expand event, clear content on collapse</h3>
         <ext:Panel runat="server" Title="Panel" Width="500" Height="300" Collapsible="true">
             <AutoLoad Url="Child.aspx" Mode="IFrame" NoCache="true" ShowMask="true" />
            <Listeners>
                <Expand Handler="this.reload();" />
                <Collapse Handler="this.clearContent();" />
            </Listeners>
        </ext:Panel>
        
        <br />
        <br />
         
        <h3>4. Window - loading on show with reloading, clear content on close</h3>
        <ext:Button runat="server" Text="Show window">
            <Listeners>
                <Click Handler="#{Window1}.show();" />
            </Listeners>
        </ext:Button>
        
        <ext:Window ID="Window1" runat="server" Title="Window" Frame="true" Width="300" Height="185" ShowOnLoad="false">
             <AutoLoad 
                Url="Child.aspx" 
                Mode="IFrame" 
                NoCache="true"
                TriggerEvent="show"
                ReloadOnEvent="true"
                ShowMask="true"
                />
            <Listeners>
                <Hide Handler="this.clearContent();" />
            </Listeners>
        </ext:Window>
     </form>     
</body>
</html>
