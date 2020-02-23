<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoHeight TabPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <ext:ScriptManager runat="server" />
    <ext:TabPanel runat="server" ActiveTabIndex="0" Width="450">
        <Tabs>
            <ext:Tab 
                runat="server" 
                Title="Short Text" 
                AutoHeight="true"
                BodyStyle="padding: 6px;">
                <Body>
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Sed metus nibh, sodales
                    a, porta at, vulputate eget, dui. Pellentesque ut nisl. Maecenas tortor turpis,
                    interdum non, sodales non, iaculis ac, lacus.<br />
                    <br />
                    Vestibulum auctor, tortor quis iaculis malesuada, libero lectus bibendum purus,
                    sit amet tincidunt quam turpis vel lacus. In pellentesque nisl non sem. Suspendisse
                    nunc sem, pretium eget, cursus a, fringilla vel, urna.
                </Body>
            </ext:Tab>
            <ext:Tab 
                runat="server" 
                Title="Long Text" 
                AutoHeight="true"
                BodyStyle="padding: 6px;">
                <Body>
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Sed metus nibh, sodales
                    a, porta at, vulputate eget, dui. Pellentesque ut nisl. Maecenas tortor turpis,
                    interdum non, sodales non, iaculis ac, lacus. Vestibulum auctor, tortor quis iaculis
                    malesuada, libero lectus bibendum purus, sit amet tincidunt quam turpis vel lacus.
                    In pellentesque nisl non sem. Suspendisse nunc sem, pretium eget, cursus a, fringilla
                    vel, urna.<br />
                    <br />
                    Aliquam commodo ullamcorper erat. Nullam vel justo in neque porttitor laoreet. Aenean
                    lacus dui, consequat eu, adipiscing eget, nonummy non, nisi. Morbi nunc est, dignissim
                    non, ornare sed, luctus eu, massa. Vivamus eget quam. Vivamus tincidunt diam nec
                    urna. Curabitur velit.
                </Body>
            </ext:Tab>
        </Tabs>
    </ext:TabPanel>
</body>
</html>