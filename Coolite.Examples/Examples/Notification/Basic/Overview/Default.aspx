<%@ Page Language="C#" %>
<%@ Import Namespace="System.Threading"%>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Ext.IsAjaxRequest)
        {
            ScriptManager1.RegisterIcon(Icon.Information);
        }
    }

    public static string stub = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.";
    
    protected void Option1_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            Html = stub
        });
    }

    protected void Option2_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.TopRight, 
                TargetAnchor = AnchorPoint.TopRight,
                OffsetX = -20,
                OffsetY = 20
            },
            ShowFx = new SlideIn { Anchor = AnchorPoint.TopRight, Options = new Fx.Config { Easing = Easing.BounceOut } },
            HideFx = new Ghost{Anchor = AnchorPoint.TopRight},
            Html = stub
        });
    }

    protected void Option3_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.TopLeft,
                TargetAnchor = AnchorPoint.TopLeft,
                OffsetX = 20,
                OffsetY = 20
            },
            ShowFx = new Frame { Color = "C3DAF9", Count = 1, Options = new Fx.Config{Duration = 2}},
            HideFx = new SwitchOff(),
            Html = stub
        });
    }

    protected void Option4_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            HideDelay = 1000,
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.BottomLeft,
                TargetAnchor = AnchorPoint.BottomLeft,
                OffsetX = 20,
                OffsetY = -20
            },
            ShowFx = new FadeIn { Options = new Fx.Config { Duration = 2 } },
            HideFx = new FadeOut { Options = new FadeOut.FadeOutConfig { Duration = 2, EndOpacity = 0.25f } },
            Html = stub
        });
    }

    protected void Option10_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            Height = 150,
            Width = 300,
            BodyStyle = "padding:10px",
            Html = stub,
            ShowFx = new SlideIn{ Anchor = AnchorPoint.Right},
            HideFx = new SlideOut { Anchor = AnchorPoint.Right }
        });
    }

    protected void Option11_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            AutoHide = false,
            Html = stub
        });
    }

    protected void Option12_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            HideDelay = 2000,
            Html = stub
        });
    }

    protected void Option13_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            PinEvent = "mouseover",
            Html = stub
        });
    }

    protected void Option14_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            CloseVisible = true,
            Html = stub
        });
    }

    protected void Option15_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            Width = 500,
            Height = 400,
            AutoHide = false,
            AutoLoad = new LoadConfig("http://www.google.com",LoadMode.IFrame),
            Html = stub
        });
    }

    protected void Option16_Click(object sender, AjaxEventArgs e)
    {
        WindowListeners listeners = new WindowListeners();
        
        listeners.BeforeShow.Handler = string.Concat(BarLabel.ClientID, ".setText(new Date().format('g:i:s A'));");
        
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            Icon = Icon.Information,
            Height = 150,
            AutoHide = false,
            CloseVisible = true,
            ContentEl = "customEl",
            Listeners = listeners
        });
    }

    protected void Option5_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.TopLeft,
                TargetAnchor = AnchorPoint.BottomRight,
                OffsetX = 10,
                OffsetY = 10,
                El = Window1.ClientID
            },
            ShowFx = new SlideIn { Anchor = AnchorPoint.Top },
            HideFx = new Ghost { Anchor = AnchorPoint.Top },
            Html = stub
        });
    }

    protected void Option6_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            BringToFront = true,
            AlignCfg = new Notification.AlignConfig
            {
                OffsetX = -10,
                OffsetY = -10,
                El = Window1.ClientID
            },
            Html = stub
        });
    }

    protected void Option7_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.Bottom,
                TargetAnchor = AnchorPoint.Top,
                OffsetX = 0,
                OffsetY = -2,
                El = Window1.ClientID
            },
            Width = 300,
            Html = stub
        });
    }

    protected void Option8_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.Top,
                TargetAnchor = AnchorPoint.Bottom,
                OffsetX = 0,
                OffsetY = 2,
                El = Window1.ClientID
            },
            ShowFx = new SlideIn{Anchor = AnchorPoint.Top},
            HideFx = new Ghost{Anchor = AnchorPoint.Top},
            Width = 300,
            Html = stub
        });
    }

    protected void Option9_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            AlignCfg = new Notification.AlignConfig
            {
                ElementAnchor = AnchorPoint.Left,
                TargetAnchor = AnchorPoint.Right,
                OffsetX = 2,
                OffsetY = 0,
                El = Window1.ClientID
            },
            ShowFx = new SlideIn { Anchor = AnchorPoint.Left },
            HideFx = new Ghost { Anchor = AnchorPoint.Left },
            Height = 350,
            Html = stub
        });
    }

    protected void Option17_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            ShowPin = true,
            Html = stub
        });
    }

    protected void Option18_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            ShowPin = true,
            Pinned = true,
            Html = stub
        });
    }

    protected void Option19_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            Title = "Title",
            ShowPin = true,
            Tools = new ToolsCollection{new Tool{Type = ToolType.Help, Handler = Ext.MessageBox.Alert("Help","Help button clicked").Serialize()}},
            Html = stub
        });
    }

    protected void Option20_Click(object sender, AjaxEventArgs e)
    {
        Ext.Notification.Show(new Notification.Config
        {
            PinEvent = "none",
            Html = stub
        });
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Summary of Notification options - Coolite Toolkit Examples</title>  
    <style type="text/css">
        .menu-label {
            border-bottom: dotted 1px;
            margin-left:25px;
        }
    </style> 
</head>
<body>
    <ext:ScriptManager ID="ScriptManager1" runat="server" />
    
    <ext:Window ID="Window1" runat="server" Title="Notifications" Width="300" Height="350">
        <Body>
            <ext:Accordion runat="server">
                <ext:MenuPanel runat="server" Title="Basic Align" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show bottom right">
                                <AjaxEvents>
                                    <Click OnEvent="Option1_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text=" top right">
                                <AjaxEvents>
                                    <Click OnEvent="Option2_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show top left">
                                <AjaxEvents>
                                    <Click OnEvent="Option3_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show bottom left">
                                <AjaxEvents>
                                    <Click OnEvent="Option4_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="Custom Align" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show bottom right outside window">
                                <AjaxEvents>
                                    <Click OnEvent="Option5_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show bottom right inside window">
                                <AjaxEvents>
                                    <Click OnEvent="Option6_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show above the top edge of the window">
                                <AjaxEvents>
                                    <Click OnEvent="Option7_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show below the bottom edge of the window">
                                <AjaxEvents>
                                    <Click OnEvent="Option8_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem ID="MenuItem1" runat="server" Text="Show to the right">
                                <AjaxEvents>
                                    <Click OnEvent="Option9_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="ShowMode" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:TextMenuItem runat="server" Text="Click several times" Cls="menu-label" />
                            <ext:MenuItem runat="server" Text="Show bottom right">
                                <AjaxEvents>
                                    <Click OnEvent="Option10_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>                                
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="Hide Functionality" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show without auto hiding">
                                <AjaxEvents>
                                    <Click OnEvent="Option11_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>    
                            
                            <ext:MenuItem runat="server" Text="Show with 2 sec delay hidding">
                                <AjaxEvents>
                                    <Click OnEvent="Option12_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>                                                            
                            
                            <ext:MenuItem runat="server" Text="Show with mouse over stop hiding event">
                                <AjaxEvents>
                                    <Click OnEvent="Option13_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>   
                            
                            <ext:MenuItem runat="server" Text="Show with close all visible notifications">
                                <AjaxEvents>
                                    <Click OnEvent="Option14_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>   
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="Content Functionality" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show with AutoLoad">
                                <AjaxEvents>
                                    <Click OnEvent="Option15_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show with content element">
                                <AjaxEvents>
                                    <Click OnEvent="Option16_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="Tools" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show pin tool button">
                                <AjaxEvents>
                                    <Click OnEvent="Option17_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show pinned pin tool button">
                                <AjaxEvents>
                                    <Click OnEvent="Option18_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                            
                            <ext:MenuItem runat="server" Text="Show custom tool button">
                                <AjaxEvents>
                                    <Click OnEvent="Option19_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                        </Items>
                    </Menu>
                </ext:MenuPanel>
                
                <ext:MenuPanel runat="server" Title="Other" SaveSelection="false">
                    <Menu>
                        <Items>
                            <ext:MenuItem runat="server" Text="Show without title">
                                <AjaxEvents>
                                    <Click OnEvent="Option20_Click" />
                                </AjaxEvents>
                            </ext:MenuItem>
                        </Items>
                    </Menu>
                </ext:MenuPanel>
            </ext:Accordion>
        </Body>
        <Plugins>
            <ext:KeepActive runat="server" />
        </Plugins>
    </ext:Window>
    
    <div id="customEl" class="x-hidden">
        <ext:Panel ID="CustomEl1" runat="server" Border="false" BodyStyle="padding:2px;" AutoDataBind="true" Height="113">
            <Body>
                <%# stub %>
            </Body>
            <BottomBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:ToolbarTextItem ID="BarLabel" runat="server" />
                        <ext:ToolbarFill runat="server" />
                        <ext:ToolbarButton runat="server" Icon="Add" />
                        <ext:ToolbarButton runat="server" Icon="Email" />
                    </Items>
                </ext:Toolbar>
            </BottomBar>
        </ext:Panel>
    </div>
</body>
</html>