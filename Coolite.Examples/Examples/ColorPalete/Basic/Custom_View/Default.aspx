<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ColorPalate Overview - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .x-color-palette {
            width: 350px !important;
        }
    </style>  
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:ColorPalette runat="server">
            <Template runat="server">
                <tpl for=".">
                    <a href="#" class="color-{.}" hidefocus="on">
                        <em style="padding:2px;">
                            <span style="background:#{.}; width:50px; height:20px; border:1px solid black;" unselectable="on">
                                &#160;
                            </span>
                            <div style="font-size:10px;text-align:center;">#{.}</div>
                        </em>
                    </a>
                </tpl>
            </Template>
        </ext:ColorPalette>
    </form>
</body>
</html>
