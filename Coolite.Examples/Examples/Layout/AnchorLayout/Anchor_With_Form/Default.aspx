<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AnchorLayout with Form - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />

        <h1>AnchorLayout with Form</h1>
        
        <ext:Window 
            runat="server"
            Title="Resize Me"
            Icon="ArrowOut"
            Width="500"
            Height="300"
            MinWidth="300"
            MinHeight="200"
            Plain="true" 
            BodyStyle="padding:5px;"
            ButtonAlign="Center">
            <Body>
                <ext:FormLayout runat="server" LabelWidth="55">
                    <Anchors>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="Send to" />
                        </ext:Anchor>
                        
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="Subject" />
                        </ext:Anchor>
                        
                        <ext:Anchor Horizontal="100%" Vertical="-53">
                            <ext:TextArea runat="server" HideLabel="true" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
            
            <Buttons>
                <ext:Button runat="server" Text="Save" Icon="Disk" />
                <ext:Button runat="server" Text="Cancel" Icon="Decline" />
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>
