﻿<%@ Page Language="C#" %>
<%@ Import Namespace="System.Threading"%>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<script runat="server">
    protected void Button1_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Confirm("Confirm", "Are you sure you want to do that?", new JFunction { Fn = "showResult" }).Show();
    }

    protected void Button2_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Prompt("Name", "Please enter your name:", new JFunction { Fn = "showResultText" }).Show();
    }

    protected void Button3_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Show(new MessageBox.Config
        {
            Title = "Address",
            Message = "Please enter your address:",
            Width = 300,
            Buttons = MessageBox.Button.OKCANCEL,
            Multiline = true,
            AnimEl = this.Button3.ClientID,
            Fn = new JFunction { Fn = "showResultText" }
        });
    }

    protected void Button4_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Show(new MessageBox.Config
        {
            Title = "Save Changes?",
            Message = "You are closing a tab that has unsaved changes. <br />Would you like to save your changes?",
            Buttons = MessageBox.Button.YESNOCANCEL,
            Icon = MessageBox.Icon.QUESTION,
            Fn = new JFunction { Fn = "showResult" },
            AnimEl = this.Button4.ClientID
        });
    }
    
    protected void Button5_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Show(new MessageBox.Config
        {
            Title = "Please wait",
            Message = "Loading items...",
            ProgressText = "Initializing...",
            Width = 300,
            Progress = true,
            Closable = false,
            AnimEl = this.Button5.ClientID
        });
        
        this.StartLongAction();
    }

    protected void Button6_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Show(new MessageBox.Config
        {
            Message = "Saving your data, please wait...",
            ProgressText = "Saving...",
            Width = 300,
            Wait = true,
            WaitConfig = new ProgressBar.WaitConfig { Interval = 200 },
            IconCls = "ext-mb-download",
            AnimEl = this.Button6.ClientID
        });

        this.ScriptManager1.AddScript("setTimeout(function(){ Ext.MessageBox.hide(); Ext.example.msg('Done', 'Your data was saved!'); }, 8000);");
    }

    protected void Button7_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Alert("Status", "Changes saved successfully.", new JFunction { Fn = "showResult" }).Show();
    }

    protected void Button8_Click(object sender, AjaxEventArgs e)
    {
        Ext.Msg.Show(new MessageBox.Config
        {
            Title = "Icon Support",
            Message = "Message with an Icon",
            Buttons = MessageBox.Button.OK,
            Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), e.ExtraParams["Icon"]),
            AnimEl = this.Button8.ClientID,
            Fn = new JFunction { Fn = "showResult" }
        });
    }
    
    private void StartLongAction()
    {
        this.Session["Task1"] = 0;
        ThreadPool.QueueUserWorkItem(LongAction);
        
        this.TaskManager1.StartTask("Task1");
    }

    private void LongAction(object state)
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
            this.Session["Task1"] = i + 1;
        }
        this.Session.Remove("Task1");
    }

    protected void RefreshProgress(object sender, AjaxEventArgs e)
    {
        object progress = this.Session["Task1"];
        if (progress != null)
        {
            Ext.MessageBox.UpdateProgress(((int)progress) / 10f, string.Format("Step {0} of {1}...", progress.ToString(), 10));
        }
        else
        {
            this.TaskManager1.StopTask("Task1");
            Ext.MessageBox.Hide();
            this.ScriptManager1.AddScript("Ext.example.msg('Done', 'Your fake items were loaded!');");
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MessageBox - Coolite Toolkit Examples</title>   
    
    <ext:ScriptContainer runat="server" /> 
   
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../resources/examples.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var showResult = function (btn) {
            Ext.example.msg('Button Click', 'You clicked the {0} button', btn);
        };

        var showResultText = function (btn, text) {
            Ext.example.msg('Button Click', 'You clicked the {0} button and entered the text "{1}".', btn, text);
        };
    </script> 
    
    <style type="text/css">
        .x-window-dlg .ext-mb-download {
            background: transparent url(resources/images/download.gif) no-repeat top left;
            height: 46px;
        }
    </style>  
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <h1>MessageBox Dialogs</h1>
    
    <p>The following samples demonstrate how to display various MessageBox options.</p>
    
    <h2>1. Confirm</h2>
    
    <span>Standard Yes/No dialog.</span>
    
    <ext:Button ID="Button1" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button1_Click" />
        </AjaxEvents>
    </ext:Button>
    
    <h2>2. Prompt</h2>
    
    <span>Standard prompt dialog.</span>
    
    <ext:Button ID="Button2" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button2_Click" />
        </AjaxEvents>
    </ext:Button>
    
    <h2>3. Multi-line Prompt</h2>
    
    <span>A multi-line prompt dialog.</span>
    
    <ext:Button ID="Button3" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button3_Click" />
        </AjaxEvents>
    </ext:Button>
    
    <h2>4. Yes/No/Cancel</h2>
    
    <span>Standard Yes/No/Cancel dialog.</span>
    
    <ext:Button ID="Button4" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button4_Click" />
        </AjaxEvents>
    </ext:Button>
    
    <h2>5. Progress Dialog</h2>
    
    <span>Dialog with measured progress bar.</span>
    
    <ext:Button ID="Button5" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button5_Click" />
        </AjaxEvents>
    </ext:Button>

    <ext:TaskManager ID="TaskManager1" runat="server">
        <Tasks>
            <ext:Task 
                TaskID="Task1"
                Interval="1000" 
                AutoRun="false">
                <AjaxEvents>
                    <Update OnEvent="RefreshProgress" />
                </AjaxEvents>                    
            </ext:Task>
        </Tasks>
    </ext:TaskManager>

    <h2>6. Wait Dialog</h2>
    
    <span>Dialog with indefinite ProgressBar and custom Icon (will close after 8 sec).</span>
    
    <ext:Button ID="Button6" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button6_Click" />
        </AjaxEvents>
    </ext:Button>

    <h2>7. Alert</h2>
    
    <span>Standard Alert dialog.</span>
    
    <ext:Button ID="Button7" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button7_Click" />
        </AjaxEvents>
    </ext:Button>

    <h2>8. Icons</h2>
    
    <span>Standard Alert with optional Icon.</span>
    
    <ext:ComboBox ID="ComboBox1" runat="server" Editable="false">
        <Items>
            <ext:ListItem Text="Error" Value="ERROR" />
            <ext:ListItem Text="Informational" Value="INFO" />
            <ext:ListItem Text="Question" Value="QUESTION" />
            <ext:ListItem Text="Warning" Value="WARNING" />
        </Items>
        <SelectedItem Value="ERROR" />
    </ext:ComboBox>
    
    <ext:Button ID="Button8" runat="server" Text="Show">
        <AjaxEvents>
            <Click OnEvent="Button8_Click">
                <ExtraParams>
                    <ext:Parameter Name="Icon" Value="#{ComboBox1}.getValue()" Mode="Raw"></ext:Parameter>
                </ExtraParams>
            </Click>
        </AjaxEvents>
    </ext:Button>
</body>
</html>
