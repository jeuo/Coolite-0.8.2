<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Window1.Title = "Ajaxian";
        this.Window1.Width = Unit.Pixel(1000);
        this.Window1.Height = Unit.Pixel(600);
        this.Window1.Modal = true;
        this.Window1.Collapsible = true;
        this.Window1.Maximizable = true;
        this.Window1.AutoLoad.Url = "http://ajaxian.com/archives/mad-cool-date-library/";
        this.Window1.AutoLoad.Mode = LoadMode.IFrame;
        this.Window1.ShowOnLoad = false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Load External Website into Window - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <h1>Load External Website into an &lt;ext:Window></h1>
            <p>Load an external url into a Window using the AutoLoadIFrame property. 
                All Properties for the &lt;ext:Window> are set during the Page_Load Event.</p>
            <div style="margin: 15px 0;">
                <p>
                    <ext:Button runat="server" Text="Show Ajaxian">
                        <Listeners>
                            <Click Handler="#{Window1}.show(this);" />
                        </Listeners>
                    </ext:Button>
                </p>
            </div>
        </div>
        <ext:Window ID="Window1" runat="server" />
    </form>
</body>
</html>
