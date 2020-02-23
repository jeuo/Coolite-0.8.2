<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<script runat="server">
    [AjaxMethod]
    public void DoConfirm()
    {
        // Manually configure Handler...
        //Ext.Msg.Confirm("Message", "Confirm?", "if(buttonId == 'yes') { CompanyX.DoYes(); } else { CompanyX.DoNo(); }").Show();

        // Configure individualock Buttons using a ButtonsConfig...
        Ext.Msg.Confirm("Message", "Confirm?", new MessageBox.ButtonsConfig
        {
            Yes = new MessageBox.ButtonConfig
            {
                Handler = "CompanyX.DoYes()",
                Text = "Yes Please"
            },
            No = new MessageBox.ButtonConfig
            {
                Handler = "CompanyX.DoNo()",
                Text = "No Thanks"
            }
        }).Show();
    }

    [AjaxMethod]
    public void DoYes()
    {
        this.Label1.Text = "YES";
    }

    [AjaxMethod]
    public void DoNo()
    {
        this.Label1.Text = "NO";
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Confirm MessageBox with ButtonsConfig - Coolite Toolkit Examples</title>   
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <ext:ScriptManager runat="server" AjaxMethodNamespace="CompanyX" />
    
    <h1>Confirm MessageBox with ButtonsConfig</h1>
    
    <p>The following sample demonstrates using an AjaxMethod to call back to the server and return a Confirmation MessageBox.</p>
    
    <p>The Confirm MessageBox is configured with a custom <b>ButtonsConfig</b> object which enables customization of each Button <code>.Text</code> and <code>.Handler</code> properties.</p>
     <pre class="code">
Ext.Msg.Confirm("Message", "Confirm?", new MessageBox.ButtonsConfig
{
    Yes = new MessageBox.ButtonConfig
    {
        Handler = "CompanyX.DoYes()",
        Text = "Yes Please"
    },
    No = new MessageBox.ButtonConfig
    {
        Handler = "CompanyX.DoNo()",
        Text = "No Thanks"
    }
}).Show();    
    </pre>
    
    <p>The <code>.Handler</code> property is set with JavaScript Code which executes on the client when the Button is clicked. The 'Yes' and 'No' Buttons in this
    sample each call an individual AjaxMethod which in turn calls a server-side Method.</p>
    
    <p>Upon selecting 'Yes' or 'No', another AjaxMethod will be fired and returns a message to the Page.</p>
    
    <div class="information">
        <p>Please ensure you call the <code>.Show()</code> Method of the <b>MessageBox</b> object to render the MessageBox to the Page.</p>
    </div>
    
    <p>In addition to the custom <b>ButtonsConfig</b> object, the sample also demonstrates setting the <code>AjaxMethodNamespace</code> to configure a custom namespace value for AjaxMethod's instead of using the default "<code>Coolite.AjaxMethods</code>".</p>
    
    <pre class="code">
// Default
Coolite.AjaxMethods.DoYes();

// Custom AjaxMethodNamespace
CompanyX.DoYes();    
    </pre>
    <h4>Example</h4>
    
    <form runat="server">
        <p>
            <ext:Button runat="server" Text="Confirm" Icon="Error">
                <Listeners>
                    <Click Handler="CompanyX.DoConfirm()" />
                </Listeners>
            </ext:Button>
        </p>
        
        <p><ext:Label ID="Label1" runat="server" Format="You clicked the '<b>{0}</b>' Button." /></p>
    </form>
</body>
</html>
