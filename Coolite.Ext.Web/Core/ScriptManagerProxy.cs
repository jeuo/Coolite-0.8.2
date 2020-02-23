/********
 * @version   : 0.8.2 - Professional Edition (Coolite Professional License)
 * @author    : Coolite Inc. http://www.coolite.com/
 * @date      : 2009-12-21
 * @copyright : Copyright (c) 2006-2009, Coolite Inc. (http://www.coolite.com/). All rights reserved.
 * @license   : See license.txt and http://www.coolite.com/license/. 
 ********/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Coolite.Utilities;

namespace Coolite.Ext.Web
{
    [ToolboxData("<{0}:ScriptManagerProxy runat=\"server\" />")]
    [Designer(typeof(ScriptManagerDesigner))]
    [ToolboxBitmap(typeof(Coolite.Ext.Web.ScriptManager), "Build.Resources.ToolboxIcons.ScriptManagerProxy.bmp")]
    [Description("Coolite ScriptMangerProxy Control. Used when the ScriptManager is inaccessible.")]
    public class ScriptManagerProxy : ScriptManager
    {
        private const string errorMessage = "This global property must be set in the ScriptManager";
        protected override void OnInit(EventArgs e) { }

        protected override object SaveViewState()
        {
            return null;
        }

        protected override void TrackViewState() { }

        protected override void LoadViewState(object savedState) { }

        protected override void Render(HtmlTextWriter writer)
        {
            if(!this.DesignMode)
            {
                this.SimpleRender(writer);
            }
            else
            {
                base.Render(writer);
            }
        }

        public override void AddScript(string script)
        {
            this.ScriptManager.AddScript(script);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if(!this.DesignMode)
            {
                if (!Ext.IsAjaxRequest)
                {
                    this.RegisterEvents(this);
                    this.RegisterCustomListeners();
                    this.RegisterAjaxEvents();
                }
            }
            else
            {
                base.OnPreRender(e);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string AjaxEventUrl
        {
            get { return base.AjaxEventUrl; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ViewStateMode AjaxViewStateMode
        {
            get { return base.AjaxViewStateMode; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ClientProxy AjaxMethodProxy
        {
            get { return base.AjaxMethodProxy; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override IDMode IDMode
        {
            get { return base.IDMode; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool CleanResourceUrl
        {
            get { return base.CleanResourceUrl; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override InitScriptMode InitScriptMode
        {
            get { return base.InitScriptMode; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ResourceLocationType RenderScripts
        {
            get { return base.RenderScripts; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ResourceLocationType RenderStyles
        {
            get { return base.RenderStyles; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string ResourcePath
        {
            get { return base.ResourcePath; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ScriptMode ScriptMode
        {
            get { return base.ScriptMode; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool SourceFormatting
        {
            get { return base.SourceFormatting; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Theme Theme
        {
            get { return base.Theme; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ScriptAdapter ScriptAdapter
        {
            get { return base.ScriptAdapter; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override StateProvider StateProvider
        {
            get { return base.StateProvider; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool QuickTips
        {
            get { return base.QuickTips; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Locale
        {
            get { return base.Locale; }
            set
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}