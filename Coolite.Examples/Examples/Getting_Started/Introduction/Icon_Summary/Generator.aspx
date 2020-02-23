<%@ Page Language="C#" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> icons = Enum.GetNames(typeof(Icon)).ToList<string>();

        icons.Remove("None");

        List<object> data = new List<object>(icons.Count);

        icons.ForEach(icon => data.Add(
            new
            {
                name = icon,
                url = this.ScriptManager1.GetIconUrl((Icon)Enum.Parse(typeof(Icon), icon))
            }
        ));

        this.Store1.DataSource = data;
        this.Store1.DataBind();
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Icons - Coolite Toolkit Examples</title>
    <style type="text/css">
        body {
            padding: 20px;
            font:normal 11px arial,helvetica,sans-serif;
        }
        
        .thumb-wrap {
            float: left;
            width: 150px;
            height: 22px;
            color: #333;
        }
        
        .thumb-wrap img {
            width: 16px;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Store runat="server" ID="Store1" AutoLoad="true">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="name" />
                    <ext:RecordField Name="url" />      
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>        

    <ext:DataView 
        ID="DataView1" 
        runat="server"
        StoreID="Store1"
        AutoHeight="true"
        ItemSelector="div.thumb-wrap">
        <Template ID="Template1" runat="server">
            <tpl for=".">
                <div class="thumb-wrap" id="{name}">
                    <div class="thumb"><img src="{url}" title="{name}">&nbsp;{name}</div>
                </div>
            </tpl>
            <div class="x-clear"></div>                            
        </Template>                         
    </ext:DataView>
</body>
</html>