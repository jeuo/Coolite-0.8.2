<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="System.Xml"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example</title>
    
    <script runat="server">
        public class Country
        {
            public string Name { get; set; }
        }

        protected void SaveSelection(object sender, BeforeStoreChangedEventArgs e)
        {
            ChangeRecords<Country> records = e.DataHandler.ObjectData<Country>();
            List<Country> countries = records.Created;
        }
    </script>
    
    <script type="text/javascript">
        var CountrySelector = {
            swapRecords: function(source, destination, records) {
                Window1.body.mask('Working...');
                if (destination.id == 'GridPanel2') {
                    for (var i = 0; i < records.length; i++) {
                        destination.addRecord(records[i].data);
                    }
                }
                else {
                    for (var i = 0; i < records.length; i++) {
                        var record = new destination.record(records[i].data);
                        destination.store.addSorted(record);
                    }
                }
                Window1.body.unmask();
            },

            add: function(source, destination) {
                source = source || GridPanel1;
                destination = destination || GridPanel2;
                if (source.hasSelection()) {
                    var records = source.selModel.getSelections();
                    this.swapRecords(source, destination, records);
                    source.deleteSelected();
                }
            },
            addAll: function(source, destination) {
                source = source || GridPanel1;
                destination = destination || GridPanel2;
                this.swapRecords(source, destination, source.store.getRange());
                source.store.removeAll();
            },

            remove: function(source, destination) {
                this.add(destination, source);
            },
            removeAll: function(source, destination) {
                this.addAll(destination, source);
            }
        };
    </script>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
        
        <ext:Label ID="Label1" runat="server" />
        
        <asp:XmlDataSource 
            ID="XmlDataSource1" 
            runat="server" 
            DataFile="Countries.xml" 
            TransformFile="Countries.xsl"
            />
         
        <ext:Store runat="server" ID="Store1" DataSourceID="XmlDataSource1">
            <SortInfo Field="Name" Direction="ASC" />            
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Name" />                        
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store runat="server" ID="Store2" OnBeforeStoreChanged="SaveSelection">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Name" />                        
                    </Fields>
                </ext:JsonReader>
            </Reader>         
        </ext:Store>
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            ShowOnLoad="true"
            Closable="false"
            Height="553"
            Width="700"
            Icon="WorldAdd"
            Title="Country Selector"
            BodyStyle="padding:5px;"
            BodyBorder="false">            
            <Body>
                <ext:ColumnLayout runat="server" FitHeight="true">
                    <ext:LayoutColumn ColumnWidth="0.5">
                       <ext:GridPanel 
                            runat="server" 
                            ID="GridPanel1" 
                            EnableDragDrop="true"
                            AutoExpandColumn="Country"
                            StoreID="Store1">
                            <ColumnModel runat="server">
	                            <Columns>
                                    <ext:Column ColumnID="Country" Header="Available Countries" DataIndex="Name" Sortable="true" />                   
	                            </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" />
                                <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" />--%>
                            </SelectionModel> 
                            <Plugins>
                                <ext:GridFilters ID="GridFilters1" runat="server" Local="true">
                                    <Filters>
                                        <ext:StringFilter DataIndex="Name" />
                                    </Filters>
                                </ext:GridFilters>
                            </Plugins>
                        </ext:GridPanel>
                    </ext:LayoutColumn>
                    <ext:LayoutColumn>
                        <ext:Panel ID="Panel2" runat="server" Width="35" BodyStyle="background-color: transparent;" Border="false">
                            <Body>
                                <ext:AnchorLayout runat="server">
                                    <ext:Anchor Vertical="40%">
                                        <ext:Panel runat="server" Border="false" BodyStyle="background-color: transparent;" />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:Panel ID="Panel1" runat="server" Border="false" BodyStyle="padding:5px;background-color: transparent;">
                                            <Body>
                                                <ext:Button runat="server" Icon="ResultsetNext" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.add();" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Add" Html="Add Selected Rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetLast" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.addAll();" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Add all" Html="Add All Rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetPrevious" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.remove(GridPanel1, GridPanel2);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Remove" Html="Remove Selected Rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetFirst" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.removeAll(GridPanel1, GridPanel2);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Remove all" Html="Remove All Rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                            </Body>
                                        </ext:Panel>
                                    </ext:Anchor>
                                </ext:AnchorLayout>
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                    <ext:LayoutColumn ColumnWidth="0.5">
                        <ext:GridPanel 
                            runat="server" 
                            ID="GridPanel2" 
                            EnableDragDrop="false"
                            AutoExpandColumn="Country" 
                            StoreID="Store2">
                            <Listeners>
                            </Listeners>
                            <ColumnModel ID="ColumnModel1" runat="server">
	                            <Columns>
                                    <ext:Column ColumnID="Country" Header="Selected Countries" DataIndex="Name" Sortable="true" />                   
	                            </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" />
                            </SelectionModel>  
                            <SaveMask ShowMask="true" />
                        </ext:GridPanel>
                    </ext:LayoutColumn>
                </ext:ColumnLayout>              
            </Body>  
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Save Selected Countries" Icon="Disk">
                    <Listeners>
                        <Click Handler="#{GridPanel2}.save();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="Cancel" Icon="Cancel">
                    <Listeners>
                        <Click Handler="CountrySelector.removeAll(GridPanel1, GridPanel2);" />
                    </Listeners>
                </ext:Button>
            </Buttons>      
        </ext:Window>
    </form>
</body>
</html>