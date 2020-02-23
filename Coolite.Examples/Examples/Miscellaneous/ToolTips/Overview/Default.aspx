<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ToolTips - Coolite Toolkit Examples</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .tip-target {
            width: 120px;
            height: 40px;
            text-align:center;
            padding: 5px 0;
            border:1px dotted #99bbe8;
            background:#dfe8f6;
            color: #15428b;
            cursor:default;
            margin:10px;
            font:bold 11px tahoma,arial,sans-serif;
            float:left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>ToolTip Examples</h1>
        
        <div id="tip1" class="tip-target">Basic ToolTip</div>
        <div id="tip2" class="tip-target">AutoHide Disabled</div>
        <div id="ajax-tip" class="tip-target">Ajax ToolTip</div>
        <div id="track-tip" class="tip-target">Track Mouse</div>
        <div id="tip4" class="tip-target" ext:qtip="My QuickTip">QuickTip</div>
        
        <ext:ToolTip 
            runat="server" 
            Target="tip1" 
            Html="A very simple tooltip" 
            />
        
        <ext:ToolTip 
            runat="server" 
            Target="ajax-tip" 
            Width="200" 
            DismissDelay="15000"             
        >
        <AutoLoad ShowMask="true" Url="ajax-tip.html"></AutoLoad>
        </ext:ToolTip>
        
        <ext:ToolTip 
            runat="server" 
            Target="tip2" 
            Html="Click the X to close me" 
            Title="My Tip Title" 
            AutoHide="false" 
            Closable="true"
            Draggable="true"
            />
        
        <ext:ToolTip 
            runat="server" 
            Target="track-tip" 
            Html="This tip will follow the mouse while it is over the element" 
            Title="Mouse Track"             
            Width="200"
            TrackMouse="true"
            />
    </form>
</body>
</html>
