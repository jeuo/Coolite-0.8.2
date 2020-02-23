<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>FormLayouts built in markup - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>FormLayouts built in markup</h1>
        
        <h2>Form1 - Very Simple</h2>

        <ext:Panel 
            ID="Panel1" 
            runat="server" 
            Title="Simple Form"
            BodyStyle="padding:5px 5px 0"
            Width="350"
            Frame="true"
            ButtonAlign="Center">
            <Body>
                <ext:FormLayout runat="server">
                    <Anchors>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="First Name" AllowBlank="false" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="Last Name" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="Company" />
                        </ext:Anchor>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField runat="server" FieldLabel="Email" Vtype="email" />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save" />
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
        </ext:Panel>

        <h2>Form 2 - Adding fieldsets</h2>
        
        <ext:Panel 
            ID="Panel2" 
            runat="server"
            Title="Simple Form with FieldSets"
            BodyStyle="padding: 5px 5px 0"
            Width="350"
            Frame="true"
            ButtonAlign="Center">
            <Body>
                <ext:ContainerLayout runat="server">
                    <ext:FieldSet 
                        runat="server"
                        CheckboxToggle="true"
                        Title="User Information"
                        AutoHeight="true"
                        Collapsed="true">
                        <Body>
                            <ext:FormLayout runat="server" LabelWidth="75">
                                <Anchors>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="First Name" AllowBlank="false" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Last Name" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Company" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Email" />
                                    </ext:Anchor>
                                </Anchors>
                            </ext:FormLayout>
                        </Body>
                    </ext:FieldSet>
                    <ext:FieldSet
                        runat="server"
                        CheckboxToggle="true"
                        Title="Phone Number"
                        AutoHeight="true">
                        <Body>
                            <ext:FormLayout runat="server" LabelWidth="75">
                                <Anchors>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Home" Text="(888) 555-1212" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Business" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Mobile" />
                                    </ext:Anchor>
                                    <ext:Anchor Horizontal="100%">
                                        <ext:TextField runat="server" FieldLabel="Fax" />
                                    </ext:Anchor>
                                </Anchors>
                            </ext:FormLayout>
                        </Body>
                    </ext:FieldSet>
                </ext:ContainerLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save" />
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
        </ext:Panel>
        
        <h2>Form 3 - A little more complex</h2>
        
        <ext:Panel 
            ID="Panel3"
            runat="server" 
            Title="Multi Column, Nested Layouts and Anchoring" 
            Frame="true"
            BodyStyle="padding:5px 5px 0;"
            Width="600"
            ButtonAlign="Center">
            <Body>
                <ext:Panel runat="server">
                    <Body>
                        <ext:ColumnLayout runat="server">
                            <ext:LayoutColumn ColumnWidth=".5">
                                <ext:Panel runat="server" Border="false" Header="false">
                                    <Body>
                                        <ext:FormLayout runat="server" LabelAlign="Top">
                                            <Anchors>
                                                <ext:Anchor Horizontal="95%">
                                                    <ext:TextField runat="server" FieldLabel="First Name" />
                                                </ext:Anchor>
                                                <ext:Anchor Horizontal="95%">
                                                    <ext:TextField runat="server" FieldLabel="Company" />
                                                </ext:Anchor>
                                            </Anchors>
                                        </ext:FormLayout>
                                    </Body>
                                </ext:Panel>
                            </ext:LayoutColumn>
                            <ext:LayoutColumn ColumnWidth=".5">
                                <ext:Panel runat="server">
                                    <Body>
                                        <ext:FormLayout runat="server" LabelAlign="Top">
                                            <Anchors>
                                                <ext:Anchor Horizontal="95%">
                                                    <ext:TextField runat="server" FieldLabel="Last Name" />
                                                </ext:Anchor>
                                                <ext:Anchor Horizontal="95%">
                                                    <ext:TextField runat="server" FieldLabel="Email" />
                                                </ext:Anchor>
                                            </Anchors>
                                        </ext:FormLayout>
                                    </Body>
                                </ext:Panel>
                            </ext:LayoutColumn>
                        </ext:ColumnLayout>
                    </Body>
                </ext:Panel>
                <ext:Panel runat="server">
                    <Body>
                        <ext:FormLayout runat="server" LabelAlign="Top">
                            <Anchors>
                                <ext:Anchor Horizontal="98%">
                                    <ext:HtmlEditor runat="server" Height="200" FieldLabel="Biography" />
                                </ext:Anchor>
                            </Anchors>
                        </ext:FormLayout>
                    </Body>
                </ext:Panel>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save" />
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
        </ext:Panel>
        
        <h2>Form 4 - Forms can be a TabPanel...</h2>
        
        <ext:Panel
            ID="Panel4"
            runat="server"
            Border="false"
            Width="350"
            ButtonAlign="Center">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:TabPanel runat="server" ActiveTabIndex="0">
                        <Tabs>
                            <ext:Tab 
                                runat="server" 
                                Title="Personal Details" 
                                AutoHeight="true" 
                                BodyStyle="padding:10px;">
                                <Body>
                                    <ext:FormLayout ID="FormLayout1" runat="server" LabelWidth="75">
                                        <Anchors>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField runat="server" FieldLabel="First Name" AllowBlank="false" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField runat="server" FieldLabel="Last Name" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField runat="server" FieldLabel="Company" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField runat="server" FieldLabel="Email" />
                                            </ext:Anchor>
                                        </Anchors>
                                    </ext:FormLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab 
                                runat="server"
                                Title="Phone Numbers"
                                AutoHeight="true"
                                BodyStyle="padding:10px;">
                                <Body>
                                    <ext:FormLayout ID="FormLayout2" runat="server" LabelWidth="75">
                                        <Anchors>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField ID="TextField1" runat="server" FieldLabel="Home" Text="(888) 555-1212" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField ID="TextField2" runat="server" FieldLabel="Business" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField ID="TextField3" runat="server" FieldLabel="Mobile" />
                                            </ext:Anchor>
                                            <ext:Anchor Horizontal="100%">
                                                <ext:TextField ID="TextField4" runat="server" FieldLabel="Fax" />
                                            </ext:Anchor>
                                        </Anchors>
                                    </ext:FormLayout>
                                </Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </ext:FitLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save" />
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
        </ext:Panel>
        
        <h2>Form 5 - ... and forms can contain TabPanel(s)</h2>
        
        <ext:Panel
            ID="Panel5"
            runat="server"
            Title="Inner Tabs"
            Width="600"
            BodyStyle="padding:5px;"
            ButtonAlign="Center">
            <Body>
                <ext:ContainerLayout runat="server">
                    <ext:Panel runat="server" Border="false">
                        <Body>
                            <ext:ColumnLayout runat="server">
                                <ext:LayoutColumn ColumnWidth=".5">
                                    <ext:Panel runat="server" Border="false" Header="false">
                                        <Body>
                                            <ext:FormLayout runat="server" LabelAlign="Top">
                                                <Anchors>
                                                    <ext:Anchor Horizontal="95%">
                                                        <ext:TextField runat="server" FieldLabel="First Name" />
                                                    </ext:Anchor>
                                                    <ext:Anchor Horizontal="95%">
                                                        <ext:TextField runat="server" FieldLabel="Company" />
                                                    </ext:Anchor>
                                                </Anchors>
                                            </ext:FormLayout>
                                        </Body>
                                    </ext:Panel>
                                </ext:LayoutColumn>
                                <ext:LayoutColumn ColumnWidth=".5">
                                    <ext:Panel runat="server" Border="false">
                                        <Body>
                                            <ext:FormLayout runat="server" LabelAlign="Top">
                                                <Anchors>
                                                    <ext:Anchor Horizontal="95%">
                                                        <ext:TextField runat="server" FieldLabel="Last Name" />
                                                    </ext:Anchor>
                                                    <ext:Anchor Horizontal="95%">
                                                        <ext:TextField runat="server" FieldLabel="Email" />
                                                    </ext:Anchor>
                                                </Anchors>
                                            </ext:FormLayout>
                                        </Body>
                                    </ext:Panel>
                                </ext:LayoutColumn>
                            </ext:ColumnLayout>
                        </Body>
                    </ext:Panel>
                    <ext:TabPanel 
                        runat="server" 
                        ActiveTabIndex="0" 
                        Plain="true"
                        Height="235">
                        <Tabs>
                            <ext:Tab
                                runat="server" 
                                Title="Personal Details" 
                                AutoHeight="true" 
                                BodyStyle="padding:10px;">
                                <Body>
                                    <ext:FormLayout runat="server" LabelWidth="75" LabelAlign="Top">
                                        <Anchors>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="First Name" AllowBlank="false" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Last Name" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Company" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Email" Width="230" />
                                            </ext:Anchor>
                                        </Anchors>
                                    </ext:FormLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab
                                runat="server"
                                Title="Phone Numbers"
                                AutoHeight="true" 
                                BodyStyle="padding:10px;">
                                <Body>
                                    <ext:FormLayout runat="server" LabelWidth="75" LabelAlign="Top">
                                        <Anchors>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Home" Text="(888) 555-1212" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Business" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Mobile" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField runat="server" FieldLabel="Fax" Width="230" />
                                            </ext:Anchor>
                                        </Anchors>
                                    </ext:FormLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab
                                runat="server"
                                Title="Biography"
                                BodyStyle="padding:10px">
                                <Body>
                                    <ext:FitLayout runat="server">
                                        <ext:HtmlEditor runat="server" />
                                    </ext:FitLayout>
                                </Body>
                            </ext:Tab>
                            <ext:Tab 
                                ID="Tab4"
                                runat="server"
                                Title="Tab 4">
                                <Body>
                                    <ext:FormLayout ID="FormLayout3" runat="server" LabelWidth="75" LabelAlign="Top">
                                        <Anchors>
                                            <ext:Anchor>
                                                <ext:TextField ID="txtName" runat="server" FieldLabel="Name" Width="230" />
                                            </ext:Anchor>
                                            <ext:Anchor>
                                                <ext:TextField ID="txtAge" runat="server" FieldLabel="Age" Width="230" />
                                            </ext:Anchor>
                                        </Anchors>
                                    </ext:FormLayout>
                                </Body>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </ext:ContainerLayout>
            </Body>
            <Buttons>
                <ext:Button runat="server" Text="Save" />
                <ext:Button runat="server" Text="Cancel" />
            </Buttons>
        </ext:Panel>
    </form>
</body>
</html>