<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.ObjectModel"%>
<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Store1_RefreshData(object sender, StoreRefreshDataEventArgs e)
    {
        List<object> data = FiltersTestData.Data;

        string s = e.Parameters[this.GridFilters1.ParamPrefix];
        //or with hardcoding - string s = e.Parameters["gridfilters"];;
        
        
        //-- start filtering ------------------------------------------------------------
        if(!string.IsNullOrEmpty(s))
        {
            FilterConditions fc = new FilterConditions(s);

            foreach (FilterCondition condition in fc.Conditions)
            {
                Comparison comparison = condition.Comparison;
                string field = condition.Name;
                FilterType type = condition.FilterType;
                
                object value;
                switch(condition.FilterType)
                {
                    case FilterType.Boolean:
                        value = condition.ValueAsBoolean;
                       break;
                    case FilterType.Date:
                        value = condition.ValueAsDate;
                        break;
                    case FilterType.List:
                        value = condition.ValuesList;
                        break;
                    case FilterType.Numeric:
                        if (data.Count > 0 && data[0].GetType().GetProperty(field).PropertyType == typeof(int))
                        {
                            value = condition.ValueAsInt;
                        }
                        else
                        {
                            value = condition.ValueAsDouble;
                        }
                        
                        break;
                    case FilterType.String:
                        value = condition.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                data.RemoveAll(
                    item =>
                        {
                            object oValue = item.GetType().GetProperty(field).GetValue(item, null);
                            IComparable cItem = oValue as IComparable;
                            
                            switch (comparison)
                            {
                                case Comparison.Eq:
                                    
                                    switch(type)
                                    {
                                        case FilterType.List:
                                            return !(value as ReadOnlyCollection<string>).Contains(oValue.ToString());
                                        case FilterType.String:
                                            return !oValue.ToString().StartsWith(value.ToString());
                                        default:
                                            return !cItem.Equals(value);
                                    }
                                    
                                case Comparison.Gt:
                                    return cItem.CompareTo(value) < 1;
                                case Comparison.Lt:
                                    return cItem.CompareTo(value) > -1;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                );
                
            }
        }
        //-- end filtering ------------------------------------------------------------


        //-- start sorting ------------------------------------------------------------
        if (!string.IsNullOrEmpty(e.Sort))
        {
            data.Sort(delegate(object x, object y)
            {
                object a;
                object b;

                int direction = e.Dir == Coolite.Ext.Web.SortDirection.DESC ? -1 : 1;

                a = x.GetType().GetProperty(e.Sort).GetValue(x, null);
                b = y.GetType().GetProperty(e.Sort).GetValue(y, null);
                return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
            });
        }
        //-- end sorting ------------------------------------------------------------


        //-- start paging ------------------------------------------------------------
        var limit = e.Limit;
        if ((e.Start + e.Limit) > data.Count)
        {
            limit = data.Count - e.Start;
        }

        List<object> rangeData = (e.Start < 0 || limit < 0) ? data : data.GetRange(e.Start, limit);
        //-- end paging ------------------------------------------------------------

        //The TotalCount can be set in RefreshData event as below
        //or (Store1.Proxy.Proxy as DataSourceProxy).TotalCount in anywhere
        //Please pay attention that the TotalCount make a sence only during AjaxEvent because
        //the Store with DataSourceProxy get/refresh data using ajax request

        e.TotalCount = data.Count;
        this.Store1.DataSource = rangeData;
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridPanel with Remote Filtering, Sorting and Paging - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h1>GridPanel with Remote Filtering, Sorting and Paging</h1>
    <p>Please see column header menu for apllying filters</p>
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true" RemoteSort="true" OnRefreshData="Store1_RefreshData">
        <Proxy>
            <ext:DataSourceProxy />
        </Proxy>
        
        <Reader>
            <ext:JsonReader ReaderID="Id">
                <Fields>
                    <ext:RecordField Name="Id" Type="Int" />
                    <ext:RecordField Name="Company" Type="String" />
                    <ext:RecordField Name="Price" Type="Float" />
                    <ext:RecordField Name="Date" Type="Date" DateFormat="Y-m-dTh:i:s" />
                    <ext:RecordField Name="Size" Type="String" />
                    <ext:RecordField Name="Visible" Type="Boolean" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        
        <BaseParams>
            <ext:Parameter Name="start" Value="0" Mode="Raw" />
            <ext:Parameter Name="limit" Value="10" Mode="Raw" />
            <ext:Parameter Name="sort" Value="" />
            <ext:Parameter Name="dir" Value="" />
        </BaseParams>
        
        <SortInfo Field="Company" Direction="ASC" />
        
        <AjaxEventConfig Method="GET"></AjaxEventConfig>
    </ext:Store>
    
    <ext:Window 
        ID="Window1" 
        runat="server" 
        Width="700" 
        Height="400" 
        Closable="false"
        Collapsible="true" 
        Title="Example"
        Maximizable="true">
        <Body>
            <ext:FitLayout ID="FitLayout1" runat="server">
                <ext:GridPanel 
                    runat="server" 
                    ID="GridPanel1" 
                    Border="false"
                    StoreID="Store1">
                    <ColumnModel runat="server">
					<Columns>
                        <ext:Column Header="Id" DataIndex="Id" Sortable="true" />
                        <ext:Column Header="Company" DataIndex="Company" Sortable="true" />
                        <ext:Column Header="Price" DataIndex="Price" Sortable="true">
                            <Renderer Format="UsMoney" />
                        </ext:Column>                        
                        <ext:Column Header="Date" DataIndex="Date" Sortable="true" Align="Center">
                            <Renderer Fn="Ext.util.Format.dateRenderer('Y-m-d')" />
                        </ext:Column>
                        <ext:Column Header="Size" DataIndex="Size" Sortable="true" />
                        <ext:Column Header="Visible" DataIndex="Visible" Sortable="true" Align="Center">
                            <Renderer Handler="return (value) ? 'Yes':'No';" />
                        </ext:Column>
					</Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" />
                    <Plugins>
                        <ext:GridFilters runat="server" ID="GridFilters1">
                            <Filters>
                                <ext:NumericFilter DataIndex="Id" />
                                <ext:StringFilter DataIndex="Company" />
                                <ext:NumericFilter DataIndex="Price" />
                                <ext:DateFilter DataIndex="Date">
                                    <DatePickerOptions runat="server" TodayText="Now"></DatePickerOptions>
                                </ext:DateFilter>
                                <ext:ListFilter DataIndex="Size" Options="extra small,small,medium,large,extra large" />
                                <ext:BooleanFilter DataIndex="Visible" />
                            </Filters>
                        </ext:GridFilters>
                    </Plugins>
                    <BottomBar>
                        <ext:PagingToolBar 
                            ID="PagingToolBar1" 
                            runat="server" 
                            PageSize="10" 
                            StoreID="Store1"
                            DisplayInfo="true"              
                            />
                    </BottomBar>
                </ext:GridPanel>
            </ext:FitLayout>
        </Body>
    </ext:Window>    
</body>
</html>