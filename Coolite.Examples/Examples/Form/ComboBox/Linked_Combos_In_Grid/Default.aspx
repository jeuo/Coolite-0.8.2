﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Ext.IsAjaxRequest)
            {
                List<object> data = new List<object>
                                        {
                                            new {Country = "C1", State = "C1_S1", City = "C1_S1_C1", Region = "C1_S1_C1_R1"},
                                            new {Country = "C2", State = "C2_S1", City = "C2_S1_C1", Region = "C2_S1_C1_R1"},
                                            new {Country = "C3", State = "C3_S1", City = "C3_S1_C1", Region = "C3_S1_C1_R1"},
                                        };
                Store1.DataSource = data;
                Store1.DataBind();
                
                List<object> countries = new List<object>(10);
                for (int i = 1; i <= 10; i++)
                {
                    countries.Add(new {Text = "C"+i});
                }

                CountryStore.DataSource = countries;
                CountryStore.DataBind();
            }
        }
        
        protected void StatesRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            string country = e.Parameters["query"];
            
            List<object> states = new List<object>(10);
            for (int i = 1; i <= 10; i++)
            {
                states.Add(new { Text = country + "_S" + i });
            }

            StateStore.DataSource = states;
            StateStore.DataBind();
        }

        protected void CitiesRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            string state = e.Parameters["query"];

            List<object> cities = new List<object>(10);
            for (int i = 1; i <= 10; i++)
            {
                cities.Add(new { Text = state + "_C" + i });
            }

            CityStore.DataSource = cities;
            CityStore.DataBind();
        }

        protected void RegionsRefresh(object sender, StoreRefreshDataEventArgs e)
        {
            string city = e.Parameters["query"];

            List<object> regions = new List<object>(10);
            for (int i = 1; i <= 10; i++)
            {
                regions.Add(new { Text = city + "_R" + i });
            }

            RegionStore.DataSource = regions;
            RegionStore.DataBind();
        }
    </script>
    
    <script type="text/javascript">
        var beforeEdit = function(e){
             switch(e.field){
                case "state":
                    this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('country');
                    break;
                case "city":
                    this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('state');
                    break;
                case "region":
                    this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('city');
                    break;
             }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Store runat="server" ID="Store1">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="country" Type="String" Mapping="Country" />
                        <ext:RecordField Name="state" Type="String" Mapping="State" />
                        <ext:RecordField Name="city" Type="String" Mapping="City" />
                        <ext:RecordField Name="region" Type="String" Mapping="Region" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="CountryStore" runat="server">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="text" Type="String" Mapping="Text" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="StateStore" runat="server" OnRefreshData="StatesRefresh">
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="text" Type="String" Mapping="Text" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="CityStore" runat="server" OnRefreshData="CitiesRefresh">
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="text" Type="String" Mapping="Text" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="RegionStore" runat="server" OnRefreshData="RegionsRefresh">            
            <Proxy>
                <ext:DataSourceProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="text" Type="String" Mapping="Text" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:GridPanel runat="server" 
            Height="300" 
            Width="600"
            Title="Grid"
            StoreID="Store1"            
        >
            <ColumnModel>
                <Columns>
                    <ext:Column DataIndex="country" Header="Country">
                        <Editor>
                             <ext:ComboBox ID="CountryCombo" runat="server" 
                                Mode="Local" 
                                TriggerAction="All" 
                                StoreID="CountryStore" 
                                ValueField="text"
                                DisplayField="text">                                
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                    
                    <ext:Column DataIndex="state" Header="State">
                        <Editor>
                             <ext:ComboBox ID="StateCombo" runat="server" 
                                StoreID="StateStore" 
                                ValueField="text"
                                DisplayField="text">
                                <CustomConfig>
                                    <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                </CustomConfig>
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                    
                    <ext:Column DataIndex="city" Header="City">
                        <Editor>
                             <ext:ComboBox ID="CityCombo" runat="server" 
                                Mode="Remote" 
                                StoreID="CityStore" 
                                ValueField="text"
                                DisplayField="text">
                                <CustomConfig>
                                    <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                </CustomConfig>
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                    
                    <ext:Column DataIndex="region" Header="Region">
                        <Editor>
                             <ext:ComboBox ID="regionCombo" runat="server" 
                                Mode="Remote" 
                                StoreID="RegionStore" 
                                ValueField="text"
                                DisplayField="text">
                                <CustomConfig>
                                    <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                </CustomConfig>
                            </ext:ComboBox>
                        </Editor>
                    </ext:Column>
                </Columns>
            </ColumnModel>
            
            <View>
                <ext:GridView runat="server" ForceFit="true" />
            </View>
            
            <Listeners>
                <BeforeEdit Fn="beforeEdit" />
            </Listeners>
        </ext:GridPanel>
    
    </form>
</body>
</html>
