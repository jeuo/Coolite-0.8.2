<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Items Actions</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                List<object> list = new List<object>
                                        {
                                            new {Text = "Text3", Value = 3},
                                            new {Text = "Text4", Value = 4},
                                            new {Text = "Text5", Value = 5}
                                        };
                Store1.DataSource = list;
                Store1.DataBind();
                //Please note that inner items will be above store's items
                ComboBox1.Items.Insert(0,new Coolite.Ext.Web.ListItem("None", "-"));
                ComboBox1.SelectedItem.Value = "-";
            }
        }
        
        protected void InsertRecord(object sender, AjaxEventArgs e)
        {
            Dictionary<string, string> values = new Dictionary<string, string>(2);
            values.Add("Text", "Text0");
            values.Add("Value", "0");
            ComboBox1.InsertRecord(1, values);
            ComboBox1.SelectedItem.Value = "0";
        }

        protected void InsertRecord2(object sender, AjaxEventArgs e)
        {
            ComboBox2.InsertItem(0, "Text0","0");
            ComboBox2.SelectedItem.Value = "0";
        }
    </script> 
    
    
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Store runat="server" ID="Store1">          
            <Reader>
                <ext:JsonReader ReaderID="Value">
                    <Fields>
                        <ext:RecordField Name="Text" />
                        <ext:RecordField Name="Value" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <p>1. Combo with a store and inner items (merge mode)</p>
        <br />
        <ext:ComboBox ID="ComboBox1" runat="server" 
            StoreID="Store1" 
            DisplayField="Text" 
            ValueField="Value"
            Mode="Local"
            >
            <Items>
                <ext:ListItem Text="Text2" Value="2" />
            </Items>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" Qtip="Remove selected" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="this.removeByValue(this.getValue());this.clearValue();" />
            </Listeners>
        </ext:ComboBox>
        <br />
        <ext:Panel ID="Panel1" runat="server" Border="false">
            <Body>
                <ext:TableLayout ID="TableLayout1" runat="server" Columns="2">
                    <ext:Cell>
                        <ext:Button ID="Button1" runat="server" Text="Insert: client side">
                            <Listeners>
                                <Click Handler="#{ComboBox1}.insertRecord(1, {Text:'Text1', Value:1});#{ComboBox1}.setValue(1);" />
                            </Listeners>
                        </ext:Button>
                    </ext:Cell>
                    
                    <ext:Cell>
                        <ext:Button ID="Button2" runat="server" Text="Insert: server side">
                            <AjaxEvents>
                                <Click OnEvent="InsertRecord"></Click>
                            </AjaxEvents>
                        </ext:Button>
                    </ext:Cell>
                </ext:TableLayout>
            </Body>
        </ext:Panel>
        
        <br />
        <br />
        <p>2. Combo with inner items</p>
        <br />
        <%--please note that the insertRecord function works with inner items also--%>
        <ext:ComboBox ID="ComboBox2" runat="server">
            <Items>
                <ext:ListItem Text="Text2" Value="2" />
                <ext:ListItem Text="Text3" Value="3" />
                <ext:ListItem Text="Text4" Value="4" />
                <ext:ListItem Text="Text5" Value="5" />                
            </Items>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" Qtip="Remove selected" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="this.removeByValue(this.getValue());this.clearValue();" />
            </Listeners>
        </ext:ComboBox>
        <br />
        <ext:Panel ID="Panel2" runat="server" Border="false">
            <Body>
                <ext:TableLayout ID="TableLayout2" runat="server" Columns="2">
                    <ext:Cell>
                        <ext:Button ID="Button3" runat="server" Text="Insert: client side">
                            <Listeners>
                                <Click Handler="#{ComboBox2}.insertItem(0, 'Text1', 1);#{ComboBox2}.setValue(1);" />
                            </Listeners>
                        </ext:Button>
                    </ext:Cell>
                    
                    <ext:Cell>
                        <ext:Button ID="Button4" runat="server" Text="Insert: server side">
                            <AjaxEvents>
                                <Click OnEvent="InsertRecord2"></Click>
                            </AjaxEvents>
                        </ext:Button>
                    </ext:Cell>
                </ext:TableLayout>
            </Body>
        </ext:Panel>
        <br />
        <h3>Other functions:</h3>
        <ul>
            <li>addRecord: function(values)</li>
            <li>addItem: function(text, value)</li>
            <li>insertRecord: function(rowIndex, values)</li>
            <li>insertItem: function(rowIndex, text, value)</li>
            <li>removeByField: function(field, value)</li>
            <li>removeByIndex: function(index)</li>
            <li>removeByValue: function(value)</li>
            <li>removeByText: function(text)</li>
        </ul>
    </form>
</body>
</html>