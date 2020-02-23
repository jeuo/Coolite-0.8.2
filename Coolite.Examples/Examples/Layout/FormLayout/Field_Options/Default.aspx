<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>FormLayout Overview - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .blueborder {
        	border  : dotted 1px blue;
        	padding : 1px 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
            
        <ext:Panel runat="server" Frame="true" Title="Label Style and Separator" Width="400">
            <Body>
                <ext:FormLayout runat="server" LabelSeparator="-" LabelStyle="color:red;" LabelWidth="55">
                    <Anchors>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" LabelStyle="color:blue;" LabelSeparator="+" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
        </ext:Panel>
        
        <br />
        
        <ext:Panel ID="Panel1" runat="server" Frame="true" Title="Without labels" Width="400">
            <Body>
                <ext:FormLayout runat="server" HideLabels="true">
                    <Anchors>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
        </ext:Panel>
    
        <br />
        
        <ext:Panel ID="Panel2" runat="server" Frame="true" Title="Item style" Width="400">
            <Body>
                <ext:FormLayout runat="server" ItemCls="blueborder">
                    <Anchors>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
        </ext:Panel>
        
        <br />
        
        <ext:Panel ID="Panel3" runat="server" Frame="true" Title="Label Top Align" Width="400">
            <Body>
                <ext:FormLayout runat="server" LabelAlign="Top">
                    <Anchors>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField ID="TextField1" runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="95%">
                            <ext:TextField ID="TextField2" runat="server" FieldLabel="Label" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
        </ext:Panel>
    </form>
</body>
</html>


