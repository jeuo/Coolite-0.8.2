<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Card Layout - Coolite Toolkit Examples</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Next_Click(object sender, AjaxEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);
            
            if ((index + 1) < WizardPanel.Items.Count)
            {
                WizardPanel.ActiveIndex = index + 1;
            }
            
            CheckButtons();
        }

        protected void Prev_Click(object sender, AjaxEventArgs e)
        {
            int index = int.Parse(e.ExtraParams["index"]);
            if ((index - 1) >= 0)
            {
                WizardPanel.ActiveIndex = index - 1;
            }
            CheckButtons();
        }

        private void CheckButtons()
        {
            int index = WizardPanel.ActiveIndex;
            btnNext.Disabled = index == (WizardPanel.Items.Count - 1);
            btnPrev.Disabled = index == 0;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:Panel ID="WizardPanel" runat="server" Title="Example Wizard" BodyStyle="padding:15px" Height="300" ActiveIndex="0">       
            <Body>
                <ext:CardLayout ID="WizardLayout" runat="server">
                    <ext:Panel 
                        ID="Panel1"
                        runat="server" 
                        Html="<h1>Welcome to the Wizard!</h1><p>Step 1 of 3</p>" 
                        Border="false" 
                        Header="false" 
                        />
                    <ext:Panel 
                        ID="Panel2"
                        runat="server" 
                        Html="<h1>Card 2</h1><p>Step 2 of 3</p>" 
                        Border="false" 
                        Header="false" 
                        />
                    <ext:Panel 
                        ID="Panel3"
                        runat="server" 
                        Html="<h1>Congratulations!</h1><p>Step 3 of 3 - Complete</p>" 
                        Border="false" 
                        Header="false" 
                        />
                </ext:CardLayout> 
            </Body>         
            <Buttons>
                <ext:Button ID="btnPrev" runat="server" Text="Prev" Disabled="true" Icon="PreviousGreen">
                    <AjaxEvents>
                        <Click OnEvent="Prev_Click">
                            <ExtraParams>
                                <ext:Parameter Name="index" Value="#{WizardPanel}.items.indexOf(#{WizardPanel}.layout.activeItem)" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </AjaxEvents>
                </ext:Button>
                <ext:Button ID="btnNext" runat="server" Text="Next" Icon="NextGreen">
                    <AjaxEvents>
                        <Click OnEvent="Next_Click">
                            <ExtraParams>
                                <ext:Parameter Name="index" Value="#{WizardPanel}.items.indexOf(#{WizardPanel}.layout.activeItem)" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </AjaxEvents>
                </ext:Button>
            </Buttons>     
        </ext:Panel>
    </form>
</body>
</html>
