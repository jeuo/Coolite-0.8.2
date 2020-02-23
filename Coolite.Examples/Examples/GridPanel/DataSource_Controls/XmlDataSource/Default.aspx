<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Store1.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit - GridPanel with XmlDataSource</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="../../Shared/Plants.xml"
            TransformFile="../../Shared/Plants.xsl" />
            
        <ext:Store runat="server" ID="Store1" DataSourceID="XmlDataSource1">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Common" />
                        <ext:RecordField Name="Botanical" />
                        <ext:RecordField Name="Light" />
                        <ext:RecordField Name="Price" Type="Float" />
                        <ext:RecordField Name="Availability" Type="Date" DateFormat="m/d/Y" />
                        <ext:RecordField Name="Indoor" Type="Boolean" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="Common" Direction="ASC" />
        </ext:Store>
        
        <ext:GridPanel runat="server" ID="GridPanel1" Width="600" Height="300" AutoExpandColumn="Common"
            Title="Plants" Frame="true" StoreID="Store1">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:Column ColumnID="Common" Header="Common Name" DataIndex="Common" Width="220"
                        Sortable="true" />
                    <ext:Column Header="Light" DataIndex="Light" Width="130" Sortable="true" />
                    <ext:Column Header="Price" DataIndex="Price" Width="70" Align="right" Sortable="true" />
                    <ext:Column Header="Available" DataIndex="Availability" Width="95" Sortable="true">
                        <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                    </ext:Column>
                    <ext:Column Header="Indoor?" DataIndex="Indoor" Width="55" Sortable="true" />
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
            </SelectionModel>
            <BottomBar>
                <ext:PagingToolBar ID="PagingToolBar1" runat="server" PageSize="10" />
            </BottomBar>
        </ext:GridPanel>    
    </form>
</body>
</html>
