﻿<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Customer> list = new List<Customer>(5);

        for (int i = 1; i <= 5; i++)
        {
            Customer customer = new Customer
                                    {
                                        ID = i,
                                        FirstName = ("FirstName" + i),
                                        LastName = ("LastName" + i),
                                        Company = ("Company" + i)
                                    };

            Address address = new Address
                                  {
                                      StreetAddress = ("Street" + i), 
                                      City = ("City" + i)
                                  };
            
            customer.Address = address;

            list.Add(customer);
        }

        Store1.DataSource = list;
        Store1.DataBind();
    }


    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example - Data Prepare</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         function prepare(value, rec) {
             rec.City = value.City;
             rec.StreetAddress = value.StreetAddress;
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
       <ext:Store runat="server" ID="Store1" AutoLoad="true" SerializationMode="Complex">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="ID" Type="Int" />
                        <ext:RecordField Name="FirstName" />
                        <ext:RecordField Name="LastName" />
                        <ext:RecordField Name="Company" />
                        <ext:RecordField Name="Address">
                            <Convert Fn="prepare" />
                        </ext:RecordField>
                        <ext:RecordField Name="City" />
                        <ext:RecordField Name="StreetAddress" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
         <ext:GridPanel 
            ID="GridPanel1" 
            runat="server" 
            Title="Customers" 
            Height="300"
            StoreID="Store1">
            <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:Column Header="ID" DataIndex="ID" />
                <ext:Column Header="FirstName" DataIndex="FirstName"  />
                <ext:Column Header="LastName" DataIndex="LastName" />
                <ext:Column Header="Company" DataIndex="Company" />            
                <ext:Column Header="City" DataIndex="City" />            
                <ext:Column Header="Street" DataIndex="StreetAddress" />            
            </Columns>
            </ColumnModel>
            <LoadMask ShowMask="true" />
        </ext:GridPanel> 
    </form>
</body>
</html>
