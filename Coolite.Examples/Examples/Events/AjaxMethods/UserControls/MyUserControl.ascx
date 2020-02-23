<%@ Control Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
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
</script>

<ext:Button ID="Button1" runat="server">
    <Listeners>
        <Click Handler="#{AjaxMethods}.GetName();" />
    </Listeners>
</ext:Button>