<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SiteMap - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMapNode siteNode = SiteMap.RootNode;
            Coolite.Ext.Web.TreeNode root = this.CreateNode(siteNode);
            TreePanel1.Root.Add(root);
            
            //dynamic tree root
            TreePanel2.Root.Add(this.CreateNodeWithOutChildren(siteNode));
        }
        
        //page tree node loader handler
        protected void LoadPages(object sender, NodeLoadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NodeID))
            {
                SiteMapNode siteMapNode = SiteMap.Provider.FindSiteMapNodeFromKey(e.NodeID);

                SiteMapNodeCollection children = siteMapNode.ChildNodes;
                if (children != null && children.Count > 0)
                {
                    foreach (SiteMapNode mapNode in siteMapNode.ChildNodes)
                    {
                        e.Nodes.Add(this.CreateNodeWithOutChildren(mapNode));
                    }
                }
            }
        }
        
        //dynamic node creation
        private Coolite.Ext.Web.TreeNodeBase CreateNodeWithOutChildren(SiteMapNode siteMapNode)
        {
            Coolite.Ext.Web.TreeNodeBase treeNode;

            if (siteMapNode.ChildNodes != null && siteMapNode.ChildNodes.Count>0)
            {
                treeNode = new Coolite.Ext.Web.AsyncTreeNode();
            }
            else
            {
                treeNode = new Coolite.Ext.Web.TreeNode();
                treeNode.Leaf = true;
            }
            
            if (!string.IsNullOrEmpty(siteMapNode.Url))
            {
                treeNode.Href = this.Page.ResolveUrl(siteMapNode.Url);
            }

            treeNode.NodeID = siteMapNode.Key;
            treeNode.Text = siteMapNode.Title;
            treeNode.Qtip = siteMapNode.Description;

            return treeNode;
        }

        //static node creation with children
        private Coolite.Ext.Web.TreeNode CreateNode(SiteMapNode siteMapNode)
        {
            Coolite.Ext.Web.TreeNode treeNode = new Coolite.Ext.Web.TreeNode();
            if (!string.IsNullOrEmpty(siteMapNode.Url))
            {
                treeNode.Href = this.Page.ResolveUrl(siteMapNode.Url);    
            }
            treeNode.NodeID = siteMapNode.Key;
            treeNode.Text = siteMapNode.Title;
            treeNode.Qtip = siteMapNode.Description;

            SiteMapNodeCollection children = siteMapNode.ChildNodes;
            if (children != null && children.Count > 0)
            {
                foreach (SiteMapNode mapNode in siteMapNode.ChildNodes)
                {
                    treeNode.Nodes.Add(this.CreateNode(mapNode));
                }
            }
            
            return treeNode;
        }
    </script>
    
    <script type="text/javascript">
        var loadPage = function(tabPanel, node) {
            var tab = tabPanel.getItem(node.id);
            if (!tab) {
                tab = tabPanel.add({
                    id: node.id,
                    title: node.text,
                    closable: true,
                    autoLoad: {
                        showMask: true,
                        url: node.attributes.href,
                        mode: 'iframe',
                        maskMsg: 'Loading ' + node.attributes.href + '...'
                    },
                    listeners: {
                        update: {
                            fn: function(tab, cfg) {
                                cfg.iframe.setHeight(cfg.iframe.getSize().height - 20); 
                            },
                            scope: this,
                            single: true
                        }
                    }
                });                
            }
            tabPanel.setActiveTab(tab);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:ViewPort ID="ViewPort1" runat="server">
            <Body>
                <ext:BorderLayout ID="BorderLayout1" runat="server">
                    <West>
                        <ext:TreePanel ID="TreePanel1" runat="server" Width="300" Title="Site Map - preloaded tree" Icon="ChartOrganisation">            
                            <Listeners>
                                <Click Handler="if(node.attributes.href){e.stopEvent();loadPage(#{Pages}, node);}" />
                            </Listeners>
                        </ext:TreePanel>
                    </West>
                    
                    <Center>
                       <ext:TabPanel ID="Pages" runat="server" EnableTabScroll="true">
                       </ext:TabPanel>
                    </Center>
                    
                    <East>
                        <ext:TreePanel ID="TreePanel2" runat="server" Width="300" Title="Site Map - dynamic tree" Icon="ChartOrganisation">            
                            <Listeners>
                                <Click Handler="if(node.attributes.href){e.stopEvent();loadPage(#{Pages}, node);}" />
                            </Listeners>
                            <Loader>
                                <ext:PageTreeLoader OnNodeLoad="LoadPages"></ext:PageTreeLoader>
                            </Loader>
                        </ext:TreePanel>
                    </East>
                </ext:BorderLayout>
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>