<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private static List<Data> dataSource;

    public static List<Data> DataSource
    {
        get
        {
            if (dataSource == null || dataSource.Count == 0)
            {
                dataSource = new List<Data>(5);
                for (int i = 1; i <= 5; i++)
                {
                    Data data = new Data();
                    data.ID = i;
                    data.Company = "Company" + i;
                    data.Price = i * 10;
                    data.Change = i;
                    data.LastChange = DateTime.Now.AddDays(i);
                    dataSource.Add(data);
                }
            }

            return dataSource;
        }
    }

    private Data GetByID(int id)
    {
        foreach (Data data in DataSource)
        {
            if (data.ID == id)
            {
                return data;
            }
        }

        return null;
    }

    private int GetMaxID()
    {
        int id = 0;

        foreach (Data data in DataSource)
        {
            if (data.ID > id)
            {
                id = data.ID;
            }
        }

        return id + 1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void FetchRecord(object sender, StoreRefreshDataEventArgs e)
    {
        Store1.DataSource = new List<Data> { DataSource[e.Start] };
        Store1.DataBind();

        e.TotalCount = DataSource.Count;
    }

    protected void DeleteClick(object sender, AjaxEventArgs e)
    {
        int id = int.Parse(e.ExtraParams["recordId"]);
        Data data = this.GetByID(id);

        int index = DataSource.IndexOf(data);
        DataSource.Remove(data);

        if (index == DataSource.Count)
        {
            index--;
        }

        if (index >= 0)
        {
            Pager1.PageIndex = index + 1;
        }
    }

    protected void AddClick(object sender, AjaxEventArgs e)
    {
        Data data = new Data();
        data.ID = this.GetMaxID();
        DataSource.Add(data);
        Pager1.PageIndex = DataSource.Count;
    }

    protected void SaveClick(object sender, AjaxEventArgs e)
    {
        int id = int.Parse(e.ExtraParams["recordId"]);

        Data data = this.GetByID(id);

        data.Company = this.CompanyField.Text;
        data.Price = (float)this.PriceField.Number;
        data.Change = (float)this.ChangeField.Number;
        data.LastChange = this.LastChangeField.SelectedDate;
    }

    public class Data
    {
        public int ID
        {
            get;
            set;
        }
        public string Company
        {
            get;
            set;
        }
        public float Price
        {
            get;
            set;
        }
        public float Change
        {
            get;
            set;
        }
        public DateTime LastChange
        {
            get;
            set;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Toolkit Example - Edit Form View</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:Store ID="Store1" runat="server" OnRefreshData="FetchRecord">
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="={0}" />
            </AutoLoadParams>
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader ReaderID="ID">
                    <Fields>
                        <ext:RecordField Name="Company" />
                        <ext:RecordField Name="Price" Type="Float" />
                        <ext:RecordField Name="Change" Type="Float" />
                        <ext:RecordField Name="LastChange" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <DataChanged Handler="var record = this.getAt(0) || {};#{FormPanel1}.getForm().loadRecord(record);" />
                <BeforeLoad Handler="#{FormWindow}.body.mask('Loading...', 'x-mask-loading');" />
                <Load Handler="#{FormWindow}.body.unmask();" />
                <LoadException Handler="#{FormWindow}.body.unmask();" />
            </Listeners>
        </ext:Store>
        
        <ext:Window ID="FormWindow" runat="server" Title="Form View" CenterOnLoad="true"
            Width="420" Height="205" BodyStyle="padding:10px;" Resizable="false" Closable="false">
            <Body>
                <ext:FitLayout ID="FitLayout1" runat="server">
                    <ext:FormPanel ID="FormPanel1" runat="server" Border="false" MonitorValid="true" BodyStyle="background-color:transparent;">
                        <Body>
                            <ext:FormLayout runat="server">
                                <ext:Anchor>
                                    <ext:TextField ID="CompanyField" runat="server" DataIndex="Company" MsgTarget="Side" AllowBlank="false"
                                        FieldLabel="Company" Width="260" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:NumberField ID="PriceField" runat="server" DataIndex="Price" MsgTarget="Side" AllowBlank="false"
                                        FieldLabel="Price" Width="260" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:NumberField ID="ChangeField" runat="server" DataIndex="Change" MsgTarget="Side" AllowBlank="false"
                                        FieldLabel="Change" Width="260" />
                                </ext:Anchor>
                                <ext:Anchor>
                                    <ext:DateField ID="LastChangeField" runat="server" DataIndex="LastChange" MsgTarget="Side" AllowBlank="false"
                                        FieldLabel="Last change" Width="260" />
                                </ext:Anchor>
                            </ext:FormLayout>
                        </Body>
                        <Listeners>
                            <ClientValidation Handler="btnSaveRecord.setDisabled(!#{FormPanel1}.getForm().isValid());" />
                        </Listeners>
                    </ext:FormPanel>
                </ext:FitLayout>
            </Body>
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarButton runat="server" Icon="Add" Text="Add">
                            <AjaxEvents>
                                <Click OnEvent="AddClick" />
                            </AjaxEvents>
                        </ext:ToolbarButton>
                        <ext:ToolbarButton runat="server" Icon="Delete" Text="Delete">
                            <AjaxEvents>
                                <Click OnEvent="DeleteClick" Success="Ext.Msg.alert('', 'Deleted');">
                                    <ExtraParams>
                                        <ext:Parameter Name="recordId" Value="#{Store1}.getAt(0).id" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </AjaxEvents>
                        </ext:ToolbarButton>
                        <ext:ToolbarFill runat="server" />
                        <ext:ToolbarButton ID="btnSaveRecord" runat="server" Icon="Disk" Text="Save">
                            <AjaxEvents>
                                <Click OnEvent="SaveClick" Before="return #{FormPanel1}.getForm().isValid();" Success="Ext.Msg.alert('', 'Saved');">
                                    <ExtraParams>
                                        <ext:Parameter Name="recordId" Value="#{Store1}.getAt(0).id" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </AjaxEvents>
                        </ext:ToolbarButton>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <BottomBar>
                <ext:PagingToolbar ID="Pager1" runat="server" PageSize="1" StoreID="Store1" DisplayInfo="false" />
            </BottomBar>
        </ext:Window>
    </form>
</body>
</html>
