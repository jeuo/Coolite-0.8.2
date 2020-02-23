<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Globalization"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<script runat="server">
    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        //Ext.History.Add("token here");
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>History - Coolite Toolkit Examples</title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />    
    
    <script type="text/javascript">
        var change = function (token) {
            if (token) {
                var parts = token.split(":");
                var tabPanel = Ext.getCmp(parts[0]);
                var tabId = parts[1];
                if (tabPanel.id == "TabPanel2") {
                    TabPanel1.setActiveTab(0);
                }
                tabPanel.show();
                tabPanel.setActiveTab(tabId);
            } else {
                // This is the initial default state.  Necessary if you navigate starting from the
                // page without any existing history token params and go back to the start state.
                TabPanel1.setActiveTab(0);
                TabPanel2.setActiveTab(0);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:History ID="History1" runat="server">
            <Listeners>
                <Change Fn="change" />
            </Listeners>
        </ext:History>
        
        <ext:TabPanel ID="TabPanel1" runat="server" Height="300" Width="600" ActiveTabIndex="0">
            <Tabs>
                <ext:Tab ID="Tab1" runat="server" Title="Tab1">
                    <Body>
                        <ext:FitLayout runat="server">
                            <ext:TabPanel ID="TabPanel2" runat="server" ActiveTabIndex="0" TabPosition="Bottom">
                                <Tabs>
                                    <ext:Tab ID="SubTab1" runat="server" Title="Sub-tab 1" />
                                    <ext:Tab ID="SubTab2" runat="server" Title="Sub-tab 2" />
                                    <ext:Tab ID="SubTab3" runat="server" Title="Sub-tab 3" />
                                </Tabs>
                                <Listeners>
                                    <TabChange Handler="History1.add(el.id + ':' + tab.id);" />
                                </Listeners>
                            </ext:TabPanel>
                        </ext:FitLayout>
                    </Body>
                </ext:Tab>
                
                <ext:Tab ID="Tab2" runat="server" Title="Tab 2" />
                <ext:Tab ID="Tab3" runat="server" Title="Tab 3" />
                <ext:Tab ID="Tab4" runat="server" Title="Tab 4" />
                <ext:Tab ID="Tab5" runat="server" Title="Tab 5" />
            </Tabs>
            <Listeners>
                <TabChange Handler="if(tab.id != '#{Tab1}'){History1.add(el.id + ':' + tab.id);}" />
            </Listeners>
        </ext:TabPanel>
    </form>
</body>
</html>
