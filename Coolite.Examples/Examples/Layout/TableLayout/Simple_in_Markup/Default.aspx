<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple TableLayout in Markup - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #pnlTableLayout .x-table-layout {
            padding:5px;
        }
        
        #pnlTableLayout .x-table-layout td {
            font-size:11px;
            padding:5px;
            vertical-align:top;
        }
    </style>
</head>
<body>
    <ext:ScriptManager runat="server" />

    <ext:ViewPort ID="ViewPort1" runat="server">
        <Body>
            <ext:BorderLayout ID="BorderLayout1" runat="server">
                <Center>
                    <ext:Panel
                        id="pnlTableLayout"
                        runat="server" 
                        Title="Table Layout"
                        Border="false"
                        BodyStyle="padding:15px;">
                        <Body>
                            <ext:TableLayout runat="server" Columns="4">
                                <ext:Cell RowSpan="3">
                                    <ext:Panel 
                                        ID="Panel1" 
                                        runat="server" 
                                        Title="Lots of Spanning" 
                                        BodyStyle="padding:15px;"
                                        Html="<p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p>"
                                        />
                                </ext:Cell>
                                <ext:Cell>
                                    <ext:Panel 
                                        ID="Panel2" 
                                        runat="server" 
                                        Title="Basic Table Cell" 
                                        BodyStyle="padding:15px;"
                                        Html="<p>Basic panel in a table cell.</p>"
                                        />
                                </ext:Cell>
                                <ext:Cell>
                                    <ext:Panel 
                                        ID="Panel3" 
                                        runat="server" 
                                        Header="false"
                                        BodyStyle="padding:15px;"
                                        Html="<p>Plain panel</p>"
                                        />
                                </ext:Cell>  
                                <ext:Cell RowSpan="2">
                                    <ext:Panel 
                                        ID="Panel4" 
                                        runat="server" 
                                        Title="Another Cell"
                                        Width="300"
                                        BodyStyle="padding:15px;"
                                        Html="<p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p><br /><p>Row spanning.</p>"
                                        />
                                </ext:Cell>    
                                <ext:Cell ColSpan="2">
                                    <ext:Panel 
                                        ID="Panel5" 
                                        runat="server" 
                                        Header="false"
                                        BodyStyle="padding:15px;"
                                        Html="Plain cell spanning two columns"
                                        />
                                </ext:Cell>                                      
                                <ext:Cell ColSpan="3">
                                    <ext:Panel 
                                        ID="Panel6" 
                                        runat="server" 
                                        Title="More Column Spanning"
                                        BodyStyle="padding:15px;"
                                        Html="<p>Spanning three columns.</p>"
                                        />
                                </ext:Cell> 
                                <ext:Cell ColSpan="4">
                                    <ext:Panel 
                                        ID="Panel7" 
                                        runat="server" 
                                        Title="Spanning All Columns"
                                        BodyStyle="padding:15px;"
                                        Html="<p>Spanning all columns.</p>"
                                        />
                                </ext:Cell>                                     
                            </ext:TableLayout>
                        </Body>
                    </ext:Panel>
                </Center>
            </ext:BorderLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>