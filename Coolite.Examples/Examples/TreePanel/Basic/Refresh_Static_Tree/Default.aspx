<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>SiteMap - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Ext.IsAjaxRequest)
            {
                this.BuildTree(TreePanel1.Root);
            }
        }
        
        private Coolite.Ext.Web.TreeNodeCollection BuildTree(Coolite.Ext.Web.TreeNodeCollection nodes)
        {
            if(nodes == null)
            {
                nodes = new Coolite.Ext.Web.TreeNodeCollection();
            }
            
            Coolite.Ext.Web.TreeNode root = new Coolite.Ext.Web.TreeNode();
            root.Text = "Root";
            nodes.Add(root);

            string prefix = DateTime.Now.Second + "_";
            for (int i = 0; i < 10; i++)
            {
                Coolite.Ext.Web.TreeNode node = new Coolite.Ext.Web.TreeNode();
                node.Text = prefix + i;
                root.Nodes.Add(node);
            }

            return nodes;
        }

        [AjaxMethod]
        public string RefreshMenu()
        {
            Coolite.Ext.Web.TreeNodeCollection nodes = this.BuildTree(null);
            return nodes.ToJson();
        }
    </script>
    
    <script type="text/javascript">
        function refreshTree(tree) {
            Coolite.AjaxMethods.RefreshMenu({
                success: function(result) {
                    var nodes = eval(result);
                    tree.root.ui.remove();
                    tree.initChildren(nodes);
                    tree.root.render();
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
        
        <ext:TreePanel ID="TreePanel1" runat="server" 
            Icon="Anchor" 
            Title="Tree"
            AutoScroll="true" 
            Width="250" 
            Collapsed="False" 
            CollapseFirst="True" 
            HideParent="False"
            RootVisible="False" 
            BodyStyle="padding-left:10px">
          <Tools>            
           <ext:Tool Type="Refresh" Qtip="Refresh" Handler="refreshTree(#{TreePanel1});" />
          </Tools>
         </ext:TreePanel>
    </form>
</body>
</html>