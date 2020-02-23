<%@ Page Language="C#" %>
<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
     {
         CultureInfo ci = new CultureInfo("en-US");
         this.Store1.DataSource = new List<Project> 
         { 
            new Project(100, "Ext Forms: Field Anchoring", 112, "Integrate 2.0 Forms with 2.0 Layouts", 6, 150, 0, DateTime.Parse("06/24/2007",ci)),
            new Project(100, "Ext Forms: Field Anchoring", 113, "Implement AnchorLayout", 4, 150, 0, DateTime.Parse("06/25/2007",ci)),
            new Project(100, "Ext Forms: Field Anchoring", 114, "Add support for multiple types of anchors", 4, 150, 0, DateTime.Parse("06/27/2007",ci)),
            new Project(100, "Ext Forms: Field Anchoring", 115, "Testing and debugging", 8, 0, 0, DateTime.Parse("06/29/2007",ci)),
            new Project(101, "Ext Grid: Single-level Grouping", 101, "Add required rendering \"hooks\" to GridView", 6, 100, 0, DateTime.Parse("07/01/2007",ci)),
            new Project(101, "Ext Grid: Single-level Grouping", 102, "Extend GridView and override rendering functions", 6, 100, 0, DateTime.Parse("07/03/2007",ci)),
            new Project(101, "Ext Grid: Single-level Grouping", 103, "Extend Store with grouping functionality", 4, 100, 0, DateTime.Parse("07/04/2007",ci)),
            new Project(101, "Ext Grid: Single-level Grouping", 121, "Default CSS Styling", 2, 100, 0, DateTime.Parse("07/05/2007",ci)),
            new Project(101, "Ext Grid: Single-level Grouping", 104, "Testing and debugging", 6, 100, 0, DateTime.Parse("07/06/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 105, "Ext Grid plugin integration", 4, 125, 0, DateTime.Parse("07/01/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 106, "Summary creation during rendering phase", 4, 125, 0, DateTime.Parse("07/02/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 107, "Dynamic summary updates in editor grids", 6, 125, 0, DateTime.Parse("07/05/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 108, "Remote summary integration", 4, 125, 0, DateTime.Parse("07/05/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 109, "Summary renderers and calculators", 4, 125, 0, DateTime.Parse("07/06/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 110, "Integrate summaries with GroupingView", 10, 125, 0, DateTime.Parse("07/11/2007",ci)),
            new Project(102, "Ext Grid: Summary Rows", 111, "Testing and debugging", 8, 125, 0, DateTime.Parse("07/15/2007",ci))
         };

        this.Store1.DataBind();
    }

    public class Project
    {
        public Project(int projectId, string name, int taskId, string description, int estimate, double rate, double cost, DateTime due)
        {
            this.ProjectID = projectId;
            this.Name = name;
            this.TaskID = taskId;
            this.Description = description;
            this.Estimate = estimate;
            this.Rate = rate;
            this.Due = due;
        }

        public int ProjectID { get; set; }
        public string Name { get;set; }
        public int TaskID { get; set; }
        public string Description { get;set; }
        public int Estimate { get;set; }
        public double Rate { get; set; }
        public double Cost { get; set; }
        public DateTime Due { get; set; }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example - GroupingSummary Plugin</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
   
    <style type="text/css">
        .x-grid3-body .x-grid3-td-Cost {
            background-color:#f1f2f4;
        }
        
        .x-grid3-summary-row .x-grid3-td-Cost {
            background-color:#e1e2e4;
        }
        
        .x-grid3-dirty-cell {
            background-image:none;
        }
        
        .x-grid3-summary-row {
            background:#F1F2F4 none repeat scroll 0 0;
            border-left:1px solid #FFFFFF;
            border-right:1px solid #FFFFFF;
            color:#333333;
        }
        
        .x-grid3-summary-row .x-grid3-cell-inner {
            font-weight:bold;
            padding-bottom:4px;
        }
        
        .x-grid3-cell-first .x-grid3-cell-inner {
            padding-left:16px;
        }
        
        .x-grid-hide-summary .x-grid3-summary-row {
            display:none;
        }
        
        .x-grid3-summary-msg {
            font-weight:bold;
            padding:4px 16px;
        }        
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <h1>Group Summary Plugin</h1>

        <ext:ScriptManager ID="ScriptManager1" runat="server">
            <Listeners>
                <DocumentReady Handler="Ext.grid.GroupSummary.Calculations['totalCost'] = function(v, record, field){
                        return v + (record.data.Estimate * record.data.Rate);
                    }" />
            </Listeners>
        </ext:ScriptManager>
        
        <ext:Store ID="Store1" runat="server" GroupField="Name">
            <SortInfo Direction="ASC" Field="Due" />
            <Reader>
                <ext:JsonReader ReaderID="TaskID">
                    <Fields>
                        <ext:RecordField Name="ProjectID" />
                        <ext:RecordField Name="Name" />
                        <ext:RecordField Name="TaskID" />
                        <ext:RecordField Name="Description" />
                        <ext:RecordField Name="Estimate" />
                        <ext:RecordField Name="Rate" />
                        <ext:RecordField Name="Due" Type="Date" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:GridPanel 
            ID="GridPanel1" 
            runat="server" 
            Frame="true"
            StoreID="Store1"
            StripeRows="true"
            Title="Sponsored Projects"
            AutoExpandColumn="Description" 
            Collapsible="true"
            AnimCollapse="false"
            Icon="ApplicationViewColumns"
            TrackMouseOver="false"
            Width="800"
            Height="450"
            ClicksToEdit="1">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:GroupingSummaryColumn 
                        ColumnID="Description" 
                        Header="Task" 
                        Sortable="true" 
                        DataIndex="Description" 
                        Hideable="false"
                        SummaryType="Count">
                        <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Tasks)' : '(1 Task)');" />    
                    </ext:GroupingSummaryColumn>
                    
                    <ext:Column ColumnID="Name" Header="Project" Sortable="true" DataIndex="Name" Width="20" />
                    
                    <ext:GroupingSummaryColumn 
                        ColumnID="Due" 
                        Width="25"
                        Header="Due Date" 
                        Sortable="true" 
                        DataIndex="Due"
                        SummaryType="Max">
                        <Renderer Format="Date" FormatArgs="'m/d/Y'" />
                    </ext:GroupingSummaryColumn>

                    <ext:GroupingSummaryColumn 
                        Width="20"
                        ColumnID="Estimate" 
                        Header="Estimate" 
                        Sortable="true" 
                        DataIndex="Estimate" 
                        SummaryType="Max">
                        <Renderer Handler="return value +' hours';" />    
                    </ext:GroupingSummaryColumn>
                    
                    <ext:GroupingSummaryColumn 
                        Width="20"
                        ColumnID="Rate" 
                        Header="Rate" 
                        Sortable="true" 
                        DataIndex="Rate"
                        SummaryType="Average">
                        <Renderer Format="UsMoney" />
                    </ext:GroupingSummaryColumn>
                    
                    <ext:GroupingSummaryColumn 
                        Width="20"
                        ColumnID="Cost" 
                        Header="Cost" 
                        Sortable="false"
                        Groupable="false" 
                        DataIndex="Cost"
                        CustomSummaryType="totalCost">
                        <Renderer Handler="return Ext.util.Format.usMoney(record.data.Estimate * record.data.Rate);" />
                        <SummaryRenderer Format="UsMoney" />
                    </ext:GroupingSummaryColumn>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GroupingView 
                    ID="GroupingView1"  
                    runat="server" 
                    ForceFit="true"
                    ShowGroupName="false"
                    EnableNoGroups="true"
                    HideGroupedColumn="true"
                    />
            </View>            
            <Plugins>
                <ext:GroupingSummary ID="GroupingSummary1" runat="server" />
            </Plugins>           
        </ext:GridPanel>
    </form>
  </body>
</html>