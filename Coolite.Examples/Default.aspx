<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Coolite.Examples" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            // Reset the Session Theme on Page_Load.
            // The Theme switcher will persist the current theme only 
            // until the main Page is refreshed.
            this.Session["Coolite.Theme"] = Coolite.Ext.Web.Theme.Default;
            
            this.cbTheme.SelectedItem.Value = this.ScriptManager1.Theme.ToString();

            string loadExample = "";
            
            if (!this.IsPostBack)
            {
                string url = "";
                if (this.Request.QueryString.Count > 0)
                {
                    url = this.Request.QueryString[0];
                }

                if (!string.IsNullOrEmpty(url) && this.CheckQueryString(url))
                {
                    loadExample = string.Concat("loadExample(", JSON.Serialize(url), ", '-');");

                    this.ScriptManager1.Listeners.DocumentReady.Handler = loadExample;
                }
            }

            bool refreshMapSite = false;
            
            if(this.Request.IsLocal && Request.QueryString["refreshMap"] != null)
            {
                refreshMapSite = true;
                //refresh map site
                UIHelpers.BuildTreeNodes(true);
            }
            
            if(refreshMapSite)
            {   
                Response.Redirect("~/");
            }
        }
    }

    protected void RefreshHomeTabData(object sender, StoreRefreshDataEventArgs e)
    {
        var data = this.Page.Cache["ExamplesGroups"] as List<ExampleGroup>;
        
        if(data == null)
        {
            data = new List<ExampleGroup>();
            UIHelpers.FindExamples(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("~/Examples/")), 1, 3, data);
            this.Page.Cache.Add("ExamplesGroups", data, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        
        this.Store1.DataSource = data;
        this.Store1.DataBind();
    }
    
    protected void GetExamplesNodes(object sender, NodeLoadEventArgs e)
    {
        if(e.NodeID == "root")
        {
            var nodes = this.Page.Cache["ExamplesTreeNodes"] as Coolite.Ext.Web.TreeNodeCollection;
            
            if(nodes == null)
            {
                nodes = UIHelpers.BuildTreeNodes(false);
                this.Page.Cache.Add("ExamplesTreeNodes", nodes, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default,null);
            }
            
            e.Nodes = nodes;
        }
    }

    private bool CheckQueryString(string url)
    {
        url = url.ToLower();

        if (!url.EndsWith("/"))
        {
            url = string.Concat(url, "/");
        }

        string examplesFolder = new Uri(HttpContext.Current.Request.Url, "/Examples/").ToString().ToLower();
        
        if (!url.StartsWith(examplesFolder))
        {
            url = string.Concat(examplesFolder.TrimEnd(new[] { '/' }), url);
        }

        Uri uri = new Uri(url, UriKind.Absolute);

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.Server.MapPath(uri.AbsolutePath));

        return System.IO.File.Exists(string.Concat(dir.FullName, "config.xml"));
    }

    [AjaxMethod]
    public string GetThemeUrl(string theme)
    {
        Theme temp = (Theme)Enum.Parse(typeof(Theme), theme);

        this.Session["Coolite.Theme"] = temp;
        
        return (temp == Coolite.Ext.Web.Theme.Default) ? "Default" : this.ScriptManager1.GetThemeUrl(temp);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Examples - ExtJS ASP.NET Web Controls with AJAX</title>
    <link rel="stylesheet" type="text/css" href="resources/css/main.css" />
    <script type="text/javascript" src="resources/ExampleTab.js"></script>

    <script type="text/javascript">
        var loadExample = function (href, id) {
            var tab = ExampleTabs.getComponent(id);
            
            if (tab) {
                ExampleTabs.setActiveTab(tab);
            } else {
                createExampleTab(id, href);               
            }
        }

        var selectionChaged = function (dv, nodes) {
            if (nodes.length > 0) {
                var url = nodes[0].getAttribute("ext:url"),
                    id = nodes[0].getAttribute("ext:id");
                    
                loadExample(url, id);
            }
        }

        var viewClick = function (dv, e) {
            var group = e.getTarget("h2", 3, true);
            
            if (group) {
                group.up("div").toggleClass("collapsed");
            }
        }
    </script>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store ID="Store1" runat="server" AutoLoad="true" SerializationMode="Complex" OnRefreshData="RefreshHomeTabData">
        <Proxy>
            <ext:DataSourceProxy/>
        </Proxy>
        <AjaxEventConfig Method="GET"/>
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="id" />
                    <ext:RecordField Name="title" />
                    <ext:RecordField Name="samples" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
            <BeforeLoad Handler="#{tabHome}.body.mask('Loading...', 'x-mask-loading');" />
            <Load Handler="#{tabHome}.body.unmask();" />
            <LoadException Handler="#{tabHome}.body.unmask();" />
        </Listeners>
    </ext:Store>
    
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North Margins-Bottom="5">
                    <ext:Panel IDMode="Ignore" runat="server" Header="false" Border="false" Html="<div class='message'>You are viewing the <strong>Version 0.8</strong> Examples Explorer. See <a href='http://examples07.coolite.com/'>Version 0.7 Explorer</a>.</div><div id='header'><h1>Coolite Examples Explorer (v0.8)</h1></div>" />
                </North>
                <West Collapsible="true" Split="true">
                    <ext:Panel runat="server" Header="false" Border="false" Width="240">
                        <Body>
                            <ext:FitLayout runat="server">
                                <ext:TreePanel 
                                    ID="exampleTree" 
                                    runat="server" 
                                    Title="Examples" 
                                    AutoScroll="true"
                                    Lines="false"
                                    CollapseFirst="false" 
                                    ContainerScroll="true"
                                    RootVisible="false">
                                    <TopBar>
                                        <ext:Toolbar runat="server">
                                            <Items>
                                                <ext:ToolbarTextItem runat="server" Text="Theme: " />
                                                <ext:ComboBox 
                                                    ID="cbTheme" 
                                                    runat="server" 
                                                    EmptyText="Choose Theme" 
                                                    Width="75"
                                                    Editable="false"
                                                    TypeAhead="true">
                                                    <Items>
                                                        <ext:ListItem Text="Default" Value="Default" />
                                                        <ext:ListItem Text="Gray" Value="Gray" />
                                                        <ext:ListItem Text="Slate" Value="Slate" />
                                                    </Items>
                                                    <Listeners>
                                                        <Select Handler="Coolite.AjaxMethods.GetThemeUrl(cbTheme.getValue(),{
                                                                success: function (result) {
                                                                    Coolite.Ext.setTheme(result);
                                                                    ExampleTabs.items.each(function (el) {
                                                                        if (!Ext.isEmpty(el.iframe)) {
                                                                            el.iframe.dom.contentWindow.Coolite.Ext.setTheme(result);
                                                                        }
                                                                    });
                                                                }
                                                            });" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                
                                                <ext:ToolbarFill runat="server" />
                                                
                                                <ext:ToolbarButton runat="server" IconCls="icon-expand-all">
                                                    <Listeners>
                                                        <Click Handler="#{exampleTree}.root.expand(true);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip IDMode="Ignore" runat="server" Html="Expand All" />
                                                    </ToolTips>
                                                </ext:ToolbarButton>
                                                
                                                <ext:ToolbarButton runat="server" IconCls="icon-collapse-all">
                                                    <Listeners>
                                                        <Click Handler="#{exampleTree}.root.collapse(true);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip IDMode="Ignore" runat="server" Html="Collapse All" />
                                                    </ToolTips>
                                                </ext:ToolbarButton>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Root>
                                        <ext:AsyncTreeNode Text="Examples" NodeID="root" Expanded="true" />
                                    </Root>
                                    <Loader>
                                        <ext:PageTreeLoader RequestMethod="GET" OnNodeLoad="GetExamplesNodes" PreloadChildren="true">
                                            <EventMask ShowMask="true" Target="Parent" Msg="Loading..." />
                                        </ext:PageTreeLoader>
                                    </Loader>
                                    <Listeners>
                                        <Click Handler="if(node.isLeaf()){e.stopEvent();loadExample(node.attributes.href, node.id);}" />
                                    </Listeners>                                                           
                                </ext:TreePanel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </West>
                <Center>
                    <ext:TabPanel ID="ExampleTabs" runat="server" ActiveTabIndex="0" EnableTabScroll="true">
                        <Tabs>
                            <ext:Tab ID="tabHome" runat="server" IconCls="icon-application" Title="Home" AutoScroll="true">
                                <Body>
                                    <ext:FitLayout runat="server">
                                        <ext:Panel ID="ImagePanel" runat="server" Cls="images-view" AutoHeight="true" Border="false">
                                            <Body>
                                                <ext:FitLayout ID="FitLayout1" runat="server">
                                                    <ext:DataView 
                                                        IDMode="Ignore"
                                                        runat="server" 
                                                        StoreID="Store1" 
                                                        SingleSelect="true"
                                                        OverClass="x-view-over" 
                                                        ItemSelector="div.thumb-wrap" 
                                                        AutoHeight="true" 
                                                        EmptyText="No examples to display">
                                                        <Template runat="server">
                                                            <div id="sample-ct">
	                                                            <tpl for=".">
	                                                                <div>
	                                                                    <a name="{id}"></a>
	                                                                    <h2><div>{title}</div></h2>
	                                                                    <dl>
		                                                                    <tpl for="samples">
			                                                                    <div class="thumb-wrap" ext:url="{url}" ext:id="{id}">
	                                                                                <img src="{imgUrl}" title="{name}" />
	                                                                                <div>
	                                                                                    <H6>{sub}</H6>
	                                                                                    <H4>{name}</H4>
                                                                                        <P>{descr}</P>
	                                                                                </div>
                                                                                </div>
		                                                                    </tpl>
	                                                                        <div style="clear:left"></div>
	                                                                     </dl>
	                                                                </div>
	                                                            </tpl>
                                                            </div>
                                                        </Template>
                                                        <Listeners>
                                                            <SelectionChange Fn="selectionChaged" />
                                                            <ContainerClick Fn="viewClick" />
                                                        </Listeners>
                                                    </ext:DataView>
                                                </ext:FitLayout>
                                            </Body>
                                        </ext:Panel>
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                        </Tabs>
                        <Plugins>
                            <ext:TabCloseMenu runat="server" />
                        </Plugins>
                    </ext:TabPanel>
                </Center>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>
