﻿<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<%@ Register src="Address.ascx" tagname="Address" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.BindCustomer(this.GetCustomer());
        }
    }

    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        string template = "{0}: {1}<br />";
        StringBuilder sb = new StringBuilder(255);

        sb.AppendFormat(template, "Customer ID", this.txtCustomerID.Value);
        sb.AppendFormat(template, "First Name", this.txtFirstName.Text);
        sb.AppendFormat(template, "Last Name", this.txtLastName.Text);
        sb.AppendFormat(template, "Company", this.txtCompany.Text);

        this.Label1.Html = sb.ToString();
    }

    public void BindCustomer(Customer customer)
    {
        this.txtCustomerID.Value = customer.ID;
        
        this.txtFirstName.Text = customer.FirstName;
        this.txtLastName.Text = customer.LastName;
        this.txtCompany.Text = customer.Company;

        this.ucShipping.StreetAddress = customer.ShippingAddress.StreetAddress;
        this.ucShipping.ZipPostalCode = customer.ShippingAddress.ZipPostalCode;
        this.ucShipping.City = customer.ShippingAddress.City;
        this.ucShipping.CountryID = customer.ShippingAddress.Country.Code;

        if (customer.BillingAddress != null)
        {
            this.ucBilling.StreetAddress = customer.ShippingAddress.StreetAddress;
            this.ucBilling.ZipPostalCode = customer.ShippingAddress.ZipPostalCode;
            this.ucBilling.City = customer.ShippingAddress.City;
            this.ucBilling.CountryID = customer.ShippingAddress.Country.Code;
        }
        else
        {
            this.ucBilling.ShowCheckbox = true;
            this.ucBilling.Checked = true;
            this.ucBilling.CheckboxMessage = "Same as Shipping";
        }
    }

    public Customer GetCustomer()
    {
        Customer customer = new Customer();
        customer.ID = "08-1";
        customer.FirstName = "Geoffrey";
        customer.LastName = "McGill";
        customer.Company = "Coolite Inc.";

        Address address = new Address();
        address.StreetAddress = "#208, 10113 104 Street";
        address.ZipPostalCode = "T5J 1A1";
        address.City = "Edmonton";

        Country country = new Country();
        country = new Country();
        country.Code = "CA";
        country.Name = "Canada";

        address.Country = country;
        customer.ShippingAddress = address;
        
        return customer;
    }
    
    public class Customer
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Company { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
    }

    public class Address
    {
        public string StreetAddress { get; set; }
        public string ZipPostalCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FormLayout with nested UserControls - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:Label ID="Label1" runat="server" />
        
        <ext:Hidden ID="txtCustomerID" runat="server" />
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Icon="User"
            Closable="false"
            Title="Customer Details"
            Width="350"
            Height="435"
            Resizable="false"
            BodyStyle="padding:5px;background-color:#fff;">
            <Body>
                <ext:FormLayout ID="FormLayout1" runat="server">
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtFirstName" runat="server" FieldLabel="First Name" AllowBlank="false" />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtLastName" runat="server" FieldLabel="Last Name" AllowBlank="false" />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField ID="txtCompany" runat="server" FieldLabel="Company" />
                    </ext:Anchor>
                    <ext:Anchor>
                        <ext:Panel ID="Panel1" runat="server" Header="false" Border="false">
                            <Body>
                                <uc1:Address ID="ucShipping" runat="server" Title="SHIPPING ADDRESS" />
                            </Body>
                        </ext:Panel>
                    </ext:Anchor>
                    <ext:Anchor>
                        <ext:Panel ID="Panel2" runat="server" Header="false" Border="false">
                            <Body>
                                <uc1:Address ID="ucBilling" runat="server" Title="BILLING ADDRESS" />
                            </Body>
                        </ext:Panel>
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Save" Icon="Disk">
                    <AjaxEvents>
                        <Click OnEvent="Button1_Click">
                            <EventMask ShowMask="true" Msg="Saving..." MinDelay="500" />
                        </Click>
                    </AjaxEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>