<%@ Page Title="" Language="C#" MasterPageFile="NoneID.Master" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>

<%@ Register src="Alias.ascx" tagname="Alias" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    
    <h3>Example</h3>
    
    <uc1:Alias ID="Alias1" runat="server" />
    
    <ext:Button runat="server" Text="UserControl">
        <Listeners>
            <Click Handler="Coolite.AjaxMethods.UC.HelloUserControl();" />
        </Listeners>
    </ext:Button>
    <br />
    <ext:Button ID="Button1" runat="server" Text="MasterPage">
        <Listeners>
            <Click Handler="Coolite.AjaxMethods.HelloMasterPage();" />
        </Listeners>
    </ext:Button>
</asp:Content>
