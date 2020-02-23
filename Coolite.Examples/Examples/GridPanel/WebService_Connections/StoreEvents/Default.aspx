<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data.Linq"%>
<%@ Import Namespace="Coolite.Examples.Code.Northwind"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>The CRUD Example</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        private NorthwindDataContext db;
        protected void Store1_BeforeChanged(object sender, BeforeStoreChangedEventArgs e)
        {
            this.db = new NorthwindDataContext();
        }

        //This handler will be calling for each inserted record
        protected void Store1_BeforeInserted(object sender, BeforeRecordInsertedEventArgs e)
        {
            Supplier supplier = e.Object<Supplier>();
            db.Suppliers.InsertOnSubmit(supplier);
        }

        //This handler will be calling for each changed record
        protected void Store1_BeforeUpdated(object sender, BeforeRecordUpdatedEventArgs e)
        {
            Supplier supplier = e.Object<Supplier>();
            this.db.Suppliers.Attach(supplier);
            this.db.Refresh(RefreshMode.KeepCurrentValues, supplier);
        }

        //This handler will be calling for each deleted record
        protected void Store1_BeforeDeleted(object sender, BeforeRecordDeletedEventArgs e)
        {
            Supplier supplier = e.Object<Supplier>();
            this.db.Suppliers.Attach(supplier);
            this.db.Suppliers.DeleteOnSubmit(supplier);
        }

        protected void Store1_AfterChanged(object sender, AfterStoreChangedEventArgs e)
        {
            this.db.SubmitChanges();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true"
            OnBeforeStoreChanged="Store1_BeforeChanged" 
            OnAfterStoreChanged="Store1_AfterChanged"
            OnBeforeRecordInserted="Store1_BeforeInserted"
            OnBeforeRecordUpdated="Store1_BeforeUpdated"
            OnBeforeRecordDeleted="Store1_BeforeDeleted">
        <Proxy>
            <ext:HttpProxy Method="GET" Url="../../Shared/SuppliersService.asmx/GetAllSuppliers" />
        </Proxy>        
        <Reader>
            <ext:XmlReader Record="Supplier" ReaderID="SupplierID">
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
                </Fields>
            </ext:XmlReader>
        </Reader>
        <SortInfo Field="CompanyName" Direction="ASC" />
        <Listeners>
            <LoadException Handler="var e = e || {message: response.responseText}; alert('Load failed: ' + e.message);" />
            <SaveException Handler="alert('save failed');" />
            <CommitDone Handler="alert('commit success');" />
            <CommitFailed Handler="alert('Commit failed\nReason: '+msg)" />
        </Listeners>
    </ext:Store>
    
    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <North>
                    <ext:Panel ID="Panel1" runat="server" Border="false" Height="120" BodyStyle="padding:6px;">
                        <Body>
                            <h1>CRUD Grid Example</h1>
                            <p>Demonstrates how to get data from web-service and save using Store (with store events).</p>                           
                        </Body>
                    </ext:Panel>
                </North>
                <Center>
                     <ext:GridPanel runat="server" ID="GridPanel1" Title="Suppliers" AutoExpandColumn="CompanyName"
                        StoreID="Store1" Icon="Lorry" Frame="true">
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column ColumnID="CompanyName" DataIndex="CompanyName" Header="Company Name">
                                    <Editor>
                                        <ext:TextField ID="TextField1" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="ContactName" Header="Contact Name">
                                    <Editor>
                                        <ext:TextField ID="TextField2" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="ContactTitle" Header="Contact Title">
                                    <Editor>
                                        <ext:TextField ID="TextField3" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="Address" Header="Address">
                                    <Editor>
                                        <ext:TextField ID="TextField4" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="City" Header="City">
                                    <Editor>
                                        <ext:TextField ID="TextField5" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="Region" Header="Region" >
                                    <Editor>
                                        <ext:TextField ID="TextField6" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="PostalCode" Header="Postal Code">
                                    <Editor>
                                        <ext:TextField ID="TextField7" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="Country" Header="Country">
                                    <Editor>
                                        <ext:TextField ID="TextField8" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="Phone" Header="Phone">
                                    <Editor>
                                        <ext:TextField ID="TextField9" runat="server" />
                                    </Editor>
                                </ext:Column>
                                <ext:Column DataIndex="Fax" Header="Fax">
                                    <Editor>
                                        <ext:TextField ID="TextField10" runat="server" />
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        
                        <LoadMask ShowMask="true" />
                        <SaveMask ShowMask="true" />
                        
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" SingleSelect="false" runat="server">
                                <Listeners>
                                    <RowSelect Handler="#{btnDelete}.enable();" />
                                    <RowDeselect Handler="if (!#{GridPanel1}.hasSelection()) {#{btnDelete}.disable();}" />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        
                        <Buttons>
                            <ext:Button ID="Button1" runat="server" Text="Add" AutoPostBack="false" Icon="Add">
                                <Listeners>
                                    <Click Handler="var rowIndex = #{GridPanel1}.addRecord(); #{GridPanel1}.getView().focusRow(rowIndex); #{GridPanel1}.startEditing(rowIndex, 0);" />
                                </Listeners>
                            </ext:Button>
                            
                            <ext:Button ID="Button2" runat="server" Text="Insert" AutoPostBack="false" Icon="Add">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.insertRecord(0, {});#{GridPanel1}.getView().focusRow(0);#{GridPanel1}.startEditing(0, 0);" />
                                </Listeners>
                            </ext:Button>
                            
                            <ext:Button ID="btnDelete" runat="server" Text="Delete" AutoPostBack="false" Icon="Delete" Disabled="true">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.deleteSelected();if (!#{GridPanel1}.hasSelection()) {#{btnDelete}.disable();}" />
                                </Listeners>
                            </ext:Button>
                            
                            <ext:Button ID="Button3" runat="server" Text="Save" AutoPostBack="false" Icon="Disk">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.save();" />
                                </Listeners>
                            </ext:Button>
                            
                            <ext:Button ID="Button4" runat="server" Text="Clear" AutoPostBack="false" Icon="Cancel">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.clear();if (!#{GridPanel1}.hasSelection()) {#{btnDelete}.disable();}" />
                                </Listeners>
                            </ext:Button>
                            
                            <ext:Button ID="Button5" runat="server" Text="Refresh" AutoPostBack="false" Icon="ArrowRefresh">
                                <Listeners>
                                    <Click Handler="#{GridPanel1}.reload();" />
                                </Listeners>
                            </ext:Button>
                        </Buttons>
                    </ext:GridPanel>
                </Center>            
            </ext:BorderLayout>
        </Body>        
    </ext:ViewPort>   
    </form>      
</body>
</html>
