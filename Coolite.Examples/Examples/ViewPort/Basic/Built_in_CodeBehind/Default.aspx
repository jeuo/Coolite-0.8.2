<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        //////////////////
        // NORTH REGION //
        //////////////////

        // Make Panel for South Region
        Coolite.Ext.Web.Panel north = new Coolite.Ext.Web.Panel();
        north.ID = "NorthPanel";
        north.Title = "North";
        north.Height = Unit.Pixel(150);
        north.BodyStyle = "padding:6px;";
        north.Html = "North";
        
        
        /////////////////
        // WEST REGION //
        /////////////////
        
        // Make Panel for West Region
        Coolite.Ext.Web.Panel west = new Coolite.Ext.Web.Panel();
        west.ID = "WestPanel";
        west.Title = "West";
        west.Width = Unit.Pixel(225);
        
        // Make Navigation Panel for Accordion
        Coolite.Ext.Web.Panel pnlNavigation = new Coolite.Ext.Web.Panel();
        pnlNavigation.ID = "Navigation";
        pnlNavigation.Title = "Navigation";
        pnlNavigation.Border = false;
        pnlNavigation.BodyStyle = "padding:6px;";
        pnlNavigation.Icon = Icon.FolderGo;
        pnlNavigation.Html = "West";

        // Make Settings Panel for Accordion
        Coolite.Ext.Web.Panel pnlSettings = new Coolite.Ext.Web.Panel();
        pnlSettings.ID = "Settings";
        pnlSettings.Title = "Settings";
        pnlSettings.Border = false;
        pnlSettings.BodyStyle = "padding:6px;";
        pnlSettings.Icon = Icon.FolderWrench;
        pnlSettings.Html = "Some settings in here";        
     
        // Make Accordion container
        Accordion acc = new Accordion();
        acc.Animate = true;
        
        // Add Navigation and Settings Panels to Accordion
        acc.Items.Add(pnlNavigation);
        acc.Items.Add(pnlSettings);
        
        // Add Accordion to West Panel
        west.Items.Add(acc);


        ///////////////////
        // CENTER REGION //
        ///////////////////   

        // Make TabPanel for Center Region
        TabPanel center = new TabPanel();
        center.ID = "CenterPanel";
        center.ActiveTabIndex = 0;
        
        // Make Tab
        Tab tab1 = new Tab();
        tab1.ID = "Tab2";
        tab1.Title = "Center";
        tab1.Border = false;
        tab1.BodyStyle = "padding:6px;";
        tab1.Html = "<h1>ViewPort with BorderLayout</h1>";

        Tab tab2 = new Tab();
        tab2.ID = "Tab1";
        tab2.Title = "Close Me";
        tab2.Closable = true;
        tab2.Border = false;
        tab2.BodyStyle = "padding:6px;";
        tab2.Html = "Closable Tab";
        
        // Add Tab's to TabPanel
        center.Tabs.Add(tab1);
        center.Tabs.Add(tab2);

        
        /////////////////
        // EAST REGION //
        /////////////////
        
        // Make Panel for East Region
        Coolite.Ext.Web.Panel east = new Coolite.Ext.Web.Panel();
        east.ID = "EastPanel";
        east.Title = "East";
        east.Width = Unit.Pixel(225);
        
        // Make TabPanel for East Panel
        TabPanel tpEast = new TabPanel();
        tpEast.ActiveTabIndex = 1;
        tpEast.TabPosition = TabPosition.Bottom;
        tpEast.Border = false;
        
        // Make Tab 1
        Tab tabEast1 = new Tab();
        tabEast1.Title = "Tab 1";
        tabEast1.Border = false;
        tabEast1.BodyStyle = "padding:6px;";
        tabEast1.Html = "East Tab 1";
        
        // Make Tab 2
        Tab tabEast2 = new Tab();
        tabEast2.Title = "Tab 2";
        tabEast2.Border = false;
        tabEast2.BodyStyle = "padding:6px;";
        tabEast2.Html = "East Tab 2";
        
        // Add Tab's to East TabPanel
        tpEast.Tabs.Add(tabEast1);
        tpEast.Tabs.Add(tabEast2);
        
        // Make new FitLayout container
        FitLayout fit = new FitLayout();
        
        // Add TabPanel East to FitLayout
        fit.Items.Add(tpEast);
        
        // Add FitLayout container to East Panel
        east.Items.Add(fit);
        
        
        //////////////////
        // SOUTH REGION //
        //////////////////
        
        // Make Panel for South Region
        Coolite.Ext.Web.Panel south = new Coolite.Ext.Web.Panel();
        south.ID = "SouthPanel";
        south.Title = "South";
        south.Height = Unit.Pixel(150);
        south.BodyStyle = "padding:6px;";
        south.Html = "South";
        
        
        //////////////
        // VIEWPORT //
        //////////////        
        
        // Make BorderLayout container for ViewPort
        BorderLayout layout = new BorderLayout();

        // Set North Region properties
        layout.North.Split = true;
        layout.North.Collapsible = true;
        
        // Set West Region properties
        layout.West.MinWidth = Unit.Pixel(225);
        layout.West.MaxWidth = Unit.Pixel(400);
        layout.West.Split = true;
        layout.West.Collapsible = true;
        
        // Set East Region properties
        layout.East.MinWidth = Unit.Pixel(225);
        layout.East.Split = true;
        layout.East.Collapsible = true;
        
        // Set South Region properties
        layout.South.Split = true;
        layout.South.Collapsible = true;
        
        // Add Panels to BorderLayout Regions
        layout.North.Items.Add(north);
        layout.West.Items.Add(west);
        layout.Center.Items.Add(center);
        layout.East.Items.Add(east);
        layout.South.Items.Add(south);

        // Make ViewPort to fold everything
        ViewPort vp = new ViewPort();
        vp.ID = "ViewPort1";

        // Add BorderLayout to ViewPort
        vp.Items.Add(layout);
        
        // Add ViewPort to Form
        this.Controls.Add(vp);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViewPort with BorderLayout - Coolite Toolkit Examples</title>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
</body>
</html>