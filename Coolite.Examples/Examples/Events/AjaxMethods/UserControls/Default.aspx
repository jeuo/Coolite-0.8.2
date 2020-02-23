<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
<%@ Register src="MyUserControl.ascx" tagname="MyUserControl" tagprefix="uc" %>

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
        
        <h2>UserControl with [AjaxMethod]</h2>
        
        <p>An <code>[AjaxMethod]</code> can be defined within a UserControl(.ascx) and called from within the UserControl or from the Parent Page.</p>
        
        <p>
            The following sample demonstrates adding a UserControl to the Page and setting a custom <code>Name</code> property for each.
            The UserControl defines an <code>[AjaxMethod]</code> which is called when the Button is clicked.
        </p>
        
        <h3>Code</h3>
        
<pre class="code">&lt;uc:MyUserControl ID="UserControl1" runat="server" Name="Bob" />
&lt;uc:MyUserControl ID="UserControl2" runat="server" Name="Billy" /></pre>
        
         <h3>Code (.ascx)</h3>
        
<pre class="code">&lt;%@ Control Language="C#" %>

&lt;%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

&lt;script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.Button1.Text = string.Concat("UserControl (", this.Name, ")");
        }
    }
    
    [AjaxMethod]
    public void GetName()
    {
        Ext.Msg.Alert("Name", this.Name).Show();
    }

    public string Name
    {
        get;
        set;
    }
&lt;/script>

&lt;ext:Button ID="Button1" runat="server">
    &lt;Listeners>
        &lt;Click Handler="#{AjaxMethods}.GetName();" />
    &lt;/Listeners>
&lt;/ext:Button></pre> 

        <h3>Example</h3>
           
        <uc:MyUserControl ID="UserControl1" runat="server" Name="Bob" />
        <br />
        <uc:MyUserControl ID="UserControl2" runat="server" Name="Billy" />
        
        <h2>Calling UserControl [AjaxMethod] from Parent .aspx Page</h2>
        
        <p>The following sample demonstrates manually calling the <code>[AjaxMethod]</code> defined in the above UserControls from the parent Page.</p>
        
        <h3>Code</h3>
        
<pre class="code">&lt;ext:Button ID="Button1" runat="server" Text="Call UserControl AjaxMethod (Bob)">
    &lt;Listeners>
        &lt;Click Handler="CompanyX.UserControl1.GetName();" />
    &lt;/Listeners>
&lt;/ext:Button>

&lt;ext:Button ID="Button2" runat="server" Text="Call UserControl AjaxMethod (Billy)">
    &lt;Listeners>
        &lt;Click Handler="CompanyX.UserControl2.GetName();" />
    &lt;/Listeners>
&lt;/ext:Button></pre>
        
        <h3>Example</h3>
        
        <ext:Button runat="server" Text="Call UserControl AjaxMethod (Bob)">
            <Listeners>
                <Click Handler="CompanyX.UserControl1.GetName();" />
            </Listeners>
        </ext:Button>
        <br />
        <ext:Button runat="server" Text="Call UserControl AjaxMethod (Billy)">
            <Listeners>
                <Click Handler="CompanyX.UserControl2.GetName();" />
            </Listeners>
        </ext:Button>
    </form>
</body>
</html>