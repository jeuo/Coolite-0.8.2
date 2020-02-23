<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
    protected void Button_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("AjaxEvent", string.Format("Item - {0}", e.ExtraParams["Item"])).Show();
    }
</script>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Button Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .custom-icon1 {
        	background-image: url(arrow-down.gif) !important;
        }
        
        .custom-icon2 {
        	background-image: url(arrow-down.gif) !important;
        	background-position: right !important; 
        	padding-left: 0px !important;        	
        	padding-right: 18px !important;
        }
    </style>
</head>
<body>
    <ext:ScriptManager runat="server" />
   
    <h1>Button Control Variations</h1>
    
    <h2>1. Button with Listener</h2>
    
    <ext:Button runat="server" Text="Click Me">
        <Listeners>
            <Click Handler="alert('Clicked');" />
        </Listeners>
    </ext:Button>
    
    <h2>2. Button with AjaxEvent</h2>
    
    <ext:Button runat="server" Text="Click Me">
        <AjaxEvents>
            <Click OnEvent="Button_Click">
                <EventMask ShowMask="true" />
                <ExtraParams>
                    <ext:Parameter Name="Item" Value="One" />
                </ExtraParams>
            </Click>
        </AjaxEvents>
    </ext:Button>
    
    <h2>3. Button with Icon</h2>
    
    <ext:Button runat="server" Text="Text" Icon="Add" />
    
    <h2>4. Button with Custom Icon</h2> 
    
    <ext:Button runat="server" Text="Text" IconCls="custom-icon1" />
    
    <h2>5. Button with Right Aligned Custom Icon</h2>
    
    <ext:Button runat="server" Text="Text" IconCls="custom-icon2" />
    
    <h2>6. Button with QuickTip</h2>
    
    <ext:Button runat="server" Text="Text">
        <ToolTips>
            <ext:ToolTip runat="server" Title="Title" Html="Description" />
        </ToolTips>
    </ext:Button>
    
    <h2>7. Toggle Buttons</h2>
    
    <ext:Button runat="server" Text="Button1" EnableToggle="true" ToggleGroup="Group1" Pressed="true" />
    <ext:Button runat="server" Text="Button2" EnableToggle="true" ToggleGroup="Group1" />
    <ext:Button runat="server" Text="Button3" EnableToggle="true" ToggleGroup="Group1" />
    
    <h2>8. Button with menu</h2>
    
    <ext:Button runat="server" Text="Text">
        <Menu>
            <ext:Menu runat="server">
                <Items>                    
                    <ext:MenuItem runat="server" Text="Item 1" Icon="GroupAdd" />
                    <ext:MenuItem runat="server" Text="Item 2" Icon="GroupDelete" />
                    <ext:MenuItem runat="server" Text="Item 3" Icon="GroupEdit" />
                </Items>
            </ext:Menu>
        </Menu>
    </ext:Button>
    
    <h2>9. SplitButton with menu</h2>
    
    <ext:SplitButton runat="server" Text="Text">
        <Menu>
            <ext:Menu runat="server">
                <Items>                    
                    <ext:MenuItem runat="server" Text="Item 1" Icon="GroupAdd" />
                    <ext:MenuItem runat="server" Text="Item 2" Icon="GroupDelete" />
                    <ext:MenuItem runat="server" Text="Item 3" Icon="GroupEdit" />
                </Items>
            </ext:Menu>
        </Menu>
    </ext:SplitButton>
    
    <h2>10. CycleButton</h2>
    
    <ext:CycleButton runat="server" ShowText="true" PrependText="View As ">
        <Menu>
            <ext:Menu runat="server">
                <Items>
                    <ext:CheckMenuItem runat="server" Text="Text Only" Icon="Note" />
                    <ext:CheckMenuItem runat="server" Text="Html" Icon="Html" Checked="true" />
                </Items>
            </ext:Menu>
        </Menu>
    </ext:CycleButton>
    
    <h2>11. Flat Button</h2>
    
    <ext:Button runat="server" Text="FlatButton" Icon="Accept" Flat="true" />
</body>
</html>
