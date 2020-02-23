<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Toolbar with Menus - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .menu-title{
            background: #ebeadb;
            border-bottom:1px solid #99bbe8;
            color:#15428b;
            font:bold 10px tahoma,arial,verdana,sans-serif;
            display:block;
            padding:3px;
        } 
    </style>
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            Coolite.Ext.Web.MenuItem item = new Coolite.Ext.Web.MenuItem();
            item.Text = "Dynamically added Item";
            item.Handler = "onItemClick";
            MenuButton.Menu.Primary.Items.Add(item);

            item = new Coolite.Ext.Web.MenuItem();
            item.Text = "Disabled Item";
            item.Disabled = true;
            MenuButton.Menu.Primary.Items.Add(item);
        }
    </script>
    
    <script type="text/javascript">
        var onButtonClick = function (btn) {
            msg('Button Click','You clicked the "{0}" button.', btn.text);
        }

        var onItemClick = function (item) {
            msg('Menu Click', 'You clicked the "{0}" menu item.', item.text);
        }

        var onItemCheck = function (item, checked) {
            msg('Item Check', 'You {1} the "{0}" menu item.', item.text, checked ? 'checked' : 'unchecked');
        }

        var onItemToggle = function (item, pressed) {
            msg('Button Toggled', 'Button "{0}" was toggled to {1}.', item.text, pressed);
        }

        var msg = function (title, format) {
            var s = String.format.apply(String, Array.prototype.slice.call(arguments, 1));
            Ext.get('notificationArea').update(s).highlight();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug" />

        <h1>Toolbar with Menus</h1>        
        
        <ext:Toolbar runat="server" Width="500">
            <Items>
                <ext:ToolbarButton ID="MenuButton" runat="server" Text="Button w/Menu" Icon="ArrowDown">
                    <Menu>
                        <ext:Menu runat="server">
                            <Items>
                                <ext:CheckMenuItem runat="server" Checked="true" Text="I like ASP.NET" CheckHandler="onItemCheck" />
                                <ext:CheckMenuItem runat="server" Checked="true" Text="Item 2" CheckHandler="onItemCheck" />
                                <ext:CheckMenuItem runat="server" Text="Item 3" CheckHandler="onItemCheck" />
                                <ext:MenuSeparator />
                                <ext:MenuItem runat="server" Text="Radio Options">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:TextMenuItem runat="server" Text="<b class='menu-title'>Choose a Theme</b>" />
                                                <ext:CheckMenuItem runat="server" Text="Aero Glass"  Checked="true" Group="theme" CheckHandler="onItemCheck" />
                                                <ext:CheckMenuItem runat="server" Text="Vista Black" Group="theme" CheckHandler="onItemCheck" />
                                                <ext:CheckMenuItem runat="server" Text="Gray Theme" Group="theme" CheckHandler="onItemCheck" />
                                                <ext:CheckMenuItem runat="server" Text="Default Theme" Group="theme" CheckHandler="onItemCheck" />
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:MenuItem>
                                <ext:MenuItem runat="server" Text="Choose a Date" Icon="Calendar">
                                    <Menu>
                                        <ext:DateMenu runat="server">
                                            <Picker runat="server">
                                                <Listeners>
                                                    <Select Handler="msg('Date Selected', 'You chose {0}.', date.format('M j, Y'));" />
                                                </Listeners>
                                            </Picker>
                                        </ext:DateMenu>
                                    </Menu>
                                </ext:MenuItem>
                                <ext:MenuItem runat="server" Text="Choose a Color" Icon="ColorSwatch">
                                    <Menu>
                                        <ext:ColorMenu runat="server">
                                            <Palette runat="server">
                                                <Listeners>
                                                    <Select Handler="msg('Color Selected', 'You chose {0}.', color);" />
                                                </Listeners>
                                            </Palette>
                                        </ext:ColorMenu>
                                    </Menu>
                                </ext:MenuItem>
                                <ext:MenuSeparator />
                            </Items>
                        </ext:Menu>
                    </Menu>
                </ext:ToolbarButton>
                
                <ext:ToolbarSplitButton runat="server" Text="Split Button" Icon="NoteGo" Handler="onButtonClick">
                    <Menu>
                        <ext:Menu runat="server">
                            <Items>
                                <ext:MenuItem runat="server" Text="<b>Bold</b>" Handler="onItemClick"></ext:MenuItem>
                                <ext:MenuItem runat="server" Text="<i>Italic</i>" Handler="onItemClick"></ext:MenuItem>
                                <ext:MenuItem runat="server" Text="<u>Underline</u>" Handler="onItemClick"></ext:MenuItem>
                                <ext:MenuSeparator />
                                <ext:MenuItem runat="server" Text="Pick a Color" Handler="onItemClick">
                                    <Menu>
                                        <ext:Menu runat="server">
                                            <Items>
                                                <ext:ColorMenuItem runat="server">
                                                    <Palette ID="Palette1" runat="server">
                                                        <Listeners>
                                                            <Select Handler="msg('Color Selected', 'You chose {0}.', color);" />
                                                        </Listeners>
                                                    </Palette>
                                                </ext:ColorMenuItem>
                                                <ext:MenuItem runat="server" Text="More Colors..."></ext:MenuItem>
                                            </Items>
                                        </ext:Menu>
                                    </Menu>
                                </ext:MenuItem>
                                 <ext:MenuItem runat="server" Text="Extellent!" Handler="onItemClick"></ext:MenuItem>
                            </Items>
                        </ext:Menu>
                    </Menu>
                    <ToolTips>
                        <ext:ToolTip ID="Tip1" runat="server" Title="Tip Title" Html="This is a an example QuickTip for a toolbar item" /> 
                    </ToolTips>
                </ext:ToolbarSplitButton>
                <ext:ToolbarSeparator />
                <ext:ToolbarButton Text="Toggle Me" EnableToggle="true" ToggleHandler="onItemToggle" />
                <ext:ToolbarSeparator />
                
                <ext:ToolbarButton Icon="Table">
                    <ToolTips>
                        <ext:ToolTip Html="<b>Quick Tips</b><br/>Icon only button with tooltip" />
                    </ToolTips>
                </ext:ToolbarButton>
                
                <ext:ToolbarSeparator />
                
                <ext:ComboBox ID="ComboBox1" runat="server" EmptyText="Select an option">
                    <Items>
                        <ext:ListItem Text="Option1" />
                        <ext:ListItem Text="Option2" />
                        <ext:ListItem Text="Option3" />
                        <ext:ListItem Text="Option4" />
                        <ext:ListItem Text="Option5" />
                    </Items>
                </ext:ComboBox>
            </Items>
        </ext:Toolbar>
        
        <div id="notificationArea" style="width:478px; padding:10px; border:1px solid black; height:40px;"></div>
    </form>
</body>
</html>