<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coolite Toolkit Example - Group Commands</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function prepareCommand(grid, command, record, row) {
            // you can prepare group command
            if (command.command == 'Delete' && record.data.Price < 5) {
                command.hidden = true;
                command.hideMode = 'visibility'; //you can try 'display' also                 
            }
        }
        
        function prepareGroupCommand(grid, command, groupId, group) {
            // you can prepare group command
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>ImageCommandColumn is a simpler and faster version of CommanColumn</h1>

        <ext:Store runat="server" ID="Store1" AutoLoad="true" GroupField="Light">
            <Proxy>
                <ext:HttpProxy Method="POST" Url="../../Shared/PlantService.asmx/Plants" />
            </Proxy>
            <Reader>
                <ext:XmlReader Record="Plant">
                    <Fields>
                        <ext:RecordField Name="Common" />
                        <ext:RecordField Name="Botanical" />
                        <ext:RecordField Name="Zone" Type="Int" />
                        <ext:RecordField Name="ColorCode" />
                        <ext:RecordField Name="Light" />
                        <ext:RecordField Name="Price" Type="Float" />
                        <ext:RecordField Name="Availability" Type="Date" />
                        <ext:RecordField Name="Indoor" Type="Boolean" />
                    </Fields>
                </ext:XmlReader>
            </Reader>
            <SortInfo Field="Common" Direction="ASC" />
        </ext:Store>
        
        <ext:GridPanel
            ID="GridPanel1"
            runat="server" 
            Collapsible="true" 
            Width="600" 
            Height="350" 
            AutoExpandColumn="Common" 
            Title="Plants" 
            Frame="true" 
            StoreID="Store1">
            <ColumnModel runat="server">
                <Columns>
                    <ext:Column ColumnID="Common" Header="Common Name" DataIndex="Common" Width="220" Sortable="true" />
                    <ext:Column Header="Light" DataIndex="Light" Width="130" Sortable="true" />
                    <ext:Column Header="Price" DataIndex="Price" Width="70" Align="right" Sortable="true" Groupable="false">
                        <Renderer Format="UsMoney" />
                    </ext:Column>
                    <ext:Column Header="Available" DataIndex="Availability" Width="95" Sortable="true" Groupable="false">
                        <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                    </ext:Column>
                    <ext:Column Header="Indoor?" DataIndex="Indoor" Width="55" Sortable="true" />
                    <ext:ImageCommandColumn Width="110">
                        <Commands>
                            <ext:ImageCommand CommandName="Delete" Icon="Delete" Text="Delete">
                                <ToolTip Text="Delete" />
                            </ext:ImageCommand>
                            <ext:ImageCommand CommandName="Edit" Icon="TableEdit" Text="Edit">
                                <ToolTip Text="Edit" />
                            </ext:ImageCommand>
                        </Commands>
                        
                        <GroupCommands>
                            <ext:GroupImageCommand CommandName="Delete" Icon="Delete" Text="Delete">
                                <ToolTip Text="Delete" />
                            </ext:GroupImageCommand>
                            <ext:GroupImageCommand CommandName="Edit" Icon="TableEdit" Text="Edit">
                                <ToolTip Text="Edit" />
                            </ext:GroupImageCommand>
                            <ext:GroupImageCommand CommandName="Chart" Icon="ChartBar" RightAlign="true">
                                <ToolTip Text="Chart" />
                            </ext:GroupImageCommand>
                        </GroupCommands>
                        
                        <PrepareCommand Fn="prepareCommand" />
                        <PrepareGroupCommand Fn="prepareGroupCommand" />
                    </ext:ImageCommandColumn>
                </Columns>
            </ColumnModel>
            
            <Listeners>
                <Command Handler="Ext.Msg.alert(command, record.data.Common);" />
                <GroupCommand Handler="Ext.Msg.alert(command, 'Group id: '+groupId+'<br/>Count - ' + records.length);" />
            </Listeners>
            
            <LoadMask ShowMask="true" />
            
            <SelectionModel>
                <ext:RowSelectionModel runat="server" />
            </SelectionModel>
            
            <View>
                <ext:GroupingView  
                    ID="GroupingView1"
                    HideGroupedColumn="true"
                    runat="server" 
                    ForceFit="true"
                    GroupTextTpl='{text} ({[values.rs.length]} {[values.rs.length > 1 ? "Items" : "Item"]})'
                    EnableRowBody="true">
                    <GetRowClass Handler="var d = record.data; rowParams.body = String.format('<div style=\'padding:0 5px 5px 5px;\'>The {0} [{1}] requires light conditions of <i>{2}</i>.<br /><b>Price: {3}</b></div>', d.Common, d.Botanical, d.Light, Ext.util.Format.usMoney(d.Price));" />
                </ext:GroupingView>
            </View>            
        </ext:GridPanel>
    </form>
</body>
</html>
