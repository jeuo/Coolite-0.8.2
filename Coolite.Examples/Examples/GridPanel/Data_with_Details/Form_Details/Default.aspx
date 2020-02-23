<%@ Page Language="C#" %>
<%@ Import Namespace="Coolite.Examples.Code.Northwind"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void RowSelect(object sender, AjaxEventArgs e)
    {
        string employeeID = e.ExtraParams["EmployeeID"];

        Employee empl = Employee.GetEmployee(int.Parse(employeeID));

        this.EmployeeID.Text = empl.EmployeeID.ToString();
        this.FirstName.Text = empl.FirstName;
        this.LastName.Text = empl.LastName;
        this.Title.Text = empl.Title;
        
        if(empl.ReportsTo.HasValue)
        {
            Employee reportsTo = Employee.GetEmployee(empl.ReportsTo.Value);
            this.ReportsTo.Text = reportsTo != null ? reportsTo.LastName : "";
        }
        
        this.HireDate.SelectedDate = empl.HireDate.HasValue ? empl.HireDate.Value : DateTime.MinValue;
        this.Extension.Text = empl.Extension;
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

    protected void Store1_Refresh(object sender, StoreRefreshDataEventArgs e)
    {
        this.Store1.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GridPanel with Form Details - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <asp:LinqDataSource 
            ID="LinqDataSource1" 
            runat="server" 
            ContextTypeName="Coolite.Examples.Code.Northwind.NorthwindDataContext"
            Select="new (EmployeeID, LastName, FirstName)" 
            TableName="Employees" 
            />
            
        <ext:Store ID="Store1" runat="server" DataSourceID="LinqDataSource1" OnRefreshData="Store1_Refresh">
            <Reader>
                <ext:JsonReader ReaderID="EmployeeID">
                    <Fields>
                        <ext:RecordField Name="LastName" />
                        <ext:RecordField Name="FirstName" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <LoadException Handler="Ext.Msg.alert('Employees - Load failed', e.message || response.statusText);" />
            </Listeners>
        </ext:Store>
        
        <ext:ViewPort ID="ViewPort1" runat="server">
            <Body>
                <ext:BorderLayout ID="BorderLayout1" runat="server">
                    <North MarginsSummary="5 5 5 5">
                        <ext:Panel 
                            ID="Panel1" 
                            runat="server" 
                            Title="Description" 
                            Height="100" 
                            BodyStyle="padding: 5px;"
                            Frame="true" 
                            Icon="Information">
                            <Body>
                                <h1>GridPanel with Form Details</h1>
                                <p>Click on any record with the GridPanel and the record details will be loaded into the Details Form.</p>
                            </Body>
                        </ext:Panel>
                    </North>
                    <Center MarginsSummary="0 0 5 5">
                        <ext:Panel 
                            runat="server" 
                            Frame="true" 
                            Title="Employees" 
                            Icon="UserSuit">
                            <Body>
                                <ext:FitLayout runat="server">
                                    <ext:GridPanel 
                                        ID="GridPanel1" 
                                        runat="server" 
                                        AutoExpandColumn="LastName" 
                                        StoreID="Store1"
                                        Border="false">
                                        <ColumnModel runat="server">
                                            <Columns>
                                                <ext:Column ColumnID="LastName" DataIndex="LastName" Header="LastName" />
                                                <ext:Column DataIndex="FirstName" Header="FirstName" Width="150" />
                                            </Columns>
                                        </ColumnModel>
                                        <SelectionModel>
                                            <ext:RowSelectionModel runat="server" SingleSelect="true">
                                                <AjaxEvents>
                                                    <RowSelect OnEvent="RowSelect" Buffer="250">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="#{Details}" />
                                                        <ExtraParams>
                                                            <%-- or can use params[2].id as value --%>
                                                            <ext:Parameter Name="EmployeeID" Value="this.getSelected().id" Mode="Raw" />
                                                        </ExtraParams>
                                                    </RowSelect>
                                                </AjaxEvents>
                                            </ext:RowSelectionModel>
                                        </SelectionModel>
                                        <BottomBar>
                                            <ext:PagingToolBar 
                                                ID="PagingToolBar1" 
                                                runat="server" 
                                                PageSize="10" 
                                                StoreID="Store1" 
                                                />
                                        </BottomBar>
                                        <LoadMask ShowMask="true" />
                                    </ext:GridPanel>
                                </ext:FitLayout>
                            </Body>
                        </ext:Panel>
                    </Center>
                    <East MarginsSummary="0 5 5 5">
                        <ext:Panel 
                            ID="Details" 
                            runat="server" 
                            Frame="true" 
                            Title="Employee Details" 
                            Width="280"
                            Icon="User">
                            <Body>
                                <ext:FormLayout runat="server">
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="EmployeeID" 
                                            runat="server" 
                                            FieldLabel="Employee ID" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="FirstName" 
                                            runat="server" 
                                            FieldLabel="First Name" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="LastName" 
                                            runat="server" 
                                            FieldLabel="Last Name" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="Title" 
                                            runat="server" 
                                            FieldLabel="Title" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="ReportsTo" 
                                            runat="server" 
                                            FieldLabel="Reports to" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:DateField 
                                            ID="HireDate" 
                                            runat="server" 
                                            Width="150" 
                                            FieldLabel="Hire date" 
                                            Format="yyyy-M-d"
                                            ReadOnly="true" 
                                            Disabled="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="Extension" 
                                            runat="server" 
                                            FieldLabel="Extension" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="Address" 
                                            runat="server" 
                                            FieldLabel="Address" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="City" 
                                            runat="server" 
                                            FieldLabel="City" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="PostCode" 
                                            runat="server" 
                                            FieldLabel="Post Code" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="HomePhone" 
                                            runat="server" 
                                            FieldLabel="Home Phone" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="TitleCourt" 
                                            runat="server" 
                                            FieldLabel="Title Of Courtesy" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:DateField 
                                            ID="BirthDate" 
                                            runat="server" 
                                            Width="150" 
                                            FieldLabel="Birth date"
                                            Format="yyyy-M-d" 
                                            ReadOnly="true" 
                                            Disabled="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="Region" 
                                            runat="server" 
                                            FieldLabel="Region" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextField 
                                            ID="Country" 
                                            runat="server" 
                                            FieldLabel="Country" 
                                            Width="150" 
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                    <ext:Anchor>
                                        <ext:TextArea 
                                            ID="Note" 
                                            runat="server" 
                                            FieldLabel="Note" 
                                            Height="50" 
                                            Width="150"
                                            ReadOnly="true" 
                                            />
                                    </ext:Anchor>
                                </ext:FormLayout>
                            </Body>
                        </ext:Panel>
                    </East>
                </ext:BorderLayout>
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>
