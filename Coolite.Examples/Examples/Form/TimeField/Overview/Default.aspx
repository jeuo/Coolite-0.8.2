<%@ Page Language="C#" Culture="en-US" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TimeField Control Variations - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                this.TimeField1.SelectedTime = DateTime.Now.TimeOfDay;                   
            }
        }
        
        protected void SwapValues(object sender, AjaxEventArgs e)
        {
            TimeSpan time1 = TimeField1.SelectedTime;
            TimeSpan time2 = TimeField2.SelectedTime;

            TimeField1.SelectedTime = time2;
            TimeField2.SelectedTime = time1;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
       
        <h1>TimeField Control Variations</h1>
        <br />
        
        <h2>1. 12-hour time format</h2>    
        <ext:TimeField ID="TimeField1" runat="server" MinTime="9:00" MaxTime="18:00" Increment="30" Format="hh:mm tt"/>
        
        <br />
        <br />
        
        <h2>2. 24-hour time format</h2>    
        <ext:TimeField ID="TimeField2" runat="server" MinTime="9:00" MaxTime="18:00" Increment="30" SelectedTime="09:00" Format="H:mm" />
        
        <br />
        <br />
        
        <ext:Button runat="server" Text="Swap selected values" Icon="ArrowInout">
            <AjaxEvents>
                <Click OnEvent="SwapValues"></Click>
            </AjaxEvents>
        </ext:Button>
    </form>
    
</body>
</html>
