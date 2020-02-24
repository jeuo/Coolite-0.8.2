/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Coolite.Ext.Web
{
    /// <summary>
    /// Base Class for any visual Component that uses a box contentContainer.
    /// </summary>
    [Xtype("box")]
    [Description("Base Class for any visual Component that uses a box contentContainer.")]
    public abstract class BoxComponent : Component
    {
        /// <summary>
        /// True to use height:'auto', false to use fixed height (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to use height:'auto', false to use fixed height (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool AutoHeight
        {
            get
            {
                object obj = this.ViewState["AutoHeight"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoHeight"] = value;
            }
        }

        /// <summary>
        /// True to use width:'auto', false to use fixed width (defaults to false).
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(false)]
        [Description("True to use width:'auto', false to use fixed width (defaults to false).")]
        [NotifyParentProperty(true)]    
        public virtual bool AutoWidth
        {
            get
            {
                object obj = this.ViewState["AutoWidth"];
                return (obj == null) ? false : (bool)obj;
            }
            set
            {
                this.ViewState["AutoWidth"] = value;
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
        [NotifyParentProperty(true)]
        new public virtual Unit Height
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Height"], Unit.Empty, "Height");
            }
            set
            {
                this.ViewState["Height"] = value;
            }
        }

        /// <summary>
        /// The page level x coordinate for this component if contained within a positioning container.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The page level x coordinate for this component if contained within a positioning container.")]
        [NotifyParentProperty(true)]
        public virtual Unit PageX
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["PageX"], Unit.Empty, "PageX");
            }
            set
            {
                this.ViewState["PageX"] = value;
            }
        }

        /// <summary>
        /// The page level y coordinate for this component if contained within a positioning container.
        /// </summary>
        [ClientConfig]
        [Category("Config Options")]
        [DefaultValue(typeof(Unit), "")]
        [Description("The page level y coordinate for this component if contained within a positioning container.")]
        [NotifyParentProperty(true)]
        public virtual Unit PageY
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["PageY"], Unit.Empty, "PageY");
            }
            set
            {
                this.ViewState["PageY"] = value;
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
        [NotifyParentProperty(true)]    
        new public virtual Unit Width
        {
            get
            {
                return this.UnitPixelTypeCheck(ViewState["Width"], Unit.Empty, "Width");
            }
            set
            {
                this.ViewState["Width"] = value;
            }
        }

        /// <summary>
        /// The local x (left) coordinate for this component if contained within a positioning container.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The local x (left) coordinate for this component if contained within a positioning container.")]
        public virtual int X
        {
            get
            {
                object obj = this.ViewState["X"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["X"] = value;
            }
        }

        /// <summary>
        /// The local y (addToStart) coordinate for this component if contained within a positioning container.
        /// </summary>
        [ClientConfig(JsonMode.Raw)]
        [Category("Config Options")]
        [DefaultValue(0)]
        [Description("The local y (addToStart) coordinate for this component if contained within a positioning container.")]
        public virtual int Y
        {
            get
            {
                object obj = this.ViewState["Y"];
                return (obj == null) ? 0 : (int)obj;
            }
            set
            {
                this.ViewState["Y"] = value;
            }
        }


        /*  Public Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Sets the page XY position of the component. To set the left and addToStart instead, use setPosition. This method fires the move event.
        /// </summary>
        [Description("Sets the page XY position of the component. To set the left and addToStart instead, use setPosition. This method fires the move event.")]
        public virtual void SetPagePosition(Unit x, Unit y)
        {
            this.SetPagePosition(Convert.ToInt32(x.Value), Convert.ToInt32(y.Value));
        }

        /// <summary>
        /// Sets the page XY position of the component. To set the left and addToStart instead, use setPosition. This method fires the move event.
        /// </summary>
        [Description("Sets the page XY position of the component. To set the left and addToStart instead, use setPosition. This method fires the move event.")]
        public virtual void SetPagePosition(int x, int y)
        {
            this.AddScript("{0}.setPagePosition({1},{2});", this.ClientID, x, y);
        }

        /// <summary>
        /// Sets the left and addToStart of the component. To set the page XY position instead, use setPagePosition. This method fires the move event.
        /// </summary>
        [Description("Sets the left and addToStart of the component. To set the page XY position instead, use setPagePosition. This method fires the move event.")]
        public virtual void SetPosition(int left, int top)
        {
            this.AddScript("{0}.setPosition({1},{2});", this.ClientID, left, top);
        }

        /// <summary>
        /// Sets the left and addToStart of the component. To set the page XY position instead, use setPagePosition. This method fires the move event.
        /// </summary>
        [Description("Sets the left and addToStart of the component. To set the page XY position instead, use setPagePosition. This method fires the move event.")]
        public virtual void SetPosition(Unit left, Unit top)
        {
            this.SetPosition(Convert.ToInt32(left.Value), Convert.ToInt32(top.Value));
        }

        /// <summary>
        /// Sets the width and height of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the width and height of the component. This method fires the resize event.")]
        public virtual void SetSize(int width, int height)
        {
            this.AddScript("{0}.setSize({1},{2});", this.ClientID, width, height);
        }

        /// <summary>
        /// Sets the width and height of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the width and height of the component. This method fires the resize event.")]
        public virtual void SetSize(Unit width, Unit height)
        {
            this.SetSize(Convert.ToInt32(width.Value), Convert.ToInt32(height.Value));
        }

        /// <summary>
        /// Force the component's size to recalculate based on the underlying element's current height and width.
        /// </summary>
        [Description("Force the component's size to recalculate based on the underlying element's current height and width.")]
        public virtual void SyncSize()
        {
            this.AddScript("{0}.syncSize();", this.ClientID);
        }

        /// <summary>
        /// Sets the current box measurements of the component's underlying element.
        /// </summary>
        [Description("Sets the current box measurements of the component's underlying element.")]
        public virtual void UpdateBox(int x, int y, int width, int height)
        {
            this.AddScript("{0}.updateBox({{x:{1},y:{2},width:{3},height:{4}}});", this.ClientID, x, y, width, height);
        }

        /// <summary>
        /// Sets the current box measurements of the component's underlying element.
        /// </summary>
        [Description("Sets the current box measurements of the component's underlying element.")]
        public virtual void UpdateBox(Unit x, Unit y, Unit width, Unit height)
        {
            this.UpdateBox(Convert.ToInt32(x.Value), Convert.ToInt32(y.Value), Convert.ToInt32(width.Value), Convert.ToInt32(height.Value));
        }


        /*  Protected Client Methods
            -----------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Sets the height of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the height of the component. This method fires the resize event.")]
        protected virtual void SetHeight(int height)
        {
            this.AddScript("{0}.setHeight({1});", this.ClientID, height);
        }

        /// <summary>
        /// Sets the height of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the height of the component. This method fires the resize event.")]
        protected virtual void SetHeight(Unit height)
        {
            this.SetHeight(Convert.ToInt32(height.Value));
        }

        /// <summary>
        /// Sets the width of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the width of the component. This method fires the resize event.")]
        protected virtual void SetWidth(int width)
        {
            this.AddScript("{0}.setSize({1});", this.ClientID, width);
        }

        /// <summary>
        /// Sets the width of the component. This method fires the resize event.
        /// </summary>
        [Description("Sets the width of the component. This method fires the resize event.")]
        protected virtual void SetWidth(Unit width)
        {
            this.SetWidth(Convert.ToInt32(width.Value));
        }
    }
}