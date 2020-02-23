<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add FieldTrigger to ComboBox - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <p><a href="Default.aspx">Reload</a></p>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:ComboBox ID="ComboBox1" runat="server" MinChars="1">
            <Items>
                <ext:ListItem Text="1" />
                <ext:ListItem Text="2" />
                <ext:ListItem Text="3" />
                <ext:ListItem Text="4" />
                <ext:ListItem Text="5" />
            </Items>
            <Triggers>
                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
            </Triggers>
            <Listeners>
                <Select Handler="this.triggers[0].show();" />
                <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                <TriggerClick Handler="if(index == 0) { this.clearValue(); this.triggers[0].hide(); }" />
            </Listeners>
        </ext:ComboBox>
    </form>
</body>
</html>
