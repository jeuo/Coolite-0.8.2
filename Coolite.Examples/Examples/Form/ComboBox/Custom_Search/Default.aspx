<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create Store
        Store store = new Store();

        // Create Proxy
        HttpProxy proxy = new HttpProxy();
        proxy.Method = HttpMethod.POST;
        proxy.Url = "Plants.ashx";

        // Create Reader
        JsonReader reader = new JsonReader();
        reader.Root = "plants";
        reader.TotalProperty = "totalCount";

        // Add Fields
        reader.Fields.Add(new RecordField("Common"));
        reader.Fields.Add(new RecordField("Botanical"));
        reader.Fields.Add(new RecordField("Light"));
        reader.Fields.Add(new RecordField("Price", RecordFieldType.Float));
        reader.Fields.Add(new RecordField("Indoor", RecordFieldType.Boolean));

        // Add Proxy and Reader to Store
        store.Proxy.Add(proxy);
        store.Reader.Add(reader);
        store.AutoLoad = false;

        // Add Store to Controls Collection
        this.Placeholder1.Controls.Add(store);
        
        
        // Create ComboBox
        ComboBox combobox = new ComboBox();

        combobox.StoreID = store.ClientID;
        combobox.DisplayField = "Common";
        combobox.ValueField = "Common";
        combobox.TypeAhead = false;
        combobox.LoadingText = "Searching...";
        combobox.Width = Unit.Pixel(570);
        combobox.PageSize = 10;
        combobox.HideTrigger = true;
        combobox.ItemSelector = "div.search-item";
        combobox.MinChars = 1;

        combobox.Template.Text = @"
               <tpl for=""."">
                  <div class=""search-item"">
                     <h3><span>${Price}</span>{Common}</h3>
                     {Botanical}
                  </div>
               </tpl>";

        // Add ComboBox to Controls Collection
        this.Placeholder1.Controls.Add(combobox);
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ComboBox with Template - Coolite Toolkit Example</title>
    <style type="text/css">
        .search-item {
            font: normal 11px tahoma, arial, helvetica, sans-serif;
            padding: 3px 10px 3px 10px;
            border: 1px solid #fff;
            border-bottom: 1px solid #eeeeee;
            white-space: normal;
            color: #555;
        }
        
        .search-item h3 {
            display: block;
            font: inherit;
            font-weight: bold;
            color: #222;
        }

        .search-item h3 span {
            float: right;
            font-weight: normal;
            margin: 0 0 5px 5px;
            width: 100px;
            display: block;
            clear: none;
        } 
        
        p { width: 650px; }
        
        .ext-ie .x-form-text { position: static !important; }
    </style>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <p>
            <b>Combo with Templates and Ajax</b><br />
            This is a more advanced example that shows how you can combine paging, Template to create a "live search" feature.
        </p>
        
        <ext:Store ID="Store1" runat="server" AutoLoad="false">
            <Proxy>
                <ext:HttpProxy Method="POST" Url="Plants.ashx" />
            </Proxy>
            <Reader>
                <ext:JsonReader Root="plants" TotalProperty="totalCount" >
                    <Fields>
                        <ext:RecordField Name="Common" />
                        <ext:RecordField Name="Botanical" />
                        <ext:RecordField Name="Light" />
                        <ext:RecordField Name="Price" Type="Float" />
                        <ext:RecordField Name="Indoor" Type="Boolean" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <div style="width:600px;">
            <div class="x-box-tl"><div class="x-box-tr"><div class="x-box-tc"></div></div></div>
            <div class="x-box-ml"><div class="x-box-mr"><div class="x-box-mc">
                <h3 style="margin-bottom:5px;">Search the plants</h3>
                
            <ext:ComboBox 
                ID="ComboBox1"
                runat="server" 
                StoreID="Store1"
                DisplayField="Common" 
                ValueField="Common"
                TypeAhead="false"
                LoadingText="Searching..." 
                Width="570"
                PageSize="10"
                HideTrigger="true"
                ItemSelector="div.search-item"        
                MinChars="1">
                <Template runat="server">
                   <tpl for=".">
                      <div class="search-item">
                         <h3><span>${Price}</span>{Common}</h3>
                         {Botanical}
                      </div>
                   </tpl>
                </Template>
            </ext:ComboBox>    
            
            <div style="padding-top:4px;">
                Plants search (type '*' (asterisk) for showing all)
            </div>
            </div></div></div>
            <div class="x-box-bl"><div class="x-box-br"><div class="x-box-bc"></div></div>
        </div>
            
        <br />
        <br />
        <br />
            
        <div style="width:600px;">
            <div class="x-box-tl">
                <div class="x-box-tr">
                    <div class="x-box-tc"></div>
                </div>
            </div>
            <div class="x-box-ml">
                <div class="x-box-mr">
                    <div class="x-box-mc">
                        <h3 style="margin-bottom:5px;">Search the plants (controls dynamically created)</h3>
                        <asp:PlaceHolder ID="Placeholder1" runat="server" />
                        <div style="padding-top:4px;">
                            Plants search (type '*' (asterisk) for showing all)
                        </div>
                    </div>
                </div>
            </div>
            <div class="x-box-bl">
                <div class="x-box-br">
                    <div class="x-box-bc"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
