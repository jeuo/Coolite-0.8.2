/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    [Xtype("window")]
    [InstanceOf(ClassName = "Ext.Window")]
    [ContainerStyle("display:none;")]
    public abstract class WindowBase : ContentPanel
    {
        [ReadOnly(true)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoWidth
        {
            get 
            { 
                return base.AutoWidth; 
            }
            set 
            { 
                base.AutoWidth = value; 
            }
        }

        /// <summary>
        /// Id or element from which the window should animate while opening (defaults to null with no animation).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetAnimateTarget")]
        [Category("Config Options")]
        [DefaultValue("")]
        [TypeConverter(typeof(ControlConverter))]
        [Description("Id or element from which the window should animate while opening (defaults to null with no animation).")]
        public virtual string AnimateTarget
        {
            get
            {
                return (string)this.ViewState["AnimateTarget"] ?? "";
            }
            set
            {
                this.ViewState["AnimateTarget"] = value;
            }
        }

        /// <summary>
        /// True to display the 'close' tool button and allow the user to close the window, false to hide the button and disallow closing the window (default to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to display the 'close' tool button and allow the user to close the window, false to hide the button and disallow closing the window (default to true).")]
        public virtual bool Closable
        {
            get
            {
                object obj = this.ViewState["Closable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Closable"] = value;
            }
        }

        /// <summary>
        /// The action to take when the close button is clicked. The default action is 'close' which will actually remove the window from the DOM and destroy it. The other valid option is 'hide' which will simply hide the window by setting visibility to hidden and applying negative offsets, keeping the window available to be redisplayed via the show method.
        /// </summary>
        [ClientConfig(JsonMode.ToLower)]
        [Category("Config Options")]
        [DefaultValue(CloseAction.Close)]
        [Description("The action to take when the close button is clicked. The default action is 'close' which will actually remove the window from the DOM and destroy it. The other valid option is 'hide' which will simply hide the window by setting visibility to hidden and applying negative offsets, keeping the window available to be redisplayed via the show method.")]
        public virtual CloseAction CloseAction
        {
            get
            {
                object obj = this.ViewState["CloseAction"];
                return (obj == null) ? CloseAction.Hide : (CloseAction)obj;
            }
            set
            {
                this.ViewState["CloseAction"] = value;
            }
        }

        /// <summary>
        /// True to constrain the window to the viewport, false to allow it to fall outside of the viewport (defaults to false). Optionally the header only can be constrained using ConstrainHeader.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to constrain the window to the viewport, false to allow it to fall outside of the viewport (defaults to false). Optionally the header only can be constrained using ConstrainHeader.")]
        public virtual bool Constrain
        {
            get
            {
                object obj = this.ViewState["Constrain"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Constrain"] = value;
            }
        }

        /// <summary>
        /// True to constrain the window header to the viewport, allowing the window body to fall outside of the viewport, false to allow the header to fall outside the viewport (defaults to false). Optionally the entire window can be constrained using constrain.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to constrain the window header to the viewport, allowing the window body to fall outside of the viewport, false to allow the header to fall outside the viewport (defaults to false). Optionally the entire window can be constrained using constrain.")]
        public virtual bool ConstrainHeader
        {
            get
            {
                object obj = this.ViewState["ConstrainHeader"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["ConstrainHeader"] = value;
            }
        }

        /// <summary>
        /// The id of a button to focus when this window received the focus.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("")]
        [Description("The id of a button to focus when this window received the focus.")]
        public virtual string DefaultButton
        {
            get
            {
                return (string)this.ViewState["DefaultButton"] ?? "";
            }
            set
            {
                this.ViewState["DefaultButton"] = value;
            }
        }

        /// <summary>
        /// True to allow the window to be dragged by the header bar, false to disable dragging (defaults to true). Note that by default the window will be centered in the viewport, so if dragging is disabled the window may need to be positioned programmatically after render (e.g., myWindow.setPosition(100, 100);).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to allow the window to be dragged by the header bar, false to disable dragging (defaults to true). Note that by default the window will be centered in the viewport, so if dragging is disabled the window may need to be positioned programmatically after render (e.g., myWindow.setPosition(100, 100);).")]
        public override bool Draggable
        {
            get
            {
                
                object obj = this.ViewState["Draggable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Draggable"] = value;
            }
        }

        /// <summary>
        /// True to always expand the window when it is displayed, false to keep it in its current state (which may be collapsed) when displayed (defaults to true).
        /// </summary>
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to always expand the window when it is displayed, false to keep it in its current state (which may be collapsed) when displayed (defaults to true).")]
        public virtual bool ExpandOnShow
        {
            get
            {
                object obj = this.ViewState["ExpandOnShow"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["ExpandOnShow"] = value;
            }
        }

        [ClientConfig("expandOnShow")]
        [DefaultValue(true)]
        internal bool ExpandOnShowProxy
        {
            get
            {
                return (this.Collapsed) ? false : this.ExpandOnShow;
            }
        }

        //[ClientConfig]
        //[Category("Config Options")]
        //[DefaultValue(null)]
        //[Description("A reference to the WindowGroup that should manage this window (defaults to Ext.WindowMgr).")]
        //public virtual ManagerGroup Manager
        //{
        //    get
        //    {
        //        object obj = this.ViewState["Manager"];
        //        return (obj == null) ? null : (ManagerGroup)obj;
        //    }
        //    set
        //    {
        //        this.ViewState["Manager"] = value;
        //    }
        //}

        /// <summary>
        /// True to display the 'maximize' tool button and allow the user to maximize the window, false to hide the button and disallow maximizing the window (defaults to false). Note that when a window is maximized, the tool button will automatically change to a 'restore' button with the appropriate behavior already built-in that will restore the window to its previous size.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display the 'maximize' tool button and allow the user to maximize the window, false to hide the button and disallow maximizing the window (defaults to false). Note that when a window is maximized, the tool button will automatically change to a 'restore' button with the appropriate behavior already built-in that will restore the window to its previous size.")]
        public virtual bool Maximizable
        {
            get
            {
                object obj = this.ViewState["Maximizable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Maximizable"] = value;
            }
        }

        /// <summary>
        /// The height of this component in pixels (defaults to auto).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetHeight")]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The height of this component in pixels (defaults to auto).")]
        public override Unit Height
        {
            get
            {
                Unit height = this.UnitPixelTypeCheck(ViewState["Height"], Unit.Empty, "Height");
                return (height.Value < this.MinHeight.Value) ? this.MinHeight : height;
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        /// <summary>
        /// The minimum height in pixels allowed for this window (defaults to 100).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "100")]
        [Description("The minimum height in pixels allowed for this window (defaults to 100).")]
        public virtual Unit MinHeight
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinHeight"], Unit.Pixel(100), "MinHeight");
            }
            set
            {
                this.ViewState["MinHeight"] = value;
            }
        }

        /// <summary>
        /// The width of this component in pixels (defaults to auto).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "SetWidth")]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The width of this component in pixels (defaults to auto).")]
        public override Unit Width
        {
            get
            {
                Unit width = this.UnitPixelTypeCheck(ViewState["Width"], Unit.Empty, "Width");
                return (width.Value < this.MinWidth.Value) ? this.MinWidth : width;
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        /// <summary>
        /// The minimum width in pixels allowed for this window (defaults to 200). Only applies when resizable = true.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "200")]
        [Description("The minimum width in pixels allowed for this window (defaults to 200). Only applies when resizable = true.")]
        public virtual Unit MinWidth
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["MinWidth"], Unit.Pixel(200), "MinWidth");
            }
            set
            {
                this.ViewState["MinWidth"] = value;
            }
        }

        /// <summary>
        /// True to display the 'minimize' tool button and allow the user to minimize the window, false to hide the button and disallow minimizing the window (defaults to false). Note that this button provides no implementation -- the behavior of minimizing a window is implementation-specific, so the minimize event must be handled and a custom minimize behavior implemented for this option to be useful.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to display the 'minimize' tool button and allow the user to minimize the window, false to hide the button and disallow minimizing the window (defaults to false). Note that this button provides no implementation -- the behavior of minimizing a window is implementation-specific, so the minimize event must be handled and a custom minimize behavior implemented for this option to be useful.")]
        public virtual bool Minimizable
        {
            get
            {
                object obj = this.ViewState["Minimizable"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Minimizable"] = value;
            }
        }

        /// <summary>
        /// True to make the window modal and mask everything behind it when displayed, false to display it without restricting access to other UI elements (defaults to false).
        /// </summary>
        [ClientConfig]
        [AjaxEventUpdate(MethodName = "ToggleModal")]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to make the window modal and mask everything behind it when displayed, false to display it without restricting access to other UI elements (defaults to false).")]
        public virtual bool Modal
        {
            get
            {
                object obj = this.ViewState["Modal"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Modal"] = value;
            }
        }

        /// <summary>
        /// Allows override of the built-in processing for the escape key. Default action is to close the Window (performing whatever action is specified in closeAction. To prevent the Window closing when the escape key is pressed, specify this as Ext.emptyFn (See Ext.emptyFn).
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue("Ext.emptyFn")]
        [Description("Allows override of the built-in processing for the escape key. Default action is to close the Window (performing whatever action is specified in closeAction. To prevent the Window closing when the escape key is pressed, specify this as Ext.emptyFn (See Ext.emptyFn).")]
        public virtual string OnEsc
        {
            get
            {
                object obj = this.ViewState["OnEsc"];
                return (obj == null) ? "Ext.emptyFn" : (string)obj;
            }
            set
            {
                this.ViewState["OnEsc"] = value;
            }
        }

        /// <summary>
        /// True to render the window body with a transparent background so that it will blend into the framing elements, false to add a lighter background color to visually highlight the body element and separate it more distinctly from the surrounding frame (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to render the window body with a transparent background so that it will blend into the framing elements, false to add a lighter background color to visually highlight the body element and separate it more distinctly from the surrounding frame (defaults to false).")]
        public virtual bool Plain
        {
            get
            {
                object obj = this.ViewState["Plain"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["Plain"] = value;
            }
        }

        /// <summary>
        /// True to allow user resizing at each edge and corner of the window, false to disable resizing (defaults to true).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(true)]
        [Description("True to allow user resizing at each edge and corner of the window, false to disable resizing (defaults to true).")]
        public virtual bool Resizable
        {
            get
            {
                object obj = this.ViewState["Resizable"];
                return (obj == null) ? true : (bool)obj;
            }
            set
            {
                this.ViewState["Resizable"] = value;
            }
        }

        /// <summary>
        /// A valid Ext.Resizable handles config string (defaults to 'all'). Only applies when resizable = true.
        /// 
        /// Value   Description
        /// ------  -------------------
        /// 'n'     north
        /// 's'     south
        /// 'e'     east
        /// 'w'     west
        /// 'nw'    northwest
        /// 'sw'    southwest
        /// 'se'    southeast
        /// 'ne'    northeast
        /// 'all'   all
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue("all")]
        [Description("A valid Ext.Resizable handles config string (defaults to 'all'). Only applies when resizable = true.")]
        public virtual string ResizeHandles
        {
            get
            {
                object obj = this.ViewState["ResizeHandles"];
                return (obj == null) ? "all" : (string)obj;
            }
            set
            {
                this.ViewState["ResizeHandles"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Aligns the window to the specified element
        /// </summary>
        [Description("Aligns the window to the specified element")]
        public virtual void AlignTo(string element, string position)
        {
            string template = "{0}.alignTo({1},\"{2}\");";
            this.AddScript(template, this.ClientID, element, position);
        }

        /// <summary>
        /// Aligns the window to the specified element
        /// </summary>
        [Description("Aligns the window to the specified element")]
        public virtual void AlignTo(string element, string position, int offsetX, int offsetY)
        {
            string template = "{0}.alignTo({1},\"{2}\",[{3},{4}]);";
            this.AddScript(template, this.ClientID, element, position, offsetX, offsetY);
        }

        /// <summary>
        /// Anchors this window to another element and realigns it when the window is resized or scrolled.
        /// </summary>
        [Description("Anchors this window to another element and realigns it when the window is resized or scrolled.")]
        public virtual void AnchorTo(string element, string position)
        {
            string template = "{0}.anchorTo({1},\"{2}\");";
            this.AddScript(template, this.ClientID, element, position);
        }

        /// <summary>
        /// Anchors this window to another element and realigns it when the window is resized or scrolled.
        /// </summary>
        [Description("Anchors this window to another element and realigns it when the window is resized or scrolled.")]
        public virtual void AnchorTo(string element, string position, int offsetX, int offsetY)
        {
            string template = "{0}.anchorTo({1},\"{2}\",[{3},{4}]);";
            this.AddScript(template, this.ClientID, element, position, offsetX, offsetY);
        }

        /// <summary>
        /// Anchors this window to another element and realigns it when the window is resized or scrolled.
        /// </summary>
        [Description("Anchors this window to another element and realigns it when the window is resized or scrolled.")]
        public virtual void AnchorTo(string element, string position, int offsetX, int offsetY, bool monitorScroll)
        {
            string template = "{0}.anchorTo({1},\"{2}\",[{3},{4}],{5});";
            this.AddScript(template, this.ClientID, element, position, offsetX, offsetY, monitorScroll.ToString().ToLower());
        }

        /// <summary>
        /// Centers this window in the viewport
        /// </summary>
        [Description("Centers this window in the viewport")]
        public virtual void Center()
        {
            string template = "{0}.center();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Closes the window, removes it from the DOM and destroys the window object. The beforeclose event is fired before the close happens and will cancel the close action if it returns false.
        /// </summary>
        [Description("Closes the window, removes it from the DOM and destroys the window object. The beforeclose event is fired before the close happens and will cancel the close action if it returns false.")]
        public virtual void Close()
        {
            string template = "{0}.close();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Focuses the window. If a defaultButton is set, it will receive focus, otherwise the window itself will receive focus.
        /// </summary>
        [Description("Focuses the window. If a defaultButton is set, it will receive focus, otherwise the window itself will receive focus.")]
        public override void Focus()
        {
            base.Focus();
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public override void Hide()
        {
            base.Hide();
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(Control animateTarget)
        {
            string template = "{0}.hide(\"{1}\");";
            this.AddScript(template, this.ClientID, animateTarget.ClientID);
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(Control animateTarget, string callback)
        {
            string template = "{0}.hide(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, animateTarget.ClientID, callback);
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(Control animateTarget, string callback, string scope)
        {
            string template = "{0}.hide(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, animateTarget.ClientID, callback, scope);
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(string animateTarget)
        {
            string template = "{0}.hide(\"{1}\");";
            this.AddScript(template, this.ClientID, animateTarget);
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(string animateTarget, string callback)
        {
            string template = "{0}.hide(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, animateTarget, callback);
        }

        /// <summary>
        /// Hides the window, setting it to invisible and applying negative offsets.
        /// </summary>
        [Description("Hides the window, setting it to invisible and applying negative offsets.")]
        public virtual void Hide(string animateTarget, string callback, string scope)
        {
            string template = "{0}.hide(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, animateTarget, callback, scope);
        }

        /// <summary>
        /// Fits the window within its current container and automatically replaces the 'maximize' tool button with the 'restore' tool button.
        /// </summary>
        [Description("Fits the window within its current container and automatically replaces the 'maximize' tool button with the 'restore' tool button.")]
        public virtual void Maximize()
        {
            string template = "{0}.maximize();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Placeholder method for minimizing the window. By default, this method simply fires the minimize event since the behavior of minimizing a window is application-specific. To implement custom minimize behavior, either the minimize event can be handled or this method can be overridden.
        /// </summary>
        [Description("Placeholder method for minimizing the window. By default, this method simply fires the minimize event since the behavior of minimizing a window is application-specific. To implement custom minimize behavior, either the minimize event can be handled or this method can be overridden.")]
        public virtual void Minimize()
        {
            string template = "{0}.minimize();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Restores a maximized window back to its original size and position prior to being maximized and also replaces the 'restore' tool button with the 'maximize' tool button.
        /// </summary>
        [Description("Restores a maximized window back to its original size and position prior to being maximized and also replaces the 'restore' tool button with the 'maximize' tool button.")]
        public virtual void Restore()
        {
            string template = "{0}.restore();";
            this.AddScript(template, this.ClientID);
        }

        /// <summary>
        /// Makes this the active window by showing its shadow, or deactivates it by hiding its shadow. This method also fires the activate or deactivate event depending on which action occurred.
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        [Description("Makes this the active window by showing its shadow, or deactivates it by hiding its shadow. This method also fires the activate or deactivate event depending on which action occurred.")]
        public virtual void SetActive(bool active)
        {
            string template = "{0}.setActive({1});";
            this.AddScript(template, this.ClientID, active.ToString().ToLower());
        }

        /// <summary>
        /// Sets the target element from which the window should animate while opening.
        /// </summary>
        [Description("Sets the target element from which the window should animate while opening.")]
        public virtual void SetAnimateTarget(string element)
        {
            string template = "{0}.setAnimateTarget({1});";
            this.AddScript(template, this.ClientID, element);
        }

        /// <summary>
        /// Sets the target element from which the window should animate while opening.
        /// </summary>
        [Description("Sets the target element from which the window should animate while opening.")]
        public virtual void SetAnimateTarget(Control element)
        {
            string template = "{0}.setAnimateTarget({1});";
            this.AddScript(template, this.ClientID, element.ClientID);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(Control animateTarget)
        {
            string template = "{0}.show(\"{1}\");";
            this.AddScript(template, this.ClientID, animateTarget.ClientID);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(Control animateTarget, string callback)
        {
            string template = "{0}.show(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, animateTarget.ClientID, callback);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(Control animateTarget, string callback, string scope)
        {
            string template = "{0}.show(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, animateTarget.ClientID, callback, scope);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(string animateTarget)
        {
            this.AddScript("{0}.show(\"{1}\");", this.ClientID, animateTarget);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(string animateTarget, string callback)
        {
            string template = "{0}.show(\"{1}\",{2});";
            this.AddScript(template, this.ClientID, animateTarget, callback);
        }

        /// <summary>
        /// Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.
        /// </summary>
        [Description("Shows the window, rendering it first if necessary, or activates it and brings it to front if hidden.")]
        public virtual void Show(string animateTarget, string callback, string scope)
        {
            string template = "{0}.show(\"{1}\",{2},{3});";
            this.AddScript(template, this.ClientID, animateTarget, callback, scope);
        }

        /// <summary>
        /// Sends this window to the back of (lower z-index than) any other visible windows
        /// </summary>
        [Description("Sends this window to the back of (lower z-index than) any other visible windows")]
        public virtual void ToBack()
        {
            this.AddScript("{0}.toBack();", this.ClientID);
        }

        /// <summary>
        /// Brings this window to the front of any other visible windows
        /// </summary>
        [Description("Brings this window to the front of any other visible windows")]
        public virtual void ToFront()
        {
            this.AddScript("{0}.toFront();", this.ClientID);
        }

        /// <summary>
        /// A shortcut method for toggling between maximize and restore based on the current maximized state of the window.
        /// </summary>
        [Description("A shortcut method for toggling between maximize and restore based on the current maximized state of the window.")]
        public virtual void ToggleMaximize()
        {
            this.AddScript("{0}.ToggleMaximize();", this.ClientID);
        }

        /// <summary>
        /// Shows the Window in a Modal state.
        /// </summary>
        [Description("Shows the Window in a Modal state.")]
        public virtual void ShowModal()
        {
            this.AddScript("{0}.showModal();", this.ClientID);
        }

        /// <summary>
        /// Shows the Window in a non-Modal state.
        /// </summary>
        [Description("Shows the Window in a non-Modal state.")]
        public virtual void HideModal()
        {
            this.AddScript("{0}.hideModal();", this.ClientID);
        }

        /// <summary>
        /// Toggle the Modal state of the Window. Shows or Hides the body mask. 
        /// </summary>
        [Description("Toggle the Modal state of the Window. Shows or Hides the body mask.")]
        public virtual void ToggleModal()
        {
            this.AddScript("{0}.toggleModal();", this.ClientID);
        }

        /// <summary>
        /// Toggle the Modal state of the Window. Shows or Hides the body mask. 
        /// </summary>
        /// <param name="hide">true to show the body mask.</param>
        [Description("Toggle the Modal state of the Window. Shows or Hides the body mask.")]
        public virtual void ToggleModal(bool show)
        {
            if (show)
            {
                this.ShowModal();
            }
            else
            {
                this.HideModal();
            }
        }
    }
}