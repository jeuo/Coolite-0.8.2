<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
    protected void Button_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("Clicked", "ImageButton").Show();
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ImageButton Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
   
    <h1>ImageButton Control Variations</h1>
    
    <h2>1. Simple ImageButton</h2> 
       
    <ext:ImageButton 
        runat="server" 
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif">
        <AjaxEvents>
            <Click OnEvent="Button_Click" />
        </AjaxEvents>
    </ext:ImageButton>
    
    <h2>2. Disabled ImageButton</h2>
    
    <ext:ImageButton 
        runat="server" 
        Disabled="true"
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif"
        />
    
    <h2>3. Toggling ImageButton</h2>    
    
    <ext:ImageButton
        runat="server" 
        EnableToggle="true"
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif"
        />
    
    <h2>4. ImageButton with menu</h2>    
    
    <ext:ImageButton
        runat="server" 
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif">
        <Menu>
            <ext:Menu runat="server">
                <Items>
                    <ext:MenuItem runat="server" Text="Add" Icon="Add" />
                    <ext:MenuItem runat="server" Text="Accept" Icon="Accept" />
                </Items>
            </ext:Menu>
        </Menu>
    </ext:ImageButton>
    
    <h2>5. ImageButton in group</h2>
    
    <ext:ImageButton 
        runat="server" 
        ToggleGroup="G1"
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif"
        />
    
    <ext:ImageButton 
        runat="server" 
        ToggleGroup="G1"
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif"
        />
    
    <ext:ImageButton 
        runat="server" 
        ToggleGroup="G1"
        ImageUrl="button.gif" 
        OverImageUrl="overButton.gif" 
        DisabledImageUrl="disabled.gif" 
        PressedImageUrl="pressed.gif"
        />
</body>
</html>
