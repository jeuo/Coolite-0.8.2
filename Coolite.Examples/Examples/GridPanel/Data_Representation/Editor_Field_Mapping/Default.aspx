﻿<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

        protected void Page_Load(object sender, EventArgs e)
        {
            StoreCombo.DataSource = Department.GetAll();
            StoreCombo.DataBind();

            Store1.DataSource = Employee.GetAll();
            Store1.DataBind();
        }

        public class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public Department Department { get; set; }

            public int DepartmentId
            {
                get { return this.Department != null ? this.Department.ID : -1; }
            }

            public static List<Employee> GetAll()
            {
                return new List<Employee>
                           {
                               new Employee
                                   {
                                       ID = 1,
                                       Name = "Nancy",
                                       Surname = "Davolio",
                                       Department = Department.GetAll()[0]
                                   },
                               new Employee
                                   {
                                       ID = 2,
                                       Name = "Andrew",
                                       Surname = "Fuller",
                                       Department = Department.GetAll()[2]
                                   }
                           };
            }
        }

        public class Department
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public static List<Department> GetAll()
            {
                return new List<Department>
                           {
                               new Department {ID = 1, Name = "Department A"},
                               new Department {ID = 2, Name = "Department B"},
                               new Department {ID = 3, Name = "Department C"}
                           };
            }
        }


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example - Field Mapping</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var departmentRenderer = function(value) {
            var r = StoreCombo.getById(value);

            if (Ext.isEmpty(r)) {
                return "";
            }

            return r.data.Name;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Store ID="StoreCombo" runat="server">
            <Reader>
                <ext:JsonReader ReaderID="ID">
                    <Fields>
                        <ext:RecordField Name="ID" />
                        <ext:RecordField Name="Name" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="Store1" runat="server">
            <Reader>
                <ext:JsonReader ReaderID="ID">
                    <Fields>
                        <ext:RecordField Name="ID" Type="Int" />
                        <ext:RecordField Name="Name" />
                        <ext:RecordField Name="Surname" />
                        <ext:RecordField Name="DepartmentId" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:GridPanel ID="GridPanel1" EnableViewState="true" AutoHeight="true" runat="server"
            StoreID="Store1" Title="List" Icon="Application">
            <ColumnModel ID="ColumnModel1" runat="server">
                
                <Columns>
                    <ext:Column Header="ID" Sortable="true" DataIndex="ID" />
                    <ext:Column Header="NAME" Sortable="true" DataIndex="Name" />
                    <ext:Column Header="SURNAME" Sortable="true" DataIndex="Surname" />
                    <ext:Column DataIndex="DepartmentId" Header="Department" Width="240">
                        <Renderer Fn="departmentRenderer" />
                        <Editor>                        
                            <ext:ComboBox ID="cbDepartment" runat="server" Shadow="Drop" Mode="Local" TriggerAction="All" ForceSelection="true"
                                StoreID="StoreCombo" DisplayField="Name" ValueField="ID">
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" />
            </SelectionModel>
            <LoadMask Msg="Loading" ShowMask="true" />
        </ext:GridPanel>
    </form>
</body>
</html>
