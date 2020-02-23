<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ColumnLayout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <ext:ViewPort runat="server">
        <Body>
            <ext:ColumnLayout runat="server" Split="true" FitHeight="true">
                <Columns>
                    <ext:LayoutColumn ColumnWidth="0.25">
                        <ext:Panel runat="server" Title="Width=0.25">
                            <Body>
                                This is some content.
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                    
                    <ext:LayoutColumn ColumnWidth="0.75">
                        <ext:Panel runat="server" Title="Width=0.75">
                            <Body>
                                This is some content.
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                    
                    <ext:LayoutColumn>
                        <ext:Panel runat="server" Title="Width=250px" Width="250">
                            <Body>
                                This is some content.
                            </Body>
                        </ext:Panel>
                    </ext:LayoutColumn>
                </Columns>
            </ext:ColumnLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>
