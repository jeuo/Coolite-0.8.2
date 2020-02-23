<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        TreePanel tree = new TreePanel();

        tree.Title = "Online Users";
        tree.RootVisible = false;

        tree.Tools.Add(new Tool(ToolType.Refresh, "Ext.Msg.alert('Message','Refresh Tool Clicked!');", ""));

        Coolite.Ext.Web.TreeNode root = new Coolite.Ext.Web.TreeNode();
        root.NodeID = "root";

        tree.Root.Add(root);
        
        Coolite.Ext.Web.TreeNode node1 = new Coolite.Ext.Web.TreeNode();
        
        node1.Text = "Friends";
        node1.Expanded = true;
        
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Jack", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Brian", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Jon", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Tim", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Nige", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Fred", Icon.User));
        node1.Nodes.Add(new Coolite.Ext.Web.TreeNode("Bob", Icon.User));

        root.Nodes.Add(node1);

        Coolite.Ext.Web.TreeNode node2 = new Coolite.Ext.Web.TreeNode();
        node2.Text = "Family";
        node2.Expanded = true;
        
        node2.Nodes.Add(new Coolite.Ext.Web.TreeNode("Kelly", Icon.UserFemale));
        node2.Nodes.Add(new Coolite.Ext.Web.TreeNode("Sara", Icon.UserFemale));
        node2.Nodes.Add(new Coolite.Ext.Web.TreeNode("Zack", Icon.UserGreen));
        node2.Nodes.Add(new Coolite.Ext.Web.TreeNode("John", Icon.UserGreen));

        root.Nodes.Add(node2);
        
        Coolite.Ext.Web.Panel panel1 = new Coolite.Ext.Web.Panel("Settings");
        Coolite.Ext.Web.Panel panel2 = new Coolite.Ext.Web.Panel("Even More Stuff");
        Coolite.Ext.Web.Panel panel3 = new Coolite.Ext.Web.Panel("My Stuff");

        Accordion accordion = new Accordion();

        accordion.Items.Add(tree);
        accordion.Items.Add(panel1);
        accordion.Items.Add(panel2);
        accordion.Items.Add(panel3);

        Toolbar toolbar = new Toolbar();

        Coolite.Ext.Web.Button button1 = new Coolite.Ext.Web.Button();
        button1.Icon = Icon.Connect;

        ToolTip tooltip = new ToolTip();
        tooltip.Title = "Rich ToolTips";
        tooltip.Html = "Let your users know what they can do!";

        button1.ToolTips.Add(tooltip);
        
        Coolite.Ext.Web.Button button2 = new Coolite.Ext.Web.Button();
        button2.Icon = Icon.UserAdd;
        
        Coolite.Ext.Web.Button button3 = new Coolite.Ext.Web.Button();
        button3.Icon = Icon.UserDelete;

        toolbar.Items.Add(button1);
        toolbar.Items.Add(button2);
        toolbar.Items.Add(button3);
        
        Window window = new Window();

        window.Title = "Accordion Window";
        window.Width = Unit.Pixel(250);
        window.Height = Unit.Pixel(400);
        window.Maximizable = true;
        window.Icon = Icon.ApplicationTileVertical;
        window.BodyBorder = false;

        window.TopBar.Add(toolbar);

        window.BodyControls.Add(accordion);

        this.PlaceHolder1.Controls.Add(window);
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Accordion Layout in Code-Behind - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h1>Accordion Layout in Code-Behind</h1>
    
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
</body>
</html>
