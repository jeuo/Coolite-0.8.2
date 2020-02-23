<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Button1_Click(object sender, EventArgs e)
    {
        //PropertyGrid1.Source contains changed data
        //you can handle this data: read, save and etc

        StringBuilder html = new StringBuilder();
        html.Append("<table class=\"data\">");
        html.Append("<tr><th>Name</th><th>Value</th></tr>");

        // Loop through all PropertyGridParameters and reference by Index. 

        foreach (PropertyGridParameter param in this.PropertyGrid1.Source)
        {
            html.Append("<tr>");
            html.Append("<td>" + param.Name + "</td>");
            html.Append("<td>" + param.Value.ToString() + "</td>");
            html.Append("</tr>");
        }
        html.Append("</table>");

        this.Label1.Html = html.ToString();

        // Data can be referenced by the PropertyGridParameter 
        // "Name" value as well.
        // string name = this.PropertyGrid1.Source["(name)"].Value;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PropertyGrid with Button AjaxEvent saving - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .data th {
        	font-weight: bold;
        }
        
        .data th, .data td {
            padding: 4px;
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>PropertyGrid with Button AjaxEvent Saving</h1>
        
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
                        <ext:DateField runat="server" />
                    </Editor>
                </ext:PropertyGridParameter>
                <ext:PropertyGridParameter Name="tested" Value="false" Mode="Raw" />
                <ext:PropertyGridParameter Name="version" Value="0.01" />
                <ext:PropertyGridParameter Name="borderWidth" Value="1+4" Mode="Raw" />
            </Source>
            <View>
                <ext:GridView ForceFit="true" ScrollOffset="2" runat="server" />
            </View>
            <Buttons>
                <ext:Button runat="server" ID="Button1" Text="Save" Icon="Disk">
                    <AjaxEvents>
                        <Click OnEvent="Button1_Click" />
                    </AjaxEvents>
                </ext:Button>                
            </Buttons>           
        </ext:PropertyGrid>
        
        <p><ext:Label ID="Label1" runat="server" /></p>
    </form>

</body>
</html>