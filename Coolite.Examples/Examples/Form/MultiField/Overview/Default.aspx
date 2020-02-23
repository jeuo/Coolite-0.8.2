<%@ Page Language="C#" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Field Note - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .ext-ie7 .onepx-shift {
            top: 1px;
            position: relative;
        }
        
        .dot-label {
            font-weight: bold; 
            font-size: 20px;
        }
        
        .form-toolbar {
	        top: 1px;
            position: relative;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:FormPanel runat="server" Height="400" Title="Form Panel" BodyStyle="padding:5px">
            <Body>
                <ext:FormLayout runat="server">
                    <ext:Anchor>
                        <ext:MultiField runat="server" FieldLabel="Text fields">
                            <Fields>
                                <ext:TextField runat="server" Width="150" />
                                <ext:TextField runat="server" Width="300" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField runat="server" FieldLabel="Mix">
                            <Fields>
                                <ext:TextField runat="server" Width="150" Cls="onepx-shift" />
                                <ext:ComboBox runat="server" Width="300" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField runat="server" FieldLabel="With toolbar">
                            <Fields>
                                <ext:TextField runat="server" Width="150" Cls="onepx-shift" />
                                <ext:DateField runat="server" Width="300" />
                                <ext:Toolbar runat="server" Cls="form-toolbar" Flat="true">
                                    <Items>
                                        <ext:ToolbarButton runat="server" Text="Button" Icon="Add" />
                                        <ext:ToolbarSplitButton runat="server" Text="Split" Icon="ArrowDown">
                                            <Menu>
                                                <ext:Menu runat="server">
                                                    <Items>
                                                        <ext:MenuItem runat="server" Text="Item1" />
                                                        <ext:MenuItem runat="server" Text="Item2" />
                                                    </Items>
                                                </ext:Menu>
                                            </Menu>
                                        </ext:ToolbarSplitButton>
                                    </Items>
                                </ext:Toolbar>
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField ID="MultiField1" runat="server" FieldLabel="With button">
                            <Fields>
                                <ext:TextField ID="TextField1" runat="server" Width="150" Cls="onepx-shift" />
                                <ext:DateField ID="DateField1" runat="server" Width="300" />
                                <ext:Button ID="Button1" runat="server" Text="..." Cls="onepx-shift" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField runat="server" FieldLabel="IP Address">
                            <Fields>
                                <ext:NumberField runat="server" Width="40" />
                                <ext:Label runat="server" Text="." Cls="dot-label" />
                                <ext:NumberField runat="server" Width="40" />
                                <ext:Label runat="server" Text="." Cls="dot-label" />
                                <ext:NumberField runat="server" Width="40" />
                                <ext:Label runat="server" Text="." Cls="dot-label" />
                                <ext:NumberField runat="server" Width="40" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField runat="server" FieldLabel="Long note" Note="Lorem ipsum dolor sit amet, consectetuer adipiscing elit">
                            <Fields>
                                <ext:TextField runat="server" />
                                <ext:TextField runat="server" />
                                <ext:TextField runat="server" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                    
                    <ext:Anchor>
                        <ext:MultiField ID="MultiField2" runat="server" FieldLabel="Several notes">
                            <Fields>
                                <ext:TextField runat="server" Note="Note" />
                                <ext:TextField runat="server" Note="Note" />
                                <ext:TextField runat="server" Note="Note" />
                            </Fields>
                        </ext:MultiField>
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>
        </ext:FormPanel>
    </form>    
</body>
</html>
