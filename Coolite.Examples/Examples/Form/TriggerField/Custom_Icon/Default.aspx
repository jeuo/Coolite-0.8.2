﻿<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .custom-trigger
        {
            background-image:url(resources/images/custom-trigger.gif) !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:TriggerField 
            ID="TriggerField1" 
            runat="server" 
            Width="200" 
            EmptyText="Click Trigger Button -->">
            <Triggers>
                <ext:FieldTrigger IconCls="custom-trigger" />
            </Triggers>
            <Listeners>
                <TriggerClick Handler="Ext.Msg.alert('Message', 'You Clicked the Trigger!');" />
            </Listeners>
        </ext:TriggerField>
        
       
    </form>
</body>
</html>
