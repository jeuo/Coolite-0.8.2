﻿<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Register src="WindowEditor.ascx" tagname="WindowEditor" tagprefix="uc1" %>
<%@ Import Namespace="Coolite.Examples.Code.Northwind"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Init(object sender, EventArgs e)
        {
            this.WindowEditor1.GridStore = this.Store1;
        }
        
        protected void ShowDetails(object sender, AjaxEventArgs e)
        {
            string id = e.ExtraParams["id"];
            this.WindowEditor1.SetEmployee(Employee.GetEmployee(int.Parse(id)));
            this.WindowEditor1.Show();            
        }

        protected void Store1_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            int count;
            Store1.DataSource = Employee.GetEmployeesFilter(e.Start, e.Limit, e.Sort, e.Dir.ToString(), out count);
            e.TotalCount = count;

            Store1.DataBind();
        }
    </script>
<script type="text/javascript">                
        var employeeDetailsRender = function () {
            return '<img class="imgEdit" ext:qtip="Click to view/edit additional details" style="cursor:pointer;" src="vcard_edit.png" />';
        }

        var cellClick = function (grid, rowIndex, columnIndex, e) {
            var t = e.getTarget();
            var record = grid.getStore().getAt(rowIndex);  // Get the Record
            var columnId = grid.getColumnModel().getColumnId(columnIndex); // Get column id

            if (t.className == 'imgEdit' && columnId == 'Details') {
                //the ajax event allowed
                return true;
            }
            
            //forbidden
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Custom window editor (ajax version)</h1>
        
        <p>For view/edit additional properties please click on the image in last column</p>
        
        <ext:Store runat="server" ID="Store1" AutoLoad="true" RemoteSort="true" OnRefreshData="Store1_RefreshData">
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader ReaderID="EmployeeID">
                    <Fields>
                        <ext:RecordField Name="EmployeeID" />
                        <ext:RecordField Name="FirstName" />
                        <ext:RecordField Name="LastName" />
                        <ext:RecordField Name="Title" />                        
                        <ext:RecordField Name="BirthDate" Type="Date" DateFormat="Y-m-dTh:i:s" />                       
                        <ext:RecordField Name="City" />
                        <ext:RecordField Name="Address" />                  
                    </Fields>
                </ext:JsonReader>
            </Reader>
            
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="5" Mode="Raw" />
            </AutoLoadParams>
            
            <SortInfo Field="LastName" Direction="ASC" />
            
            <Listeners> 
                <LoadException Handler="Ext.MessageBox.alert('Load failed', response.statusText);" />                            
            </Listeners>
        </ext:Store>
        
        <ext:GridPanel 
            runat="server" 
            ID="GridPanel1" 
            StoreID="Store1" 
            Title="Employees" 
            Header="false" 
            Height="175" 
            AutoExpandColumn="Employee">
            <ColumnModel ID="ColumnModel1" runat="server">
			    <Columns>
                    <ext:Column ColumnID="Employee" Header="Full Name" DataIndex="LastName" Sortable="true">  
                        <Renderer Handler="return '<b>'+record.data['LastName']+'</b>,'+record.data['FirstName']" />                 
                    </ext:Column>
                    
                    <ext:Column Header="Title" DataIndex="Title" Sortable="true" Width="150" />
                        
                    <ext:Column Header="Birth Date" DataIndex="BirthDate" Sortable="true">
                        <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />                        
                    </ext:Column>
                                        
                    <ext:Column Header="City" DataIndex="City" Sortable="true" Width="100" />
                    <ext:Column Header="Address" DataIndex="Address" Sortable="true" Width="250" />
                    
                    <ext:Column 
                        ColumnID="Details" 
                        Header="Details" 
                        Width="50" 
                        Align="Center" 
                        Fixed="true" 
                        MenuDisabled="true" 
                        Resizable="false">
                        <Renderer Fn="employeeDetailsRender" />                    
                    </ext:Column>
			    </Columns>
            </ColumnModel>
            
            <LoadMask ShowMask="true" />
            <SaveMask ShowMask="true" />
            
            <BottomBar>
                <ext:PagingToolBar ID="PagingToolBar1" runat="server" 
                    PageSize="5" 
                    DisplayInfo="true"
                    DisplayMsg="Displaying employees {0} - {1} of {2}"
                    EmptyMsg="No employees to display"                
                    />
            </BottomBar>
            
            <Listeners>
                <CellClick Fn="cellClick" />
            </Listeners>       
            
            <AjaxEvents>
                <CellClick OnEvent="ShowDetails" Failure="Ext.MessageBox.alert('Load failed', 'Error during ajax event!');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{GridPanel1}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="params[0].getStore().getAt(params[1]).id" Mode="Raw" />
                    </ExtraParams>
                </CellClick>
            </AjaxEvents>    
        </ext:GridPanel>
        
        <uc1:WindowEditor ID="WindowEditor1" runat="server" />
    </form>
</body>
</html>
