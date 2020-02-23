<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Coolite Toolkit Example - Spotlight</title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.IsAjaxRequest)
            {
                this.UpdateButtons(null);
            }
        }

        protected void UpdateSpot(object sender, AjaxEventArgs e)
        {
            string cmp = e.ExtraParams["cmp"];
            if(cmp == null)
            {
                Spot.Hide();

                this.UpdateButtons(null);
            }
            else
            {
                Coolite.Ext.Web.Panel panel = (Coolite.Ext.Web.Panel)Coolite.Utilities.ControlUtils.FindControl(this, cmp);
                
                Spot.Show(panel);

                this.UpdateButtons(panel);
            }
        }
        
        private void UpdateButtons(Coolite.Ext.Web.Panel panel)
        {
            Button1.Enabled = panel != null ? panel.ID == "Panel1" : false;
            Button2.Enabled = panel != null ? panel.ID == "Panel2" : false;
            Button3.Enabled = panel != null ? panel.ID == "Panel3" : false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>Spotlight</h1>
        <p>This control allows you to restrict input to a particular element by masking all other page content.</p>

        
        <ext:Spotlight ID="Spot" runat="server" Easing="EaseOut" Duration="0.3" />
        
        <ext:Button runat="server" Text="Start">
            <AjaxEvents>
                <Click OnEvent="UpdateSpot">
                    <ExtraParams>
                        <ext:Parameter Name="cmp" Value="Panel1" />
                    </ExtraParams>
                </Click>
            </AjaxEvents>
        </ext:Button>
        
        <ext:Panel runat="server" Border="false">
            <Body>
                <ext:TableLayout runat="server" Columns="3">
                    <ext:Cell>
                        <ext:Panel ID="Panel1" runat="server" 
                            Frame="true"
                            Title="Demo Panel"
                            Width="200"
                            Height="150"
                            Html="Some panel content goes here!" 
                            BodyStyle="padding:10px 15px;" 
                            >
                            <Buttons>
                                <ext:Button ID="Button1" runat="server" Text="Next Panel">
                                    <AjaxEvents>
                                        <Click OnEvent="UpdateSpot">
                                            <ExtraParams>
                                                <ext:Parameter Name="cmp" Value="Panel2" />
                                            </ExtraParams>
                                        </Click>
                                    </AjaxEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </ext:Cell>
                    
                    <ext:Cell>
                        <ext:Panel ID="Panel2" runat="server" 
                            Frame="true"
                            Title="Demo Panel"
                            Width="200"
                            Height="150"
                            Html="Some panel content goes here!" 
                            BodyStyle="padding:10px 15px;" 
                            >
                            <Buttons>
                                <ext:Button ID="Button2" runat="server" Text="Next Panel">
                                    <AjaxEvents>
                                        <Click OnEvent="UpdateSpot">
                                            <ExtraParams>
                                                <ext:Parameter Name="cmp" Value="Panel3" />
                                            </ExtraParams>
                                        </Click>
                                    </AjaxEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </ext:Cell>
                    
                    <ext:Cell>
                        <ext:Panel ID="Panel3" runat="server" 
                            Frame="true"
                            Title="Demo Panel"
                            Width="200"
                            Height="150"
                            Html="Some panel content goes here!" 
                            BodyStyle="padding:10px 15px;" 
                            >
                            <Buttons>
                                <ext:Button ID="Button3" runat="server" Text="Done">
                                    <AjaxEvents>
                                        <Click OnEvent="UpdateSpot">                                            
                                        </Click>
                                    </AjaxEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </ext:Cell>
                </ext:TableLayout>
            </Body>
        </ext:Panel>
        
        
    </form>
</body>
</html>
