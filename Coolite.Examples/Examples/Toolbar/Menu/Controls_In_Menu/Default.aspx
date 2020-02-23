<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
     {
        string[][] data = new string[10][];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new string[5];
            for (int j = 0; j < data[i].Length; j++)
            {
                data[i][j] = string.Format("[{0},{1}]", i+1, j+1);
            }
        }
        this.Store1.DataSource = data;
        this.Store1.DataBind();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coolite Example</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var cellSelect = function (grid, rowIndex, colIndex, textField, ctxMenu) {
            var record = grid.store.getAt(rowIndex);
            var name = grid.getColumnModel().getDataIndex(colIndex);
            var value = record.get(name);

            textField.setValue(value);
            ctxMenu.hide();
        }

        function refreshPanel(panel, force) {
            if (!panel.loaded || force) {
                panel.body.update('<iframe frameborder="0" height="100%" width="100%" src="http://www.coolite.com/"></iframe>');
                panel.loaded = true;
            }
        }
    </script>
    <style type="text/css">
        .shift
        {
        	padding-left:24px;   
        }
        
        .lst
        {
        	z-index:99999;
        }
    </style>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
    
    <ext:Store ID="Store1" runat="server" >
        <Reader>
            <ext:ArrayReader>
                <Fields>
                    <ext:RecordField Name="Col1" />
                    <ext:RecordField Name="Col2" />
                    <ext:RecordField Name="Col3" />
                    <ext:RecordField Name="Col4" />
                    <ext:RecordField Name="Col5" />
                </Fields>
            </ext:ArrayReader>
        </Reader>
    </ext:Store>    
      
    <div class="x-hide-display">
        <ext:GridPanel 
            ID="GridPanel1" 
            runat="server" 
            StoreID="Store1"
            EnableHdMenu="false"
            Border="false"
            Width="420"
            Height="240">
            <ColumnModel ID="ColumnModel1" runat="server">
		        <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column Header="Column №1" Width="75" Sortable="true" DataIndex="Col1" />
                    <ext:Column Header="Column №2" Width="75" Sortable="true" DataIndex="Col2" />
                    <ext:Column Header="Column №3" Width="75" Sortable="true" DataIndex="Col3" />
                    <ext:Column Header="Column №4" Width="75" Sortable="true" DataIndex="Col4" />
                    <ext:Column Header="Column №5" Width="75" Sortable="true" DataIndex="Col5" />
		        </Columns>
            </ColumnModel>             
            <Listeners>
                <CellClick Handler="cellSelect(this, rowIndex, columnIndex, #{TextField1}, #{ContextMenu});" />
            </Listeners>  
        </ext:GridPanel>
        
        <%--Property Grid--%>
        <ext:PropertyGrid 
            ID="PropertyGrid1" 
            runat="server" 
            Width="300" 
            AutoHeight="true" >
            <Source>
                <ext:PropertyGridParameter Name="(name)" Value="Properties Grid" />
                <ext:PropertyGridParameter Name="grouping" Value="false" Mode="Raw" />
                <ext:PropertyGridParameter Name="autoFitColumns" Value="true" Mode="Raw" />
                <ext:PropertyGridParameter Name="productionQuality" Value="false" Mode="Raw" />
                <ext:PropertyGridParameter Name="created" Value="10/15/2006">
                    <Editor>
                        <ext:DateField ID="DateField1" runat="server" HideTrigger="true">
                        </ext:DateField>
                    </Editor>
                </ext:PropertyGridParameter>
                <ext:PropertyGridParameter Name="tested" Value="false" Mode="Raw" />
                <ext:PropertyGridParameter Name="version" Value="0.01" />
                <ext:PropertyGridParameter Name="borderWidth" Value="5" Mode="Raw" />
            </Source>
            <View>
                <ext:GridView ID="GridView1" ForceFit="true" ScrollOffset="2" runat="server" />
            </View>
            <Buttons>
                <ext:Button runat="server" ID="Button1" Text="Save" Icon="Disk">                    
                </ext:Button>                
            </Buttons>           
        </ext:PropertyGrid>
        
        <%--Form controls--%>
        <ext:TextField ID="TextField2" runat="server" Width="200" />
        
        <ext:TextArea ID="TextArea1" runat="server" Width="200" Height="100" />         
        
        <ext:FieldSet ID="pnlAccount" runat="server" Title="Account Information">
            <Body>
                <ext:TextField ID="txtTest" runat="server" Width="170" />    <br/>            
                <ext:TextField ID="TextField3" runat="server" Width="170" />                
            </Body>
        </ext:FieldSet>         
        
        <%--Panels controls--%>
        <ext:Panel ID="Panel1" runat="server" 
            Title="Coolite Site (lazy loading)"   
            Width="300" 
            Height="200">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarFill />
                        <ext:ToolbarButton runat="server" Text="Reload" Icon="ArrowRefreshSmall">
                            <Listeners>
                                <Click Handler="refreshPanel(#{Panel1}, true);" />
                            </Listeners>
                        </ext:ToolbarButton>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:Panel>
            
       <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0" Width="300" Height="100">
            <Tabs>
                <ext:Tab runat="server" Title="Tab1" Icon="Tab">
                    <Body>
                        <ext:Button runat="server" Text="Close Menu" Icon="Door">
                            <Listeners>
                                <Click Handler="#{PanelsMenu}.hide();" />
                            </Listeners>
                        </ext:Button>                        
                    </Body>
                </ext:Tab>
                <ext:Tab runat="server" Title="Tab2" Icon="Tab"></ext:Tab>
                <ext:Tab runat="server" Title="Tab3" Icon="Tab"></ext:Tab>
            </Tabs>
       </ext:TabPanel>
       
       <%--Layouts--%>
       <ext:Panel ID="BorderLPanel" runat="server" Width="300" Height="200">
           <Body>
                <ext:BorderLayout runat="server">
                    <West Split="true">
                        <ext:Panel runat="server" Collapsible="true" Title="West" Width="100" />
                    </West>
                    <Center>
                        <ext:Panel runat="server" Title="Center" />
                    </Center>
                </ext:BorderLayout>
           </Body>
       </ext:Panel>
       <ext:Panel ID="AccordionPanel" runat="server" Width="300" Height="200">
           <Body>
               <ext:Accordion runat="server">
                  <ext:Panel runat="server" Title="Panel1" Collapsed="false" />
                  <ext:Panel runat="server" Title="Panel2" />
                  <ext:Panel runat="server" Title="Panel3" />
               </ext:Accordion>
           </Body>
       </ext:Panel>
    </div>
    
    <ext:Menu ID="ContextMenu" runat="server">
        <Items>
            <ext:ElementMenuItem ID="ElementMenuItem1" runat="server" Target="#{GridPanel1}" Shift="false" />                            
        </Items>
        <Listeners>
            <Show Handler="#{GridPanel1}.syncSize();" />
        </Listeners>
    </ext:Menu>
    
    <h1>Menus with controls</h1>
    <p>1. Click the right button on the text field for select value</p>
    
    <ext:TextField ID="TextField1" runat="server" Width="200" ContextMenuID="ContextMenu" ReadOnly="true" />
    <br/>
    <br/>
        
    <p>2. See menu in the toolbar</p>
    <ext:Toolbar runat="server" Width="500">
        <Items>
            <ext:ToolbarButton runat="server" Text="Form controls" Icon="NoteEdit">
                <Menu>
                    <ext:Menu runat="server">
                        <Items>
                            <ext:MenuItem runat="server" Icon="NoteEdit" Text="Item" />
                            <ext:ElementMenuItem Target="#{TextField2}" />
                            <ext:MenuSeparator />
                            <ext:ElementMenuItem Target="#{TextArea1}" />
                            <ext:MenuSeparator />
                            <ext:ComboMenuItem runat="server">
                                <ComboBox runat="server" LazyInit="false" ID="ComboBox1" CtCls="shift" Width="200" ReadOnly="true">
                                    <Items>
                                        <ext:ListItem Text="Text1" />
                                        <ext:ListItem Text="Text2" />
                                        <ext:ListItem Text="Text3" />
                                        <ext:ListItem Text="Text4" />
                                        <ext:ListItem Text="Text5" />
                                    </Items>
                                    <SelectedItem Value="Text4" />
                                </ComboBox>                            
                            </ext:ComboMenuItem>
                            <ext:MenuSeparator />
                            <ext:ElementMenuItem Target="#{pnlAccount}" />
                        </Items>
                    </ext:Menu>
                </Menu>
            </ext:ToolbarButton>
            
            <ext:ToolbarButton ID="ToolbarButton1" runat="server" Text="Panels" Icon="Application">
                <Menu>
                    <ext:Menu ID="PanelsMenu" runat="server">
                        <Items>
                            <ext:ElementMenuItem Target="#{Panel1}" Shift="false" />
                            <ext:MenuSeparator />
                            <ext:ElementMenuItem Target="#{TabPanel1}" Shift="false" />
                        </Items>
                        <Listeners>
                            <Show Handler="refreshPanel(#{Panel1}, false);" />
                        </Listeners>
                    </ext:Menu>
                </Menu>
            </ext:ToolbarButton>
            <ext:ToolbarButton ID="ToolbarButton2" runat="server" Text="Property Grid" Icon="Table">
                <Menu>
                    <ext:Menu ID="Menu1" runat="server">
                        <Items>
                            <ext:ElementMenuItem Target="#{PropertyGrid1}" Shift="false" />
                        </Items>                                                
                    </ext:Menu>
                </Menu>
            </ext:ToolbarButton>
            <ext:ToolbarButton ID="ToolbarButton3" runat="server" Text="Layouts" Icon="Layout">
                <Menu>
                    <ext:Menu ID="Menu2" runat="server">
                        <Items>
                            <ext:ElementMenuItem Target="#{BorderLPanel}" Shift="false" />
                            <ext:ElementMenuItem Target="#{AccordionPanel}" Shift="false" />
                        </Items>                        
                    </ext:Menu>
                </Menu>
            </ext:ToolbarButton>
        </Items>
    </ext:Toolbar>    
</body>
</html>
