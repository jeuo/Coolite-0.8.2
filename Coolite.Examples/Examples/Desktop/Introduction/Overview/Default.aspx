<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Desktop - Coolite Toolkit Examples</title>    
    
    <script runat="server">
        protected void Button1_Click(object sender, AjaxEventArgs e)
        {
            // Do some Authentication...
            
            // Then user send to application
            Response.Redirect("Desktop.aspx");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="150" 
            Icon="Lock" 
            Title="Login"
            Draggable="false"
            Width="350"
            Modal="true"
            BodyStyle="padding:5px;">
            <Body>
                <ext:FormLayout ID="FormLayout1" runat="server">
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField 
                            ID="txtUsername" 
                            runat="server" 
                            ReadOnly="true"
                            FieldLabel="Username" 
                            AllowBlank="false"
                            BlankText="Your username is required."
                            Text="Demo"
                            />
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:TextField 
                            ID="txtPassword" 
                            runat="server" 
                            ReadOnly="true"
                            InputType="Password" 
                            FieldLabel="Password" 
                            AllowBlank="false" 
                            BlankText="Your password is required."
                            Text="Demo"
                            />
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Login" Icon="Accept">
                    <AjaxEvents>
                        <Click OnEvent="Button1_Click" Success="Window1.close();">
                            <EventMask ShowMask="true" Msg="Verifying..." MinDelay="1000" />
                        </Click>
                    </AjaxEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>