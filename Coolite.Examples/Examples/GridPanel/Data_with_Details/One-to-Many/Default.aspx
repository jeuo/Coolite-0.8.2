<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Store2_Refresh(object sender, StoreRefreshDataEventArgs e)
    {
        string id = e.Parameters["SupplierID"];
        LinqDataSource2.WhereParameters["SupplierID"].DefaultValue = id ?? "-1";
        Store2.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>One to Many Data Relationship with GridPanels - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:LinqDataSource 
        ID="LinqDataSource1" 
        runat="server" 
        ContextTypeName="Coolite.Examples.Code.Northwind.NorthwindDataContext"
        Select="new (SupplierID,
                 CompanyName, 
                 ContactName, 
                 ContactTitle, 
                 Address, 
                 City, 
                 Region, 
                 PostalCode, 
                 Country, 
                 Phone, 
                 Fax)" 
        TableName="Suppliers" 
        />
    
    <asp:LinqDataSource 
        ID="LinqDataSource2" 
        runat="server" 
        ContextTypeName="Coolite.Examples.Code.Northwind.NorthwindDataContext"
        Select="new (ProductName,
                 QuantityPerUnit,
                 UnitPrice, 
                 UnitsInStock, 
                 Discontinued, 
                 UnitsOnOrder, 
                 ReorderLevel, 
                 ProductID, 
                 SupplierID)" 
        TableName="Products" 
        AutoGenerateWhereClause="True">
        <WhereParameters>
            <asp:Parameter Name="SupplierID" Type="Int32" DefaultValue="-1" />
        </WhereParameters>
    </asp:LinqDataSource>
    
    <ext:Store ID="Store1" runat="server" DataSourceID="LinqDataSource1">
        <Reader>
            <ext:JsonReader ReaderID="SupplierID">
                <Fields>
                    <ext:RecordField Name="CompanyName" />
                    <ext:RecordField Name="ContactName" />
                    <ext:RecordField Name="ContactTitle" />
                    <ext:RecordField Name="Address" />
                    <ext:RecordField Name="City" />
                    <ext:RecordField Name="Region" />
                    <ext:RecordField Name="PostalCode" />
                    <ext:RecordField Name="Country" />
                    <ext:RecordField Name="Phone" />
                    <ext:RecordField Name="Fax" />
                    <ext:RecordField Name="HomePage" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    <ext:Store ID="Store2" runat="server" DataSourceID="LinqDataSource2" OnRefreshData="Store2_Refresh">
        <Reader>
            <ext:JsonReader ReaderID="ProductID">
                <Fields>
                    <ext:RecordField Name="ProductName" />
                    <ext:RecordField Name="QuantityPerUnit" />
                    <ext:RecordField Name="UnitPrice" Type="Float" />
                    <ext:RecordField Name="UnitsInStock" Type="Int" />
                    <ext:RecordField Name="UnitsOnOrder" Type="Int" />
                    <ext:RecordField Name="ReorderLevel" Type="Int" />
                    <ext:RecordField Name="Discontinued" Type="Boolean" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <BaseParams>
            <ext:Parameter 
                Name="SupplierID" 
                Value="#{GridPanel1}.getSelectionModel().hasSelection() ? #{GridPanel1}.getSelectionModel().getSelected().id : -1"
                Mode="Raw" />
        </BaseParams>
        <Listeners>
            <LoadException Handler="Ext.Msg.alert('Products - Load failed', e.message || response.statusText);" />
        </Listeners>
    </ext:Store>
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North MarginsSummary="5 5 5 5">
                    <ext:Panel ID="Panel1" runat="server" Title="Description" Height="100" BodyStyle="padding: 5px;"
                        Frame="true" Icon="Information">
                        <Body>
                            <h1>One to Many Data Relationship with GridPanels</h1>
                            <p>Click on any record within the parent GridPanel to load related data into second GridPanel.</p>
                            <p>If South Region is collapsed then Ajax load is not performed for the second GridPanel.
                            After South Region is expanded the Ajax request will be performed.</p>
                        </Body>
                    </ext:Panel>
                </North>
                <Center MarginsSummary="0 5 0 5">
                    <ext:Panel runat="server" Frame="true" Title="Suppliers" Icon="Lorry">
                        <Body>
                            <ext:FitLayout runat="server">
                                <ext:GridPanel ID="GridPanel1" runat="server" AutoExpandColumn="CompanyName" StoreID="Store1">
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="CompanyName" DataIndex="CompanyName" Header="Company Name" />
                                            <ext:Column DataIndex="ContactName" Header="Contact Name" />
                                            <ext:Column DataIndex="ContactTitle" Header="Contact Title" />
                                            <ext:Column DataIndex="Address" Header="Address" />
                                            <ext:Column DataIndex="City" Header="City" />
                                            <ext:Column DataIndex="Region" Header="Region" Width="200" />
                                            <ext:Column DataIndex="PostalCode" Header="Postal Code" />
                                            <ext:Column DataIndex="Country" Header="Country" />
                                            <ext:Column DataIndex="Phone" Header="Phone" />
                                            <ext:Column DataIndex="Fax" Header="Fax" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel runat="server" SingleSelect="true">
                                            <Listeners>
                                                <RowSelect Handler="if(#{pnlSouth}.isVisible()){#{Store2}.reload();}" Buffer="250" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                    <BottomBar>
                                        <ext:PagingToolBar ID="PagingToolBar1" runat="server" PageSize="10" StoreID="Store1" />
                                    </BottomBar>
                                    <LoadMask ShowMask="true" />
                                </ext:GridPanel>
                            </ext:FitLayout>
                        </Body>
                    </ext:Panel>
                </Center>
                <South Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                    <ext:Panel ID="pnlSouth" runat="server" Title="Products" Height="200" Icon="Basket">
                        <Body>
                            <ext:FitLayout runat="server">
                                <ext:GridPanel ID="GridPanel2" runat="server" StoreID="Store2" AutoExpandColumn="ProductName"
                                    Border="false">
                                    <ColumnModel runat="server">
                                        <Columns>
                                            <ext:Column ColumnID="ProductName" DataIndex="ProductName" Header="Product Name" />
                                            <ext:Column DataIndex="QuantityPerUnit" Header="Quantity Per Unit" />
                                            <ext:Column DataIndex="UnitPrice" Header="Unit Price" />
                                            <ext:Column DataIndex="UnitsInStock" Header="Units In Stock" />
                                            <ext:Column DataIndex="UnitsOnOrder" Header="Units On Order" />
                                            <ext:Column DataIndex="ReorderLevel" Header="Reorder Level" />
                                            <ext:CheckColumn DataIndex="Discontinued" Header="Discontinued" />
                                        </Columns>
                                    </ColumnModel>
                                    <LoadMask ShowMask="true" />
                                    <SelectionModel>
                                        <ext:RowSelectionModel runat="server" SingleSelect="true" />
                                    </SelectionModel>                                   
                                </ext:GridPanel>
                            </ext:FitLayout>
                        </Body>
                        <Listeners>
                            <Expand Handler="#{Store2}.reload();" />
                        </Listeners>
                    </ext:Panel>
                </South>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
    </form>
</body>
</html>
