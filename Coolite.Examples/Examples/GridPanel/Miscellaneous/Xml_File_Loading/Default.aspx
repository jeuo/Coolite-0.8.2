<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XML File Loading</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true">
        <Proxy>
            <ext:HttpProxy runat="server" Url="../../Shared/Plants.xml" />
        </Proxy>
        <Reader>
            <ext:XmlReader Record="plant">
                <Fields>
                    <ext:RecordField Name="common" />
                    <ext:RecordField Name="botanical" />
                    <ext:RecordField Name="light" />
                    <ext:RecordField Name="price" Type="Float" />
                    <ext:RecordField Name="availability" Type="Date" />
                    <ext:RecordField Name="indoor" Type="Boolean" />
                </Fields>
            </ext:XmlReader>
        </Reader>
        <SortInfo Field="common" Direction="ASC" />
    </ext:Store>
    
    <ext:GridPanel 
        runat="server" 
        Width="600" 
        Height="300" 
        AutoExpandColumn="common" 
        Title="Plants" 
        Frame="true" 
        StoreID="Store1">
        <ColumnModel runat="server">
		<Columns>
            <ext:Column ColumnID="common" Header="Common Name" DataIndex="common" Width="220" Sortable="true" />
            <ext:Column Header="Light" DataIndex="light" Width="130" Sortable="true" />
            <ext:Column Header="Price" DataIndex="price" Width="70" Align="right" Sortable="true">
                <Renderer Format="UsMoney" />
            </ext:Column>
            <ext:Column Header="Available" DataIndex="availability" Width="95" Sortable="true">
                <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
            </ext:Column>
            <ext:Column Header="Indoor?" DataIndex="indoor" Width="55" Sortable="true" />
		</Columns>
        </ColumnModel>
        <LoadMask ShowMask="true" />        
    </ext:GridPanel>
</body>
</html>