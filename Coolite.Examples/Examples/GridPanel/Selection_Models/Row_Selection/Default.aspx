﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Xml"%>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
     {
         this.Store1.DataSource = new List<Company> 
         { 
             new Company("3m Co", 71.72, 0.02, 0.03),
             new Company("Alcoa Inc", 29.01, 0.42, 1.47),
             new Company("Altria Group Inc", 83.81, 0.28, 0.34),
             new Company("American Express Company", 52.55, 0.01, 0.02),
             new Company("American International Group, Inc.", 64.13, 0.31, 0.49),
             new Company("AT&T Inc.", 31.61, -0.48, -1.54),
             new Company("Boeing Co.", 75.43, 0.53, 0.71),
             new Company("Caterpillar Inc.", 67.27, 0.92, 1.39),
             new Company("Citigroup, Inc.", 49.37, 0.02, 0.04),
             new Company("E.I. du Pont de Nemours and Company", 40.48, 0.51, 1.28),
             new Company("Exxon Mobil Corp", 68.1, -0.43, -0.64),
             new Company("General Electric Company", 34.14, -0.08, -0.23),
             new Company("General Motors Corporation", 30.27, 1.09, 3.74),
             new Company("Hewlett-Packard Co.", 36.53, -0.03, -0.08),
             new Company("Honeywell Intl Inc", 38.77, 0.05, 0.13),
             new Company("Intel Corporation", 19.88, 0.31, 1.58),
             new Company("International Business Machines", 81.41, 0.44, 0.54),
             new Company("Johnson & Johnson", 64.72, 0.06, 0.09),
             new Company("JP Morgan & Chase & Co", 45.73, 0.07, 0.15),
             new Company("McDonald\"s Corporation", 36.76, 0.86, 2.40),
             new Company("Merck & Co., Inc.", 40.96, 0.41, 1.01),
             new Company("Microsoft Corporation", 25.84, 0.14, 0.54),
             new Company("Pfizer Inc", 27.96, 0.4, 1.45),
             new Company("The Coca-Cola Company", 45.07, 0.26, 0.58),
             new Company("The Home Depot, Inc.", 34.64, 0.35, 1.02),
             new Company("The Procter & Gamble Company", 61.91, 0.01, 0.02),
             new Company("United Technologies Corporation", 63.26, 0.55, 0.88),
             new Company("Verizon Communications", 35.57, 0.39, 1.11),
             new Company("Wal-Mart Stores, Inc.", 45.45, 0.73, 1.63)
         };

         

        if(!Ext.IsAjaxRequest)
        {
            this.Store1.DataBind();
            RowSelectionModel sm = this.GridPanel1.SelectionModel.Primary as RowSelectionModel;
            sm.SelectedRows.Add(new SelectedRow(1));
            sm.SelectedRows.Add(new SelectedRow("Caterpillar Inc."));
        }
    }

    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        RowSelectionModel sm = this.GridPanel1.SelectionModel.Primary as RowSelectionModel;

        StringBuilder sb = new StringBuilder();
        foreach (SelectedRow row in sm.SelectedRows)
        {
            sb.AppendFormat("RecordID: {0}&nbsp;&nbsp;&nbsp;&nbsp;Row index: {1}<br/>", row.RecordID, row.RowIndex);
        }
        this.Label1.Html = sb.ToString();
    }

    protected void Clear_Click(object sender, AjaxEventArgs e)
    {
        RowSelectionModel sm = this.GridPanel1.SelectionModel.Primary as RowSelectionModel;
        sm.SelectedRows.Clear();
        sm.UpdateSelection();
    }

    protected void Add_Click(object sender, AjaxEventArgs e)
    {
        RowSelectionModel sm = this.GridPanel1.SelectionModel.Primary as RowSelectionModel;
        sm.SelectedRows.Add(new SelectedRow("Boeing Co."));
        sm.UpdateSelection();
    }

    protected void SubmitSelection(object sender, AjaxEventArgs e)
    {
        string json = e.ExtraParams["Values"];

        Dictionary<string, string>[] companies = JSON.Deserialize<Dictionary<string, string>[]>(json);

        StringBuilder sb = new StringBuilder();
        sb.Append("<table  cellspacing='15'>");
        bool addHeader = true;
        
        foreach (Dictionary<string, string> row in companies)
        {
            if(addHeader)
            {
                sb.Append("<tr>");
                foreach (KeyValuePair<string, string> keyValuePair in row)
                {
                    sb.Append("<td style='white-space:nowrap;font-weight:bold;'>");

                    sb.Append(keyValuePair.Key);

                    sb.Append("</td>");
                }
                sb.Append("</tr>");

                addHeader = false;
            }
            
            sb.Append("<tr>");
            foreach (KeyValuePair<string, string> keyValuePair in row)
            {
                sb.Append("<td style='white-space:nowrap;'>");

                sb.Append(keyValuePair.Value);

                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        this.Label1.Html = sb.ToString();
    }

    public class Company
    {
        public Company(string name, double price, double change, double pctChange)
        {
            this.Name = name;
            this.Price = price;
            this.Change = change;
            this.PctChange = pctChange;
        }

        public Company()
        {
        }

        public string Name { get;set; }
        public double Price { get;set; }
        public double Change { get;set; }
        public double PctChange { get;set; }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Row Selection Model</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>

     <form id="form1" runat="server">
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store ID="Store1" runat="server" >
        <Reader>
            <ext:JsonReader ReaderID="Name">
                <Fields>
                    <ext:RecordField Name="Name" />
                    <ext:RecordField Name="Price" />
                    <ext:RecordField Name="Change" />
                    <ext:RecordField Name="PctChange" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    
    <ext:GridPanel 
        ID="GridPanel1" 
        runat="server" 
        StoreID="Store1"
        StripeRows="true"
        Title="Company List"
        AutoExpandColumn="Company" 
        Collapsible="true"
        Width="600"
        Height="350">
        <ColumnModel ID="ColumnModel1" runat="server">
		    <Columns>
                <ext:Column ColumnId="Company" Header="Company" Width="160" Sortable="true" DataIndex="Name" />
                <ext:Column Header="Price" Width="75" Sortable="true" DataIndex="Price">
                    <Renderer Format="UsMoney" />
                </ext:Column>
                <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="Change" />
                <ext:Column Header="Change" Width="75" Sortable="true" DataIndex="PctChange" />
		    </Columns>
        </ColumnModel>
        <SelectionModel>
            <ext:RowSelectionModel runat="server" />
        </SelectionModel>
        
        <Buttons>
            <ext:Button ID="Button1" runat="server" Text="Submit">
                <AjaxEvents>
                    <Click OnEvent="Button1_Click" />
                </AjaxEvents>
            </ext:Button>
            
            <ext:Button ID="Button4" runat="server" Text="Submit with values">
                <AjaxEvents>
                    <Click OnEvent="SubmitSelection">
                        <ExtraParams>
                            <ext:Parameter Name="Values" Value="Ext.encode(#{GridPanel1}.getRowsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </AjaxEvents>
            </ext:Button>
            
            <ext:Button ID="Button2" runat="server" Text="Clear">
                <AjaxEvents>
                    <Click OnEvent="Clear_Click" />
                </AjaxEvents>
            </ext:Button>
            
            <ext:Button ID="Button3" runat="server" Text="Add 'Boeing Co.' to selection ">
                <AjaxEvents>
                    <Click OnEvent="Add_Click" />
                </AjaxEvents>
            </ext:Button>
        </Buttons>
     </ext:GridPanel>
     
     <div style="width:590px; border:1px solid gray; padding:5px;">
            <ext:Label ID="Label1" runat="server" />
     </div>    
    
    </form>
  </body>
</html>
