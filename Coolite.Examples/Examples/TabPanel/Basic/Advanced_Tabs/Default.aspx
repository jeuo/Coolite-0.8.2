<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        TabPanel tabs = new TabPanel();

        tabs.ID = "TabPanel1";
        tabs.ResizeTabs = true;
        tabs.MinTabWidth = Unit.Pixel(115);
        tabs.TabWidth = Unit.Pixel(135);
        tabs.EnableTabScroll = true;
        tabs.Width = Unit.Pixel(600);
        tabs.Height = Unit.Pixel(250);
        tabs.ActiveTabIndex = 6;

        int index = 1;
        while (index <= 7)
        {
            Tab tab = new Tab();
            tab.ID = "Tab" + index.ToString();
            tab.Title = "New Tab " + index.ToString();
            tab.Icon = Icon.Tab;
            tab.Html = "Tab Body " + index.ToString();
            tab.Closable = true;
            tab.BodyStyle = "padding: 6px;";
            
            tabs.Tabs.Add(tab);
            index++;
        }
        
        this.PlaceHolder1.Controls.Add(tabs);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Advanced TabPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
    </form>
</body>
</html>
