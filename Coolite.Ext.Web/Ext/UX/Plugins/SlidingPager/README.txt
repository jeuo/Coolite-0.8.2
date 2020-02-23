// In Markup

<ext:Panel ID="Panel1" runat="server" Title="Accordion" Width="185" Height="350">
    <Body>
        <ext:Accordion runat="server">
            <ext:Panel runat="server" Title="Item1" />
            <ext:Panel runat="server" Title="Item2" />
            <ext:Panel runat="server" Title="Item3" />
            <ext:Panel runat="server" Title="Item4" />
        </ext:Accordion>
    </Body>
    <Plugins>
        <ext:KeepActive runat="server" />
    </Plugins>
</ext:Panel>



// Manual Add

Ext.onReady(function() {
    var pnl = new Ext.Panel({
        title: "Accordion",
        layout: "accordion",
        width: 185,
        height: 350,
        items: [
            { title: "Item1" },
            { title: "Item2" },
            { title: "Item3" },
            { title: "Item4" }
        ],
        plugins: [ new Ext.ux.plugins.KeepActive() ]
    });
    
    pnl.render(Ext.getBody());
});