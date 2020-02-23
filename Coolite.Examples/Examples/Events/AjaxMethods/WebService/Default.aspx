<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calling an AjaxMethod from a WebService - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        var callXmlWebMethod = function (name) {
            Coolite.AjaxMethod.request({
                url          : "TestXmlService.asmx/SayHello",
                cleanRequest : true,
                params       : {
                    name : name
                },
                success      : function (result) {
                    Ext.Msg.alert("Message", Ext.DomQuery.selectValue("string", result, ""));
                }
            });
        }
        
        var callJsonWebMethod = function (name) {
            Coolite.AjaxMethod.request({
                url          : "TestJsonService.asmx/SayHello",
                cleanRequest : true,
                json         : true,
                params       : {
                    name : name
                },
                success : function (result) {
                    Ext.Msg.alert("Message", result);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server"/>
        
        <ext:Panel 
            runat="server" 
            Title="Say Hello" 
            Width="300" 
            Height="185" 
            Frame="true" 
            ButtonAlign="Center">
            <Body>
                <ext:FormLayout runat="server">
                    <Anchors>
                        <ext:Anchor Horizontal="100%">
                            <ext:TextField 
                                ID="txtName" 
                                runat="server" 
                                FieldLabel="Name" 
                                EmptyText="Your name here..." 
                                />
                        </ext:Anchor>
                    </Anchors>
                </ext:FormLayout>
            </Body>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="XML WebMethod">
                    <Listeners>
                        <Click Handler="callXmlWebMethod(#{txtName}.getValue());" />
                    </Listeners>
                </ext:Button>
                
                <ext:Button ID="Button2" runat="server" Text="JSON WebMethod">
                    <Listeners>
                        <Click Handler="callJsonWebMethod(#{txtName}.getValue());" />
                    </Listeners>
                </ext:Button>
           </Buttons>
        </ext:Panel>
    </form>
</body>
</html>