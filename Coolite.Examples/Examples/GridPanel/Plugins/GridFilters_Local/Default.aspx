<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.ObjectModel" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GridPanel with Local Filtering, Sorting and Paging - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                this.Store1.DataSource = FiltersTestData.Data;
                this.Store1.DataBind();
            }
        }
        protected void SetFilter(object sender, AjaxEventArgs e)
        {
            StringFilter sf = (StringFilter)GridFilters1.Filters[1];
            sf.SetValue("3m Co");
            sf.SetActive(true);
        }
    </script>

</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h1>GridPanel with Local Filtering, Sorting and Paging</h1>
    <p>Please see column header menu for apllying filters</p>
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true" RemoteSort="true">
        <Reader>
            <ext:JsonReader ReaderID="Id">
                <Fields>
                    <ext:RecordField Name="Id" Type="Int" />
                    <ext:RecordField Name="Company" Type="String" />
                    <ext:RecordField Name="Price" Type="Float" />
                    <ext:RecordField Name="Date" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    <ext:RecordField Name="Size" Type="String" />
                    <ext:RecordField Name="Visible" Type="Boolean" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="Company" Direction="ASC" />
    </ext:Store>
    
    <ext:Window 
        ID="Window1" 
        runat="server"         
        Width="700" 
        Height="400" 
        Closable="false"
        Collapsible="true"
        Title="Example" 
        Maximizable="true">
        <Body>
            <ext:FitLayout ID="FitLayout1" runat="server">
                <ext:GridPanel runat="server" ID="GridPanel1" Border="false" StoreID="Store1">
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column Header="Id" DataIndex="Id" Sortable="true" />
                            <ext:Column Header="Company" DataIndex="Company" Sortable="true" />
                            <ext:Column Header="Price" DataIndex="Price" Sortable="true">
                                <Renderer Format="UsMoney" />
                            </ext:Column>
                            <ext:Column Header="Date" DataIndex="Date" Sortable="true" Align="Center">
                                <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                            </ext:Column>
                            <ext:Column Header="Size" DataIndex="Size" Sortable="true" />
                            <ext:Column Header="Visible" DataIndex="Visible" Sortable="true" Align="Center">
                                <Renderer Handler="return (value) ? 'Yes':'No';" />
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" />
                    <Plugins>
                        <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                            <Filters>
                                <ext:NumericFilter DataIndex="Id" />
                                <ext:StringFilter DataIndex="Company" />
                                <ext:NumericFilter DataIndex="Price" />
                                <ext:DateFilter DataIndex="Date">
                                    <DatePickerOptions runat="server" TodayText="Now" />
                                </ext:DateFilter>
                                <ext:ListFilter DataIndex="Size" Options="extra small,small,medium,large,extra large" />
                                <ext:BooleanFilter DataIndex="Visible" />
                            </Filters>
                        </ext:GridFilters>
                    </Plugins>
                    <BottomBar>
                        <ext:PagingToolBar ID="PagingToolBar1" runat="server" PageSize="10">
                            <Items>
                                <ext:ToolbarSeparator runat="server" />
                                <ext:ToolbarButton runat="server" Text="Find '3m Co'">
                                    <AjaxEvents>
                                        <Click OnEvent="SetFilter"></Click>
                                    </AjaxEvents>
                                </ext:ToolbarButton>
                            </Items>
                        </ext:PagingToolBar>
                    </BottomBar>                   
                </ext:GridPanel>
            </ext:FitLayout>
        </Body>
    </ext:Window>
</body>
</html>
