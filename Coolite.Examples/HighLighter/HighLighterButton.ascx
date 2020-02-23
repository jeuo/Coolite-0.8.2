<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HighLighterButton.ascx.cs" Inherits="Coolite.Examples.HighLighter.HighLighterButton" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<ext:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</ext:ScriptManagerProxy>

<ext:Button  ID="btnSource" runat="server" Text="Source" Icon="PageWhiteCode" AutoPostBack="false">
    <AjaxEvents>
        <Click OnEvent="btnSource_Click"></Click>
    </AjaxEvents>
</ext:Button>

<ext:Window ID="winSource" 
            runat="server" 
            Title="Source"
            CloseAction="Hide" 
            Collapsible="true" 
            Height="600px" 
            AutoScroll="true"
            BodyStyle="padding: 6px; background-color: #fff;"
            Icon="PageWhiteCode"
            Width="900"
            ShowOnLoad="false"     
            AnimateTarget="btnSource">
</ext:Window>
