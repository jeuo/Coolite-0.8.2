﻿<%@ Page Language="C#" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Coolite Toolkit Example - Slider</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        #custom-slider {
            width:214px;
            padding-top:6px;
            background-image: url(resources/images/ticks.gif);
        }
        #custom-slider .x-slider-thumb {
            background-image: url(resources/images/slider-thumb.png);
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <h3>Basic Slider</h3>
        <ext:Slider runat="server" ID="Slider1" Width="214" />        
        <br/>
        
        <h3>Snapping Slider</h3>
        <ext:Slider runat="server" ID="Slider2" Width="214" Value="50" Increment="10" />
        <br/>
        
        <h3>Vertical Slider</h3>
        <ext:Slider runat="server" ID="Slider3" Height="214" Vertical="true"  />        
        <br/>
        
        <h3>Slider with tip</h3>
        <ext:Slider runat="server" ID="Slider4" Width="214">
            <Plugins>
                <ext:SliderTip runat="server" />
            </Plugins>
        </ext:Slider>        
        <br/>
        
        <h3>Slider with custom tip</h3>
        <ext:Slider runat="server" ID="Slider5" Width="214">
            <Plugins>
                <ext:SliderTip runat="server">
                    <GetText Fn="function(slider){return String.format('<b>{0}% complete</b>', slider.getValue());}" />
                </ext:SliderTip>
            </Plugins>
        </ext:Slider>        
        <br/>
        
        <h3>CSS Customized Slider</h3>
        <div id="custom-slider">
            <ext:Slider runat="server" ID="Slider6" Width="214" Increment="10">
                <Plugins>
                    <ext:SliderTip ID="SliderTip1" runat="server" />
                </Plugins>
            </ext:Slider>        
        </div>
    </form>
</body>
</html>
