<%@ Control Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<%@ Import Namespace="Coolite.Examples.Code.Northwind"%>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        this.EmployeesStore.DataSource = Employee.GetAll();
        this.EmployeesStore.DataBind();
    }
</script>

<script type="text/javascript">
    var employeeRecord;
    
    var openEmployeeDetails = function (record, animTrg) {
        employeeRecord = record;
        var window = <%= EmployeeDetailsWindow.ClientID %>;
        window.setTitle(String.format('Employee Details: {0}, {1}',record.data['LastName'],record.data['FirstName']));
        
        <%= EmployeeID1.ClientID %>.setValue(record.id);
        <%= EmployeeID2.ClientID %>.setValue(record.id);
        
        //Company        
        
        <%= FirstName.ClientID %>.setValue(record.data['FirstName']);
        <%= LastName.ClientID %>.setValue(record.data['LastName']);
        <%= Title.ClientID %>.setValue(record.data['Title']);
        <%= HireDate.ClientID %>.setValue(record.data['HireDate']);
        <%= Extension.ClientID %>.setValue(record.data['Extension']);
        <%= ReportsTo.ClientID %>.setValue(record.data['ReportsTo']);
        
        //Personal
        <%= Address.ClientID %>.setValue(record.data['Address']);
        <%= City.ClientID %>.setValue(record.data['City']);
        <%= PostCode.ClientID %>.setValue(record.data['PostalCode']);
        <%= HomePhone.ClientID %>.setValue(record.data['Homephone']);
        <%= TitleCourt.ClientID %>.setValue(record.data['TitleOfCourtesy']);
        <%= BirthDate.ClientID %>.setValue(record.data['BirthDate']);
        <%= Region.ClientID %>.setValue(record.data['Region']);
        <%= Country.ClientID %>.setValue(record.data['Country']);
        <%= Note.ClientID %>.setValue(record.data['Notes']);

        window.show(animTrg);
    }
    
    var saveEmployee = function () {
        //Company
        employeeRecord.set('FirstName',<%= FirstName.ClientID %>.getValue());
        employeeRecord.set('LastName',<%= LastName.ClientID %>.getValue());
        employeeRecord.set('Title',<%= Title.ClientID %>.getValue());
        employeeRecord.set('HireDate',<%= HireDate.ClientID %>.getValue());
        employeeRecord.set('Extension',<%= Extension.ClientID %>.getValue());        
        employeeRecord.set('ReportsTo',<%= ReportsTo.ClientID %>.getValue());
        
        
        //Personal
        employeeRecord.set('Address',<%= Address.ClientID %>.getValue());
        employeeRecord.set('City',<%= City.ClientID %>.getValue());
        employeeRecord.set('PostalCode',<%= PostCode.ClientID %>.getValue());
        employeeRecord.set('Homephone',<%= HomePhone.ClientID %>.getValue());
        employeeRecord.set('TitleOfCourtesy',<%= TitleCourt.ClientID %>.getValue());
        employeeRecord.set('BirthDate',<%= BirthDate.ClientID %>.getValue());
        employeeRecord.set('Region',<%= Region.ClientID %>.getValue());
        employeeRecord.set('Country',<%= Country.ClientID %>.getValue());
        employeeRecord.set('Notes',<%= Note.ClientID %>.getValue());
        
        <%= EmployeeDetailsWindow.ClientID %>.hide(null);
    }
</script>

<ext:Store ID="EmployeesStore" runat="server" AutoLoad="true">
    <Reader>
        <ext:JsonReader ReaderID="EmployeeID">
            <Fields>
                <ext:RecordField Name="EmployeeID" />
                <ext:RecordField Name="LastName" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>

<ext:Window 
    ID="EmployeeDetailsWindow" 
    runat="server" 
    Icon="Group" 
    Title="Employee Details" 
    Width="400" 
    Height="400" 
    AutoShow="false" 
    Modal="true"
    ShowOnLoad="false">
    <Body>
        <ext:FitLayout runat="server">
            <ext:TabPanel runat="server" ActiveTabIndex="0" Border="false">
                <Tabs>
                    <ext:Tab 
                        ID="CompanyInfoTab" 
                        runat="server" 
                        Title="Company Info" 
                        Icon="ChartOrganisation"
                        BodyStyle="padding:5px;">
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="EmployeeID1"
                                        runat="server" 
                                        FieldLabel="Employee ID"
                                        Width="250"
                                        Disabled="true"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="FirstName"
                                        runat="server" 
                                        FieldLabel="First Name"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="LastName"
                                        runat="server" 
                                        FieldLabel="Last Name"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="Title"
                                        runat="server" 
                                        FieldLabel="Title"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:ComboBox 
                                        ID="ReportsTo"
                                        runat="server" 
                                        StoreID="EmployeesStore"
                                        FieldLabel="Reports to" 
                                        AllowBlank="true"
                                        DisplayField="LastName"
                                        ValueField="EmployeeID"
                                        TypeAhead="true" 
                                        Mode="Local"
                                        ForceSelection="true"
                                        TriggerAction="All"
                                        EmptyText="Select an employee..."
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:DateField 
                                        ID="HireDate" 
                                        runat="server" 
                                        Width="250" 
                                        FieldLabel="Hire date"
                                        Format="yyyy-m-d"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        runat="server" 
                                        ID="Extension"
                                        FieldLabel="Extension"
                                        Width="250"
                                        />
                                </ext:Anchor>
                            </ext:FormLayout>
                        </Body>
                    </ext:Tab>
                    <ext:Tab 
                        ID="PersonalInfoTab" 
                        runat="server" 
                        Title="Personal Info" 
                        Icon="User"
                        BodyStyle="padding:5px;">
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="EmployeeID2"
                                        runat="server" 
                                        FieldLabel="Employee ID"
                                        Width="250"
                                        Disabled="true"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="Address"
                                        runat="server" 
                                        FieldLabel="Address"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="City" 
                                        runat="server" 
                                        FieldLabel="City"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="PostCode"
                                        runat="server" 
                                        FieldLabel="Post Code"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="HomePhone" 
                                        runat="server" 
                                        FieldLabel="Home Phone"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="TitleCourt" 
                                        runat="server" 
                                        FieldLabel="Title Of Courtesy"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:DateField 
                                        ID="BirthDate" 
                                        runat="server" 
                                        Width="233" 
                                        FieldLabel="Birth date"
                                        Format="yyyy-m-d"
                                        />
                                </ext:Anchor>
                                    
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="Region" 
                                        runat="server" 
                                        FieldLabel="Region"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextField 
                                        ID="Country" 
                                        runat="server" 
                                        FieldLabel="Country"
                                        Width="250"
                                        />
                                </ext:Anchor>
                                
                                <ext:Anchor>
                                    <ext:TextArea 
                                        ID="Note" 
                                        runat="server" 
                                        FieldLabel="Note"
                                        Height="50" 
                                        Width="250"
                                        />
                                </ext:Anchor>
                            </ext:FormLayout>                            
                        </Body>
                    </ext:Tab>
                </Tabs>                
            </ext:TabPanel>
        </ext:FitLayout>
    </Body>
    <Buttons>
        <ext:Button ID="SaveButton" runat="server" Text="Save" Icon="Disk">
           <Listeners>
                <Click Handler="saveEmployee();" />
            </Listeners>
        </ext:Button>
        <ext:Button ID="CancelButton" runat="server" Text="Cancel" Icon="Cancel">
            <Listeners>
                <Click Handler="#{EmployeeDetailsWindow}.hide(null);" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
