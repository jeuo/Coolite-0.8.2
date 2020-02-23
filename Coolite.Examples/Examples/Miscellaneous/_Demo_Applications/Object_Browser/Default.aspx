<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Object Browser - Coolite Toolkit Examples</title>    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    <link href="resources/css/styles.css" rel="stylesheet" type="text/css" />    
    
    <script type="text/javascript">
        function loadMembers(node, membersTree) {
            if (node.attributes.isType && node.attributes.marker) {
                var root = membersTree.getRootNode();
                root.attributes.marker = node.attributes.marker;
                membersTree.el.mask('Members Loading...');
                root.reload(removeMask.createCallback(membersTree));
            }
        }

        function removeMask(membersTree) {
            membersTree.el.unmask();
        }

        function refreshDescription(result, label) {
            var params = result.extraParamsResponse || {};

            if (params.mPrototype) {
                label.setText(params.mPrototype);
            }
        }

        function clearDescription(label) {
            label.setText('');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />
        
        <ext:ViewPort runat="server">
            <Body>
                <ext:BorderLayout runat="server">
                    <West Split="true" Collapsible="true">
                        <ext:TreePanel runat="server" IconCls="namespace-icon" Title="Namespaces of loaded assemblies" AutoScroll="true" UseArrows="true" RootVisible="false" Width="350">
                            <Root>
                                <ext:AsyncTreeNode>
                                    <CustomAttributes>
                                        <ext:ConfigItem Name="marker" Value="_root" Mode="Value" />
                                    </CustomAttributes>
                                </ext:AsyncTreeNode>
                            </Root>            
                            <Loader>
                                <ext:WebServiceTreeLoader DataUrl="ReflectionService.asmx/GetNamespaces" PreloadChildren="true">    
                                    <BaseParams>
                                        <ext:Parameter Name="marker" Value="node.attributes.marker" Mode="Raw" />
                                    </BaseParams>
                                    <Listeners>
                                        <LoadException Handler="Ext.Msg.alert('Error','Request failed: \n'+response.responseText);" />
                                    </Listeners>               
                                </ext:WebServiceTreeLoader>
                            </Loader>
                            <Listeners>
                                <Click Handler="clearDescription(#{lblPrototype});loadMembers(node, #{MemebersTree})" />
                            </Listeners>
                        </ext:TreePanel>
                    </West>
                    <Center>
                        <ext:Panel runat="server">
                            <Body>
                                <ext:BorderLayout runat="server">
                                    <Center>
                                        <ext:TreePanel ID="MemebersTree" runat="server" AutoScroll="true" RootVisible="false">
                                            <Root>
                                                <ext:AsyncTreeNode>
                                                    <CustomAttributes>
                                                        <ext:ConfigItem Name="marker" Value="-" Mode="Value" />
                                                    </CustomAttributes>
                                                </ext:AsyncTreeNode>
                                            </Root>
                                            <Loader>
                                                <ext:WebServiceTreeLoader DataUrl="ReflectionService.asmx/GetMembers">    
                                                    <BaseParams>
                                                        <ext:Parameter Name="marker" Value="node.attributes.marker" Mode="Raw" />
                                                    </BaseParams>
                                                    <Listeners>
                                                        <LoadException Handler="Ext.Msg.alert('Error','Members request failed: \n'+response.responseText);" />
                                                    </Listeners>               
                                                </ext:WebServiceTreeLoader>
                                            </Loader>
                                            <AjaxEvents>
                                                <Click 
                                                    Url="ReflectionService.asmx/GetMemberDescription" 
                                                    Type="Load" 
                                                    Method="POST" 
                                                    CleanRequest="true"
                                                    Success="refreshDescription(result, #{lblPrototype});"
                                                    >                                                    
                                                    <ExtraParams>
                                                        <ext:Parameter Name="marker" Value="node.attributes.marker" Mode="Raw" />
                                                        <ext:Parameter Name="member" Value="node.attributes.member" Mode="Raw" />
                                                    </ExtraParams>
                                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{South}.body}" />
                                                </Click>
                                            </AjaxEvents>
                                        </ext:TreePanel>
                                    </Center>
                                    <South Split="true">
                                        <ext:Panel ID="South" runat="server" Height="150" BodyStyle="padding:6px;">
                                            <Body>
                                                <ext:Label ID="lblPrototype" runat="server" />
                                            </Body>
                                        </ext:Panel>
                                    </South>
                                </ext:BorderLayout>
                            </Body>
                        </ext:Panel>
                    </Center>
                </ext:BorderLayout>            
            </Body>
        </ext:ViewPort>
    </form>
</body>
</html>
