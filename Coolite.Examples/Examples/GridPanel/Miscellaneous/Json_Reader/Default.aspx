<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AJAX request to HttpHander returns Json - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="../../Shared/JsonHandler.ashx" />
        </Proxy>
        <Reader>
            <ext:JsonReader Root="plants">
                <Fields>
                    <ext:RecordField Name="Common" Type="String" />
                    <ext:RecordField Name="Botanical" Type="String" />
                    <ext:RecordField Name="Light" />
                    <ext:RecordField Name="Price" Type="Float" />
                    <ext:RecordField Name="Availability" Type="Date" />
                    <ext:RecordField Name="Indoor" Type="Boolean" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <SortInfo Field="Common" Direction="ASC" />
    </ext:Store>
    
    <ext:Window 
        runat="server" 
        Collapsible="true" 
        Title="Plant Summary" 
        Height="500" 
        Width="1000">
        <Body>
            <ext:FitLayout ID="FitLayout1" runat="server">
                <ext:GridPanel 
                    ID="GridPanel1" 
                    runat="server" 
                    AutoExpandColumn="Common" 
                    Title="Plants" 
                    Frame="false" 
                    StoreID="Store1">
                    <ColumnModel ID="ColumnModel1" runat="server">
                    <Columns>
                        <ext:Column ColumnID="Common" Header="Common Name" DataIndex="Common" Width="220" Sortable="true" />
                        <ext:Column Header="Light" DataIndex="Light" Width="130" Sortable="true" />
                        <ext:Column Header="Price" DataIndex="Price" Width="70" Align="right" Sortable="true" />
                        <ext:Column Header="Available" DataIndex="Availability" Width="95" Sortable="true" />
                        <ext:Column Header="Indoor?" DataIndex="Indoor" Width="55" Sortable="true" />
                    </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" />
                </ext:GridPanel>                            
            </ext:FitLayout>
        </Body>
    </ext:Window>    
</body>
</html>