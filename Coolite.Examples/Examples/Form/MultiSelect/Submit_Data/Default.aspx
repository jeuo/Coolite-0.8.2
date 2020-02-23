<%@ Page Language="C#" %>
<%@ Import Namespace="System.Xml"%>
<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        //JSON representation
        string multi1 = e.ExtraParams["multi1"];
        string multi2 = e.ExtraParams["multi2"];

        // Array of ListItems
        Coolite.Ext.Web.ListItem[] items1 = JSON.Deserialize<Coolite.Ext.Web.ListItem[]>(multi1);
        Coolite.Ext.Web.ListItem[] items2 = JSON.Deserialize<Coolite.Ext.Web.ListItem[]>(multi2);

        StringBuilder sb = new StringBuilder(256);

        sb.Append("<h2>Results</h2>");

        sb.Append("<h3>As ListItems</h3>");
        
        foreach (Coolite.Ext.Web.ListItem item in items1)
        {
            sb.AppendFormat("Value: {0}<br />", item.Value);
        }
        
        // XML representation
        XmlNode multi1Xml = JSON.DeserializeXmlNode("{items:{item:" + multi1 + "}}");
        XmlNode multi2Xml = JSON.DeserializeXmlNode("{items:{item:" + multi2 + "}}");

        sb.Append("<h3>As XML</h3>");

        //foreach (XmlNode node in multi1)
        //{
        //    sb.AppendFormat("Value: {0}<br />", node.Value);
        //}
        
        // Array of Dictionaries
        Dictionary<string, string>[] multi1Json = JSON.Deserialize<Dictionary<string, string>[]>(multi1);
        Dictionary<string, string>[] multi2Json = JSON.Deserialize<Dictionary<string, string>[]>(multi2);

        sb.Append("<h3>As XML</h3>");

        //foreach (XmlNode node in multi1)
        //{
        //    sb.AppendFormat("Value: {0}<br />", node.Value);
        //}

        this.Label1.Html = sb.ToString();
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Submit MultiSelect Values - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Panel runat="server" Title="MultiSelects" BodyStyle="padding:10px;" Width="660">
            <Body>
                <ext:ColumnLayout ID="TableLayout1" runat="server">
                    <ext:LayoutColumn ColumnWidth="0.5">
                        <ext:Panel ID="Panel1" runat="server" Border="false" BodyStyle="height: 260px;">
                            <Body>
                                <ext:MultiSelect ID="MultiSelect1" runat="server" Legend="MultiSelect1" Width="300" Height="250">
                                    <Items>
                                        <ext:ListItem Text="Item 1" Value="1" />
                                        <ext:ListItem Text="Item 2" Value="2" />
                                        <ext:ListItem Text="Item 3" Value="3" />
                                        <ext:ListItem Text="Item 4" Value="4" />
                                        <ext:ListItem Text="Item 5" Value="5" />
                                    </Items>
                                </ext:MultiSelect>      
                            </Body>
                        </ext:Panel>                                                             
                    </ext:LayoutColumn>
                    
                    <ext:LayoutColumn ColumnWidth="0.5">
                        <ext:Panel ID="Panel2" runat="server" Border="false" BodyStyle="height: 260px;">
                            <Body>
                                <ext:MultiSelect ID="MultiSelect2" runat="server" Legend="MultiSelect2" Width="300" Height="250">
                                    <Items>
                                        <ext:ListItem Text="Item 1" Value="1" />
                                        <ext:ListItem Text="Item 2" Value="2" />
                                        <ext:ListItem Text="Item 3" Value="3" />
                                        <ext:ListItem Text="Item 4" Value="4" />
                                        <ext:ListItem Text="Item 5" Value="5" />
                                    </Items>
                                </ext:MultiSelect>      
                            </Body>
                        </ext:Panel>                                                             
                    </ext:LayoutColumn>
                </ext:ColumnLayout>                        
            </Body>
            <Buttons>
                 <ext:Button runat="server" Text="Submit MultiSelects">
                    <AjaxEvents>
                        <Click OnEvent="Button1_Click">
                            <ExtraParams>
                                <ext:Parameter Name="multi1" Value="Ext.encode(#{MultiSelect1}.getValues())" Mode="Raw" />
                                <ext:Parameter Name="multi2" Value="Ext.encode(#{MultiSelect2}.getValues(true))" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </AjaxEvents>
                </ext:Button>
            </Buttons>
        </ext:Panel>
        
        <ext:Label ID="Label1" runat="server" />
    </form>
</body>
</html>
