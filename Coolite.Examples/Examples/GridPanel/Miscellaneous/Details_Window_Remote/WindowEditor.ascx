<%@ Control Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Import Namespace="Coolite.Examples.Code.Northwind" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.EmployeesStore.DataSource = Employee.GetAll();
            this.EmployeesStore.DataBind();
        }
    }

    public Store GridStore
    {
        get;
        set;
    }

    public void Show()
    {
        this.EmployeeDetailsWindow.Show();
    }

    public void SetEmployee(Employee empl)
    {
        this.EmployeeID1.Text = empl.EmployeeID.ToString();
        this.EmployeeID2.Text = empl.EmployeeID.ToString();

        //Company
        this.FirstName.Text = empl.FirstName;
        this.LastName.Text = empl.LastName;
        this.Title.Text = empl.Title;
        this.ReportsTo.SetValue(empl.ReportsTo);
        this.HireDate.SelectedDate = empl.HireDate.HasValue ? empl.HireDate.Value : DateTime.MinValue;
        this.Extension.Text = empl.Extension;

        //Personal
        this.Address.Text = empl.Address;
        this.City.Text = empl.City;
        this.PostCode.Text = empl.PostalCode;
        this.HomePhone.Text = empl.HomePhone;
        this.TitleCourt.Text = empl.TitleOfCourtesy;
        this.BirthDate.SelectedDate = empl.BirthDate.HasValue ? empl.BirthDate.Value : DateTime.MinValue;
        this.Region.Text = empl.Region;
        this.Country.Text = empl.Country;
        this.Note.Text = empl.Notes;
    }

    protected void SaveEmployee(object sender, AjaxEventArgs e)
    {
        NorthwindDataContext db = new NorthwindDataContext();
        int id = int.Parse(e.ExtraParams["id"]);

        Employee empl = Employee.GetEmployee(id, db);

        //Company
        empl.FirstName = this.FirstName.Text;
        empl.LastName = this.LastName.Text;
        empl.Title = this.Title.Text;

        if (!string.IsNullOrEmpty(this.ReportsTo.SelectedItem.Value))
        {
            empl.ReportsTo = int.Parse(this.ReportsTo.SelectedItem.Value);
        }
        else
        {
            empl.ReportsTo = null;
        }

        empl.HireDate = this.HireDate.SelectedDate;
        empl.Extension = this.Extension.Text;

        //Personal
        empl.Address = this.Address.Text;
        empl.City = this.City.Text;
        empl.PostalCode = this.PostCode.Text;
        empl.HomePhone = this.HomePhone.Text;
        empl.TitleOfCourtesy = this.TitleCourt.Text;
        empl.BirthDate = this.BirthDate.SelectedDate;
        empl.Region = this.Region.Text;
        empl.Country = this.Country.Text;
        empl.Notes = this.Note.Text;

        db.SubmitChanges();

        this.GridStore.DataBind();
        //this.GridStore.AddScript(string.Format("{0}.reload();", this.GridStore.ClientID));

        this.EmployeeDetailsWindow.Hide();
    }
</script>

<ext:Store runat="server" ID="EmployeesStore" AutoLoad="true">
    <Reader>
        <ext:JsonReader ReaderID="EmployeeID">
            <Fields>
                <ext:RecordField Name="EmployeeID" />
                <ext:RecordField Name="LastName" />
            </Fields>
        </ext:JsonReader>
    </Reader>
</ext:Store>
<ext:Window ID="EmployeeDetailsWindow" runat="server" Icon="Group" Title="Employee Details"
    Width="400" Height="400" AutoShow="false" Modal="true" ShowOnLoad="false">
    <Body>
        <ext:FitLayout runat="server">
            <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Border="false" DeferredRender="false">
                <Tabs>
                    <ext:Tab runat="server" ID="CompanyInfoTab" Title="Company Info" Icon="ChartOrganisation"
                        BodyStyle="padding:5px;">
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField ID="EmployeeID1" runat="server" FieldLabel="Employee ID" Width="250"
                                        Disabled="true" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="FirstName" runat="server" FieldLabel="First Name" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="LastName" runat="server" FieldLabel="Last Name" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="Title" runat="server" FieldLabel="Title" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:ComboBox ID="ReportsTo" runat="server" 
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
                                        Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:DateField ID="HireDate" runat="server" Width="250" FieldLabel="Hire date" Format="yyyy-M-d" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="Extension" runat="server" FieldLabel="Extension" Width="250" />
                                </ext:Anchor>
                            </ext:FormLayout>
                        </Body>
                    </ext:Tab>
                    <ext:Tab ID="PersonalInfoTab" runat="server" Title="Personal Info" Icon="User" BodyStyle="padding:5px;">
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField ID="EmployeeID2" runat="server" FieldLabel="Employee ID" Width="250"
                                        Disabled="true" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="Address" runat="server" FieldLabel="Address" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="City" runat="server" FieldLabel="City" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="PostCode" runat="server" FieldLabel="Post Code" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="HomePhone" runat="server" FieldLabel="Home Phone" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="TitleCourt" runat="server" FieldLabel="Title Of Courtesy" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:DateField ID="BirthDate" runat="server" Width="233" FieldLabel="Birth date"
                                        Format="yyyy-M-d" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="Region" runat="server" FieldLabel="Region" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextField ID="Country" runat="server" FieldLabel="Country" Width="250" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:TextArea ID="Note" runat="server" FieldLabel="Note" Height="50" Width="250" />
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
            <AjaxEvents>
                <Click OnEvent="SaveEmployee" Failure="Ext.MessageBox.alert('Saving failed', 'Error during ajax event');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{EmployeeDetailsWindow}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="#{EmployeeID1}.getValue()" Mode="Raw" />
                    </ExtraParams>
                </Click>
            </AjaxEvents>
        </ext:Button>
        <ext:Button ID="CancelButton" runat="server" Text="Cancel" Icon="Cancel">
            <Listeners>
                <Click Handler="#{EmployeeDetailsWindow}.hide(null);" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
