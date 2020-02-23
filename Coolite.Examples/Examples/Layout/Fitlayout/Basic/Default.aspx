<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FitLayout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
    
        <ext:ViewPort runat="server">
            <Body>
                <ext:FitLayout runat="server">
                    <Items>
                        <ext:Panel 
                            runat="server" 
                            BodyStyle="padding:15px;" 
                            Title="Inner panel"
                            Html="This panel is fit 100% Height and 100% Width within its container."
                            />
                    </Items>
                </ext:FitLayout>
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>
