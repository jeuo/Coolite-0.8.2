<%@ Page Language="C#" %>
<%@ Import Namespace="System.Threading"%>
<%@ Import Namespace="System.Collections.Generic"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<script runat="server">
    void SetTime()
    {
        this.Label1.Text = DateTime.Now.ToLongTimeString();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Ext.IsAjaxRequest)
        {
            this.SetTime();
        }
    }
    
    protected void UpdateTimeStamp(object sender, AjaxEventArgs e)
    {
        this.SetTime();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>AjaxEvents Summary - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .msg {
        	border: 1px solid #999;
        	padding: 6px;
        	width: 250px;
        	font-weight: bold;
            text-align: center;
            margin-bottom: 30px;
        }
        
        .msg em {
        	font-style: italic;
        	font-weight: bold;
        }
        
        .box {
        	width: 100px;
        	height: 50px;
        	border: 1px solid #000;
        	background-color: white;
        	text-align: center;
        	margin-bottom: 4px;
        }
        
        .red  {
        	background-color: red; 
        	color: #fff;
        }
        
        .blue  {
        	background-color: blue; 
        	color: #fff;
        }
        
        h2 { margin-top: 36px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server">
            <CustomAjaxEvents>
                <ext:AjaxEvent Target="Button2" OnEvent="UpdateTimeStamp">
                    <EventMask ShowMask="true" MinDelay="500" Msg="Updating TimeStamp..." />
                </ext:AjaxEvent>
                <ext:AjaxEvent Target="Button3" OnEvent="UpdateTimeStamp">
                    <EventMask ShowMask="true" MinDelay="500" Msg="Updating TimeStamp..." />
                </ext:AjaxEvent>
                <ext:AjaxEvent Target="Span1" OnEvent="UpdateTimeStamp">
                    <EventMask ShowMask="true" MinDelay="500" Msg="Updating TimeStamp..." />
                </ext:AjaxEvent>
                <ext:AjaxEvent Target="${div.box:not(div.red)}" OnEvent="UpdateTimeStamp">
                    <EventMask ShowMask="true" MinDelay="500" Msg="Updating TimeStamp..." />
                </ext:AjaxEvent>
            </CustomAjaxEvents>
        </ext:ScriptManager>
        
        <h1>Summary of AjaxEvents</h1>
        
        <p>The action of each of the following samples will trigger an Ajax request to the server and update the "<span style="font-weight:bold;">Server TimeStamp</span>" message below.</p>
        <p>Each AjaxEvent demonstrated in this example sets the MinDelay property to "500". The delay will ensure the load mask ("Working...") will be visible for a a minimum of 500ms (1/2 second).</p>
        <p>The MinDelay property was added because the AjaxEvents can happen so quickly that the load mask would just "flicker" and cause possible confusion for the end user, 
        especially if you need to present a message to the user during the AjaxEvent request/response lifecycle.</p>
        
        <div class="msg x-box-mc">
            Server TimeStamp: <em><ext:Label ID="Label1" runat="server" /></em>
        </div>
        
        <h2>1. Add a &lt;Click> AjaxEvent to &lt;ext:Button></h2>
        
        <ext:Button ID="Button1" runat="server" Text="Click Me">
            <AjaxEvents>
                <Click OnEvent="UpdateTimeStamp">
                    <EventMask ShowMask="true" MinDelay="500" Msg="Updating TimeStamp..."   />
                </Click>
            </AjaxEvents>
        </ext:Button>
        
        <h2>2. Add a Click AjaxEvent to &lt;asp:Button></h2>
        
        <asp:Button ID="Button2" runat="server" Text="Click Me" />
        
        <h2>3. Add a Click AjaxEvent to a standard html &lt;input> button</h2>
        
        <input id="Button3" type="button" value="Click Me" />
        
        <h2>4. Add a Click AjaxEvent to html &lt;span> element</h2>
        
        <span id="Span1" style="cursor: pointer;">*Click Me*</span>
        
        <h2>5. Add a Click AjaxEvent to several html &lt;div> elements by using a Target Query</h2>
        
        <p>By using a Target Query we can attach the same AjaxEvent (or Listener) to several html elements or Controls and fire a server-side event.</p>
        <p>The target(s) do not require an "id" and do not need a runat="server" attribute. The elements can be any html element available on the Page.</p>
        <p>The following Target Query will select all &lt;div> elements on the Page that contain the css class "box", but do not contain the css class "red".</p>
        
        <div class="box">Click Me</div>
        <div class="box red">Not Me</div>
        <div class="box blue">Click Me too!</div>
    </form>
</body>
</html>
