﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Desktop - Coolite Toolkit Examples</title>    
    
    <script runat="server">
        private object[] TestData
        {
            get
            {
                DateTime now = DateTime.Now;
                return new object[]
                {
                    new object[] {"3m Co", 71.72, 0.02, 0.03, now},
                    new object[] {"Alcoa Inc", 29.01, 0.42, 1.47, now},
                    new object[] {"Altria Group Inc", 83.81, 0.28, 0.34, now},
                    new object[] {"American Express Company", 52.55, 0.01, 0.02, now},
                    new object[] {"American International Group, Inc.", 64.13, 0.31, 0.49, now},
                    new object[] {"AT&T Inc.", 31.61, -0.48, -1.54, now},
                    new object[] {"Boeing Co.", 75.43, 0.53, 0.71, now},
                    new object[] {"Caterpillar Inc.", 67.27, 0.92, 1.39, now},
                    new object[] {"Citigroup, Inc.", 49.37, 0.02, 0.04, now},
                    new object[] {"E.I. du Pont de Nemours and Company", 40.48, 0.51, 1.28, now},
                    new object[] {"Exxon Mobil Corp", 68.1, -0.43, -0.64, now},
                    new object[] {"General Electric Company", 34.14, -0.08, -0.23, now},
                    new object[] {"General Motors Corporation", 30.27, 1.09, 3.74, now},
                    new object[] {"Hewlett-Packard Co.", 36.53, -0.03, -0.08, now},
                    new object[] {"Honeywell Intl Inc", 38.77, 0.05, 0.13, now},
                    new object[] {"Intel Corporation", 19.88, 0.31, 1.58, now},
                    new object[] {"International Business Machines", 81.41, 0.44, 0.54, now},
                    new object[] {"Johnson & Johnson", 64.72, 0.06, 0.09, now},
                    new object[] {"JP Morgan & Chase & Co", 45.73, 0.07, 0.15, now},
                    new object[] {"McDonald\"s Corporation", 36.76, 0.86, 2.40, now},
                    new object[] {"Merck & Co., Inc.", 40.96, 0.41, 1.01, now},
                    new object[] {"Microsoft Corporation", 25.84, 0.14, 0.54, now},
                    new object[] {"Pfizer Inc", 27.96, 0.4, 1.45, now},
                    new object[] {"The Coca-Cola Company", 45.07, 0.26, 0.58, now},
                    new object[] {"The Home Depot, Inc.", 34.64, 0.35, 1.02, now},
                    new object[] {"The Procter & Gamble Company", 61.91, 0.01, 0.02, now},
                    new object[] {"United Technologies Corporation", 63.26, 0.55, 0.88, now},
                    new object[] {"Verizon Communications", 35.57, 0.39, 1.11, now},
                    new object[] {"Wal-Mart Stores, Inc.", 45.45, 0.73, 1.63, now}
                };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                this.Store1.DataSource = this.TestData;
                this.Store1.DataBind();
            }
        }

        protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
        {
            this.Store1.DataSource = this.TestData;
            this.Store1.DataBind();
        }

        protected void Logout_Click(object sender, AjaxEventArgs e)
        {
            // Logout from Authenticated Session
            Response.Redirect("Default.aspx");
        }

        [AjaxMethod]
        public Customer AddCustomer()
        {
            Customer customer = new Customer();

            customer.ID = 99;
            customer.FirstName = this.txtFirstName.Text;
            customer.LastName = this.txtLastName.Text;
            customer.Company = this.txtCompany.Text;
            customer.Country = new Country(this.cmbCountry.SelectedItem.Value);
            customer.Premium = this.chkPremium.Checked;
            customer.DateCreated = DateTime.Now;

            return customer;
        }
        
        // Define Customer Class
        public class Customer
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }
            public Country Country { get; set; }
            public bool Premium { get; set; }
            public DateTime DateCreated { get; set; }
        }

        // Define Country Class
        public class Country
        {
            public Country(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }
        }
        
        protected void GetQuickSearchItems(object sender, StoreRefreshDataEventArgs e)
        {
            string filter = e.Parameters["Filter"];
            
            if(!string.IsNullOrEmpty(filter))
            {
                QuickSearchStore.DataSource = new List<object>
                                              {
                                                  new { SearchItem = filter + " 1" },
                                                  new { SearchItem = filter + " 2" },
                                                  new { SearchItem = filter + " 3" },
                                                  new { SearchItem = filter + " 4" },
                                                  new { SearchItem = filter + " 5" },
                                                  new { SearchItem = filter + " 6" },
                                                  new { SearchItem = filter + " 7" },
                                                  new { SearchItem = filter + " 8" },
                                                  new { SearchItem = filter + " 9" },
                                                  new { SearchItem = filter + " 10" }
                                              };
            }
            
            QuickSearchStore.DataBind();
        }
    </script>
    
    <style type="text/css">        
        .start-button {
            background-image:url(vista_start_button.gif) !important;
        }
        
        .shortcut-icon {
            width:48px;
            height:48px;
            filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="window.png", sizingMethod="scale");
        }
        
        .icon-grid48 {
            background-image: url(grid48x48.png) !important;
            filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="grid48x48.png", sizingMethod="scale");
        }
        
        .icon-user48 {
            background-image: url(user48x48.png) !important;
            filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="user48x48.png", sizingMethod="scale");
        }
        
        .icon-window48 {
            background-image: url(window48x48.png) !important;
            filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src="window48x48.png", sizingMethod="scale");
        }
        
        .desktopEl {
            position:absolute !important;
        }
    </style>
    
    <script type="text/javascript">
        var alignPanels = function () {
            pnlSample.getEl().alignTo(Ext.getBody(), "tr", [-505, 5], false)
        }

        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value);
        }

        var pctChange = function (value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value + '%');
        }

        function createDynamicWindow (app) {
            var desk = app.getDesktop();

            var w = desk.createWindow({
                title: "Web Browser",
                width: 1000,
                height: 600,
                maximizable: true,
                minimizable: true,
                autoLoad: {
                    url: "http://ajaxian.com/archives/mad-cool-date-library/",
                    mode: "iframe",
                    showMask: true
                }
            });

            w.center();
            w.show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug">
            <Listeners>
                <DocumentReady Handler="alignPanels();" />
                <WindowResize Handler="alignPanels();" />
            </Listeners>
        </ext:ScriptManager>
        
        <%--Quick Search--%>
        
        <ext:Store ID="QuickSearchStore" runat="server" AutoLoad="false" OnRefreshData="GetQuickSearchItems">
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SearchItem"></ext:RecordField>
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <BaseParams>
                <ext:Parameter Name="Filter" Value="#{QuickSearchFilter}.getValue()" Mode="Raw"></ext:Parameter>
            </BaseParams>
        </ext:Store>
        
        <div class="x-hidden">
            <ext:GridPanel ID="QuickSearchGrid" runat="server" Width="218" Height="300" StoreID="QuickSearchStore" AutoExpandColumn="SearchItem">
                <ColumnModel>
                    <Columns>
                        <ext:CommandColumn Width="30">
                            <Commands>
                                <ext:GridCommand Icon="Note"></ext:GridCommand>
                            </Commands>
                        </ext:CommandColumn>
                        
                        <ext:Column ColumnID="SearchItem" Header="SearchItem" DataIndex="SearchItem"></ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel runat="server" SingleSelect="true"></ext:RowSelectionModel>
                </SelectionModel>
                <LoadMask ShowMask="true" />
            </ext:GridPanel>
            
            <ext:TriggerField ID="QuickSearchFilter" runat="server" Width="218">
                <Triggers>
                    <ext:FieldTrigger Icon="Search" />
                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                </Triggers>
                <Listeners>
                    <TriggerClick Handler="if(index === 1){trigger.hide(); this.setValue('');}else{this.triggers[1].show();} #{QuickSearchGrid}.reload();" />
                </Listeners>
            </ext:TriggerField>
        </div>
        
        <%--End Quick Search--%>

        <ext:Desktop ID="MyDesktop" runat="server" BackgroundColor="Black" ShortcutTextColor="White" Wallpaper="desktop.jpg">
            <StartButton Text="Start" IconCls="start-button" />
            <%-- NOTE: Body Controls must be added to a container with position:absolute --%>
            <Body>
                <ext:Panel 
                    ID="pnlSample" 
                    runat="server" 
                    Title="Sample Panel"
                    Cls="deskEl" 
                    Height="400" 
                    Width="500"
                    BodyStyle="padding:5px;"
                    Collapsible="true">
                    <Body>
                        <ext:BorderLayout ID="BorderLayout1" runat="server">
                            <West Collapsible="true" Split="true" MarginsSummary="5 0 0 5" CMarginsSummary="5 5 0 5">
                                <ext:Panel ID="Panel1" runat="server" Title="West" Width="150" />
                            </West>
                            <Center MarginsSummary="5 0 0 0">
                                <ext:Panel 
                                    ID="Panel2" 
                                    runat="server" 
                                    Title="Center" 
                                    Html="<h1>Center</h1>Positioned Panel with BorderLayout" 
                                    BodyStyle="padding:5px;">
                                    <BottomBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:Button runat="server" Text="Button" />
                                                <ext:SplitButton runat="server" Text="Split Button">
                                                    <Menu>
                                                        <ext:Menu runat="server">
                                                            <Items>
                                                                <ext:MenuItem runat="server" Text="Item 1" />
                                                                <ext:MenuItem runat="server" Text="Item 2">
                                                                    <Menu>
                                                                        <ext:DateMenu runat="server" />
                                                                    </Menu>
                                                                </ext:MenuItem>
                                                                <ext:MenuItem runat="server" Text="Item 3">
                                                                    <Menu>
                                                                        <ext:ColorMenu runat="server" />
                                                                    </Menu>
                                                                </ext:MenuItem>
                                                            </Items>
                                                        </ext:Menu>
                                                    </Menu>
                                                </ext:SplitButton>
                                            </Items>
                                        </ext:Toolbar>
                                    </BottomBar>    
                                </ext:Panel>
                            </Center>
                            <East Collapsible="true" Split="true" MarginsSummary="5 5 0 0" CMarginsSummary="5 5 0 5">
                                <ext:Panel ID="Panel3" runat="server" Title="East" Width="150">
                                    <Body>
                                        <ext:FitLayout ID="FitLayout1" runat="server">
                                            <ext:TabPanel 
                                                ID="TabPanel1" 
                                                runat="server" 
                                                Height="300" 
                                                TabPosition="Bottom"
                                                Border="false">
                                                <Tabs>
                                                    <ext:Tab 
                                                        ID="Tab1" 
                                                        runat="server" 
                                                        Title="Tab 1" 
                                                        />
                                                    <ext:Tab 
                                                        ID="Tab2" 
                                                        runat="server" 
                                                        Title="Tab 2" 
                                                        Html="Hello!" 
                                                        BodyStyle="padding:5px;" 
                                                        />
                                                </Tabs>
                                            </ext:TabPanel>
                                        </ext:FitLayout>
                                    </Body>
                                </ext:Panel>
                            </East>
                            <South Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                                <ext:Panel ID="Panel4" runat="server" Height="125" Title="South" Collapsed="true" />
                            </South>
                        </ext:BorderLayout>
                    </Body>
                </ext:Panel>
            </Body>
            <Modules>
                <ext:DesktopModule ModuleID="DesktopModule1" WindowID="winCustomer" AutoRun="true">
                    <Launcher ID="Launcher1" runat="server" Text="Add Customer" Icon="Add" />
                </ext:DesktopModule>
                
                <ext:DesktopModule ModuleID="DesktopModule2" WindowID="winCompany" AutoRun="true">
                    <Launcher ID="Launcher2" runat="server" Text="Company Info" Icon="Lorry" />
                </ext:DesktopModule>
                
                <ext:DesktopModule ModuleID="DesktopModule3" WindowID="winBrowser">
                    <Launcher ID="Launcher3" runat="server" Text="Web Browser" Icon="World" />
                </ext:DesktopModule>
            </Modules>  
            
            <Shortcuts>
                <ext:DesktopShortcut ModuleID="DesktopModule1" Text="Add Customer" IconCls="shortcut-icon icon-user48" />
                <ext:DesktopShortcut ModuleID="DesktopModule2" Text="Company Info" IconCls="shortcut-icon icon-grid48" />
                <ext:DesktopShortcut ModuleID="modMisc" Text="Shortcut" IconCls="shortcut-icon icon-window48" X="{DX}-90" Y="{DY}-90" />
            </Shortcuts>
            
            <StartMenu Width="400" Height="388" ToolsWidth="227">
                <ToolItems>
                    <ext:MenuItem Text="Settings" Icon="Wrench">
                        <Listeners>
                            <Click Handler="Ext.Msg.alert('Message', 'Settings Clicked');" />
                        </Listeners>
                    </ext:MenuItem>
                    <ext:MenuItem Text="Logout" Icon="Disconnect">
                        <AjaxEvents>
                            <Click OnEvent="Logout_Click">
                                <EventMask ShowMask="true" Msg="Good Bye..." MinDelay="1000" />
                            </Click>
                        </AjaxEvents>
                    </ext:MenuItem>
                    
                    <ext:MenuSeparator />
                    
                    <ext:ElementMenuItem runat="server" Target="#{QuickSearchGrid}" Shift="false">                        
                    </ext:ElementMenuItem>
                    
                    <ext:ElementMenuItem runat="server" Target="#{QuickSearchFilter}" Shift="false" TargetElement="Wrap">                        
                    </ext:ElementMenuItem>
                </ToolItems>
                <Items>
                    <ext:MenuItem ID="MenuItem1" runat="server" Text="All" Icon="Folder" HideOnClick="false">
                        <Menu>
                            <ext:Menu ID="Menu1" runat="server">
                                <Items>
                                    <ext:MenuItem Text="Add Customer" Icon="Add">
                                        <Listeners>
                                            <Click Handler="#{winCustomer}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Company Info" Icon="Lorry">
                                        <Listeners>
                                            <Click Handler="#{winCompany}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Web Browser" Icon="World">
                                        <Listeners>
                                            <Click Handler="#{winBrowser}.show();" />
                                        </Listeners>
                                    </ext:MenuItem>
                                    <ext:MenuItem Text="Create dynamic" Icon="World">
                                        <Listeners>
                                            <Click Handler="createDynamicWindow(#{MyDesktop});" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:MenuItem>
                    <ext:MenuSeparator />
                </Items>
            </StartMenu>
        </ext:Desktop>
        
        <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh">
            <Reader>
                <ext:ArrayReader>
                    <Fields>
                        <ext:RecordField Name="company" />
                        <ext:RecordField Name="price" Type="Float" />
                        <ext:RecordField Name="change" Type="Float" />
                        <ext:RecordField Name="pctChange" Type="Float" />
                        <ext:RecordField Name="lastChange" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    </Fields>
                </ext:ArrayReader>
            </Reader>
        </ext:Store>
        
        <ext:DesktopWindow 
            ID="winCustomer" 
            runat="server" 
            Title="Add Customer" 
            CenterOnLoad="false"
            Icon="User" 
            BodyStyle="padding:5px;"
            Width="350"
            Height="200"
            PageX="100" 
            PageY="25">
            <Body>
                <ext:FormLayout ID="FormLayout1" runat="server" LabelWidth="120">
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtFirstName" runat="server" FieldLabel="First Name" Text="Steve" />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtLastName" runat="server" FieldLabel="Last Name" Text="Caballero" />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtCompany" runat="server" FieldLabel="Company" Text="Pure Awesome Industries" />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:ComboBox ID="cmbCountry" runat="server" FieldLabel="Country">
                            <SelectedItem Value="United States" />
                            <Items>
                                <ext:ListItem Text="Australia" />
                                <ext:ListItem Text="Canada" />
                                <ext:ListItem Text="Great Britian" />
                                <ext:ListItem Text="Japan" />
                                <ext:ListItem Text="United States" />
                            </Items>
                        </ext:ComboBox>
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:Checkbox ID="chkPremium" runat="server" FieldLabel="Premium Member" Checked="true" />
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button ID="btnSaveCustomer" runat="server" Text="Save" Icon="Disk">
                    <Listeners>
                        <Click Handler="Coolite.AjaxMethods.AddCustomer({
                            success: function(customer) {
                                var template = 'ID: {0}{7} Name: {1} {2}{7} Company: {3}{7} Country: {4}{7} Premium Member: {5}{7} Date Created: {6}{7}',
                                    msg = String.format(template, 
                                            customer.ID, 
                                            customer.FirstName, 
                                            customer.LastName, 
                                            customer.Company, 
                                            customer.Country.Name, 
                                            customer.Premium, 
                                            customer.DateCreated,
                                            '&lt;br /&gt;&lt;br /&gt;');
                                
                                Ext.Msg.alert('Customer Saved', msg);
                            }
                        });" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:DesktopWindow>
        
        <ext:DesktopWindow 
            ID="winCompany" 
            runat="server" 
            CenterOnLoad="false"
            Title="Company Info" 
            Icon="Lorry"             
            Width="550"
            Height="320"
            PageX="200" 
            PageY="125">
            <TopBar>
                <ext:Toolbar ID="ToolBar1" runat="server">
                    <Items>
                        <ext:Button ID="btnSave" runat="server" Text="Save" Icon="Disk">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.save();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnLoad" runat="server" Text="Reload" Icon="ArrowRefresh">
                            <Listeners>
                                <Click Handler="#{GridPanel1}.load();" />
                            </Listeners>
                        </ext:Button>
                        <ext:ToolbarButton ID="extbtnedit" runat="server" Icon="Add" >
                            <ToolTips>
                                <ext:ToolTip ID="ToolTip2" Title="Edit Entry" runat="server" Html="Edit" />
                            </ToolTips>
                        </ext:ToolbarButton>
                    </Items>
                </ext:Toolbar>
            </TopBar>           
            <Body>
                <ext:FitLayout ID="FitLayout2" runat="server">
                    <ext:GridPanel 
                        ID="GridPanel1" 
                        runat="server" 
                        StoreID="Store1" 
                        StripeRows="true"
                        Border="false"
                        AutoExpandColumn="Company">
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column ColumnID="Company" Header="Company" Width="160" Sortable="true" DataIndex="company" />
                                <ext:Column Header="Price" Width="75" Sortable="true" DataIndex="price">
                                    <Renderer Format="UsMoney" />
                                </ext:Column>
                                <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="change">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="pctChange">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
                        </SelectionModel>
                        <LoadMask ShowMask="true" />
                        <BottomBar>
                            <ext:PagingToolBar ID="PagingToolBar2" runat="server" PageSize="10" StoreID="Store1" />
                        </BottomBar>
                    </ext:GridPanel>
                </ext:FitLayout>
            </Body>
        </ext:DesktopWindow>
        
        <ext:DesktopWindow 
            ID="winBrowser" 
            runat="server" 
            Title="Web Browser" 
            Icon="World"              
            Width="1000"
            Height="600"
            PageX="25" 
            PageY="25">
            <AutoLoad Url="http://ajaxian.com/archives/mad-cool-date-library/" Mode="IFrame" />
        </ext:DesktopWindow>
    </form>
</body>
</html>
