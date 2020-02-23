<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="System.Xml"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
    {
        int start = int.Parse(e.Parameters["startRemote"]);
        int limit = int.Parse(e.Parameters["limitRemote"]);
        List<object> data = new List<object>(limit);
        for (int i = start; i < start + limit; i++)
        {
            data.Add(new {field = "Value"+(i+1)});
        }

        e.TotalCount = 8000;
        this.Store1.DataSource = data;
        this.Store1.DataBind(); 
    }

    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Local paging for remote data - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />   
    
    <script type="text/javascript">
        function remoteLoad(grid){
            grid.body.mask('Loading...', 'x-mask-loading');
            delete grid.store.lastParams; 
            grid.store.load({callback:function(){grid.body.unmask();}});
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
        
        <ext:Store 
            ID="Store1"
            runat="server"    
            AutoLoad="false"
            RemotePaging="false"         
            OnRefreshData="MyData_Refresh">
            <Proxy>
                <ext:DataSourceProxy></ext:DataSourceProxy>
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="field" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <BaseParams>
                <ext:Parameter Name="startRemote" Value="#{Slider1}.getValue()" Mode="Raw"></ext:Parameter>
                <ext:Parameter Name="limitRemote" Value="1000" Mode="Raw"></ext:Parameter>
                
                <ext:Parameter Name="start" Value="0" Mode="Raw"></ext:Parameter>
                <ext:Parameter Name="limit" Value="10" Mode="Raw"></ext:Parameter>
            </BaseParams>
        </ext:Store>
        
        <ext:GridPanel ID="GridPanel1" 
            runat="server" 
            StoreID="Store1" 
            StripeRows="true"
            Title="Grid" 
            Width="600" 
            Height="320"
            AutoExpandColumn="Field">
            <ColumnModel runat="server">
                <Columns>
                    <ext:Column ColumnID="Field" Header="Field" Width="160" Sortable="true" DataIndex="field">
                        <Editor>
                            <ext:TextField runat="server" />
                        </Editor>
                    </ext:Column>                   
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" />
            </SelectionModel>
            <BottomBar>
                <ext:PagingToolBar runat="server" PageSize="10" />
            </BottomBar>
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>                        
                        <ext:ToolbarTextItem runat="server" Text="Remote Pager:"></ext:ToolbarTextItem>     
                        <ext:ToolbarSpacer runat="server" Width="20"/>                   
                        <ext:Slider ID="Slider1" runat="server" MinValue="0" MaxValue="7000" Increment="1000" Width="250">
                            <Plugins>
                                <ext:SliderTip ID="SliderTip1" runat="server" />
                            </Plugins>
                            <Listeners>
                                <Change Handler="#{RangeText}.setText((newValue+1)+'-'+(newValue+1000));" />
                                <ChangeComplete Handler="remoteLoad(#{GridPanel1});" />
                            </Listeners>
                        </ext:Slider>
                        <ext:ToolbarSpacer runat="server" Width="20"/>
                        <ext:ToolbarTextItem ID="RangeText" runat="server" Text="(1-1000) "></ext:ToolbarTextItem>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            
            <Listeners>
                <Render Handler="remoteLoad(#{GridPanel1});" Delay="20" />
            </Listeners>
        </ext:GridPanel>  
        
        <ext:Label ID="Label1" runat="server" />
    </form>
</body>
</html>
