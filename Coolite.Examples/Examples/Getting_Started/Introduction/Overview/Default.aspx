<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Getting Started - Coolite Toolkit</title>
    <link href="../../../../resources/css/examples.css"  rel="stylesheet" type="text/css" />
    <base target="_blank" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>Welcome to the Coolite Toolkit Examples Explorer</h1>
        
        <h2>Overview</h2>
        
        <p>The Coolite Toolkit is a suite of AJAX enabled ASP.NET Web Controls.</p>
        <p>The Coolite Toolkit builds upon the powerful cross-browser <a href="http://www.extjs.com/">ExtJS</a> JavaScript Library and 
            simplifies the development of powerful and rich AJAX enabled Web Applications.</p>
        <p>The Coolite Toolkit and ExtJS JavaScript Library are licensed in both an Open-Source Community License or 
            Commercial License. The <a href="http://www.coolite.com/license/">License Summary</a> outlines differences between the two licensing options. 
            The code and functionality of both the Community and Professional editions are essentially the same and the only effective difference is the License under which each is released.</p>
        <p>Direct access to the latest Coolite Toolkit source code, via read-only SVN access, is available to all Professional License holders with a valid Support Subscription. More information regarding
            Professional Licenses and Support Subscriptions are available on the <a href="http://www.coolite.com/products" />Product Summary</a>.</p>
            
        <h2>System Requirements</h2>
        
        <ol class="expanded">
            <li><a href="http://www.microsoft.com/visualstudio/">Visual Studio</a> 2005 or 2008, or</li>
            <li><a href="http://www.microsoft.com/express/vwd/">Visual Web Developer Express</a> 2005 or 2008</li>
            <li>.NET Framework 3.5 (only required to be installed on machine), you can code in .NET 2.0, 3.0 or 3.5</li>
        </ol>
        
        <h2>Getting Started (Step-by-Step)</h2>
        
        <ol class="expanded">
            <li>First ensure you have Visual Studio or Visual Web Developer Express installed on your computer.
                <div class="information"><p>If you do not have a copy of Visual Studio already installed, the <a href="http://www.microsoft.com/express/vwd/">Visual Web Developer Express Edition</a> is free to use and 
                is a great way to get started with ASP.NET and the Coolite Toolkit. The Coolite Toolkit works exactly the same in both environments.</p></div></li>
            <li><a href="http://www.coolite.com/download/">Download</a> and run the Coolite Toolkit automatic installer (.exe). After downloading the .exe, if installing on Vista, you might have to right-click
                on the .exe and select "Run as Administrator...".
                <p>A Manual installation package is also available. Both the Automatic and Manual installation downloads contain the exact same files. 
                The Automatic installer (.exe) only conveniently copies the required files to your computers file system and adds a "Coolite" Menu Item to your Windows "Start" Menu.</p></li>
            <li>Create your first "Web Site" Project.
                <ol style="list-style-type: lower-roman;">
                    <li>Open Visual Studio (or Visual Web Developer) and create a new "Web Site" project. From the File Menu, select New > Web Site.</li>
                    <li>The "New Web Site" dialog will open, ensure "ASP.NET Web Site" is selected from the list of Templates.</li>
                    <li>For your first project, the "Location" option of "File System" and default file path should be fine, or modify to fit your preference.</li>
                    <li>Please select your "Language" preference. Whether you choose "Visual C#" or "Visual Basic" is ultimately just dependent on personal coding preferences. 
                        The Coolite Toolkit is written in C#, but can be used in any .NET language, including Visual Basic or C#.</li>
                    <li>Click "OK".</li>
                </ol>
            </li>
            <li>Add the Coolite Toolkit Controls to your Visual Studio (or Visual Web Developer) Toolbox, see also <a href="http://coolite.com/support/readme.txt">README.txt</a>
                <ol style="list-style-type: lower-roman;">
                    <li>Open Visual Studio or Visual Web Developer Express</li>
                    <li>Open an existing web site or create a new web site project.</li>
                    <li>Open or create a new .aspx page.</li>
                    <li>Open the ToolBox panel, typically located on the left side in a fly-out panel (Ctrl + Alt + x).</li>
                    <li>Create a new "Coolite Toolkit" Tab:
                        <ol style="list-style-type: lower-alpha;">
                            <li>Right-Click in the ToolBox area</li>
                            <li>Select "Add Tab"</li>
                            <li>Enter "Coolite Toolkit"</li>
                        </ol>
                    </li>
		            <li>Inside the "Coolite Toolkit" tab, Right-Click and select "Choose Items...".</li>
		            <li>Under the ".NET Framework Components" Tab select the "Browse" button.</li>
		            <li>Navigate to and select the Coolite.Ext.Web.dll file, choose open.
		                <div class="information">
		                    <p>If the automatic installer has been run previously, the Coolite.Ext.Web.dll can typically be found in the following location:</p>
		                    <p>C:\Program Files\Coolite\Coolite Toolkit - Version 0.8.0\</p>
		                </div>
		            </li>
                    <li>The Coolite Toolkit controls should now be added to the list and pre-checked. You can confirm by sorting the list by "Namespace" and scrolling to "Coolite.Ext.Web"</li>
                    <li>Click "OK". The icons should be added to your ToolBox. You should now be able to drag/drop a Coolite component onto your .aspx Page.</li>
                </ol>
            </li>
            <li>Create your first web page.
                <ol style="list-style-type: lower-roman;">
                    <li>Open a .aspx Page</li>
                    <li>Drag the Coolite "ScriptManager" control onto your Page. One &lt;ext:ScriptManager> is required on each .aspx Page</li>
                    <li>Drag a Coolite "Window" control onto your Page, then Save (Ctrl + s) your Page.</li>
                    <li>Hit the "F5" key, start debugging, or Right-Click on the Page and select "View in Browser". Your Page should now render in the browser and the &lt;ext:Window> will be displayed.</li>
                    <li>Enjoy.</li>
                </ol>
            </li>
        </ol>
    </form>
  </body>
</html>
    