<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Converting table into GridPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        #data { width:400px; border:1px solid #bbb; border-collapse:collapse; }
        #data td, #data th { border:1px solid #ccc; border-collapse:collapse; padding:5px; }
    </style>
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h1>Convert an existing HTML &lt;table> into a simple GridPanel</h1>
    
    <ext:TableGrid runat="server" Table="data" StripeRows="true" />
    
    <table style="visibility:hidden;" id="data">
        <thead>
            <tr>
                <th>Name</th>
                <th>Age</th>
                <th>Gender</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Barney Rubble</td>
                <td>32</td>
                <td>Male</td>
            </tr>
            <tr>
                <td>Fred Flintstone</td>
                <td>33</td>
                <td>Male</td>
            </tr>
            <tr>
                <td>Betty Rubble</td>
                <td>32</td>
                <td>Female</td>
            </tr>
            <tr>
                <td>Pebbles</td>
                <td>1</td>
                <td>Female</td>
            </tr>
            <tr>
                <td>Bamm Bamm</td>
                <td>2</td>
                <td>Male</td>
            </tr>
        </tbody>
    </table>
</body>
</html>
