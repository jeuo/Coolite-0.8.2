﻿<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FormPanel - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void UploadClick(object sender, AjaxEventArgs e)
        {
            string tpl = "Uploaded file: {0}<br/>Size: {1} bytes";
            
            if(this.FileUploadField1.HasFile)
            {
                Ext.Msg.Show(new MessageBox.Config
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.INFO,
                    Title = "Success",
                    Message = string.Format(tpl, this.FileUploadField1.PostedFile.FileName, this.FileUploadField1.PostedFile.ContentLength)
                });
            }
            else
            {
                Ext.Msg.Show(new MessageBox.Config
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "Fail",
                    Message = "No file uploaded"
                });
            }
        }
    </script>
    
    <style type="text/css">
        #fi-button-msg {
            border: 2px solid #ccc;
            padding: 5px 10px;
            background: #eee;
            margin: 5px;
            float: left;
        }
    </style>
    
    <script type="text/javascript">
        var showFile = function (fb, v) {
            var el = Ext.fly('fi-button-msg');
            el.update('<b>Selected:</b> ' + v);
            if (!el.isVisible()) {
                el.slideIn('t', {
                    duration: .2,
                    easing: 'easeIn',
                    callback: function() {
                        el.highlight();
                    }
                });
            } else {
                el.highlight();
            }
        }            
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h1>File Upload Field</h1>

        <h2>Basic FileUpload</h2>
        
        <p>A typical file upload field with Ext style.  Direct editing of the text field cannot be done in a 
            consistent, cross-browser way, so it is always read-only in this implementation.</p>
        
        <ext:FileUploadField ID="BasicField" runat="server" Width="400" Icon="Attach" />
        
        <ext:Button runat="server" Text="Get File Path">
            <Listeners>
                <Click Handler="var v = #{BasicField}.getValue(); Ext.Msg.alert('Selected&nbsp;File', v && v != '' ? v : 'None');" />
            </Listeners>
        </ext:Button>

        <h2>Basic FileUpload (Button-only)</h2>
        
        <p>You can also render the file input as a button without the text field, with access to the field's value via the 
            standard <tt>TextField</tt> interface or by handling the <tt>FileSelected</tt> event (as in this example).</p>
            
        <ext:FileUploadField runat="server" ButtonOnly="true">
            <Listeners>
                <FileSelected Fn="showFile" />
            </Listeners>
        </ext:FileUploadField>
        <div id="fi-button-msg" style="display:none;"></div>
        <div class="x-clear"></div>
        
        <h2>Form Example</h2>
        
        <p>The FileUploadField can also be used in standard form layouts, with support for anchoring, validation (the
            field is required in this example), empty text, etc.</p>
            
        <ext:FormPanel 
            ID="BasicForm" 
            runat="server"
            Width="500"
            Frame="true"
            Title="File Upload Form"
            AutoHeight="true"
            MonitorValid="true"
            BodyStyle="padding: 10px 10px 0 10px;">                
            <Defaults>
                <ext:Parameter Name="anchor" Value="95%" Mode="Value" />
                <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
            </Defaults>
            <Body>
                <ext:FormLayout runat="server" LabelWidth="50">
                    <ext:Anchor>
                        <ext:TextField ID="PhotoName" runat="server" FieldLabel="Name" />
                    </ext:Anchor>
                    <ext:Anchor>
                        <ext:FileUploadField 
                            ID="FileUploadField1" 
                            runat="server" 
                            EmptyText="Select an image"
                            FieldLabel="Photo"
                            ButtonText=""
                            Icon="ImageAdd">
                        </ext:FileUploadField>
                    </ext:Anchor>
                </ext:FormLayout>
            </Body>
            <Listeners>
                <ClientValidation Handler="#{SaveButton}.setDisabled(!valid);" />
            </Listeners>
            <Buttons>
                <ext:Button ID="SaveButton" runat="server" Text="Save">
                    <AjaxEvents>
                        <Click 
                            OnEvent="UploadClick"
                            Before="if(!#{BasicForm}.getForm().isValid()) { return false; } 
                                Ext.Msg.wait('Uploading your photo...', 'Uploading');"
                                
                            Failure="Ext.Msg.show({ 
                                title   : 'Error', 
                                msg     : 'Error during uploading', 
                                minWidth: 200, 
                                modal   : true, 
                                icon    : Ext.Msg.ERROR, 
                                buttons : Ext.Msg.OK 
                            });">
                        </Click>
                    </AjaxEvents>
                </ext:Button>
                <ext:Button runat="server" Text="Reset">
                    <Listeners>
                        <Click Handler="#{BasicForm}.getForm().reset();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:FormPanel>
    </form>
</body>
</html>