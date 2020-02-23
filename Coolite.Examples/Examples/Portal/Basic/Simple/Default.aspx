<%@ Page Language="C#" %>
<%@ Import Namespace="Coolite.Utilities" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            string text = @"Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Sed metus nibh, sodales a, porta at, vulputate eget, dui. Pellentesque ut nisl. Maecenas tortor turpis, interdum non, sodales non, iaculis ac, lacus. Vestibulum auctor, tortor quis iaculis malesuada, libero lectus bibendum purus, sit amet tincidunt quam turpis vel lacus. In pellentesque nisl non sem. Suspendisse nunc sem, pretium eget, cursus a, fringilla vel, urna.";

            this.ScriptManager1.RegisterClientScriptBlock("text", string.Format("var text=\"{0}\";", text));

            foreach (Portlet portlet in ControlUtils.FindControls<Portlet>(this.Page))
            {
                portlet.Html = "={text}";
                portlet.Tools.Add(new Tool(ToolType.Close, string.Concat(portlet.ClientID, ".hide();"), "Close Portlet"));
            }
        }

        foreach (Portlet portlet in ControlUtils.FindControls<Portlet>(this.Page))
        {
            portlet.AjaxEvents.Hide.Event += Portlet_Hide;
            portlet.AjaxEvents.Hide.EventMask.ShowMask = true;
            portlet.AjaxEvents.Hide.EventMask.Msg = "Saving...";
            portlet.AjaxEvents.Hide.EventMask.MinDelay = 500;
            
            portlet.AjaxEvents.Hide.ExtraParams.Add(new Coolite.Ext.Web.Parameter("ID", portlet.ClientID));
        }
    }

    protected void Portlet_Hide(object sender, AjaxEventArgs e)
    {
        string id = e.ExtraParams["ID"];
        this.ScriptManager1.AddScript("Ext.Msg.alert('Status','" + id + " Hidden');");
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Portal in TabPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        <ext:ViewPort ID="ViewPort1" runat="server">
            <Body>
                <ext:BorderLayout ID="BorderLayout1" runat="server">
                    <West 
                        Collapsible="true" 
                        Split="true" 
                        MinWidth="175" 
                        MaxWidth="400" 
                        MarginsSummary="5 0 5 5" 
                        CMarginsSummary="5 5 5 5">
                        <ext:Panel 
                            runat="server" 
                            Title="West" 
                            Width="200" 
                            ID="pnlWest">
                            <Body>
                                <ext:Accordion runat="server" Animate="true">
                                    <ext:Panel 
                                        ID="pnlContent" 
                                        runat="server" 
                                        Border="false" 
                                        Collapsed="true" 
                                        Icon="Note"
                                        AutoScroll="true"
                                        Title="Content"
                                        Html="={text}"
                                        BodyStyle="padding:5px;"
                                        />
                                    <ext:Panel
                                        ID="pnlSettings"
                                        runat="server" 
                                        Border="false" 
                                        Collapsed="true" 
                                        Icon="FolderWrench" 
                                        AutoScroll="true"
                                        Title="Settings"
                                        Html="={text}"
                                        BodyStyle="padding:5px;"
                                        />
                                </ext:Accordion>
                            </Body>
                        </ext:Panel>
                    </West>
                    <Center MarginsSummary="5 5 5 0">
                        <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Title="TabPanel">
                            <Tabs>
                                <ext:Tab ID="Tab1" runat="server" Title="Tab 1">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout1" runat="server">
                                            <ext:Portal ID="Portal1" runat="server" Border="false">
                                                <Listeners>
                                                    <Drop Handler="e.panel.el.frame();" />
                                                </Listeners>
                                                <Body>
                                                    <ext:ColumnLayout ID="ColumnLayout1" runat="server">
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn1" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout1" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet1" runat="server" Title="Another Panel 1" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn2" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout2" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet2" runat="server" Title="Panel 2" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet3" runat="server" Title="Another Panel 2" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn3" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout3" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet4" runat="server" Title="Panel 3" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet5" runat="server" Title="Another Panel 3" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                    </ext:ColumnLayout>    
                                                </Body>
                                            </ext:Portal>
                                        </ext:FitLayout>
                                    </Body>
                                </ext:Tab>
                                <ext:Tab ID="Tab2" runat="server" Title="Tab 2">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout2" runat="server">
                                            <ext:Portal ID="Portal2" runat="server" Border="false">
                                                <Body>
                                                    <ext:ColumnLayout ID="ColumnLayout2" runat="server">
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn6" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout6" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet9" Title="Panel 3" runat="server" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet10" Title="Another Panel 3" runat="server" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn5" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout5" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet7" Title="Panel 2" runat="server" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet8" Title="Another Panel 2" runat="server" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn4" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout4" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet6" Title="Another Panel 1" runat="server" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                    </ext:ColumnLayout>    
                                                </Body>
                                            </ext:Portal>                                    
                                        </ext:FitLayout>
                                    </Body>
                                </ext:Tab>
                            </Tabs>
                        </ext:TabPanel> 
                    </Center>
                </ext:BorderLayout>
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>