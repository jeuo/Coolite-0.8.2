<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
   "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Multiple DateFields with DateRange Validation - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Multiple DateFields with DateRange Validation</h1>
        
        <ext:Window 
            ID="Panel1" 
            runat="server" 
            Width="350"
            AutoHeight="true"
            Title="DateRange"
            Icon="Date"
            Closable="false"
            BodyStyle="padding:6px;"
            CenterOnLoad="false"
            PageX="20"
            PageY="100">
            <Body>
                <ext:FormLayout ID="FormLayout1" runat="server" LabelSeparator="">
                    <ext:Anchor>
                        <ext:Label 
                            ID="Label1" 
                            runat="server"                             
                            StyleSpec="display:block;padding:0 0 12px 0;"
                            Html="If a value is specified / selected in the 'FromDate field', 
                            the 'ToDate field' doesn't allow any date prior to the 'FromDate' 
                            entry to be specified / selected and vice versa.">
                        </ext:Label>
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:DateField 
                            runat="server"
                            ID="FromDate" 
                            Vtype="daterange"
                            FieldLabel="To">
                            <Listeners>
                                <Render Handler="this.endDateField = '#{ToDate}'" />
                            </Listeners>                            
                        </ext:DateField>
                    </ext:Anchor>
                    <ext:Anchor Horizontal="100%">
                        <ext:DateField 
                            runat="server" 
                            ID="ToDate"
                            Vtype="daterange"
                            FieldLabel="From">
                            <Listeners>
                                <Render Handler="this.startDateField = '#{FromDate}'" />
                            </Listeners>                            
                        </ext:DateField>     
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>            
        </ext:Window>                
   </form>
</body>
</html>
