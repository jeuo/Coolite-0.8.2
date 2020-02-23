/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;

namespace Coolite.Ext.Web
{
    public class MessageBox : ScriptClass
    {
        private MessageBox() { }

        public static MessageBox Instance
        {
            get
            {
                return (HttpContext.Current.Items["Ext.Msg"] ?? (HttpContext.Current.Items["Ext.Msg"] = new MessageBox())) as MessageBox;
            }
        }


        /*  Lifecycle
            -----------------------------------------------------------------------------------------------*/

        public override string Serialize()
        {
            return this.currentConfig != null ? this.Build(string.Concat("Ext.Msg.show(", this.currentConfig.Serialize(), ");")) : "";
        }


        /*  Configure
            -----------------------------------------------------------------------------------------------*/

        private Config currentConfig;

        public virtual MessageBox Configure(Config config)
        {
            this.currentConfig = config;
            return this;
        }


        /*  Show
            -----------------------------------------------------------------------------------------------*/

        public virtual MessageBox Show()
        {
            this.Render();
            return this;
        }

        public virtual MessageBox Show(Config config)
        {
            return this.Configure(config).Show();
        }

        
        /*  Alert
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg)
        {
            return this.Alert(title, msg, "", "");
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, JFunction fn)
        {
            return this.Alert(title, msg, fn, "");
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, string handler)
        {
            return this.Alert(title, msg, handler, "");
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, string handler, string scope)
        {
            return this.Alert(title, msg, !string.IsNullOrEmpty(handler) ? new JFunction(handler, "buttonId", "text") : null as JFunction, scope);
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, JFunction fn, string scope)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OK;
            config.Fn = fn;
            config.Scope = scope;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, ButtonsConfig buttonsConfig)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OK;
            config.ButtonsConfig = buttonsConfig;

            return this.Configure(config);       
        }

        /// <summary>
        /// Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript alert prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Alert(string title, string msg, ButtonsConfig buttonsConfig, string scope)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OK;
            config.ButtonsConfig = buttonsConfig;
            config.Scope = scope;

            return this.Configure(config);
        }


        /*  Confirm
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript confirm prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Confirm(string title, string msg)
        {
            return this.Confirm(title, msg, "", "");
        }

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript confirm prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Confirm(string title, string msg, JFunction fn)
        {
            return this.Confirm(title, msg, fn, "");
        }

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript confirm prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Confirm(string title, string msg, string handler)
        {
            return this.Confirm(title, msg, handler, "");
        }

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript confirm prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Confirm(string title, string msg, string handler, string scope)
        {
            return this.Confirm(title, msg, !string.IsNullOrEmpty(handler) ? new JFunction(handler, "buttonId", "text") : null as JFunction, scope);
        }

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a standard read-only message box with an OK button (comparable to the basic JavaScript confirm prompt). If a callback function is passed it will be called after the user clicks the button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).")]
        public virtual MessageBox Confirm(string title, string msg, JFunction fn, string scope)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.YESNO;
            config.Fn = fn;
            config.Scope = scope;
            config.Icon = Icon.QUESTION;

            return this.Configure(config);
        }
        
        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        public virtual MessageBox Confirm(string title, string msg, ButtonsConfig buttonsConfig)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.YESNO;
            config.ButtonsConfig = buttonsConfig;
            config.Icon = Icon.QUESTION;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a confirmation message box with Yes and No buttons (comparable to JavaScript's confirm). If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked will be passed as the only parameter to the callback (could also be the top-right close button).
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        public virtual MessageBox Confirm(string title, string msg, ButtonsConfig buttonsConfig, string scope)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.YESNO;
            config.ButtonsConfig = buttonsConfig;
            config.Scope = scope;
            config.Icon = Icon.QUESTION;

            return this.Configure(config);
        }

        
        /*  Progress
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Displays a message box with a progress bar. This message box has no buttons and is not closeable by the user. You are responsible for updating the progress bar as needed via Ext.MessageBox.updateProgress and closing the message box when the process is complete.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        [Description("Displays a message box with a progress bar. This message box has no buttons and is not closeable by the user. You are responsible for updating the progress bar as needed via Ext.MessageBox.updateProgress and closing the message box when the process is complete.")]
        public virtual MessageBox Progress(string title, string msg)
        {
            return this.Progress(title, msg, "");
        }

        /// <summary>
        /// Displays a message box with a progress bar. This message box has no buttons and is not closeable by the user. You are responsible for updating the progress bar as needed via Ext.MessageBox.updateProgress and closing the message box when the process is complete.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="progressText">(optional) The text to display inside the progress bar (defaults to '')</param>
        [Description("Displays a message box with a progress bar. This message box has no buttons and is not closeable by the user. You are responsible for updating the progress bar as needed via Ext.MessageBox.updateProgress and closing the message box when the process is complete.")]
        public virtual MessageBox Progress(string title, string msg, string progressText)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Progress = true;
            config.Closable = true;
            config.ProgressText = progressText;

            return this.Configure(config);
        }


        /*  Prompt
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        public virtual MessageBox Prompt(string title, string msg)
        {
            return this.Prompt(title, msg, "");
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        public virtual MessageBox Prompt(string title, string msg, JFunction fn)
        {
            return this.Prompt(title, msg, fn, "");
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        public virtual MessageBox Prompt(string title, string msg, string handler)
        {
            return this.Prompt(title, msg, handler, "");
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="handler">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        [Description("Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.")]
        public virtual MessageBox Prompt(string title, string msg, string handler, string scope)
        {
            return this.Prompt(title, msg, !string.IsNullOrEmpty(handler) ? new JFunction(handler, "buttonId", "text") : null as JFunction, scope);
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        public virtual MessageBox Prompt(string title, string msg, JFunction fn, string scope)
        {
            return this.Prompt(title, msg, fn, scope, false, "");
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        /// <param name="multiline">(optional) True to create a multiline textbox using the defaultTextHeight property, or the height in pixels to create the textbox (defaults to false / single-line)</param>
        /// <param name="value">(optional) Default value of the text input element (defaults to '')</param>
        public virtual MessageBox Prompt(string title, string msg, JFunction fn, string scope, bool multiline, string value)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OKCANCEL;
            config.Fn = fn;
            config.MinWidth = Unit.Pixel(250);
            config.Scope = scope;
            config.Prompt = true;
            config.Multiline = multiline;
            config.Value = value;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="fn">(optional) The callback function invoked after the message box is closed</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        /// <param name="multiline">(optional) True to create a multiline textbox using the defaultTextHeight property, or the height in pixels to create the textbox (defaults to false / single-line)</param>
        /// <param name="value">(optional) Default value of the text input element (defaults to '')</param>
        public virtual MessageBox Prompt(string title, string msg, JFunction fn, string scope, Unit multiline, string value)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OKCANCEL;
            config.Fn = fn;
            config.MinWidth = Unit.Pixel(250);
            config.Scope = scope;
            config.Prompt = true;
            config.DefaultTextHeight = multiline;
            config.Value = value;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        public virtual MessageBox Prompt(string title, string msg, ButtonsConfig buttonsConfig)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OKCANCEL;
            config.ButtonsConfig = buttonsConfig;
            config.MinWidth = Unit.Pixel(250);
            config.Prompt = true;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        /// <param name="multiline">(optional) True to create a multiline textbox using the defaultTextHeight property, or the height in pixels to create the textbox (defaults to false / single-line)</param>
        /// <param name="value">(optional) Default value of the text input element (defaults to '')</param>
        public virtual MessageBox Prompt(string title, string msg, ButtonsConfig buttonsConfig, string scope, bool multiline, string value)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OKCANCEL;
            config.ButtonsConfig = buttonsConfig;
            config.MinWidth = Unit.Pixel(250);
            config.Scope = scope;
            config.Prompt = true;
            config.Multiline = multiline;
            config.Value = value;

            return this.Configure(config);
        }

        /// <summary>
        /// Displays a message box with OK and Cancel buttons prompting the user to enter some text (comparable to JavaScript's prompt). The prompt can be a single-line or multi-line textbox. If a callback function is passed it will be called after the user clicks either button, and the id of the button that was clicked (could also be the top-right close button) and the text that was entered will be passed as the two parameters to the callback.
        /// </summary>
        /// <param name="title">The title bar text</param>
        /// <param name="msg">The message box body text</param>
        /// <param name="buttonsConfig">A ButtonsConfig object for configuring the Text value and JavaScript Handler for each MessageBox Button.</param>
        /// <param name="scope">(optional) The scope of the callback function</param>
        /// <param name="multiline">(optional) True to create a multiline textbox using the defaultTextHeight property, or the height in pixels to create the textbox (defaults to false / single-line)</param>
        /// <param name="value">(optional) Default value of the text input element (defaults to '')</param>
        public virtual MessageBox Prompt(string title, string msg, ButtonsConfig buttonsConfig, string scope, Unit multiline, string value)
        {
            Config config = new Config();
            config.Title = title;
            config.Message = msg;
            config.Buttons = Button.OKCANCEL;
            config.ButtonsConfig = buttonsConfig;
            config.MinWidth = Unit.Pixel(250);
            config.Scope = scope;
            config.Prompt = true;
            config.DefaultTextHeight = multiline;
            config.Value = value;

            return this.Configure(config);
        }


        /*  Wait
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.
        /// </summary>
        /// <param name="msg">The message box body text</param>
        [Description("Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.")]
        public virtual MessageBox Wait(string msg)
        {
            return this.Wait(msg, "");
        }

        /// <summary>
        /// Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.
        /// </summary>
        /// <param name="msg">The message box body text</param>
        /// <param name="title">(optional) The title bar text</param>
        [Description("Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.")]
        public virtual MessageBox Wait(string msg, string title)
        {
            return this.Wait(msg, title, null as ProgressBar.WaitConfig);
        }

        /// <summary>
        /// Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.
        /// </summary>
        /// <param name="msg">The message box body text</param>
        /// <param name="title">(optional) The title bar text</param>
        /// <param name="config">(optional) A Ext.ProgressBar.waitConfig object</param>
        [Description("Displays a message box with an infinitely auto-updating progress bar. This can be used to block user interaction while waiting for a long-running process to complete that does not have defined intervals. You are responsible for closing the message box when the process is complete.")]
        public virtual MessageBox Wait(string msg, string title, ProgressBar.WaitConfig config)
        {
            Config config2 = new Config();
            config2.Title = title;
            config2.Message = msg;
            config2.Closable = false;
            config2.Wait = true;
            config2.Modal = true;
            config2.WaitConfig = config;

            return this.Configure(config2);
        }


        /*  Hide
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Hides the message box if it is displayed
        /// </summary>
        [Description("Hides the message box if it is displayed")]
        public virtual MessageBox Hide()
        {
            this.AddScript("Ext.Msg.hide();");
            return this;
        }

        
        /*  SetIcon
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Adds the specified icon to the dialog. By default, the class 'ext-mb-icon' is applied for default styling, and the class passed in is expected to supply the background image url. Pass in empty string ('') to clear any existing icon.
        /// </summary>
        /// <param name="icon">A CSS classname specifying the icon's background image url, or empty string to clear the icon</param>
        [Description("Adds the specified icon to the dialog. By default, the class 'ext-mb-icon' is applied for default styling, and the class passed in is expected to supply the background image url. Pass in empty string ('') to clear any existing icon.")]
        public virtual MessageBox SetIcon(string icon)
        {
            this.AddScript(this.Build("Ext.Msg.setIcon({0});", icon));
            return this;
        }


        /*  SetIcon
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Adds the specified icon to the dialog. By default, the class 'ext-mb-icon' is applied for default styling, and the class passed in is expected to supply the background image url. Pass in empty string ('') to clear any existing icon.
        /// </summary>
        /// <param name="icon">A CSS classname specifying the icon's background image url, or empty string to clear the icon</param>
        [Description("Adds the specified icon to the dialog. By default, the class 'ext-mb-icon' is applied for default styling, and the class passed in is expected to supply the background image url. Pass in empty string ('') to clear any existing icon.")]
        public virtual MessageBox SetIcon(Icon icon)
        {
            this.AddScript(this.Build(string.Format("Ext.Msg.setIcon(Ext.MessageBox.{0});", icon.ToString())));
            return this;
        }


        /*  UpdateProgress
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Updates a progress-style message box's text and progress bar. Only relevant on message boxes initiated via Ext.MessageBox.progress or Ext.MessageBox.wait, or by calling Ext.MessageBox.show with progress: true.
        /// </summary>
        /// <param name="value">Any number between 0 and 1 (e.g., .5, defaults to 0)</param>
        /// <param name="progressText">The progress text to display inside the progress bar (defaults to '')</param>
        /// <param name="msg">The message box's body text is replaced with the specified string (defaults to undefined so that any existing body text will not get overwritten by default unless a new value is passed in)</param>
        [Description("Updates a progress-style message box's text and progress bar. Only relevant on message boxes initiated via Ext.MessageBox.progress or Ext.MessageBox.wait, or by calling Ext.MessageBox.show with progress: true.")]
        public virtual MessageBox UpdateProgress(float value, string progressText, string msg)
        {
            this.AddScript(this.Build("Ext.Msg.updateProgress({0},{1},{2});", JSON.Serialize(value), progressText, msg));
            return this;
        }

        /// <summary>
        /// Updates a progress-style message box's text and progress bar. Only relevant on message boxes initiated via Ext.MessageBox.progress or Ext.MessageBox.wait, or by calling Ext.MessageBox.show with progress: true.
        /// </summary>
        /// <param name="value">Any number between 0 and 1 (e.g., .5, defaults to 0)</param>
        /// <param name="progressText">The progress text to display inside the progress bar (defaults to '')</param>
        [Description("Updates a progress-style message box's text and progress bar. Only relevant on message boxes initiated via Ext.MessageBox.progress or Ext.MessageBox.wait, or by calling Ext.MessageBox.show with progress: true.")]
        public virtual MessageBox UpdateProgress(float value, string progressText)
        {
            this.AddScript(this.Build("Ext.Msg.updateProgress({0},{1});", JSON.Serialize(value), progressText));
            return this;
        }


        /*  UpdateText
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Updates the message box body text
        /// </summary>
        [Description("Updates the message box body text")]
        public virtual MessageBox UpdateText()
        {
            this.AddScript("Ext.Msg.updateText();");
            return this;
        }

        /// <summary>
        /// Updates the message box body text
        /// </summary>
        /// <param name="text">(optional) Replaces the message box element's innerHTML with the specified string (defaults to the XHTML-compliant non-breaking space character '&#160;')</param>
        [Description("Updates the message box body text")]
        public virtual MessageBox UpdateText(string text)
        {
            this.AddScript(this.Build("Ext.Msg.updateText({0});", text));
            return this;
        }


        /*  Public Properties
            -----------------------------------------------------------------------------------------------*/

        private ButtonTextConfig buttonText;

        public virtual ButtonTextConfig ButtonText
        {
            get
            {
                if (this.buttonText == null)
                {
                    this.buttonText = new ButtonTextConfig(this);
                }

                return this.buttonText;
            }
        }

        private Unit defaultTextHeight = Unit.Pixel(75);

        [DefaultValue(typeof(Unit), "75")]
        [Description("The default height in pixels of the message box's multiline textarea if displayed (defaults to 75)")]
        public virtual Unit DefaultTextHeight
        {
            get
            {
                return this.defaultTextHeight;
            }
            set
            {
                this.defaultTextHeight = value;
                this.AddScript(this.Build("Ext.Msg.defaultTextHeight={0};", value.Value));
            }
        }

        private Unit maxWidth = Unit.Pixel(600);

        [DefaultValue(typeof(Unit), "600")]
        [Description("The maximum width in pixels of the message box (defaults to 600)")]
        public virtual Unit MaxWidth
        {
            get
            {
                return this.maxWidth;
            }
            set
            {
                this.maxWidth = value;
                this.AddScript(this.Build("Ext.Msg.maxWidth={0};", value.Value));
            }
        }

        private Unit minProgressWidth = Unit.Pixel(250);

        [DefaultValue(typeof(Unit), "250")]
        [Description("The minimum width in pixels of the message box if it is a progress-style dialog. This is useful for setting a different minimum width than text-only dialogs may need (defaults to 250)")]
        public virtual Unit MinProgressWidth
        {
            get
            {
                return this.minProgressWidth;
            }
            set
            {
                this.minProgressWidth = value;
                this.AddScript(this.Build("Ext.Msg.minProgressWidth={0};", value.Value));
            }
        }

        private Unit minWidth = Unit.Pixel(100);

        [DefaultValue(typeof(Unit), "100")]
        [Description("The minimum width in pixels of the message box (defaults to 100)")]
        public virtual Unit MinWidth
        {
            get
            {
                return this.minWidth;
            }
            set
            {
                this.minWidth = value;
                this.AddScript(this.Build("Ext.Msg.minWidth={0};", value.Value));
            }
        }

        /*  Enums
            -----------------------------------------------------------------------------------------------*/

        public enum Button
        {
            NONE,
            CANCEL,
            OK,
            OKCANCEL,
            YESNO,
            YESNOCANCEL
        }

        public enum Icon
        {
            NONE,
            ERROR,
            INFO,
            QUESTION,
            WARNING
        }

        /*  Config
            -----------------------------------------------------------------------------------------------*/

        [ToolboxItem(false)]
        [Description("A config object containing any or all of the following properties. If this object is not specified the status will be cleared using the defaults.")]
        public class Config
        {
            public virtual string Serialize()
            {
                return new ClientConfig().Serialize(this);
            }
            
            string title = "";

            /// <summary>
            /// The title text
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("The title text")]
            public virtual string Title
            {
                get
                {
                    return this.title;
                }
                set
                {
                    this.title = value;
                }
            }

            string animEl = "";

            /// <summary>
            /// An id or Element from which the message box should animate as it opens and closes (defaults to undefined)
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("An id or Element from which the message box should animate as it opens and closes (defaults to undefined)")]
            public virtual string AnimEl
            {
                get
                {
                    return this.animEl;
                }
                set
                {
                    this.animEl = value;
                }
            }

            private MessageBox.Button buttons = MessageBox.Button.NONE;

            /// <summary>
            /// A buttons kind, or NONE to not show any buttons (defaults to NONE)
            /// </summary>
            [DefaultValue(MessageBox.Button.NONE)]
            [NotifyParentProperty(true)]
            [Description("A buttons kind, or NONE to not show any buttons (defaults to NONE)")]
            public virtual MessageBox.Button Buttons
            {
                get
                {
                    return this.buttons;
                }
                set
                {
                    this.buttons = value;
                }
            }

            private MessageBox.ButtonsConfig buttonsConfig = null;

            /// <summary>
            /// A buttons kind, or NONE to not show any buttons (defaults to NONE)
            /// </summary>
            [DefaultValue(null)]
            [NotifyParentProperty(true)]
            [Description("A buttons kind, or NONE to not show any buttons (defaults to NONE)")]
            public virtual MessageBox.ButtonsConfig ButtonsConfig
            {
                get
                {
                    return this.buttonsConfig;
                }
                set
                {
                    this.buttonsConfig = value;
                }
            }

            [ClientConfig("buttons", JsonMode.Raw)]
            [DefaultValue("")]
            internal string ButtonsProxy
            {
                get
                {
                    if (this.Buttons == MessageBox.Button.NONE)
                    {
                        return "false";
                    }

                    if (this.ButtonsConfig != null)
                    {
                        return this.ButtonsConfig.ToScript();
                    }

                    return string.Concat("Ext.Msg.", this.Buttons.ToString());
                }
            }

            private bool closable = true;
            /// <summary>
            /// False to hide the top-right close button (defaults to true). Note that progress and wait dialogs will ignore this property and always hide the close button as they can only be closed programmatically.
            /// </summary>
            [DefaultValue(true)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("False to hide the top-right close button (defaults to true). Note that progress and wait dialogs will ignore this property and always hide the close button as they can only be closed programmatically.")]
            public virtual bool Closable
            {
                get
                {
                    return this.closable;
                }
                set
                {
                    this.closable = value;
                }
            }

            string cls = "";

            /// <summary>
            /// A custom CSS class to apply to the message box's container element
            /// </summary>
            [ClientConfig]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("A custom CSS class to apply to the message box's container element")]
            public virtual string Cls
            {
                get
                {
                    return this.cls;
                }
                set
                {
                    this.cls = value;
                }
            }

            private Unit defaultTextHeight = Unit.Pixel(75);

            /// <summary>
            /// The default height in pixels of the message box's multiline textarea if displayed (defaults to 75)
            /// </summary>
            [ClientConfig]
            [DefaultValue(typeof(Unit), "75")]
            [Description("The default height in pixels of the message box's multiline textarea if displayed (defaults to 75)")]
            public virtual Unit DefaultTextHeight
            {
                get
                {
                    return this.defaultTextHeight;
                }
                set
                {
                    this.defaultTextHeight = value;
                }
            }

            string handler = "";

            /// <summary>
            /// A callback function which is called when the dialog is dismissed either by clicking on the configured buttons, or on the dialog close button, or by pressing the return button to enter input.
            /// Progress and wait dialogs will ignore this option since they do not respond to user actions and can only be closed programmatically, so any required function should be called by the same code after it closes the dialog. Parameters passed:
            ///     buttonId : String
            ///         The ID of the button pressed, one of:
            ///             ok
            ///             yes
            ///             no
            ///             cancel
            ///     text : String
            ///         Value of the input field if either prompt or multiline is true
            /// </summary>
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("A callback function which is called when the dialog is dismissed either by clicking on the configured buttons, or on the dialog close button, or by pressing the return button to enter input.")]
            public virtual string Handler
            {
                get
                {
                    return this.handler;
                }
                set
                {
                    this.handler = value;
                }
            }

            private JFunction fn = null;

            /// <summary>
            /// A callback function which is called when the dialog is dismissed either by clicking on the configured buttons, or on the dialog close button, or by pressing the return button to enter input.
            /// Progress and wait dialogs will ignore this option since they do not respond to user actions and can only be closed programmatically, so any required function should be called by the same code after it closes the dialog. Parameters passed:
            ///     buttonId : String
            ///         The ID of the button pressed, one of:
            ///             ok
            ///             yes
            ///             no
            ///             cancel
            ///     text : String
            ///         Value of the input field if either prompt or multiline is true
            /// </summary>
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("A callback function which is called when the dialog is dismissed either by clicking on the configured buttons, or on the dialog close button, or by pressing the return button to enter input.")]
            public JFunction Fn
            {
                get
                {
                    return this.fn;
                }
                set
                {
                    this.fn = value;
                }
            }

            [ClientConfig("fn", JsonMode.Raw)]
            [DefaultValue("")]
            internal string FnProxy
            {
                get
                {
                    if (this.ButtonsConfig != null)
                    {
                        this.Fn = this.ButtonsConfig.Fn;
                    }

                    if (!string.IsNullOrEmpty(this.Handler))
                    {
                        return new JFunction(handler, "buttonId", "text").ToString();
                    }

                    if (this.Fn == null || this.Fn.IsDefault)
                    {
                        return "";
                    }

                    return this.Fn.ToString();
                }
            }

            string scope = "";

            /// <summary>
            /// The scope of the callback function
            /// </summary>
            [ClientConfig(JsonMode.Raw)]
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("The scope of the callback function")]
            public virtual string Scope
            {
                get
                {
                    return this.scope;
                }
                set
                {
                    this.scope = value;
                }
            }

            private MessageBox.Icon icon = MessageBox.Icon.NONE;

            /// <summary>
            /// A CSS class that provides a background image to be used as the body icon for the dialog (e.g. Ext.MessageBox.WARNING or 'custom-class') (defaults to '')
            /// </summary>
            [DefaultValue(MessageBox.Icon.NONE)]
            [NotifyParentProperty(true)]
            [Description("A CSS class that provides a background image to be used as the body icon for the dialog (e.g. Ext.MessageBox.WARNING or 'custom-class') (defaults to '')")]
            public virtual MessageBox.Icon Icon
            {
                get
                {
                    return this.icon;
                }
                set
                {
                    this.icon = value;
                }
            }

            string iconCls = "";

            /// <summary>
            /// A CSS class that provides a background image
            /// </summary>
            [DefaultValue("")]
            [NotifyParentProperty(true)]
            [Description("A CSS class that provides a background image")]
            public virtual string IconCls
            {
                get
                {
                    return this.iconCls;
                }
                set
                {
                    this.iconCls = value;
                }
            }

            [ClientConfig("icon", JsonMode.Raw)]
            internal string IconProxy
            {
                get
                {
                    if (this.Icon != MessageBox.Icon.NONE)
                    {
                        return string.Concat("Ext.Msg.", this.Icon.ToString());
                    }

                    if (!string.IsNullOrEmpty(this.IconCls))
                    {
                        return JSON.Serialize(this.IconCls);
                    }

                    return "";
                }
            }

            private Coolite.Ext.Web.Icon headerIcon = Coolite.Ext.Web.Icon.None;

            /// <summary>
            /// The standard Ext.Window.iconCls to add an optional header icon (defaults to '')
            /// </summary>
            [Category("Config Options")]
            [DefaultValue(Coolite.Ext.Web.Icon.None)]
            [Description("The standard Ext.Window.iconCls to add an optional header icon (defaults to '')")]
            public virtual Coolite.Ext.Web.Icon HeaderIcon
            {
                get
                {
                    return this.headerIcon;
                }
                set
                {
                    this.headerIcon = value;
                }
            }

            private string headerIconCls = "";
            /// <summary>
            /// The standard Ext.Window.iconCls to add an optional header icon (defaults to '')
            /// </summary>
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("The standard Ext.Window.iconCls to add an optional header icon (defaults to '')")]
            [NotifyParentProperty(true)]
            public virtual string HeaderIconCls
            {
                get
                {
                    return this.headerIconCls;
                }
                set
                {
                    this.headerIconCls = value;
                }
            }

            [ClientConfig("iconCls")]
            [DefaultValue("")]
            internal virtual string HeaderIconClsProxy
            {
                get
                {
                    if (this.HeaderIcon != Coolite.Ext.Web.Icon.None)
                    {
                        if (!Ext.IsAjaxRequest && HttpContext.Current != null)
                        {
                            ScriptManager sm = ScriptManager.GetInstance(HttpContext.Current);
                            if (sm != null)
                            {
                                sm.RegisterIcon(this.HeaderIcon);
                            }
                        }
                        return ScriptManager.GetIconClassName(this.HeaderIcon);
                    }
                    return this.HeaderIconCls;
                }
            }

            private Unit maxWidth = Unit.Pixel(600);

            /// <summary>
            /// The maximum width in pixels of the message box (defaults to 600)
            /// </summary>
            [ClientConfig]
            [DefaultValue(typeof(Unit), "600")]
            [Description("The maximum width in pixels of the message box (defaults to 600)")]
            public virtual Unit MaxWidth
            {
                get
                {
                    return this.maxWidth;
                }
                set
                {
                    this.maxWidth = value;
                }
            }

            private Unit minWidth = Unit.Pixel(100);

            /// <summary>
            /// The minimum width in pixels of the message box (defaults to 100)
            /// </summary>
            [ClientConfig]
            [DefaultValue(typeof(Unit), "100")]
            [Description("The minimum width in pixels of the message box (defaults to 100)")]
            public virtual Unit MinWidth
            {
                get
                {
                    return this.minWidth;
                }
                set
                {
                    this.minWidth = value;
                }
            }

            private bool modal = true;
            /// <summary>
            /// False to allow user interaction with the page while the message box is displayed (defaults to true)
            /// </summary>
            [DefaultValue(true)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("False to allow user interaction with the page while the message box is displayed (defaults to true)")]
            public virtual bool Modal
            {
                get
                {
                    return this.modal;
                }
                set
                {
                    this.modal = value;
                }
            }

            private string msg = "";
            /// <summary>
            /// A string that will replace the existing message box body text (defaults to the XHTML-compliant non-breaking space character '&#160;')
            /// </summary>
            [ClientConfig("msg")]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("A string that will replace the existing message box body text (defaults to the XHTML-compliant non-breaking space character '&#160;')")]
            [NotifyParentProperty(true)]
            public virtual string Message
            {
                get
                {
                    return this.msg;
                }
                set
                {
                    this.msg = value;
                }
            }

            private bool multiline = false;

            /// <summary>
            /// True to prompt the user to enter multi-line text (defaults to false)
            /// </summary>
            [DefaultValue(false)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("True to prompt the user to enter multi-line text (defaults to false)")]
            public virtual bool Multiline
            {
                get
                {
                    return this.multiline;
                }
                set
                {
                    this.multiline = value;
                }
            }

            private bool progress = false;
            /// <summary>
            /// True to display a progress bar (defaults to false)
            /// </summary>
            [DefaultValue(false)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("True to display a progress bar (defaults to false)")]
            public virtual bool Progress
            {
                get
                {
                    return this.progress;
                }
                set
                {
                    this.progress = value;
                }
            }

            private string progressText = "";
            /// <summary>
            /// The text to display inside the progress bar if progress = true (defaults to '')
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("The text to display inside the progress bar if progress = true (defaults to '')")]
            [NotifyParentProperty(true)]
            public virtual string ProgressText
            {
                get
                {
                    return this.progressText;
                }
                set
                {
                    this.progressText = value;
                }
            }

            private bool prompt = false;
            /// <summary>
            /// True to prompt the user to enter single-line text (defaults to false)
            /// </summary>
            [DefaultValue(false)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("True to prompt the user to enter single-line text (defaults to false)")]
            public virtual bool Prompt
            {
                get
                {
                    return this.prompt;
                }
                set
                {
                    this.prompt = value;
                }
            }

            private bool proxyDrag = false;
            /// <summary>
            /// True to display a lightweight proxy while dragging (defaults to false)
            /// </summary>
            [DefaultValue(false)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("True to display a lightweight proxy while dragging (defaults to false)")]
            public virtual bool ProxyDrag
            {
                get
                {
                    return this.proxyDrag;
                }
                set
                {
                    this.proxyDrag = value;
                }
            }

            private string value = "";
            /// <summary>
            /// The string value to set into the active textbox element if displayed
            /// </summary>
            [ClientConfig]
            [Category("Config Options")]
            [DefaultValue("")]
            [Description("The string value to set into the active textbox element if displayed")]
            [NotifyParentProperty(true)]
            public virtual string Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;
                }
            }

            private bool wait = false;
            /// <summary>
            /// True to display a progress bar (defaults to false)
            /// </summary>
            [DefaultValue(false)]
            [ClientConfig]
            [NotifyParentProperty(true)]
            [Description("True to display a progress bar (defaults to false)")]
            public virtual bool Wait
            {
                get
                {
                    return this.wait;
                }
                set
                {
                    this.wait = value;
                }
            }

            private ProgressBar.WaitConfig waitConfig = new ProgressBar.WaitConfig();

            /// <summary>
            /// A WaitConfig object (applies only if Wait = true)
            /// </summary>
            [DefaultValue(null)]
            [NotifyParentProperty(true)]
            [Description("A Ext.ProgressBar.waitConfig object (applies only if wait = true)")]
            public ProgressBar.WaitConfig WaitConfig
            {
                get
                {
                    return this.waitConfig;
                }
                set
                {
                    this.waitConfig = value;
                }
            }

            [ClientConfig("waitConfig", JsonMode.Raw)]
            [DefaultValue("")]
            internal string WaitConfigProxy
            {
                get
                {
                    if (this.WaitConfig == null)
                    {
                        return "";
                    }

                    string cfg = this.WaitConfig.ToJsonString();
                    if (string.IsNullOrEmpty(cfg) || cfg.Equals("{}"))
                    {
                        return "";
                    }

                    return cfg;
                }
            }

            private Unit width = Unit.Pixel(0);

            /// <summary>
            /// The width of the dialog in pixels
            /// </summary>
            [ClientConfig]
            [DefaultValue(typeof(Unit), "0")]
            [Description("The width of the dialog in pixels")]
            public virtual Unit Width
            {
                get
                {
                    return this.width;
                }
                set
                {
                    this.width = value;
                }
            }
        }


        /*  ButtonTextConfig
            -----------------------------------------------------------------------------------------------*/

        public class ButtonTextConfig
        {
            private MessageBox mb;

            internal ButtonTextConfig(MessageBox mb)
            {
                this.mb = mb;
            }

            private string ok = "";

            [ClientConfig]
            [DefaultValue("")]
            public string Ok
            {
                get
                {
                    return this.ok;
                }
                set
                {
                    this.ok = value;
                    this.mb.AddScript(this.mb.Build("Ext.Msg.buttonText.ok={0};", this.ok));
                }
            }

            private string cancel = "";

            [ClientConfig]
            [DefaultValue("")]
            public string Cancel
            {
                get
                {
                    return this.cancel;
                }
                set
                {
                    this.cancel = value;
                    this.mb.AddScript(this.mb.Build("Ext.Msg.buttonText.cancel={0};", this.cancel));
                }
            }

            private string yes = "";

            [ClientConfig]
            [DefaultValue("")]
            public string Yes
            {
                get
                {
                    return this.yes;
                }
                set
                {
                    this.yes = value;
                    this.mb.AddScript(this.mb.Build("Ext.Msg.buttonText.yes={0};", this.yes));
                }
            }

            private string no = "";

            [ClientConfig]
            [DefaultValue("")]
            public string No
            {
                get
                {
                    return this.no;
                }
                set
                {
                    this.no = value;
                    this.mb.AddScript(this.mb.Build("Ext.Msg.buttonText.no={0};", this.no));
                }
            }
        }


        /*  ButtonsConfig
            -----------------------------------------------------------------------------------------------*/

        public class ButtonsConfig
        {
            public virtual string ToScript()
            {
                JsonObject config = new JsonObject();

                if (this.Ok != null)
                {
                    config.Add("ok", this.Ok.Text);
                }

                if (this.Cancel != null)
                {
                    config.Add("cancel", this.Cancel.Text);
                }

                if (this.Yes != null)
                {
                    config.Add("yes", this.Yes.Text);
                }

                if (this.No != null)
                {
                    config.Add("no", this.No.Text);
                }

                return config.Count > 0 ? config.ToJson() : "";
            }

            public virtual bool HasHandlers
            {
                get
                {
                    return (this.Ok != null && !string.IsNullOrEmpty(this.Ok.Handler)) ||
                        (this.Cancel != null && !string.IsNullOrEmpty(this.Cancel.Handler)) ||
                        (this.Yes != null && !string.IsNullOrEmpty(this.Yes.Handler)) ||
                        (this.No != null && !string.IsNullOrEmpty(this.No.Handler));
                }
            }

            public virtual JFunction Fn
            {
                get
                {
                    if (!this.HasHandlers)
                    {
                        return null;
                    }

                    StringBuilder handler = new StringBuilder();

                    handler.Append("switch(buttonId){");

                    if(this.Ok != null && !string.IsNullOrEmpty(this.Ok.Handler))
                    {
                        handler.Append("case 'ok':");
                        handler.Append(this.Ok.HandlerProxy);
                        handler.Append("break;");
                    }

                    if (this.Cancel != null && !string.IsNullOrEmpty(this.Cancel.Handler))
                    {
                        handler.Append("case 'cancel':");
                        handler.Append(this.Cancel.HandlerProxy);
                        handler.Append("break;");
                    }

                    if (this.Yes != null && !string.IsNullOrEmpty(this.Yes.Handler))
                    {
                        handler.Append("case 'yes':");
                        handler.Append(this.Yes.HandlerProxy);
                        handler.Append("break;");
                    }

                    if (this.No != null && !string.IsNullOrEmpty(this.No.Handler))
                    {
                        handler.Append("case 'no':");
                        handler.Append(this.No.HandlerProxy);
                        handler.Append("break;");
                    }

                    handler.Append("}");

                    return new JFunction(handler.ToString(), "buttonId", "text");
                }
            }

            private ButtonConfig ok = null;

            [DefaultValue(null)]
            public ButtonConfig Ok
            {
                get
                {
                    return this.ok;
                }
                set
                {
                    this.ok = value;
                }
            }

            private ButtonConfig cancel = null;

            [DefaultValue(null)]
            public ButtonConfig Cancel
            {
                get
                {
                    return this.cancel;
                }
                set
                {
                    this.cancel = value;
                }
            }

            private ButtonConfig yes = null;

            [DefaultValue(null)]
            public ButtonConfig Yes
            {
                get
                {
                    return this.yes;
                }
                set
                {
                    this.yes = value;
                }
            }

            private ButtonConfig no = null;

            [DefaultValue(null)]
            public ButtonConfig No
            {
                get
                {
                    return this.no;
                }
                set
                {
                    this.no = value;
                }
            }
        }


        /*  ButtonConfig
            -----------------------------------------------------------------------------------------------*/

        public class ButtonConfig
        {
            private string handler = "";

            [DefaultValue("")]
            public string Handler
            {
                get
                {
                    return this.handler;
                }
                set
                {
                    this.handler = value;
                }
            }

            internal string HandlerProxy
            {
                get
                {
                    string handler = this.Handler;

                    if (!string.IsNullOrEmpty(handler))
                    {
                        if (handler.EndsWith("}"))
                        {
                            return handler;
                        }

                        if (!handler.EndsWith(";"))
                        {
                            return string.Concat(handler, ";");
                        }
                    }

                    return handler;
                }
            }

            private string text = "";

            [DefaultValue("")]
            public string Text
            {
                get
                {
                    return this.text;
                }
                set
                {
                    this.text = value;
                }
            }
        }
    }
}