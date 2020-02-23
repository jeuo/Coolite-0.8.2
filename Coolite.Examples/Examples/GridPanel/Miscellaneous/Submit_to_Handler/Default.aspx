<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>
<%@ Import Namespace="System.Xml"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example</title>
    
    <script type="text/javascript">
        var CountrySelector = {
            add: function(source, destination) {
                source = source || GridPanel1;
                destination = destination || GridPanel2;
                if (source.hasSelection()) {
                    destination.store.add(source.selModel.getSelections());
                    source.deleteSelected();
                }
            },
            addAll: function(source, destination) {
                source = source || GridPanel1;
                destination = destination || GridPanel2;
                destination.store.add(source.store.getRange());
                source.store.removeAll();
            },
            addByName: function(name) {
                if (!Ext.isEmpty(name)) {
                    var result = Store1.query("Name", name);
                    if (!Ext.isEmpty(result.items)) {
                        GridPanel2.store.add(result.items[0]);
                        GridPanel1.store.remove(result.items[0]);
                    }
                }
            },
            addByNames: function(name) {
                for (var i = 0; i < name.length; i++) {
                    this.addByName(name[i]);
                }
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
        
        <ext:Store runat="server" ID="Store2">
            <UpdateProxy>
                <ext:HttpWriteProxy Method="POST" Url="SubmitHandler.ashx" />
            </UpdateProxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="Name" />                        
                    </Fields>
                </ext:JsonReader>
            </Reader>  
            <Listeners>
                <Save Handler="Ext.Msg.alert('Submit','Submit successful');" />
                <SaveException Handler="Ext.Msg.alert('Submit','Submit failure: ' + e.message);" />
            </Listeners>       
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
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:Button ID="Button3" runat="server" Text="Options">
                            <Menu>
                                <ext:Menu ID="Menu1" runat="server">
                                    <Items>
                                        <ext:MenuItem runat="server" Text="Select All">
                                            <Listeners>
                                                <Click Handler="CountrySelector.addAll(GridPanel1, GridPanel2);" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem runat="server" Text="UnSelect All">
                                            <Listeners>
                                                <Click Handler="CountrySelector.removeAll(GridPanel1, GridPanel2);" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="MenuItem1" runat="server" Text="Regions">
                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="Asia">
                                                            <Listeners>
                                                                <Click Handler="CountrySelector.addByNames(['China', 'Japan', 'Taiwan', 'South Korea']);" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="Europe">
                                                            <Listeners>
                                                                <Click Handler="CountrySelector.addByNames(['United Kingdom', 'France', 'Germany', 'Spain', 'Switzerland', 'Italy', 'Austria', 'Belgium']);" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                        <ext:MenuItem runat="server" Text="North America">
                                                            <Listeners>
                                                                <Click Handler="CountrySelector.addByNames(['Canada', 'United States', 'Mexico']);" />
                                                            </Listeners>
                                                        </ext:MenuItem>
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                            <Listeners>
                                                <Click Handler="CountrySelector.removeAll(GridPanel1, GridPanel2);" />
                                            </Listeners>
                                        </ext:MenuItem>
                                    </Items>
                                </ext:Menu>
                            </Menu>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Body>
                <ext:ColumnLayout runat="server" FitHeight="true">
                    <ext:LayoutColumn ColumnWidth="0.5">
                       <ext:GridPanel 
                            runat="server" 
                            ID="GridPanel1" 
                            EnableDragDrop="false"
                            AutoExpandColumn="Country"
                            StoreID="Store1">
                            <ColumnModel runat="server">
	                            <Columns>
                                    <ext:Column ColumnID="Country" Header="Available Countries" DataIndex="Name" Sortable="true" />                   
	                            </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" />
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
                                                        <ext:ToolTip runat="server" Title="Add" Html="Add selected rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetLast" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.addAll();" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Add all" Html="Add all rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetPrevious" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.remove(GridPanel1, GridPanel2);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Remove" Html="Remove selected rows" />
                                                    </ToolTips>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="ResultsetFirst" StyleSpec="margin-bottom:2px;">
                                                    <Listeners>
                                                        <Click Handler="CountrySelector.removeAll(GridPanel1, GridPanel2);" />
                                                    </Listeners>
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Title="Remove all" Html="Remove all rows" />
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
                        <Click Handler="#{GridPanel2}.submitData();" />
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