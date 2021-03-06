﻿<%@ Page Language="C#" %>
<%@ Register assembly="Coolite.Ext.Web" namespace="Coolite.Ext.Web" tagprefix="ext" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ColorPalate Overview - Coolite Toolkit Examples</title>
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] customColorsBlue = new string[256];
            string[] customColorsGreen = new string[256];
            string[] customColorsRed = new string[256];

            for (int i = 0; i < 256; i++)
            {
                customColorsBlue[i] = i.ToString("X6");
                customColorsGreen[i] = (i << 8).ToString("X6");
                customColorsRed[i] = (i << 16).ToString("X6");
            }

            this.ColorPalette1.Colors = customColorsBlue;
            this.ColorPalette2.Colors = customColorsGreen;
            this.ColorPalette3.Colors = customColorsRed;
            
            if(this.IsPostBack && !Ext.IsAjaxRequest)
            {
                ColorPalette1.Value = "";
                ColorPalette2.Value = "";
            }
        }

        protected void ColorPalette2_Select(object sender, AjaxEventArgs e)
        {
            this.ColorPalette2.AddScript(string.Format("setColor({0},\"{1}\");", this.ColorPalette2.ClientID, this.ColorPalette2.Value));
        }

        protected void ColorPalette3_Changed(object sender, EventArgs e)
        {
            this.ColorPalette3.AddScript(string.Format("setColor({0},\"{1}\");", this.ColorPalette3.ClientID, this.ColorPalette3.Value));
        }
    </script>
    
    <style type="text/css">
        body {
        	padding:20px;
        }
        
        .column {
        	 float: left;
        }
        
        .palette {
            color: #fff; 
            text-align: center;
            width: 138px;
            height: 45px;
            margin: 0px 9px 0px 3px;
        }
        
        #swatch {
        	margin-top: 2px;
        	height: 570px;
        	border: 1px solid black;
        }
    </style>
    
    <script type="text/javascript">
        var setColor = function (el, color) {
            if (Ext.isEmpty(color, false)) {
                return;
            }
            var div = Ext.get('swatch');
            div.setStyle('background-color', '#' + color);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager runat="server" />
        
        <h1>Custom Colors using ColorPalette</h1>
        
        <div class="column">
            <div class="palette" style="background-color:Blue;">Blue Palette<br />(Listener)</div>
            <ext:ColorPalette ID="ColorPalette1" runat="server">
                <Listeners>
                    <Select Fn="setColor" />
                </Listeners>
            </ext:ColorPalette>
        </div>
        
        <div class="column">
            <div class="palette" style="background-color:Green;">Green Palette<br />(AjaxEvent)</div>
            <ext:ColorPalette ID="ColorPalette2" runat="server">
                <AjaxEvents>
                    <Select OnEvent="ColorPalette2_Select">
                        <EventMask ShowMask="true" MinDelay="500" Msg="Updating Color Swatch..." />
                    </Select>
                </AjaxEvents>
            </ext:ColorPalette>
        </div>
        
        <div class="column">
            <div class="palette" style="background-color:Red;">Red Palette<br />(PostBack)</div>
            <ext:ColorPalette 
                ID="ColorPalette3" 
                runat="server" 
                OnColorChanged="ColorPalette3_Changed"
                AutoPostBack="true"
                />
        </div>
        
        <div class="column">
            <div class="palette" style="background-color:black;width:80px;margin:0;">Swatch</div>
            <div id="swatch" style="width:78px;"></div>
        </div>
    </form>
</body>
</html>
