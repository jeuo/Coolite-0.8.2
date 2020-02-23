<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Window1.Show();
    }

    protected void Button3_Click(object sender, AjaxEventArgs e)
    {
        this.Window3.Show();
    }

    protected void Button5_Click(object sender, AjaxEventArgs e)
    {
        this.Window5.Show();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to Show a Window - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server">
            <CustomAjaxEvents>
                <ext:AjaxEvent Target="Button5" OnEvent="Button5_Click">
                    <EventMask ShowMask="true" MinDelay="250" />
                </ext:AjaxEvent>
            </CustomAjaxEvents>
            <CustomListeners>
                <ext:Listener Target="Button6" Handler="Window6.show();return false;" />
            </CustomListeners>
        </ext:ScriptManager>
        
        <h1>Showing a Window</h1>
        
        <p>This sample demonstrates several client-side and server-side techniques for triggering the &lt;ext:Window> to "show" if hidden.</p>
        
        <p>Within server-side code, the .Show() Method should be used. Within client-side code, the .show() function should be used.</p>
        
        <div class="information">
            <p>In the example below, the Buttons which fire AjaxEvents have been set with a 250 millisecond delay (<code>MinDelay="250"</code>) to allow the user time to read the Ajax Load Mask ("Working...").</p>
        </div>
        
        <h3>Example</h3>
        
        <b>ext:Button's</b>
        
        <p>
            <ext:Button 
                ID="Button1" 
                runat="server" 
                Text="With PostBack" 
                OnClick="Button1_Click"
                AutoPostBack="true"
                />
        </p>

        <p>
            <ext:Button 
                ID="Button2" 
                runat="server" 
                Text="Click Listener">
                <Listeners>
                    <Click Handler="Window2.show()" />
                </Listeners>
            </ext:Button>
        </p>
        
        <p>
            <ext:Button ID="Button3" runat="server" Text="Click AjaxEvent">
                <AjaxEvents>
                    <Click OnEvent="Button3_Click">
                        <EventMask ShowMask="true" MinDelay="250" />
                    </Click>
                </AjaxEvents>
            </ext:Button>
        </p>
        
        <br />
        
        <b>asp:Button's</b>
        
        <br />
        <br />
        
        <p>
            <asp:Button 
                ID="Button4" 
                runat="server" 
                Text="ASP.NET Button with OnClientClick" 
                OnClientClick="Window4.show();return false;" 
                />
        </p>
            
        <p>
            <asp:Button 
                ID="Button5" 
                runat="server" 
                OnClientClick="return false;"
                Text="ASP.NET Button with Custom AjaxEvent" 
                />
        </p>
                
        <p>
            <asp:Button 
                ID="Button6" 
                runat="server" 
                Text="ASP.NET Button with Custom Listener"
                OnClientClick="return false;"
                />
        </p>
        
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Icon="House" 
            Title="With PostBack" 
            ShowOnLoad="false"
            X="250"
            Y="100"
            />
            
        <ext:Window 
            ID="Window2" 
            runat="server" 
            Icon="House" 
            Title="Click Listener" 
            ShowOnLoad="false" 
            X="275"
            Y="150"
            />
            
        <ext:Window 
            ID="Window3" 
            runat="server" 
            Icon="House" 
            Title="Click AjaxEvent" 
            ShowOnLoad="false" 
            X="300"
            Y="200"
            />
            
        <ext:Window 
            ID="Window4" 
            runat="server" 
            Icon="House" 
            Title="ASP.NET Button with OnClientClick" 
            ShowOnLoad="false" 
            X="325"
            Y="250"
            /> 
            
        <ext:Window 
            ID="Window5" 
            runat="server" 
            Icon="House" 
            Title="ASP.NET Button with Custom AjaxEvent" 
            ShowOnLoad="false" 
            X="350"
            Y="300"
            /> 
            
        <ext:Window 
            ID="Window6" 
            runat="server" 
            Icon="House" 
            Title="ASP.NET Button with Custom Listener" 
            ShowOnLoad="false"
            X="375"
            Y="350"
            />  
    </form>
</body>
</html>