<%@ Page Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml.Xsl"%>
<%@ Import Namespace="System.Xml"%>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.Store1.DataSource = this.GetDataReader();
            this.Store1.DataBind();
        }
    }

    protected void Store1_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        this.Store1.DataSource = this.GetDataReader();
        this.Store1.DataBind();
    }
    
    private SqlDataReader GetDataReader()
    {
        SqlConnection myConnection;
        SqlCommand myCommand;
        SqlDataReader myDataReader;

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["NORTHWNDConnectionString"].ConnectionString;
            
        myConnection = new SqlConnection(strConn);
        myConnection.Open();

        myCommand = new SqlCommand("SELECT * FROM Suppliers", myConnection);
        myDataReader = myCommand.ExecuteReader();

        return myDataReader;
    }

    protected void commandClick(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("Ajax Event Alert", "This is a test... ").Show();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GridPanel using DataReader with Local Paging and Remote Reloading - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
        }

        var pctChange = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value + "%");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" ScriptMode="Debug" />
        
        <h1>GridPanel using DataReader with Local Paging and Remote Reloading</h1>
        
        <ext:Store 
            ID="Store1" 
            runat="server" 
            OnRefreshData="Store1_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SupplierID" />
                        <ext:RecordField Name="CompanyName" />
                        <ext:RecordField Name="ContactName" />
                        <ext:RecordField Name="ContactTitle" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Hidden ID="FormatType" runat="server" />

        <ext:GridPanel 
            id="GridPanel1"
            runat="server" 
            StoreID="Store1" 
            Title="DataReader Grid" 
            TrackMouseOver="true"
            Width="600" 
            Height="320"
            AutoExpandColumn="CompanyName">
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:Column Header="Supplier ID" DataIndex="SupplierID" />
                    <ext:Column ColumnID="CompanyName" Header="Company Name" DataIndex="CompanyName" />
                    <ext:Column Header="Contact Name" DataIndex="ContactName" />
                    <ext:Column Header="Contact Title" DataIndex="ContactTitle" />
                    <ext:CommandColumn Width="150" Align="Center">
                        <Commands>
                            <ext:GridCommand Icon="TelephoneEdit" CommandName="Details" Text="Edit" />
                        </Commands>
                    </ext:CommandColumn>
                </Columns>
            </ColumnModel>
            <SelectionModel>
                <ext:RowSelectionModel runat="server" />
            </SelectionModel>
            <LoadMask ShowMask="true" />
            <BottomBar>
                <ext:PagingToolBar runat="server" PageSize="10" StoreID="Store1" />
            </BottomBar>
            <AjaxEvents>
                <Command OnEvent="commandClick" />
            </AjaxEvents>
        </ext:GridPanel>
        
        <%--<ext:FormPanel 
            ID="FormPanel2" 
            runat="server" 
            Title="title"
            StyleSpec="padding-left:16px;padding-top:16px;" 
            MonitorValid="true"
            Width="1024" 
            BodyStyle="padding:5px;" 
            ButtonAlign="Right">
            <Body>
                <ext:ColumnLayout ID="ColumnLayout1" runat="server">
                    <ext:LayoutColumn ColumnWidth=".5">
                        <ext:Panel ID="Panel1" runat="server" Border="false">
                            <Body>
                                <ext:ContainerLayout ID="ContainerLayout1" runat="server">
                                    <ext:Panel 
                                        ID="Panel11" 
                                        runat="server" 
                                        Title="Data" 
                                        Border="false" 
                                        Frame="true" 
                                        Header="true"
                                        FormGroup="true">
                                        <Defaults>
                                            <ext:Parameter Name="MsgTarget" Value="side" />
                                        </Defaults>
                                        <Body>
                                            <ext:FormLayout
                                                ID="FormLayout2" 
                                                runat="server" 
                                                LabelAlign="Left" 
                                                LabelCls="form-label" 
                                                LabelWidth="160">
                                                <Anchors>
                                                    <ext:Anchor Horizontal="92%">
                                                        <ext:Label ID="labelKind" runat="server" FieldLabel="Kind"   />
                                                    </ext:Anchor>
                                                </Anchors>
                                            </ext:FormLayout>
                                        </Body>
                                    </ext:Panel>
                                </ext:ContainerLayout>
                            </Body>
                        </ext:Panel>
                   </ext:LayoutColumn>
               </ext:ColumnLayout>
           </Body>
       </ext:FormPanel>--%>
           
                                            
                                            
                                            
        <ext:Window ID="Window1" runat="server" Height="185" Width="350" Title="Title">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:FormPanel 
                        ID="FormPanel1" 
                        runat="server"
                        MonitorValid="true">
                        <Body>
                            <ext:FormLayout ID="FormLayout1" runat="server">
                                <Anchors>
                                    <ext:Anchor>
                                        <ext:Label ID="Label1" runat="server" Text="test" FieldLabel="Label" />
                                    </ext:Anchor>
                                    
                                    <ext:Anchor>
                                        <ext:TextField ID="TextField1" runat="server" Text="text" AllowBlank="false" FieldLabel="TextField" />
                                    </ext:Anchor>
                                </Anchors>
                            </ext:FormLayout>
                        </Body>
                    </ext:FormPanel>
                </ext:FitLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save">
                    <Listeners>
                        <Click Handler="" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>
