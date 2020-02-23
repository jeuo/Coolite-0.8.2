function createExampleTab(id, url){
    var win, tab, hostName, exampleName, node;
    
    if(id == "-"){
        id = Ext.id();
        url = "/Examples"+url;
    }
    
    win = new Ext.Window({
        id: "w"+id,
        layout: "fit",        
        title: "Source code",
        iconCls: "icon-pagewhitecode",
        width: 925,
        height: 650,
        maximizable: true,
        constrain: true,
        closeAction: "hide",
        listeners: {
            beforeshow: {
                fn: function(el) {
                    var height = Ext.getBody().getViewSize().height;
                    if (el.getSize().height > height) {
                        el.setHeight(height - 20)
                    }
                }
            },
            show:{
                fn:function(){
                    this.body.mask("Loading...", "x-mask-loading");
                    Ext.Ajax.request({
                        url: 'ExampleLoader.ashx',
                        success: function(response) { 
                            this.body.unmask();
                            eval(response.responseText); 
                        },
                        failure: function(response) {
                            this.body.unmask();
                            Ext.Msg.alert('Failure', 'The error during example loading:\n' + response.responseText);
                        },
                        params: { id: id, url: url, wId: this.id},
                        scope: this
                    });
                },
                
                single:true
            }
        },
        buttons:[
            {
                id: "b"+id,
                text: "Download",
                iconCls: "icon-compress",
                listeners: {
                    click: {
                        fn: function(el, e) {
                            window.location = "/GenerateSource.ashx?t=1&e="+url;
                        }
                    }
                }
            }
        ]        
    });
    
    hostName = window.location.protocol+"//"+window.location.host;
    exampleName = url.substr(9);
    
    tab = ExampleTabs.add(new Ext.Panel({
        id: id,
        tbar: [{
            text: 'Source Code',
            iconCls: 'icon-pagewhitecode',
            listeners: {
                'click': function() {
                    Ext.getCmp('w'+id).show(null);
                }
            }
        },
        '->', 
	    {
            text: 'Direct Link',
            iconCls: 'icon-link',
            handler: function() {
                new Ext.Window({
                    modal: true,
                    iconCls: "icon-link",
                    layout: 'absolute',
                    defaultButton: "dl"+id,
                    width: 400,
                    height: 110,
                    title: "Direct Link",
                    closable: false,
                    resizable: false,
                    items: [{
                        xtype: "textfield",
                        cls: "dlText",
                        width: 364,
                        x: 10,
                        y: 10,
                        selectOnFocus: true,
                        id: "dl"+id,
                        readOnly: true,
                        value: hostName+"/?"+exampleName
                    }],
                    buttons: [{
                        xtype: "button",
                        text: " Open",
                        iconCls: "icon-applicationdouble",
                        tooltip: "Open Example in the separate window",
                        handler: function() {
                            window.open(hostName+"/?"+exampleName);
                        }
                    },
                    {
                        xtype: "button",
                        text: " Open (Direct)",
                        iconCls: "icon-applicationgo",
                        tooltip: "Open Example in the separate window using a direct link",
                        handler: function() {
                            window.open(hostName+url, '_blank');
                        }
                    },
                    {
                        xtype: "button",
                        text: "Close",
                        handler: function() {
                            this.findParentByType("window").hide(null);
                        }
                    }]
                }).show(null);
            }
        },
        '-', 
        {
            text: 'Refresh',
            handler: function() {
                Ext.getCmp(id).reload(true)
            },
            iconCls: 'icon-arrow-refresh'
        }],
        title: "Overview",
        autoLoad: {
            showMask: true,
            scripts: true,
            mode: "iframe",
            url: hostName+url
        },
        listeners: {
            deactivate: {
                fn: function(el) {
                    if (this.sWin && this.sWin.isVisible()) {
                        this.sWin.hide();
                    }
                }
            }
        },
        closable: true
    }));
    
    tab.sWin = win;
    ExampleTabs.setActiveTab(tab);
    
    node = exampleTree.getNodeById(id);            
    if(node){
        node.ensureVisible();
    }   
}