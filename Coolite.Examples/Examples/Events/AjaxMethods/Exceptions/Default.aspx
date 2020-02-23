<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
    [AjaxMethod]
    public void UnhandledException()
    {
        throw new Exception("Unhandled Exception");
    }

    [AjaxMethod]
    public void CatchException()
    {
        try
        {
            throw new Exception("An Exception was Thrown");
        }
        catch(Exception e)
        {
            Coolite.Ext.Web.ScriptManager.AjaxSuccess = false;
            Coolite.Ext.Web.ScriptManager.AjaxErrorMessage = e.Message;
        }
    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AjaxMethod and UserControls - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" AjaxMethodNamespace="CompanyX" />
        
        <h1>[AjaxMethod] and UserControls</h1>
        
        <div class="information">
            <p>The <code>AjaxMethodNamespace</code> property has been set to "<b>CompanyX</b>" on the &lt;ext:ScriptManager> which overrides the default <code>[AjaxMethod]</code> Namespace value of "<b>Coolite.AjaxMethods</b>".</p>
        </div>
        
        <h2>Dealing with [AjaxMethod] Exceptions and request failure</h2>
        
        <p>&nbsp;</p>
        
        <h3>Example</h3>
           
        <ext:Button ID="Button1" runat="server" Text="Unhandled Exception">
            <Listeners>
                <Click Handler="CompanyX.UnhandledException({ failure: function (msg) { Ext.Msg.alert('Failure', msg); } });" />
            </Listeners>
        </ext:Button>
        
        <br />
        
        <ext:Button ID="Button2" runat="server" Text="Catch Exception">
            <Listeners>
                <Click Handler="CompanyX.CatchException({ failure: function (msg) { Ext.Msg.alert('Failure', msg); } });" />
            </Listeners>
        </ext:Button>
    </form>
</body>
</html>