<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Store1.DataSource = new object[]
        {
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"},
            new object[] {"Test"}
        };

        this.Store1.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Marking changed GridPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />  
    
    <style type="text/css">
        .dirty-row {
	        background:#FFFDD8;
        }
        .new-row {
	        background:#c8ffc8;
        } 
    </style>
</head>
<body>    
    <form id="form1" runat="server">
        <script type="text/javascript">
            function getRowClass(record) {
                if (record.newRecord) {
                    return 'new-row';
                }
                if (record.dirty) {
                    return 'dirty-row';
                }
            }

            function insertRecord() {
                var grid = <%= GridPanel1.ClientID %>;
                grid.insertRecord(0, {});
                grid.getView().focusRow(0);
                grid.startEditing(0, 0);
            }
        </script>  
        
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Marking Records</h1>
        <p>Demonstrates how to select the records  by color (just edit any cell or insert new record).</p>
       
        <ext:Store ID="Store1" runat="server">
            <Reader>
                <ext:ArrayReader>
                    <Fields>
                        <ext:RecordField Name="TestCell" />                       
                    </Fields>
                </ext:ArrayReader>
            </Reader>
        </ext:Store>
        
        <ext:GridPanel 
            ID="GridPanel1" 
            runat="server" 
            StoreID="Store1" 
            StripeRows="true" 
            ClicksToEdit="1"
            Title="Test Grid" 
            Width="600"  
            Height="350"
            AutoExpandColumn="TestCell">
            <ColumnModel runat="server">
                <Columns>
                    <ext:Column ColumnID="TestCell" Header="TestCell" DataIndex="TestCell">
                        <Editor>
                            <ext:TextField ID="TextBox1" runat="server" />
                        </Editor>
                    </ext:Column>                    
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ID="GridView1" runat="server" >
                    <GetRowClass Fn="getRowClass" />                       
                </ext:GridView>
            </View>
            <Buttons>
                <ext:Button runat="server" Text="Insert record" Icon="Add">
                    <Listeners>
                        <Click Fn="insertRecord" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:GridPanel>  
    </form>
</body>
</html>