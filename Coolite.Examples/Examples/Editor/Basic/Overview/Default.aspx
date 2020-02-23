<%@ Page Language="C#" %>

<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<script runat="server">
    protected void CompleteEdit(object sender, AjaxEventArgs e)
    {
        this.AjaxLabel.Html = e.ExtraParams["value"];
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Editor Overview - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .editable {
            font: 14px Tahoma;
            padding: 3px 5px;
            cursor: pointer;
            margin-bottom: 20px;
        }
        .editable-over {
            background-color: #ffc;
        }
        .x-form-field-editor {
            font: 12px Tahoma;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <ext:ViewPort runat="server">
            <Body>
                <ext:FitLayout runat="server">
                    <ext:TabPanel runat="server" Border="false">
                        <Defaults>
                            <ext:Parameter Name="BodyStyle" Value="padding:10px;" Mode="Value" />
                        </Defaults>
                        <Tabs>
                            <ext:Tab ID="Tab1" runat="server" Title="Labels">
                                <Body>
                                    <ext:Label 
                                        ID="Label1" 
                                        runat="server" 
                                        Cls="editable" 
                                        Icon="NoteEdit"
                                        OverCls="editable-over"
                                        Text="With predefined field and fixed width">
                                        <Editor>
                                            <ext:Editor runat="server">
                                                <Field>
                                                    <ext:TextField runat="server" Cls="x-form-field-editor" Width="300" />
                                                </Field>
                                            </ext:Editor>
                                        </Editor>
                                    </ext:Label>
                                    <br />
                                    <br />
                                    <ext:Label 
                                        ID="Label2" 
                                        runat="server" 
                                        Cls="editable" 
                                        Icon="NoteEdit"
                                        OverCls="editable-over"
                                        Text="With standard field and auto width">
                                        <Editor>
                                            <ext:Editor ID="Editor1" runat="server" AutoSize="Width" />                                                
                                        </Editor>
                                    </ext:Label>
                                    <br />
                                    <br />
                                    <ext:Label 
                                        ID="Label4" 
                                        runat="server" 
                                        Cls="editable" 
                                        Icon="NoteEdit"
                                        OverCls="editable-over"
                                        Text="Double click editing">
                                        <Editor>
                                            <ext:Editor ID="Editor3" runat="server" AutoSize="Width" ActivateEvent="dblclick" />                                                
                                        </Editor>
                                    </ext:Label>
                                    <br />
                                    <br />
                                    <ext:Label ID="Label5" runat="server" 
                                        Cls="editable" 
                                        Icon="NoteEdit"
                                        OverCls="editable-over"
                                        Text="Custom editor aligment">
                                        <Editor>
                                            <ext:Editor ID="Editor4" runat="server" AutoSize="Width" HideEl="false">
                                                <Alignment ElementAnchor="TopLeft" TargetAnchor="BottomLeft" />                                                
                                            </ext:Editor>                                                
                                        </Editor>
                                    </ext:Label>
                                    <br />
                                    <br />
                                    <ext:Label ID="Label3" runat="server" 
                                        Cls="editable" 
                                        Html="<hr/><p>With TextArea</p><p>Line1</p><p>Line2</p><hr/>">
                                        <Editor>
                                            <ext:Editor ID="Editor2" runat="server" AutoSize="Fit">
                                                <Field>
                                                    <ext:TextArea runat="server" Cls="x-form-field-editor" />
                                                </Field>
                                            </ext:Editor>                                                
                                        </Editor>
                                    </ext:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="aspLabel" runat="server" Text="&amp;lt;asp:Label> with Editor" />

                                    <p><ext:Editor runat="server" Target="aspLabel" /></p>
                                </Body>
                            </ext:Tab>
                            
                            <ext:Tab ID="Tab2" runat="server" Title="Divs">
                                <Body>
                                    <div class="editable" field="txt">Lorem ipsum dolor sit amet #1</div>
                                    <div class="editable" field="txt">Lorem ipsum dolor sit amet #2</div>
                                    <div class="editable" field="tar">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas a urna. Praesent libero. Nullam vitae nisl. Maecenas pellentesque, magna eget pharetra fermentum, tortor dolor suscipit est, ultricies sollicitudin turpis orci vitae quam. Vestibulum augue diam, auctor ac, interdum eu, egestas ac, leo. Nulla justo mauris, mollis quis, consectetur non, varius ac, nunc. Sed facilisis. Sed elementum. Praesent tortor. Maecenas ipsum tellus, fringilla nec, scelerisque sed, scelerisque a, nulla. Phasellus eleifend interdum arcu. Nulla porta ligula placerat tortor.</div>
                                    
                                    <ext:Editor ID="TextLineEditor" runat="server" AutoSize="Fit" />
                                    
                                    <ext:Editor ID="TextBlockEditor" runat="server" AutoSize="Fit">
                                        <Field>
                                            <ext:TextArea runat="server" />
                                        </Field>
                                    </ext:Editor>
                                </Body>
                                <Listeners>
                                    <Activate Handler="#{TextLineEditor}.retarget(${#Tab2 [@field=txt].editable});#{TextBlockEditor}.retarget(${#Tab2 [@field=tar].editable});" Single="true" />
                                </Listeners>
                            </ext:Tab>
                            
                            <ext:Tab ID="Tab3" runat="server" Title="Ajax Update">
                                <Body>
                                    <ext:Label 
                                        ID="AjaxLabel" 
                                        runat="server" 
                                        Cls="editable" 
                                        Icon="NoteEdit"
                                        OverCls="editable-over"
                                        Text="Ajax Label">
                                        <Editor>
                                            <ext:Editor ID="AjaxLabelEditor" runat="server" AutoSize="Width" UpdateEl="false">
                                                <AjaxEvents>
                                                    <Complete OnEvent="CompleteEdit">
                                                        <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{Tab3}.body}" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="value" Value="value" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Complete>
                                                </AjaxEvents>
                                            </ext:Editor>                                                
                                        </Editor>
                                    </ext:Label>
                                </Body>
                            </ext:Tab>
                            
                            <ext:Tab ID="Tab4" runat="server" Title="Editor Panel">
                                <Body>
                                    <ext:Panel 
                                        ID="Panel1" 
                                        runat="server" 
                                        Width="610" 
                                        Height="300" 
                                        Html="Content" 
                                        BodyStyle="padding:6px;"
                                        Title="Editor panel">            
                                        <TopBar>
                                            <ext:Toolbar runat="server">
                                                <Items>
                                                    <ext:ToolbarButton ID="Button1" runat="server" Text="Edit" Icon="Pencil">
                                                        <Listeners>
                                                            <Click Handler="el.setDisabled(true);#{Button2}.setDisabled(false);#{PanelEditor}.startEdit(#{Panel1}.getBody());" />
                                                        </Listeners>
                                                    </ext:ToolbarButton>
                                                    <ext:ToolbarButton ID="Button2" runat="server" Text="Save" Icon="Disk" Disabled="true">
                                                        <Listeners>
                                                            <Click Handler="el.setDisabled(true);#{Button1}.setDisabled(false);#{PanelEditor}.completeEdit();" />
                                                        </Listeners>
                                                    </ext:ToolbarButton>    
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                    </ext:Panel>
                                    <ext:Editor 
                                        ID="PanelEditor" 
                                        runat="server"
                                        AutoSize="Fit"
                                        Shadow="None">
                                        <Field>
                                            <ext:HtmlEditor ID="HtmlEditor1" runat="server" />
                                        </Field>
                                    </ext:Editor>
                                </Body>
                                <Listeners>
                                    <Deactivate Handler="#{Button1}.setDisabled(false);#{Button2}.setDisabled(true);#{PanelEditor}.completeEdit();" />
                                </Listeners>
                            </ext:Tab>
                        </Tabs>
                    </ext:TabPanel>
                </ext:FitLayout>
            </Body>
        </ext:ViewPort>        
    </form>    
</body>
</html>
