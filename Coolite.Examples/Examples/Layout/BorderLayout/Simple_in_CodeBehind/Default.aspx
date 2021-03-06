﻿<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        // Build Panel for West Region
        Coolite.Ext.Web.Panel pnl = new Coolite.Ext.Web.Panel();
        pnl.ID = "Panel1";
        pnl.Title = "Navigation";
        pnl.Width = Unit.Pixel(175);

        // Build TabPanel for Center Region
        Tab tab1 = new Tab();
        tab1.Title = "First Tab";
        tab1.BodyStyle = "padding: 6px;";
        tab1.Html = "First Tab";

        Tab tab2 = new Tab();
        tab2.Title = "Another Tab";
        tab2.BodyStyle = "padding: 6px;";
        tab2.Html = "Another Tab";

        Tab tab3 = new Tab();
        tab3.Title = "Closeable Tab";
        tab3.Closable = true;
        tab3.BodyStyle = "padding: 6px;";
        tab3.Html = "Closeable Tab";

        TabPanel tp = new TabPanel();
        tp.ID = "TabPanel1";
        
        // Set first Tab to be the .ActiveTabIndex
        tp.ActiveTabIndex = 0;
        
        // Add Tabs to TabPanel
        tp.Tabs.Add(tab1);
        tp.Tabs.Add(tab2);
        tp.Tabs.Add(tab3);

        // Build BorderLayout container
        BorderLayout BorderLayout1 = new BorderLayout();

        // Set West Region properties
        BorderLayout1.West.Collapsible = true;
        BorderLayout1.West.MinWidth = Unit.Pixel(175);
        BorderLayout1.West.Split = true;

        // Add Panel to West Region
        BorderLayout1.West.Items.Add(pnl);

        // Add TabPanel to Center Region
        BorderLayout1.Center.Items.Add(tp);
        
        // Build Window to hold everything
        Window win = new Window();
        win.ID = "Window1";
        win.Title = "Simple Layout";
        win.Icon = Icon.Coolite;
        win.Width = Unit.Pixel(600);
        win.Height = Unit.Pixel(350);
        win.Border = false;
        win.Collapsible = true;
        win.Plain = true;

        // Add BorderLayout container to Window
        win.Items.Add(BorderLayout1);

        // Add Window to Form
        this.PlaceHolder1.Controls.Add(win);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple BorderLayout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <h1>Simple BorderLayout in CodeBehind</h1>
    
    <ext:Button 
        ID="Button1" 
        runat="server" 
        Text="Show Window" 
        Icon="Application">
        <Listeners>
            <Click Handler="#{Window1}.show();" />
        </Listeners>    
    </ext:Button>
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    
</body>
</html>
