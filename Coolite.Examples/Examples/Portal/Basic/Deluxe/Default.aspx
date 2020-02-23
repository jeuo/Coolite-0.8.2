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
                if (!portlet.ID.Equals("Portlet1"))
                {
                    portlet.Html = "={text}";
                }
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deluxe Portal in TabPanel - Coolite Toolkit Examples</title>
    <ext:ScriptContainer runat="server" />    
</head>
<body>
    <style type="text/css">
        body {
            font-size: 12px;
            background-color: #284051;
            font-family: "Trebuchet MS",sans-serif;
        }
        
        #settingsWrapper {
        	border-bottom: 1px solid #111;
        	background-color: #323232;
        	padding-left: 4px;
        }
        
        #settings {
        	padding: 2px;
            border-bottom: 1px solid #3B3B3B;
        }
        
        #settings ul li {
        	display: inline;
        	color: #fff;
        	margin-right: 8px;
            line-height: 24px;
            padding: 2px;
            padding-left: 19px;
        }
        
        #settings ul li a, #settings ul li a:link {
        	color: #fff;
            text-decoration: none;
        }
        
        #settings ul li a:hover {
            text-decoration: underline;
        }
        
        #pageTitle {
            font-family: "Trebuchet MS",sans-serif;
            font-size: 17px;
        	text-align: center;
        	color: #fff;
        	margin-top: 20px;
        }
        
        #itemAddContent {
        	background: url(<%= this.ScriptManager1.GetIconUrl(Icon.Add) %>) no-repeat 0 2px;
        }
        
        #itemComments {
        	background: url(<%= this.ScriptManager1.GetIconUrl(Icon.Comment) %>) no-repeat 0 2px;
        }
        
        #itemActivities {
        	background: url(<%= this.ScriptManager1.GetIconUrl(Icon.Star) %>) no-repeat 0 2px;
        }
        
        #itemContacts {
        	background: url(<%= this.ScriptManager1.GetIconUrl(Icon.Vcard) %>) no-repeat 0 2px;
        }
        
        #TabPanel1 ul.x-tab-strip-top {
            background-image: none !important;
            background-color: #284051;
        }
      
        .x-panel-dd-spacer {
	        border: 2px dashed #284051;
        }
    </style>
    
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" Theme="Slate" />
        <ext:ViewPort ID="ViewPort1" runat="server" StyleSpec="background-color: transparent;">
            <Body>
                <ext:BorderLayout runat="server">
                    <North>
                        <ext:Panel 
                            ID="pnlNorth" 
                            runat="server" 
                            Height="80" 
                            Border="false" 
                            Header="false" 
                            BodyStyle="background-color: transparent;">
                            <Body>
                            <div id="settingsWrapper">
                                <div id="settings">
                                    <ul>
                                        <li id="itemAddContent"><a href="#">Add content</a></li>
                                        <li id="itemComments"><a href="#">Comments</a></li>
                                        <li id="itemActivities"><a href="#">Activities</a></li>
                                        <li id="itemContacts"><a href="#">Contacts</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div id="pageTitle">Your page title here</div>
                            </Body>
                        </ext:Panel>
                    </North>
                    <West 
                        Collapsible="true"
                        Split="true" 
                        MinWidth="175" 
                        MaxWidth="400" 
                        MarginsSummary="31 0 5 5" 
                        CMarginsSummary="31 5 5 5">
                        <ext:Panel 
                            runat="server" 
                            Title="Settings" 
                            Width="200" 
                            ID="pnlSettings"
                            BodyStyle="background-color: #284051;">
                            <Body>
                                <ext:Accordion runat="server" Animate="true">
                                    <ext:Panel 
                                        runat="server" 
                                        Border="false" 
                                        Collapsed="True" 
                                        Icon="PageWhiteCopy"
                                        AutoScroll="true"
                                        Title="Content"
                                        Html="={text}"
                                        BodyStyle="padding:6px;"
                                        />
                                    <ext:Panel 
                                        runat="server" 
                                        Border="false" 
                                        Collapsed="true" 
                                        Icon="Star" 
                                        AutoScroll="true"
                                        Title="Activities" 
                                        Html="={text}"
                                        BodyStyle="padding:6px;"
                                        />
                                    <ext:Panel ID="Panel1" 
                                        runat="server" 
                                        Border="false" 
                                        Collapsed="true" 
                                        Icon="Group" 
                                        AutoScroll="true"
                                        Title="Contacts" 
                                        Html="={text}"
                                        BodyStyle="padding:6px;"
                                        />                                        
                                </ext:Accordion>
                            </Body>
                        </ext:Panel>
                    </West>
                    <Center MarginsSummary="5 5 5 0">
                        <ext:TabPanel 
                            ID="TabPanel1" 
                            runat="server" 
                            Title="TabPanel" 
                            ActiveTabIndex="0" 
                            Border="false" 
                            BodyStyle="background-color: #4D778B; border: 1px solid #AABBCC; border-top: none;">
                            <Tabs>
                                <ext:Tab ID="Tab1" runat="server" Title="Internet" Icon="Vcard" BodyStyle="background-color: transparent;">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout1" runat="server">
                                            <ext:Portal ID="Portal1" runat="server" Border="false" BodyStyle="background-color: transparent;">
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
                                                                            <ext:Portlet 
                                                                                ID="Portlet1" 
                                                                                Title="Google Search" 
                                                                                runat="server" 
                                                                                Height="450"
                                                                                BodyStyle="padding:0;"
                                                                                >
                                                                                <AutoLoad Url="http://www.google.com/" Mode="IFrame" />
                                                                            </ext:Portlet>
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
                                                                            <ext:Portlet ID="Portlet2" Title="Panel 2" runat="server" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet3" Title="Another Panel 2" runat="server" />
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
                                                                            <ext:Portlet ID="Portlet4" Title="Panel 3" runat="server" />
                                                                        </ext:Anchor>
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet5" Title="Another Panel 3" runat="server" />
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
                                <ext:Tab ID="Tab2" runat="server" Title="General" Icon="House" BodyStyle="background-color: transparent;">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout2" runat="server">
                                            <ext:Portal ID="Portal2" runat="server" Border="false" BodyStyle="background-color: transparent;">
                                                <Body>
                                                    <ext:ColumnLayout ID="ColumnLayout2" runat="server">
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn6" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px"
                                                                />
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn5" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout5" runat="server">
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
                                <ext:Tab ID="Tab3" runat="server" Title="Fun" Icon="ColorSwatch" BodyStyle="background-color: transparent;">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout3" runat="server">
                                            <ext:Portal ID="Portal3" runat="server" Border="false" BodyStyle="background-color: transparent;">
                                                <Body>
                                                    <ext:ColumnLayout ID="ColumnLayout3" runat="server">
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn7" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px">
                                                                <Body>
                                                                    <ext:AnchorLayout ID="AnchorLayout7" runat="server">
                                                                        <ext:Anchor>
                                                                            <ext:Portlet ID="Portlet11" Title="My &quot;Fun&quot; Portlet" runat="server" />
                                                                        </ext:Anchor>
                                                                    </ext:AnchorLayout>
                                                                </Body>
                                                            </ext:PortalColumn>
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn8" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px" 
                                                                />
                                                        </ext:LayoutColumn>
                                                        <ext:LayoutColumn ColumnWidth=".33">
                                                            <ext:PortalColumn 
                                                                ID="PortalColumn9" 
                                                                runat="server" 
                                                                StyleSpec="padding:10px 0 10px 10px" 
                                                                />
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
