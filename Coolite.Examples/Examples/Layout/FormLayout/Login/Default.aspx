<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void btnLogin_Click(object sender, AjaxEventArgs e)
    {
        this.Window1.Hide();

        string template = "<br /><b>LOGIN SUCCESS</b><br /><br />Username: {0}<br />Password: {1}";
        string username = this.txtUsername.Text;
        string password = this.txtPassword.Text;
        this.lblMessage.Html = string.Format(template, username, password);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Simple Login Form with Ajax Submit - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
            
        <h1>Simple Login Form with Ajax Submit</h1>
        
        <ext:Button 
            ID="Button1" 
            runat="server" 
            Text="Logout" 
            Icon="LockOpen">
            <Listeners>
                <Click Handler="#{Window1}.show();" />
            </Listeners>    
        </ext:Button>
        
        <ext:Label ID="lblMessage" runat="server" />
        
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
                    <Anchors>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField 
                                ID="txtUsername" 
                                runat="server" 
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
                                InputType="Password" 
                                FieldLabel="Password" 
                                AllowBlank="false" 
                                BlankText="Your password is required."
                                Text="Demo"
                                />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button ID="btnLogin" runat="server" Text="Login" Icon="Accept">
                    <Listeners>
                        <Click Handler="
                            if(!#{txtUsername}.validate() || !#{txtPassword}.validate()) {
                                Ext.Msg.alert('Error','The Username and Password fields are both required');
                                // return false to prevent the btnLogin_Click Ajax Click event from firing.
                                return false; 
                            }" />
                    </Listeners>
                    <AjaxEvents>
                        <Click OnEvent="btnLogin_Click">
                            <EventMask ShowMask="true" Msg="Verifying..." MinDelay="500" />
                        </Click>
                    </AjaxEvents>
                </ext:Button>
                <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Decline">
                    <Listeners>
                        <Click Handler="el.ownerCt.hide();#{lblMessage}.setText('LOGIN CANCELED')" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>


