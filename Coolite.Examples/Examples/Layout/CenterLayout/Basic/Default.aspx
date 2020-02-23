<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Center Layout - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    
    <ext:ViewPort runat="server">
        <Body>
            <ext:CenterLayout runat="server">
                <ext:Panel 
                    runat="server" 
                    Title="Centered Panel: 75% of container width and fit height"
                    AutoScroll="true"
                    BodyStyle="padding:20px 0;">                        
                    <CustomConfig>
                        <ext:ConfigItem Name="width" Value="75%" Mode="Value" />
                    </CustomConfig>
                    <Body>                            
                        <ext:CenterLayout runat="server">
                            <ext:Panel 
                                runat="server" 
                                Title="Inner Centered Panel" 
                                Width="300" 
                                Frame="true" 
                                AutoHeight="true"
                                BodyStyle="padding:10px 20px;">
                                <Body>
                                    Fixed 300px wide and auto height. The container panel will also autoscroll if narrower than 300px.
                                </Body>
                            </ext:Panel>
                        </ext:CenterLayout>
                    </Body>                        
                </ext:Panel>
            </ext:CenterLayout>
        </Body>
    </ext:ViewPort>
</body>
</html>
