<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fixed Height TabPanel with options - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" Hide="True" />
        
        <div>
            <h1>Fixed Height TabPanel with options</h1>
            <p>Tabs with no tab strip and a fixed height that scroll the content.</p>
        </div>
        
        <div>
            <ext:TabPanel 
                ID="TabPanel1" 
                runat="server" 
                ActiveTabIndex="0" 
                Width="600" 
                Height="250" 
                Plain="true">
                <Tabs>
                    <ext:Tab 
                        ID="Tab1" 
                        runat="server" 
                        Title="Normal Tab" 
                        Html="My content was added with the Html Property."
                        BodyStyle="padding: 6px;" 
                        AutoScroll="true" 
                        />
                    <ext:Tab 
                        ID="Tab2" 
                        runat="server" 
                        Title="Closable Tab" 
                        Html="You can close this Tab."
                        BodyStyle="padding: 6px;" 
                        Closable="true" 
                        />
                    <ext:Tab 
                        ID="Tab3" 
                        runat="server" 
                        Title="Ajax Tab"                         
                        BodyStyle="padding: 6px;"
                        AutoScroll="true">
                        <AutoLoad Url="ajax.aspx" />
                    </ext:Tab>
                    <ext:Tab 
                        ID="Tab4" 
                        runat="server" 
                        Title="Event Tab" 
                        Html="I am tab 3's content. I also have an event listener attached."
                        BodyStyle="padding: 6px;" 
                        AutoScroll="true">
                        <Listeners>
                            <Activate Handler="Ext.Msg.alert('Event', el.title + ' was activated.');" />
                        </Listeners>
                    </ext:Tab>
                    <ext:Tab 
                        ID="Tab5" 
                        runat="server" 
                        Title="Disabled Tab" 
                        Disabled="true" 
                        Html="Can't see me cause I'm disabled"
                        AutoScroll="true" 
                        />
                </Tabs>
            </ext:TabPanel>
        </div>
    </form>
</body>
</html>
