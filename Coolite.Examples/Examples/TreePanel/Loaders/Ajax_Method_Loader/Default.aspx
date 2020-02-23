<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TreePanel using Ajax Method - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">

        [AjaxMethod]
        public static string NodeLoad(string nodeID)
        {
            Coolite.Ext.Web.TreeNodeCollection nodes = new Coolite.Ext.Web.TreeNodeCollection();
            if (!string.IsNullOrEmpty(nodeID))
            {
                for (int i = 1; i < 6; i++)
                {
                    AsyncTreeNode asyncNode = new AsyncTreeNode();
                    asyncNode.Text = nodeID + i;
                    asyncNode.NodeID = nodeID + i;
                    nodes.Add(asyncNode);
                }

                for (int i = 6; i < 11; i++)
                {
                    Coolite.Ext.Web.TreeNode treeNode = new Coolite.Ext.Web.TreeNode();
                    treeNode.Text = nodeID + i;
                    treeNode.NodeID = nodeID + i;
                    treeNode.Leaf = true;
                    nodes.Add(treeNode);
                }
            }

            return nodes.ToJson();
        }
    </script>
    
    <script type="text/javascript">
        function nodeLoad(node) {
            Coolite.AjaxMethods.NodeLoad(node.id, {
                success: function(result) {
                    var data = eval("(" + result + ")");
                    node.loadNodes(data);
                },

                failure: function(errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
        
        <h1>TreePanel using Ajax Method</h1>         
        
        <ext:TreePanel 
            ID="TreePanel1" 
            runat="server" 
            Title="Tree" 
            AutoHeight="true" 
            Border="false">
            <Root>
                <ext:AsyncTreeNode NodeID="0" Text="Root" />
            </Root>
            <Listeners>
                <BeforeLoad Fn="nodeLoad" />
            </Listeners>
        </ext:TreePanel>       
    </form>
</body>
</html>