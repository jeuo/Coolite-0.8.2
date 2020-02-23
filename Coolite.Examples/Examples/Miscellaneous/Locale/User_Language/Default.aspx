<%@ Page Language="C#" %>
<%@ Import Namespace="System.Globalization"%>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Coolite Toolkit Example - Locale</title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserLanguages == null)
            {
                UserLangLabel.Html = "User Language: <b>Is Absent</b>";
                return;
            }

            string lang = Request.UserLanguages[0];
            UserLangLabel.Html = "User Language: <b>" + lang + "</b>";
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" Locale="Client" />
        
        <h1>User Language</h1>       
        
        <ext:Label ID="UserLangLabel" runat="server" />
        <br />
        <ext:DatePicker runat="server"></ext:DatePicker>
    </form>
</body>
</html>
