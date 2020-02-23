<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private object[] TestData
    {
        get
        {
            DateTime now = DateTime.Now;
            
            return new object[]
            {
                new object[] {"3m Co", 71.72, 0.02, 0.03, now},
                new object[] {"Alcoa Inc", 29.01, 0.42, 1.47, now},
                new object[] {"Altria Group Inc", 83.81, 0.28, 0.34, now},
                new object[] {"American Express Company", 52.55, 0.01, 0.02, now},
                new object[] {"American International Group, Inc.", 64.13, 0.31, 0.49, now},
                new object[] {"AT&T Inc.", 31.61, -0.48, -1.54, now},
                new object[] {"Boeing Co.", 75.43, 0.53, 0.71, now},
                new object[] {"Caterpillar Inc.", 67.27, 0.92, 1.39, now},
                new object[] {"Citigroup, Inc.", 49.37, 0.02, 0.04, now},
                new object[] {"E.I. du Pont de Nemours and Company", 40.48, 0.51, 1.28, now},
                new object[] {"Exxon Mobil Corp", 68.1, -0.43, -0.64, now},
                new object[] {"General Electric Company", 34.14, -0.08, -0.23, now},
                new object[] {"General Motors Corporation", 30.27, 1.09, 3.74, now},
                new object[] {"Hewlett-Packard Co.", 36.53, -0.03, -0.08, now},
                new object[] {"Honeywell Intl Inc", 38.77, 0.05, 0.13, now},
                new object[] {"Intel Corporation", 19.88, 0.31, 1.58, now},
                new object[] {"International Business Machines", 81.41, 0.44, 0.54, now},
                new object[] {"Johnson & Johnson", 64.72, 0.06, 0.09, now},
                new object[] {"JP Morgan & Chase & Co", 45.73, 0.07, 0.15, now},
                new object[] {"McDonald\"s Corporation", 36.76, 0.86, 2.40, now},
                new object[] {"Merck & Co., Inc.", 40.96, 0.41, 1.01, now},
                new object[] {"Microsoft Corporation", 25.84, 0.14, 0.54, now},
                new object[] {"Pfizer Inc", 27.96, 0.4, 1.45, now},
                new object[] {"The Coca-Cola Company", 45.07, 0.26, 0.58, now},
                new object[] {"The Home Depot, Inc.", 34.64, 0.35, 1.02, now},
                new object[] {"The Procter & Gamble Company", 61.91, 0.01, 0.02, now},
                new object[] {"United Technologies Corporation", 63.26, 0.55, 0.88, now},
                new object[] {"Verizon Communications", 35.57, 0.39, 1.11, now},
                new object[] {"Wal-Mart Stores, Inc.", 45.45, 0.73, 1.63, now}
            };
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Ext.IsAjaxRequest)
        {
            this.Store1.DataSource = this.TestData;
            this.Store1.DataBind(); 
        }
    }

    protected void MyData_Refresh(object sender, StoreRefreshDataEventArgs e)
    {
        this.Store1.DataSource = this.TestData;
        this.Store1.DataBind(); 
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Simple Array Grid With Paging and Remote reloading - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var template = '<span style="color:{0};">{1}</span>';

        var change = function(value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value);
        }

        var pctChange = function(value) {
            return String.format(template, (value > 0) ? 'green' : 'red', value + '%');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Array Grid with Paging and Remote reloading</h1>
        <p>Demonstrates how to create a grid from Array data with Local Paging and Remote Reloading.</p>
        <p>Notice <b>Last Updated</b> column is revised with a new server-side DateTime stamp when the GridPanel "Refresh" button is clicked.<br />This demonstrates that when the GridPanel is refreshed, the Data is requested again from the server via an AjaxEvent, but the Paging and Sorting is done completely client-side in the browser.</p>
        
        <ext:Store ID="Store1" runat="server" OnRefreshData="MyData_Refresh">
            <Reader>
                <ext:ArrayReader>
                    <Fields>
                        <ext:RecordField Name="company" />
                        <ext:RecordField Name="price" Type="Float" />
                        <ext:RecordField Name="change" Type="Float" />
                        <ext:RecordField Name="pctChange" Type="Float" />
                        <ext:RecordField Name="lastChange" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    </Fields>
                </ext:ArrayReader>
            </Reader>
        </ext:Store>
        
        <ext:GridPanel 
            runat="server" 
            StoreID="Store1" 
            StripeRows="true"
            Title="Array Grid" 
            Width="600" 
            Height="290"
            AutoExpandColumn="Company">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:Column ColumnID="Company" Header="Company" Width="160" Sortable="true" DataIndex="company" />
                    <ext:Column Header="Price" Width="75" Sortable="true" DataIndex="price">
                        <Renderer Format="UsMoney" />
                    </ext:Column>
                    <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="change">
                        <Renderer Fn="change" />
                    </ext:Column>
                    <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="pctChange">
                        <Renderer Fn="pctChange" />
                    </ext:Column>
                    <ext:Column Header="Last Updated" Width="85" Sortable="true" DataIndex="lastChange">
                        <Renderer Fn="Ext.util.Format.dateRenderer('G:i:s')" />
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" />
            </SelectionModel>
            <LoadMask ShowMask="true" />
            <BottomBar>
                <ext:PagingToolBar runat="server" PageSize="10" StoreID="Store1" />
            </BottomBar>
        </ext:GridPanel>
    </form>
</body>
</html>
